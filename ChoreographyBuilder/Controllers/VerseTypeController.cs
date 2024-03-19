using ChoreographyBuilder.Core.Contracts;
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
		[AutoValidateAntiforgeryToken] // Add this to every method that is a form
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
