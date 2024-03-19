using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.Figure
{
	public class FigureQueryServiceModel : EntityQueryBaseModel
	{
		public IEnumerable<FigureTableViewModel> Figures { get; set; } = new List<FigureTableViewModel>();
	}
}
