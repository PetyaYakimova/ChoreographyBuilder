using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class PositionConfiguration : IEntityTypeConfiguration<Position>
	{
		public void Configure(EntityTypeBuilder<Position> builder)
		{
			var data = new SeedData();

			builder.HasData(new Position[] { data.OpenPositionLeftShoulderToTheAudience, data.OpenPositionRightShoulderToTheAudience, data.ClosedPositionLeftShoulderToTheAudience, data.ClosedPositionRightShoulderToTheAudience });
		}
	}
}
