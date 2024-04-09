using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.FullChoreography
{
	/// <summary>
	/// View model for previewing full choreography in a table and when getting it for delete view. 
	/// No added validation attributes.
	/// </summary>
	public class FullChoreographyTableViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		[Display(Name = "Number of verse choreographies:")]
		public int NumberOfVerses { get; set; }
	}
}
