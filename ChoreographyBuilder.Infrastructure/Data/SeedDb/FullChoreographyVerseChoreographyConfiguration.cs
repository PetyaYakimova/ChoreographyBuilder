using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

	internal class FullChoreographyVerseChoreographyConfiguration : IEntityTypeConfiguration<FullChoreographyVerseChoreography>
{
		private bool seedData;

		public FullChoreographyVerseChoreographyConfiguration(bool seedData = true) : base()
		{
			this.seedData = seedData;
		}

		public void Configure(EntityTypeBuilder<FullChoreographyVerseChoreography> builder)
    {
        builder
            .HasOne(fcvc => fcvc.VerseChoreography)
            .WithMany(vc => vc.FullChoreographies)
            .OnDelete(DeleteBehavior.Restrict);

        if (seedData)
        {
            var data = new SeedData();

            builder.HasData(new FullChoreographyVerseChoreography[] { data.FullChoreographyVerse1, data.FullChoreographyVerse2, data.FullChoreographyVerse3 });
        }
    }
}
