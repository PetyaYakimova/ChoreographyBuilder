namespace ChoreographyBuilder.Core.Models.FigureOption;

/// <summary>
/// View model for previewing figure option when trying to delete it. 
/// No added validation attributes.
/// </summary>
public class FigureOptionDeleteViewModel
{
	public int Id { get; set; }

	public string FigureName { get; set; } = string.Empty;

	public int FigureId { get; set; }
}
