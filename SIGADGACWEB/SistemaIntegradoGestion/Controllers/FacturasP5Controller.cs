using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class FacturasP5Controller : Controller
    {
        /// <summary>
        /// cambio por github
        /// </summary>
        private static tbUsuario SesionUsuario;
        // GET: SolicitarModificaciones
        //public ActionResult AfectacionPresupuestaria()
        //{
        //    if (Session["Usuario"] == null)
        //        return RedirectToAction("login", "Login");

        //    return View();
        //}

        /// <summary>
        /// Accion gf
        /// </summary>
        /// <returns></returns>
        public ActionResult AsignacionRecursos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }
        public ActionResult ListadoFacturasP5()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbFacturasP5> listado = new List<tbFacturasP5>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            
            listado = CD_FacturasP5.Instancia.DetalleFacturasP5();
            return View(listado);
        }
        [HttpPost]
        public ActionResult ListadoFacturasP5(string NombreCliente)
        {
        
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            var FacturaP5Consulta = CD_FacturasP5.Instancia.DetalleFacturasP5Cliente(NombreCliente);
            return View(FacturaP5Consulta);
        }
        public ActionResult DetalleFacturasP5(Int32 OidFactura)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            var FacturaP5Consulta = CD_FacturasP5.Instancia.DetalleFacturasRucP5(OidFactura);
            return View(FacturaP5Consulta);
        }
        //buscar por nombre cliente
        public ActionResult DetalleFacturasP5Cliente(string NombreCliente)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            var FacturaP5Consulta = CD_FacturasP5.Instancia.DetalleFacturasP5Cliente(NombreCliente);
            return View(FacturaP5Consulta);
        }

        [HttpGet]
        public JsonResult CargaDetalleFacturaP5(Int32 OidFactura)
        {
            tbFacturasP5 DetalleFacturaP5 = new tbFacturasP5();
            
            if (Session["Usuario"] == null)
                return Json(DetalleFacturaP5, JsonRequestBehavior.AllowGet);

            try
            {
                if (OidFactura > 0)
                {
                    DetalleFacturaP5 = CD_FacturasP5.Instancia.DetalleFacturasRucP5(OidFactura);
                    
                    return Json(DetalleFacturaP5, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleFacturaP5, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleFacturaP5, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
    }
}