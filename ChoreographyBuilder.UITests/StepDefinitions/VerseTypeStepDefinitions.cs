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
            Assert.That(verseType.BeatCounts, Is.EqualTo(beatsCount));
            Assert.That(verseType.IsActive, Is.EqualTo(isActive));
        });
    }

    [Then(@"assert that the first verse type in the table has name (.*) and beats count (.*)")]
    public void AssertFirstVerseTypeInTableHasNameAndBeatsCount(string name, string beatsCount)
    {
        VerseTypeFromTableModel? verseType = verseTypePage.GetVerseTypesFromTable()[0];

        Assert.Multiple(() =>
        {
            Assert.That(verseType.Name, Is.EqualTo(name));
            Assert.That(verseType.BeatCounts, Is.EqualTo(beatsCount));
        });
    }
}
