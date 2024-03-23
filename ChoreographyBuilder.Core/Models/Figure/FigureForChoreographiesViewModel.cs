namespace ChoreographyBuilder.Core.Models.Figure
{
    /// <summary>
	/// View model only for previewing figures when filtering verse choreographies. No added validations.
	/// </summary>
    public class FigureForChoreographiesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
