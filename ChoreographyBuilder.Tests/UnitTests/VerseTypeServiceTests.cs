using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class VerseTypeServiceTests : UnitTestsBase
	{
		private IVerseTypeService verseTypeService;
		private ILogger<VerseTypeService> logger;

		[SetUp]
		public void Setup()
		{
			var mockLogger = new Mock<ILogger<VerseTypeService>>();
			this.logger = mockLogger.Object;

			this.verseTypeService = new VerseTypeService(this.logger, repository, mapper);
		}

		[Test]
		public async Task GetVerseTypeById_ShouldReturnValidVerseTypeWithCorrectDataWhenIdExists()
		{
			var result = await verseTypeService.GetVerseTypeById(FirstVerseType.Id);

			Assert.Multiple(() =>
			{
				Assert.That(result.Name, Is.EqualTo(FirstVerseType.Name));
				Assert.That(result.BeatCounts, Is.EqualTo(FirstVerseType.BeatCounts));
				Assert.That(result.IsActive, Is.EqualTo(FirstVerseType.IsActive));
			});
		}

		[Test]
		public void GetVerseTypeById_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await verseTypeService.GetVerseTypeById(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task GetVerseTypeForDelete_ShouldReturnValidVerseTypeWithCorrectDataWhenIdExists()
		{
			var result = await verseTypeService.GetVerseTypeForDeleteAsync(FirstVerseType.Id);

			Assert.Multiple(() =>
			{
				Assert.That(result.Id, Is.EqualTo(FirstVerseType.Id));
				Assert.IsTrue(result.Name.Contains(FirstVerseType.Name));
				Assert.IsTrue(result.Name.Contains(FirstVerseType.BeatCounts.ToString()));
			});
		}

		[Test]
		public void GetVerseTypeForDelete_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await verseTypeService.GetVerseTypeForDeleteAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AllVerseTypes_ShouldReturnAllVerseTypesWhenThereAreNoSearchCriteria()
		{
			var expectedCount = this.data.VerseTypes.Count();

			var result = await verseTypeService.AllVerseTypesAsync();

			Assert.Multiple(() =>
			{
				Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
				Assert.That(result.Entities.Count(), Is.EqualTo(expectedCount));
			});
		}

		[Test]
		public async Task AllVerseTypes_ShouldReturnOnlySomeVerseTypesWhenThereIsSearchCriteria()
		{
			var result = await verseTypeService.AllVerseTypesAsync("First", 32);

			Assert.Multiple(() =>
			{
				Assert.That(result.TotalCount, Is.EqualTo(1));
				Assert.That(result.Entities.Count(), Is.EqualTo(1));
			});
		}

		[Test]
		public async Task AllActiveVerseTypesAndSelectedVerseType_ShouldReturnAllActiveVerseTypesWhenNoSelectedVerseTypeIsGiven()
		{
			var expectedCount = this.data.VerseTypes.Count(p => p.IsActive);

			var result = await verseTypeService.AllActiveVerseTypesOrSelectedVerseTypeAsync();

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task AllActiveVerseTypesAndSelectedVerseTypes_ShouldReturnAllActiveVerseTypesAndTheSelectedInActiveVerseType()
		{
			var expectedCount = this.data.VerseTypes.Count(p => p.IsActive || p.Id == InactiveVerseType.Id);

			var result = await verseTypeService.AllActiveVerseTypesOrSelectedVerseTypeAsync(InactiveVerseType.Id);

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task AllActiveVerseTypesAndSelectedVerseTypes_ShouldReturnAllActiveVerseTypesWhenSelectedIdIsForAnActiveVerseType()
		{
			var expectedCount = this.data.VerseTypes.Count(p => p.IsActive);

			var result = await verseTypeService.AllActiveVerseTypesOrSelectedVerseTypeAsync(FirstVerseType.Id);

			Assert.That(result.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task VerseTypeExistsById_ShouldReturnTrueForValidId()
		{
			var result = await verseTypeService.VerseTypeExistByIdAsync(FirstVerseType.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task VerseTypeExistsById_ShouldReturnFalseForInvalidId()
		{
			var result = await verseTypeService.VerseTypeExistByIdAsync(10);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsVerseTypeUsedInChoreographies_ShouldReturnTrueWhenTheVerseTypeIsUsed()
		{
			var result = await verseTypeService.IsVerseTypeUsedInChoreographiesAsync(FirstVerseType.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsVerseTypeUsedInChoreographies_ShouldReturnFalseWhenTheVerseTypeIsNotUsed()
		{
			var result = await verseTypeService.IsVerseTypeUsedInChoreographiesAsync(InactiveVerseType.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public void IsVerseTypeUsedInChoreographies_ShouldThrowAnExceptionIfTheVerseTypeDoesntExist()
		{
			Assert.That(async () => await verseTypeService.IsVerseTypeUsedInChoreographiesAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task AddVerseType_ShouldAddTheVerseType()
		{
			var verseTypesCountBefore = this.data.VerseTypes.Count();

			VerseTypeFormViewModel model = new VerseTypeFormViewModel()
			{
				Name = "Test verse type",
				BeatCounts = 40,
				IsActive = true
			};

			await verseTypeService.AddVerseTypeAsync(model);

			var verseTypesCountAfter = this.data.VerseTypes.Count();
			Assert.That(verseTypesCountAfter, Is.EqualTo(verseTypesCountBefore + 1));
		}

		[Test]
		public void AddVerseType_ShouldThrowAnExceptionIfTheBeatsCountIsOddNumber()
		{
			VerseTypeFormViewModel model = new VerseTypeFormViewModel()
			{
				Name = "Test verse type",
				BeatCounts = 7,
				IsActive = true
			};

			Assert.That(async () => await verseTypeService.AddVerseTypeAsync(model),
				Throws.Exception.TypeOf<InvalidModelException>());
		}

		[Test]
		public async Task ChangeVerseTypeStatus_ShouldChangeTheStatusToActiveWhenTheVerseTypeWasInactive()
		{
			await verseTypeService.ChangeVerseTypeStatusAsync(InactiveVerseType.Id);

			Assert.That(InactiveVerseType.IsActive, Is.EqualTo(true));
		}

		[Test]
		public async Task ChangeVerseTypeStatus_ShouldChangeTheStatusToInactiveWhenTheVerseTypeWasActive()
		{
			await verseTypeService.ChangeVerseTypeStatusAsync(SecondVerseType.Id);

			Assert.That(SecondVerseType.IsActive, Is.EqualTo(false));
		}

		[Test]
		public void ChangeVerseTypeStatus_ShouldThrowAnExceptionIfTheVerseTypeDoesntExist()
		{
			Assert.That(async () => await verseTypeService.ChangeVerseTypeStatusAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task EditVerseType_ShouldEditTheVerseTypeSuccessfullyForValidVerseType()
		{
			var model = new VerseTypeFormViewModel()
			{
				Name = "Edited name",
				BeatCounts = 66
			};

			await verseTypeService.EditVerseTypeAsync(SecondVerseType.Id, model);

			Assert.Multiple(() =>
			{
				Assert.That(SecondVerseType.Name, Is.EqualTo(model.Name));
				Assert.That(SecondVerseType.BeatCounts, Is.EqualTo(model.BeatCounts));
			});
		}

		[Test]
		public void EditVerseType_ShouldThrowAnExceptionIfTheVerseTypeDoesntExist()
		{
			Assert.That(async () => await verseTypeService.EditVerseTypeAsync(10, new VerseTypeFormViewModel()),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public void EditVerseType_ShouldThrowAnExceptionIfTheBeatsCountIsOddNumber()
		{
			VerseTypeFormViewModel model = new VerseTypeFormViewModel()
			{
				Name = "Edited name",
				BeatCounts = 11
			};

			Assert.That(async () => await verseTypeService.EditVerseTypeAsync(SecondVerseType.Id, model),
				Throws.Exception.TypeOf<InvalidModelException>());
		}

		[Test]
		public async Task DeleteVerseType_ShouldDeleteValidVerseType()
		{
			var verseTypesCountBefore = data.VerseTypes.Count();

			await verseTypeService.DeleteVerseTypeAsync(InactiveVerseType.Id);

			var verseTypesCountAfter = data.VerseTypes.Count();
			Assert.That(verseTypesCountAfter, Is.EqualTo(verseTypesCountBefore - 1));
		}
	}
}
