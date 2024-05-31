namespace ChoreographyBuilder.Core.Models.Figure
{
	/// <summary>
	/// View model only for previewing figures when copying a figure. 
	/// No added validation attributes.
	/// </summary>
	public class FigureForCopyViewModel : FigureForPreviewViewModel
	{
		public int FigureOptionsCount { get; set; }

		public string UserEmailAddress { get; set; } = null!;
	}
}
