namespace ChoreographyBuilder.Core.Models.Position
{
	public class PositionQueryServiceModel
	{
		public int TotalCount { get; set; }

		public IEnumerable<PositionTableViewModel> Positions { get; set; } = new List<PositionTableViewModel>();
	}
}
