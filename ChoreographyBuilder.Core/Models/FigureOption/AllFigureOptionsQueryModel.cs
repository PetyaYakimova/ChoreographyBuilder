using ChoreographyBuilder.Core.Models.BaseModels;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.FigureOption
{
	/// <summary>
	/// A model used for getting criteria for filtering figure options and pagination info.
	/// No added validation attributes.
	/// </summary>
	public class AllFigureOptionsQueryModel : AllEntitiesQueryBaseModel<FigureOptionTableViewModel>
	{
		public int? FigureId { get; set; }
		public string FigureName { get; set; } = string.Empty;

		[Display(Name = "Start position")]
		public int? StartPosition { get; init; }

		[Display(Name = "End position")]
		public int? EndPosition { get; init; }

		[Display(Name = "Beats count")]
		public int? BeatsCount { get; init; }

		[Display(Name = "Dynamics type")]
		public DynamicsType? DynamicsType { get; init; }

		public IEnumerable<PositionForPreviewViewModel> Positions { get; set; } = new List<PositionForPreviewViewModel>();

		public IEnumerable<DynamicsType> DynamicsTypes { get; set; } = new List<DynamicsType>();
	}
}
