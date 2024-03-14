using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;

namespace ChoreographyBuilder.Core.Contracts
{
    public interface IFigureService
	{
		Task<IEnumerable<FigureTableViewModel>> AllUserFiguresAsync(string userId);

        Task<int> AddFigureAsync(FigureFormViewModel model, string userId);

        Task<string> GetUserIdForFigureByIdAsync(int figureId);

        Task<FigureWithOptionsViewModel> GetFigureWithOptionsAsync(int figureId);

        Task<string> GetFigureNameByIdAsync(int figureId);

        Task AddFigureOptionAsync(FigureOptionFormViewModel model);
    }
}
