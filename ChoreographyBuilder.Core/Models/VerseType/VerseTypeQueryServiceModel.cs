using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.VerseType
{
	public class VerseTypeQueryServiceModel : EntityQueryBaseModel
	{
		public IEnumerable<VerseTypeTableViewModel> VerseTypes { get; set; } = new List<VerseTypeTableViewModel>();
	}
}
