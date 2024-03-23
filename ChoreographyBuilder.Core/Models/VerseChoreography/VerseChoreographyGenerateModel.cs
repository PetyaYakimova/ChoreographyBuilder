using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	public class VerseChoreographyGenerateModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[Display(Name = "Verse type")]
		public int VerseTypeId { get; set; }

		[Display(Name = "Start position (Optional)")]
		public int? StartPositionId { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Display(Name = "Final figure")]
		public int FinalFigureId { get; set; }

		public IEnumerable<VerseTypeForChoreographiesViewModel> VerseTypes { get; set; } = new List<VerseTypeForChoreographiesViewModel>();

		public IEnumerable<PositionForSelectionViewModel> Positions { get; set; } = new List<PositionForSelectionViewModel>();

		public IEnumerable<FigureForChoreographiesViewModel> Figures { get; set; } = new List<FigureForChoreographiesViewModel>();
	}
}
