using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.Statistics
{
	/// <summary>
	/// A view model only for displaying statistics for users. No added validations.
	/// </summary>
	public class UserStatisticModel
	{
		[Display(Name = "My Figures:")]
		public int MyTotalNumberOfFigures { get; set; }

		[Display(Name = "My Verse Choreographies:")]
		public int MyTotalNumberOfVerseChoreographies { get; set; }

		[Display(Name = "My Full Choreographies:")]
		public int MyTotalNumberOfFullChoreographies { get; set; }
	}
}
