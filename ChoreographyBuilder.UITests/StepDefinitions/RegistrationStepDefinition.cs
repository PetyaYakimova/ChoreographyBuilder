using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class RegistrationStepDefinition : BaseStepDefinitions
{
    private readonly RegisterPage registerPage;

    public RegistrationStepDefinition(RegisterPage registerPage) : base()
    {
        this.registerPage = registerPage;
    }

    [StepDefinition($"I fill the registration form with email (.*), password (.*), confirm password (.*)")]
    public void IFillRegistrationFormWithData(string email, string password, string confirmPassword)
    {
        registerPage.FillRegisterForm(email, password, confirmPassword);
    }

    [StepDefinition(@"I click the Register button")]
    public void ClickTheRegisterButton()
    {
        registerPage.ClickRegisterButton();
    }

    [Then(@"assert that I see email (.*) in the header")]
    public void AssertThatISeeEmailInTheHeader(string email)
    {
        string actualText = registerPage.GetGreetingFromHeader();
        Assert.That(actualText.Contains(email), Is.True);
    }
}
