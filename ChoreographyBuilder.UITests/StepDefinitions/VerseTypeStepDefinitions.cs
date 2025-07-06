using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Pages;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.StepDefinitions;

public class VerseTypeStepDefinitions : BaseStepDefinitions
{
    private readonly VerseTypePage verseTypePage;

    public VerseTypeStepDefinitions(VerseTypePage verseTypePage) : base()
    {
        this.verseTypePage = verseTypePage;
    }

    [Then(@"I have asserted that a verse type with name (.*), beats count (.*), that (.*) active exists")]
    public void AssertVerseTypeWithNameBeatsCountExists(string name, int beatsCount, string active)
    {
        bool isActive = GetBooleanFromString(active);
        VerseType? verseType = verseTypePage.GetVerseTypeFromDbByName(name);

        Assert.NotNull(verseType, $"Verse type with name '{name}' does not exist in the database.");

        Assert.Multiple(() =>
        {
            Assert.That(verseType.BeatCounts, Is.EqualTo(beatsCount), $"Expected verse type beat counts '{beatsCount}', but found '{verseType.BeatCounts}'.");
            Assert.That(verseType.IsActive, Is.EqualTo(isActive));
        });
    }

    [Then(@"assert that the first position in the table has name (.*)")]
    public void AssertFirstPositionInTableHasName(string name)
    {
        PositionFromTableModel? position = positionPage.GetPositionsFromTable()[0];
        Assert.That(position.Name, Is.EqualTo(name));
    }
}
