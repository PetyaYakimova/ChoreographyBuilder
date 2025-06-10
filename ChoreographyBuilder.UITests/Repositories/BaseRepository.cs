using ChoreographyBuilder.Infrastructure.Data;
using ChoreographyBuilder.UITests.Setup;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.UITests.Repositories;

public class BaseRepository
{
    public ChoreographyBuilderDbContext context;
    public DbContextOptionsBuilder<ChoreographyBuilderDbContext> optionsBuilder;

    public BaseRepository(AppSettings settings)
    {
        optionsBuilder = new DbContextOptionsBuilder<ChoreographyBuilderDbContext>();
        optionsBuilder.UseSqlServer(settings.ConnectionStrings.DBConnectionString);
        context = new ChoreographyBuilderDbContext(optionsBuilder.Options);
    }

    public void UpdateContext()
    {
        context = new ChoreographyBuilderDbContext(optionsBuilder.Options);
    }
}
