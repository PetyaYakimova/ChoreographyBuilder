using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class FormStepDefinitions : BaseStepDefinitions
{
    private readonly BasePage basePage;

    public FormStepDefinitions(BasePage basePage) : base()
    {
        this.basePage = basePage;
    }

    [Then(@"assert that I see validation error message for (.*) field with text (.*)")]
    public void AssertThatISeeValidationErrorMessageForFieldWithText(string fieldName, string expectedMessage)
    {
        string actualMessage = basePage.GetValidationErrorMessage(fieldName);
        Assert.That(actualMessage, Is.EqualTo(expectedMessage),
            $"Expected validation error message for field '{fieldName}' to be '{expectedMessage}', but found '{actualMessage}'.");
    }

    [Then(@"assert that I see toaster message with text (.*)")]
    public void AssertThatISeeToasterMessageWithText(string expectedMessage)
    {
        string actualMessage = basePage.GetToasterMessage();
        Assert.That(actualMessage, Is.EqualTo(expectedMessage),
            $"Expected toaster message '{expectedMessage}', but found '{actualMessage}'.");
    }
}
