using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.VerseType;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
	public class VerseTypeController : BaseController
    {
        private IVerseTypeService verseTypeService;

        public VerseTypeController(IVerseTypeService verseTypeService)
        {
            this.verseTypeService = verseTypeService;
        }

        [HttpGet]
		//Check that user is admin
		public async Task<IActionResult> All([FromQuery] AllVerseTypesQueryModel query)
        {
            var model = await verseTypeService.AllVerseTypesAsync(
                query.SearchTerm,
                query.SearchBeats,
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
            var model = new VerseTypeFormViewModel();

            return View(model);
        }

        [HttpPost]
		//Check that user is admin
		public async Task<IActionResult> Add(VerseTypeFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await verseTypeService.AddVerseTypeAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        //Check that user is admin
        [VerseTypeExists]
		public async Task<IActionResult> ChangeStatus(int id)
        {
            await verseTypeService.ChangeVerseTypeStatusAsync(id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
		//Check that user is admin
		[VerseTypeExists]
		[VerseTypeNotUsedInChoreographies]
		public async Task<IActionResult> Edit(int id)
        {
            var model = await verseTypeService.GetVerseTypeById(id);

            return View(model);
        }

        [HttpPost]
		//Check that user is admin
		[VerseTypeExists]
        [VerseTypeNotUsedInChoreographies]
		public async Task<IActionResult> Edit(int id, VerseTypeFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await verseTypeService.EditVerseTypeAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        //Check that user is admin
        [VerseTypeExists]
        [VerseTypeNotUsedInChoreographies]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await verseTypeService.GetVerseTypeForDeleteAsync(id);

            return View(model);
        }

        [HttpPost]
        //Check that user is admin
        [VerseTypeExists]
        [VerseTypeNotUsedInChoreographies]
        public async Task<IActionResult> Delete(VerseTypeForPreviewViewModel model)
        {
            await verseTypeService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
