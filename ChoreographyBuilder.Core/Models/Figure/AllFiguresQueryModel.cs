using ChoreographyBuilder.Core.Models.BaseModels;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.Figure;

/// <summary>
/// A model used for getting criteria for filtering figures and pagination info.
/// No added validation attributes.
/// </summary>
public class AllFiguresQueryModel : AllEntitiesQueryBaseModel <FigureTableViewModel>
{
	[Display(Name = "Option start position")]
	public int? StartPosition { get; init; }

	[Display(Name = "Option end position")]
	public int? EndPosition { get; init; }

	[Display(Name = "Option beats count")]
	public int? BeatsCount { get; init; }

	[Display(Name = "Option dynamics type")]
	public DynamicsType? DynamicsType { get; init; }

	public IEnumerable<PositionForPreviewViewModel> Positions { get; set; } = new List<PositionForPreviewViewModel>();

	public IEnumerable<DynamicsType> DynamicsTypes { get; set; } = new List<DynamicsType>();
}
