using ChoreographyBuilder.Core.Models.FigureOption;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreographyFigure
{
    /// <summary>
	/// View model only for when selecting figure option for verse choreography. 
	/// Added validation attributes.
	/// </summary>
	public class VerseChoreographyFigureOptionFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Figure option:")]
        public int FigureOptionId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Order:")]
        public int FigureOrder { get; set; }

        [Display(Name = "Start position:")]
        public string? StartPositionName { get; set; }

        //TODO: Change it with model that has the figure name in it as well
        public IEnumerable<FigureOptionWithFigureViewModel> Figures { get; set; } = new List<FigureOptionWithFigureViewModel>();
    }
}
