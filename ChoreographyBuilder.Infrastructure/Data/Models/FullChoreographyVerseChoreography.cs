using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoreographyBuilder.Infrastructure.Data.Models;

[Comment("Full Choreography Verse Choreographies")]
public class FullChoreographyVerseChoreography
{
	[Key]
	[Comment("Full Choreography Verse Choreography Identifier")]
	public int Id { get; set; }

	[Required]
	[Comment("Full Choreography Identifier")]
	public int FullChoreographyId { get; set; }

	[ForeignKey(nameof(FullChoreographyId))]
	public FullChoreography FullChoreography { get; set; } = null!;

	[Required]
	[Comment("Verse Choreography Identifier")]
	public int VerseChoreographyId { get; set; }

	[ForeignKey(nameof(VerseChoreographyId))]
	public VerseChoreography VerseChoreography { get; set;} = null!;

	[Required]
	[Comment("Verse Choreography Order in which it appears in this choreography")]
	public int VerseChoreographyOrder { get; set; }
}
