using CapaDatos;
using CapaModelo;
using SistemaIntegradoGestion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Pkcs;
using iTextSharp.text.pdf.security;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using SistemaIntegradoGestion.Utilitario;
using SistemaIntegradoGestion.Utilitarios;
using System.Text;
using iTextSharp.text;
using System.Data;
using System.Globalization;

namespace SistemaIntegradoGestion.Controllers
{

    public class SolicitarModificacionesController : Controller
    {
        private static string SesionControlador = "SolicitarModificaciones";
        private static string ssrsurl = ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToString();
        private static tbUsuario SesionUsuario;
        private static tbMenu SesionMenu;        

        #region "Certificado POA"

        /// <summary>
        /// Controlado Carga todas las solictudes de certificados POA 
        /// </summary>
        /// <returns></returns>
        public ActionResult SolicitarCertificadoPOA()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = ""; // oSistema.FechaSistema.Substring(0, 4);

            listado = CD_SolicitudPOA.Instancia.SolicitarCertificadoSoloPOA(cAnio, SesionUsuario.CodigoSubsistema);
            Session["ActionResul"] = "SolicitarCertificadoPOA";
            Session["TituloActionResul"] = "Solicitar Certificación POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();

            foreach (var item in listado)
            {
                string direccionDirectory = @"\" + item.CodigoDireccionPYGE + @"\" + item.AnioSolicitud + @"\" + item.TipoSolicitud + @"\" + item.NumeroSolicitud;
                item.numeroDocumentoAdjunto = DocumentacionExiste(direccionDirectory);
            }

            return View(listado);
        }

