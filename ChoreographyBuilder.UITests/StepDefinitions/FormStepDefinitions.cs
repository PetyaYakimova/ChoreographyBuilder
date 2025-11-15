using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class FormStepDefinitions : BaseStepDefinitions
{
    private readonly FormPage formPage;

    public FormStepDefinitions(FormPage formPage) : base()
    {
        this.formPage = formPage;
    }

    [StepDefinition(@"I fill the position form with name (.*)")]
    public void IFillThePositionFormWithData(string name)
    {
        formPage.FillField("Name", name);
    }

    [StepDefinition(@"I fill the verse type form with name (.*), beat counts (.*)")]
    public void IFillTheVerseTypeFormWithData(string name, string beatsCount)
    {
        formPage.FillField("Name", name);
        formPage.FillField("BeatCounts", beatsCount);
    }

    [StepDefinition(@"I fill the figure form with name (.*), that (.*) highlight, that (.*) favourite, that (.*) shared with other users")]
    public void IFillTheFigureFormWithData(string name, string isHighlight, string isFavourite, string isShared)
    {
        bool isHighlightBool = GetBooleanFromString(isHighlight);
        bool isFavouriteBool = GetBooleanFromString(isFavourite);
        bool isSharedBool = GetBooleanFromString(isShared);

        formPage.FillField("Name", name);
        if (isHighlightBool)
        {
            formPage.ClickCheckbox("IsHighlight");
        }

        if (isFavouriteBool)
        {
            formPage.ClickCheckbox("IsFavourite");
        }

        if (isSharedBool)
        {
            formPage.ClickCheckbox("CanBeShared");
        }
    }

    [StepDefinition(@"I fill the figure option form with start position (.*), end position (.*), beats count (.*), dynamics type (.*)")]
    public void IFillTheFigureOptionFormWithData(string startPosition, string endPosition, string beatsCount, string dynamicsType)
    {
        formPage.SelectOptionInDropdown(startPosition, "StartPosition");
        formPage.SelectOptionInDropdown(endPosition, "EndPosition");
        formPage.FillField("BeatCounts", beatsCount);
        formPage.SelectOptionInDropdown(dynamicsType, "DynamicsType");
    }

    [StepDefinition(@"I clear the (.*) field")]
    public void IClearField(string fieldName)
    {
        formPage.ClearField(fieldName);
    }

    [StepDefinition(@"I fill the (.*) field with (.*)")]
    public void IFillFieldWithValue(string fieldName, string value)
    {
        formPage.FillField(fieldName, value);
    }

    [Then(@"assert that I see validation error message for (.*) field with text (.*)")]
    public void AssertThatISeeValidationErrorMessageForFieldWithText(string fieldName, string expectedMessage)
    {
        string actualMessage = formPage.GetValidationErrorMessage(fieldName);
        Assert.That(actualMessage, Is.EqualTo(expectedMessage),
            $"Expected validation error message for field '{fieldName}' to be '{expectedMessage}', but found '{actualMessage}'.");
    }

    [Then(@"assert that I see toaster message with text (.*)")]
    public void AssertThatISeeToasterMessageWithText(string expectedMessage)
    {
        string actualMessage = formPage.GetToasterMessage();
        Assert.That(actualMessage, Is.EqualTo(expectedMessage),
            $"Expected toaster message '{expectedMessage}', but found '{actualMessage}'.");
    }
}
