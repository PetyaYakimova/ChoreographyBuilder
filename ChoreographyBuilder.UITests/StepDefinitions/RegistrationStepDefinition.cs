using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class RegistrationStepDefinition : BaseStepDefinitions
{
    private readonly RegisterPage registerPage;

    public RegistrationStepDefinition(RegisterPage registerPage) : base()
    {
        this.registerPage = registerPage;
    }

    [StepDefinition($"I fill the registration form with email (.*), password (.*), confirm password (.*)")]
    public void IFillRegistrationFormWithData(string email, string password, string confirmPassword)
    {
        registerPage.FillRegisterForm(email, password, confirmPassword);
    }

    [StepDefinition(@"I click the Register button")]
    public void ClickTheRegisterButton()
    {
        registerPage.ClickRegisterButton();
    }

    [Then(@"assert that I see email (.*) in the header")]
    public void AssertThatISeeEmailInTheHeader(string email)
    {
        string actualText = registerPage.GetGreetingFromHeader();
        Assert.That(actualText.Contains(email), Is.True);
    }

    [Then(@"I have asserted that a new user with email (.*) is saved")]
    public void IHaveAssertedThatANewUserWithEmailIsSaved(string email)
    {
        Assert.That(registerPage.IsUserSaved(email), Is.True);
    }

    [Then(@"assert that I see validation error message for the email field with text (.*)")]
    public void AssertThatISeeValidationMessageForEmailFieldWithText(string expectedText)
    {
        string actualText = registerPage.GetEmailFieldErrorText();
        Assert.That(actualText, Is.EqualTo(expectedText));
    }

    [Then(@"assert that I see validation error message for the password field with text (.*)")]
    public void AssertThatISeeValidationMessageForPasswordFieldWithText(string expectedText)
    {
        string actualText = registerPage.GetPasswordFieldErrorText();
        Assert.That(actualText, Is.EqualTo(expectedText));
    }

    [Then(@"assert that I see validation error message for the confirm password field with text (.*)")]
    public void AssertThatISeeValidationMessageForConfirmPasswordFieldWithText(string expectedText)
    {
        string actualText = registerPage.GetConfirmPasswordFieldErrorText();
        Assert.That(actualText, Is.EqualTo(expectedText));
    }
}
