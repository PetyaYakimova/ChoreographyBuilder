using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.Figure
{
	/// <summary>
	/// View model for creating and editing figures. 
	/// Added validation attributes.
	/// </summary>
	public class FigureFormViewModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(FigureNameMaxLength,
			MinimumLength = FigureNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Highlight Figure")]
		public bool IsHighlight { get; set; } = false;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Favourite Figure")]
		public bool IsFavourite { get; set; } = false;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Share Figure With Other Users")]
		public bool CanBeShared { get; set; } = false;
	}
}
