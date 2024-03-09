using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.VerseType;
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
		public async Task<IActionResult> All()
		{
			//Check that user is admin
			var model = await verseTypeService.AllVerseTypesAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			//Check that user is admin
			var model = new VerseTypeFormViewModel();

			return View(model);
		}

		[HttpPost]
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
        public async Task<IActionResult> ChangeStatus(int id)
        {
			//Check that user is admin
			try
			{
				await verseTypeService.ChangeVerseTypeStatusAsync(id);

                return RedirectToAction(nameof(All));
            }
			catch (ArgumentNullException)
			{
				return BadRequest();
			}
        }
    }
}
