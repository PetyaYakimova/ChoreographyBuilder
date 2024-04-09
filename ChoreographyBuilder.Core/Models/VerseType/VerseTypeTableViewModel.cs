namespace ChoreographyBuilder.Core.Models.VerseType
{
	/// <summary>
	/// View model only for previewing verse types in a table. 
	/// No added validation attributes.
	/// </summary>
	public class VerseTypeTableViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public int BeatCounts { get; set; }

		public bool IsActive { get; set; }

		public bool HasChoreographies { get; set; }
	}
}
