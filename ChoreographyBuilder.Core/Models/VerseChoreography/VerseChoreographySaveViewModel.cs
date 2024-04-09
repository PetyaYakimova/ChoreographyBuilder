using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// A model for saving a suggested verse choreography. 
	/// Added validation attributes.
	/// </summary>
	public class VerseChoreographySaveViewModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(VerseChoreographyNameMaxLength,
			MinimumLength = VerseChoreographyNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		public int VerseTypeId { get; init; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Display(Name = "Total score:")]
		public int Score { get; init; }

		public IList<VerseChoreographyFigureViewModel> Figures { get; set; } = new List<VerseChoreographyFigureViewModel>();
	}
}
