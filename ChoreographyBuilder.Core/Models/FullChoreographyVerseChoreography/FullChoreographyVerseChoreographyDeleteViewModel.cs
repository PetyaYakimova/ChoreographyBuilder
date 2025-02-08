namespace ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;

/// <summary>
/// View model for previewing verse choreography in full choreography when trying to delete it. 
/// No added validation attributes.
/// </summary>
public class FullChoreographyVerseChoreographyDeleteViewModel
{
	public int Id { get; set; }

	public string VerseChoreographyName { get; set; } = string.Empty;

	public string FullChoreographyName { get; set; } = string.Empty;

	public int FullChoreographyId { get; set; }
}
