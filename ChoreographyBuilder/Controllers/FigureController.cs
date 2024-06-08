using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Controllers
{
	public class FigureController : BaseController
	{
		private readonly ILogger<FigureController> logger;
		private readonly IFigureService figureService;
		private readonly IFigureOptionService figureOptionService;
		private readonly IPositionService positionService;

		public FigureController(ILogger<FigureController> logger, IFigureService figureService, IFigureOptionService figureOptionService, IPositionService positionService)
		{
			this.logger = logger;
			this.figureService = figureService;
			this.figureOptionService = figureOptionService;
			this.positionService = positionService;
		}

		[HttpGet]
		public async Task<IActionResult> Mine([FromQuery] AllFiguresQueryModel query)
		{
			var model = await figureService.AllUserFiguresAsync(
				User.Id(),
				false,
				query.SearchTerm,
				query.StartPosition,
				query.EndPosition,
				query.BeatsCount,
				query.DynamicsType,
				query.CurrentPage,
				query.ItemsPerPage);

			query.TotalItemCount = model.TotalCount;
			query.Entities = model.Entities;
			query.Positions = await GetAllActivePositionsAndSelectedPositionAsync();
			query.DynamicsTypes = GetAllDynamicsTypes();

			return View(query);
		}

		[HttpGet]
		public async Task<IActionResult> Shared([FromQuery] AllFiguresQueryModel query)
		{
			var model = await figureService.AllUserFiguresAsync(
				User.Id(),
				true,
				query.SearchTerm,
				query.StartPosition,
				query.EndPosition,
				query.BeatsCount,
				query.DynamicsType,
				query.CurrentPage,
				query.ItemsPerPage);

			query.TotalItemCount = model.TotalCount;
			query.Entities = model.Entities;
			query.Positions = await GetAllActivePositionsAndSelectedPositionAsync();
			query.DynamicsTypes = GetAllDynamicsTypes();

			return View(query);
		}

		[HttpGet]
		public IActionResult Add()
		{
			FigureFormViewModel model = new FigureFormViewModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(FigureFormViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			int figureId = await figureService.AddFigureAsync(model, User.Id());

			TempData[UserMessageSuccess] = string.Format(ItemAddedSuccessMessage, FigureAsString);

			return RedirectToAction(nameof(Options), new { Id = figureId });
		}

		[HttpGet]
		[FigureExistsForThisUserOrIsShared]
		public async Task<IActionResult> Copy(int id)
		{
			var model = await figureService.GetFigureForCopyAsync(id);

			return View(model);
		}

		[HttpPost]
		[FigureExistsForThisUserOrIsShared]
		public async Task<IActionResult> Copy(FigureForCopyViewModel model)
		{
			await figureService.CopyFigureForUserAsync(model.Id, User.Id());

			TempData[UserMessageSuccess] = string.Format(ItemAddedSuccessMessage, FigureAsString);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
		[FigureExistsForThisUser]
		public async Task<IActionResult> Edit(int id)
		{
			FigureFormViewModel model = await figureService.GetFigureByIdAsync(id);

			return View(model);
		}

		[HttpPost]
		[FigureExistsForThisUser]
		public async Task<IActionResult> Edit(int id, FigureFormViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await figureService.EditFigureAsync(id, model);

			TempData[UserMessageSuccess] = string.Format(ItemUpdatedSuccessMessage, FigureAsString);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
		[FigureExistsForThisUser]
		[FigureNotUsedInChoreographies]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await figureService.GetFigureForDeleteAsync(id);

			return View(model);
		}

		[HttpPost]
		[FigureExistsForThisUser]
		[FigureNotUsedInChoreographies]
		public async Task<IActionResult> Delete(FigureForPreviewViewModel model)
		{
			await figureService.DeleteFigureAsync(model.Id);

			TempData[UserMessageSuccess] = string.Format(ItemDeletedSuccessMessage, FigureAsString);

			return RedirectToAction(nameof(Mine));
		}

		[HttpGet]
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
		[FigureExistsForThisUserOrIsShared]
		public async Task<IActionResult> PreviewOptions(int id, [FromQuery] AllFigureOptionsQueryModel query)
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

			TempData[UserMessageSuccess] = string.Format(ItemAddedSuccessMessage, FigureOptionAsString);

			return RedirectToAction(nameof(Options), new { Id = id });
		}

		[HttpGet]
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> EditOption(int id)
		{
			FigureOptionFormViewModel model = await figureOptionService.GetFigureOptionByIdAsync(id);

			string figureName = await figureService.GetFigureNameByIdAsync(model.FigureId);

			model.StartPositions = await GetAllActivePositionsAndSelectedPositionAsync(model.StartPositionId);
			model.EndPositions = await GetAllActivePositionsAndSelectedPositionAsync(model.EndPositionId);
			model.DynamicsTypes = GetAllDynamicsTypes();
			model.FigureName = figureName;

			return View(model);
		}

		[HttpPost]
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> EditOption(FigureOptionFormViewModel model, int id)
		{
			FigureOptionFormViewModel option = await figureOptionService.GetFigureOptionByIdAsync(id);
			if (option.FigureId != model.FigureId)
			{
				logger.LogError(UnmatchedFigureIdsLoggerErrorMessage);
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

			TempData[UserMessageSuccess] = string.Format(ItemUpdatedSuccessMessage, FigureOptionAsString);

			return RedirectToAction(nameof(Options), new { Id = option.FigureId });
		}

		[HttpGet]
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> DeleteOption(int id)
		{
			var model = await figureOptionService.GetFigureOptionForDeleteAsync(id);

			return View(model);
		}

		[HttpPost]
		[FigureOptionExistsForThisUser]
		[FigureOptionNotUsedInChoreographies]
		public async Task<IActionResult> DeleteOption(FigureOptionDeleteViewModel model)
		{
			await figureOptionService.DeleteFigureOptionAsync(model.Id);

			TempData[UserMessageSuccess] = string.Format(ItemDeletedSuccessMessage, FigureOptionAsString);

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
