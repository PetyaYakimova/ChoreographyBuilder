using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure
{
	/// <summary>
	/// View model only for previewing the figures in a verse choreography 
	/// Added validation attribute only for the figure order property.
	/// </summary>
	public class VerseChoreographyFigureViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Display(Name = "Figure order in choreography:")]
		public int FigureOrder { get; set; }

		public int FigureOptionId { get; set; }

		[Display(Name = "Figure name:")]
		public string FigureName { get; init; } = string.Empty;

		[Display(Name = "Is favourite:")]
		public bool IsFavourite { get; init; }

		[Display(Name = "Is highlight:")]
		public bool IsHighlight { get; init; }

		[Display(Name = "Start position:")]
		public string StartPosition { get; init; } = string.Empty;

		[Display(Name = "End position:")]
		public string EndPosition { get; init; } = string.Empty;

		[Display(Name = "Beats count:")]
		public int BeatsCount { get; init; }

		[Display(Name = "Dynamics type:")]
		public string DynamicsType { get; init; } = string.Empty;
	}
}
