using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class GarantiasController : Controller
    {
        private static tbUsuario SesionUsuario;
        // GET: SolicitarModificaciones
        public ActionResult AfectacionPresupuestaria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }
        public ActionResult AsignacionRecursos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }
        public ActionResult ListadoGarantias()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbGarantias> listado = new List<tbGarantias>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
           // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_Garantias.Instancia.DetalleGarantias();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

        public ActionResult DetalleGarantia( string Ruc)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            var GarantiaConsulta = CD_Garantias.Instancia.DetalleGarantiaRuc(Ruc);
            return View(GarantiaConsulta);
        }
    }
}