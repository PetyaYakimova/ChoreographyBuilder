using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFigureService
	{
		/// <summary>
		/// Returns FigureFormViewModel for the figure with the selected id.
		/// Throws an exception if there is no such figure with this id.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <returns></returns>
		Task<FigureFormViewModel> GetFigureByIdAsync(int figureId);

		/// <summary>
		/// Return the name of the figure with this id.
		/// Throws an exception if there is no such figure with this id.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <returns></returns>
		Task<string> GetFigureNameByIdAsync(int figureId);

		/// <summary>
		/// Returns the figure for preview model of a figure with the selected id. 
		/// Throws an exception if there is no such figure with this id.
		/// </summary>
		/// <param name="id">Id of the figure</param>
		/// <returns></returns>
		Task<FigureForPreviewViewModel> GetFigureForDeleteAsync(int id);

		/// <summary>
		/// Returns the figure for copy model of a figure with the selected id. 
		/// Throws an exception if there is no such figure with this id.
		/// </summary>
		/// <param name="id">Id of the figure</param>
		/// <returns></returns>
		Task<FigureForCopyViewModel> GetFigureForCopyAsync(int id);

		/// <summary>
		/// Gets the figures for the user (his own figure or the shared figures from other users) by the selected search criteria and returns only those of them that should be displayed on the given page.
		/// </summary>
		/// <param name="userId">Id of the user</param>
		/// <param name="searchTerm"></param>
		/// <param name="currentPage"></param>
		/// <param name="itemsPerPage"></param>
		/// <returns></returns>
		Task<FigureQueryServiceModel> AllUserFiguresAsync(string userId, bool sharedFigures = false, string? searchTerm = null, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedBeatsCount = null, DynamicsType? searchedDynamicsType = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		/// <summary>
		/// Returns a collection with all the highlight figures for the user.
		/// </summary>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<IEnumerable<FigureForPreviewViewModel>> AllUserHighlightFiguresForChoreographiesAsync(string userId);

		/// <summary>
		/// Returns true if the figure is for this user.
		/// Returns false if the figure doesn't exist at all, or if it is for another user.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<bool> FigureExistForThisUserByIdAsync(int figureId, string userId);

		/// <summary>
		/// Returns true if the figure exists and can be shared.
		/// Returns false if the figure doesn't exist at all, or if it cannot be shared.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <returns></returns>
		Task<bool> FigureExistAndCanBeCopiedByIdAsync(int figureId);

		/// <summary>
		/// Returns true if there is at least one verse choreography that uses a figure option of this figure. 
		/// Returns false if the figure is not used for any verse choreography. 
		/// Throws an exception if there is no such figure with this id.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <returns></returns>
		Task<bool> IsFigureUsedInChoreographiesAsync(int figureId);

		/// <summary>
		/// Adds a new figure for the user with data from the model.
		/// Throws an exception if user with the given id doesn't exist.
		/// </summary>
		/// <param name="model">FigureFormViewModel model</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<int> AddFigureAsync(FigureFormViewModel model, string userId);

		/// <summary>
		/// Copies the figure with the given id with all its options for the given user.
		/// Throws an exception if the figure or the user with the given id doesn't exist.
		/// </summary>
		/// <param name="figureId">Id of the existing figure</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<int> CopyFigureForUserAsync(int figureId, string userId);

		/// <summary>
		/// Edits the figure with the given id with the updated data from the model.
		/// Throws an exception if there is no such figure with this id.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <param name="model">FigureFormViewModel model</param>
		/// <returns></returns>
		Task EditFigureAsync(int figureId, FigureFormViewModel model);

		/// <summary>
		/// Deletes a figure by its id.
		/// </summary>
		/// <param name="id">Id of the figure</param>
		/// <returns></returns>
		Task DeleteFigureAsync(int id);
	}
}
