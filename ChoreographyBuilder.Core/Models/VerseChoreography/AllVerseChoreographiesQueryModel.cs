using ChoreographyBuilder.Core.Models.BaseModels;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.VerseChoreography;

	/// <summary>
	/// A model used for getting criteria for filtering verse choreographies and pagination info.
	/// No added validation attributes.
	/// </summary>
	public class AllVerseChoreographiesQueryModel : AllEntitiesQueryBaseModel<VerseChoreographyTableViewModel>
{
    [Display(Name = "Search by verse type")]
    public int? VerseType { get; init; }

    [Display(Name = "Search by start position")]
    public int? StartPosition { get; init; }

    [Display(Name = "Search by end position")]
    public int? EndPosition { get; init; }

    [Display(Name = "Search by beats")]
    public int? BeatsCount { get; init; }

    [Display(Name = "Search by final figure")]
    public int? FinalFigure { get; init; }

    public IEnumerable<VerseTypeForPreviewViewModel> VerseTypes { get; set; } = new List<VerseTypeForPreviewViewModel>();

    public IEnumerable<PositionForPreviewViewModel> Positions { get; set; } = new List<PositionForPreviewViewModel>();

    public IEnumerable<FigureForPreviewViewModel> Figures { get; set; } = new List<FigureForPreviewViewModel>();
}
