using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class UserStepDefinitions : BaseStepDefinitions
{
    private readonly UserPage userPage;

    public UserStepDefinitions(UserPage userPage) : base()
    {
        this.userPage = userPage;
    }

    [Then(@"assert that the first user in the table has email (.*), at least (.*) figures, at least (.*) verse choreographies, at least (.*) full choreographies")]
    public void AssertFirstUserInTableHasData(string email, int minFigures, int minVerseChoreographies, int minFullChoreographies)
    {
        UserFromTableModel? user = userPage.GetUsersFromTable()[0];

        Assert.Multiple(() =>
        {
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.Figures, Is.GreaterThanOrEqualTo(minFigures));
            Assert.That(user.VerseChoreographies, Is.GreaterThanOrEqualTo(minVerseChoreographies));
            Assert.That(user.FullChoreographies, Is.GreaterThanOrEqualTo(minFullChoreographies));
        });
    }
}
