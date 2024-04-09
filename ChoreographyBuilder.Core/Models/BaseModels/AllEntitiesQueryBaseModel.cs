using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Models.BaseModels
{
	/// <summary>
	/// A base class for all models used to get filtering criteria and pagination info.
	/// No added validation attributes.
	/// </summary>
	/// <typeparam name="Т">Type of model entities</typeparam>
	public abstract class AllEntitiesQueryBaseModel<Т>
	{
		[Display(Name = "Search by name")]
		public string? SearchTerm { get; init; }

		public int ItemsPerPage { get; init; } = DefaultNumberOfItemsPerPage;

		public int CurrentPage { get; init; } = 1;

		public int TotalItemCount { get; set; }

		public IEnumerable<Т> Entities { get; set; } = new List<Т>();
	}
}
