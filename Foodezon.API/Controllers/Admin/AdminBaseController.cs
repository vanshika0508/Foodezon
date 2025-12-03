using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Foodezon.Api.Controllers.Admin
{
    public abstract class AdminBaseController : Controller
    {
        private const string SessionAdminKey = "IsAdmin";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}