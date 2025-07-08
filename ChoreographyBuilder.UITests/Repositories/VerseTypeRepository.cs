using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Setup;

namespace ChoreographyBuilder.UITests.Repositories;

public class VerseTypeRepository : BaseRepository
{
    public VerseTypeRepository(AppSettings settings) : base(settings)
    {
    }

    public VerseType? GetVerseTypeByName(string name)
    {
        UpdateContext();
        return context.VerseTypes.FirstOrDefault(vt => vt.Name == name);
    }
}
