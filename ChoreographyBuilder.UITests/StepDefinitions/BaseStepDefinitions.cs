using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

[Binding]
public class BaseStepDefinitions
{
    public BaseStepDefinitions()
    {

    }

    protected bool GetBooleanFromString(string value)
    {
        return value.ToLower().Contains("not") ? false : true;
    }
}
