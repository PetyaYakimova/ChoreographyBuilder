namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// View model only for previewing saved verse choreography when trying to delete it. No added validations.
	/// </summary>
	public class VerseChoreographyDeleteViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public int NumberOfFigures { get; set; }
	}
}
