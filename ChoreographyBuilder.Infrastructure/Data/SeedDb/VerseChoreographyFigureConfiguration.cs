using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class VerseChoreographyFigureConfiguration : IEntityTypeConfiguration<VerseChoreographyFigure>
{
	private bool seedData;

	public VerseChoreographyFigureConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<VerseChoreographyFigure> builder)
	{
		builder
			.HasOne(vcf => vcf.VerseChoreography)
			.WithMany(vc => vc.Figures)
			.OnDelete(DeleteBehavior.Restrict);

		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new VerseChoreographyFigure[] { data.SwingVerse1Figure1, data.SwingVerse1Figure2, data.SwingVerse1Figure3, data.SwingVerse1Figure4,
				data.SwingVerse2Figure1, data.SwingVerse2Figure2, data.SwingVerse2Figure3,
				data.BluesVerseFigure1, data.BluesVerseFigure2, data.BluesVerseFigure3, data.BluesVerseFigure4, data.BluesVerseFigure5, data.BluesVerseFigure6});
		}
	}
}
