using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class BasePage
{
    protected readonly AppSettings settings;
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;

    protected BasePage(
        AppSettings settings,
        IWebDriver driver,
        WebDriverWait wait)
    {
        this.settings = settings;
        this.driver = driver;
        this.wait = wait;
    }

    private IWebElement HeaderGreetingText => driver.FindElement(HeaderGreetingTextBy);
    private By HeaderGreetingTextBy => By.Id("greeting");

    public void OpenHomePage()
        => driver.Navigate().GoToUrl(settings.DomainSettings.Domain);

    public void OpenPage(string pageName)
        => driver.Navigate().GoToUrl(settings.DomainSettings.Domain + pageName);

    public string GetCurrentURL()
        => driver.Url;

    public string GetCurrentPage()
        => GetCurrentURL().Replace(settings.DomainSettings.Domain, string.Empty);

    public string GetGreetingFromHeader()
        => HeaderGreetingText.Text;

    public bool DoesElementExistAndIsDisplayed(By locator)
    {
        try
        {
            return driver.FindElement(locator).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
