using ChoreographyBuilder.Core.Models.Statistics;
using ChoreographyBuilder.Core.Models.User;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts;

    public interface IUserService
{
	/// <summary>
	/// Returns AdminStatisticsModel with all the data needed for the administrator statistics.
	/// </summary>
	/// <returns></returns>
	Task<AdminStatisticModel> GetAdminStatisticsAsync();

	/// <summary>
	/// Returns UserStatisticModel with all the data needed for the user statistics for this user.
	/// </summary>
	/// <param name="userId">Id of the user</param>
	/// <returns></returns>
	Task<UserStatisticModel> GetUserStatisticsAsync(string userId);

	/// <summary>
	/// Gets the statistics for all the users and filters them by the selected search criteria and returns only those of them that should be displayed on the given page.
	/// </summary>
	/// <param name="searchTerm"></param>
	/// <param name="currentPage"></param>
	/// <param name="itemsPerPage"></param>
	/// <returns></returns>
	Task<UserQueryServiceModel> GetAllUserStatisticsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);
}
