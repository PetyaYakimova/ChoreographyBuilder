using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Infrastructure.Data.Models;

[Comment("Verse Types")]
public class VerseType
{
	[Key]
	[Comment("Verse Identifier")]
	public int Id { get; set; }

	[Required]
	[MaxLength(VerseTypeNameMaxLength)]
	[Comment("Verse Name")]
	public string Name { get; set; } = string.Empty;

	[Required]
	[Comment("Verse Beat Counts")]
	public int BeatCounts { get; set; }

	[Required]
	[Comment("Verse Is Active")]
	public bool IsActive { get; set; }

	public IEnumerable<VerseChoreography> VerseChoreographies { get; set; } = new List<VerseChoreography>();
}
