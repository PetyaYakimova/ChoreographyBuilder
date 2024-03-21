using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Extensions;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Controllers
{
	public class FigureController : BaseController
	{
		private readonly IFigureService figureService;
		private readonly IFigureOptionService figureOptionService;
		private readonly IPositionService positionService;

		public FigureController(IFigureService figureService, IFigureOptionService figureOptionService, IPositionService positionService)
		{
			this.figureService = figureService;
			this.figureOptionService = figureOptionService;
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
			query.Entities = model.Entities;

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

			return RedirectToAction(nameof(Options), new { FigureId = figureId });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//Check is user and not admin
			var model = await figureService.GetFigureByIdAsync(id);
			if (model == null)
			{
				return BadRequest();
			}

			string figureUserId = await figureService.GetUserIdForFigureByIdAsync(id);
			if (figureUserId != User.Id())
			{
				return Unauthorized();
			}

			//For now edit is possible for figures used in choreos
			//if (await figureService.IsFigureUsedInChoreographiesAsync(id))
			//{
			//	return BadRequest();
			//}

			return View(model);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Edit(int id, FigureFormViewModel model)
		{
			//Check is user and not admin
			var figure = await figureService.GetFigureByIdAsync(id);
			if (figure == null)
			{
				return BadRequest();
			}

			string figureUserId = await figureService.GetUserIdForFigureByIdAsync(id);
			if (figureUserId != User.Id())
			{
				return Unauthorized();
			}

			//For now edit is possible for figures used in choreos
			//if (await figureService.IsFigureUsedInChoreographiesAsync(id))
			//{
			//	return BadRequest();
			//}

			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await figureService.EditFigureAsync(id, model);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
		public async Task<IActionResult> Options([FromQuery] AllFigureOptionsQueryModel query)
		{
			//Check is user and not admin
			try
			{
				string figureUserId = await figureService.GetUserIdForFigureByIdAsync(query.FigureId);
				if (figureUserId != User.Id())
				{
					return Unauthorized();
				}

				var model = await figureOptionService.GetFigureOptionsAsync(
					query.FigureId,
					query.StartPosition,
					query.EndPosition,
					query.BeatsCount,
					query.DynamicsType,
					query.CurrentPage,
					query.ItemsPerPage);

				query.TotalItemCount = model.TotalCount;
				query.Entities = model.Entities;
				query.FigureId = model.FigureId;
				query.FigureName = model.FigureName;
				query.Positions = await GetAllActivePositionsAndSelectedPosition(null);
				query.DynamicsTypes = GetAllDynamicsTypes();


				return View(query);
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

				var positions = await GetAllActivePositionsAndSelectedPosition(null);
				var dynamicTypes = GetAllDynamicsTypes();

				if (!positions.Any(p => p.Id == model.StartPositionId))
				{
					ModelState.AddModelError(nameof(model.StartPositionId), PositionDoesntExistErrorMessage);
				}

				if (!positions.Any(p => p.Id == model.EndPositionId))
				{
					ModelState.AddModelError(nameof(model.EndPositionId), PositionDoesntExistErrorMessage);
				}

				if (!dynamicTypes.Any(dt => dt == model.DynamicsType))
				{
					ModelState.AddModelError(nameof(model.DynamicsType), DynamicsTypeDoesntExistErrorMessage);
				}

				if (ModelState.IsValid == false)
				{
					string figureName = await figureService.GetFigureNameByIdAsync(id);

					model.StartPositions = positions;
					model.EndPositions = positions;
					model.DynamicsTypes = dynamicTypes;
					model.FigureId = id;
					model.FigureName = figureName;
					return View(model);
				}

				await figureOptionService.AddFigureOptionAsync(model);

				return RedirectToAction(nameof(Options), new { FigureId = id });
			}

			catch (ArgumentNullException)
			{
				return BadRequest();
			}
		}

		[HttpGet]
		public async Task<IActionResult> EditOption(int id)
		{
			//Check is user and not admin
			var model = await figureOptionService.GetFigureOptionByIdAsync(id);
			if (model == null)
			{
				return BadRequest();
			}

			string figureUserId = await figureOptionService.GetUserIdForFigureOptionByIdAsync(id);
			if (figureUserId != User.Id())
			{
				return Unauthorized();
			}

			if (await figureOptionService.IsFigureOptionUsedInChoreographiesAsync(id))
			{
				return BadRequest();
			}

            string figureName = await figureService.GetFigureNameByIdAsync(model.FigureId);

            model.StartPositions = await GetAllActivePositionsAndSelectedPosition(model.StartPositionId);
            model.EndPositions = await GetAllActivePositionsAndSelectedPosition(model.EndPositionId);
            model.DynamicsTypes = GetAllDynamicsTypes();
            model.FigureName = figureName;

            return View(model);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> EditOption(FigureOptionFormViewModel model, int id)
		{
			//Check is user and not admin
			var option = await figureOptionService.GetFigureOptionByIdAsync(id);
			if (option == null)
			{
				return BadRequest();
			}

			if (option.FigureId != model.FigureId)
			{
                return BadRequest();
            }

			string figureUserId = await figureOptionService.GetUserIdForFigureOptionByIdAsync(id);
			if (figureUserId != User.Id())
			{
				return Unauthorized();
			}

			if (await figureOptionService.IsFigureOptionUsedInChoreographiesAsync(id))
			{
				return BadRequest();
			}

			var startPositions = await GetAllActivePositionsAndSelectedPosition(option.StartPositionId);
			var endPositions = await GetAllActivePositionsAndSelectedPosition(option.EndPositionId);
			var dynamicTypes = GetAllDynamicsTypes();

			if (!startPositions.Any(p => p.Id == model.StartPositionId))
			{
				ModelState.AddModelError(nameof(model.StartPositionId), PositionDoesntExistErrorMessage);
			}

			if (!endPositions.Any(p => p.Id == model.EndPositionId))
			{
				ModelState.AddModelError(nameof(model.EndPositionId), PositionDoesntExistErrorMessage);
			}

			if (!dynamicTypes.Any(dt => dt == model.DynamicsType))
			{
				ModelState.AddModelError(nameof(model.DynamicsType), DynamicsTypeDoesntExistErrorMessage);
			}

			if (ModelState.IsValid == false)
			{
				string figureName = await figureService.GetFigureNameByIdAsync(model.FigureId);

                model.StartPositions = startPositions;
				model.EndPositions = endPositions;
				model.DynamicsTypes = dynamicTypes;
				model.FigureName = figureName;
				return View(model);
			}

			await figureOptionService.EditFigureOptionAsync(id, model);

			return RedirectToAction(nameof(Options), new { FigureId = option.FigureId });
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
