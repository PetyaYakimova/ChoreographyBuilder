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
}
