using ChoreographyBuilder.Core.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.VerseType
{
	/// <summary>
	/// A model used for getting criteria for filtering verse types and pagination info.
	/// No added validation attributes.
	/// </summary>
	public class AllVerseTypesQueryModel : AllEntitiesQueryBaseModel<VerseTypeTableViewModel>
	{
		[Display(Name = "Search by beats")]
		public int? SearchBeats { get; init; }
	}
}
