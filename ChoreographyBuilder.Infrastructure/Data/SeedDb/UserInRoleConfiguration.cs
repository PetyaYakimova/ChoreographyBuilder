using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb;

internal class UserInRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	private bool seedData;

	public UserInRoleConfiguration(bool seedData = true) : base()
	{
		this.seedData = seedData;
	}

	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{
		if (seedData)
		{
			var data = new SeedData();

			builder.HasData(new IdentityUserRole<string>[] { data.DemoUserWitRole, data.AdminUserWitRole });
		}
	}
}
