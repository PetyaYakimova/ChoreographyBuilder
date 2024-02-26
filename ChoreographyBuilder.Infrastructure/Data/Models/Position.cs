using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ChoreographyBuilder.Infrastructure.Constants.DataConstants;

namespace ChoreographyBuilder.Infrastructure.Data.Models
{
	[Comment("Positions")]
	public class Position
	{
		[Key]
		[Comment("Position Identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(PositionNameMaxLenght)]
		[Comment("Position Name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[Comment("Position Is Active")]
		public bool IsActive { get; set; }
	}
}
