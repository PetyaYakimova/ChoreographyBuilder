using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
    /// <summary>
	/// View model only for previewing saved verse choreographies in a table and when selecting one for a full choreography. 
	/// No added validation attributes.
	/// </summary>
	public class VerseChoreographyTableViewModel
	{
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

		[Display(Name = "Verse type:")]
		public string VerseTypeName { get; set; } = string.Empty;

		[Display(Name = "Start position:")]
		public string StartPositionName { get; set; } = string.Empty;

		[Display(Name = "End position:")]
		public string EndPositionName { get; set; } = string.Empty;

		[Display(Name = "Total number of figures:")]
		public int NumberOfFigures { get; set; }

		[Display(Name = "Final figure:")]
		public string FinalFigureName { get; set; } = string.Empty;

        public bool UsedInFullChoreographies { get; set; }
    }
}
