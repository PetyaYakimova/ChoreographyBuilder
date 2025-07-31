using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class StatsStepDefinitions : BaseStepDefinitions
{
    private readonly StatsPage statsPage;

    public StatsStepDefinitions(StatsPage statsPage) : base()
    {
        this.statsPage = statsPage;
    }

    [StepDefinition(@"assert that I see at least (.*) for the (.*) label")]
    public void AssertThatISeeAtLeastValueForLabel(int expectedMinValue, string label)
    {
        int actualValue = int.Parse(statsPage.GetValueForLabel(label));
        Assert.That(actualValue, Is.GreaterThanOrEqualTo(expectedMinValue),
            $"Expected value for label '{label}' to be at least {expectedMinValue}, but found {actualValue}.");
    }
}
