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

    [StepDefinition(@"I click on (.*) in the header navigation")]
    public void IClickOnMenuInHeaderNavigation(string menuName)
    {
        basePage.ClickOnMenu(menuName);
    }

    [StepDefinition(@"I click on the site logo")]
    public void IClickOnTheSiteLogo()
    {
        basePage.ClickOnSiteLogo();
    }

    [Then(@"assert that I am on (.*) page")]
    public void AssertThatIAmOnPage(string pageName)
    {
        string actualPage = basePage.GetCurrentPage();
        Assert.That(actualPage, Is.EqualTo(pageName));
    }

    [Then(@"assert that the menus I see in the header are (.*)")]
    public void AssertThatTheMenusISeeInTheHeaderAre(string menus)
    {
        List<string> expectedMenus = menus.Split(',').Select(m => m.Trim()).ToList();

        List<string> actualMenus = basePage.GetHeaderMenus();

        CollectionAssert.AreEqual(expectedMenus, actualMenus);
    }
}
