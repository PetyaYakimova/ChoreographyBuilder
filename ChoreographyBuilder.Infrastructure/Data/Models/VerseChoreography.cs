using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Infrastructure.Data.Models
{
	[Comment("Verse Choreographies")]
	public class VerseChoreography
	{
		[Key]
		[Comment("Verse Choreography Identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(VerseChoreographyNameMaxLength)]
		[Comment("Verse Choreography Name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[Comment("Verse Type Identifier")]
		public int VerseTypeId { get; set; }

		[ForeignKey(nameof(VerseTypeId))]
		public VerseType VerseType { get; set; } = null!;

		[Required]
		[Comment("Verse Choreography Score at the time of saving it")]
		public int Score { get; set; }

		[Required]
		[Comment("User Identifier")]
		public string UserId { get; set; } = string.Empty;

		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; } = null!;

		public IEnumerable<VerseChoreographyFigure> Figures { get; set; } = new List<VerseChoreographyFigure>();

		public IEnumerable<FullChoreographyVerseChoreography> FullChoreographies { get; set; } = new List<FullChoreographyVerseChoreography>();
	}
}
