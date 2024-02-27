using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class VerseTypeConfiguration : IEntityTypeConfiguration<VerseType>
	{
		public void Configure(EntityTypeBuilder<VerseType> builder)
		{
			var data = new SeedData();

			builder.HasData(new VerseType[] { data.SwingVerse, data.BluesVerse });
		}
	}
}
