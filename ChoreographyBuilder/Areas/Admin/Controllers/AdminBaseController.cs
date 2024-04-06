using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;
using static ChoreographyBuilder.Constants.AreasConstants;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	[Area(AdminAreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class AdminBaseController : Controller
	{
	}
}
