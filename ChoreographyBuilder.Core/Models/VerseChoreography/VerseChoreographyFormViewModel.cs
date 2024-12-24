using ChoreographyBuilder.Core.Models.VerseType;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// A model for generating by hand a verse choreography. 
	/// Added validation attributes.
	/// </summary>
	public class VerseChoreographyFormViewModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(VerseChoreographyNameMaxLength,
			MinimumLength = VerseChoreographyNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		public int VerseTypeId { get; init; }

		public IEnumerable<VerseTypeForPreviewViewModel> VerseTypes { get; set; } = new List<VerseTypeForPreviewViewModel>();
	}
}
