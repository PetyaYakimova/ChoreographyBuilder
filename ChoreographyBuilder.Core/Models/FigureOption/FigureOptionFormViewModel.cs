using ChoreographyBuilder.Core.Attributes;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Core.Constants.MessageConstants;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Core.Models.FigureOption
{
	/// <summary>
	/// View model for creating and editing option for figure. Added validations.
	/// </summary>
	public class FigureOptionFormViewModel
	{
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int FigureId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Figure Name")]
        public string FigureName { get; set;} = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Start Position")]
		public int StartPositionId { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("End Position")]
		public int EndPositionId { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(FigureOptionBeatsCountMin,
			FigureOptionBeatsCountMax,
			ErrorMessage = NumberMustBeInRangeErrorMessage)]
		[IsEven(ErrorMessage = NumberMustBeEvenErrorMessage)]
		[Display(Name = "Beats Count")]
		public int BeatCounts { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[DisplayName("Dynamics Type")]
		public DynamicsType DynamicsType { get; set; }

		public IEnumerable<PositionForPreviewViewModel> StartPositions { get; set; } = new List<PositionForPreviewViewModel>();

		public IEnumerable<PositionForPreviewViewModel> EndPositions { get; set; } = new List<PositionForPreviewViewModel>();

		public IEnumerable<DynamicsType> DynamicsTypes { get; set; } = new List<DynamicsType>();
	}
}
