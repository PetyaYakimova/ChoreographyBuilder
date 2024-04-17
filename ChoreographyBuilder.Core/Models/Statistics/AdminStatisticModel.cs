using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.Statistics
{
	/// <summary>
	/// A view model only for displaying statistics to admins. 
	/// No added validation attributes.
	/// </summary>
	public class AdminStatisticModel
	{
		[Display(Name = "All Positions:")]
		public int TotalNumberOfPositions { get; set; }

		[Display(Name = "Active Positions:")]
		public int NumberOfActivePositions { get; set; }

		[Display(Name = "All Verse Types:")]
		public int TotalNumberOfVerseTypes { get; set; }

		[Display(Name = "Active Verse Types:")]
		public int NumberOfActiveVerseTypes { get; set; }

		[Display(Name = "All Figures:")]
		public int TotalNumberOfFigures { get; set; }

		[Display(Name = "Users With Figures:")]
		public int UsersWithAtLeastOneFigure { get; set; }

		[Display(Name = "All Verse Choreographies:")]
		public int TotalNumberOfSavedVerseChoreographies { get; set; }

		[Display(Name = "Users With Verse Choreographies:")]
		public int UsersWithAtLeastOneVerseChoreography { get; set; }

		[Display(Name = "All Full Choreographies:")]
		public int TotalNumberOfSavedFullChoreographies { get; set; }

		[Display(Name = "Users With Full Choreographies:")]
		public int UsersWithAtLeastOneFullChoreography { get; set; }
	}
}
