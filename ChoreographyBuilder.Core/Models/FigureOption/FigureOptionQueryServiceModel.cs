using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.FigureOption
{
	public class FigureOptionQueryServiceModel : EntityQueryBaseModel<FigureOptionTableViewModel>
	{
		public int FigureId { get; set; }

		public string FigureName { get; set; } = string.Empty;
	}
}
