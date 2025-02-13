namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure;

/// <summary>
/// Service model for transferring the data for the selected new figure in the verse choreography.
/// No added validation attributes.
/// </summary>
public class VerseChoreographyFigureSelectedReplacementServiceModel
{
	public int FigureOptionId { get; set; }

	public int FigureOrder { get; set; }
}
