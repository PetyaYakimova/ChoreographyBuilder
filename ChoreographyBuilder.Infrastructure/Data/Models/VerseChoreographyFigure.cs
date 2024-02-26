using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoreographyBuilder.Infrastructure.Data.Models
{
	[Comment("Verse Choreography Figures")]
	public class VerseChoreographyFigure
	{
		[Key]
		[Comment("Verse Choreograhy Figure Identifier")]
		public int Id { get; set; }

		[Required]
		[Comment("Verse Choreography Identifier")]
		public int VerseChoreographyId { get; set; }

		[ForeignKey(nameof(VerseChoreographyId))]
		public VerseChoreography VerseChoreography { get; set; } = null!;

		[Required]
		[Comment("Figure Identifier")]
		public int FigureId { get; set; }

		[ForeignKey(nameof(FigureId))]
		public Figure Figure { get; set; } = null!;

		[Required]
		[Comment("Figure Order in which it appears in this choreography")]
		public int FigureOrder { get; set; }
	}
}
