﻿using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Attributes
{
	public class IsEven : ValidationAttribute
	{
		public IsEven()
		{
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null && (int)value % 2 != 0)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}
	}
}