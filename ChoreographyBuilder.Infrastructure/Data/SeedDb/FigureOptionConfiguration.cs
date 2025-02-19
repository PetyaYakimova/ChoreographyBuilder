using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class FigureOptionConfiguration : IEntityTypeConfiguration<FigureOption>
{
	private bool seedData;

	public FigureOptionConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<FigureOption> builder)
	{
		builder
			.HasOne(fo => fo.StartPosition)
			.WithMany(p => p.FiguresWithStartPosition)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.HasOne(fo => fo.EndPosition)
			.WithMany(p => p.FiguresWithEndPosition)
			.OnDelete(DeleteBehavior.Restrict);

		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new FigureOption[] { data.ChangeOfPlaceOption1, data.ChangeOfPlaceOption2,
				data.AmericanSpinOption1, data.AmericanSpinOption2, data.AmericanSpinOption3, data.AmericanSpinOption4, data.AmericanSpinOption5,
				data.AmericanSpinOption6, data.AmericanSpinOption7, data.AmericanSpinOption8, data.AmericanSpinOption9, data.AmericanSpinOption10,
				data.SpinWithBlockOption1, data.SpinWithBlockOption2, data.SpinWithBlockOption3, data.SpinWithBlockOption4,
				data.TunnelOption1, data.TunnelOption2,
				data.CartwheelOption1,
				data.SendInOption1, data.SendInOption2,
				data.SwingOutOption1, data.SwingOutOption2, data.SwingOutOption3, data.SwingOutOption4,
				data.HelicopterOption1,
				data.LeftSidePassOption1,
				data.SendOutOption1, data.SendOutOption2});
		}
	}
}
