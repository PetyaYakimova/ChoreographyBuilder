using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class VerseTypeConfiguration : IEntityTypeConfiguration<VerseType>
	{
		private bool seedData;

		public VerseTypeConfiguration(bool seedData = true) : base()
		{
			this.seedData = seedData;
		}

		public void Configure(EntityTypeBuilder<VerseType> builder)
		{
			if (seedData)
			{
				var data = new SeedData();

				builder.HasData(new VerseType[] { data.SwingVerse, data.BluesVerse });
			}
		}
	}
}
