using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.Figure
{
	/// <summary>
	/// View model for creting and editing figures. Added validations.
	/// </summary>
	public class FigureFormViewModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(FigureNameMaxLength,
			MinimumLength = FigureNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Hightlight Figure")]
		public bool IsHighlight { get; set; } = false;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Favourite Figure")]
		public bool IsFavourite { get; set; } = false;
	}
}
