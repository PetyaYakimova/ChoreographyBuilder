using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// A view model for previewing a suggested verse choreography and saving some. Added validations for the fields that are filled by the user.
	/// </summary>
	public class SuggestedVerseChoreographyModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(VerseChoreographyNameMaxLength,
			MinimumLength = VerseChoreographyNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Display(Name = "Verse type")]
		public int VerseTypeId { get; init; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		public int Score { get; init; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		public string UserId { get; init; } = string.Empty;

		public IEnumerable<VerseChoreographyFigureViewModel> Figures { get; init; } = new List<VerseChoreographyFigureViewModel>();
	}
}
