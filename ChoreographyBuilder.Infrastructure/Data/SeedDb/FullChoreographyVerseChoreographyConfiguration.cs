using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class FullChoreographyVerseChoreographyConfiguration : IEntityTypeConfiguration<FullChoreographyVerseChoreography>
	{
		public void Configure(EntityTypeBuilder<FullChoreographyVerseChoreography> builder)
		{
			builder
				.HasOne(fcvc => fcvc.VerseChoreography)
				.WithMany(vc => vc.FullChoreographies)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
