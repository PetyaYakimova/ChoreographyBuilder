namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure
{
	/// <summary>
	/// View model only for previewing the figures in a verse choreography. No added validations.
	/// </summary>
	public class VerseChoreographyFigureViewModel
	{
		public int FigureOrder { get; set; }

		public int FigureOptionId { get; set; }

		public string FigureName { get; set; } = string.Empty;

		public bool IsFavourite { get; set; }

		public bool IsHighlight { get; set; }

		public string StartPostion { get; set; } = string.Empty;

		public string EndPosition { get; set; } = string.Empty;

		public int BeatsCount { get; set; }
	}
}
