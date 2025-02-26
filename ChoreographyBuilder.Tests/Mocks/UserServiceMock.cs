using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Statistics;
using Moq;

namespace ChoreographyBuilder.Tests.Mocks;

public class UserServiceMock
{
	public static IUserService Instance
	{
		get
		{
			var userServiceMock = new Mock<IUserService>();


			userServiceMock
				.Setup(s => s.GetUserStatisticsAsync("userId"))
				.ReturnsAsync(new UserStatisticModel()
				{
					MyTotalNumberOfFigures = 10,
					MyTotalNumberOfVerseChoreographies = 3,
					MyTotalNumberOfFullChoreographies = 1
				});

			return userServiceMock.Object;
		}
	}
}
