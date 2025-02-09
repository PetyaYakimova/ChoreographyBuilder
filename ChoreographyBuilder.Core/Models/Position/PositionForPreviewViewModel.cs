namespace ChoreographyBuilder.Core.Models.Position;

/// <summary>
/// View model only for previewing positions when selecting one for a figure or verse choreography or when trying to delete a position. 
/// No added validation attributes.
/// </summary>
public class PositionForPreviewViewModel
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
}
