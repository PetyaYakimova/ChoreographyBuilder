using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;

namespace ChoreographyBuilder.Core.Models.FullChoreography
{
	/// <summary>
	/// View model only for previewing details of full choreographies. No added validations.
	/// </summary>
	public class FullChoreographyDetailsViewModel : FullChoreographyTableViewModel
	{
		public IList<FullChoreographyVerseChoreographyViewModel> Verses { get; set; } = new List<FullChoreographyVerseChoreographyViewModel>();
	}
}
