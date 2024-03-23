namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// A view model for previewing all sugested verse choreographies. No added validations.
	/// </summary>
	public class AllSuggestedVerseChoreographiesViewModel : VerseChoreographyGenerateModel
	{
		public IEnumerable<SuggestedVerseChoreographyModel> Choreographies { get; init; } = new List<SuggestedVerseChoreographyModel>();
	}
}
