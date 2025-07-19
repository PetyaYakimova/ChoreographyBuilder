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

    [StepDefinition($"I search in the table by (.*) search term")]
    public void SearchInTableBySearchTerm(string searchTerm)
    {
        basePage.SearchInTableBySearchTerm(searchTerm);
    }

    [StepDefinition(@"I click the (.*) button")]
    public void IClickTheButton(string buttonName)
    {
        basePage.ClickButtonWithValue(buttonName);
    }

    [Then(@"assert that the table has at least (.*) rows")]
    public void AssertThatTheTableHasAtLeastRows(int expectedRowCount)
    {
        int actualRowCount = basePage.GetNumberOfRowsInTable();
        Assert.That(actualRowCount, Is.GreaterThanOrEqualTo(expectedRowCount),
            $"Expected at least {expectedRowCount} rows, but found {actualRowCount}.");
    }

    [Then(@"assert that row with (.*) is (.*) in the table")]
    public void AssertThatRowIsVisibleInTheTable(string value, string isVisible)
    {
        bool expectedVisibility = GetBooleanFromString(isVisible);
        bool actualVisibility = basePage.IsRowWithValueVisible(value);

        Assert.That(actualVisibility, Is.EqualTo(expectedVisibility),
            $"Expected row with value '{value}' to be {(expectedVisibility ? "visible" : "not visible")}, but it was {(actualVisibility ? "visible" : "not visible")}.");
    }

    [Then(@"assert that the table has columns with names (.*)")]
    public void AssertThatTheTableHasColumnsWithNames(string columnNames)
    {
        var expectedColumnNames = columnNames.Split(',').Select(name => name.Trim()).ToList();
        var actualColumnNames = basePage.GetTableColumnNames();
        CollectionAssert.AreEqual(expectedColumnNames, actualColumnNames);
    }
}
