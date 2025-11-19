using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Setup;

namespace ChoreographyBuilder.UITests.Repositories;

public class FigureOptionRepository : BaseRepository
{
    public FigureOptionRepository(AppSettings settings) : base(settings)
    {
    }

    public FigureOption? GetFigureOptionByFigureNameAndBeatsCount(string figureName, int beatsCount)
    {
        UpdateContext();
        return context.FigureOptions.Where(fo => fo.Figure.Name == figureName).FirstOrDefault(fo => fo.BeatCounts == beatsCount);
    }
}
