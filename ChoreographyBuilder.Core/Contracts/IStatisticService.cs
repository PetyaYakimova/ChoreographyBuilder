using ChoreographyBuilder.Core.Models.Statistics;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IStatisticService
	{
		Task<AdminStatisticModel> GetAdminStatisticsAsync();

		Task<UserStatisticModel> GetUserStatisticsAsync(string userId);
	}
}
