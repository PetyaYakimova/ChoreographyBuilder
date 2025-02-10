using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.User;

/// <summary>
/// A model used for getting criteria for filtering users and pagination info.
/// No added validation attributes.
/// </summary>
public class AllUsersQueryModel : AllEntitiesQueryBaseModel<UserTableViewModel>
{
}
