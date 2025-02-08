using ChoreographyBuilder.Core.Models.VerseChoreography;

namespace ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;

/// <summary>
/// View model only for previewing verse choreographies in full choreographies. 
/// No added validation attributes.
/// </summary>
public class FullChoreographyVerseChoreographyViewModel
{
	public int Id { get; set; }

	public int VerseChoreographyOrder { get; set; }

	public VerseChoreographyTableViewModel VerseChoreography { get; set; } = null!;
}
