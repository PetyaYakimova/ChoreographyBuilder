namespace ChoreographyBuilder.Core.Models.Figure
{
	/// <summary>
	/// View model only for previewing figures in a table. No added validations.
	/// </summary>
	public class FigureViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public bool IsHighlight { get; set; }

		public bool IsFavourite { get; set; }

		public string UserId { get; set; } = string.Empty;

		public int FigureOptionsCount { get; set; }
	}
}
