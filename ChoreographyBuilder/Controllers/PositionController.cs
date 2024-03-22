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
        public async Task<IActionResult> All([FromQuery] AllPositionsQueryModel query)
        {
            //Check that user is admin
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
            if (!await DoesPositionExist(id))
            {
                return BadRequest();
            }

            await positionService.ChangePositionStatusAsync(id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Check that user is admin
            if (!await DoesPositionExistAndIsNotUsedForFigures(id))
            {
                return BadRequest();
            }

            var model = await positionService.GetPositionByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, PositionFormViewModel model)
        {
            //Check that user is admin
            if (!await DoesPositionExistAndIsNotUsedForFigures(id))
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await positionService.EditPositionAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        private async Task<bool> DoesPositionExist(int positionid)
        {
            var position = await positionService.GetPositionByIdAsync(positionid);
            if (position == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> DoesPositionExistAndIsNotUsedForFigures(int positionId)
        {
            if (!await DoesPositionExist(positionId))
            {
                return false;
            }

            if (await positionService.IsPositionUsedInFiguresAsync(positionId))
            {
                return false;
            }

            return true;
        }
    }
}
