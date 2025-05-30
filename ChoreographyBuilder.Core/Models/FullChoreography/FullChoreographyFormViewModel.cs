﻿using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.FullChoreography;

/// <summary>
/// A model for adding and editing a full choreography. 
/// Added validation attributes.
/// </summary>
public class FullChoreographyFormViewModel
{
	[Required(ErrorMessage = RequiredErrorMessage)]
	[StringLength(FullChoreographyNameMaxLength,
		MinimumLength = FullChoreographyNameMinLength,
		ErrorMessage = StringLengthErrorMessage)]
	public string Name { get; set; } = string.Empty;
}
