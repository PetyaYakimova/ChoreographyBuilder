using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;

namespace ChoreographyBuilder.Controllers
{
	[Authorize(Roles = UserRoleName)]
	public class BaseController : Controller
	{
	}
}
