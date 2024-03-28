using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Position;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
	public class PositionController : BaseController
    {
        private IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }

        [HttpGet]
		//Check that user is admin
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
		//Check that user is admin
		public IActionResult Add()
        {
            var model = new PositionFormViewModel();

            return View(model);
        }

        [HttpPost]
		//Check that user is admin
		public async Task<IActionResult> Add(PositionFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await positionService.AddPositionAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
		//Check that user is admin
		[PositionExists]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            await positionService.ChangePositionStatusAsync(id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
		//Check that user is admin
		[PositionExists]
        [PositionNotUsedInFigures]
		public async Task<IActionResult> Edit(int id)
        {
            var model = await positionService.GetPositionByIdAsync(id);

            return View(model);
        }

        [HttpPost]
		//Check that user is admin
		[PositionExists]
		[PositionNotUsedInFigures]
		public async Task<IActionResult> Edit(int id, PositionFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await positionService.EditPositionAsync(id, model);

            return RedirectToAction(nameof(All));
        }

		[HttpGet]
		//Check that user is admin
		[PositionExists]
		[PositionNotUsedInFigures]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await positionService.GetPositionForDeleteAsync(id);

			return View(model);
		}

		[HttpPost]
		//Check that user is admin
		[PositionExists]
		[PositionNotUsedInFigures]
		public async Task<IActionResult> Delete(PositionForPreviewViewModel model)
		{
			await positionService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(All));
		}
	}
}
