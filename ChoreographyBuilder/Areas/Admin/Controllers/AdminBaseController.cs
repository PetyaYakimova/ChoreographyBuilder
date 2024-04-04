using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = AdminRoleName)]
	public class AdminBaseController : Controller
	{
	}
}
