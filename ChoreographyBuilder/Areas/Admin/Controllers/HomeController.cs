using ChoreographyBuilder.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	public class HomeController : AdminBaseController
	{
		private readonly IUserService userService;

		public HomeController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Stats()
		{
			var model = await userService.GetAdminStatisticsAsync();

			return View(model);
		}
	}
}
