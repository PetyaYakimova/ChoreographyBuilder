using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Setup;

namespace ChoreographyBuilder.UITests.Repositories;

public class PositionRepository : BaseRepository
{
    public PositionRepository(AppSettings settings) : base(settings)
    {
    }

    public Position? GetPositionByName(string name)
    {
        UpdateContext();
        return context.Positions.FirstOrDefault(p => p.Name == name);
    }
}
