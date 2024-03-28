using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseType;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseTypeService
	{
		Task<IEnumerable<VerseTypeForPreviewViewModel>> AllActiveVerseTypesOrSelectedVerseTypeAsync(int? selectedVerseTypeId = null);

		Task AddVerseTypeAsync(VerseTypeFormViewModel model);

        Task ChangeVerseTypeStatusAsync(int id);

		Task<VerseTypeQueryServiceModel> AllVerseTypesAsync(string? searchTerm = null, int? searchedBeatsCount = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<VerseTypeFormViewModel?> GetVerseTypeById(int id);

		Task<bool> IsVerseTypeUsedInChoreographiesAsync(int id);

        Task EditVerseTypeAsync(int verseTypeId, VerseTypeFormViewModel model);

		Task<bool> VerseTypeExistByIdAsync(int id);

        Task<VerseTypeForPreviewViewModel?> GetVerseTypeForDeleteAsync(int id);

		Task DeleteAsync(int id);
    }
}
