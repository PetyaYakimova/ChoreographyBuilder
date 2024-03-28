using ChoreographyBuilder.Core.Models.VerseChoreography;

namespace ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography
{
	/// <summary>
	/// View model only for previewing verse choreografies in full choreographies. No added validations.
	/// </summary>
	public class FullChoreographyVerseChoreographyViewModel
	{
		public int Id { get; set; }

		public int VerseChoreographyOrder { get; set; }

		public VerseChoreographyTableViewModel VerseChoreography { get; set; } = null!;
	}
}
