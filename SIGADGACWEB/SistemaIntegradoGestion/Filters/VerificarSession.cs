using CapaModelo;
using SistemaIntegradoGestion.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            tbUsuario oUsuario = (tbUsuario)HttpContext.Current.Session["Usuario"];

            if (oUsuario == null)
            {
                if (filterContext.Controller is LoginController == false)
                {
                    // Al asignar un Result, se corta la ejecución del Action y se redirige inmediatamente.
                    filterContext.Result = new RedirectResult("~/Login/login");
                    return;
                }
            }
            else
            {
                if (filterContext.Controller is LoginController == true)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}