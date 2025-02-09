using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.Position;

/// <summary>
/// View model for creating and editing positions. 
/// Added validation attributes.
/// </summary>
public class PositionFormViewModel
{
	[Required(ErrorMessage = RequiredErrorMessage)]
	[StringLength(PositionNameMaxLength,
		MinimumLength = PositionNameMinLength,
		ErrorMessage = StringLengthErrorMessage)]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = RequiredErrorMessage)]
	public bool IsActive { get; set; } = true;
}
