using ChoreographyBuilder.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ChoreographyBuilder.Attributes
{
	public class FigureOptionExistsForThisUserAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			IFigureOptionService? service = context.HttpContext.RequestServices.GetService<IFigureOptionService>();

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
					if (service != null && service.FigureOptionExistForThisUserByIdAsync(id, context.HttpContext.User.Id()).Result == false)
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
