using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;

namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// View model only for previewing details of saved verse choreographies. No added validations.
	/// </summary>
	public class VerseChoreographyDetailsViewModel : VerseChoreographyTableViewModel
	{
		public IEnumerable<VerseChoreographyFigureViewModel> Figures { get; set; } = new List<VerseChoreographyFigureViewModel>();
	}
}
