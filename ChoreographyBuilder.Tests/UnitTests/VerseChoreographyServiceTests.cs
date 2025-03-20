using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using ChoreographyBuilder.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Tests.UnitTests;

[TestFixture]
public class VerseChoreographyServiceTests : UnitTestsBase
{
    private IVerseChoreographyService verseChoreographyService;
    private ILogger<VerseChoreographyService> logger;

    [SetUp]
    public void Setup()
    {
        var mockLogger = new Mock<ILogger<VerseChoreographyService>>();
        this.logger = mockLogger.Object;

        this.verseChoreographyService = new VerseChoreographyService(this.logger, repository, mapper);
    }

    [Test]
    public async Task GetVerseChoreographyById_ShouldReturnValidVerseChoreographyWithCorrectDataWhenIdExists()
    {
        var result = await verseChoreographyService.GetVerseChoreographyByIdAsync(FirstVerseChoreography.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo((FirstVerseChoreography.Id)));
            Assert.That(result.Name, Is.EqualTo(FirstVerseChoreography.Name));
            Assert.That(result.Figures.Count(), Is.EqualTo(FirstVerseChoreography.Figures.Count()));
            Assert.That(result.NumberOfFigures, Is.EqualTo(FirstVerseChoreography.Figures.Count()));
            Assert.That(result.FinalFigureName, Is.EqualTo(HighlightFigure.Name));
            Assert.That(result.StartPositionName, Is.EqualTo(FirstPosition.Name));
            Assert.That(result.EndPositionName, Is.EqualTo(FirstPosition.Name));
        });
    }

    [Test]
    public void GetVerseChoreographyById_ShouldThrowExceptionWhenIdDoesntExists()
    {
        Assert.That(async () => await verseChoreographyService.GetVerseChoreographyByIdAsync(10),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task GetVerseChoreographyForDelete_ShouldReturnValidVerseChoreographyWithCorrectDataWhenIdExists()
    {
        var result = await verseChoreographyService.GetVerseChoreographyForDeleteAsync(FirstVerseChoreography.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo((FirstVerseChoreography.Id)));
            Assert.That(result.Name, Is.EqualTo(FirstVerseChoreography.Name));
            Assert.That(result.NumberOfFigures, Is.EqualTo(FirstVerseChoreography.Figures.Count()));
        });
    }

    [Test]
    public void GetVerseChoreographyForDelete_ShouldThrowExceptionWhenIdDoesntExists()
    {
        Assert.That(async () => await verseChoreographyService.GetVerseChoreographyForDeleteAsync(10),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task GetLastFigureEndPosition_ShouldReturnValidPositionWhenChoreoHasFigures()
    {
        var result = await verseChoreographyService.GetLastFigureEndPositionAsync(FirstVerseChoreography.Id);

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo((FirstPosition.Id)));
            Assert.That(result.Name, Is.EqualTo(FirstPosition.Name));
        });
    }

    [Test]
    public async Task GetLastFigureEndPosition_ShouldReturnNullWhenChoreoHasNoFigures()
    {
        var result = await verseChoreographyService.GetLastFigureEndPositionAsync(FourthVerseChoreography.Id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetNumberOfFiguresForVerseChoreography_ShouldReturnValidNumberWhenChoreoExists()
    {
        var result = await verseChoreographyService.GetNumberOfFiguresForVerseChoreographyAsync(FirstVerseChoreography.Id);

        Assert.That(result, Is.EqualTo(FirstVerseChoreography.Figures.Count()));
    }

    [Test]
    public void GetNumberOfFiguresForVerseChoreography_ShouldThrowExceptionWhenIdDoesntExists()
    {
        Assert.That(async () => await verseChoreographyService.GetNumberOfFiguresForVerseChoreographyAsync(10),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task GetNumberOfRemainingBeatsForVerseChoreography_ShouldReturnValidNumberWhenChoreoExists()
    {
        var result = await verseChoreographyService.GetNumberOfRemainingBeatsForVerseChoreographyAsync(FourthVerseChoreography.Id);

        Assert.That(result, Is.EqualTo(FirstVerseType.BeatCounts));
    }

    [Test]
    public void GetNumberOfRemainingBeatsForVerseChoreography_ShouldThrowExceptionWhenIdDoesntExists()
    {
        Assert.That(async () => await verseChoreographyService.GetNumberOfRemainingBeatsForVerseChoreographyAsync(10),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task GetStartPositionNameForVerseChoreography_ShouldReturnValidNameWhenChoreoExistsAndHasFigures()
    {
        var result = await verseChoreographyService.GetStartPositionNameForVerseChoreographyAsync(FirstVerseChoreography.Id);

        Assert.That(result, Is.EqualTo(FirstPosition.Name));
    }

    [Test]
    public async Task GetStartPositionNameForVerseChoreography_ShouldReturnNullWhenChoreoExistsButHasNoFigures()
    {
        var result = await verseChoreographyService.GetStartPositionNameForVerseChoreographyAsync(FourthVerseChoreography.Id);

        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void GetStartPositionNameForVerseChoreography_ShouldThrowExceptionWhenIdDoesntExists()
    {
        Assert.That(async () => await verseChoreographyService.GetStartPositionNameForVerseChoreographyAsync(10),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task AllUserVerseChoreographies_ShouldReturnAllUserVerseChoreographiesWhenThereAreNoSearchCriteria()
    {
        var expectedCount = this.data.VerseChoreographies.Count(c => c.UserId == FirstUser.Id);

        var result = await verseChoreographyService.AllUserVerseChoreographiesAsync(FirstUser.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
            Assert.That(result.Entities.Count(), Is.EqualTo(expectedCount));
        });
    }

    [Test]
    public async Task AllUserVerseChoreographies_ShouldReturnOnlySomeVerseChoreographiesWhenThereIsSearchCriteria()
    {
        var result = await verseChoreographyService.AllUserVerseChoreographiesAsync(FirstUser.Id, "First", FirstVerseType.Id, FirstPosition.Id, FirstPosition.Id, HighlightFigure.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.TotalCount, Is.EqualTo(1));
            Assert.That(result.Entities.Count(), Is.EqualTo(1));
        });
    }

    [Test]
    public async Task AllUserCompleteVerseChoreographiesStartingWithPosition_ShouldReturnAllCompleteUserVerseChoreographiesWhenNoPositionIsSelected()
    {
        var result = await verseChoreographyService.AllUserCompleteVerseChoreographiesStartingWithPositionAsync(FirstUser.Id);

        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task AllUserCompleteVerseChoreographiesStartingWithPosition_ShouldReturnSomeVerseChoreographiesWhenPositionIsSelected()
    {
        var result = await verseChoreographyService.AllUserCompleteVerseChoreographiesStartingWithPositionAsync(FirstUser.Id, SecondPosition.Id);

        Assert.That(result.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task VerseChoreographyExistsForThisUserById_ShouldReturnTrueForValidIdForThisUser()
    {
        var result = await verseChoreographyService.VerseChoreographyExistForThisUserByIdAsync(FirstVerseChoreography.Id, FirstUser.Id);

        Assert.IsTrue(result);
    }

    [Test]
    public async Task VerseChoreographyExistsForThisUserById_ShouldReturnFalseForInvalidId()
    {
        var result = await verseChoreographyService.VerseChoreographyExistForThisUserByIdAsync(10, FirstUser.Id);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task VerseChoreographyExistsForThisUserById_ShouldReturnFalseForValidIdForAnotherUserVerseChoreography()
    {
        var result = await verseChoreographyService.VerseChoreographyExistForThisUserByIdAsync(FirstVerseChoreography.Id, SecondUser.Id);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task IsVerseChoreographyUsedInFullChoreographies_ShouldReturnTrueWhenTheVerseChoreographyIsUsed()
    {
        var result = await verseChoreographyService.IsVerseChoreographyUsedInFullChoreographiesAsync(FirstVerseChoreography.Id);

        Assert.IsTrue(result);
    }

    [Test]
    public async Task IsVerseChoreographyUsedInFullChoreographies_ShouldReturnFalseWhenTheVerseChoreographyIsNotUsed()
    {
        var result = await verseChoreographyService.IsVerseChoreographyUsedInFullChoreographiesAsync(FourthVerseChoreography.Id);

        Assert.IsFalse(result);
    }

    [Test]
    public void IsVerseChoreographyUsedInFullChoreographies_ShouldThrowAnExceptionIfTheVerseChoreographyDoesntExist()
    {
        Assert.That(async () => await verseChoreographyService.IsVerseChoreographyUsedInFullChoreographiesAsync(10),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task IsVerseChoreographyComplete_ShouldReturnTrueWhenTheVerseChoreographyIsComplete()
    {
        var result = await verseChoreographyService.IsVerseChoreographyCompleteAsync(FirstVerseChoreography.Id);

        Assert.IsTrue(result);
    }

    [Test]
    public async Task IsVerseChoreographyComplete_ShouldReturnFalseWhenTheVerseChoreographyIsNotComplete()
    {
        var result = await verseChoreographyService.IsVerseChoreographyCompleteAsync(SecondVerseChoreography.Id);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task SaveVerseChoreography_ShouldAddTheVerseChoreographyForValidUser()
    {
        var verseChoreographiesCountBefore = this.data.VerseChoreographies.Count(f => f.UserId == FirstUser.Id);

        VerseChoreographySaveViewModel model = new VerseChoreographySaveViewModel()
        {
            Name = "Test verse choreography",
            VerseTypeId = FirstVerseType.Id,
            Score = 4,
            Figures = new List<VerseChoreographyFigureViewModel>()
            {
                new VerseChoreographyFigureViewModel()
                {
                    FigureOrder = 1,
                    FigureOptionId = FirstFigureFirstOption.Id
                }
            }
        };

        await verseChoreographyService.SaveVerseChoreographyAsync(model, FirstUser.Id);

        var verseChoreographiesCountAfter = this.data.Figures.Count(f => f.UserId == FirstUser.Id);
        Assert.That(verseChoreographiesCountAfter, Is.EqualTo(verseChoreographiesCountBefore + 1));
    }

    [Test]
    public void SaveVerseChoreography_ShouldThrowAnExceptionIfUserIdIsNotValid()
    {
        Assert.That(async () => await verseChoreographyService.SaveVerseChoreographyAsync(new VerseChoreographySaveViewModel(), "InvalidUserId"),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void SaveVerseChoreography_ShouldThrowAnExceptionIfVerseTypeIdIsNotValid()
    {
        VerseChoreographySaveViewModel model = new VerseChoreographySaveViewModel()
        {
            Name = "Test verse choreography",
            VerseTypeId = 10,
            Score = 4,
            Figures = new List<VerseChoreographyFigureViewModel>()
        };

        Assert.That(async () => await verseChoreographyService.SaveVerseChoreographyAsync(model, FirstUser.Id),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void SaveVerseChoreography_ShouldThrowAnExceptionIfAnyOfTheFigureOptionIdsAreNotForThisUser()
    {
        VerseChoreographySaveViewModel model = new VerseChoreographySaveViewModel()
        {
            Name = "Test verse choreography",
            VerseTypeId = FirstVerseType.Id,
            Score = 4,
            Figures = new List<VerseChoreographyFigureViewModel>()
            {
                new VerseChoreographyFigureViewModel()
                {
                    FigureOrder = 1,
                    FigureOptionId = FourthFigureFirstOption.Id
                }
            }
        };

        Assert.That(async () => await verseChoreographyService.SaveVerseChoreographyAsync(model, FirstUser.Id),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public async Task ChangeFigureInVerseChoreography_ShouldChangeTheFigureCorrectlyWithValidData()
    {
        VerseChoreographyFigureSelectedReplacementServiceModel newFigure = new VerseChoreographyFigureSelectedReplacementServiceModel()
        {
            FigureOrder = 1,
            FigureOptionId = SecondFigureThirdOption.Id
        };

        await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(FirstVerseChoreography.Id, newFigure);

        var verseChoreographyFigureAfter = await this.data.VerseChoreographiesFigures
            .Where(vcf => vcf.VerseChoreographyId == FirstVerseChoreography.Id)
            .FirstOrDefaultAsync(vcf => vcf.FigureOrder == newFigure.FigureOrder);
        Assert.That(verseChoreographyFigureAfter.FigureOptionId, Is.EqualTo(newFigure.FigureOptionId));
    }

    [Test]
    public void ChangeFigureInVerseChoreography_ShouldThrowAnExceptionIfChoreographyDoesntExist()
    {
        Assert.That(async () => await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(10, new VerseChoreographyFigureSelectedReplacementServiceModel()),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void ChangeFigureInVerseChoreography_ShouldThrowAnExceptionIfNewOptionDoesntExist()
    {
        VerseChoreographyFigureSelectedReplacementServiceModel newFigure = new VerseChoreographyFigureSelectedReplacementServiceModel()
        {
            FigureOrder = 1,
            FigureOptionId = 50
        };

        Assert.That(async () => await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(FirstVerseChoreography.Id, newFigure),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void ChangeFigureInVerseChoreography_ShouldThrowAnExceptionIfUserForTheNewOptionIsDifferentFromUserFOrVerseChoreography()
    {
        VerseChoreographyFigureSelectedReplacementServiceModel newFigure = new VerseChoreographyFigureSelectedReplacementServiceModel()
        {
            FigureOrder = 1,
            FigureOptionId = FourthFigureFirstOption.Id
        };

        Assert.That(async () => await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(FirstVerseChoreography.Id, newFigure),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void ChangeFigureInVerseChoreography_ShouldThrowAnExceptionIfInvalidFigureOrderIsGiven()
    {
        VerseChoreographyFigureSelectedReplacementServiceModel newFigure = new VerseChoreographyFigureSelectedReplacementServiceModel()
        {
            FigureOrder = 10,
            FigureOptionId = SecondFigureThirdOption.Id
        };

        Assert.That(async () => await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(FirstVerseChoreography.Id, newFigure),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void ChangeFigureInVerseChoreography_ShouldThrowAnExceptionIfSomeOfTheParametersForTheNewFigureDoNotMatchTheOldFigure()
    {
        VerseChoreographyFigureSelectedReplacementServiceModel newFigure = new VerseChoreographyFigureSelectedReplacementServiceModel()
        {
            FigureOrder = 1,
            FigureOptionId = SecondFigureFirstOption.Id
        };

        Assert.That(async () => await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(FirstVerseChoreography.Id, newFigure),
            Throws.Exception.TypeOf<InvalidModelException>());
    }

    [Test]
    public async Task DeleteVerseChoreography_ShouldDeleteTheVerseChoreographySuccessfullyForValidVerseChoreographyWithNoFigures()
    {
        var verseChoreographyCountBefore = data.VerseChoreographies.Count();
        var verseChoreographyFiguresCountBefore = data.VerseChoreographiesFigures.Count();

        await verseChoreographyService.DeleteVerseChoreographyAsync(FourthVerseChoreography.Id);

        var verseChoreographyCountAfter = data.VerseChoreographies.Count();
        var verseChoreographyFiguresCountAfter = data.VerseChoreographiesFigures.Count();
        Assert.Multiple(() =>
        {
            Assert.That(verseChoreographyCountAfter, Is.EqualTo(verseChoreographyCountBefore - 1));
            Assert.That(verseChoreographyFiguresCountAfter, Is.EqualTo(verseChoreographyFiguresCountBefore));
        });
    }

    [Test]
    public async Task DeleteVerseChoreography_ShouldDeleteTheVerseChoreographyAndItsFiguresSuccessfullyForValidVerseChoreographyWithFigures()
    {
        var verseChoreographyCountBefore = data.VerseChoreographies.Count();
        var verseChoreographyFiguresCountBefore = data.VerseChoreographiesFigures.Count();
        var thisVerseChoreographyFiguresCount = data.VerseChoreographiesFigures.Count(f => f.VerseChoreographyId == ThirdVerseChoreography.Id);

        await verseChoreographyService.DeleteVerseChoreographyAsync(ThirdVerseChoreography.Id);

        var verseChoreographyCountAfter = data.VerseChoreographies.Count();
        var verseChoreographyFiguresCountAfter = data.VerseChoreographiesFigures.Count();
        Assert.Multiple(() =>
        {
            Assert.That(verseChoreographyCountAfter, Is.EqualTo(verseChoreographyCountBefore - 1));
            Assert.That(verseChoreographyFiguresCountAfter, Is.EqualTo(verseChoreographyFiguresCountBefore - thisVerseChoreographyFiguresCount));
        });
    }

    [Test]
    public async Task GenerateChoreographies_ShouldGenerateSuggestionsWhenNoStartPositionIsGiven()
    {
        VerseChoreographyGenerateModel query = new VerseChoreographyGenerateModel()
        {
            FinalFigureId = HighlightFigure.Id,
            VerseTypeId = FirstVerseType.Id
        };

        var result = await verseChoreographyService.GenerateChoreographies(query, FirstUser.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.LessThanOrEqualTo(MaxNumberOfSuggestedChoreographies));
        });
    }

    [Test]
    public async Task GenerateChoreographies_ShouldGenerateSuggestionsWhenStartPositionIsGiven()
    {
        VerseChoreographyGenerateModel query = new VerseChoreographyGenerateModel()
        {
            FinalFigureId = HighlightFigure.Id,
            VerseTypeId = FirstVerseType.Id,
            StartPositionId = FirstPosition.Id
        };

        var result = await verseChoreographyService.GenerateChoreographies(query, FirstUser.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.LessThanOrEqualTo(MaxNumberOfSuggestedChoreographies));
        });
    }

    [Test]
    public void GenerateChoreographies_ShouldThrowAnExceptionWhenInvalidFigureIdIsGiven()
    {
        VerseChoreographyGenerateModel query = new VerseChoreographyGenerateModel()
        {
            FinalFigureId = 10,
            VerseTypeId = FirstVerseType.Id
        };

        Assert.That(async () => await verseChoreographyService.GenerateChoreographies(query, FirstUser.Id),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }

    [Test]
    public void GenerateChoreographies_ShouldThrowAnExceptionWhenInvalidVerseTypeIsGiven()
    {
        VerseChoreographyGenerateModel query = new VerseChoreographyGenerateModel()
        {
            FinalFigureId = HighlightFigure.Id,
            VerseTypeId = 10
        };

        Assert.That(async () => await verseChoreographyService.GenerateChoreographies(query, FirstUser.Id),
            Throws.Exception.TypeOf<EntityNotFoundException>());
    }
}
