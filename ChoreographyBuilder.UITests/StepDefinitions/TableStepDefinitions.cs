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

    [StepDefinition($"I search in the table by (.*) search term in the (.*) search field")]
    public void SearchInTableBySearchTerm(string searchTerm, string fieldName)
    {
        basePage.SearchInSearchFieldInTableBySearchTerm(fieldName, searchTerm);
    }

    [StepDefinition(@"I clear search field (.*)")]
    public void ClearSearchField(string fieldName)
    {
        basePage.ClearSearchField(fieldName);
    }

    [StepDefinition(@"I search in the table by (.*) dropdown option in (.*) dropdown")]
    public void ISearchInTableByDropdownOption(string option, string dropdownName)
    {
        basePage.SelectOptionInDropdownForSearch(option, dropdownName);
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

    [Then(@"assert that I see element with text (.*)")]
    public void AssertThatISeeElementWithText(string text)
    {
        bool isVisible = basePage.IsElementWithTextVisible(text);
        Assert.IsTrue(isVisible, $"Expected to see element with text '{text}', but it was not found.");
    }
}
