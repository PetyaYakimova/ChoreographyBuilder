using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.FigureOption
{
	/// <summary>
	/// A model that has the total count of figure options and a collection of a certain amount of them to display them on pages.
	/// No added validation attributes.
	/// </summary>
	public class FigureOptionQueryServiceModel : EntityQueryBaseModel<FigureOptionTableViewModel>
	{
		public int FigureId { get; set; }

		public string FigureName { get; set; } = string.Empty;
	}
}
