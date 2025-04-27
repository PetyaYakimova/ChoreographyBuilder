using ChoreographyBuilder.UITests.Setup;

namespace ChoreographyBuilder.UITests.Repositories;

public class UserRepository : BaseRepository
{
    public UserRepository(AppSettings settings) : base(settings)
    {
    }

    public bool IsUserSaved(string email)
        => context.Users.Any(u => u.Email == email);
}
