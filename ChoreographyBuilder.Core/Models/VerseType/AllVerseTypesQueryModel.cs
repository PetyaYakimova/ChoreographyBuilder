using ChoreographyBuilder.Core.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.VerseType
{
	public class AllVerseTypesQueryModel : AllEntitiesQueryBaseModel<VerseTypeTableViewModel>
	{
		[Display(Name = "Search by beats")]
		public int? SearchBeats { get; init; }
	}
}
