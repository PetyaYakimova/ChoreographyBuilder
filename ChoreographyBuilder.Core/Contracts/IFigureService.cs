using ChoreographyBuilder.Core.Models.Figure;

namespace ChoreographyBuilder.Core.Contracts
{
    public interface IFigureService
	{
		Task<IEnumerable<FigureTableViewModel>> AllUserFiguresAsync(string userId);

        Task<int> AddFigureAsync(FigureFormViewModel model, string userId);

        Task<string> GetUserIdForFigureByIdAsync(int figureId);

        Task<FigureWithOptionsViewModel> GetFigureWithOptionsAsync(int figureId);
    }
}
