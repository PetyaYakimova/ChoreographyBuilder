using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Core.Services;
using ChoreographyBuilder.Extensions;
using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
    public class FigureController : BaseController
    {
        private readonly IFigureService figureService;
        private readonly IPositionService positionService;

        public FigureController(IFigureService figureService, IPositionService positionService)
        {
            this.figureService = figureService;
            this.positionService = positionService;
        }

		[HttpGet]
		public async Task<IActionResult> Mine([FromQuery] AllFiguresQueryModel query)
		{
			//Check that user is admin
			
            var model = await figureService.AllUserFiguresAsync(
                User.Id(),
				query.SearchTerm,
				query.CurrentPage,
				query.ItemsPerPage);

			query.TotalItemCount = model.TotalCount;
			query.Figures = model.Figures;

			return View(query);
		}


		[HttpGet]
        public IActionResult Add()
        {
            //Check is user and not admin
            FigureFormViewModel model = new FigureFormViewModel();

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

            return RedirectToAction(nameof(Options), new { Id = figureId });
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

        [HttpGet]
        public async Task<IActionResult> AddOption(int id)
        {
            //Check is user and not admin
            try
            {
                string figureUserId = await figureService.GetUserIdForFigureByIdAsync(id);
                if (figureUserId != User.Id())
                {
                    return Unauthorized();
                }

                string figureName = await figureService.GetFigureNameByIdAsync(id);

                FigureOptionFormViewModel model = new FigureOptionFormViewModel();
                model.StartPositions = await GetAllActivePositionsAndSelectedPosition(null);
                model.EndPositions = await GetAllActivePositionsAndSelectedPosition(null);
                model.DynamicsTypes = GetAllDynamicsTypes();
                model.FigureId = id;
                model.FigureName = figureName;

                return View(model);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddOption(FigureOptionFormViewModel model, int id)
        {
            //Check is user and not admin
            try
            {
                string figureUserId = await figureService.GetUserIdForFigureByIdAsync(id);
                if (figureUserId != User.Id())
                {
                    return Unauthorized();
                }

                if (ModelState.IsValid == false)
                {
                    string figureName = await figureService.GetFigureNameByIdAsync(id);

                    model.StartPositions = await GetAllActivePositionsAndSelectedPosition(null);
                    model.EndPositions = await GetAllActivePositionsAndSelectedPosition(null);
                    model.DynamicsTypes = GetAllDynamicsTypes();
                    model.FigureId = id;
                    model.FigureName = figureName;
                    return View(model);
                }

                await figureService.AddFigureOptionAsync(model);

                return RedirectToAction(nameof(Options), new { Id = id });
            }

            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }


        private async Task<IEnumerable<PositionForFigureViewModel>> GetAllActivePositionsAndSelectedPosition(int? currentPositionId)
        {
            return await positionService.AllActivePositionsAndSelectedPositionAsync(currentPositionId);
        }

        private DynamicsType[] GetAllDynamicsTypes()
        {
            return (DynamicsType[])Enum.GetValues(typeof(DynamicsType));
        }
    }
}
