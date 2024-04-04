using ChoreographyBuilder.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChoreographyBuilder.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IStatisticService statisticService;

		public HomeController(IStatisticService statisticService)
		{
			this.statisticService = statisticService;
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Index()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
			{
				if (User.IsAdmin())
				{
					return RedirectToAction("Stats", "Home", new { area = "Admin" });
				}
				else if (User.IsUser())
				{
					return RedirectToAction(nameof(Stats));
				}
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Stats()
		{
			var model = await statisticService.GetUserStatisticsAsync(User.Id());

			return View(model);
		}

		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 400)
			{
				return View("Error400");
			}

			if (statusCode == 401)
			{
				return View("Error401");
			}

			if (statusCode == 404)
			{
				return View("Error404");
			}

			if (statusCode == 500)
			{
				return View("Error500");
			}

			return View();
		}
	}
}