using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	public class UserController : AdminBaseController
	{
		private readonly IUserService statisticService;

		public UserController(IUserService statisticService)
		{
			this.statisticService = statisticService;
		}

		[HttpGet]
		public async Task<IActionResult> All([FromQuery] AllUsersQueryModel query)
		{
			var model = await statisticService.GetAllUserStatisticsAsync(
				query.SearchTerm,
				query.CurrentPage,
				query.ItemsPerPage);

			query.TotalItemCount = model.TotalCount;
			query.Entities = model.Entities;

			return View(query);
		}
	}
}
