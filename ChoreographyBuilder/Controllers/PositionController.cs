﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChoreographyBuilder.Controllers
{
	public class PositionController : BaseController
	{
		public IActionResult All()
		{
			return View();
		}
	}
}
