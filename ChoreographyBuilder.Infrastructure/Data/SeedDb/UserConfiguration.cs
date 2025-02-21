using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
	private bool seedData;

	public UserConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<IdentityUser> builder)
	{
		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new IdentityUser[] { data.DemoUser, data.AdminUser });
		}
	}
}
