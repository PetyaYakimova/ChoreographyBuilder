using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Position;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
    public class PositionController : AdminBaseController
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllPositionsQueryModel query)
        {
            var model = await positionService.AllPositionsAsync(
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
            var model = new PositionFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PositionFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await positionService.AddPositionAsync(model);

            TempData[UserMessageSuccess] = string.Format(ItemAddedSuccessMessage, PositionAsString);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [PositionExists]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            await positionService.ChangePositionStatusAsync(id);

            TempData[UserMessageSuccess] = String.Format(ChangedStatusSuccessMessage, PositionAsString);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [PositionExists]
        [PositionNotUsedInFigures]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await positionService.GetPositionByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        [PositionExists]
        [PositionNotUsedInFigures]
        public async Task<IActionResult> Edit(int id, PositionFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await positionService.EditPositionAsync(id, model);

            TempData[UserMessageSuccess] = String.Format(ItemUpdatedSuccessMessage, PositionAsString);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [PositionExists]
        [PositionNotUsedInFigures]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await positionService.GetPositionForDeleteAsync(id);

            return View(model);
        }

        [HttpPost]
        [PositionExists]
        [PositionNotUsedInFigures]
        public async Task<IActionResult> Delete(PositionForPreviewViewModel model)
        {
            await positionService.DeleteAsync(model.Id);

            TempData[UserMessageSuccess] = String.Format(ItemDeletedSuccessMessage, PositionAsString);

            return RedirectToAction(nameof(All));
        }
    }
}
