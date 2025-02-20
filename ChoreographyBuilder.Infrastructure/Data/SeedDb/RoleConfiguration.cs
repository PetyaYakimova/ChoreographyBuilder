using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
	private bool seedData;

	public RoleConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new IdentityRole[] { data.AdminRole, data.UserRole });
		}
	}
}
