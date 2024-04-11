using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoreographyBuilder.Tests.UnitTests
{
	[TestFixture]
	public class UserServiceTests : UnitTestsBase
	{
		private IUserService userService;

		[SetUp]
		public void Setup()
		{
			this.userService = new UserService(repository);
		}

		[Test]
		public async Task GetAdminStatistics_ShouldReturnTheCorrectData()
		{
			var result = await userService.GetAdminStatisticsAsync();

			Assert.That(result.NumberOfActivePositions, Is.EqualTo(data.Positions.Count(p => p.IsActive)));
			Assert.That(result.TotalNumberOfPositions, Is.EqualTo(data.Positions.Count()));
			Assert.That(result.NumberOfActiveVerseTypes, Is.EqualTo(data.VerseTypes.Count(v => v.IsActive)));
			Assert.That(result.TotalNumberOfVerseTypes, Is.EqualTo(data.VerseTypes.Count()));
			Assert.That(result.TotalNumberOfFigures, Is.EqualTo(data.Figures.Count()));
			Assert.That(result.UsersWithAtLeastOneFigure, Is.EqualTo(2));
			Assert.That(result.TotalNumberOfSavedVerseChoreographies, Is.EqualTo(data.VerseChoreographies.Count()));
			Assert.That(result.UsersWithAtLeastOneVerseChoreography, Is.EqualTo(2));
			Assert.That(result.TotalNumberOfSavedFullChoreographies, Is.EqualTo(data.FullChoreographies.Count()));
			Assert.That(result.UsersWithAtLeastOneFullChoreography, Is.EqualTo(1));
		}

		[Test]
		public async Task GetUserStatistics_ShouldReturnTheCorrectData()
		{
			var result = await userService.GetUserStatisticsAsync(FirstUser.Id);

			Assert.That(result.MyTotalNumberOfFigures, Is.EqualTo(data.Figures.Count(f => f.UserId == FirstUser.Id)));
			Assert.That(result.MyTotalNumberOfVerseChoreographies, Is.EqualTo(data.VerseChoreographies.Count(v => v.UserId == FirstUser.Id)));
			Assert.That(result.MyTotalNumberOfFullChoreographies, Is.EqualTo(data.FullChoreographies.Count(f => f.UserId == FirstUser.Id)));
		}

		[Test]
		public async Task GetAllUserStatistics_ShouldReturnTheCorrectDataForAllUsersWhenNoSearchCriteria()
		{
			var result = await userService.GetAllUserStatisticsAsync();

			Assert.That(result.TotalCount, Is.EqualTo(data.Users.Count()));
			Assert.That(result.Entities.Count(), Is.EqualTo(data.Users.Count()));
		}

		[Test]
		public async Task GetAllUserStatistics_ShouldReturnTheCorrectDataForSomeUsersWhenSearchCriteriaIsAdded()
		{
			var result = await userService.GetAllUserStatisticsAsync("First");

			Assert.That(result.TotalCount, Is.EqualTo(1));
			Assert.That(result.Entities.Count(), Is.EqualTo(1));
		}
	}
}
