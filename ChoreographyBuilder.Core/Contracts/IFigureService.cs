using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFigureService
	{
		Task<FigureQueryServiceModel> AllUserFiguresAsync(string userId, string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<int> AddFigureAsync(FigureFormViewModel model, string userId);

        Task<string> GetUserIdForFigureByIdAsync(int figureId);

        Task<FigureWithOptionsViewModel> GetFigureWithOptionsAsync(int figureId);

        Task<string> GetFigureNameByIdAsync(int figureId);

        Task AddFigureOptionAsync(FigureOptionFormViewModel model);
    }
}
