using ChoreographyBuilder.Infrastructure.Data.Models;
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

    [StepDefinition(@"I fill the name field for position with (.*)")]
    public void IFillTheNameFieldForPositionWith(string name)
    {
        positionPage.FillNameField(name);
    }

    [Then(@"I have asserted that a position with name (.*) that (.*) active exists")]
    public void AssertPositionWithNameExists(string name, string active)
    {
        bool isActive = GetBooleanFromString(active);
        Position? position = positionPage.GetPositionFromDbByName(name);

        Assert.NotNull(position, $"Position with name '{name}' does not exist in the database.");

        Assert.That(position.IsActive, Is.EqualTo(isActive));
    }
}
