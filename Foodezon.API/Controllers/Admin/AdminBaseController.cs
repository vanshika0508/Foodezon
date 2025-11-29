using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Foodezon.Api.Controllers.Admin
{
    public abstract class AdminBaseController : Controller
    {
        private const string SessionAdminKey = "IsAdmin";

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            var val = context.HttpContext.Session.GetInt32(SessionAdminKey);
            if (!val.HasValue || val.Value != 1)
            {
                context.Result = new RedirectToActionResult("Login", "AdminAuth", new {area = ""});
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}