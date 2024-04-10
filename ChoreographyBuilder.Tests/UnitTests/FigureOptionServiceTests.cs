using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Services;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class FigureOptionServiceTests : UnitTestsBase
	{
		private IFigureOptionService figureOptionService;
		private ILogger<FigureOptionService> logger;

		[SetUp]
		public void Setup()
		{
			var mockLogger = new Mock<ILogger<FigureOptionService>>();
			this.logger = mockLogger.Object;

			this.figureOptionService = new FigureOptionService(this.logger, repository, mapper);
		}


		[Test]
		public async Task GetFigureOptionById_ShouldReturnValidFigureOptionWithCorrectDataWhenIdExists()
		{
			var result = await figureOptionService.GetFigureOptionByIdAsync(FirstFigureFirstOption.Id);

			Assert.That(result.BeatCounts, Is.EqualTo(FirstFigureFirstOption.BeatCounts));
			Assert.That(result.DynamicsType, Is.EqualTo(FirstFigureFirstOption.DynamicsType));
			Assert.That(result.StartPositionId, Is.EqualTo(FirstFigureFirstOption.StartPositionId));
			Assert.That(result.EndPositionId, Is.EqualTo(FirstFigureFirstOption.EndPositionId));
			Assert.That(result.FigureId, Is.EqualTo(FirstFigureFirstOption.FigureId));
			Assert.That(result.FigureName, Is.EqualTo(FirstFigure.Name));
		}

		[Test]
		public async Task GetFigureOptionById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await figureOptionService.GetFigureOptionByIdAsync(50),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetFigureOptionForDelete_ShouldReturnValidFigureOptionWithCorrectDataWhenIdExists()
		{
			var result = await figureOptionService.GetFigureOptionForDeleteAsync(FirstFigureFirstOption.Id);

			Assert.That(result.Id, Is.EqualTo(FirstFigureFirstOption.Id));
			Assert.That(result.FigureName, Is.EqualTo(FirstFigure.Name));
		}

		[Test]
		public async Task GetFigureOptionForDelete_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await figureOptionService.GetFigureOptionForDeleteAsync(50),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetFigureOptions_ShouldReturnAllFigureOptionsWhenThereAreNoSearchCriteria()
		{
			var expectedCount = this.data.FigureOptions.Count(f => f.FigureId == FirstFigure.Id);

			var result = await figureOptionService.GetFigureOptionsAsync(FirstFigure.Id);

			Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
			Assert.That(result.Entities.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetFigureOptions_ShouldReturnOnlySomeFigureOptionsWhenThereIsSearchCriteria()
		{
			var result = await figureOptionService.GetFigureOptionsAsync(FirstFigure.Id, FirstPosition.Id, SecondPosition.Id, 6, DynamicsType.Regular);

			Assert.That(result.TotalCount, Is.EqualTo(1));
			Assert.That(result.Entities.Count(), Is.EqualTo(1));
		}

		[Test]
		public async Task GetFigureOptions_ShouldThrowExceptionWhenFigureIdDoesntExists()
		{
			Assert.That(async () => await figureOptionService.GetFigureOptionsAsync(50),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}


		[Test]
		public async Task FigureOptionExistsForThisUserById_ShouldReturnTrueForValidIdForThisUser()
		{
			var result = await figureOptionService.FigureOptionExistForThisUserByIdAsync(FirstFigureFirstOption.Id, FirstUser.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task FigureOptionExistsForThisUserById_ShouldReturnFalseForInvalidId()
		{
			var result = await figureOptionService.FigureOptionExistForThisUserByIdAsync(50, FirstUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task FigureOptionExistsForThisUserById_ShouldReturnFalseForValidIdForAnotherUserFigure()
		{
			var result = await figureOptionService.FigureOptionExistForThisUserByIdAsync(FirstFigureFirstOption.Id, SecondUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsFigureOptionUsedInChoreographies_ShouldReturnTrueWhenTheFigureOptionIsUsed()
		{
			var result = await figureOptionService.IsFigureOptionUsedInChoreographiesAsync(FirstFigureFirstOption.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsFigureOptionUsedInChoreographies_ShouldReturnFalseWhenTheFigureOptionIsNotUsed()
		{
			var result = await figureOptionService.IsFigureOptionUsedInChoreographiesAsync(SecondFigureSecondOption.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsFigureOptionUsedInChoreographies_ShouldThrowAnExceptionIfTheFigureOptionDoesntExist()
		{
			Assert.That(async () => await figureOptionService.IsFigureOptionUsedInChoreographiesAsync(50),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AddFigureOption_ShouldAddTheFigureOptionForValidFigure()
		{
			var figureOptionsForTheFigureCountBefore = this.data.FigureOptions.Count(fo => fo.FigureId == FirstFigure.Id);

			FigureOptionFormViewModel model = new FigureOptionFormViewModel()
			{
				BeatCounts = 8,
				DynamicsType = DynamicsType.Regular,
				EndPositionId = FirstPosition.Id,
				StartPositionId = FirstPosition.Id,
				FigureId = FirstFigure.Id,
				FigureName = FirstFigure.Name
			};

			await figureOptionService.AddFigureOptionAsync(model);

			var figureOptionsForTheFigureCountAfter = this.data.FigureOptions.Count(fo => fo.FigureId == FirstFigure.Id);
			Assert.That(figureOptionsForTheFigureCountAfter, Is.EqualTo(figureOptionsForTheFigureCountBefore + 1));
		}

		[Test]
		public async Task AddFigureOption_ShouldThrowAnExceptionIfFigureIdIsNotValid()
		{
			FigureOptionFormViewModel model = new FigureOptionFormViewModel()
			{
				BeatCounts = 8,
				DynamicsType = DynamicsType.Regular,
				EndPositionId = FirstPosition.Id,
				StartPositionId = FirstPosition.Id,
				FigureId = 10,
				FigureName = FirstFigure.Name
			};

			Assert.That(async () => await figureOptionService.AddFigureOptionAsync(model),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AddFigureOption_ShouldThrowAnExceptionIfStartPositionIdIsNotValid()
		{
			FigureOptionFormViewModel model = new FigureOptionFormViewModel()
			{
				BeatCounts = 8,
				DynamicsType = DynamicsType.Regular,
				EndPositionId = FirstPosition.Id,
				StartPositionId = 10,
				FigureId = FirstFigure.Id,
				FigureName = FirstFigure.Name
			};

			Assert.That(async () => await figureOptionService.AddFigureOptionAsync(model),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AddFigureOption_ShouldThrowAnExceptionIfEndPositionIdIsNotValid()
		{
			FigureOptionFormViewModel model = new FigureOptionFormViewModel()
			{
				BeatCounts = 8,
				DynamicsType = DynamicsType.Regular,
				EndPositionId = 10,
				StartPositionId = FirstPosition.Id,
				FigureId = FirstFigure.Id,
				FigureName = FirstFigure.Name
			};

			Assert.That(async () => await figureOptionService.AddFigureOptionAsync(model),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditFigureOption_ShouldEditTheFigureOptionSuccessfullyForValidFigureOption()
		{
			var model = new FigureOptionFormViewModel()
			{
				BeatCounts = 12,
				StartPositionId = SecondPosition.Id,
				EndPositionId = SecondPosition.Id,
				DynamicsType = DynamicsType.Slow,
				FigureId = FirstFigure.Id,
				FigureName = FirstFigure.Name
			};

			await figureOptionService.EditFigureOptionAsync(FirstFigureFirstOption.Id, model);

			Assert.That(FirstFigureFirstOption.BeatCounts, Is.EqualTo(model.BeatCounts));
			Assert.That(FirstFigureFirstOption.StartPositionId, Is.EqualTo(model.StartPositionId));
			Assert.That(FirstFigureFirstOption.EndPositionId, Is.EqualTo(model.EndPositionId));
			Assert.That(FirstFigureFirstOption.DynamicsType, Is.EqualTo(model.DynamicsType));
		}

		[Test]
		public async Task EditFigureOption_ShouldThrowAnExceptionIfThFigureOptionDoesntExist()
		{
			Assert.That(async () => await figureOptionService.EditFigureOptionAsync(50, new FigureOptionFormViewModel()),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditFigureOption_ShouldThrowAnExceptionIfStartPositionIdIsNotValid()
		{
			FigureOptionFormViewModel model = new FigureOptionFormViewModel()
			{
				BeatCounts = 8,
				DynamicsType = DynamicsType.Regular,
				EndPositionId = FirstPosition.Id,
				StartPositionId = 10,
				FigureId = FirstFigure.Id,
				FigureName = FirstFigure.Name
			};

			Assert.That(async () => await figureOptionService.EditFigureOptionAsync(FirstFigureFirstOption.Id, model),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditFigureOption_ShouldThrowAnExceptionIfEndPositionIdIsNotValid()
		{
			FigureOptionFormViewModel model = new FigureOptionFormViewModel()
			{
				BeatCounts = 8,
				DynamicsType = DynamicsType.Regular,
				EndPositionId = 10,
				StartPositionId = FirstPosition.Id,
				FigureId = FirstFigure.Id,
				FigureName = FirstFigure.Name
			};

			Assert.That(async () => await figureOptionService.EditFigureOptionAsync(FirstFigureFirstOption.Id, model),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task DeleteFigureOption_ShouldDeleteTheFigureOptionSuccessfully()
		{
			var figureOptionsCountBefore = data.FigureOptions.Count(fo => fo.FigureId == FirstFigure.Id);

			await figureOptionService.DeleteFigureOptionAsync(FirstFigureSecondOption.Id);

			var figureOptionsCountAfter = data.FigureOptions.Count(fo => fo.FigureId == FirstFigure.Id);
			Assert.That(figureOptionsCountAfter, Is.EqualTo(figureOptionsCountBefore - 1));
		}
	}
}
