using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Core.Services;
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

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			//Check is user and not admin
			var model = await figureService.AllUserFiguresAsync(User.Id());

			return View(model);
		}


		[HttpGet]
		public IActionResult Add()
		{
			//Check is user and not admin
			var model = new FigureFormViewModel();

			return View(model);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Add(FigureFormViewModel model)
		{
			//Check is user and not admin
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			int figureId = await figureService.AddFigureAsync(model, User.Id());

			//Change it to redirect to the figure options
			return RedirectToAction(nameof(Options), figureId);
		}

		[HttpGet]
		public async Task<IActionResult> Options(int id)
		{
			//Check is user and not admin
			try
			{
				string figureUserId = await figureService.GetUserIdForFigureByIdAsync(id);
				if (figureUserId != User.Id())
				{
					return Unauthorized();
				}

				var model = await figureService.GetFigureWithOptionsAsync(id);

				return View(model);
			}
			catch (ArgumentNullException)
			{
				return BadRequest();
			}
		}
	}
}
