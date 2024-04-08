using ChoreographyBuilder.Core.Models.VerseChoreography;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseChoreographyService
	{
		/// <summary>
		/// Returns VerseChoreographyDetailsViewModel for the verse choreography with the selected id.
		/// Throws an exception if there is no such verse choreography with this id.
		/// </summary>
		/// <param name="id">Id of the verse choreography</param>
		/// <returns></returns>
		Task<VerseChoreographyDetailsViewModel> GetChoreographyByIdAsync(int id);

		/// <summary>
		/// Returns VerseChoreographyDeleteViewModel for the verse choreography with the selected id.
		/// Throws an exception if there is no such verse choreography with this id.
		/// </summary>
		/// <param name="id">Id of the verse choreography</param>
		/// <returns></returns>
		Task<VerseChoreographyDeleteViewModel> GetVerseChoreographyForDeleteAsync(int id);

		/// <summary>
		/// Gets the verse choreographies for the user by the selected search criteria and returns only those of them that should be displayed on the given page.
		/// </summary>
		/// <param name="userId">Id of the user</param>
		/// <param name="searchTerm"></param>
		/// <param name="searchedVerseTypeId"></param>
		/// <param name="searchedStartPositionId"></param>
		/// <param name="searchedEndPositionId"></param>
		/// <param name="searchedFinalFigureId"></param>
		/// <param name="currentPage"></param>
		/// <param name="itemsPerPage"></param>
		/// <returns></returns>
		Task<VerseChoreographyQueryServiceModel> AllUserVerseChoreographiesAsync(string userId, string? searchTerm = null, int? searchedVerseTypeId = null, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedFinalFigureId = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		/// <summary>
		/// Returns a collection with all the verse choreographies for the user that start with a certain start position.
		/// </summary>
		/// <param name="userId">Id of the user</param>
		/// <param name="startPositionId">Id of the start position</param>
		/// <returns></returns>
		Task<IEnumerable<VerseChoreographyTableViewModel>> AllUserVerseChoreographiesStartingWithPositionAsync(string userId, int? startPositionId = null);

		/// <summary>
		/// Returns true if the verse choreography is for this user.
		/// Returns false if the verse choreography doesn't exist at all, or if it is for another user.
		/// </summary>
		/// <param name="id">Id of the verse choreorgaphy</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<bool> VerseChoreographyExistForThisUserByIdAsync(int id, string userId);

		/// <summary>
		/// Returns true if there is at least one full choreography that uses this verse choreography 
		/// Returns false if the verse choreography is not used for any full choreography. 
		/// Throws an exception if there is no such verse choreography with this id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<bool> IsVerseChoreographyUsedInFullChoreographies(int id);

		/// <summary>
		/// Adds a new verse choreography for the user with data from the model.
		/// Throws an exception if user with the given id doesn't exist.
		/// </summary>
		/// <param name="model">VerseChoreographySaveViewModel model</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task SaveChoreographyAsync(VerseChoreographySaveViewModel model, string userId);

		/// <summary>
		/// Deletes a verse choreography by its id.
		/// </summary>
		/// <param name="id">Id of the verse choreography</param>
		/// <returns></returns>
		Task DeleteVerseChoreographyAsync(int id);

		/// <summary>
		/// Returns a collection with suggested verse choreographies for the user based on the selected criteria.
		/// </summary>
		/// <param name="query">VerseChoreographyGenerateModel query</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<IList<VerseChoreographySaveViewModel>> GenerateChoreographies(VerseChoreographyGenerateModel query, string userId);
	}
}
