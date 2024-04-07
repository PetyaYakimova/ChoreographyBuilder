﻿using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFigureOptionService
	{
		/// <summary>
		/// Gets the figure options for a figure by the selected search criteria and returns only those of them that should be displayed on the given page.
		/// IThrows an exception if there is no such figure with the given figure id.
		/// </summary>
		/// <param name="figureId">Id of the figure</param>
		/// <param name="searchedStartPositionId"></param>
		/// <param name="searchedEndPositionId"></param>
		/// <param name="searchedBeatsCount"></param>
		/// <param name="searchedDynamicsType"></param>
		/// <param name="currentPage"></param>
		/// <param name="itemsPerPage"></param>
		/// <returns>FigureOptionQueryServiceModel with the options for the current page and the total number of opitons by this search criteria</returns>
		Task<FigureOptionQueryServiceModel> GetFigureOptionsAsync(int figureId, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedBeatsCount = null, DynamicsType? searchedDynamicsType = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		/// <summary>
		/// Adds a new figure option with data from the model.
		/// </summary>
		/// <param name="model">FigureOptionFormViewModel model</param>
		/// <returns></returns>
		Task AddFigureOptionAsync(FigureOptionFormViewModel model);

		/// <summary>
		/// Returns FigureOptionFormViewModel for the figure option with the selected id.
		/// Throws an exception if there is no such figure option with this id.
		/// </summary>
		/// <param name="optionId">Id of the option</param>
		/// <returns></returns>
		Task<FigureOptionFormViewModel> GetFigureOptionByIdAsync(int optionId);

		/// <summary>
		/// Returns true if there is at least one verse choreography that uses this figure option. 
		/// Returns false if the figure option is not used for any verse choreography. 
		/// Throws an exception if there is no such figure option with this id.
		/// </summary>
		/// <param name="optionId">Id of the option</param>
		/// <returns></returns>
		Task<bool> IsFigureOptionUsedInChoreographiesAsync(int optionId);

		/// <summary>
		/// Edits the figure option with the given id with the updated data from the model.
		/// </summary>
		/// <param name="optionId">Id of the option</param>
		/// <param name="model">FigureOptionFormViewModel model</param>
		/// <returns></returns>
		Task EditFigureOptionAsync(int optionId, FigureOptionFormViewModel model);

		/// <summary>
		/// Returns true if the figure options is linked to a figure that is for this user. 
		/// Returns false if the figuure option doesn't exist at all, or if it is for another user.
		/// </summary>
		/// <param name="optionId">Id of the option</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<bool> FigureOptionExistForThisUserByIdAsync(int optionId, string userId);

		/// <summary>
		/// Returns the figure option delete model of a figure option with the selected id. 
		/// Throws an exception if there is no such figure option with this id.
		/// </summary>
		/// <param name="id">Id of the option</param>
		/// <returns>FigureOptionDeleteModel</returns>
		Task<FigureOptionDeleteViewModel> GetFigureOptionForDeleteAsync(int id);

		/// <summary>
		/// Deletes a figure option by its id.
		/// </summary>
		/// <param name="id">Id of the option</param>
		/// <returns></returns>
		Task DeleteAsync(int id);
	}
}
