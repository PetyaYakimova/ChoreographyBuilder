namespace ChoreographyBuilder.Core.Models.Position;

/// <summary>
/// View model only for previewing positions in a table. 
/// No added validation attributes.
/// </summary>
public class PositionTableViewModel
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public bool IsActive { get; set; }

	public bool HasFigures { get; set; }
}
