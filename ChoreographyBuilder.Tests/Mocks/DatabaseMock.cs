using ChoreographyBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Tests.Mocks;

public static class DatabaseMock
{
	public static ChoreographyBuilderDbContext Instance
	{
		get
		{
			var dbContextOptions = new DbContextOptionsBuilder<ChoreographyBuilderDbContext>()
				.UseInMemoryDatabase("ChoreographyBuilderInMemoryDb" + DateTime.Now.Ticks.ToString())
				.Options;

			return new ChoreographyBuilderDbContext(dbContextOptions, false);
		}
	}
}
