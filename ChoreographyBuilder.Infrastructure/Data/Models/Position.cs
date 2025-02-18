using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Infrastructure.Data.Models;

[Comment("Positions")]
public class Position
{
	[Key]
	[Comment("Position Identifier")]
	public int Id { get; set; }

	[Required]
	[MaxLength(PositionNameMaxLength)]
	[Comment("Position Name")]
	public string Name { get; set; } = string.Empty;

	[Required]
	[Comment("Position Is Active")]
	public bool IsActive { get; set; }

	public IEnumerable<FigureOption> FiguresWithStartPosition { get; set; } = new List<FigureOption>();

	public IEnumerable<FigureOption> FiguresWithEndPosition { get; set; } = new List<FigureOption>();
}
