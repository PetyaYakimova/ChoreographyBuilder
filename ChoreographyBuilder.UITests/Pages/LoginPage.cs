using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class LoginPage : BasePage
{
    public LoginPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }
}
