using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
	public class FigureController : BaseController
	{
		private readonly IFigureService figureService;

		public FigureController(IFigureService figureService)
		{
			this.figureService = figureService;
		}

		public async Task<IActionResult> Mine()
		{
			var model = await figureService.AllUserFiguresAsync(User.Id());

			return View(model);
		}
	}
}
