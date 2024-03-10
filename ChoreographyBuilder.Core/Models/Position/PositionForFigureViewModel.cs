namespace ChoreographyBuilder.Core.Models.Position
{
	/// <summary>
	/// View model only for previewing positions when selecting one for a figure. No added validations.
	/// </summary>
	public class PositionForFigureViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
	}
}
