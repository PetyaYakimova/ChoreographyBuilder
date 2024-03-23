namespace ChoreographyBuilder.Core.Models.Position
{
	/// <summary>
	/// View model only for previewing positions when selecting one for a figure or verse choreography. No added validations.
	/// </summary>
	public class PositionForSelectionViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
	}
}
