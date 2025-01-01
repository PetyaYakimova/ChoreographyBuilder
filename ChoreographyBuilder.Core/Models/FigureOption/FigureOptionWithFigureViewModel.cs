namespace ChoreographyBuilder.Core.Models.FigureOption
{
    /// <summary>
    /// View model for previewing figure option when selecting one for verse choreography.
    /// No added validation attributes.
    /// </summary>
    public class FigureOptionWithFigureViewModel : FigureOptionTableViewModel
    {
        public string FigureName { get; set; } = string.Empty;
    }
}
