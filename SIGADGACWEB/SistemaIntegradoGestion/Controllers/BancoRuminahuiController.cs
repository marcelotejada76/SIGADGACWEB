using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class BancoRuminahuiController : Controller
    {
        /// <summary>
        /// cambio por github
        /// </summary>
        private static tbUsuario SesionUsuario;
        // GET: SolicitarModificaciones
        public ActionResult AfectacionPresupuestaria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }

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
        public ActionResult ListadoBancoRuminahui()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbBancoRuminahui> listado = new List<tbBancoRuminahui>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_BancoRuminahui.Instancia.DetalleDepositos();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

     

        [HttpPost]
        public ActionResult ListadoBancoRuminahui(string Comprobante)
        {
            Comprobante = Comprobante.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbBancoRuminahui> listado = new List<tbBancoRuminahui>();
            //Compania.ToUpper();
             listado = CD_BancoRuminahui.Instancia.DetalleDepositoComprobante(Comprobante);
            if (listado.Count==0)
            {
                listado = CD_BancoRuminahui.Instancia.DetalleDepositoFecha(Comprobante);
                if (listado.Count == 0)
                {
                    listado = CD_BancoRuminahui.Instancia.DetalleDepositante (Comprobante);
                }
            }
            return View(listado);
        }
      
        [HttpGet]
        public JsonResult CargaDetalleBanco(string FechaDeposito, string NumeroComprobante)
        {
            tbBancoRuminahui DetalleDepsoito = new tbBancoRuminahui();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (NumeroComprobante !="")
                {
                    DetalleDepsoito = CD_BancoRuminahui.Instancia.DetalleDepositoPorFecha(FechaDeposito, NumeroComprobante);

                    return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
      
      
    }
}