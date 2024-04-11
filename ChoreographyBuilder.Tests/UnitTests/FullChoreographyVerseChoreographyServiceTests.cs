using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Services;
using ChoreographyBuilder.Infrastructure.Data.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class FullChoreographyVerseChoreographyServiceTests : UnitTestsBase
	{
		private IFullChoreographyVerseChoreographyService fullChoreographyVerseChoreographyService;
		private ILogger<FullChoreographyVerseChoreographyService> logger;

		[SetUp]
		public void Setup()
		{
			var mockLogger = new Mock<ILogger<FullChoreographyVerseChoreographyService>>();
			this.logger = mockLogger.Object;

			this.fullChoreographyVerseChoreographyService = new FullChoreographyVerseChoreographyService(this.logger, repository, mapper);
		}

		[Test]
		public async Task GetVerseChoreographyForDelete_ShouldReturnValidVerseChoreographyWithCorrectDataWhenIdExists()
		{
			var result = await fullChoreographyVerseChoreographyService.GetVerseChoreographyForDeleteAsync(FirstFullChoreographySecondVerse.Id);

			Assert.That(result.Id, Is.EqualTo(FirstFullChoreographySecondVerse.Id));
			Assert.That(result.FullChoreographyId, Is.EqualTo(FirstFullChoreography.Id));
			Assert.That(result.FullChoreographyName, Is.EqualTo(FirstFullChoreography.Name));
		}

		[Test]
		public async Task GetVerseChoreographyForDelete_ShouldThrowExceptionWhenIdDoesntExists()
		{
			Assert.That(async () => await fullChoreographyVerseChoreographyService.GetVerseChoreographyForDeleteAsync(10),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task VerseChoreographyInFullChoreographyExistsForThisUserById_ShouldReturnTrueForValidIdForThisUser()
		{
			var result = await fullChoreographyVerseChoreographyService.VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(FirstFullChoreographySecondVerse.Id, FirstUser.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task VerseChoreographyInFullChoreographyExistsForThisUserById_ShouldReturnFalseForInvalidId()
		{
			var result = await fullChoreographyVerseChoreographyService.VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(10, FirstUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task VerseChoreographyInFullChoreographyExistsForThisUserById_ShouldReturnFalseForValidIdForAnotherUserVerseChoreographyInFullChoreography()
		{
			var result = await fullChoreographyVerseChoreographyService.VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(FirstFullChoreographyFirstVerse.Id, SecondUser.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task VerseChoreographyIsLastForFullChoreographyById_ShouldReturnTrueWhenTheVerseIsTheLastOne()
		{
			var result = await fullChoreographyVerseChoreographyService.VerseChoreographyIsLastForFullChoreographyByIdAsync(FirstFullChoreographySecondVerse.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task VerseChoreographyIsLastForFullChoreographyById_ShouldReturnFalseWhenTheVerseIsNotTheLastOne()
		{
			var result = await fullChoreographyVerseChoreographyService.VerseChoreographyIsLastForFullChoreographyByIdAsync(FirstFullChoreographyFirstVerse.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task VerseChoreographyIsLastForFullChoreographyById_ShouldReturnFalseWhenVerseIsNotValid()
		{
			var result = await fullChoreographyVerseChoreographyService.VerseChoreographyIsLastForFullChoreographyByIdAsync(10);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task AddVerseChoreographyToFullChoreography_AddsSuccessfullyForValidFullChoreography()
		{
			var countVerseChoreographiesForThisFullChoreographyBefore = data.FullChoreographiesVerseChoreographies.Count(v => v.FullChoreographyId == FirstFullChoreography.Id);

			var model = new FullChoreographyVerseChoreographyFormViewModel()
			{
				VerseChoreographyOrder = 3,
				VerseChoreographyId = ThirdVerseChoreography.Id
			};

			await fullChoreographyVerseChoreographyService.AddVerseChoreographyToFullChoreographyAsync(FirstFullChoreography.Id, model);

			var countVerseChoreographiesForThisFullChoreographyAfter = data.FullChoreographiesVerseChoreographies.Count(v => v.FullChoreographyId == FirstFullChoreography.Id);
			Assert.That(countVerseChoreographiesForThisFullChoreographyAfter, Is.EqualTo(countVerseChoreographiesForThisFullChoreographyBefore + 1));
		}

		[Test]
		public async Task AddVerseChoreographyToFullChoreography_ShouldThrowExceptionWhenFullChoreographyIdDoesntExists()
		{
			Assert.That(async () => await fullChoreographyVerseChoreographyService.AddVerseChoreographyToFullChoreographyAsync(10, new FullChoreographyVerseChoreographyFormViewModel()),
				Throws.Exception.TypeOf<EntityNotFoundException>());
		}

		[Test]
		public async Task DeleteVerseChoreographyFromFullChoreography_ShouldDeleteSuccessfully()
		{
			var countVerseChoreographiesForThisFullChoreographyBefore = data.FullChoreographiesVerseChoreographies.Count(v => v.FullChoreographyId == FirstFullChoreography.Id);

			await fullChoreographyVerseChoreographyService.DeleteVerseChoreographyFromFullChoreographyAsync(FirstFullChoreographySecondVerse.Id);

			var countVerseChoreographiesForThisFullChoreographyAfter = data.FullChoreographiesVerseChoreographies.Count(v => v.FullChoreographyId == FirstFullChoreography.Id);
			Assert.That(countVerseChoreographiesForThisFullChoreographyAfter, Is.EqualTo(countVerseChoreographiesForThisFullChoreographyBefore - 1));
		}
	}
}
