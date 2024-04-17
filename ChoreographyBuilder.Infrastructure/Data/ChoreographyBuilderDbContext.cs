using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.SeedDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Infrastructure.Data
{
	public class ChoreographyBuilderDbContext : IdentityDbContext
	{
		private bool seedData;

		public ChoreographyBuilderDbContext(
			DbContextOptions<ChoreographyBuilderDbContext> options,
			bool seedData = true)
			: base(options)
		{
			if (Database.IsRelational())
			{
				Database.Migrate();
			}
			else
			{
				Database.EnsureCreated();
			}

			this.seedData = seedData;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new RoleConfiguration(seedData));
			builder.ApplyConfiguration(new UserConfiguration(seedData));
			builder.ApplyConfiguration(new UserInRoleConfiguration(seedData));
			builder.ApplyConfiguration(new PositionConfiguration(seedData));
			builder.ApplyConfiguration(new VerseTypeConfiguration(seedData));
			builder.ApplyConfiguration(new FigureConfiguration(seedData));
			builder.ApplyConfiguration(new FigureOptionConfiguration(seedData));
			builder.ApplyConfiguration(new VerseChoreographyConfiguration(seedData));
			builder.ApplyConfiguration(new VerseChoreographyFigureConfiguration(seedData));
			builder.ApplyConfiguration(new FullChoreographyConfiguration(seedData));
			builder.ApplyConfiguration(new FullChoreographyVerseChoreographyConfiguration(seedData));

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