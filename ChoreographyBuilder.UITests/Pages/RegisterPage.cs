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

    private IWebElement RegisterButton => driver.FindElement(RegisterButtonBy);
    private By RegisterButtonBy => By.Id("registerSubmit");

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

    public void FillConfirmPasswordField(string confirmPassword)
    {
        ConfirmPasswordField.Clear();
        ConfirmPasswordField.SendKeys(confirmPassword);
    }

    public void ClickRegisterButton()
        => RegisterButton.Click();

    public void FillRegisterForm(string email, string password, string confirmPassword)
    {
        FillEmailField(email);
        FillPasswordField(password);
        FillConfirmPasswordField(confirmPassword);
    }
}
