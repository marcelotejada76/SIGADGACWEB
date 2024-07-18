using CapaDatos;
using CapaModelo;
using SistemaIntegradoGestion.Utilitarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SistemaCargaDepositos.Controllers
{
    public class DepositosFinancieros : Controller
    {
        /// <summary>
        /// cambio por github
        /// </summary>
        //private static string ssrsurl = ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToString();
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


        public ActionResult CargaDepositosClientes()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSubirDepositos> listado = new List<tbSubirDepositos>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);

            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            Session["Controlador"] = "DepositosFinancieros";
            Session["ActionResul"] = "SubirDocumentosDepositos";
            Session["CodigoRol"] = SesionUsuario.CodigoRol;
            //listado = CD_SolicitudPOA.Instancia.SolicitudModificacionSinAfectacionPresupetariaListarDireccionAnio(SesionUsuario.CodigoSubsistema, cAnio);
            ////Verifica que si ya tiene subido los archivos
            //foreach (var item in listado)
            //{
            //    string direccionDirectory = item.CodigoDireccionPYGE + @"\" + item.TipoSolicitud + @"\" + item.AnioSolicitud + @"\" + item.NumeroSolicitud;
            //    string pathDirectorio = Constantes.poaURL + @"\" + direccionDirectory;
            //    if (GetVerifcaExisteArchivosDirectorio(pathDirectorio))
            //        item.numeroDocumentoAdjunto = 1;
            //    else
            //        item.numeroDocumentoAdjunto = 0;
            //}
            return View(listado);
        }

        public ActionResult SubirDocumentosDepositos(string canio, int numSolicitud, string vista)
        {
            string direccionDirectory = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            SesionUsuario = (tbUsuario)Session["Usuario"];
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);

            direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
            Session["ActionResul"] = vista;
            Session["DireccionPath"] = direccionDirectory;
            ViewBag.DireccionDirectory = direccionDirectory;
            oSolicitudPoa.CodigoRolPYGE = SesionUsuario.CodigoRol;
           // oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
            if (oSolicitudPoa.oModelArchivo.Count() > 0)
            {
                oSolicitudPoa.numeroDocumentoAdjunto = 1;
            }


            return View(oSolicitudPoa);
        }

        [HttpPost]
        public ActionResult SubirDocumentosHabilitantes(HttpPostedFileBase documentFile, string AnioSolicitud, int NumeroSolicitud, string vista)
        {
            string direccionDirectory = string.Empty;
            string nombreArchivo = string.Empty;
            ViewBag.mensajeError = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            SesionUsuario = (tbUsuario)Session["Usuario"];
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(AnioSolicitud, NumeroSolicitud);
            direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + AnioSolicitud + @"\" + NumeroSolicitud;
            Session["DireccionPath"] = direccionDirectory;
            ViewBag.DireccionDirectory = direccionDirectory;
            oSolicitudPoa.CodigoRolPYGE = SesionUsuario.CodigoRol;

            if (documentFile != null && documentFile.ContentLength > 0)
            {
                string urlDocumentos = Constantes.poaURL + @"\" + direccionDirectory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlDocumentos))
                    System.IO.Directory.CreateDirectory(urlDocumentos);


                nombreArchivo = documentFile.FileName;
                if (!System.IO.File.Exists(urlDocumentos + @"\" + nombreArchivo))
                {
                    documentFile.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));
                }
                else
                {
                    ViewBag.mensajeError = "El archivo ya existe no puede actualizar, cambie de nombre";
                }

            }

          //  oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
            if (oSolicitudPoa.oModelArchivo.Count() > 0)
            {
                oSolicitudPoa.numeroDocumentoAdjunto = 1;
            }

            return View(oSolicitudPoa);

        }


    }
}