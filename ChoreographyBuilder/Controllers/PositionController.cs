using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Core.Services;
using Microsoft.AspNetCore.Authorization;
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
		public async Task<IActionResult> All([FromQuery]AllPositionsQueryModel query)
		{
			//Check that user is admin
			var model = await positionService.AllPositionsAsync(
                query.SearchItem,
                query.CurrentPage,
                query.ItemsPerPage);

            query.TotalItemCount = model.TotalCount;
            query.Positions = model.Positions;

			return View(query);
		}

		[HttpGet]
        public IActionResult Add()
        {
            //Check that user is admin
            var model = new PositionFormViewModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(PositionFormViewModel model)
        {
            //Check that user is admin
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await positionService.AddPositionAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            //Check that user is admin
            try
            {
                await positionService.ChangePositionStatusAsync(id);

                return RedirectToAction(nameof(All));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }
    }
}
