using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class TotalesRecordController : Controller
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
        public ActionResult ListadoTotalesRecord()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbTotalesRecord> listado = new List<tbTotalesRecord>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_TotalRecord.Instancia.ConsultaTotalesRecord();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoTotalesRecord(DateTime Fecha)

        {
            string FechaEmision = Fecha.ToString("yyyyMMdd");
            

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbTotalesRecord> listado = new List<tbTotalesRecord>();

            //tbAtc listado = new tbAtc();
            //Compania.ToUpper();
            listado = CD_TotalRecord.Instancia.TotalesPorFecha(FechaEmision);
            //if (listado.Count == 0)
            //{
            //    listado = CD_Controlador.Instancia.ControladorLicenciaApellido(Licencia);
            //    //if (listado.Count == 0)
            //    //{
            //    //    listado = CD_BancoRuminahui.Instancia.DetalleDepositante(Licencia);
            //    //}
            //}
            return View(listado);
        }

    }
}