using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class StatsPage : BasePage
{
    public StatsPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    private IWebElement SeeAllPositionsLink => driver.FindElement(SeeAllPositionsLinkBy);
    private By SeeAllPositionsLinkBy => By.Id("See all positions");

    private IWebElement SeeAllVerseTypesLink => driver.FindElement(SeeAllVerseTypesLinkBy);
    private By SeeAllVerseTypesLinkBy => By.Id("See all verse types");

    private IWebElement SeeMyFiguresLink => driver.FindElement(SeeMyFiguresLinkBy);
    private By SeeMyFiguresLinkBy => By.Id("See my figures");

    public string GetValueForLabel(string label)
        => driver.FindElement(By.XPath($"//label[starts-with(text(), '{label}')]/../p")).Text;

    public void ClickSeeAllPositionsLink()
        => SeeAllPositionsLink.Click();

    public void ClickSeeAllVerseTypesLink()
        => SeeAllVerseTypesLink.Click();

    public void ClickSeeMyFiguresLink()
        => SeeMyFiguresLink.Click();
}
