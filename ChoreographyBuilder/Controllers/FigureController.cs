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
			var model = await figureService.AllUserFiguresAsync(User.Id());

			return View(model);
		}


        [HttpGet]
        public IActionResult Add()
        {
            var model = new FigureFormViewModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(FigureFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await figureService.AddFigureAsync(model, User.Id());

            //Change it to redirect to the figure options
            return RedirectToAction(nameof(Mine));
        }
    }
}
