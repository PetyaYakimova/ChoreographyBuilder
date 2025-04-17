using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class RegisterPage : BasePage
{
    public RegisterPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    private IWebElement EmailField => driver.FindElement(EmailFieldBy);
    private By EmailFieldBy => By.Id("email");

    private IWebElement PasswordField => driver.FindElement(PasswordFieldBy);
    private By PasswordFieldBy => By.Id("password");

    private IWebElement ConfirmPasswordField => driver.FindElement(ConfirmPasswordFieldBy);
    private By ConfirmPasswordFieldBy => By.Id("confirmPassword");
}
