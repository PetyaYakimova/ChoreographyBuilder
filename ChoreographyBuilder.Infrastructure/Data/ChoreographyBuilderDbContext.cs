using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Infrastructure.Data
{
	public class ChoreographyBuilderDbContext : IdentityDbContext
	{
		public ChoreographyBuilderDbContext(DbContextOptions<ChoreographyBuilderDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<FigureOptions>()
				.HasOne(fo => fo.StartPosition)
				.WithMany(p => p.FiguresWithStartPosition)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<FigureOptions>()
				.HasOne(fo => fo.EndPosition)
				.WithMany(p => p.FiguresWithEndPosition)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<FullChoreographyVerseChoreography>()
				.HasOne(fcvc => fcvc.VerseChoreography)
				.WithMany(vc => vc.FullChoreographies)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<VerseChoreographyFigure>()
				.HasOne(vcf => vcf.VerseChoreography)
				.WithMany(vc => vc.Figures)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}

		public DbSet<VerseType> VerseTypes { get; set; } = null!;

		public DbSet<Position> Positions { get; set; } = null!;

		public DbSet<Figure> Figures { get; set; } = null!;

		public DbSet<FigureOptions> FigureOptions { get; set; } = null!;

		public DbSet<VerseChoreography> VerseChoreographies { get; set; } = null!;

		public DbSet<VerseChoreographyFigure> VerseChoreographiesFigures { get; set; } = null!;

		public DbSet<FullChoreography> FullChoreographies { get; set; } = null!;

		public DbSet<FullChoreographyVerseChoreography> FullChoreographiesVerseChoreographies { get; set; } = null!;
	}
}