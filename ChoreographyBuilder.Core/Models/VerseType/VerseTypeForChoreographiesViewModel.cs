namespace ChoreographyBuilder.Core.Models.VerseType
{
	/// <summary>
	/// View model only for previewing verse types when selecting one for a choreography. No added validations.
	/// </summary>
	public class VerseTypeForChoreographiesViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
	}
}
