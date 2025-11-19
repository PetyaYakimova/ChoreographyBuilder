using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class FigureStepDefinitions : BaseStepDefinitions
{
    private readonly FigurePage figurePage;

    public FigureStepDefinitions(FigurePage figurePage) : base()
    {
        this.figurePage = figurePage;
    }

    [Then(@"I have asserted that a figure with name (.*), that (.*) highlight, that (.*) favourite, that (.*) shared exists")]
    public void AssertFigureWithDataExists(string name, string highlight, string favourite, string shared)
    {
        bool isHighlight = GetBooleanFromString(highlight);
        bool isFavourite = GetBooleanFromString(favourite);
        bool isShared = GetBooleanFromString(shared);
        Figure? figure = figurePage.GetFigureFromDbByName(name);

        Assert.NotNull(figure, $"Figure with name '{name}' does not exist in the database.");

        Assert.Multiple(() =>
        {
            Assert.That(figure.IsHighlight, Is.EqualTo(isHighlight));
            Assert.That(figure.IsFavourite, Is.EqualTo(isFavourite));
            Assert.That(figure.CanBeShared, Is.EqualTo(isShared));
        });
    }

    [Then(@"I have asserted that a figure option for figure with name (.*), that has (.*) beats counts, that has start position (.*), that has end position (.*) and has dynamics type (.*) exists")]
    public void AssertFigureOptionWithDataExists(string figureName, int beatsCount, string startPosition, string endPosition, string dynamicsType)
    {
        FigureOption? figure = figurePage.GetFigureFromDbByName(name);

        Assert.NotNull(figure, $"Figure with name '{name}' does not exist in the database.");

        Assert.Multiple(() =>
        {
            Assert.That(figure.IsHighlight, Is.EqualTo(isHighlight));
            Assert.That(figure.IsFavourite, Is.EqualTo(isFavourite));
            Assert.That(figure.CanBeShared, Is.EqualTo(isShared));
        });
    }

}
