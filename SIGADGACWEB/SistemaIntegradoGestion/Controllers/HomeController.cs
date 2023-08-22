using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class HomeController : Controller
    {
        private static tbUsuario SesionUsuario;
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
                SesionUsuario = (tbUsuario)Session["Usuario"];
            else
            {
                SesionUsuario = new tbUsuario();
            }
            try
            {
                ViewBag.NombreUsuario = SesionUsuario.NombresUsuario + " " + SesionUsuario.ApellidosUsuario;
                
                //    ViewBag.RolUsuario = SesionUsuario.oRol.DescripcionRol;
                
                
            }
            catch
            {

            }
          

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            Session.Remove("Usuario");
            Session["name"] = null;
            Session.Remove("name");
            Session["correo"] = null;
            Session.Remove("correo");
            return RedirectToAction("login", "Login");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}