using ChoreographyBuilder.Core.Models.Position;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IPositionService
	{
		Task<IEnumerable<PositionForPreviewViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId = null);

		Task AddPositionAsync(PositionFormViewModel model);

		Task ChangePositionStatusAsync(int id);

		Task<PositionQueryServiceModel> AllPositionsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

		Task<PositionFormViewModel?> GetPositionByIdAsync(int id);

		Task<bool> IsPositionUsedInFiguresAsync(int id);

		Task EditPositionAsync(int positionId, PositionFormViewModel model);

		Task<bool> PositionExistByIdAsync(int id);

		Task<PositionForPreviewViewModel?> GetPositionForDeleteAsync(int id);

		Task DeleteAsync(int id);
	}
}
