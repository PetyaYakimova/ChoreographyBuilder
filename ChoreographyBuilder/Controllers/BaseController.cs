using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{

	}
}
