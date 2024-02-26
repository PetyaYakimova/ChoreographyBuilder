using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Infrastructure.Data.Models
{
	[Comment("Full Choreographies")]
	public class FullChoreography
	{
		[Key]
		[Comment("Full Choreography Identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(FullChoreographyNameMaxLenght)]
		[Comment("Full Choreography Name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[Comment("User Identifier")]
		public string UserId { get; set; } = string.Empty;

		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; } = null!;

		public IEnumerable<FullChoreographyVerseChoreography> VerseChoreographies { get; set; } = new List<FullChoreographyVerseChoreography>();
	}
}
