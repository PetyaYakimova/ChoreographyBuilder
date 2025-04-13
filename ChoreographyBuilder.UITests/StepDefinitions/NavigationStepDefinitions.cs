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
}
