using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.VerseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
	[Authorize]
	public class VerseTypeController : Controller
	{
		private IVerseTypeService verseTypeService;

		public VerseTypeController(IVerseTypeService verseTypeService)
		{
			this.verseTypeService = verseTypeService;
		}

		public async Task<IActionResult> All()
		{
			var model = await verseTypeService.AllVerseTypesAsync();

			return View(model);
		}
	}
}
