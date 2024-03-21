using ChoreographyBuilder.Core.Models.Figure;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFigureService
	{
		Task<FigureQueryServiceModel> AllUserFiguresAsync(string userId, string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<int> AddFigureAsync(FigureFormViewModel model, string userId);

		Task EditFigureAsync(int figureId, FigureFormViewModel model);

		Task<FigureFormViewModel?> GetFigureByIdAsync(int figureId);

		Task<string> GetFigureNameByIdAsync(int figureId);

		Task<string> GetUserIdForFigureByIdAsync(int figureId);

		Task<bool> IsFigureUsedInChoreographiesAsync(int figureId);
    }
}
