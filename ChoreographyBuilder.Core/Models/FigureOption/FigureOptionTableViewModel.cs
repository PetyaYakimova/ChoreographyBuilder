namespace ChoreographyBuilder.Core.Models.FigureOption
{
	/// <summary>
	/// View model for previewing figure option. No added validations.
	/// </summary>
	public class FigureOptionTableViewModel
	{
		public int Id { get; set; }

		public string StartPositionName { get; set; } = string.Empty;

		public string EndPositionName { get; set; } = string.Empty;

		public int BeatCounts { get; set; }

		public string DynamicsTypeName { get; set; } = string.Empty;

		public bool UsedInChoreographies { get; set; }
	}
}
