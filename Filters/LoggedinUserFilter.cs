using System.Text.Json;
using ASP.NET_Core_Login.Models;
using ASP.NET_Core_Login.Keys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NET_Core_Login.Filters;
public class LoggedinUserFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string? session = context.HttpContext.Session.GetString("userCheckIn");

        if (string.IsNullOrEmpty(session))
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        }
        else
        {
            Users? user = JsonSerializer.Deserialize<Users>(session);

            if (user == null || user.UserStats != UsersStatsEnum.ENABLE)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        }

        base.OnActionExecuted(context);
    }
}