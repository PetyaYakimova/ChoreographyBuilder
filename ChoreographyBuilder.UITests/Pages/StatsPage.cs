using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class StatsPage : BasePage
{
    public StatsPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    public string GetValueForLabel(string label)
        => driver.FindElement(By.XPath($"//label[starts-with(text(), '{label}')]/../p")).Text;
}
