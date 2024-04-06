using ChoreographyBuilder.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	public class HomeController : AdminBaseController
	{
		private readonly IUserService statisticService;

		public HomeController(IUserService statisticService)
		{
			this.statisticService = statisticService;
		}

		[HttpGet]
		public async Task<IActionResult> Stats()
		{
			var model = await statisticService.GetAdminStatisticsAsync();

			return View(model);
		}
	}
}
