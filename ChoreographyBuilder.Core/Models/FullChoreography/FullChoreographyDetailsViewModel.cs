using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;

namespace ChoreographyBuilder.Core.Models.FullChoreography;

/// <summary>
/// View model only for previewing details of full choreographies. 
/// No added validation attributes.
/// </summary>
public class FullChoreographyDetailsViewModel : FullChoreographyTableViewModel
{
	public IList<FullChoreographyVerseChoreographyViewModel> Verses { get; set; } = new List<FullChoreographyVerseChoreographyViewModel>();
}
