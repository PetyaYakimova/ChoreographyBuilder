using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class TableStepDefinitions : BaseStepDefinitions
{
    private readonly BasePage basePage;

    public TableStepDefinitions(BasePage basePage) : base()
    {
        this.basePage = basePage;
    }

    [StepDefinition($"I search in the table by (.*)")]
    public void SearchInTableBySearchTerm(string searchTerm)
    {
        basePage.SearchInTableBySearchTerm(searchTerm);
    }

    [Then(@"assert that the table has at least (.*) rows")]
    public void AssertThatTheTableHasAtLeastRows(int expectedRowCount)
    {
        int actualRowCount = basePage.GetNumberOfRowsInTable();
        Assert.That(actualRowCount, Is.GreaterThanOrEqualTo(expectedRowCount),
            $"Expected at least {expectedRowCount} rows, but found {actualRowCount}.");
    }
}
