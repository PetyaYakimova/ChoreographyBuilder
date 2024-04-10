using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class PositionServiceTests : UnitTestsBase
	{
		private IPositionService positionService;
		private ILogger<PositionService> logger;

		[SetUp]
		public void Setup()
		{
			var mockLogger = new Mock<ILogger<PositionService>>();
			this.logger = mockLogger.Object;

			this.positionService = new PositionService(this.logger, repository, mapper);
		}

		[Test]
		public async Task GetPositionById_ShouldReturnValidPositionWithCorrectDataWhenIdExists()
		{
			var result = await positionService.GetPositionByIdAsync(FirstPosition.Id);

			Assert.That(result.Name, Is.EqualTo(FirstPosition.Name));
			Assert.That(result.IsActive, Is.EqualTo(FirstPosition.IsActive));
		}

		[Test]
		public async Task GetPositionById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await positionService.GetPositionByIdAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetPositionForDelete_ShouldReturnValidPositionWithCorrectDataWhenIdExists()
		{
			var result = await positionService.GetPositionForDeleteAsync(FirstPosition.Id);

			Assert.That(result.Id, Is.EqualTo(FirstPosition.Id));
			Assert.That(result.Name, Is.EqualTo(FirstPosition.Name));
		}

		[Test]
		public async Task GetPositionForDelete_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await positionService.GetPositionForDeleteAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AllPositions_ShouldReturnAllPositionsWhenThereAreNoSearchCriteria()
		{
			var expectedCount = this.data.Positions.Count();

			var result = await positionService.AllPositionsAsync();

			Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
			Assert.That(result.Entities.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task AllPositions_ShouldReturnOnlySomePositionsWhenThereIsSearchCriteria()
		{
			var result = await positionService.AllPositionsAsync("First");

			Assert.That(result.TotalCount, Is.EqualTo(1));
			Assert.That(result.Entities.Count(), Is.EqualTo(1));
		}

		[Test]
		public async Task AllActivePositionsAndSelectedPosition_ShouldReturnAllActivePositionsWhenNoSelectedPositionIsGiven()
		{
			var expectedCount = this.data.Positions.Count(p => p.IsActive);

			var result = await positionService.AllActivePositionsAndSelectedPositionAsync();

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task AllActivePositionsAndSelectedPosition_ShouldReturnAllActivePositionsAndTheSelectedInActivePosition()
		{
			var expectedCount = this.data.Positions.Count(p => p.IsActive || p.Id == InactivePosition.Id);

			var result = await positionService.AllActivePositionsAndSelectedPositionAsync(InactivePosition.Id);

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task AllActivePositionsAndSelectedPosition_ShouldReturnAllActivePositionsWhenSelectedIdIsForAnActivePosition()
		{
			var expectedCount = this.data.Positions.Count(p => p.IsActive);

			var result = await positionService.AllActivePositionsAndSelectedPositionAsync(FirstPosition.Id);

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task PositionExistsById_ShouldReturnTrueForValidId()
		{
			var result = await positionService.PositionExistByIdAsync(FirstPosition.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task PositionExistsById_ShouldReturnFalseForInvalidId()
		{
			var result = await positionService.PositionExistByIdAsync(10);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsPositionUsedInFigures_ShouldReturnTrueWhenThePositionIsUsed()
		{
			var result = await positionService.IsPositionUsedInFiguresAsync(FirstPosition.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsPositionUsedInFigures_ShouldReturnFalseWhenThePositionIsNotUsed()
		{
			var result = await positionService.IsPositionUsedInFiguresAsync(InactivePosition.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsPositionUsedInFigures_ShouldThrowAnExceptionIfThePositionDoesntExist()
		{
			Assert.That(async () => await positionService.IsPositionUsedInFiguresAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AddPosition_ShouldAddThePosition()
		{
			var positionsCountBefore = this.data.Positions.Count();

			PositionFormViewModel model = new PositionFormViewModel()
			{
				Name = "Test position",
				IsActive = true
			};

			await positionService.AddPositionAsync(model);

			var positionsCountAfter = this.data.Positions.Count();
			Assert.That(positionsCountAfter, Is.EqualTo(positionsCountBefore + 1));
		}

		[Test]
		public async Task ChangePositionStatus_ShouldChangeTheStatusToActiveWhenThePositionWasInactive()
		{
			await positionService.ChangePositionStatusAsync(InactivePosition.Id);

			Assert.That(InactivePosition.IsActive, Is.EqualTo(true));
		}

		[Test]
		public async Task ChangePositionStatus_ShouldChangeTheStatusToInactiveWhenThePositionWasActive()
		{
			await positionService.ChangePositionStatusAsync(SecondPosition.Id);

			Assert.That(SecondPosition.IsActive, Is.EqualTo(false));
		}

		[Test]
		public async Task ChangePositionStatus_ShouldThrowAnExceptionIfThePositionDoesntExist()
		{
			Assert.That(async () => await positionService.ChangePositionStatusAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditPosition_ShouldEditThePositionSuccessfullyForValidPosition()
		{
			var model = new PositionFormViewModel()
			{
				Name = "Edited name"
			};

			await positionService.EditPositionAsync(SecondPosition.Id, model);

			Assert.That(SecondPosition.Name, Is.EqualTo(model.Name));
		}

		[Test]
		public async Task EditPosition_ShouldThrowAnExceptionIfThePositionDoesntExist()
		{
			Assert.That(async () => await positionService.EditPositionAsync(10, new PositionFormViewModel()),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task DeletePosition_ShouldDeleteValidPosition()
		{
			var positionsCountBefore = data.Positions.Count();

			await positionService.DeletePositionAsync(ThirdPosition.Id);

			var positionsCountAfter = data.Positions.Count();
			Assert.That(positionsCountAfter, Is.EqualTo(positionsCountBefore - 1));
		}
	}
}
