using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.Position
{
	public class AllPositionsQueryModel : AllEntitiesQueryBaseModel
	{
		public IEnumerable<PositionTableViewModel> Positions { get; set; } = new List<PositionTableViewModel>();
	}
}
