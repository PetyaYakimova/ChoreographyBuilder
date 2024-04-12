using ChoreographyBuilder.Attributes;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.VerseType;
using Microsoft.AspNetCore.Mvc;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Areas.Admin.Controllers
{
	public class VerseTypeController : AdminBaseController
	{
		private readonly IVerseTypeService verseTypeService;

		public VerseTypeController(IVerseTypeService verseTypeService)
		{
			this.verseTypeService = verseTypeService;
		}

		[HttpGet]
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
		public IActionResult Add()
		{
			var model = new VerseTypeFormViewModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(VerseTypeFormViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await verseTypeService.AddVerseTypeAsync(model);

			TempData[UserMessageSuccess] = string.Format(ItemAddedSuccessMessage, VerseTypeAsString);

			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		[VerseTypeExists]
		public async Task<IActionResult> ChangeStatus(int id)
		{
			await verseTypeService.ChangeVerseTypeStatusAsync(id);

			TempData[UserMessageSuccess] = string.Format(ChangedStatusSuccessMessage, VerseTypeAsString);

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		[VerseTypeExists]
		[VerseTypeNotUsedInChoreographies]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await verseTypeService.GetVerseTypeById(id);

			return View(model);
		}

		[HttpPost]
		[VerseTypeExists]
		[VerseTypeNotUsedInChoreographies]
		public async Task<IActionResult> Edit(int id, VerseTypeFormViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await verseTypeService.EditVerseTypeAsync(id, model);

			TempData[UserMessageSuccess] = string.Format(ItemUpdatedSuccessMessage, VerseTypeAsString);

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		[VerseTypeExists]
		[VerseTypeNotUsedInChoreographies]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await verseTypeService.GetVerseTypeForDeleteAsync(id);

			return View(model);
		}

		[HttpPost]
		[VerseTypeExists]
		[VerseTypeNotUsedInChoreographies]
		public async Task<IActionResult> Delete(VerseTypeForPreviewViewModel model)
		{
			await verseTypeService.DeleteVerseTypeAsync(model.Id);

			TempData[UserMessageSuccess] = string.Format(ItemDeletedSuccessMessage, VerseTypeAsString);

			return RedirectToAction(nameof(All));
		}
	}
}
