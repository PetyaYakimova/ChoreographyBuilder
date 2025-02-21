using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class VerseChoreographyConfiguration : IEntityTypeConfiguration<VerseChoreography>
{
	private bool seedData;

	public VerseChoreographyConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<VerseChoreography> builder)
	{
		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new VerseChoreography[] { data.SwingVerseChoreography1, data.SwingVerseChoreography2, data.BluesVerseChoreography });
		}
	}
}
