using ChoreographyBuilder.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ChoreographyBuilder.Attributes
{
	public class VerseChoreographyIsLastInFullChoreographyAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			IFullChoreographyVerseChoreographyService? service = context.HttpContext.RequestServices.GetService<IFullChoreographyVerseChoreographyService>();

			if (service == null)
			{
				context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			object? value = context.HttpContext.GetRouteData().Values["id"];
			if (value == null)
			{
				context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
			}

			if (value != null)
			{
				int id = 0;
				if (int.TryParse(value.ToString(), out id))
				{
					if (service != null && service.VerseChoreographyIsLastForFullChoreographyByIdAsync(id).Result == false)
					{
						context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
					}
				}
				else
				{
					context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
				}
			}
		}
	}
}
