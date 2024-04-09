using ChoreographyBuilder.Core.Models.Statistics;

namespace ChoreographyBuilder.Core.Models.User
{
    /// <summary>
    /// A view model only for displaying statistics for every user.
    /// No added validation attributes.
    /// </summary>
    public class UserTableViewModel : UserStatisticModel
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
