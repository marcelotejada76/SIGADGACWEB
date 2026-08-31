using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class CambioUsuarioOperacionesController : Controller
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
        public ActionResult ListadoUsuarios()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbUsuario> listado = new List<tbUsuario>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            if (SesionUsuario.CodigoUsuario=="MTEJADA" || SesionUsuario.CodigoUsuario == "XCHICAIZA")
            {
                listado = CD_Usuario._instancia.UsuarioOperaciones();
            }
            else
            {
                listado = CD_Usuario._instancia.UsuarioOperaciones(SesionUsuario.CodigoUsuario);
            }
           
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoUsuarios(string Usuario)
        {
            Usuario = Usuario.ToUpper();
            // NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbUsuario> listado = new List<tbUsuario>();
            //Compania.ToUpper();
            listado = CD_Usuario.Instancia.UsuarioOperaciones(Usuario);
            //if (listado.Count==0)
            //{
            //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
            //}
            return View(listado);
        }

        //[HttpGet]
        //public JsonResult CargaDetalleUsuario(string CodigoUsuario)
        //{
        //    tbUsuario DetalleUsuario = new tbUsuario();

        //    if (Session["Usuario"] == null)
        //        return Json(DetalleUsuario, JsonRequestBehavior.AllowGet);

        //    try
        //    {
        //        if (CodigoUsuario != "")
        //        {
        //            DetalleUsuario = CD_Usuario.Instancia.ObtenerUsuarioPorCodigo(CodigoUsuario);

        //            return Json(DetalleUsuario, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            return Json(DetalleUsuario, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(DetalleUsuario, JsonRequestBehavior.AllowGet);
        //        throw ex;
        //    }

        //}

        public ActionResult DetalleUsuario(string CodigoUsuario)
        {
            CodigoUsuario = CodigoUsuario.ToUpper();
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            var DetalleUsuario = CD_Usuario.Instancia.ObtenerUsuarioPorCodigo(CodigoUsuario);
            return View(DetalleUsuario);
        }

        [HttpPost]
        public JsonResult ActualizarUsuario(tbUsuario oUsuario)
        {
            

            bool respuesta = false;
            string message = string.Empty;
            try
            {
                if (Session["Usuario"] == null)
                {
                    return Json(new { Success = false, Message = "Sesión expirada" }, JsonRequestBehavior.AllowGet);
                }

                var ousuario = (tbUsuario)Session["Usuario"];
                oUsuario.UsuarioModificacion = ousuario.CodigoUsuario;

                respuesta = CD_Usuario.Instancia.ActualizarDatosUsuario(oUsuario);

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