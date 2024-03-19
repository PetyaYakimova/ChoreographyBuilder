using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Models.VerseType
{
	public class AllVerseTypesQueryModel
	{
		[Display(Name = "Search by name")]
		public string SearchTerm { get; init; } = null!;

		[Display(Name = "Search by beats")]
		public int? SearchBeats { get; init; }

		public int ItemsPerPage { get; init; } = DefaultNumberOfItemsPerPage;

		public int CurrentPage { get; init; } = 1;

		public int TotalItemCount { get; set; }

		public IEnumerable<VerseTypeTableViewModel> VerseTypes { get; set; } = new List<VerseTypeTableViewModel>();
	}
}
