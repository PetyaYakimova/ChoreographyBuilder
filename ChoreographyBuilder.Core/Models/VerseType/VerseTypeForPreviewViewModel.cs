namespace ChoreographyBuilder.Core.Models.VerseType
{
	/// <summary>
	/// View model only for previewing verse types when selecting one for a choreography, when filling dropdowns and when previewing it before delete. No added validations.
	/// </summary>
	public class VerseTypeForPreviewViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
	}
}
