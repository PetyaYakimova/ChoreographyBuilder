namespace ChoreographyBuilder.Core.Models.Figure
{
	/// <summary>
	/// View model only for previewing figures in a table. 
	/// No added validation attributes.
	/// </summary>
	public class FigureTableViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public bool IsHighlight { get; set; }

		public bool IsFavourite { get; set; }

		public bool CanBeShared { get; set; }

		public int FigureOptionsCount { get; set; }

		public bool FigureUsedInChoreographies { get; set; }

		public string UserEmailAddress { get; set; } = string.Empty;
	}
}
