using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class VerseChoreographyConfiguration : IEntityTypeConfiguration<VerseChoreography>
	{
		public void Configure(EntityTypeBuilder<VerseChoreography> builder)
		{
			var data = new SeedData();

			builder.HasData(new VerseChoreography[] { data.SwingVerseChoreography1, data.SwingVerseChoreography2, data.BluesVerseChoreography });
		}
	}
}
