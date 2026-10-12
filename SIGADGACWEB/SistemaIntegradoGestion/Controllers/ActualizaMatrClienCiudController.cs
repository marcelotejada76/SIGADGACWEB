using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class ActualizaMatrClienCiudController : Controller
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

        public ActionResult Datos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult CargaDetalle()
        {
            bool respuesta = false;
            string message = string.Empty;

            if (Session["Usuario"] == null)
                return Json(new { Success = false, Message = "La sesión ha caducado. Inicie sesión nuevamente." }, JsonRequestBehavior.AllowGet);

            try
            {
                respuesta = CD_ActualizaMatClienCiud.Instancia.ActualizarDatosMatricula();

                if (respuesta)
                    message = "El proceso de actualización de datos finalizó correctamente.";
                else
                    message = "No se pudo completar el proceso de actualización.";
            }
            catch (Exception ex)
            {
                respuesta = false;
                message = "Error en el servidor: " + ex.Message;
            }

            return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult ListadoMatriculaP5(string NombreCompania)
        //{

        //    if (Session["Usuario"] == null)
        //        return RedirectToAction("login", "Login");

        //    List<tbMatriculas> listado = new List<tbMatriculas>();
        //    //Compania.ToUpper();
        //     listado = CD_Matriculas.Instancia.DetalleMatriculasP5Compania(NombreCompania.ToUpper());
        //    if (listado.Count==0)
        //    {
        //        listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania.ToUpper());
        //    }
        //    return View(listado);
        //}

        //[HttpGet]
        //public JsonResult CargaDetalleMatriculaP5(Int32 OidMatricula)
        //{
        //    tbMatriculas DetalleMatriculaP5 = new tbMatriculas();

        //    if (Session["Usuario"] == null)
        //        return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);

        //    try
        //    {
        //        if (OidMatricula > 0)
        //        {
        //            DetalleMatriculaP5 = CD_Matriculas.Instancia.DetalleMatriculaP5(OidMatricula);

        //            return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
        //        throw ex;
        //    }

        //}


    }
}