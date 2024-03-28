using ChoreographyBuilder.Core.Models.VerseChoreography;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseChoreographyService
	{
		Task<VerseChoreographyQueryServiceModel> AllUserVerseChoreographiesAsync(string userId, string? searchTerm = null, int? searchedVerseTypeId = null, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedFinalFigureId = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<VerseChoreographyDetailsViewModel?> GetChoreographyByIdAsync(int id);

		Task<IList<VerseChoreographySaveViewModel>> GenerateChoreographies(VerseChoreographyGenerateModel query, string userId);

		Task<bool> VerseChoreographyExistForThisUserByIdAsync(int id, string userId);

		Task SaveChoreographyAsync(VerseChoreographySaveViewModel model, string userId);

		Task DeleteAsync(int id);

		Task<bool> IsVerseChoreographyUsedInFullChoreographies(int id);

		Task<VerseChoreographyDeleteViewModel?> GetVerseChoreographyForDeleteAsync(int id);

		Task<IEnumerable<VerseChoreographyTableViewModel>> AllUserVerseChoreographiesStartingWithPositionAsync(string userId, int? startPositionId = null);
	}
}
