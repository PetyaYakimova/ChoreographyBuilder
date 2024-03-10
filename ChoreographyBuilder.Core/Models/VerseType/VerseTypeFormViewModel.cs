using ChoreographyBuilder.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.VerseType
{
	/// <summary>
	/// View model for creting and editing verse types. Added validations.
	/// </summary>
	public class VerseTypeFormViewModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(VerseTypeNameMaxLenght,
			MinimumLength = VerseTypeNameMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(VerseTypeBeatsCountMin,
			VerseTypeBeatsCountMax,
			ErrorMessage = NumberMustBeInRangeErrorMessage)]
		[IsEven(ErrorMessage = NumberMustBeEvenErrorMessage)]
		[Display(Name = "Beats Count")]
		public int BeatCounts { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		public bool IsActive { get; set; } = true;
	}
}
