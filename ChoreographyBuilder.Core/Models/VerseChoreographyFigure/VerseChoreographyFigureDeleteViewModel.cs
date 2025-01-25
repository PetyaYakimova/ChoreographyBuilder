namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure
{
    /// <summary>
	/// View model for previewing figure in verse choreography when trying to delete it. 
	/// No added validation attributes.
	/// </summary>
    public class VerseChoreographyFigureDeleteViewModel
    {
        public int Id { get; set; }

        public string FigureName { get; set; } = string.Empty;

        public string VerseChoreographyName { get; set; } = string.Empty;

        public int VerseChoreographyId { get; set; }
    }
}
