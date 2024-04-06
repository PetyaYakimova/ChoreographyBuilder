using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseType;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Controllers
{
    public class VerseChoreographyController : BaseController
    {
        private readonly ILogger<VerseChoreographyController> logger;
        private readonly IVerseChoreographyService verseChoreographyService;
        private readonly IPositionService positionService;
        private readonly IVerseTypeService verseTypeService;
        private readonly IFigureService figureService;
        private readonly IFigureOptionService figureOptionService;

        public VerseChoreographyController(
            ILogger<VerseChoreographyController> logger,
            IVerseChoreographyService verseChoreographyService,
            IPositionService positionService,
            IVerseTypeService verseTypeService,
            IFigureService figureService,
            IFigureOptionService figureOptionService)
        {
            this.logger = logger;
            this.verseChoreographyService = verseChoreographyService;
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
            var model = await verseChoreographyService.GetChoreographyByIdAsync(id);

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
                logger.LogError("Invalid verse type id when saving verse choreography!");
                return BadRequest();
            }

            List<int> figureOptionsIds = model.Figures.Select(f => f.FigureOptionId).ToList();
            string userId = User.Id();
            foreach (int figureOptionId in figureOptionsIds)
            {
                if (!await figureOptionService.FigureOptionExistForThisUserByIdAsync(figureOptionId, userId))
                {
                    logger.LogError("Invalid figure option id when saving verse choreography!");
                    return BadRequest();
                }
            }

            if (ModelState.IsValid == false)
            {
                TempData[UserMessageError] = InvalidVerseChoreograpyErrorMessage;

                return RedirectToAction(nameof(Generate));
            }

            await verseChoreographyService.SaveChoreographyAsync(model, User.Id());

            TempData[UserMessageSuccess] = String.Format(ItemAddedSuccessMessage, VerseChoreographyAsString);

            return RedirectToAction(nameof(Mine));
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
            await verseChoreographyService.DeleteAsync(model.Id);

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
    }
}
