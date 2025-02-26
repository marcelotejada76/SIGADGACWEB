using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class MatriculasAdeudadasController : Controller
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
        public ActionResult ListadoMatriculasAdeudadas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbMatriculasAdeudadas> listado = new List<tbMatriculasAdeudadas>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_MatriculasAdeudadas.Instancia.DetalleMatriculas();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

     

        [HttpPost]
        public ActionResult ListadoMatriculasAdeudadas(string NombreCompania)
        {
            NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbMatriculasAdeudadas> listado = new List<tbMatriculasAdeudadas>();
            //Compania.ToUpper();
             listado = CD_MatriculasAdeudadas.Instancia.DetalleMatriculasDeudorasCompania(NombreCompania);
            if (listado.Count==0)
            {
                listado = CD_MatriculasAdeudadas.Instancia.DetalleMatriculasDeudorasMatricula(NombreCompania);
            }
            return View(listado);
        }
      
        [HttpGet]
        public JsonResult CargaDetalleMatricula(Int32 OidMatricula)
        {
            tbMatriculasAdeudadas DetalleMatricula = new tbMatriculasAdeudadas();

            if (Session["Usuario"] == null)
                return Json(DetalleMatricula, JsonRequestBehavior.AllowGet);

            try
            {
                if (OidMatricula > 0)
                {
                    DetalleMatricula = CD_MatriculasAdeudadas.Instancia.DetalleMatriculasOid(OidMatricula);

                    return Json(DetalleMatricula, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleMatricula, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleMatricula, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
      
      
    }
}