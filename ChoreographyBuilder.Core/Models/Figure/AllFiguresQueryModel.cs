using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.Figure
{
	public class AllFiguresQueryModel : AllEntitiesQueryBaseModel 
	{
		public IEnumerable<FigureTableViewModel> Figures { get; set; } = new List<FigureTableViewModel>();
	}
}
