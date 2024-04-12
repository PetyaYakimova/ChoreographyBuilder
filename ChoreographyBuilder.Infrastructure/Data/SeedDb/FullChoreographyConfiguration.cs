using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class FullChoreographyConfiguration : IEntityTypeConfiguration<FullChoreography>
	{
		private bool seedData;

		public FullChoreographyConfiguration(bool seedData = true) : base()
		{
			this.seedData = seedData;
		}

		public void Configure(EntityTypeBuilder<FullChoreography> builder)
		{
			if (seedData)
			{
				var data = new SeedData();

				builder.HasData(new FullChoreography[] { data.FullChoreography });
			}
		}
	}
}
