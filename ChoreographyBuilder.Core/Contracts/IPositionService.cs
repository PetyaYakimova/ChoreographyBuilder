using ChoreographyBuilder.Core.Models.Position;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IPositionService
	{
		Task<IEnumerable<PositionForFigureViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId);

		Task AddPositionAsync(PositionFormViewModel model);

		Task ChangePositionStatusAsync(int id);

		//Rename me
		Task<PositionQueryServiceModel> AllPositionsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = 10);
	}
}
