namespace ChoreographyBuilder.Core.Models.Figure;

/// <summary>
	/// View model only for previewing figures when filtering verse choreographies and when trying to delete figure. 
/// No added validation attributes.
	/// </summary>
public class FigureForPreviewViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
