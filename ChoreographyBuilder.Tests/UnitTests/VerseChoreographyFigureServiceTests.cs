using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests;

[TestFixture]
public class VerseChoreographyFigureServiceTests : UnitTestsBase
{
	private IVerseChoreographyFigureService verseChoreographyFigureService;
	private ILogger<VerseChoreographyFigureService> logger;

	[SetUp]
	public void Setup()
	{
		var mockLogger = new Mock<ILogger<VerseChoreographyFigureService>>();
		this.logger = mockLogger.Object;

		this.verseChoreographyFigureService = new VerseChoreographyFigureService(this.logger, repository, mapper);
	}

	[Test]
	public async Task GetVerseChoreographyFigureForReplace_ShouldReturnValidVerseChoreographyFigureWithCorrectDataWhenIdExists()
	{
		var result = await verseChoreographyFigureService.GetVerseChoreographyFigureForReplaceAsync(FirstVerseChoreographyFirstFigure.Id);

		Assert.Multiple(() =>
		{
			Assert.That(result.Id, Is.EqualTo((FirstVerseChoreography.Id)));
			Assert.That(result.BeatsCount, Is.EqualTo(FirstFigureFirstOption.BeatCounts));
			Assert.That(result.EndPosition, Is.EqualTo(FirstFigureFirstOption.EndPosition.Name));
			Assert.That(result.StartPosition, Is.EqualTo(FirstFigureFirstOption.StartPosition.Name));
			Assert.That(result.FigureOptionId, Is.EqualTo(FirstFigureFirstOption.Id));
			Assert.That(result.FigureName, Is.EqualTo(FirstFigureFirstOption.Figure.Name));
			Assert.That(result.FigureOrder, Is.EqualTo(FirstVerseChoreographyFirstFigure.FigureOrder));
			Assert.That(result.DynamicsType, Is.EqualTo(FirstFigureFirstOption.DynamicsType.ToString()));
			Assert.That(result.IsFavourite, Is.EqualTo(FirstFigure.IsFavourite));
			Assert.That(result.IsHighlight, Is.EqualTo(FirstFigure.IsHighlight));
		});
	}

	[Test]
	public void GetVerseChoreographyFigureForReplace_ShouldThrowExceptionWhenIdDoesntExists()
	{
		Assert.That(async () => await verseChoreographyFigureService.GetVerseChoreographyFigureForReplaceAsync(10),
			Throws.Exception.TypeOf<EntityNotFoundException>());
	}

	[Test]
	public async Task GetPossibleReplacementsForVerseChoreographyFigure_ShouldReturnValidCollectionWhenIdExists()
	{
		var result = await verseChoreographyFigureService.GetPossibleReplacementsForVerseChoreographyFigureAsync(FirstVerseChoreographyFirstFigure.Id);

		Assert.Multiple(() =>
		{
			Assert.IsNotNull(result);
			Assert.That(result.Count(), Is.EqualTo(1));
		});
	}

	[Test]
	public void GetPossibleReplacementsForVerseChoreographyFigure_ShouldThrowExceptionWhenIdDoesntExists()
	{
		Assert.That(async () => await verseChoreographyFigureService.GetPossibleReplacementsForVerseChoreographyFigureAsync(10),
			Throws.Exception.TypeOf<EntityNotFoundException>());
	}

    [Test]
    public async Task GetFigureForDelete_ShouldReturnValidFigureWithCorrectDataWhenIdExists()
    {
        var result = await verseChoreographyFigureService.GetFigureForDeleteAsync(FirstVerseChoreographyFirstFigure.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo((FirstVerseChoreography.Id)));
            Assert.That(result.FigureName, Is.EqualTo(FirstVerseChoreographyFirstFigure.FigureOption.Figure.Name));
            Assert.That(result.VerseChoreographyName, Is.EqualTo(FirstVerseChoreographyFirstFigure.VerseChoreography.Name));
            Assert.That(result.VerseChoreographyId, Is.EqualTo(FirstVerseChoreographyFirstFigure.VerseChoreographyId));
        });
    }

    [Test]
    public void GetFigureForDelete_ShouldThrowExceptionWhenIdDoesntExists()
    {
        Assert.That(async () => await verseChoreographyFigureService.GetFigureForDeleteAsync(100),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
	public async Task GetVerseChoreographyIdForVerseChoreographyFigureById_ShouldReturnValidIdWhenIdExists()
	{
		var result = await verseChoreographyFigureService.GetVerseChoreographyIdForVerseChoreographyFigureByIdAsync(FirstVerseChoreographyFirstFigure.Id);

		Assert.That(result, Is.EqualTo(FirstVerseChoreography.Id));
	}

	[Test]
	public void GetVerseChoreographyIdForVerseChoreographyFigureById_ShouldThrowExceptionWhenIdDoesntExists()
	{
		Assert.That(async () => await verseChoreographyFigureService.GetVerseChoreographyIdForVerseChoreographyFigureByIdAsync(10),
			Throws.Exception.TypeOf<EntityNotFoundException>());
	}

	[Test]
	public async Task VerseChoreographyFigureExistsForThisUserById_ShouldReturnTrueForValidIdForThisUser()
	{
		var result = await verseChoreographyFigureService.VerseChoreographyFigureExistForThisUserByIdAsync(FirstVerseChoreographyFirstFigure.Id, FirstUser.Id);

		Assert.IsTrue(result);
	}

	[Test]
	public async Task VerseChoreographyFigureExistsForThisUserById_ShouldReturnFalseForInvalidId()
	{
		var result = await verseChoreographyFigureService.VerseChoreographyFigureExistForThisUserByIdAsync(10, FirstUser.Id);

		Assert.IsFalse(result);
	}

	[Test]
	public async Task VerseChoreographyFigureExistsForThisUserById_ShouldReturnFalseForValidIdForAnotherUserVerseChoreographyFigure()
	{
		var result = await verseChoreographyFigureService.VerseChoreographyFigureExistForThisUserByIdAsync(FirstVerseChoreographyFirstFigure.Id, SecondUser.Id);

		Assert.IsFalse(result);
	}

    [Test]
    public async Task FigureIsLastForVerseChoreographyByIdAsync_ShouldReturnTrueForValidIdOfLastFigure()
    {
        var result = await verseChoreographyFigureService.FigureIsLastForVerseChoreographyByIdAsync(FirstVerseChoreographyThirdFigure.Id);

        Assert.IsTrue(result);
    }

    [Test]
    public async Task FigureIsLastForVerseChoreographyByIdAsync_ShouldReturnFalseForValidIdOfNonLastFigure()
    {
        var result = await verseChoreographyFigureService.FigureIsLastForVerseChoreographyByIdAsync(FirstVerseChoreographySecondFigure.Id);

        Assert.IsFalse(result);
    }
}
