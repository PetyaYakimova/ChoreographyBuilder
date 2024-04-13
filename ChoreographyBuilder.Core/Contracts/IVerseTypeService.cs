using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseType;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseTypeService
	{
		/// <summary>
		/// Returns VerseTypeFormViewModel for the verse type with the selected id.
		/// Throws an exception if there is no such verse type with this id.
		/// </summary>
		/// <param name="id">Id of the verse type</param>
		/// <returns></returns>
		Task<VerseTypeFormViewModel> GetVerseTypeById(int id);

		/// <summary>
		/// Returns VerseTypeForPreviewViewModel for the verse type with the selected id.
		/// Throws an exception if there is no such verse type with this id.
		/// </summary>
		/// <param name="id">Id of the verse type</param>
		/// <returns></returns>
		Task<VerseTypeForPreviewViewModel> GetVerseTypeForDeleteAsync(int id);

		/// <summary>
		/// Gets the verse types by the selected search criteria and returns only those of them that should be displayed on the given page.
		/// </summary>
		/// <param name="searchTerm"></param>
		/// <param name="searchedBeatsCount"></param>
		/// <param name="currentPage"></param>
		/// <param name="itemsPerPage"></param>
		/// <returns></returns>
		Task<VerseTypeQueryServiceModel> AllVerseTypesAsync(string? searchTerm = null, int? searchedBeatsCount = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		/// <summary>
		/// Returns a collection with all the active verse types and the verse type with the given id even if it is not active.
		/// </summary>
		/// <param name="selectedVerseTypeId">Id of the selected verse type</param>
		/// <returns></returns>
		Task<IEnumerable<VerseTypeForPreviewViewModel>> AllActiveVerseTypesOrSelectedVerseTypeAsync(int? selectedVerseTypeId = null);

		/// <summary>
		/// Returns true if the verse type exists.
		/// Returns false if there is no such verse type with this id.
		/// </summary>
		/// <param name="id">Id of the verse type</param>
		/// <returns></returns>
		Task<bool> VerseTypeExistByIdAsync(int id);

		/// <summary>
		/// Returns true if there is at least one verse choreography that uses this verse type. Otherwise it returns false.
		/// Throws an exception if there is no verse type with this id.
		/// </summary>
		/// <param name="id">Id of the verse type</param>
		/// <returns></returns>
		Task<bool> IsVerseTypeUsedInChoreographiesAsync(int id);

		/// <summary>
		/// Adds a new verse type with data from the model.
		/// Throws an exception if the given beats count in the model are not an even number.
		/// </summary>
		/// <param name="model">VerseTypeFormViewModel model</param>
		/// <returns></returns>
		Task AddVerseTypeAsync(VerseTypeFormViewModel model);

		/// <summary>
		/// Changes the status of the verse type with this id.
		/// Throws an exception if there is no verse type with this id.
		/// </summary>
		/// <param name="id">Id of the verse type</param>
		/// <returns></returns>
		Task ChangeVerseTypeStatusAsync(int id);

		/// <summary>
		/// Edits the verse type with the given id with the updated data from the model.
		/// Throws an exception if there is no such verse type with this id.
		/// Throws an exception if the given beats count in the model are not an even number.
		/// </summary>
		/// <param name="verseTypeId">Id of the verse type</param>
		/// <param name="model">VerseTypeFormViewModel model</param>
		/// <returns></returns>
		Task EditVerseTypeAsync(int verseTypeId, VerseTypeFormViewModel model);

		/// <summary>
		/// Deletes a verse type by its id.
		/// </summary>
		/// <param name="id">Id of the verse type</param>
		/// <returns></returns>
		Task DeleteVerseTypeAsync(int id);
	}
}
