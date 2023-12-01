using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class ConsultaTutController : Controller
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
        public ActionResult ListadoTut()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbTut> listado = new List<tbTut>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_Tut.Instancia.ConsultaTut();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

     

        [HttpPost]
        public ActionResult ListadoTut(DateTime FechaEmision)
        {
            string Fecha = FechaEmision.ToString("yyyyMMdd");
           // NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbTut> listado = new List<tbTut>();
            //Compania.ToUpper();
             listado = CD_Tut.Instancia.ConsultaTutPorFecha(Fecha);
            //if (listado.Count==0)
            //{
            //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
            //}
            return View(listado);
        }
      
        [HttpGet]
        public JsonResult CargaDetalleTutDatos(Int16 NumeroTut, string Ato, string Ano)
        {
            tbTut DetalleTut = new tbTut();

            if (Session["Usuario"] == null)
                return Json(DetalleTut, JsonRequestBehavior.AllowGet);

            try
            {
                if (NumeroTut > 0)
                {
                    DetalleTut = CD_Tut.Instancia.ConsultaTutPorSecuencia(NumeroTut, Ato,Ano);

                    return Json(DetalleTut, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleTut, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleTut, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
      
      
    }
}