using ChoreographyBuilder.UITests.Repositories;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class RegisterPage : BasePage
{
    private UserRepository userRepository;

    public RegisterPage(AppSettings settings, IWebDriver driver, WebDriverWait wait, UserRepository userRepository) : base(settings, driver, wait)
    {
        this.userRepository = userRepository;
    }

    private IWebElement EmailField => driver.FindElement(EmailFieldBy);
    private By EmailFieldBy => By.Id("email");

    private IWebElement EmailFieldError => driver.FindElement(EmailFieldErrorBy);
    private By EmailFieldErrorBy => By.Id("email-error");

    private IWebElement PasswordField => driver.FindElement(PasswordFieldBy);
    private By PasswordFieldBy => By.Id("password");

    private IWebElement ConfirmPasswordField => driver.FindElement(ConfirmPasswordFieldBy);
    private By ConfirmPasswordFieldBy => By.Id("confirmPassword");

    private IWebElement ConfirmPasswordFieldError => driver.FindElement(ConfirmPasswordFieldErrorBy);
    private By ConfirmPasswordFieldErrorBy => By.Id("confirmPassword-error");

    private IWebElement RegisterButton => driver.FindElement(RegisterButtonBy);
    private By RegisterButtonBy => By.Id("registerSubmit");

    public void FillEmailField(string email)
    {
        EmailField.Clear();
        EmailField.SendKeys(email);
    }

    public string GetEmailFieldErrorText()
        => EmailFieldError.Text;

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

    public string GetConfirmPasswordFieldErrorText()
        => ConfirmPasswordFieldError.Text;

    public void ClickRegisterButton()
        => RegisterButton.Click();

    public void FillRegisterForm(string email, string password, string confirmPassword)
    {
        FillEmailField(email);
        FillPasswordField(password);
        FillConfirmPasswordField(confirmPassword);
    }

    public bool IsUserSaved(string email)
        => userRepository.IsUserSaved(email);
}
