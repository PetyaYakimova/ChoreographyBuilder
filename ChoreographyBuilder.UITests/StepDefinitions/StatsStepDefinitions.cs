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

    [StepDefinition(@"I click the See all positions link")]
    public void ClickSeeAllPositionsLink()
    {
        statsPage.ClickSeeAllPositionsLink();
    }

    [StepDefinition(@"I click the See all verse types link")]
    public void ClickSeeAllVerseTypesLink()
    {
        statsPage.ClickSeeAllVerseTypesLink();
    }

    [StepDefinition(@"I click the See my figures link")]
    public void ClickSeeMyFiguresLink()
    {
        statsPage.ClickSeeMyFiguresLink();
    }

    [Then(@"assert that I see at least (.*) for the (.*) label")]
    public void AssertThatISeeAtLeastValueForLabel(int expectedMinValue, string label)
    {
        int actualValue = int.Parse(statsPage.GetValueForLabel(label));
        Assert.That(actualValue, Is.GreaterThanOrEqualTo(expectedMinValue),
            $"Expected value for label '{label}' to be at least {expectedMinValue}, but found {actualValue}.");
    }
}
