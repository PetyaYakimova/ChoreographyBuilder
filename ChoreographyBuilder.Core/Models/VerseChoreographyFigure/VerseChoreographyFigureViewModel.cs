namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure
{
	/// <summary>
	/// View model only for previewing the figures in a verse choreography. 
	/// No added validation attributes.
	/// </summary>
	public class VerseChoreographyFigureViewModel
	{
		public int FigureOrder { get; set; }

		public int FigureOptionId { get; set; }

		public string FigureName { get; init; } = string.Empty;

		public bool IsFavourite { get; init; }

		public bool IsHighlight { get; init; }

		public string StartPosition { get; init; } = string.Empty;

		public string EndPosition { get; init; } = string.Empty;

		public int BeatsCount { get; init; }

		public string DynamicsType { get; init; } = string.Empty;
	}
}
