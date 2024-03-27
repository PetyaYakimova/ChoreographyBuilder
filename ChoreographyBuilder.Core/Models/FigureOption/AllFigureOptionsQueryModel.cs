using ChoreographyBuilder.Core.Models.BaseModels;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.FigureOption
{
	public class AllFigureOptionsQueryModel : AllEntitiesQueryBaseModel<FigureOptionTableViewModel>
	{
		public int? FigureId { get; set; }
		public string FigureName { get; set; } = string.Empty;

		[Display(Name = "Search by start position")]
		public int? StartPosition { get; init; }

		[Display(Name = "Search by end position")]
		public int? EndPosition { get; init; }

		[Display(Name = "Search by beats")]
		public int? BeatsCount { get; init; }

		[Display(Name = "Search by dynamics type")]
		public DynamicsType? DynamicsType { get; init; }

		public IEnumerable<PositionForPreviewViewModel> Positions { get; set; } = new List<PositionForPreviewViewModel>();

		public IEnumerable<DynamicsType> DynamicsTypes { get; set; } = new List<DynamicsType>();
	}
}
