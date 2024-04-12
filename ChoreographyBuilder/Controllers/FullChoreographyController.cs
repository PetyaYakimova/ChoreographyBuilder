using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.FullChoreography;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Controllers
{
	public class FullChoreographyController : BaseController
    {
        private readonly IFullChoreographyService fullChoreographyService;
        private readonly IVerseChoreographyService verseChoreographyService;
        private readonly IFullChoreographyVerseChoreographyService fullChoreographyVerseChoreographyService;

        public FullChoreographyController(IFullChoreographyService fullChoreographyService, IVerseChoreographyService verseChoreographyService, IFullChoreographyVerseChoreographyService fullChoreographyVerseChoreographyService)
        {
            this.fullChoreographyService = fullChoreographyService;
            this.verseChoreographyService = verseChoreographyService;
            this.fullChoreographyVerseChoreographyService = fullChoreographyVerseChoreographyService;
        }

        [HttpGet]
        public async Task<IActionResult> Mine([FromQuery] AllFullChoreographiesQueryModel query)
        {
            var model = await fullChoreographyService.AllUserFullChoreographiesAsync(
                User.Id(),
                query.SearchTerm,
                query.CurrentPage,
                query.ItemsPerPage);

            query.TotalItemCount = model.TotalCount;
            query.Entities = model.Entities;

            return View(query);
        }

        [HttpGet]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> Details(int id)
        {
            var model = await fullChoreographyService.GetChoreographyDetailsByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new FullChoreographyFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FullChoreographyFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            int fullChoreographyId = await fullChoreographyService.AddFullChoreographyAsync(model, User.Id());

            TempData[UserMessageSuccess] = String.Format(ItemAddedSuccessMessage, FullChoreographyAsString);

            return RedirectToAction(nameof(Details), new { Id = fullChoreographyId });
        }

        [HttpGet]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await fullChoreographyService.GetChoreographyForEditByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> Edit(int id, FullChoreographyFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await fullChoreographyService.EditFullChoreographyAsync(id, model);

            TempData[UserMessageSuccess] = String.Format(ItemUpdatedSuccessMessage, FullChoreographyAsString);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await fullChoreographyService.GetFullChoreographyForDeleteAsync(id);

            return View(model);
        }

        [HttpPost]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> Delete(FullChoreographyTableViewModel model)
        {
            await fullChoreographyService.DeleteFullChoreographyAsync(model.Id);

            TempData[UserMessageSuccess] = String.Format(ItemDeletedSuccessMessage, FullChoreographyAsString);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> AddVerseChoreography(int id)
        {
            var model = new FullChoreographyVerseChoreographyFormViewModel();
            model.VerseChoreographyOrder = (await fullChoreographyService.GetNumberOfVerseChoreographiesForFullChoreographyAsync(id)) + 1;
            PositionForPreviewViewModel? lastVerseChoreographyEndPosition = await fullChoreographyService.GetLastVerseChoreographyEndPositionAsync(id);
            model.VerseChoreographies = await GetAllUserVerseChoreographiesWithStartPositionAsync(lastVerseChoreographyEndPosition?.Id);
            model.StartPositionName = lastVerseChoreographyEndPosition?.Name;

            return View(model);
        }

        [HttpPost]
        [FullChoreographyExistsForThisUser]
        public async Task<IActionResult> AddVerseChoreography(FullChoreographyVerseChoreographyFormViewModel model, int id)
        {
            bool verseChoreographyExists = await verseChoreographyService.VerseChoreographyExistForThisUserByIdAsync(model.VerseChoreographyId, User.Id());
            if (!verseChoreographyExists)
            {
                ModelState.AddModelError(nameof(model.VerseChoreographyId), VerseChoreographyDoesntExistErrorMessage);
            }

            int nextAvailableOrder = (await fullChoreographyService.GetNumberOfVerseChoreographiesForFullChoreographyAsync(id)) + 1;

            if (nextAvailableOrder != model.VerseChoreographyOrder)
            {
                ModelState.AddModelError(nameof(model.VerseChoreographyOrder), InvalidVerseChoreographyOrderErrorMessage);
            }

            if (ModelState.IsValid == false)
            {
                model.VerseChoreographyOrder = nextAvailableOrder;
                PositionForPreviewViewModel? lastVerseChoreographyEndPosition = await fullChoreographyService.GetLastVerseChoreographyEndPositionAsync(id);
                model.VerseChoreographies = await GetAllUserVerseChoreographiesWithStartPositionAsync(lastVerseChoreographyEndPosition?.Id);
                model.StartPositionName = lastVerseChoreographyEndPosition?.Name;

                return View(model);
            }

            await fullChoreographyVerseChoreographyService.AddVerseChoreographyToFullChoreographyAsync(id, model);

            TempData[UserMessageSuccess] = String.Format(ItemAddedSuccessMessage, VerseChoreographyAsString);

            return RedirectToAction(nameof(Details), new { Id = id });
        }

        [HttpGet]
        [VerseChoreographyInFullChoreographyExistsForThisUser]
        [VerseChoreographyIsLastInFullChoreography]
        public async Task<IActionResult> DeleteVerseChoreography(int id)
        {
            var model = await fullChoreographyVerseChoreographyService.GetVerseChoreographyForDeleteAsync(id);

            return View(model);
        }

        [HttpPost]
        [VerseChoreographyInFullChoreographyExistsForThisUser]
        [VerseChoreographyIsLastInFullChoreography]
        public async Task<IActionResult> DeleteVerseChoreography(FullChoreographyVerseChoreographyDeleteViewModel model)
        {
            await fullChoreographyVerseChoreographyService.DeleteVerseChoreographyFromFullChoreographyAsync(model.Id);

            TempData[UserMessageSuccess] = String.Format(ItemDeletedSuccessMessage, VerseChoreographyAsString);

            return RedirectToAction(nameof(Details), new { Id = model.FullChoreographyId });
        }

        private async Task<IEnumerable<VerseChoreographyTableViewModel>> GetAllUserVerseChoreographiesWithStartPositionAsync(int? startPositionId = null)
        {
            return await verseChoreographyService.AllUserVerseChoreographiesStartingWithPositionAsync(User.Id(), startPositionId);
        }
    }
}
