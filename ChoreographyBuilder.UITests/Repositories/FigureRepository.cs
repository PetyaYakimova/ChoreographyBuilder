using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Setup;

namespace ChoreographyBuilder.UITests.Repositories;

public class FigureRepository : BaseRepository
{
    public FigureRepository(AppSettings settings) : base(settings)
    {
    }

    public Figure? GetFigureByName(string name)
    {
        UpdateContext();
        return context.Figures.FirstOrDefault(f => f.Name == name);
    }
}
