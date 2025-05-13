using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class LoginStepDefinitions : BaseStepDefinitions
{
    private readonly LoginPage loginPage;

    public LoginStepDefinitions(LoginPage loginPage) : base()
    {
        this.loginPage = loginPage;
    }

    [StepDefinition($"I log in as (.*)")]
    public void ILogInAs(string user)
    {
        loginPage.LogInAsUser(user);
    }
}
