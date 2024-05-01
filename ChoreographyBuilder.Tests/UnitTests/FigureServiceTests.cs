using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class FigureServiceTests : UnitTestsBase
	{
		private IFigureService figureService;
		private ILogger<FigureService> logger;

		[SetUp]
		public void Setup()
		{
			var mockLogger = new Mock<ILogger<FigureService>>();
			this.logger = mockLogger.Object;

			this.figureService = new FigureService(this.logger, repository, mapper);
		}

		[Test]
		public async Task GetFigureById_ShouldReturnValidFigureWithCorrectDataWhenIdExists()
		{
			var result = await figureService.GetFigureByIdAsync(FirstFigure.Id);

			Assert.Multiple(() =>
			{
				Assert.That(result.Name, Is.EqualTo(FirstFigure.Name));
				Assert.That(result.IsFavourite, Is.EqualTo(FirstFigure.IsFavourite));
				Assert.That(result.IsHighlight, Is.EqualTo(FirstFigure.IsHighlight));
			});
		}

		[Test]
		public void GetFigureById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await figureService.GetFigureByIdAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetFigureNameById_ShouldReturnValidFigureNameWhenIdExists()
		{
			var result = await figureService.GetFigureNameByIdAsync(FirstFigure.Id);

			Assert.That(result, Is.EqualTo(FirstFigure.Name));
		}

		[Test]
		public void GetFigureNameById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await figureService.GetFigureNameByIdAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetFigureForDelete_ShouldReturnValidFigureWithCorrectDataWhenIdExists()
		{
			var result = await figureService.GetFigureForDeleteAsync(FirstFigure.Id);

			Assert.Multiple(() =>
			{
				Assert.That(result.Id, Is.EqualTo(FirstFigure.Id));
				Assert.That(result.Name, Is.EqualTo(FirstFigure.Name));
			});
		}

		[Test]
		public void GetFigureForDelete_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await figureService.GetFigureForDeleteAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AllUserFigures_ShouldReturnAllUserFiguresWhenThereAreNoSearchCriteria()
		{
			var expectedCount = this.data.Figures.Count(f => f.UserId == FirstUser.Id);

			var result = await figureService.AllUserFiguresAsync(FirstUser.Id);

			Assert.Multiple(() =>
			{
				Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
				Assert.That(result.Entities.Count(), Is.EqualTo(expectedCount));
			});
		}

		[Test]
		public async Task AllUserFigures_ShouldReturnOnlySomeFiguresWhenThereIsSearchCriteria()
		{
			var result = await figureService.AllUserFiguresAsync(FirstUser.Id, "Second");

			Assert.Multiple(() =>
			{
				Assert.That(result.TotalCount, Is.EqualTo(1));
				Assert.That(result.Entities.Count(), Is.EqualTo(1));
			});
		}

		[Test]
		public async Task AllUserHighlightFiguresForChoreographies_ShouldReturnAllUserHighlightFigures()
		{
			var expectedCount = this.data.Figures.Count(f => f.UserId == FirstUser.Id && f.IsHighlight);

			var result = await figureService.AllUserHighlightFiguresForChoreographiesAsync(FirstUser.Id);

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}


		[Test]
		public async Task FigureExistsForThisUserById_ShouldReturnTrueForValidIdForThisUser()
		{
			var result = await figureService.FigureExistForThisUserByIdAsync(FirstFigure.Id, FirstUser.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task FigureExistsForThisUserById_ShouldReturnFalseForInvalidId()
		{
			var result = await figureService.FigureExistForThisUserByIdAsync(10, FirstUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task FigureExistsForThisUserById_ShouldReturnFalseForValidIdForAnotherUserFigure()
		{
			var result = await figureService.FigureExistForThisUserByIdAsync(FirstFigure.Id, SecondUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsFigureUsedInChoreographies_ShouldReturnTrueWhenTheFigureIsUsed()
		{
			var result = await figureService.IsFigureUsedInChoreographiesAsync(FirstFigure.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsFigureUsedInChoreographies_ShouldReturnFalseWhenTheFigureIsNotUsed()
		{
			var result = await figureService.IsFigureUsedInChoreographiesAsync(FourthFigure.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public void IsFigureUsedInChoreographies_ShouldThrowAnExceptionIfTheFigureDoesntExist()
		{
			Assert.That(async () => await figureService.IsFigureUsedInChoreographiesAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AddFigure_ShouldAddTheFigureForValidUser()
		{
			var figureCountBefore = this.data.Figures.Count(f => f.UserId == FirstUser.Id);

			FigureFormViewModel model = new FigureFormViewModel()
			{
				Name = "Test position",
				IsFavourite = true,
				IsHighlight = true
			};

			await figureService.AddFigureAsync(model, FirstUser.Id);

			var figureCountAfter = this.data.Figures.Count(f => f.UserId == FirstUser.Id);
			Assert.That(figureCountAfter, Is.EqualTo(figureCountBefore + 1));
		}

		[Test]
		public void AddFigure_ShouldThrowAnExceptionIfUserIdIsNotValid()
		{
			Assert.That(async () => await figureService.AddFigureAsync(new FigureFormViewModel(), "InvalidUserId"),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditFigure_ShouldEditTheFigureSuccessfullyForValidFigure()
		{
			var model = new FigureFormViewModel()
			{
				Name = "Edited name",
				IsFavourite = false,
				IsHighlight = true
			};

			await figureService.EditFigureAsync(SecondFigure.Id, model);

			Assert.Multiple(() =>
			{
				Assert.That(SecondFigure.Name, Is.EqualTo(model.Name));
				Assert.That(SecondFigure.IsFavourite, Is.EqualTo(model.IsFavourite));
				Assert.That(SecondFigure.IsHighlight, Is.EqualTo(model.IsHighlight));
			});
		}

		[Test]
		public void EditFigure_ShouldThrowAnExceptionIfThFigureDoesntExist()
		{
			Assert.That(async () => await figureService.EditFigureAsync(10, new FigureFormViewModel()),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task DeleteFigure_ShouldDeleteTheFigureSuccessfullyForValidFigureWithNoOptions()
		{
			var figureCountBefore = data.Figures.Count();
			var figureOptionsCountBefore = data.FigureOptions.Count();

			await figureService.DeleteFigureAsync(ThirdFigure.Id);

			var figureCountAfter = data.Figures.Count();
			var figureOptionsCountAfter = data.FigureOptions.Count();
			Assert.Multiple(() =>
			{
				Assert.That(figureCountAfter, Is.EqualTo(figureCountBefore - 1));
				Assert.That(figureOptionsCountAfter, Is.EqualTo(figureOptionsCountBefore));
			});
		}

		[Test]
		public async Task DeleteFigure_ShouldDeleteTheFigureAndItsOptionsSuccessfullyForValidFigureWithOptions()
		{
			var figureCountBefore = data.Figures.Count();
			var figureOptionsCountBefore = data.FigureOptions.Count();
			var thisFigureOptionsCount = data.FigureOptions.Count(f => f.FigureId == FirstFigure.Id);

			await figureService.DeleteFigureAsync(FirstFigure.Id);

			var figureCountAfter = data.Figures.Count();
			var figureOptionsCountAfter = data.FigureOptions.Count();
			Assert.Multiple(() =>
			{
				Assert.That(figureCountAfter, Is.EqualTo(figureCountBefore - 1));
				Assert.That(figureOptionsCountAfter, Is.EqualTo(figureOptionsCountBefore - thisFigureOptionsCount));
			});
		}
	}
}
