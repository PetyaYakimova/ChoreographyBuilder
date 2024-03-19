using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Models.BaseModels
{
	public abstract class AllEntitiesQueryBaseModel<Т>
	{
		[Display(Name = "Search by name")]
		public string SearchTerm { get; init; } = null!;

		public int ItemsPerPage { get; init; } = DefaultNumberOfItemsPerPage;

		public int CurrentPage { get; init; } = 1;

		public int TotalItemCount { get; set; }

		public IEnumerable<Т> Entities { get; set; } = new List<Т>();
	}
}
