using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.Position
{
	public class AllPositionsQueryModel
	{
		[Display(Name = "Search by name")]
		public string SearchItem { get; init; } = null!;

		public int ItemsPerPage { get; init; } = 10;

		public int CurrentPage { get; init; } = 1;

		public int TotalItemCount { get; set; }

		public IEnumerable<PositionTableViewModel> Positions { get; set; } = new List<PositionTableViewModel>();
	}
}
