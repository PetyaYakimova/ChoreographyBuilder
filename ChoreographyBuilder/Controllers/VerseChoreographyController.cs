using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Controllers
{
    public class VerseChoreographyController : BaseController
    {
        private readonly ILogger<VerseChoreographyController> logger;
        private readonly IVerseChoreographyService verseChoreographyService;
        private readonly IVerseChoreographyFigureService verseChoreographyFigureService;
        private readonly IPositionService positionService;
        private readonly IVerseTypeService verseTypeService;
        private readonly IFigureService figureService;
        private readonly IFigureOptionService figureOptionService;

        public VerseChoreographyController(
            ILogger<VerseChoreographyController> logger,
            IVerseChoreographyService verseChoreographyService,
            IVerseChoreographyFigureService verseChoreographyFigureService,
            IPositionService positionService,
            IVerseTypeService verseTypeService,
            IFigureService figureService,
            IFigureOptionService figureOptionService)
        {
            this.logger = logger;
            this.verseChoreographyService = verseChoreographyService;
            this.verseChoreographyFigureService = verseChoreographyFigureService;
            this.positionService = positionService;
            this.verseTypeService = verseTypeService;
            this.figureService = figureService;
            this.figureOptionService = figureOptionService;
        }

        [HttpGet]
        public async Task<IActionResult> Mine([FromQuery] AllVerseChoreographiesQueryModel query)
        {
            var model = await verseChoreographyService.AllUserVerseChoreographiesAsync(
                User.Id(),
                query.SearchTerm,
                query.VerseType,
                query.StartPosition,
                query.EndPosition,
                query.FinalFigure,
                query.CurrentPage,
                query.ItemsPerPage);

            query.TotalItemCount = model.TotalCount;
            query.Entities = model.Entities;
            query.VerseTypes = await GetAllActiveVerseTypesAsync();
            query.Positions = await GetAllActivePositionsAsync();
            query.Figures = await GetAllUserHighlightFiguresAsync();

            return View(query);
        }

        [HttpGet]
        [VerseChoreographyExistsForThisUser]
        public async Task<IActionResult> Details(int id)
        {
            var model = await verseChoreographyService.GetVerseChoreographyByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            var model = new VerseChoreographyGenerateModel();
            model.VerseTypes = await GetAllActiveVerseTypesAsync();
            model.Positions = await GetAllActivePositionsAsync();
            model.Figures = await GetAllUserHighlightFiguresAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Suggestions([FromQuery] VerseChoreographyGenerateModel query)
        {
            if (!(await GetAllActiveVerseTypesAsync()).Any(vt => vt.Id == query.VerseTypeId) ||
                query.StartPositionId != null && !(await GetAllActivePositionsAsync()).Any(p => p.Id == query.StartPositionId) ||
                !(await GetAllUserHighlightFiguresAsync()).Any(f => f.Id == query.FinalFigureId))
            {
                logger.LogError(InvalidRequestForGeneratingVerseChoreographiesErrorMessage);
                TempData[UserMessageError] = InvalidRequestForGeneratingVerseChoreographiesErrorMessage;

                return RedirectToAction(nameof(Generate));
            }

            var model = await verseChoreographyService.GenerateChoreographies(query, User.Id());

            if (model.Any())
            {
                TempData[UserMessageSuccess] = VerseChoreographiesSuggestionsGeneratedSuccessMessage;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(VerseChoreographySaveViewModel model)
        {
            if (!await verseTypeService.VerseTypeExistByIdAsync(model.VerseTypeId))
            {
                logger.LogError(InvalidVerseTypeIdWhenSavingVerseChoreographyErrorMessage);
                return BadRequest();
            }

            List<int> figureOptionsIds = model.Figures.Select(f => f.FigureOptionId).ToList();
            string userId = User.Id();
            foreach (int figureOptionId in figureOptionsIds)
            {
                if (!await figureOptionService.FigureOptionExistForThisUserByIdAsync(figureOptionId, userId))
                {
                    logger.LogError(InvalidFigureOptionIdWhenSavingVerseChoreographyErrorMessage);
                    return BadRequest();
                }
            }

            if (ModelState.IsValid == false)
            {
                TempData[UserMessageError] = InvalidVerseChoreographyErrorMessage;

                return RedirectToAction(nameof(Generate));
            }

            await verseChoreographyService.SaveVerseChoreographyAsync(model, User.Id());

            TempData[UserMessageSuccess] = String.Format(ItemAddedSuccessMessage, VerseChoreographyAsString);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            VerseChoreographyFormViewModel model = new VerseChoreographyFormViewModel();
            model.VerseTypes = await GetAllActiveVerseTypesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(VerseChoreographyFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            int verseChoreographyId = await verseChoreographyService.AddVerseChoreographyAsync(model, User.Id());

            TempData[UserMessageSuccess] = String.Format(ItemAddedSuccessMessage, VerseChoreographyAsString);

            return RedirectToAction(nameof(Details), new { Id = verseChoreographyId });
        }

        [HttpGet]
        [VerseChoreographyExistsForThisUser]
        [VerseChoreographyIsNotComplete]
        public async Task<IActionResult> AddFigure(int id)
        {
            var model = new VerseChoreographyFigureOptionFormViewModel();
            model.FigureOrder = (await verseChoreographyService.GetNumberOfFiguresForVerseChoreographyAsync(id)) + 1;
            model.RemainingBeats = await verseChoreographyService.GetNumberOfRemainingBeatsForVerseChoreographyAsync(id);
            PositionForPreviewViewModel? lastfigureEndPosition = await verseChoreographyService.GetLastFigureEndPositionAsync(id);
            model.Figures = await GetAllUserFiguresWithStartPositionAndLessThanRemainingBeatsAsync(model.RemainingBeats, lastfigureEndPosition?.Id);
            model.StartPositionName = lastfigureEndPosition?.Name;

            return View(model);
        }

        [HttpPost]
        [VerseChoreographyExistsForThisUser]
        [VerseChoreographyIsNotComplete]
        public async Task<IActionResult> AddFigure(VerseChoreographyFigureOptionFormViewModel model, int id)
        {
            bool figureOptionExists = await figureOptionService.FigureOptionExistForThisUserByIdAsync(model.FigureOptionId, User.Id());
            if (!figureOptionExists)
            {
                ModelState.AddModelError(nameof(model.FigureOptionId), FigureOptionDoesntExistErrorMessage);
            }

            int nextAvailableOrder = (await verseChoreographyService.GetNumberOfFiguresForVerseChoreographyAsync(id)) + 1;
            if (nextAvailableOrder != model.FigureOrder)
            {
                ModelState.AddModelError(nameof(model.FigureOrder), InvalidFigureOrderErrorMessage);
            }

            int remainingBeats = await verseChoreographyService.GetNumberOfRemainingBeatsForVerseChoreographyAsync(id);
            int figureBeats = await figureOptionService.GetBeatsForFigureOptionAsync(model.FigureOptionId);
            if (remainingBeats < figureBeats)
            {
                ModelState.AddModelError(nameof(model.FigureOptionId), FigureHasTooManyBeatsErrorMessage);
            }

            if (model.StartPositionName != null)
            {
                string figureStartPositionName = await figureOptionService.GetStartPositionNameForFigureOptionAsync(model.FigureOptionId);
                if (figureStartPositionName != model.StartPositionName)
                {
                    ModelState.AddModelError(nameof(model.FigureOptionId), FigureStartsWithWrongPositionErrorMessage);
                }
            }

            if (ModelState.IsValid == false)
            {
                model.FigureOrder = (await verseChoreographyService.GetNumberOfFiguresForVerseChoreographyAsync(id)) + 1;
                model.RemainingBeats = await verseChoreographyService.GetNumberOfRemainingBeatsForVerseChoreographyAsync(id);
                PositionForPreviewViewModel? lastfigureEndPosition = await verseChoreographyService.GetLastFigureEndPositionAsync(id);
                model.Figures = await GetAllUserFiguresWithStartPositionAndLessThanRemainingBeatsAsync(model.RemainingBeats, lastfigureEndPosition?.Id);
                model.StartPositionName = lastfigureEndPosition?.Name;

                return View(model);
            }

            await verseChoreographyFigureService.AddFigureToVerseChoreographyAsync(id, model);

            TempData[UserMessageSuccess] = String.Format(ItemAddedSuccessMessage, FigureAsString);

            int remainingBeatsAfterAddingFigure = await verseChoreographyService.GetNumberOfRemainingBeatsForVerseChoreographyAsync(id);
            if (remainingBeatsAfterAddingFigure > 0)
            {
                return RedirectToAction(nameof(AddFigure), new { Id = id });
            }
            else
            {
                return RedirectToAction(nameof(Details), new { Id = id });
            }
        }

        [HttpGet]
        [VerseChoreographyFigureExistsForThisUser]
        public async Task<IActionResult> ReplaceFigure(int id)
        {
            VerseChoreographyFigureReplaceViewModel model = await verseChoreographyFigureService.GetVerseChoreographyFigureForReplaceAsync(id);
            model.PossibleReplacementFigures = await verseChoreographyFigureService.GetPossibleReplacementsForVerseChoreographyFigureAsync(id);

            return View(model);
        }

        [HttpPost]
        [VerseChoreographyFigureExistsForThisUser]
        public async Task<IActionResult> ReplaceFigure(VerseChoreographyFigureReplaceViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                VerseChoreographyFigureReplaceViewModel updatedModel = await verseChoreographyFigureService.GetVerseChoreographyFigureForReplaceAsync(id);
                updatedModel.PossibleReplacementFigures = await verseChoreographyFigureService.GetPossibleReplacementsForVerseChoreographyFigureAsync(id);
                return View(updatedModel);
            }

            VerseChoreographyFigureSelectedReplacementServiceModel serviceModel = new VerseChoreographyFigureSelectedReplacementServiceModel()
            {
                FigureOptionId = model.ReplacementFigureOptionId,
                FigureOrder = model.FigureOrder
            };

            int verseChoreographyId = await verseChoreographyFigureService.GetVerseChoreographyIdForVerseChoreographyFigureByIdAsync(id);
            await verseChoreographyService.ChangeFigureInVerseChoreographyAsync(verseChoreographyId, serviceModel);
            return RedirectToAction(nameof(Details), new { Id = verseChoreographyId });
        }

        [HttpGet]
        [FigureInVerseChoreographyExistsForThisUser]
        [FigureIsLastInVerseChoreography]
        [VerseChoreographyIsNotComplete]
        public async Task<IActionResult> DeleteFigure(int id)
        {
            var model = await verseChoreographyFigureService.GetFigureForGelete(id);

            return View(model);
        }

        [HttpPost]
        [FigureInVerseChoreographyExistsForThisUser]
        [FigureIsLastInVerseChoreography]
        [VerseChoreographyIsNotComplete]
        public async Task<IActionResult> DeleteFigure(VerseChoreographyFigureDeleteViewModel model)
        {
            await verseChoreographyFigureService.DeleteFigureFromVerseChoreographyAsync(model.Id);

            TempData[UserMessageSuccess] = String.Format(ItemDeletedSuccessMessage, FigureAsString);

            return RedirectToAction(nameof(Details), new { Id = model.FullChoreographyId });
        }

        [HttpGet]
        [VerseChoreographyExistsForThisUser]
        [VerseChoreographyNotUsedInFullChoreographies]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await verseChoreographyService.GetVerseChoreographyForDeleteAsync(id);

            return View(model);
        }

        [HttpPost]
        [VerseChoreographyExistsForThisUser]
        [VerseChoreographyNotUsedInFullChoreographies]
        public async Task<IActionResult> Delete(VerseChoreographyDeleteViewModel model)
        {
            await verseChoreographyService.DeleteVerseChoreographyAsync(model.Id);

            TempData[UserMessageSuccess] = String.Format(ItemDeletedSuccessMessage, VerseChoreographyAsString);

            return RedirectToAction(nameof(Mine));
        }

        private async Task<IEnumerable<VerseTypeForPreviewViewModel>> GetAllActiveVerseTypesAsync()
        {
            return await verseTypeService.AllActiveVerseTypesOrSelectedVerseTypeAsync();
        }

        private async Task<IEnumerable<PositionForPreviewViewModel>> GetAllActivePositionsAsync()
        {
            return await positionService.AllActivePositionsAndSelectedPositionAsync();
        }

        private async Task<IEnumerable<FigureForPreviewViewModel>> GetAllUserHighlightFiguresAsync()
        {
            return await figureService.AllUserHighlightFiguresForChoreographiesAsync(User.Id());
        }

        private async Task<IEnumerable<FigureOptionWithFigureViewModel>> GetAllUserFiguresWithStartPositionAndLessThanRemainingBeatsAsync(int remainingbeats, int? startPositionId = null)
        {
            return await figureOptionService.AllUserFiguresStartingWithPositionAndLessThanBeatsAsync(User.Id(), remainingbeats, startPositionId);
        }
    }
}
