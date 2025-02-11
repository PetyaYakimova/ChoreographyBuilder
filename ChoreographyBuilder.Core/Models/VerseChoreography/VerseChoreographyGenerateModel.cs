using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreography;

/// <summary>
/// A model for getting the requirements before generating suggestions for verse choreographies.
/// Added validation attributes.
/// </summary>
public class VerseChoreographyGenerateModel
{
	[Required(ErrorMessage = RequiredErrorMessage)]
	[Display(Name = "Verse type")]
	public int VerseTypeId { get; set; }

	[Display(Name = "Start position (Optional)")]
	public int? StartPositionId { get; set; }

	[Required(ErrorMessage = RequiredErrorMessage)]
	[Display(Name = "Final figure")]
	public int FinalFigureId { get; set; }

	public IEnumerable<VerseTypeForPreviewViewModel> VerseTypes { get; set; } = new List<VerseTypeForPreviewViewModel>();

	public IEnumerable<PositionForPreviewViewModel> Positions { get; set; } = new List<PositionForPreviewViewModel>();

	public IEnumerable<FigureForPreviewViewModel> Figures { get; set; } = new List<FigureForPreviewViewModel>();
}
