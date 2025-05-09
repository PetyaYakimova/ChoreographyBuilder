﻿using ChoreographyBuilder.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChoreographyBuilder.Attributes;

public class PositionNotUsedInFiguresAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        IPositionService? positionService = context.HttpContext.RequestServices.GetService<IPositionService>();

        if (positionService == null)
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
                if (positionService != null && positionService.IsPositionUsedInFiguresAsync(id).Result)
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
