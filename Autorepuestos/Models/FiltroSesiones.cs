using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Autorepuestos.Models
{
    public class FiltroSesiones : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                context.Cancel = true;
                context.HttpContext.Response.Redirect("/Home/Index"); //Va sin punto para que llegue al index
            }
            base.OnResultExecuting(context);
        }
    }
}
