using ChoreographyBuilder.Core.Models.Position;

namespace ChoreographyBuilder.Core.Contracts
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionTableViewModel>> AllPositionsAsync();

        Task<IEnumerable<PositionForFigureViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId);

        Task AddPositionAsync(PositionFormViewModel model);

        Task ChangePositionStatusAsync(int id);
    }
}
