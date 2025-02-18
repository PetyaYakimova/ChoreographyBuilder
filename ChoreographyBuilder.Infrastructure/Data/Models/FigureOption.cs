using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoreographyBuilder.Infrastructure.Data.Models;

[Comment("Figure Options")]
public class FigureOption
{
	[Key]
	[Comment("Figure Options Identifier")]
	public int Id { get; set; }

	[Required]
	[Comment("Figure Options Figure Identifier")]
	public int FigureId { get; set; }

	[ForeignKey(nameof(FigureId))]
	public Figure Figure { get; set; } = null!;

	[Required]
	[Comment("Figure Options Start Position Identifier")]
	public int StartPositionId { get; set; }

	[ForeignKey(nameof(StartPositionId))]
	public Position StartPosition { get; set; } = null!;

	[Required]
	[Comment("Figure Options End Position Identifier")]
	public int EndPositionId { get; set; }

	[ForeignKey(nameof(EndPositionId))]
	public Position EndPosition { get; set; } = null!;

	[Required]
	[Comment("Figure Option Beat Counts")]
	public int BeatCounts { get; set; }

	[Required]
	public DynamicsType DynamicsType { get; set; }

	public IEnumerable<VerseChoreographyFigure> VerseChoreographyFigures { get; set; } = new List<VerseChoreographyFigure>();
}
