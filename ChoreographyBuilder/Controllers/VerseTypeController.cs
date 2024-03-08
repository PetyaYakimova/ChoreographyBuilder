using ChoreographyBuilder.Core.Contracts;
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
			var model = await verseTypeService.AllVerseTypesAsync();

			return View(model);
		}
	}
}
