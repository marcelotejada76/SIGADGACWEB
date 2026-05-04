using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepositosCompanias.Controllers
{
    public class HomeController : Controller
    {
        private static tbUsuario SesionUsuario;
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                return RedirectToAction("login", "Login");
            }
            else
            {
                SesionUsuario = new tbUsuario();
                return RedirectToAction("SolicitudVuelo", "VueloPrivado");

            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}