using ChoreographyBuilder.Core.Models.Statistics;
using ChoreographyBuilder.Core.Models.User;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
    public interface IUserService
	{
		Task<AdminStatisticModel> GetAdminStatisticsAsync();

		Task<UserStatisticModel> GetUserStatisticsAsync(string userId);

		Task<UserQueryServiceModel> GetAllUserStatisticsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);
	}
}
