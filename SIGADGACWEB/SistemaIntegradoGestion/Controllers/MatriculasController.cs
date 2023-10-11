using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class MatriculasController : Controller
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
        public ActionResult ListadoMatriculaP5()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbMatriculas> listado = new List<tbMatriculas>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_Matriculas.Instancia.DetalleMatriculas();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

     

        [HttpPost]
        public ActionResult ListadoMatriculaP5(string NombreCompania)
        {

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbMatriculas> listado = new List<tbMatriculas>();
            //Compania.ToUpper();
             listado = CD_Matriculas.Instancia.DetalleMatriculasP5Compania(NombreCompania.ToUpper());
            if (listado.Count==0)
            {
                listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania.ToUpper());
            }
            return View(listado);
        }
      
        [HttpGet]
        public JsonResult CargaDetalleMatriculaP5(Int32 OidMatricula)
        {
            tbMatriculas DetalleMatriculaP5 = new tbMatriculas();

            if (Session["Usuario"] == null)
                return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);

            try
            {
                if (OidMatricula > 0)
                {
                    DetalleMatriculaP5 = CD_Matriculas.Instancia.DetalleMatriculaP5(OidMatricula);

                    return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
      
      
    }
}