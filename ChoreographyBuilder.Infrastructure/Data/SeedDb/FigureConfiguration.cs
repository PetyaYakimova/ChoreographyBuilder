using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class FigureConfiguration : IEntityTypeConfiguration<Figure>
{
	private bool seedData;

	public FigureConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<Figure> builder)
	{
		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new Figure[] { data.ChangeOfPlace, data.AmericanSpin, data.SpinWithBlock, 
				data.Tunnel, data.Cartwheel, data.SendIn, data.SwingOut, data.Helicopter, 
				data.LeftSidePass, data.SendOut });
		}
	}
}
