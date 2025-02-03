using ChoreographyBuilder.Core.Models.FullChoreography;
using ChoreographyBuilder.Core.Models.Position;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts;

public interface IFullChoreographyService
{
	/// <summary>
	/// Returns FullChoreographyDetailsViewModel for the choreography with the selected id.
	/// Throws an exception if there is no such choreography with this id.
	/// </summary>
	/// <param name="id">Id of the full choreography</param>
	/// <returns></returns>
	Task<FullChoreographyDetailsViewModel> GetChoreographyDetailsByIdAsync(int id);

	/// <summary>
	/// Returns FullChoreographyFormViewModel for the choreography with the selected id.
	/// Throws an exception if there is no such choreography with this id.
	/// </summary>
	/// <param name="id">Id of the full choreography</param>
	/// <returns></returns>
	Task<FullChoreographyFormViewModel> GetChoreographyForEditByIdAsync(int id);

	/// <summary>
	/// Returns FullChoreographyTableViewModel for the choreography with the selected id.
	/// Throws an exception if there is no such choreography with this id.
	/// </summary>
	/// <param name="id">Id of the full choreography</param>
	/// <returns></returns>
	Task<FullChoreographyTableViewModel> GetFullChoreographyForDeleteAsync(int id);

	/// <summary>
	/// Returns the end position of the last verse choreography in the verse choreography. 
	/// Returns null if the full choreography doesn't have any verse choreographies or if the full choreography doesn't exist at all.
	/// </summary>
	/// <param name="fullChoreographyId">Id of the full choreography</param>
	/// <returns></returns>
	Task<PositionForPreviewViewModel?> GetLastVerseChoreographyEndPositionAsync(int fullChoreographyId);

	/// <summary>
	/// Returns the number of verse choreographies in the full choreography.
	/// Throws an exception if there is no such choreography with this id.
	/// </summary>
	/// <param name="fullChoreographyId">Id of the full choreography</param>
	/// <returns></returns>
	Task<int> GetNumberOfVerseChoreographiesForFullChoreographyAsync(int fullChoreographyId);

	/// <summary>
	/// Gets the full choreographies for for the user by the selected search criteria and returns only those of them that should be displayed on the given page.
	/// </summary>
	/// <param name="userId">Id of the user</param>
	/// <param name="searchTerm"></param>
	/// <param name="currentPage"></param>
	/// <param name="itemsPerPage"></param>
	/// <returns></returns>
	Task<FullChoreographyQueryServiceModel> AllUserFullChoreographiesAsync(string userId, string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

	/// <summary>
	/// Returns true if the full choreography is for this user.
	/// Returns false if the full choreography doesn't exist at all, or if it is for another user.
	/// </summary>
	/// <param name="id">Id of the full choreography</param>
	/// <param name="userId">Id of the user</param>
	/// <returns></returns>
	Task<bool> FullChoreographyExistForThisUserByIdAsync(int id, string userId);

	/// <summary>
	/// Adds a new full choreography for the user with data from the model.
	/// Throws an exception if a user with the given id doesn't exist.
	/// </summary>
	/// <param name="model">FullChoreographyFormViewModel model</param>
	/// <param name="userId">Id of the user</param>
	/// <returns></returns>
	Task<int> AddFullChoreographyAsync(FullChoreographyFormViewModel model, string userId);

	/// <summary>
	/// Edits the full choreography with the given id with the updated data from the model.
	/// Throws an exception if there is no such full choreography with this id.
	/// </summary>
	/// <param name="id">Id of the full choreography</param>
	/// <param name="model">FullChoreographyFormViewModel model</param>
	/// <returns></returns>
	Task EditFullChoreographyAsync(int id, FullChoreographyFormViewModel model);

	/// <summary>
	/// Deletes a full choreography by its id.
	/// </summary>
	/// <param name="id">Id of the full choreography</param>
	/// <returns></returns>
	Task DeleteFullChoreographyAsync(int id);

}
