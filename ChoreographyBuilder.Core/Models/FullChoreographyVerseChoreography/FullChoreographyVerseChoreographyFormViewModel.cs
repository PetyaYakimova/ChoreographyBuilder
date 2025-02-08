using ChoreographyBuilder.Core.Models.VerseChoreography;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;

/// <summary>
/// View model only for when selecting verse choreography for full choreography. 
/// Added validation attributes.
/// </summary>
public class FullChoreographyVerseChoreographyFormViewModel
{
	[Required(ErrorMessage = RequiredErrorMessage)]
	[Display(Name = "Verse choreography:")]
	public int VerseChoreographyId { get; set; }

	[Required(ErrorMessage = RequiredErrorMessage)]
	[Display(Name = "Order:")]
	public int VerseChoreographyOrder { get; set; }

	[Display(Name = "Start position:")]
	public string? StartPositionName { get; set; }

	public IEnumerable<VerseChoreographyTableViewModel> VerseChoreographies { get; set; } = new List<VerseChoreographyTableViewModel>();
}
