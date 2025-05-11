using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class LoginPage : BasePage
{
    public LoginPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    private IWebElement EmailField => driver.FindElement(EmailFieldBy);
    private By EmailFieldBy => By.Id("email");

    private IWebElement PasswordField => driver.FindElement(PasswordFieldBy);
    private By PasswordFieldBy => By.Id("password");

    private IWebElement LoginButton => driver.FindElement(LoginButtonBy);
    private By LoginButtonBy => By.Id("login-submit");

    public void LoginAsUser(string user)
    {
        OpenPage("Identity/Account/Login");
        FillEmailField(email);
        FillPasswordField(password);
        ClickLoginButton();
    }

    public void FillEmailField(string email)
    {
        EmailField.Clear();
        EmailField.SendKeys(email);
    }

    public void FillPasswordField(string password)
    {
        PasswordField.Clear();
        PasswordField.SendKeys(password);
    }

    public void ClickLoginButton()
        => LoginButton.Click();
}
