﻿using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class PositionStepDefinitions : BaseStepDefinitions
{
    private readonly PositionPage positionPage;

    public PositionStepDefinitions(PositionPage positionPage) : base()
    {
        this.positionPage = positionPage;
    }

    [Then(@"I have asserted that a position with name (.*) that (.*) active exists")]
    public void AssertPositionWithNameExists(string name, string active)
    {
        bool isActive = GetBooleanFromString(active);
        Position? position = positionPage.GetPositionFromDbByName(name);

        Assert.NotNull(position, $"Position with name '{name}' does not exist in the database.");

        Assert.That(position.IsActive, Is.EqualTo(isActive));
    }

    [Then(@"assert that the first position in the table has name (.*)")]
    public void AssertFirstPositionInTableHasName(string name)
    {
        PositionFromTableModel? position = positionPage.GetPositionsFromTable()[0];
        Assert.That(position.Name, Is.EqualTo(name));
    }
}
