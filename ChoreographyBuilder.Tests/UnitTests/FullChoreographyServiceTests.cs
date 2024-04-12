using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FullChoreography;
using ChoreographyBuilder.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class FullChoreographyServiceTests : UnitTestsBase
	{
		private IFullChoreographyService fullChoreographyService;
		private ILogger<FullChoreographyService> logger;

		[SetUp]
		public void Setup()
		{
			var mockLogger = new Mock<ILogger<FullChoreographyService>>();
			this.logger = mockLogger.Object;

			this.fullChoreographyService = new FullChoreographyService(this.logger, repository, mapper);
		}


		[Test]
		public async Task GetChoreographyDetailsById_ShouldReturnValidFullChoreographyWithCorrectDataWhenIdExists()
		{
			var result = await fullChoreographyService.GetChoreographyDetailsByIdAsync(FirstFullChoreography.Id);

			Assert.That(result.Id, Is.EqualTo(FirstFullChoreography.Id));
			Assert.That(result.Name, Is.EqualTo(FirstFullChoreography.Name));
			Assert.That(result.NumberOfVerses, Is.EqualTo(FirstFullChoreography.VerseChoreographies.Count()));
			Assert.That(result.Verses.Count(), Is.EqualTo(FirstFullChoreography.VerseChoreographies.Count()));
		}

		[Test]
		public async Task GetChoreographyDetailsById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await fullChoreographyService.GetChoreographyDetailsByIdAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetChoreographyForEditById_ShouldReturnValidChoreographyDataWhenIdExists()
		{
			var result = await fullChoreographyService.GetChoreographyForEditByIdAsync(FirstFullChoreography.Id);

			Assert.That(result.Name, Is.EqualTo(FirstFullChoreography.Name));
		}

		[Test]
		public async Task GetChoreographyForEditById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await fullChoreographyService.GetChoreographyForEditByIdAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetFullChoreographyForDelete_ShouldReturnValidChoreographyDataWhenIdExists()
		{
			var result = await fullChoreographyService.GetFullChoreographyForDeleteAsync(FirstFullChoreography.Id);

			Assert.That(result.Id, Is.EqualTo(FirstFullChoreography.Id));
			Assert.That(result.Name, Is.EqualTo(FirstFullChoreography.Name));
			Assert.That(result.NumberOfVerses, Is.EqualTo(FirstFullChoreography.VerseChoreographies.Count()));
		}

		[Test]
		public async Task GetFullChoreographyForDelete_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await fullChoreographyService.GetFullChoreographyForDeleteAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetLastVerseChoreographyEndPosition_ShouldReturnValidEndPositionWhenFullChoreographyHasVerses()
		{
			var result = await fullChoreographyService.GetLastVerseChoreographyEndPositionAsync(FirstFullChoreography.Id);

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(SecondPosition.Id));
			Assert.That(result.Name, Is.EqualTo(SecondPosition.Name));
		}

		[Test]
		public async Task GetLastVerseChoreographyEndPosition_ShouldReturnNullWhenFullChoreographyDoesntHaveVerses()
		{
			var result = await fullChoreographyService.GetLastVerseChoreographyEndPositionAsync(SecondFullChoreography.Id);

			Assert.That(result, Is.Null);
		}

		[Test]
		public async Task GetLastVerseChoreographyEndPosition_ShouldReturnNullWhenFullChoreographyDoesntExist()
		{
			var result = await fullChoreographyService.GetLastVerseChoreographyEndPositionAsync(10);

			Assert.That(result, Is.Null);
		}

		[Test]
		public async Task GetNumberOfVerseChoreographiesForFullChoreography_ShouldReturnValidNumberWhenFullChoreographyHasVerses()
		{
			var result = await fullChoreographyService.GetNumberOfVerseChoreographiesForFullChoreographyAsync(FirstFullChoreography.Id);

			Assert.That(result, Is.EqualTo(FirstFullChoreography.VerseChoreographies.Count()));
		}

		[Test]
		public async Task GetNumberOfVerseChoreographiesForFullChoreography_ShouldReturnZeroWhenFullChoreographyDoesntHaveVerses()
		{
			var result = await fullChoreographyService.GetNumberOfVerseChoreographiesForFullChoreographyAsync(SecondFullChoreography.Id);

			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public async Task GetNumberOfVerseChoreographiesForFullChoreography_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await fullChoreographyService.GetNumberOfVerseChoreographiesForFullChoreographyAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AllUserFullChoreographies_ShouldReturnAllUserFullChoreographiesWhenThereAreNoSearchCriteria()
		{
			var expectedCount = this.data.FullChoreographies.Count(f => f.UserId == FirstUser.Id);

			var result = await fullChoreographyService.AllUserFullChoreographiesAsync(FirstUser.Id);

			Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
			Assert.That(result.Entities.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task AllUserFullChoreographies_ShouldReturnOnlySomeFullChoreographiesWhenThereIsSearchCriteria()
		{
			var result = await fullChoreographyService.AllUserFullChoreographiesAsync(FirstUser.Id, "First");

			Assert.That(result.TotalCount, Is.EqualTo(1));
			Assert.That(result.Entities.Count(), Is.EqualTo(1));
		}


		[Test]
		public async Task FullChoreographyExistsForThisUserById_ShouldReturnTrueForValidIdForThisUser()
		{
			var result = await fullChoreographyService.FullChoreographyExistForThisUserByIdAsync(FirstFullChoreography.Id, FirstUser.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task FullChoreographyExistsForThisUserById_ShouldReturnFalseForInvalidId()
		{
			var result = await fullChoreographyService.FullChoreographyExistForThisUserByIdAsync(10, FirstUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task FullChoreographyExistsForThisUserById_ShouldReturnFalseForValidIdForAnotherUserFigure()
		{
			var result = await fullChoreographyService.FullChoreographyExistForThisUserByIdAsync(FirstFullChoreography.Id, SecondUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task AddFullChoreography_ShouldAddTheFullChoreographyForValidUser()
		{
			var fullChoreographyCountBefore = this.data.FullChoreographies.Count(f => f.UserId == FirstUser.Id);

			FullChoreographyFormViewModel model = new FullChoreographyFormViewModel()
			{
				Name = "Test position"
			};

			await fullChoreographyService.AddFullChoreographyAsync(model, FirstUser.Id);

			var fullChoreographyCountAfter = this.data.FullChoreographies.Count(f => f.UserId == FirstUser.Id);
			Assert.That(fullChoreographyCountAfter, Is.EqualTo(fullChoreographyCountBefore + 1));
		}

		[Test]
		public async Task AddFullChoreography_ShouldThrowAnExceptionIfUserIdIsNotValid()
		{
			Assert.That(async () => await fullChoreographyService.AddFullChoreographyAsync(new FullChoreographyFormViewModel(), "InvalidUserId"),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditFullChoreography_ShouldEditTheFullChoreographySuccessfullyForValidFullChoreography()
		{
			var model = new FullChoreographyFormViewModel()
			{
				Name = "Edited name"
			};

			await fullChoreographyService.EditFullChoreographyAsync(SecondFullChoreography.Id, model);

			Assert.That(SecondFullChoreography.Name, Is.EqualTo(model.Name));
		}

		[Test]
		public async Task EditFullChoreography_ShouldThrowAnExceptionIfThFullChoreographyDoesntExist()
		{
			Assert.That(async () => await fullChoreographyService.EditFullChoreographyAsync(10, new FullChoreographyFormViewModel()),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task DeleteFullChoreograph_ShouldDeleteTheFullChoreographSuccessfullyForValidFullChoreographWithNoVerses()
		{
			var fullChoreographyCountBefore = data.FullChoreographies.Count();
			var fullChoreographyVersesCountBefore = data.FullChoreographiesVerseChoreographies.Count();

			await fullChoreographyService.DeleteFullChoreographyAsync(SecondFullChoreography.Id);

			var fullChoreographyCountAfter = data.FullChoreographies.Count();
			var fullChoreographyVersesCountAfter = data.FullChoreographiesVerseChoreographies.Count();
			Assert.That(fullChoreographyCountAfter, Is.EqualTo(fullChoreographyCountBefore - 1));
			Assert.That(fullChoreographyVersesCountAfter, Is.EqualTo(fullChoreographyVersesCountBefore));
		}

		[Test]
		public async Task DeleteFullChoreograph_ShouldDeleteTheFullChoreographAndItsVersesSuccessfullyForValidFullChoreographWithVerses()
		{
			var fullChoreographyCountBefore = data.FullChoreographies.Count();
			var fullChoreographyVersesCountBefore = data.FullChoreographiesVerseChoreographies.Count();
			var thisFullChoreographyVersesCount = data.FullChoreographiesVerseChoreographies.Count(f => f.FullChoreographyId == FirstFullChoreography.Id);

			await fullChoreographyService.DeleteFullChoreographyAsync(FirstFullChoreography.Id);

			var fullChoreographyCountAfter = data.FullChoreographies.Count();
			var fullChoreographyVersesCountAfter = data.FullChoreographiesVerseChoreographies.Count();
			Assert.That(fullChoreographyCountAfter, Is.EqualTo(fullChoreographyCountBefore - 1));
			Assert.That(fullChoreographyVersesCountAfter, Is.EqualTo(fullChoreographyVersesCountBefore - thisFullChoreographyVersesCount));
		}
	}
}
