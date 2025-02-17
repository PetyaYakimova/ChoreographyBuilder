using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Infrastructure.Data.Models;

[Comment("Figures")]
public class Figure
{
	[Key]
	[Comment("Figure Identifier")]
	public int Id { get; set; }

	[Required]
	[MaxLength(FigureNameMaxLength)]
	[Comment("Figure Name")]
	public string Name { get; set; } = string.Empty;

	[Required]
	[Comment("Figure Is Highlight")]
	public bool IsHighlight { get; set; }

	[Required]
	[Comment("Figure Is Favourite")]
	public bool IsFavourite { get; set; }

	[Required]
	[Comment("User Identifier")]
	public string UserId { get; set; } = string.Empty;

	[ForeignKey(nameof(UserId))]
	public IdentityUser User { get; set; } = null!;

	[Required]
	[Comment("Figure Can Be Shared With Other Users")]
	public bool CanBeShared { get; set; }

	public IEnumerable<FigureOption> FigureOptions { get; set; } = new List<FigureOption>();
}