        public ActionResult EnviarSolicitarCertificadoPOA()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);

            listado = CD_SolicitudPOA.Instancia.SolicitarCertificadoSoloPOA(cAnio, SesionUsuario.CodigoSubsistema);
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            Session["Rol"] = SesionUsuario.CodigoRol;
            Session["ActionResul"] = actionName;
            Session["Controlador"] = controllerName;
            Session["TituloActionResul"] = "Enviar Solicitud Certificación POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();

            return View(listado);
        }


        public ActionResult AprobarSolicitudCertificacionPOA()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);

            listado = CD_SolicitudPOA.Instancia.SolicitarCertificadoSoloPOA(cAnio, SesionUsuario.CodigoSubsistema);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Session["Controlador"] = controllerName;
            Session["ActionResul"] = "SolicitarCertificadoPOA";           
            Session["TituloActionResul"] = "Solicitar Certificación POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();

            return View(listado);
        }

        public ActionResult RevisarAprobarSolicitudCertificado()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = ""; // oSistema.FechaSistema.Substring(0, 4);

            listado = CD_SolicitudPOA.Instancia.RevisarAutualizarSolicitudCertificadoPOA(cAnio);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Session["Controlador"] = controllerName;
            Session["ActionResul"] = "RevisarAprobarSolicitudCertificado";
            Session["TituloActionResul"] = "Revisar o Aprobar Solicitud Certificación POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            Session["CodigoSubsistema"] = SesionUsuario.CodigoSubsistema.Trim().ToUpper();
            Session["CodigoRol"] = SesionUsuario.CodigoRol;

            return View(listado);
        }

        public ActionResult RevisarSolicitudCertificado()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = ""; // oSistema.FechaSistema.Substring(0, 4);

            listado = CD_SolicitudPOA.Instancia.RevisarAutualizarSolicitudCertificadoPOA(cAnio);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Session["Controlador"] = controllerName;
            Session["ActionResul"] = "RevisarSolicitudCertificado";
            Session["TituloActionResul"] = "Revisar Solicitud Certificación POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            Session["CodigoSubsistema"] = SesionUsuario.CodigoSubsistema.Trim().ToUpper();
            Session["CodigoRol"] = SesionUsuario.CodigoRol;

            return View(listado);
        }

        //public SelectList ListarTipoSolicitud()
        //{

        //}

        #endregion

        #region "Solicitar Modificaciones"
        public ActionResult AfectacionPresupuestaria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_SolicitudPOA.Instancia.SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MOD");
            Session["ActionResul"] = "AfectacionPresupuestaria";
            Session["TituloActionResul"] = "Afectación presupuestaria";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();

            return View(listado);
        }
        public ActionResult AsignacionRecursos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_SolicitudPOA.Instancia.SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MAR");
            Session["ActionResul"] = "AsignacionRecursos";
            Session["TituloActionResul"] = "Asignación recursos";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            return View(listado);
        }
        public ActionResult ReprogramarSoloPOA()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_SolicitudPOA.Instancia.SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            ViewBag.DescripcionSubSistema = SesionUsuario.DescripcionSubSistema;
            Session["ActionResul"] = "ReprogramarSoloPOA";
            Session["TituloActionResul"] = "Reprogramar solo POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            return View(listado);
        }
        #endregion
        // GET: SolicitarModificaciones

        public ActionResult DocumentoHabilitante(string cdireccion, string canio, string tipoSolicitud, string numSolicitud, string estadoAutorizado, string vista)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            SesionUsuario = (tbUsuario)Session["Usuario"];
            List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            string direccionDirectory = @"\" + cdireccion + @"\" + canio + @"\" + tipoSolicitud + @"\" + numSolicitud;
            listArchivo = GetObtenerTodosArchivos(direccionDirectory);
            ViewBag.DireccionDirectory = direccionDirectory;

            var oSolicitudPOA = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, int.Parse(numSolicitud));
            Session["DireccionActividad"] = oSolicitudPOA.DescripcionActividadEjecutar;
            ViewBag.EstadoAutorizado = oSolicitudPOA.EstadoAutorizacion;
            ViewBag.canio = canio;
            ViewBag.numSolcitud = numSolicitud;
            Session["ActionResul"] = vista;
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            return View(listArchivo);
        }

        [HttpPost]
        public ActionResult DocumentoHabilitante(HttpPostedFileBase documentFile, string Directory, string canio, string numSolicitud)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            string nombreArchivo = "";
            ViewBag.DireccionDirectory = Directory;
            var oSolicitudPOA = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, int.Parse(numSolicitud));
            ViewBag.EstadoAutorizado = oSolicitudPOA.EstadoAutorizacion;
            ViewBag.canio = canio;
            ViewBag.numSolcitud = numSolicitud;

            List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            if (documentFile != null && documentFile.ContentLength > 0)
            {
                string urlDocumentos = Constantes.poaURL + Directory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlDocumentos))
                    System.IO.Directory.CreateDirectory(urlDocumentos);

                nombreArchivo = documentFile.FileName;
                documentFile.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));
            }
            listArchivo = GetObtenerTodosArchivos(Directory);
            return View(listArchivo);
        }

        private List<tbModelArchivo> GetObtenerTodosArchivos(string directory)
        {
            string carpetaPoa = string.Empty;
            string urlDocumentos = string.Empty;
            List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();

            if (directory.Length > 0)
            {
                string direccionDirectory = directory;

                ViewBag.DireccionDirectory = direccionDirectory;

                urlDocumentos = Constantes.poaURL + direccionDirectory;

                if (!System.IO.Directory.Exists(urlDocumentos))
                    System.IO.Directory.CreateDirectory(urlDocumentos);

                if (System.IO.Directory.Exists(urlDocumentos))
                {
                    string[] files = System.IO.Directory.GetFiles(urlDocumentos);

                    if (files.Length > 0)
                    {
                        for (int iFile = 0; iFile < files.Length; iFile++)
                        {
                            tbModelArchivo archvo = new tbModelArchivo();

                            archvo.NombreArchivo = new FileInfo(files[iFile]).Name;
                            archvo.FechaModificacion = new FileInfo(files[iFile]).LastWriteTime.ToString();
                            archvo.Tipo = new FileInfo(files[iFile]).Extension;
                            archvo.Tamano = new FileInfo(files[iFile]).Length.ToString() + " bytes";
                            archvo.Directorio = direccionDirectory;
                            listArchivo.Add(archvo);
                        }

                    }

                }
                else
                {
                    listArchivo = null;
                }


            }
            return listArchivo;
        }

        [HttpGet]
        public JsonResult CargaSolicitudCertificado(string canio, string numSolicitud)
        {
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, Int32.Parse(numSolicitud));

            //GenerarCertificadoPOA(canio, Int32.Parse(numSolicitud));
            return Json(oSolicitud, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RevisaSolicitudCertificadoPOA(string canio, int numSolicitud, string estadoAprobacion, string observacion, string observacion1, string observacion2)
        {
            bool respuesta;
            string errorMensaje = string.Empty;
            string fullPath = string.Empty;
            try
            {
                if (Session["Usuario"] != null)
                {
                    var ousuario = (tbUsuario)Session["Usuario"];
                    respuesta = CD_SolicitudPOA.Instancia.RevisaSolicitudCertificadoPOA(canio, numSolicitud, ousuario.CodigoUsuario, estadoAprobacion, observacion, observacion1, observacion2);
                }
                else
                {
                    respuesta = false;

                }
            }
            catch (Exception ex)
            {
                respuesta = false;

            }
            return Json(new { resultado = respuesta, mensaje = errorMensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CargaTodosArchivos(string cdireccion, string canio, string tipoSolicitud, string numSolicitud)
        {
            List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            string direccionDirectory = @"\" + cdireccion + @"\" + canio + @"\" + tipoSolicitud + @"\" + numSolicitud;
            listArchivo = GetObtenerTodosArchivos(direccionDirectory);

            return Json(listArchivo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CargaTodosArchivosDirectory(string direccionDirectory)
        {
            List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            listArchivo = GetObtenerTodosArchivos(direccionDirectory);
            return Json(listArchivo, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult EliminarDocumento(string nombreArchivo, string direccion)
        {
            bool respuesta;
            string fullPath = string.Empty;
            if (Session["Usuario"] != null)
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                fullPath = Constantes.poaURL + direccion + @"\" + nombreArchivo;
                respuesta = EliminaArchivoServidor(fullPath);
            }
            else
            {
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        private bool EliminaArchivoServidor(string path)
        {
            if (!System.IO.File.Exists(path)) return false;

            try //Maybe error could happen like Access denied or Presses Already User used
            {
                System.IO.File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }

        public ActionResult DownloadFile(string nombreArchivo, string direccion)
        {
            try
            {
                string fullName = Constantes.poaURL + direccion + @"\" + nombreArchivo;
                byte[] fileBytes = GetFile(fullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nombreArchivo);

            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("No se pudo presentar el archivo solicitado.");
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un problema al descargar el archivo.");
            }

        }


        public ActionResult GeneraReporteToPDF(string canio, Int32 numSolicitud, string cdireccion, string estAut, string cobservacion, string cobservacion1)
        {

            //Byte  
            Warning[] warnings;
            string[] streamids;
            string FilePathReturn = string.Empty;
            string FileNameFirmado = string.Empty;
            string mimeType, encoding, filenameExtension;
            byte[] bytes;
            ReportViewer viewer = new ReportViewer();
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                SesionMenu = (tbMenu)Session["MenuMaster"];

                if (CD_SolicitudPOA.Instancia.ApruebaSolicitudCertificadoPOA(canio, numSolicitud, estAut, ousuario.CodigoUsuario, cobservacion, cobservacion1))
                {
                    viewer.ProcessingMode = ProcessingMode.Remote;
                    viewer.SizeToReportContent = true;
                    viewer.AsyncRendering = true;
                    viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                    viewer.ServerReport.ReportPath = "/" + SesionMenu.DescripcionServidorReport + "/CertificadoPOA";
                    ReportParameter[] reportParameter = new ReportParameter[2];
                    reportParameter[0] = new ReportParameter("AñoSolicitud", canio);
                    reportParameter[1] = new ReportParameter("NoSolictud", numSolicitud.ToString());
                    viewer.ServerReport.SetParameters(reportParameter);

                    //viewer.ServerReport.Refresh();

                    string urlReporteElectronico = Constantes.poaURL + cdireccion;
                    //Verifica si existe la carpeta creada si no lo crear
                    if (!System.IO.Directory.Exists(urlReporteElectronico))
                        System.IO.Directory.CreateDirectory(urlReporteElectronico);

                    bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    //File  
                    if (bytes.Length > 0)
                    {
                        string FileName = "CertificadoPOA" + canio + numSolicitud.ToString() + ".pdf";
                        FileNameFirmado = "CertificadoPOA" + canio + numSolicitud.ToString() + "-signed.pdf";
                        string FilePath = urlReporteElectronico + @"\" + FileName;

                        
                        if (InsertaCertificadoDocumentoFirmado(FileName, FileNameFirmado, cdireccion, 1))
                        {
                            EliminaArchivoServidor(FilePath);
                        }
                    }

                    //return file path  
                    FilePathReturn = FileNameFirmado;
                }
                else { FilePathReturn = ""; }
            }
            catch (Exception ex)
            {
                FilePathReturn = "";
            }
            return Content(FilePathReturn);

        }


        public ActionResult VisualizarFile(string nombreArchivo, string direccion)
        {

            string fullName = Constantes.poaURL + direccion + @"\" + nombreArchivo;

            byte[] fileBytes = GetFile(fullName);

            return new FileContentResult(fileBytes, "application/pdf");
        }

        public ActionResult GeneraReporteToPDFRespado(string canio, Int32 numSolicitud, string cdireccion, string estAut, string cobservacion, string cobservacion1)
        {

            //Byte  
            Warning[] warnings;
            string[] streamids;
            string FilePathReturn = string.Empty;
            string FileNameFirmado = string.Empty;
            string mimeType, encoding, filenameExtension;
            byte[] bytes;
            ReportViewer viewer = new ReportViewer();
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                SesionMenu = (tbMenu)Session["MenuMaster"];
                if (CD_SolicitudPOA.Instancia.ApruebaSolicitudCertificadoPOA(canio, numSolicitud, estAut, ousuario.CodigoUsuario, cobservacion, cobservacion1))
                {
                    viewer.ProcessingMode = ProcessingMode.Remote;
                    viewer.SizeToReportContent = true;
                    viewer.AsyncRendering = true;
                    viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                    viewer.ServerReport.ReportPath = "/" + SesionMenu.DescripcionServidorReport + "/CertificadoPOA";
                    ReportParameter[] reportParameter = new ReportParameter[2];
                    reportParameter[0] = new ReportParameter("AñoSolicitud", canio);
                    reportParameter[1] = new ReportParameter("NoSolictud", numSolicitud.ToString());
                    viewer.ServerReport.SetParameters(reportParameter);

                    //viewer.ServerReport.Refresh();

                    string urlReporteElectronico = Constantes.poaURL + cdireccion;
                    //Verifica si existe la carpeta creada si no lo crear
                    if (!System.IO.Directory.Exists(urlReporteElectronico))
                        System.IO.Directory.CreateDirectory(urlReporteElectronico);

                    bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    //File  
                    if (bytes.Length > 0)
                    {
                        string FileName = "CertificadoPOA" + canio + numSolicitud.ToString() + ".pdf";
                        FileNameFirmado = "CertificadoPOA" + canio + numSolicitud.ToString() + "-signed.pdf";
                        string FilePath = urlReporteElectronico + @"\" + FileName;

                        //create and set PdfReader  
                        PdfReader reader = new PdfReader(bytes);
                        FileStream output = new FileStream(FilePath, FileMode.Create);

                        string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                        //create and set PdfStamper  
                        PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);

                        if (Agent.Contains("Firefox"))
                            pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                        else
                            pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                        pdfStamper.FormFlattening = false;
                        pdfStamper.Close();
                        reader.Close();

                        if (InsertaCertificadoDocumentoFirmado(FileName, FileNameFirmado, cdireccion, 1))
                        {
                            EliminaArchivoServidor(FilePath);
                        }
                    }

                    //return file path  
                    FilePathReturn = FileNameFirmado;
                }
                else { FilePathReturn = ""; }
            }
            catch (Exception ex)
            {
                FilePathReturn = "";
            }
            return Content(FilePathReturn);

        }


        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        /// <summary>
        /// Genera el reporte del Certificado POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="cdireccion"></param>
        /// <param name="estAut"></param>
        /// <param name="cobservacion"></param>
        /// <param name="cobservacion1"></param>
        /// <returns></returns>
        public ActionResult ExportToPDF(string canio, Int32 numSolicitud, string cdireccion, string estAut, string cobservacion, string cobservacion1)
        {

           
            string FilePathReturn = string.Empty;
            string FileNameFirmado = string.Empty;
           
            
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            Document dctoCertificado = new Document();
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];

                if (CD_SolicitudPOA.Instancia.ApruebaSolicitudCertificadoPOA(canio, numSolicitud, estAut, ousuario.CodigoUsuario, cobservacion, cobservacion1))
                {
                    oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                    oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                    if (oSolicitud.NumeroSolicitud > 0)
                    {
                        string urlReporteElectronico = Constantes.poaURL + cdireccion;
                        //Verifica si existe la carpeta creada si no lo crear
                        if (!System.IO.Directory.Exists(urlReporteElectronico))
                            System.IO.Directory.CreateDirectory(urlReporteElectronico);
                        
                        string FileName = "CertificadoPOA" + "_" + oSolicitud.CodigoDireccionPYGE.Trim() + "_" + oSolicitud.AnioSolicitud + "_" + oSolicitud.TipoSolicitud + "_" + oSolicitud.NumeroSolicitud.ToString() + ".pdf";
                        FileNameFirmado =  "CertificadoPOA" + "_" + oSolicitud.CodigoDireccionPYGE.Trim() + "_" + oSolicitud.AnioSolicitud + "_" + oSolicitud.TipoSolicitud + "_" + oSolicitud.NumeroSolicitud.ToString() + "-signed.pdf";
                        string FilePath = urlReporteElectronico + @"\" + FileName;

                        dctoCertificado = GenerarCertificadoPOA(canio, numSolicitud);
                        if (dctoCertificado.Left > 0)
                        {
                            if (oCertificado.TipoArchivo.Equals("A"))
                            {
                                if (InsertaCertificadoDocumentoFirmado(FileName, FileNameFirmado, cdireccion, 1))
                                {
                                    EliminaArchivoServidor(FilePath);
                                }
                                //if (InsertaFirmaCertificadoPOADocumento(FileName, FileNameFirmado, cdireccion, 1))
                                //{
                                //    EliminaArchivoServidor(FilePath);
                                //}


                            }
                            else
                            {
                                FileNameFirmado = FileName;
                            }
                        }
                    }
                  
                    FilePathReturn = FileNameFirmado;
                }
                else { FilePathReturn = ""; }
            }
            catch (Exception ex)
            {
                FilePathReturn = "";
            }
            return Content(FilePathReturn);

        }

        public ActionResult ExportaServerReportAPDF(string canio, Int32 numSolicitud)
        {
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            
            string cdireccion = string.Empty;
            string FilePathReturn = string.Empty;
            string FileNameFirmado = string.Empty;           
            string nombreArchivo = string.Empty;
            string inicionombreArchivo = string.Empty;          
            string nombreReporte = string.Empty;
            Document dctoSolicitud = new Document();
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                if (oSolicitud.NumeroSolicitud > 0)
                {
                    
                    cdireccion = oSolicitud.CodigoDireccionPYGE.Trim() + @"\" + oSolicitud.AnioSolicitud + @"\" + oSolicitud.TipoSolicitud + @"\" + oSolicitud.NumeroSolicitud.ToString();
                    nombreArchivo = "SolicitudCertificadoPOA" + "_" + oSolicitud.CodigoDireccionPYGE.Trim() + "_" + oSolicitud.AnioSolicitud + "_" + oSolicitud.TipoSolicitud + "_" + oSolicitud.NumeroSolicitud.ToString();
                    string urlReporteElectronico = Constantes.poaURL + @"\" + cdireccion;

                    string FileName = nombreArchivo + ".pdf";
                    FileNameFirmado = nombreArchivo + "-signed.pdf";
                    string FilePath = urlReporteElectronico + @"\" + FileName;
                    dctoSolicitud = GenerarSolicitudCertificadoPOA(canio, numSolicitud);
                    if (dctoSolicitud.Left > 0)
                    {
                        dctoSolicitud.CloseDocument();
                        if (oCertificado.TipoArchivo.Equals("A"))
                        {
                            if (InsertaCertificadoDocumentoFirmado(FileName, FileNameFirmado, cdireccion, 1))
                            {
                                FilePathReturn = FileNameFirmado;
                                EliminaArchivoServidor(FilePath);
                            }
                        }
                        else
                        {
                            FileNameFirmado = FileName;
                        }
                    }
                   
                                      
                    FilePathReturn = FileNameFirmado;

                }
                else
                {
                    FilePathReturn = "";
                }
            }
            catch (Exception ex)
            {
                FilePathReturn = "";
            }
            return Content(FilePathReturn);

        }

        public ActionResult ExportaServerReportAPDFRespaldo(string canio, Int32 numSolicitud)
        {
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            //Byte          
            Warning[] warnings;
            string[] streamids;
            string cdireccion = string.Empty;
            string FilePathReturn = string.Empty;
            string FileNameFirmado = string.Empty;
            string mimeType, encoding, filenameExtension;
            string nombreArchivo = string.Empty;
            string inicionombreArchivo = string.Empty;
            byte[] bytes;
            ReportParameter[] reportParameter;
            ReportViewer viewer = new ReportViewer();
            string nombreReporte = string.Empty;

            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                SesionMenu = (tbMenu)Session["MenuMaster"];
                oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                if (oSolicitud.NumeroSolicitud > 0)
                {
                    viewer.ProcessingMode = ProcessingMode.Remote;
                    viewer.SizeToReportContent = true;
                    viewer.AsyncRendering = true;
                    viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                    viewer.ServerReport.ReportPath = "/" + SesionMenu.DescripcionServidorReport + "/SolicitudCertificadoPOA";
                    reportParameter = new ReportParameter[2];
                    reportParameter[0] = new ReportParameter("AñoSolicitud", canio);
                    reportParameter[1] = new ReportParameter("NoSolictud", numSolicitud.ToString());
                    viewer.ServerReport.SetParameters(reportParameter);
                    // viewer.ServerReport.Refresh();


                    cdireccion = oSolicitud.CodigoDireccionPYGE.Trim() + @"\" + oSolicitud.AnioSolicitud + @"\" + oSolicitud.TipoSolicitud + @"\" + oSolicitud.NumeroSolicitud.ToString();
                    nombreArchivo = "SolicitudCertificadoPOA" + "_" + oSolicitud.CodigoDireccionPYGE.Trim() + "_" + oSolicitud.AnioSolicitud + "_" + oSolicitud.TipoSolicitud + "_" + oSolicitud.NumeroSolicitud.ToString();
                    string urlReporteElectronico = Constantes.poaURL + @"\" + cdireccion;

                    if (!System.IO.Directory.Exists(urlReporteElectronico))
                        System.IO.Directory.CreateDirectory(urlReporteElectronico);

                    bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    if (bytes.Length > 0)
                    {
                        string FileName = nombreArchivo + ".pdf";
                        FileNameFirmado = nombreArchivo + "-signed.pdf";
                        string FilePath = urlReporteElectronico + @"\" + FileName;

                        //create and set PdfReader  
                        PdfReader reader = new PdfReader(bytes);
                        FileStream output = new FileStream(FilePath, FileMode.Create);

                        string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                        //create and set PdfStamper  
                        PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);

                        if (Agent.Contains("Firefox"))
                            pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                        else
                            pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                        pdfStamper.FormFlattening = false;
                        pdfStamper.Close();
                        reader.Close();
                        output.Close();
                        if (oCertificado.TipoArchivo.Equals("A"))
                        {
                            if (InsertaCertificadoDocumentoFirmado(FileName, FileNameFirmado, cdireccion, 1))
                            {
                                EliminaArchivoServidor(FilePath);
                            }
                        }
                        else
                        {
                            FileNameFirmado = FileName;
                        }

                    }

                    //return file path  
                    FilePathReturn = FileNameFirmado;

                }
                else
                {
                    FilePathReturn = "";
                }
            }
            catch (Exception ex)
            {
                FilePathReturn = "";
            }
            return Content(FilePathReturn);

        }

        #region "Certificado Firma"

        private bool ValidaCertificadoContrasenaCorrecta(HttpPostedFileBase subirArchivo, string contrasena)
        {
            bool estado = false;
            string path = string.Empty;
            try
            {
                if (subirArchivo != null)
                {
                    string ruta = Server.MapPath("~/Content/CertificadoDigital");

                    // Si el directorio no existe, crearlo
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    path = Path.Combine(Server.MapPath("~/Content/CertificadoDigital"), Path.GetFileName(subirArchivo.FileName));
                    if (!System.IO.File.Exists(path))
                    {
                        subirArchivo.SaveAs(path);
                    }


                    // Cargar el certificado
                    Pkcs12Store store = new Pkcs12Store(new FileStream(path, FileMode.Open, FileAccess.Read), contrasena.ToCharArray());
                    string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));
                    if (alias == null)
                    {
                        estado = false;
                    }
                    else
                    {
                        estado = true;
                    }

                }

            }
            catch (Exception ex)
            {

                estado = false;
            }
            return estado;
        }

        private List<X509CertificateEntry> CertificadoAlias(string pathArchivo, string contrasena)
        {
            X509CertificateEntry[] chain = null;
            try
            {
                if (pathArchivo != null)
                {

                    // Cargar el certificado
                    Pkcs12Store store = new Pkcs12Store(new FileStream(pathArchivo, FileMode.Open, FileAccess.Read), contrasena.ToCharArray());
                    string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));

                    AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    chain = store.GetCertificateChain(alias);
                    chain.Select(c => c.Certificate).ToList();

                }

            }
            catch (Exception ex)
            {
                chain = null;
            }
            return chain.ToList();
        }
        public ActionResult CrearCertificado()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            var ousuario = (tbUsuario)Session["Usuario"];

            tbCertificadoDigital model = new tbCertificadoDigital();
            //var oCertificado = 
            model = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioUnRegistro(ousuario.CodigoUsuario);
            if (CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListarDelegar(ousuario.CodigoUsuario).Count > 0)
            {
                model.CodigoUsuario = model.CodigoUsuario;
                model.Secuencial = model.Secuencial;
                model.OidCertificadoPadre = model.OidCertificadoPadre;
            }
            else if (model != null)
            {
                model.Secuencial = model.Secuencial;
                model.OidCertificadoPadre = model.OidCertificadoPadre;
            }


            model.Contrasena = "";  //SeguridadEncriptar.Encriptar(model.Contrasena);

            model.CodigoUsuario = ousuario.CodigoUsuario;
            model.NombresApellidos = ousuario.NombreCorto;
            model.Cargo = ousuario.Cargo;
            model.Ruc = ousuario.CedulaUsuario;
            model.Contrasena = "";
            ViewBag.Vista = Session["ActionResul"].ToString();
            ViewBag.Controlador = Session["Controlador"].ToString();
            return View(model);
        }

        [HttpPost]
        public ActionResult CrearCertificado(tbCertificadoDigital model, HttpPostedFileBase subirArchivo, string pvista)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            bool estado = false;
            try
            {

                var ousuario = (tbUsuario)Session["Usuario"];
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                string nombreArchivo = "";
                if (model.TipoArchivo.Equals("A"))
                {
                    if (subirArchivo != null && subirArchivo.ContentLength > 0)
                    {
                        string urlDocumentos = Utilitarios.Utilitario.certificadoPOAUrl + model.CodigoUsuario + @"\";
                        //Verifica si existe la carpeta creada si no lo crear
                        if (!System.IO.Directory.Exists(urlDocumentos))
                            System.IO.Directory.CreateDirectory(urlDocumentos);
                        if ((model.CodigoUsuario == ousuario.CodigoUsuario) && (model.Secuencial == 0))
                        {
                            if (!CD_CertificadoDigital.Instancia.VerificaExistaCertificadoDigital(model.CodigoUsuario))
                            {
                                nombreArchivo = Path.GetFileName(subirArchivo.FileName);
                                if (!ValidaCertificadoFirmaExiste(model.CodigoUsuario, nombreArchivo))
                                {
                                    if (ValidaCertificadoContrasenaCorrecta(subirArchivo, model.Contrasena))
                                    {
                                        subirArchivo.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));
                                        subirArchivo.InputStream.Flush();
                                        subirArchivo.InputStream.Close();
                                        subirArchivo.InputStream.Dispose();


                                        var ocertificado = CertificadoAlias(urlDocumentos + nombreArchivo, model.Contrasena);
                                        DateTime _fechaVencimiento = ocertificado[0].Certificate.NotAfter;

                                        //Graba el registro certificado
                                        model.UsuarioCreado = ousuario.CodigoUsuario;
                                        model.FechaCreado = osistema.FechaSistema;
                                        model.HoraCreado = osistema.HoraSistema;
                                        model.FechaSubida = osistema.FechaSistema;
                                        model.FechaVencimiento = _fechaVencimiento.ToString("yyyyMMdd");
                                        model.PathDocumento = nombreArchivo;
                                        model.EstadoCertificado = "AD";
                                        model.Asignado = "AC";
                                        model.Contrasena = SeguridadEncriptar.Encriptar(model.Contrasena);
                                        estado = CD_CertificadoDigital.Instancia.CertificadoDigitalNuevo(model);
                                        if (estado)
                                        {
                                            model.CodigoUsuario = ousuario.CodigoUsuario;
                                            model.NombresApellidos = ousuario.NombreCorto;
                                            model.Cargo = ousuario.Cargo;
                                            model.Ruc = ousuario.CedulaUsuario;
                                            model.Contrasena = string.Empty;
                                            model.oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListar(ousuario.CodigoUsuario);
                                            ViewBag.mensajeError = "El registro se guardo correctamente";
                                        }
                                        else
                                        {
                                            ViewBag.mensajeError = "El registro no pudo grabar.";
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.mensajeError = "El certificado o contraseña no son correctos";
                                    }
                                }
                                else
                                {
                                    if (ValidaCertificadoContrasenaCorrecta(subirArchivo, model.Contrasena))
                                    {
                                        var ocertificado = CertificadoAlias(urlDocumentos + nombreArchivo, model.Contrasena);
                                        DateTime _fechaVencimiento = ocertificado[0].Certificate.NotAfter;

                                        //Graba el registro certificado
                                        model.UsuarioCreado = ousuario.CodigoUsuario;
                                        model.FechaCreado = osistema.FechaSistema;
                                        model.HoraCreado = osistema.HoraSistema;
                                        model.FechaSubida = osistema.FechaSistema;
                                        model.FechaVencimiento = _fechaVencimiento.ToString("yyyyMMdd");
                                        model.PathDocumento = nombreArchivo;
                                        model.EstadoCertificado = "AD";
                                        model.Asignado = "AC";
                                        model.Contrasena = SeguridadEncriptar.Encriptar(model.Contrasena);
                                        estado = CD_CertificadoDigital.Instancia.CertificadoDigitalNuevo(model);
                                        if (estado)
                                        {
                                            model.CodigoUsuario = ousuario.CodigoUsuario;
                                            model.NombresApellidos = ousuario.NombreCorto;
                                            model.Cargo = ousuario.Cargo;
                                            model.Ruc = ousuario.CedulaUsuario;
                                            model.Contrasena = string.Empty;
                                            model.oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListar(ousuario.CodigoUsuario);
                                            ViewBag.mensajeError = "El registro se guardo correctamente";
                                        }
                                        else
                                        {
                                            ViewBag.mensajeError = "El registro no pudo grabar.";
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.mensajeError = "El certificado o contraseña no son correctos";
                                    }

                                    model.oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListar(ousuario.CodigoUsuario);
                                    //ViewBag.mensajeError = "El certificado ya existe no es necesario cargar de nuevo";
                                }

                            }
                            else
                            {
                                model.Contrasena = string.Empty;
                                model.oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListar(ousuario.CodigoUsuario);
                                ViewBag.mensajeError = "El certificado ya esta cargado y vigente";
                            }
                        }
                        else if ((model.CodigoUsuario == ousuario.CodigoUsuario) && (model.Secuencial > 0))
                        {
                            nombreArchivo = Path.GetFileName(subirArchivo.FileName);
                            if (subirArchivo != null)
                            {
                                if (!CD_CertificadoDigital.Instancia.VerificaExistaCertificadoDigital(model.CodigoUsuario))
                                {
                                    if (ValidaCertificadoContrasenaCorrecta(subirArchivo, model.Contrasena))
                                    {
                                        subirArchivo.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));
                                        subirArchivo.InputStream.Flush();
                                        subirArchivo.InputStream.Close();
                                        subirArchivo.InputStream.Dispose();


                                        var ocertificado = CertificadoAlias(urlDocumentos + nombreArchivo, model.Contrasena);
                                        DateTime _fechaVencimiento = ocertificado[0].Certificate.NotAfter;

                                        //Graba el registro certificado
                                        model.UsuarioModificado = ousuario.CodigoUsuario;
                                        model.FechaModificado = osistema.FechaSistema;
                                        model.HoraModificado = osistema.HoraSistema;
                                        model.FechaSubida = osistema.FechaSistema;
                                        model.FechaVencimiento = _fechaVencimiento.ToString("yyyyMMdd");
                                        model.PathDocumento = nombreArchivo;
                                        model.Asignado = "AC";
                                        model.Contrasena = SeguridadEncriptar.Encriptar(model.Contrasena);
                                        estado = CD_CertificadoDigital.Instancia.CertificadoDigitalNuevo(model);
                                        if (estado)
                                        {
                                            model.CodigoUsuario = ousuario.CodigoUsuario;
                                            model.NombresApellidos = ousuario.NombreCorto;
                                            model.Cargo = ousuario.Cargo;
                                            model.Ruc = ousuario.CedulaUsuario;
                                            model.Contrasena = string.Empty;
                                            model.oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListar(ousuario.CodigoUsuario);
                                            ViewBag.mensajeError = "El registro se guardo correctamente";
                                        }
                                        else
                                        {
                                            ViewBag.mensajeError = "El registro no pudo grabar.";
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.mensajeError = "El certificado o contraseña no son correctos";
                                    }
                                }
                                else
                                {
                                    ViewBag.mensajeError = "Ya tiene cargado el certificado de la firma electronica.";
                                }
                            }

                        }
                    }
                }
                else if (model.TipoArchivo.Equals("T"))
                {
                    //Graba el registro certificado
                    model.UsuarioCreado = ousuario.CodigoUsuario;
                    model.FechaCreado = osistema.FechaSistema;
                    model.HoraCreado = osistema.HoraSistema;
                    model.FechaSubida = osistema.FechaSistema;
                    model.FechaVencimiento = "";
                    model.PathDocumento = "";
                    model.EstadoCertificado = "AD";
                    model.Asignado = "AC";
                    model.Contrasena = "";
                    if (!CD_CertificadoDigital.Instancia.VerificaExistaCertificadoDigital(model.CodigoUsuario))
                    {
                        estado = CD_CertificadoDigital.Instancia.CertificadoDigitalNuevo(model);
                        if (estado)
                        {
                            model.CodigoUsuario = ousuario.CodigoUsuario;
                            model.NombresApellidos = ousuario.NombreCorto;
                            model.Cargo = ousuario.Cargo;
                            model.Ruc = ousuario.CedulaUsuario;
                            model.Contrasena = string.Empty;
                            model.oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuarioListar(ousuario.CodigoUsuario);
                            ViewBag.mensajeError = "El registro se guardo correctamente";
                        }
                        else
                        {
                            ViewBag.mensajeError = "El registro no pudo grabar.";
                        }
                    }
                    else
                    {
                        ViewBag.mensajeError = "Ya esta grabado sus datos.";
                    }
                }

                ViewBag.Vista = Session["ActionResul"].ToString();
                ViewBag.Controlador = Session["Controlador"].ToString();
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = ex.Message;
            }

            ViewBag.Vista = pvista;
            return View(model);
        }

        private bool ValidaCertificadoFirmaExiste(string codUsuario, string nombreArchivo)
        {
            bool estado = false;
            try
            {
                string urlDocumentos = Utilitarios.Utilitario.certificadoPOAUrl + codUsuario + @"\" + nombreArchivo;
                //Verifica si existe el archivo
                if (System.IO.File.Exists(urlDocumentos))
                {
                    estado = true;
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }
        

        #endregion

        /// <summary>
        /// Metodo inserta el Certificado al documento firma electronica
        /// </summary>
        /// <param name="inputPdfPath"></param>
        /// <param name="outputPdfPath"></param>
        /// <returns></returns>
        private bool InsertaCertificadoDocumentoFirmado(string inputPdfPath, string outputPdfPath, string directorio, int numPage)
        {
            bool estado = false;
            string pathCertificado = string.Empty;
            string pathAutorizacion = string.Empty;
            string opiedeFirma = string.Empty;
            tbCertificadoDigital ocertificadoDigitalAto = new tbCertificadoDigital();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store store = new Pkcs12Store();
            PdfSignatureAppearance appearance;
            int nsejeX = 0;
            int nsejeY = 0;
            try
            {
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                var ousuario = (tbUsuario)Session["Usuario"];
                pathAutorizacion = Constantes.poaURL + @"\" + directorio;
                pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + ousuario.CodigoUsuario;
              
                var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                store = storeCertificado(oCertificado, ousuario);

                //AsymmetricKeyEntry keyEntry = keyEntryCertificado(oCertificado, ousuario); //store.GetKey(alias);
                if (store != null)
                {
                    string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));
                    AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    X509CertificateEntry[] chain = store.GetCertificateChain(alias);

                    // Cargar el PDF de entrada
                    using (FileStream inputStream = new FileStream(pathAutorizacion + @"\" + inputPdfPath, FileMode.Open))
                    using (FileStream outputStream = new FileStream(pathAutorizacion + @"\" + outputPdfPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        PdfReader reader = new PdfReader(inputStream);                       

                        PdfStamper stamper = PdfStamper.CreateSignature(reader, outputStream, '\0', null, true);
                         appearance = stamper.SignatureAppearance;
                        //PdfSignatureAppearance appearanceAto = stamper.SignatureAppearance;

                        numPage = reader.NumberOfPages;
                        nsejeX = 120; //disminuir para acercar al margen izquierdo y aumenta para alejarlo
                        nsejeY = 5;  //disminuir para acercar al pie de pagina erdo y aumenta para ajejarlo
                        // Configurar la apariencia de la firma

                       
                        string fechaFirma = fechaDateAs400(oSistema.FechaSistema) + " " + oSistema.HoraSistema;  //DateTime.Now.Date.ToString();
                        appearance.SignDate = DateTime.Parse(fechaFirma); // DateTime.Now.Date;


                        var codigoCertificado = chain.Select(c => c.Certificate).ToList();
                        // Crear el objeto de firma
                        IExternalSignature externalSignature = new PrivateKeySignature(keyEntry.Key, "SHA-256");
                        MakeSignature.SignDetached(appearance, externalSignature, chain.Select(c => c.Certificate).ToList(), null, null, null, 0, CryptoStandard.CMS);

                    

                        stamper.Close();

                        estado = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }


        /// <summary>
        /// Metodo inserta el Certificado al documento firma electronica
        /// </summary>
        /// <param name="inputPdfPath"></param>
        /// <param name="outputPdfPath"></param>
        /// <returns></returns>
        private bool InsertaFirmaCertificadoPOADocumento(string inputPdfPath, string outputPdfPath, string directorio, int numPage)
        {
            bool estado = false;
            string pathCertificado = string.Empty;
            string pathAutorizacion = string.Empty;
            string opiedeFirma = string.Empty;
            tbCertificadoDigital ocertificadoDigitalAto = new tbCertificadoDigital();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store store = new Pkcs12Store();
            PdfSignatureAppearance appearance;
            try
            {
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                var ousuario = (tbUsuario)Session["Usuario"];
                pathAutorizacion = Constantes.poaURL + @"\" + directorio;
                pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + ousuario.CodigoUsuario;
                //if (!Directory.Exists(pathAutorizacion))
                //{
                //    Directory.CreateDirectory(pathAutorizacion);
                //}

                var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                store = storeCertificado(oCertificado, ousuario);

                //AsymmetricKeyEntry keyEntry = keyEntryCertificado(oCertificado, ousuario); //store.GetKey(alias);
                if (store != null)
                {
                    string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));
                    AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    X509CertificateEntry[] chain = store.GetCertificateChain(alias);

                    // Cargar el PDF de entrada
                    using (FileStream inputStream = new FileStream(pathAutorizacion + @"\" + inputPdfPath, FileMode.Open))
                    using (FileStream outputStream = new FileStream(pathAutorizacion + @"\" + outputPdfPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        PdfReader reader = new PdfReader(inputStream);
                        //PdfStamper stamper = new PdfStamper(reader, outputStream);

                        PdfStamper stamper = PdfStamper.CreateSignature(reader, outputStream, '\0', null, true);
                        appearance = stamper.SignatureAppearance;

                        //appearance.Layer2Font.Size = 6.0f;
                        //numPage = reader.NumberOfPages;                       
                        // Configurar la apariencia de la firma
                        float x = 50;
                        float y = 100;
                        float width = 200;
                        float height = 690;
                        //appearance.Reason = SignReason;
                        //appearance.Location = SignLocation;
                        string fechaFirma = Utilitarios.Utilitario.Instancia.fechaDateAs400(oSistema.FechaSistema) + " " + oSistema.HoraSistema;  //DateTime.Now.Date.ToString();
                        appearance.SignDate = DateTime.Parse(fechaFirma); // DateTime.Now.Date;
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(x, y, width, height), numPage, null);//.IsInvisible
                                                                                                                          //        Left = 50,//Top = 150,Width = 200, Height = 200 40, 150, 300, 190

                        Font signatureFont = new Font();
                        signatureFont.IsItalic();
                        signatureFont.SetStyle(1);
                        signatureFont.Size = 8;//


                        opiedeFirma = "FIRMADO POR:";

                        //

                        //Custom text and background image
                        appearance.Image = GetQRCode(opiedeFirma + alias + "\n Date: " + fechaFirma);
                        appearance.ImageScale = 0.8f;
                        appearance.Acro6Layers = true;

                        StringBuilder buf = new StringBuilder();

                        buf.Append("\n\n\n\n\n\n\n\n\n\n\n\n\nDocumento firmado electrónicamente\n");
                        String name = alias;
                        string cargo = ousuario.Cargo;
                        buf.Append(name).Append('\n');
                        //buf.Append(cargo).Append('\n');
                        buf.Append("Date: ").Append(fechaFirma);

                        string text = buf.ToString();

                        appearance.Layer2Text = text;
                        appearance.Layer2Font = signatureFont;

                        appearance.Acro6Layers = true;

                        var codigoCertificado = chain.Select(c => c.Certificate).ToList();
                        // Crear el objeto de firma
                        IExternalSignature externalSignature = new PrivateKeySignature(keyEntry.Key, "SHA-256");
                        MakeSignature.SignDetached(appearance, externalSignature, chain.Select(c => c.Certificate).ToList(), null, null, null, 0, CryptoStandard.CMS);

                        stamper.Close();
                        outputStream.Close();
                        estado = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }


        public Document GenerarSolicitudCertificadoPOA(string canio, int numSolicitud)
        {
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            tbSistema oSistema = new tbSistema();
            Document pdfDoc = new Document(PageSize.A4, 40, 40, 40, 40);
            PdfWriter pdfWriter = null;
            string pathSolcitud = string.Empty;
            string directorio = string.Empty;
            string nombreArchivo = string.Empty;
            string pathCertificado = string.Empty;
            BarcodeQRCode barcodeQRCodeFirma;
            Image codeQRImageFirma;
            string alias = string.Empty;
            string fechaFirma = string.Empty;
            Paragraph oPieFirma = new Paragraph();
            Pkcs12Store store = new Pkcs12Store();
            Paragraph paraAsunto = new Paragraph();
            string textoQRCodeFirma = string.Empty;
            var FontColour = new BaseColor(35, 31, 32, 25);
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            iTextSharp.text.Font negrita13 = new iTextSharp.text.Font(bfTimes, 13f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita12 = new iTextSharp.text.Font(bfTimes, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita11 = new iTextSharp.text.Font(bfTimes, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita9 = new iTextSharp.text.Font(bfTimes, 9f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negritaBlack8 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font norma10 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font subTituloE = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna14 = new iTextSharp.text.Font(bfTimes, 14f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna13 = new iTextSharp.text.Font(bfTimes, 13f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna12 = new iTextSharp.text.Font(bfTimes, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna11 = new iTextSharp.text.Font(bfTimes, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font bodyColumna = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(bfTimes, 7f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal8 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal9 = new iTextSharp.text.Font(bfTimes, 9f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal11 = new iTextSharp.text.Font(bfTimes, 11f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal12 = new iTextSharp.text.Font(bfTimes, 12f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                DataSet dsDatosSolicitud = CD_SolicitudPOA.Instancia.SolicitudCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);

                var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                store = storeCertificado(oCertificado, ousuario);

                //AsymmetricKeyEntry keyEntry = keyEntryCertificado(oCertificado, ousuario); //store.GetKey(alias);
                if (store != null)
                {
                    foreach (DataRow dr in dsDatosSolicitud.Tables[0].Rows)
                    {
                       
                        pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + ousuario.CodigoUsuario;
                        directorio = oSolicitud.CodigoDireccionPYGE + @"\" + oSolicitud.AnioSolicitud + @"\" + oSolicitud.TipoSolicitud + @"\" + oSolicitud.NumeroSolicitud.ToString();
                        nombreArchivo = "SolicitudCertificadoPOA" + "_" + oSolicitud.CodigoDireccionPYGE.Trim() + "_" + oSolicitud.AnioSolicitud + "_" + oSolicitud.TipoSolicitud + "_" + oSolicitud.NumeroSolicitud.ToString();

                        pathSolcitud = Constantes.poaURL + @"\" + directorio;

                        //Obtengo l path del servidor donde se va crear el archivo

                        if (!System.IO.Directory.Exists(pathSolcitud))
                            System.IO.Directory.CreateDirectory(pathSolcitud);

                        FileStream file = new FileStream(pathSolcitud + @"\" + nombreArchivo + ".pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        pdfWriter = PdfWriter.GetInstance(pdfDoc, file);

                        pdfDoc.Open();

                        Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Content/imganes/logodgac.jpg"));
                        logo.ScalePercent(35f);
                        logo.SetAbsolutePosition(180, 180);

                        var tbl = new PdfPTable(new float[] { 10f, 80f }) { WidthPercentage = 100f };
                        var c1 = new PdfPCell(logo) { Rowspan = 3 };
                        var c2 = new PdfPCell(new Phrase("DIRECCIÓN GENERAL DE AVIACIÓN CIVIL DEL ECUADOR", negrita10));

                        c1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;                        
                        c2.HorizontalAlignment = PdfPCell.ALIGN_MIDDLE;
                        c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                        c1.Border = 0;
                        c2.Border = 0;
                        tbl.AddCell(c1);
                        tbl.AddCell(c2);
                        pdfDoc.Add(tbl);

                        paraAsunto.Add("");
                        pdfDoc.Add(paraAsunto);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = negrita12;
                        paraAsunto.Add(dr["TituloSolicitud"].ToString());
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paraAsunto);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = negrita12;
                        paraAsunto.Add("Fecha Solicitud: " + fechaTexto(oSolicitud.FechaSolicitud));
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paraAsunto);
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = tituloNormal12;
                        paraAsunto.Add(dr["DescripcionSolicitud1"].ToString() + '\u0022' + dr["Actividad"].ToString() + '\u0022' + dr["DescripcionSolicitud2"].ToString());
                        paraAsunto.Alignment = Element.ALIGN_JUSTIFIED;
                        pdfDoc.Add(paraAsunto);
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = tituloNormal12;
                        paraAsunto.Add("Con sentimientos de distinguida consideración.");
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paraAsunto);
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);


                        fechaFirma = fechaDateAs400(oSistema.FechaSistema) + " " + oSistema.HoraSistema;

                        alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));

                        textoQRCodeFirma = "Firmado por." + alias + "\n Date: " + DateTime.Parse(fechaFirma);

                        barcodeQRCodeFirma = new BarcodeQRCode(textoQRCodeFirma, 1000, 1000, null);
                        codeQRImageFirma = barcodeQRCodeFirma.GetImage();
                        codeQRImageFirma.ScalePercent(8f);
                        codeQRImageFirma.SetAbsolutePosition(80, 70);

                        string documentoAttentamente = "\n\nDocumento firmado electrónicamente" + "\n" + alias + "\n" + DateTime.Parse(fechaFirma);

                        oPieFirma.Font = negrita10;
                        oPieFirma.Add("Atentamente,");
                        oPieFirma.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(oPieFirma);
                        pdfDoc.Add(Chunk.NEWLINE);
                        PdfPTable tableFirma = new PdfPTable(new float[] { 15f, 80f }) { WidthPercentage = 100f };
                        var cell1 = new PdfPCell(codeQRImageFirma);
                        var cell2 = new PdfPCell(new Phrase(documentoAttentamente, negritaBlack8));

                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell1.Border = 0;
                        cell2.Border = 0;

                        tableFirma.AddCell(cell1);
                        tableFirma.AddCell(cell2);
                        pdfDoc.Add(tableFirma);


                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Add(dr["Direccion"].ToString());
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        paraAsunto.Font = tituloNormal12;
                        pdfDoc.Add(paraAsunto);                       
                    }
                    pdfWriter.CloseStream = true;
                    pdfDoc.CloseDocument();
                    pdfDoc.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pdfDoc;
        }


        /// <summary>
        /// Metodo Genera el Certificado POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns>Documento</returns>
        public Document GenerarCertificadoPOA(string canio, int numSolicitud)
        {
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            tbSistema oSistema = new tbSistema();
            Document pdfDoc = new Document(PageSize.A4, 40, 40, 40, 40);
            PdfWriter pdfWriter = null;
            string pathSolcitud = string.Empty;
            string directorio = string.Empty;
            string nombreArchivo = string.Empty;
            string pathCertificado = string.Empty;
            BarcodeQRCode barcodeQRCodeFirma;
            Image codeQRImageFirma;
            string alias = string.Empty;
            string fechaFirma = string.Empty;
            Paragraph oPieFirma = new Paragraph();
            Pkcs12Store store = new Pkcs12Store();
            Paragraph paraAsunto = new Paragraph();
            string textoQRCodeFirma = string.Empty;
            var FontColour = new BaseColor(35, 31, 32, 25);
            string referenciaCertificado = string.Empty;
            string respectoCertificado = string.Empty;
            //CultureInfo cultures = new CultureInfo("en-US");
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            iTextSharp.text.Font negrita13 = new iTextSharp.text.Font(bfTimes, 13f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita12 = new iTextSharp.text.Font(bfTimes, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita11 = new iTextSharp.text.Font(bfTimes, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negrita9 = new iTextSharp.text.Font(bfTimes, 9f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font negritaBlack8 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font norma10 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font subTituloE = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna14 = new iTextSharp.text.Font(bfTimes, 14f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna13 = new iTextSharp.text.Font(bfTimes, 13f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna12 = new iTextSharp.text.Font(bfTimes, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna11 = new iTextSharp.text.Font(bfTimes, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font TituloColumna10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font bodyColumna = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(bfTimes, 7f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal8 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal9 = new iTextSharp.text.Font(bfTimes, 9f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal11 = new iTextSharp.text.Font(bfTimes, 11f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font tituloNormal12 = new iTextSharp.text.Font(bfTimes, 12f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


            try
            {

                var ousuario = (tbUsuario)Session["Usuario"];
                oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                DataSet dsDatosCertificadoPOA = CD_SolicitudPOA.Instancia.CertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);

                var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                store = storeCertificado(oCertificado, ousuario);

                //AsymmetricKeyEntry keyEntry = keyEntryCertificado(oCertificado, ousuario); //store.GetKey(alias);
                if (store != null)
                {
                    foreach (DataRow dr in dsDatosCertificadoPOA.Tables[0].Rows)
                    {

                        pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + ousuario.CodigoUsuario;
                        directorio = oSolicitud.CodigoDireccionPYGE + @"\" + oSolicitud.AnioSolicitud + @"\" + oSolicitud.TipoSolicitud + @"\" + oSolicitud.NumeroSolicitud.ToString();
                        nombreArchivo = "CertificadoPOA" + "_" + oSolicitud.CodigoDireccionPYGE.Trim() + "_" + oSolicitud.AnioSolicitud + "_" + oSolicitud.TipoSolicitud + "_" + oSolicitud.NumeroSolicitud.ToString();

                        pathSolcitud = Constantes.poaURL + @"\" + directorio;

                        //Obtengo l path del servidor donde se va crear el archivo

                        if (!System.IO.Directory.Exists(pathSolcitud))
                            System.IO.Directory.CreateDirectory(pathSolcitud);

                        FileStream file = new FileStream(pathSolcitud + @"\" + nombreArchivo + ".pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        pdfWriter = PdfWriter.GetInstance(pdfDoc, file);

                        pdfDoc.Open();

                        Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Content/imganes/logodgac.jpg"));
                        logo.ScalePercent(35f);
                        logo.SetAbsolutePosition(180, 180);

                        var tbl = new PdfPTable(new float[] { 10f, 80f }) { WidthPercentage = 100f };
                        var c1 = new PdfPCell(logo) { Rowspan = 3 };
                        var c2 = new PdfPCell(new Phrase("DIRECCIÓN GENERAL DE AVIACIÓN CIVIL DEL ECUADOR", negrita12));
                        c1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        c2.HorizontalAlignment = PdfPCell.ALIGN_MIDDLE;
                        c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        c1.Border = 0;
                        c2.Border = 0;
                        tbl.AddCell(c1);
                        tbl.AddCell(c2);
                        c2 = new PdfPCell(new Phrase(" ", negrita12));
                        c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        c2.Border = 0;
                        tbl.AddCell(c2);
                        c2 = new PdfPCell(new Phrase("DIRECCIÓN DE PLANIFICACIÓN Y GESTIÓN ESTRATÉGICA", negrita11));
                        c2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        c2.Border = 0;
                        tbl.AddCell(c2);
                        pdfDoc.Add(tbl);

                        paraAsunto.Add("");
                        pdfDoc.Add(paraAsunto);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = negrita12;
                        paraAsunto.Add("Certificación POA No." + dr["CERTIFICACION_POA_NO"].ToString());
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paraAsunto);
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = negrita12;
                        paraAsunto.Add("Fecha Certificación POA: " + fechaDateAs400(dr["FECHAAPRBCERTIFICADO"].ToString()));
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paraAsunto);                       
                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Font = tituloNormal12;
                        referenciaCertificado = "En referencia a la solicitud No. " + dr["N_SOLICITUD"].ToString().Trim() + ", de " + fechaTexto(dr["FECHA_SOLICITUD"].ToString()).Trim() + ", mediante el cual, la Dirección " + dr["DIRECCION_SOLICITANTE"].ToString().Trim() + ", solicita la certificación POA de " + dr["ACTIVIDAD_POA"].ToString().Trim();                        
                        paraAsunto.Add(referenciaCertificado);                        
                        paraAsunto.Alignment = Element.ALIGN_JUSTIFIED;
                        pdfDoc.Add(paraAsunto);
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Clear();
                        paraAsunto.Font = tituloNormal12;
                        respectoCertificado = "Al respecto, me permito certificar que la mencionada actividad consta en el POA "+ dr["ANIO_SOLICITUD"].ToString().Trim() + " de la DGAC, siendo la Dirección "+ dr["DIRECCION_SOLICITANTE"].ToString().Trim() + " responsable de su ejecución, según la siguiente estructura:";
                        paraAsunto.Add(respectoCertificado);
                        paraAsunto.Alignment = Element.ALIGN_JUSTIFIED;
                        pdfDoc.Add(paraAsunto);
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Clear();
                        //Creamos la tabla 
                        PdfPTable tableEstructura = new PdfPTable(new float[] { 30f, 70f }) { WidthPercentage = 100f };
                        var celle1 = new PdfPCell(new Phrase("Dirección Solicitante:", negrita12));
                        var celle2 = new PdfPCell(new Phrase(dr["DIRECCION_SOLICITANTE"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Actividad Preupuestaria:", negrita12));
                        celle2 = new PdfPCell(new Phrase(dr["ACTIVIDAD_P"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Programa Presupuestario:", negrita12));
                        celle2 = new PdfPCell(new Phrase(dr["PROGRAMA"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Geográfico:", negrita12));
                        celle2 = new PdfPCell(new Phrase(dr["GEOGRAFICO"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Tipo Adquisición:", negrita12));
                        celle2 = new PdfPCell(new Phrase(dr["TIPO_ADQUISICION"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Actividad POA:", negrita12));
                        celle2 = new PdfPCell(new Phrase(dr["ACTIVIDAD_POA"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Partida Presupuestaria:", negrita12));
                        celle2 = new PdfPCell(new Phrase(dr["PARTIDA_PRESUPUESTARIA"].ToString().Trim(), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        celle1 = new PdfPCell(new Phrase("Monto Total:", negrita12));
                        celle2 = new PdfPCell(new Phrase("$" + Convert.ToDecimal(dr["MONTO_TOTAL_USD"].ToString()), tituloNormal12));
                        celle1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        celle2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        tableEstructura.AddCell(celle1);
                        tableEstructura.AddCell(celle2);
                        pdfDoc.Add(tableEstructura);
                        pdfDoc.Add(Chunk.NEWLINE);

                        fechaFirma = fechaDateAs400(oSistema.FechaSistema) + " " + oSistema.HoraSistema;

                        alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));

                        textoQRCodeFirma = "Firmado por." + alias + "\n Date: " + DateTime.Parse(fechaFirma);

                        barcodeQRCodeFirma = new BarcodeQRCode(textoQRCodeFirma, 1000, 1000, null);
                        codeQRImageFirma = barcodeQRCodeFirma.GetImage();
                        codeQRImageFirma.ScalePercent(8f);
                        codeQRImageFirma.SetAbsolutePosition(80, 70);

                        string documentoAttentamente = "\n\nDocumento firmado electrónicamente" + "\n" + alias + "\n" + DateTime.Parse(fechaFirma);

                        oPieFirma.Font = negrita10;
                        oPieFirma.Add("Aprobado por:");
                        oPieFirma.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(oPieFirma);
                        pdfDoc.Add(Chunk.NEWLINE);
                        PdfPTable tableFirma = new PdfPTable(new float[] { 15f, 80f }) { WidthPercentage = 100f };
                        var cell1 = new PdfPCell(codeQRImageFirma);
                        var cell2 = new PdfPCell(new Phrase(documentoAttentamente, negritaBlack8));

                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell1.Border = 0;
                        cell2.Border = 0;

                        tableFirma.AddCell(cell1);
                        tableFirma.AddCell(cell2);
                        pdfDoc.Add(tableFirma);


                        paraAsunto.Clear();
                        pdfDoc.Add(Chunk.NEWLINE);
                        paraAsunto.Add("DIRECTOR(A) DE PLANIFICACIÓN Y GESTIÓN ESTRATÉGICA");
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        paraAsunto.Font = tituloNormal12;
                        pdfDoc.Add(paraAsunto);
                        paraAsunto.Clear();                        
                        paraAsunto.Add(dr["USUARIO"].ToString().Trim());
                        paraAsunto.Alignment = Element.ALIGN_LEFT;
                        paraAsunto.Font = tituloNormal12;
                        pdfDoc.Add(paraAsunto);
                    }
                    pdfWriter.CloseStream = true;
                    pdfDoc.CloseDocument();
                    pdfDoc.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pdfDoc;
        }

        private Pkcs12Store storeCertificado(tbCertificadoDigital ocertificado, tbUsuario ousuario)
        {
            string pathCertificado = string.Empty;
            string pathAutorizacion = string.Empty;
            tbCertificadoDigital ocertificadoDigital = new tbCertificadoDigital();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store ostoreCertificado = null;
            try
            {
                //pathAutorizacion = Utilitario.Utilitarios.autorizacionURL;
                pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + @"\" + ousuario.CodigoUsuario;

                //if (!Directory.Exists(pathAutorizacion))
                //{
                //    Directory.CreateDirectory(pathAutorizacion);
                //}

                //var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);

                string certificatePath = pathCertificado + @"\" + ocertificado.PathDocumento;
                string certificatePassword = SeguridadEncriptar.DesEncriptar(ocertificado.Contrasena);

                // Cargar el certificado
                Pkcs12Store store = new Pkcs12Store(new FileStream(certificatePath, FileMode.Open, FileAccess.Read), certificatePassword.ToCharArray());

                //objStore.Open(OpenFlags.ReadOnly);

                string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));

                if (alias != null)
                {
                    ostoreCertificado = store;
                    // keyEntry = store.GetKey(alias);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ostoreCertificado;
        }

        public iTextSharp.text.Image GetQRCode(string content)
        {
            iTextSharp.text.pdf.BarcodeQRCode qrcode = new iTextSharp.text.pdf.BarcodeQRCode(content, 100, 100, null);
            iTextSharp.text.Image img = qrcode.GetImage();
            //MemoryStream ms = new MemoryStream(img.OriginalData);
            return img; //System.Drawing.Image.FromStream(ms);
        }

        [HttpGet]
        public JsonResult EnviaAprobarSolicitudCertificadoPOA(string canio, Int32 numSolicitud)
        {
            bool respuesta;
            string fullPath = string.Empty;
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            if (Session["Usuario"] != null)
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                Session["CodigoSubsistema"] = ousuario.CodigoSubsistema;
                Session["CodigoRol"] = ousuario.CodigoRol;
                respuesta = CD_SolicitudPOA.Instancia.ApruebaEnviaSolicitudCertificadoPOA(canio, numSolicitud, ousuario.CodigoUsuario);
            }
            else
            {
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo verifica que exista en la base de datos el certificado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult VerificaExisteCertificadoFirma()
        {
            bool estado = false;
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            try
            {
                if (Session["Usuario"] != null)
                {
                    var ousuario = (tbUsuario)Session["Usuario"];

                    oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                    if (oCertificado.CodigoUsuario != null)
                    {
                        if (oCertificado.TipoArchivo.Equals("T"))
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = true;
                            estado = ValidaCertificadoFirmaExiste(oCertificado.CodigoUsuario, oCertificado.PathDocumento);
                        }
                    }
                }
            }
            catch
            {
                estado = false;
            }
            return Json(new { resultado = estado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo verifica que exista en la base de datos el certificado
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult VerificaExisteFirmaElectronica()
        {
            bool estado = false;
            tbSistema osistema = new tbSistema();
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            DateTime dtFechaSistema;
            DateTime dtFechaVigencia;
            try
            {
                if (Session["Usuario"] != null)
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    var ousuario = (tbUsuario)Session["Usuario"];

                    oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                    if (oCertificado.CodigoUsuario != null)
                    {
                        if (oCertificado.TipoArchivo.Equals("T"))
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = true;
                            estado = ValidaCertificadoFirmaExiste(oCertificado.CodigoUsuario, oCertificado.PathDocumento);
                            if (estado)
                            {
                                dtFechaVigencia = DateTime.Parse(fechaDateAs400(oCertificado.FechaVencimiento));
                                dtFechaSistema = DateTime.Parse(fechaDateAs400(osistema.FechaSistema));

                                if (dtFechaVigencia <= dtFechaSistema)
                                {
                                    CD_CertificadoDigital.Instancia.CertificadoDigitalCambiaEstado("IA", ousuario.CodigoUsuario, oCertificado.Secuencial);
                                    estado = false;
                                }
                            }
                        }

                    }
                }
            }
            catch
            {
                estado = false;
            }
            return Json(new { resultado = estado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo obtiene Solicitud del Certificado POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns>tbSolicitudPOA</returns>
        [HttpPost]
        public JsonResult ObtieneSolicitudCertificadoPOAPorAnioNumeroSolicitud(string canio, Int32 numSolicitud)
        {
            tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
            try
            {
                oSolicitud = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
            }
            catch
            {
                oSolicitud = null;
            }

            return Json(oSolicitud, JsonRequestBehavior.AllowGet);

        }


        public JsonResult VerificaExisteCertificadoFirma(string codUsuario, string nombreArchivo)
        {
            bool estado = false;
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];

                string urlDocumentos = Utilitarios.Utilitario.certificadoPOAUrl + codUsuario + @"\" + nombreArchivo;
                //Verifica si existe el archivo
                if (System.IO.File.Exists(urlDocumentos))
                {
                    estado = true;
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return Json(new { resultado = estado }, JsonRequestBehavior.AllowGet);
        }

        private Int32 DocumentacionExiste(string urlDocumentos)
        {

            string url = "";
            int numArchivo = 0;
            try
            {
                url = Constantes.poaURL + urlDocumentos;

                string[] files = System.IO.Directory.GetFiles(url);
                if (files.Length > 0)
                {
                    numArchivo = files.Length;
                }
            }
            catch (Exception ex)
            {
                numArchivo = 0;
            }
            return numArchivo;
        }

        private string fechaTexto(string fecha)
        {
            string fechaNueva = string.Empty;
            if (fecha.Trim().Length >0)
            {
                fechaNueva = fecha.Substring(6, 2) + " de " + mestexto(fecha.Substring(4, 2)) + " de " + fecha.Substring(0, 4);
            }            
            
            return fechaNueva;
        }

        private string mestexto(string mes)
        {
            if (mes.Length < 2)
                mes = "0" + mes;

            if (mes.Equals("01"))
                return "enero";
            else if (mes.Equals("02"))
                return "febrero";
            else if (mes.Equals("03"))
                return "marzo";
            else if (mes.Equals("04"))
                return "abril";
            else if (mes.Equals("05"))
                return "mayo";
            else if (mes.Equals("06"))
                return "junio";
            else if (mes.Equals("07"))
                return "julio";
            else if (mes.Equals("08"))
                return "agosto";
            else if (mes.Equals("09"))
                return "septiembre";
            else if (mes.Equals("10"))
                return "octubre";
            else if (mes.Equals("11"))
                return "noviembre";
            else if (mes.Equals("12"))
                return "diciembre";
            else
                return "";
        }

        private String fechaDateAs400(string ofecha)
        {
            string odate = string.Empty;
            if (ofecha.Trim().Length > 0)
            {
                odate = ofecha.Substring(6, 2) + "/" + ofecha.Substring(4, 2) + "/" + ofecha.Substring(0, 4);
            }

            
            return odate;
        }

    }
}