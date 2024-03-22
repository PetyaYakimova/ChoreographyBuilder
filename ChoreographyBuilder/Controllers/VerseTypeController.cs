using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Core.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> All([FromQuery] AllVerseTypesQueryModel query)
        {
            //Check that user is admin
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
        public IActionResult Add()
        {
            //Check that user is admin
            var model = new VerseTypeFormViewModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(VerseTypeFormViewModel model)
        {
            //Check that user is admin
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await verseTypeService.AddVerseTypeAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            //Check that user is admin
            if (!await DoesVerseTypeExist(id))
            {
                return BadRequest();
            }

            await verseTypeService.ChangeVerseTypeStatusAsync(id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Check that user is admin
            if (!await DoesVerseTypeExistAndIsNotUsedForChoreographies(id))
            {
                return BadRequest();
            }

            var model = await verseTypeService.GetVerseTypeById(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, VerseTypeFormViewModel model)
        {
            //Check that user is admin
            if (!await DoesVerseTypeExistAndIsNotUsedForChoreographies(id))
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await verseTypeService.EditVerseTypeAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        private async Task<bool> DoesVerseTypeExist(int verseTypeId)
        {
            var verseType = await verseTypeService.GetVerseTypeById(verseTypeId);
            if (verseType == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> DoesVerseTypeExistAndIsNotUsedForChoreographies(int verseTypeId)
        {
            if (!await DoesVerseTypeExist(verseTypeId))
            {
                return false;
            }

            if (await verseTypeService.IsVerseTypeUsedInChoreographiesAsync(verseTypeId))
            {
                return false;
            }

            return true;
        }
    }
}
