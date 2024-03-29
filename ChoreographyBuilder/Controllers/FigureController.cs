﻿using ChoreographyBuilder.Attributes;
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
		//Check that user is user and not admin
		public async Task<IActionResult> Mine([FromQuery] AllFiguresQueryModel query)
		{
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
		//Check that user is user and not admin
		public IActionResult Add()
		{
			FigureFormViewModel model = new FigureFormViewModel();

			return View(model);
		}

		[HttpPost]
		//Check that user is user and not admin
		public async Task<IActionResult> Add(FigureFormViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			int figureId = await figureService.AddFigureAsync(model, User.Id());

			return RedirectToAction(nameof(Options), new { Id = figureId });
		}

		[HttpGet]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await figureService.GetFigureByIdAsync(id);

			return View(model);
		}

		[HttpPost]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		public async Task<IActionResult> Edit(int id, FigureFormViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await figureService.EditFigureAsync(id, model);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		[FigureNotUsedInChoreographies]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await figureService.GetFigureForDeleteAsync(id);

			return View(model);
		}

		[HttpPost]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		[FigureNotUsedInChoreographies]
		public async Task<IActionResult> Delete(FigureForPreviewViewModel model)
		{
			await figureService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		public async Task<IActionResult> Options(int id, [FromQuery] AllFigureOptionsQueryModel query)
		{
			var model = await figureOptionService.GetFigureOptionsAsync(
				id,
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
			query.Positions = await GetAllActivePositionsAndSelectedPositionAsync();
			query.DynamicsTypes = GetAllDynamicsTypes();

			return View(query);
		}

		[HttpGet]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		public async Task<IActionResult> AddOption(int id)
		{
			string figureName = await figureService.GetFigureNameByIdAsync(id);

			FigureOptionFormViewModel model = new FigureOptionFormViewModel();
			model.StartPositions = await GetAllActivePositionsAndSelectedPositionAsync();
			model.EndPositions = await GetAllActivePositionsAndSelectedPositionAsync();
			model.DynamicsTypes = GetAllDynamicsTypes();
			model.FigureId = id;
			model.FigureName = figureName;

			return View(model);
		}

		[HttpPost]
		//Check that user is user and not admin
		[FigureExistsForThisUser]
		public async Task<IActionResult> AddOption(FigureOptionFormViewModel model, int id)
		{
			var positions = await GetAllActivePositionsAndSelectedPositionAsync();
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

			return RedirectToAction(nameof(Options), new { Id = id });
		}

		[HttpGet]
		//Check that user is user and not admin
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> EditOption(int id)
		{
			FigureOptionFormViewModel model = (await figureOptionService.GetFigureOptionByIdAsync(id)) ?? new FigureOptionFormViewModel();

			string figureName = await figureService.GetFigureNameByIdAsync(model.FigureId);

			model.StartPositions = await GetAllActivePositionsAndSelectedPositionAsync(model.StartPositionId);
			model.EndPositions = await GetAllActivePositionsAndSelectedPositionAsync(model.EndPositionId);
			model.DynamicsTypes = GetAllDynamicsTypes();
			model.FigureName = figureName;

			return View(model);
		}

		[HttpPost]
		//Check that user is user and not admin
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> EditOption(FigureOptionFormViewModel model, int id)
		{
			FigureOptionFormViewModel option = await figureOptionService.GetFigureOptionByIdAsync(id) ?? new FigureOptionFormViewModel();
			if (option.FigureId != model.FigureId)
			{
				return BadRequest();
			}

			var startPositions = await GetAllActivePositionsAndSelectedPositionAsync(option.StartPositionId);
			var endPositions = await GetAllActivePositionsAndSelectedPositionAsync(option.EndPositionId);
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

			return RedirectToAction(nameof(Options), new { Id = option.FigureId });
		}

		[HttpGet]
		//Check that user is user and not admin
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> DeleteOption(int id)
		{
			var model = await figureOptionService.GetFigureOptionForDeleteAsync(id);

			return View(model);
		}

		[HttpPost]
		//Check that user is user and not admin
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> DeleteOption(FigureOptionDeleteViewModel model)
		{
			await figureOptionService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(Options), new { Id = model.FigureId });
		}

		private async Task<IEnumerable<PositionForPreviewViewModel>> GetAllActivePositionsAndSelectedPositionAsync(int? currentPositionId = null)
		{
			return await positionService.AllActivePositionsAndSelectedPositionAsync(currentPositionId);
		}

		private DynamicsType[] GetAllDynamicsTypes()
		{
			return (DynamicsType[])Enum.GetValues(typeof(DynamicsType));
		}
	}
}
