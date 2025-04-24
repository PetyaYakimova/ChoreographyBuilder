using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class NavigationStepDefinitions : BaseStepDefinitions
{
    private readonly BasePage basePage;

    public NavigationStepDefinitions(BasePage basePage) : base()
    {
        this.basePage = basePage;
    }

    [StepDefinition($"I open the (.*) page")]
    public void OpenPage(string pageName)
    {
        basePage.OpenPage(pageName);
    }

    [Then(@"assert that I am on (.*) page")]
    public void AssertThatIAmOnPage(string pageName)
    {
        string actualPage = basePage.GetCurrentPage();
        Assert.That(actualPage, Is.EqualTo(pageName));
    }
}
