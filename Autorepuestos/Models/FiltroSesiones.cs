using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Autorepuestos.Models
{
    public class FiltroSesiones : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                context.HttpContext.Response.Redirect("/Home/Index"); //Va sin punto para que llegue al index
            }
            base.OnActionExecuting(context);
        }
    }
}
