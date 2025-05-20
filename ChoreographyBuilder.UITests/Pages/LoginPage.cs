using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class LoginPage : BasePage
{
    private Credentials credentials;

    public LoginPage(AppSettings settings, IWebDriver driver, WebDriverWait wait, Credentials credentials) : base(settings, driver, wait)
    {
        this.credentials = credentials;
    }

    private IWebElement EmailField => driver.FindElement(EmailFieldBy);
    private By EmailFieldBy => By.Id("email");

    private IWebElement PasswordField => driver.FindElement(PasswordFieldBy);
    private By PasswordFieldBy => By.Id("password");

    private IWebElement LoginButton => driver.FindElement(LoginButtonBy);
    private By LoginButtonBy => By.Id("login-submit");

    public void LogInAsUser(string user)
    {
        GetUserCredentialsByUsername(user);

		OpenPage("Identity/Account/Login");
        FillEmailField(Variables.currentUser.Email);
        FillPasswordField(Variables.currentUser.Password);
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

    private void GetUserCredentialsByUsername(string username)
    {
        switch (username)
        {
            case "AdminUser":
                Variables.currentUser =  credentials.AdminUser();
                break;
            case "FirstUser":
                Variables.currentUser = credentials.FirstUser();
                break;
            case "SecondUser":
                Variables.currentUser = credentials.SecondUser();
                break;
        }
    }
}
