using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Constants.AreasConstants;
using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	[Area(AdminAreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class AdminBaseController : Controller
	{
	}
}
