using ChoreographyBuilder.Core.Models.Position;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IPositionService
	{
		Task<IEnumerable<PositionForFigureViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId);

		Task AddPositionAsync(PositionFormViewModel model);

		Task ChangePositionStatusAsync(int id);

		Task<PositionQueryServiceModel> AllPositionsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);
	}
}
