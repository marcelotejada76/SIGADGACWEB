using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class AeronaveComponenteController : Controller
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
        public ActionResult ListadoMatriculas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbAeronavesComponentes> listado = new List<tbAeronavesComponentes>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_AeronaveComponentes.Instancia.DetalleDocumentos();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }


        [HttpPost]
        public ActionResult ListadoMatriculas(string Matricula)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbAeronavesComponentes> listado = new List<tbAeronavesComponentes>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            if (Matricula != "")
            {
                listado = CD_AeronaveComponentes.Instancia.DetalleDocumentosMatricula(Matricula);
            }
            else
            {
                listado = CD_AeronaveComponentes.Instancia.DetalleDocumentos();
            }
            
            
            return View(listado);
        }



        [HttpGet]
        public JsonResult CargaDetalleAeronave(string Aeronave)
        {
            Aeronave = Aeronave.ToUpper().TrimStart().TrimEnd();

            tbAeronavesComponentes DetalleDepsoito = new tbAeronavesComponentes();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (Aeronave != "")
                {
                    DetalleDepsoito = CD_AeronaveComponentes.Instancia.DetalleDocumentosClave(Aeronave);

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


        //[HttpPost]
        //public JsonResult ActualizarMatricula(tbActualizacionMatriculas modalMatricula)
        //{
        //    bool respuesta = false;
        //    string message = string.Empty;
        //    try
        //    {
        //        var ousuario = (tbUsuario)Session["Usuario"];
        //        modalMatricula.UsuarioModificacion = ousuario.CodigoUsuario;
        //        respuesta = CD_ActualizacionMatriculas.Instancia.ActualizarDatosMatricula(modalMatricula);

        //        //respuesta = CD_TalentoHumano.Instancia.MaestroPersonalActualizarDatosAdicionales(modalMatricula);
        //        if (respuesta)
        //            message = "El registro se grabo correctamente";
        //        else
        //            message = "No se puedo guardar el registro";
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta = false;
        //        message = "Error. " + ex.Message;
        //    }

        //    return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);


        //}


        [HttpPost]
        public JsonResult ActualizarMatricula(tbActualizacionMatriculas modalMatricula)
        {
            bool respuesta = false;
            string message = string.Empty;
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                modalMatricula.UsuarioModificacion = ousuario.CodigoUsuario;

                respuesta = CD_ActualizacionMatriculas.Instancia.ActualizarDatosMatricula(modalMatricula);
                respuesta = CD_ActualizacionMatriculas.Instancia.ActualizarDatosMatricula550(modalMatricula);

                if (respuesta)
                    message = "✅ El registro se guardó correctamente.";
                else
                    message = "⚠️ No se pudo guardar el registro.";
            }
            catch (Exception ex)
            {
                respuesta = false;
                message = "❌ Error: " + ex.Message;
            }

            return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);
        }



    }
}