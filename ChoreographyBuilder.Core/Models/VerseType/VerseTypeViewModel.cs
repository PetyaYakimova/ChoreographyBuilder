using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChoreographyBuilder.Core.Models.VerseType
{
	/// <summary>
	/// View model only for previewing verse types. No added validations.
	/// </summary>
	public class VerseTypeViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public int BeatCounts { get; set; }

		public bool IsActive { get; set; }
	}
}
