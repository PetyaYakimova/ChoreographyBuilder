using ChoreographyBuilder.Core.Models.BaseModels;

namespace ChoreographyBuilder.Core.Models.User
{
	/// <summary>
	/// A model that has the total count of users and a collection of a certain amount of them to display them on pages.
	/// No added validation attributes.
	/// </summary>
	public class UserQueryServiceModel: EntityQueryBaseModel<UserTableViewModel>
	{
	}
}
