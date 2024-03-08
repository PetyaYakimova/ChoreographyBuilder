using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ChoreographyBuilder.Controllers
{
	public class FullChoreographyController : BaseController
	{
		public IActionResult All()
		{
			return View();
		}
	}
}
