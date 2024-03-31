using ChoreographyBuilder.Core.Models.FullChoreography;
using ChoreographyBuilder.Core.Models.Position;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFullChoreographyService
	{
		Task<FullChoreographyQueryServiceModel> AllUserFullChoreographiesAsync(string userId, string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<int> AddFullChoreographyAsync(FullChoreographyFormViewModel model, string userId);

		Task<bool> FullChoreographyExistForThisUserByIdAsync(int id, string userId);

		Task<FullChoreographyFormViewModel> GetChoreographyForEditByIdAsync(int id);

		Task EditFullChoreographyAsync(int id, FullChoreographyFormViewModel model);

		Task<FullChoreographyDetailsViewModel> GetChoreographyDetailsByIdAsync(int id);

		Task<PositionForPreviewViewModel?> GetLastVerseChoreographyEndPositionAsync(int fullChoreographyId);

		Task<int> GetNumberOfVerseChoreographiesForFullChoreographyAsync(int fullChoreographyId);

		Task<FullChoreographyTableViewModel> GetFullChoreographyForDeleteAsync(int id);

		Task DeleteAsync(int id);

	}
}
