using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure;

/// <summary>
/// View model for previewing a figure in verse choreography when replacing it. 
/// Added validation attributes.
/// </summary>
public class VerseChoreographyFigureReplaceViewModel : VerseChoreographyFigureViewModel
{
	[Required(ErrorMessage = RequiredErrorMessage)]
	[Display(Name = "New figure:")]
	public int ReplacementFigureOptionId { get; set; }

	public IEnumerable<VerseChoreographyFigureViewModel> PossibleReplacementFigures { get; set; } = new List<VerseChoreographyFigureViewModel>();
}
