using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ChoreographyBuilder.Controllers
{
	[Authorize]
	public class FullChoreographyController : Controller
	{
		public IActionResult All()
		{
			return View();
		}
	}
}
