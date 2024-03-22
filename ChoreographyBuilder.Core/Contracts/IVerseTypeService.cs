using ChoreographyBuilder.Core.Models.VerseType;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseTypeService
	{
		Task<IEnumerable<VerseTypeForChoreographiesViewModel>> AllActiveVerseTypesOrSelectedVerseTypeAsync(int selectedVerseTypeId);

		Task AddVerseTypeAsync(VerseTypeFormViewModel model);

        Task ChangeVerseTypeStatusAsync(int id);

		Task<VerseTypeQueryServiceModel> AllVerseTypesAsync(string? searchTerm = null, int? searchedBeatsCount = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<VerseTypeFormViewModel?> GetVerseTypeById(int id);

		Task<bool> IsVerseTypeUsedInChoreographiesAsync(int id);

        Task EditVerseTypeAsync(int verseTypeId, VerseTypeFormViewModel model);
    }
}
