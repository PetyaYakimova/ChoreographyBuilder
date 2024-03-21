using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFigureOptionService
	{
		Task<FigureOptionQueryServiceModel> GetFigureOptionsAsync(int figureId, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedBeatsCount = null, DynamicsType? searchedDynamicsType = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task AddFigureOptionAsync(FigureOptionFormViewModel model);

		Task<string> GetUserIdForFigureOptionByIdAsync(int optionId);

		Task<FigureOptionFormViewModel?> GetFigureOptionByIdAsync(int optionId);

		Task<bool> IsFigureOptionUsedInChoreographiesAsync(int optionId);

		Task EditFigureOptionAsync(int optionId, FigureOptionFormViewModel model);
	}
}
