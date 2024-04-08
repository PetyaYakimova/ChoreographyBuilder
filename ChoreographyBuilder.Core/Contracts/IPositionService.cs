using ChoreographyBuilder.Core.Models.Position;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IPositionService
	{
		/// <summary>
		/// Returns PositionFormViewModel for the position with the selected id.
		/// Throws an exception if there is no such position with this id.
		/// </summary>
		/// <param name="id">Id of the position</param>
		/// <returns></returns>
		Task<PositionFormViewModel> GetPositionByIdAsync(int id);

		/// <summary>
		/// Returns PositionForPreviewViewModel for the position with the selected id.
		/// Throws an exception if there is no such position with this id.
		/// </summary>
		/// <param name="id">Id of the position</param>
		/// <returns></returns>
		Task<PositionForPreviewViewModel> GetPositionForDeleteAsync(int id);

		/// <summary>
		/// Gets the positions by the selected search criteria and returns only those of them that should be displayed on the given page.
		/// </summary>
		/// <param name="searchTerm"></param>
		/// <param name="currentPage"></param>
		/// <param name="itemsPerPage"></param>
		/// <returns></returns>
		Task<PositionQueryServiceModel> AllPositionsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		/// <summary>
		/// Returns a collection with all the active positions and the position with the given id even if it is not active.
		/// </summary>
		/// <param name="selectedPositionId">Id of the currently selected position</param>
		/// <returns></returns>
		Task<IEnumerable<PositionForPreviewViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId = null);

		/// <summary>
		/// Returns true if the position exists.
		/// Returns false if there is no such position with this id.
		/// </summary>
		/// <param name="id">Id of the position</param>
		/// <returns></returns>
		Task<bool> PositionExistByIdAsync(int id);

		/// <summary>
		/// Returns true if there is at least one figure option that uses this position for start or end figure. Otherwise it returs false.
		/// Throws an exception if there is no position with this id.
		/// </summary>
		/// <param name="id">Id of the position</param>
		/// <returns></returns>
		Task<bool> IsPositionUsedInFiguresAsync(int id);

		/// <summary>
		/// Adds a new position with data from the model.
		/// </summary>
		/// <param name="model">PositionFormViewModel model</param>
		/// <returns></returns>
		Task AddPositionAsync(PositionFormViewModel model);

		/// <summary>
		/// Changes the status of the position with this id.
		/// Throws an exception if there is no position with this id.
		/// </summary>
		/// <param name="id">Id of the position</param>
		/// <returns></returns>
		Task ChangePositionStatusAsync(int id);

		/// <summary>
		/// Edits the position with the given id with the updated data from the model.
		/// Throws an exception if there is no such position with this id.
		/// </summary>
		/// <param name="positionId">Id of the position</param>
		/// <param name="model">PositionFormViewModel model</param>
		/// <returns></returns>
		Task EditPositionAsync(int positionId, PositionFormViewModel model);

		/// <summary>
		/// Deletes a position by its id.
		/// </summary>
		/// <param name="id">Id of the position</param>
		/// <returns></returns>
		Task DeletePositionAsync(int id);
	}
}
