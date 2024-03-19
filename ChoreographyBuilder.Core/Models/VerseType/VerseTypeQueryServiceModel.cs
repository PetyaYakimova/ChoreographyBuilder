namespace ChoreographyBuilder.Core.Models.VerseType
{
	public class VerseTypeQueryServiceModel
	{
		public int TotalCount { get; set; }

		public IEnumerable<VerseTypeTableViewModel> VerseTypes { get; set; } = new List<VerseTypeTableViewModel>();
	}
}
