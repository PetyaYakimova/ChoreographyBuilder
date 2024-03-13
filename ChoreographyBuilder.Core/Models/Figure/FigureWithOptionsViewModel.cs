using ChoreographyBuilder.Core.Models.FigureOption;

namespace ChoreographyBuilder.Core.Models.Figure
{
	/// <summary>
	/// View model for previewing figures with their options. No added validations.
	/// </summary>
	public class FigureWithOptionsViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public bool IsHighlight { get; set; }

		public bool IsFavourite { get; set; }

		public IEnumerable<FigureOptionTableViewModel> Options { get; set; } = new List<FigureOptionTableViewModel>();
	}
}
