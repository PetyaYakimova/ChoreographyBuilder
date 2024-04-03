using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.SeedDb;
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
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new UserInRoleConfiguration());
			builder.ApplyConfiguration(new PositionConfiguration());
			builder.ApplyConfiguration(new VerseTypeConfiguration());
			builder.ApplyConfiguration(new FigureConfiguration());
			builder.ApplyConfiguration(new FigureOptionConfiguration());
			builder.ApplyConfiguration(new VerseChoreographyConfiguration());
			builder.ApplyConfiguration(new VerseChoreographyFigureConfiguration());
			builder.ApplyConfiguration(new FullChoreographyConfiguration());
			builder.ApplyConfiguration(new FullChoreographyVerseChoreographyConfiguration());

			base.OnModelCreating(builder);
		}

		public DbSet<VerseType> VerseTypes { get; set; } = null!;

		public DbSet<Position> Positions { get; set; } = null!;

		public DbSet<Figure> Figures { get; set; } = null!;

		public DbSet<FigureOption> FigureOptions { get; set; } = null!;

		public DbSet<VerseChoreography> VerseChoreographies { get; set; } = null!;

		public DbSet<VerseChoreographyFigure> VerseChoreographiesFigures { get; set; } = null!;

		public DbSet<FullChoreography> FullChoreographies { get; set; } = null!;

		public DbSet<FullChoreographyVerseChoreography> FullChoreographiesVerseChoreographies { get; set; } = null!;
	}
}