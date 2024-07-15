using CapaDatos;
using CapaModelo;
using SistemaIntegradoGestion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Pkcs;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using SistemaIntegradoGestion.Utilitario;
using SistemaIntegradoGestion.Utilitarios;
using System.Text;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using System.Net;

namespace SistemaIntegradoGestion.Controllers
{
    public class ModificacionPoaController : Controller
    {
        private static string ssrsurl = ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToString();
        private static tbUsuario SesionUsuario;
        // GET: ModificacionPoa
        public ActionResult SinAfectacionPresupuestaria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);

            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            Session["Controlador"] = "ModificacionPoa";
            Session["ActionResul"] = "SinAfectacionPresupuestaria";
            Session["CodigoRol"] = SesionUsuario.CodigoRol;
            listado = CD_SolicitudPOA.Instancia.SolicitudModificacionSinAfectacionPresupetariaListarDireccionAnio(SesionUsuario.CodigoSubsistema, cAnio);
            //Verifica que si ya tiene subido los archivos
            foreach (var item in listado)
            {
                string direccionDirectory = item.CodigoDireccionPYGE + @"\" + item.TipoSolicitud + @"\" + item.AnioSolicitud + @"\" + item.NumeroSolicitud;
                string pathDirectorio = Constantes.poaURL + @"\" + direccionDirectory;
                if (GetVerifcaExisteArchivosDirectorio(pathDirectorio))
                    item.numeroDocumentoAdjunto = 1;
                else
                    item.numeroDocumentoAdjunto = 0;
            }
            return View(listado);
        }

        public ActionResult ConsultarSolicitud(string canio, int numSolicitud, string vista)
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
            //ImprimirPoaActualizado(canio, numSolicitud);
            oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
            if (oSolicitudPoa.oModelArchivo.Count() > 0)
            {
                oSolicitudPoa.numeroDocumentoAdjunto = 1;
            }


            return View(oSolicitudPoa);
        }

        public ActionResult SubirDocumentosHabilitantes(string canio, int numSolicitud, string vista)
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
            oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
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

            oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
            if (oSolicitudPoa.oModelArchivo.Count() > 0)
            {
                oSolicitudPoa.numeroDocumentoAdjunto = 1;
            }

            return View(oSolicitudPoa);

        }



        public JsonResult BuscarAeronaesPorMatriculaModeloMarca(string AnioSolicitud, int NumeroSolicitud, string nombreArchivo)
        {
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            string direccionDirectory = string.Empty;
            try
            {
                if (Session["Usuario"] == null)
                    return Json("", JsonRequestBehavior.AllowGet);


                if (AnioSolicitud.Trim().Length > 0 && NumeroSolicitud > 0 && nombreArchivo.Trim().Length > 0)
                {
                    direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + AnioSolicitud + @"\" + NumeroSolicitud;

                    //if (EliminarDocumentoSinAfectacion(nombreArchivo, direccionDirectory)    {

                    //}           

                }

            }
            catch (Exception)
            {
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



        private List<tbModelArchivo> GetObtenerTodosArchivos(string directory)
        {
            string carpetaPoa = string.Empty;
            string urlDocumentos = string.Empty;
            System.Collections.Generic.List<tbModelArchivo> listArchivo = new System.Collections.Generic.List<tbModelArchivo>();

            if (directory.Length > 0)
            {
                string direccionDirectory = directory;

                ViewBag.DireccionDirectory = direccionDirectory;

                urlDocumentos = Constantes.poaURL + @"\" + direccionDirectory;

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
        /// <summary>
        /// Obtiene todos los archivos de la direccion
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private List<tbModelArchivo> GetObtenerTodosArchivosPath(string directory)
        {
            string carpetaPoa = string.Empty;
            string urlDocumentos = string.Empty;
            System.Collections.Generic.List<tbModelArchivo> listArchivo = new System.Collections.Generic.List<tbModelArchivo>();

            if (directory.Length > 0)
            {
                string direccionDirectory = directory;

                ViewBag.DireccionDirectory = direccionDirectory;

                urlDocumentos = directory;

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

        private bool GetVerifcaExisteArchivosDirectorio(string directory)
        {
            string carpetaPoa = string.Empty;
            bool estadoArchivo = false;
            if (directory.Length > 0)
            {

                //           urlDocumentos = Constantes.modificacionpoaURL + direccionDirectory;


                if (System.IO.Directory.Exists(directory))
                {
                    string[] files = System.IO.Directory.GetFiles(directory);

                    if (files.Length > 0)
                    {
                        for (int iFile = 0; iFile < files.Length; iFile++)
                        {
                            estadoArchivo = true;
                        }

                    }

                }
                else
                {
                    estadoArchivo = false;
                }


            }
            return estadoArchivo;
        }

        public ActionResult VisualizarFileSinAfectacion(string nombreArchivo, string direccion)
        {

            string fullName = Constantes.poaURL + @"\" + direccion.Trim() + @"\" + nombreArchivo.Trim();

            byte[] fileBytes = GetFile(fullName);

            return new FileContentResult(fileBytes, "application/pdf");
        }

        public ActionResult DownloadFileSinAfectacionPresupuestaria(string nombreArchivo, string direccion)
        {
            try
            {
                string fullName = Constantes.poaURL + @"\" + direccion.Trim() + @"\" + nombreArchivo;
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
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        [HttpGet]
        public JsonResult EliminarDocumentoSinAfectacion(string nombreArchivo, string direccion)
        {
            bool respuesta;
            string fullPath = string.Empty;
            if (Session["Usuario"] != null)
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                fullPath = Constantes.poaURL + @"\" + direccion + @"\" + nombreArchivo;
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
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }


        public ActionResult EnviaSolicitudVerificacionPresupuestario(string canio, Int32 numSolicitud)
        {
            string FilePathReturn = string.Empty;
            string direccionDirectory = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            ReportViewer viewer = new ReportViewer();
            string mimeType, encoding, filenameExtension;
            byte[] bytes;
            Warning[] warnings;
            string[] streamids;
            string nombreArchivo = string.Empty;
            string message = string.Empty;
            try
            {
                if (Session["Usuario"] == null)
                    return Json(new { Message = "La seccion del usuario esta caducado actualice la pagina.", JsonRequestBehavior.AllowGet });

                SesionUsuario = (tbUsuario)Session["Usuario"];

                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
                if (CD_SolicitudPOA.Instancia.EnviaSolictudVerificacionPresupuestaria(oSolicitudPoa.AnioSolicitud, oSolicitudPoa.NumeroSolicitud, oSolicitudPoa.CodigoUnidadEjecucion, oSolicitudPoa.CodigoDireccionPYGE, SesionUsuario.CodigoUsuario))
                {
                    #region "Generar e reporte"     
                    viewer.ProcessingMode = ProcessingMode.Remote;
                    viewer.SizeToReportContent = true;
                    viewer.AsyncRendering = true;
                    viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                    viewer.ServerReport.ReportPath = "/Report Project1/ModificacionPOA";
                    ReportParameter[] reportParameter = new ReportParameter[2];
                    reportParameter[0] = new ReportParameter("anio", canio);
                    reportParameter[1] = new ReportParameter("solicitud", numSolicitud.ToString());
                    viewer.ServerReport.SetParameters(reportParameter);

                    //viewer.ServerReport.Refresh();

                    string urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                    //Verifica si existe la carpeta creada si no lo crear
                    if (!System.IO.Directory.Exists(urlReporteElectronico))
                        System.IO.Directory.CreateDirectory(urlReporteElectronico);

                    bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    if (bytes.Length > 0)
                    {
                        //nombreArchivo = "SOL_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();
                        nombreArchivo = "SOL_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim();
                        string FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                        int secuencial = 0;
                        while (existeArchivo(FilePath))
                        {
                            secuencial++;
                            FilePath = urlReporteElectronico + @"\" + nombreArchivo + "_" + secuencial + ".pdf";

                        }


                        PdfReader reader = new PdfReader(bytes);
                        FileStream output = new FileStream(FilePath, FileMode.Create);
                        //create and set PdfStamper  
                        PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);
                        string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                        if (Agent.Contains("Firefox"))
                            pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                        else
                            pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                        pdfStamper.FormFlattening = false;
                        pdfStamper.Close();
                        reader.Close();
                        output.Close();

                    }

                    #endregion
                    message = "ok";
                }
                else
                {
                    message = "Inconsistencia en Lineas de Modificación";
                }
            }
            catch (Exception ex)
            {
                //se hace un que haya un error al procesarel reporte o la actualizacion la transaccion debe hacer el Rollback
                CD_SolicitudPOA.Instancia.RegresaSolictudVerificacionPresupuestaria(canio, numSolicitud, "NS");
                message = ex.Message;
            }
            return Content(message);
        }

        private string GeneraReporteSolicitudModificacionPoa(string canio, Int32 numSolicitud)
        {
            string nombreReporte = string.Empty;
            ReportViewer viewer = new ReportViewer();
            string mimeType, encoding, filenameExtension;
            string direccionDirectory = string.Empty;
            Warning[] warnings;
            string[] streamids;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            byte[] bytes;
            try
            {
                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;

                viewer.ProcessingMode = ProcessingMode.Remote;
                viewer.SizeToReportContent = true;
                viewer.AsyncRendering = true;
                viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                viewer.ServerReport.ReportPath = "/Report Project1/ModificacionPOA";
                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("anio", canio);
                reportParameter[1] = new ReportParameter("solicitud", numSolicitud.ToString());
                viewer.ServerReport.SetParameters(reportParameter);
                string nombreArchivo = string.Empty;


                //viewer.ServerReport.Refresh();

                string urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlReporteElectronico))
                    System.IO.Directory.CreateDirectory(urlReporteElectronico);

                bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                if (bytes.Length > 0)
                {
                    nombreArchivo = "SOL_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim();
                    string FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                    nombreReporte = nombreArchivo + ".pdf";
                    int secuencial = 0;
                    while (existeArchivo(FilePath))
                    {
                        secuencial++;
                        FilePath = urlReporteElectronico + @"\" + nombreArchivo + "_" + secuencial + ".pdf";
                        nombreReporte = nombreArchivo + "_" + secuencial + ".pdf";
                    }



                    PdfReader reader = new PdfReader(bytes);
                    FileStream output = new FileStream(FilePath, FileMode.Create);
                    //create and set PdfStamper  
                    PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);
                    string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                    if (Agent.Contains("Firefox"))
                        pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                    else
                        pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                    pdfStamper.FormFlattening = false;
                    pdfStamper.Close();
                    reader.Close();
                    output.Close();
                }

            }
            catch (Exception)
            {
                nombreReporte = string.Empty;
            }
            return nombreReporte;
        }

        /// <summary>
        /// Action Elabora el informe de viabilidad modificación POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns>Mensaje</returns>
        public ActionResult ElaborarInformeViabilidadModificacionPOA(string canio, Int32 numSolicitud)
        {
            string FilePathReturn = string.Empty;
            string direccionDirectory = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            ReportViewer viewer = new ReportViewer();
            string mimeType, encoding, filenameExtension;
            byte[] bytes;
            Warning[] warnings;
            string[] streamids;
            string nombreArchivo = string.Empty;
            string message = string.Empty;
            string nombreArchivoNuevo = string.Empty;
            string FilePath = string.Empty;
            string nombreArchivoFirmado = string.Empty;
            string nombreArchivoModPoa = string.Empty;
            //variables para imprimir documento pdf 
            string nombreDocumentoModificar = string.Empty;
            string nombreDocumentoModificarFirmado = string.Empty;
            string nombreArchivoPropuesto = string.Empty;

            DocumentoFirmado documentoFirmadoPdf = new DocumentoFirmado();
            try
            {
                if (Session["Usuario"] == null)
                    return Json(new { Message = "La seccion del usuario esta caducado actualice la pagina.", JsonRequestBehavior.AllowGet });

                SesionUsuario = (tbUsuario)Session["Usuario"];

                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
                #region "Generar el reporte"     
                viewer.ProcessingMode = ProcessingMode.Remote;
                viewer.SizeToReportContent = true;
                viewer.AsyncRendering = true;
                viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                viewer.ServerReport.ReportPath = "/Report Project1/AnalisisViabilidadPOA";
                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("AnioSolictud", canio);
                reportParameter[1] = new ReportParameter("NumSolicitud", numSolicitud.ToString());
                viewer.ServerReport.SetParameters(reportParameter);

                string urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlReporteElectronico))
                    System.IO.Directory.CreateDirectory(urlReporteElectronico);

                bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                if (bytes.Length > 0)
                {
                    nombreArchivo = "IV_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                    // nombreArchivoFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + "-signed.pdf";
                    nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + "-signed.pdf";
                    int secuencial = 0;
                    while (existeArchivo(FilePath))
                    {
                        secuencial++;
                        nombreArchivoNuevo = nombreArchivo + "_" + secuencial + ".pdf";
                        nombreArchivoFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";
                        FilePath = urlReporteElectronico + @"\" + nombreArchivoNuevo;
                        nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";

                    }


                    PdfReader reader = new PdfReader(bytes);
                    FileStream output = new FileStream(FilePath, FileMode.Create);
                    //create and set PdfStamper  
                    PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);
                    string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                    if (Agent.Contains("Firefox"))
                        pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                    else
                        pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                    pdfStamper.FormFlattening = false;
                    pdfStamper.Close();
                    reader.Close();
                    output.Close();
                }
                #endregion

                nombreArchivoPropuesto = FilePath.Replace("IV_", "PP_").Replace(".pdf", ".xlsx");
                ExportarArchivoExcelViews(canio, numSolicitud, nombreArchivoPropuesto);
                message = "ok";
            }
            catch (Exception ex)
            {
                //
                message = ex.Message;
            }
            return Content(message);
        }

        private bool ElaborarInformeViabilidadModificacionPOA1(string canio, Int32 numSolicitud)
        {
            bool estado = false;

            string FilePathReturn = string.Empty;
            string direccionDirectory = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            ReportViewer viewer = new ReportViewer();
            string mimeType, encoding, filenameExtension;
            byte[] bytes;
            Warning[] warnings;
            string[] streamids;
            string nombreArchivo = string.Empty;
            string message = string.Empty;
            string nombreArchivoNuevo = string.Empty;
            string FilePath = string.Empty;
            string nombreArchivoFirmado = string.Empty;
            string nombreArchivoModPoa = string.Empty;
            //variables para imprimir documento pdf 
            string nombreDocumentoModificar = string.Empty;
            string nombreDocumentoModificarFirmado = string.Empty;
            string nombreArchivoPropuesto = string.Empty;

            DocumentoFirmado documentoFirmadoPdf = new DocumentoFirmado();
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];

                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
                #region "Generar el reporte"     
                viewer.ProcessingMode = ProcessingMode.Remote;
                viewer.SizeToReportContent = true;
                viewer.AsyncRendering = true;
                viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                viewer.ServerReport.ReportPath = "/Report Project1/AnalisisViabilidadPOA";
                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("AnioSolictud", canio);
                reportParameter[1] = new ReportParameter("NumSolicitud", numSolicitud.ToString());
                viewer.ServerReport.SetParameters(reportParameter);

                string urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlReporteElectronico))
                    System.IO.Directory.CreateDirectory(urlReporteElectronico);

                bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                if (bytes.Length > 0)
                {
                    nombreArchivo = "IV_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                    // nombreArchivoFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + "-signed.pdf";
                    nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + "-signed.pdf";
                    int secuencial = 0;
                    while (existeArchivo(FilePath))
                    {
                        secuencial++;
                        nombreArchivoNuevo = nombreArchivo + "_" + secuencial + ".pdf";
                        nombreArchivoFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";
                        FilePath = urlReporteElectronico + @"\" + nombreArchivoNuevo;
                        nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";

                    }


                    PdfReader reader = new PdfReader(bytes);
                    FileStream output = new FileStream(FilePath, FileMode.Create);
                    //create and set PdfStamper  
                    PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);
                    string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                    if (Agent.Contains("Firefox"))
                        pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                    else
                        pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                    pdfStamper.FormFlattening = false;
                    pdfStamper.Close();
                    reader.Close();
                    output.Close();
                }
                #endregion

                nombreArchivoPropuesto = FilePath.Replace("IV_", "PP_").Replace(".pdf", ".xlsx");
                ExportarArchivoExcelViews(canio, numSolicitud, nombreArchivoPropuesto);
                estado = true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return estado;
        }

        /// <summary>
        /// Metodo genera el reporte 
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="FilePath"></param>
        /// <returns>true o false</returns>
        private bool ElaborarInformeViabilidadModificacionPOAArchivoPdf(string canio, Int32 numSolicitud, string FilePath)
        {
            bool estadoReporte = false;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            ReportViewer viewer = new ReportViewer();
            string mimeType, encoding, filenameExtension;
            byte[] bytes;
            Warning[] warnings;
            string[] streamids;
            string nombreArchivo = string.Empty;
            string message = string.Empty;
            string nombreArchivoNuevo = string.Empty;
            string nombreArchivoFirmado = string.Empty;
            string nombreArchivoModPoa = string.Empty;
            //variables para imprimir documento pdf 
            string nombreDocumentoModificar = string.Empty;


            DocumentoFirmado documentoFirmadoPdf = new DocumentoFirmado();
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];

                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                #region "Generar el reporte"     
                viewer.ProcessingMode = ProcessingMode.Remote;
                viewer.SizeToReportContent = true;
                viewer.AsyncRendering = true;
                viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                viewer.ServerReport.ReportPath = "/Report Project1/AnalisisViabilidadPOA";
                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("AnioSolictud", canio);
                reportParameter[1] = new ReportParameter("NumSolicitud", numSolicitud.ToString());
                viewer.ServerReport.SetParameters(reportParameter);

                bytes = viewer.ServerReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                if (bytes.Length > 0)
                {
                    PdfReader reader = new PdfReader(bytes);
                    FileStream output = new FileStream(FilePath, FileMode.Create);
                    //create and set PdfStamper  
                    PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);
                    string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                    if (Agent.Contains("Firefox"))
                        pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
                    else
                        pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

                    pdfStamper.FormFlattening = false;
                    pdfStamper.Close();
                    reader.Close();
                    output.Close();
                    estadoReporte = true;
                }
                #endregion
            }
            catch
            {
                estadoReporte = false;
            }
            return estadoReporte;
        }

        private bool ExportarArchivoExcelViews(string canio, int numSolicitud, string nombreArchivoPropuesto)
        {
            bool estado = false;
            try
            {
                DataSet dsPoaPropuesto = new DataSet();
                dsPoaPropuesto = CD_SolicitudPOA.Instancia.ElaborarInformeViabilidadModificacionPOAPropuesto(canio, numSolicitud);

                ExportarArchivoExcel.Instancia.ExportDataTableToExcel(dsPoaPropuesto, nombreArchivoPropuesto);
                estado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }

        private bool existeArchivo(string path)
        {
            bool estado = false;
            try
            {
                if (System.IO.File.Exists(path))
                    estado = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return estado;
        }

        public ActionResult VerificaPresupuestariaSolicitud(string canio, Int32 numSolicitud)
        {
            string direccionDirectory = string.Empty;
            ViewBag.mensajeError = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
                Session["DireccionPath"] = direccionDirectory;
                ViewBag.DireccionDirectory = direccionDirectory;
                ViewBag.SelectComboEstadoFinanciero = GetSelectListValoresVerificaPresupuesto("SOLES9");
                ViewBag.SelectComboEstadoAprobarModificacionReforma = GetSelectListValoresAprobarModificacionReforma("SOLES9");
                oSolicitudPoa.CodigoRolPYGE = SesionUsuario.CodigoRol;
                oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = ex.Message;
            }

            return View(oSolicitudPoa);
        }

        [HttpPost]
        public ActionResult VerificaPresupuestariaSolicitud(tbSolicitudPOA modelo)
        {
            string direccionDirectory = string.Empty;
            ViewBag.mensajeError = string.Empty;
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
                modelo.UsuarioCreacionFIN_PRES = SesionUsuario.CodigoUsuario;
                ViewBag.SelectComboEstadoFinanciero = GetSelectListValoresVerificaPresupuesto("SOLES9");
                ViewBag.SelectComboEstadoAprobarModificacionReforma = GetSelectListValoresAprobarModificacionReforma("SOLES9");

                if (CD_SolicitudPOA.Instancia.ActualizaVerificacionPresupuestaria(modelo))
                {
                    modelo = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(modelo.AnioSolicitud, modelo.NumeroSolicitud);
                    direccionDirectory = @modelo.CodigoDireccionPYGE + @"\" + @modelo.TipoSolicitud + @"\" + modelo.AnioSolicitud + @"\" + modelo.NumeroSolicitud;
                    Session["DireccionPath"] = direccionDirectory;
                    ViewBag.DireccionDirectory = direccionDirectory;
                    modelo.CodigoRolPYGE = SesionUsuario.CodigoRol;
                    modelo.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
                    ViewBag.mensajeError = "ok";
                    //return RedirectToAction("VerificaPresupuestariaSolicitud", "ModificacionPoa", new { canio = modelo.AnioSolicitud, numSolicitud = modelo.NumeroSolicitud });
                }
                else
                {
                    modelo = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(modelo.AnioSolicitud, modelo.NumeroSolicitud);
                    direccionDirectory = @modelo.CodigoDireccionPYGE + @"\" + @modelo.TipoSolicitud + @"\" + modelo.AnioSolicitud + @"\" + modelo.NumeroSolicitud;
                    Session["DireccionPath"] = direccionDirectory;
                    ViewBag.DireccionDirectory = direccionDirectory;
                    modelo.CodigoRolPYGE = SesionUsuario.CodigoRol;
                    ViewBag.mensajeError = "No se puedo guardar el registro";
                }

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = ex.Message;
            }



            return View(modelo);
        }

        public ActionResult ListarVerificarPresupuestaria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            System.Collections.Generic.List<tbSolicitudPOA> listado = new System.Collections.Generic.List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);
            Session["ActionResul"] = "ListarVerificarPresupuestaria";
            Session["Controlador"] = "ModificacionPoa";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();

            listado = CD_SolicitudPOA.Instancia.ModificacionPOAParaVerificacionEnPresupuesto();
            return View(listado);
        }

        public ActionResult SolicitudModificacionPOA()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);

            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            Session["Controlador"] = "ModificacionPoa";
            Session["ActionResul"] = "SolicitudModificacionPOA";
            Session["CodigoRol"] = SesionUsuario.CodigoRol;
            listado = CD_SolicitudPOA.Instancia.SolicitudesModificacionPOA();
            return View(listado);
        }


        public ActionResult RevisarAprobarSolicModificacionPoa(string canio, int numSolicitud, string vista)
        {
            string direccionDirectory = string.Empty;
            ViewBag.mensajeTitulo = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            SesionUsuario = (tbUsuario)Session["Usuario"];
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);

            direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
            Session["ActionResul"] = vista;
            ViewBag.SelectComboEstadoAutorizacion = GetSelectListValoresSolicitudAutorizacionPoa("SOLES7");
            ViewBag.SelectComboEstadoAutorizacionTodos = GetSelectListarValoresSolicitudAutorizacionPoaTodos("SOLES7");
            ViewBag.SelectComboEstadoSolicitud = GetSelectListarValoresSolicitudAutorizacionPoaTodos("SOLES6");
            ViewBag.SelectComboEstadoVerificacionPreusto = GetSelectListarValoresSolicitudAutorizacionPoaTodos("SOLES9");
            ViewBag.SelectComboEstadoPresupuestoModificacion = GetSelectListarValoresSolicitudAutorizacionPoaTodos("SOLES8");
            Session["DireccionPath"] = direccionDirectory;
            ViewBag.DireccionDirectory = direccionDirectory;
            oSolicitudPoa.CodigoRolPYGE = SesionUsuario.CodigoRol;
            oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);

            return View(oSolicitudPoa);
        }

        [HttpPost]
        public ActionResult RevisarAprobarSolicModificacionPoa(tbSolicitudPOA modelo, string vista)
        {
            ViewBag.mensajeTitulo = string.Empty;
            string direccionDirectory = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            SesionUsuario = (tbUsuario)Session["Usuario"];
            Session["ActionResul"] = vista;
            var oSolicitudPoap = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(modelo.AnioSolicitud, modelo.NumeroSolicitud);

            if (oSolicitudPoap.EstadoSolicitud.Equals("SO"))
            {
                if (CD_SolicitudPOA.Instancia.RevisarApruebaSolicitudModificacionPOA(modelo.AnioSolicitud, modelo.NumeroSolicitud, modelo.EstadoAutorizacion, modelo.ObservacionAutorizacion1, modelo.ObservacionAutorizacion2, SesionUsuario.CodigoUsuario))
                {
                    if (modelo.EstadoAutorizacion.Equals("RA"))
                    {
                        if (ElaborarInformeViabilidadModificacionPOA1(modelo.AnioSolicitud, modelo.NumeroSolicitud))
                        {
                            ViewBag.mensajeTitulo = "La solicitud se guardo correctamente";
                            return RedirectToAction("RevisarAprobarSolicModificacionPoa", "ModificacionPoa", new { canio = modelo.AnioSolicitud, numSolicitud = modelo.NumeroSolicitud, vista = Session["ActionResul"].ToString() });
                        }
                    }


                }
            }
            return View(oSolicitudPoa);
        }

        [HttpGet]
        public ActionResult RevisarAprobarSolicitudModificacionPoaEstadoAutorizado(string canioSol, int numSolicitud, string cEstadoAut, string cObservacion1, string cObservacion2, string vista)
        {
            string mensajeError = string.Empty;
            string direccionDirectory = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            try
            {
                if (Session["Usuario"] == null)
                    return RedirectToAction("login", "Login");

                SesionUsuario = (tbUsuario)Session["Usuario"];
                Session["ActionResul"] = vista;
                var oSolicitudPoap = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canioSol, numSolicitud);

                if (oSolicitudPoap.EstadoSolicitud.Equals("SO"))
                {
                    if (CD_SolicitudPOA.Instancia.RevisarApruebaSolicitudModificacionPOA(canioSol, numSolicitud, cEstadoAut, cObservacion1, cObservacion2, SesionUsuario.CodigoUsuario))
                    {
                        mensajeError = "ok";
                    }
                    else
                    {
                        mensajeError = "No se puede aprobar la solicitud de la modificación POA";
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }

            return Content(mensajeError);
        }

        [HttpGet]
        public ActionResult ImprimirPoaPropuestoInformeViabilidad(string canioSol, int numSolicitud)
        {
            string mensajeError = string.Empty;
            string direccionDirectory = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            try
            {
                if (Session["Usuario"] == null)
                    return RedirectToAction("login", "Login");

                SesionUsuario = (tbUsuario)Session["Usuario"];
                //Session["ActionResul"] = vista;
                var oSolicitudPoap = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canioSol, numSolicitud);

                if (oSolicitudPoap.EstadoSolicitud.Equals("SO"))
                {
                    if (oSolicitudPoap.EstadoAutorizacion.Equals("RA") || oSolicitudPoap.EstadoAutorizacion.Equals("RN"))
                    {
                        if (ElaborarInformeViabilidadModificacionPOA1(canioSol, numSolicitud))
                        {
                            mensajeError = "ok";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }

            return Content(mensajeError);
        }

        public ActionResult AprobarInformeViabilidadModificacionPao(string canio, Int32 numSolicitud, string estAutorizacion, string obsercacionAutorizacion1, string obsercacionAutorizacion2)
        {
            string message = string.Empty;
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                if (CD_SolicitudPOA.Instancia.AprobarInformeViabilidadModificicacionPoa(canio, numSolicitud, estAutorizacion, obsercacionAutorizacion1, obsercacionAutorizacion2, SesionUsuario.CodigoUsuario))
                {
                    message = "ok";
                }
                else
                {
                    message = "No se puedo aprobar el Informe de viabilidad de la modficación PAO";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Content(message);
        }

        /// <summary>
        /// Metodo actualiza el POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="numCur"></param>
        /// <param name="fechaCur"></param>
        /// <param name="obsercacionActualiza1"></param>
        /// <param name="obsercacionActualiza2"></param>
        /// <returns>ok o mensaje</returns>
        public ActionResult ActualizarPoa(string canio, Int32 numSolicitud, Int32 numCur, string fechaCur, string obsercacionActualiza1, string obsercacionActualiza2)
        {
            string message = string.Empty;
            string nombreArchivo = string.Empty;
            string urlReporteElectronico = string.Empty;
            string direccionDirectory = string.Empty;
            string nombreDocumentoModificarFirmado = string.Empty;
            string nombreArchivoModPoa = string.Empty;
            string nombreArchivoNuevo = string.Empty;
            string _campoError = string.Empty;
            //variables para la firma 
            string pathCertificado = string.Empty;
            string contrasena = string.Empty;


            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            DocumentoFirmado documentoFirmadoPdf = new DocumentoFirmado();
            tbCertificadoDigital certificadoFirma = new tbCertificadoDigital();
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                if (ValidaExisteCargadoFirmaElectronica(SesionUsuario.CodigoUsuario))
                {
                    oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                    direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;

                    urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                    nombreArchivo = "IV_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    nombreArchivoModPoa = "MOD_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    string FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                    nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "-signed.pdf";
                    int secuencial = 0;
                    while (existeArchivo(FilePath))
                    {
                        nombreDocumentoModificarFirmado = String.Empty;
                        nombreArchivoNuevo = String.Empty;
                        FilePath = String.Empty;
                        secuencial++;
                        nombreArchivoNuevo = nombreArchivo + "_" + secuencial + ".pdf";
                        FilePath = urlReporteElectronico + @"\" + nombreArchivoNuevo;
                        nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";

                    }


                    if (CD_SolicitudPOA.Instancia.ActualizaPOA(canio, numSolicitud, numCur, fechaCur, obsercacionActualiza1, obsercacionActualiza2, SesionUsuario.CodigoUsuario))
                    {
                        //crear el archivo
                        if (ElaborarInformeViabilidadModificacionPOAArchivoPdf(canio, numSolicitud, FilePath))
                        {
                            //Obtiene los datos del certificado electronico
                            certificadoFirma = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(SesionUsuario.CodigoUsuario);
                            //Firma el documento electronico
                            DocumentoFirmadoFirmaElectronica.Instancia.InsertaFirmaCertificadoPOADocumento(FilePath, nombreDocumentoModificarFirmado, SesionUsuario);
                            message = "ok";
                        }
                        else
                        {
                            message = "Se actualizo actualizo el POA, no se puedo generar el reporte";
                        }

                    }
                    else
                    {
                        message = "No se puedo aprobar el Informe de viabilidad de la modficación PAO";
                    }
                }
                else
                {
                    message = "La firma electrónica del Director de DPGE, no está cargado en el sistema";
                }


            }
            catch (Exception ex)
            {
                message = _campoError + " / " + ex.Message;
            }
            return Content(message);
        }


        /// <summary>
        /// Metodo imprimi el POA Actualizado
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns></returns>
        public bool ImprimirPoaActualizado(string canio, Int32 numSolicitud)
        {
            bool estado = false;
            string message = string.Empty;
            string nombreArchivo = string.Empty;
            string urlReporteElectronico = string.Empty;
            string direccionDirectory = string.Empty;
            string nombreDocumentoModificarFirmado = string.Empty;
            string nombreArchivoModPoa = string.Empty;
            string nombreArchivoNuevo = string.Empty;
            string _campoError = string.Empty;
            //variables para la firma 
            string pathCertificado = string.Empty;
            string contrasena = string.Empty;


            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            DocumentoFirmado documentoFirmadoPdf = new DocumentoFirmado();
            tbCertificadoDigital certificadoFirma = new tbCertificadoDigital();
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                if (ValidaExisteCargadoFirmaElectronica(SesionUsuario.CodigoUsuario))
                {
                    oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                    direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;

                    urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                    nombreArchivo = "IV_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    nombreArchivoModPoa = "MOD_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    string FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                    nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "-signed.pdf";
                    int secuencial = 0;
                    while (existeArchivo(FilePath))
                    {
                        nombreDocumentoModificarFirmado = String.Empty;
                        nombreArchivoNuevo = String.Empty;
                        FilePath = String.Empty;
                        secuencial++;
                        nombreArchivoNuevo = nombreArchivo + "_" + secuencial + ".pdf";
                        FilePath = urlReporteElectronico + @"\" + nombreArchivoNuevo;
                        nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";
                    }

                    //crear el archivo
                    if (ElaborarInformeViabilidadModificacionPOAArchivoPdf(canio, numSolicitud, FilePath))
                    {
                        //Obtiene los datos del certificado electronico
                        certificadoFirma = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(SesionUsuario.CodigoUsuario);
                        //Firma el documento electronico
                        DocumentoFirmadoFirmaElectronica.Instancia.InsertaFirmaCertificadoPOADocumento(FilePath, nombreDocumentoModificarFirmado, SesionUsuario);
                        message = "ok";
                        estado = true;
                    }
                    else
                    {
                        message = "Se actualizo actualizo el POA, no se puedo generar el reporte";
                    }
                }
                else
                {
                    message = "La firma electrónica del Director de DPGE, no está cargado en el sistema";
                }


            }
            catch (Exception ex)
            {
                message = _campoError + " / " + ex.Message;
            }
            return estado;
        }

        public ActionResult LegalizaModificacionNegada(string canio, Int32 numSolicitud)
        {
            string message = string.Empty;
            string nombreArchivo = string.Empty;
            string urlReporteElectronico = string.Empty;
            string direccionDirectory = string.Empty;
            string nombreDocumentoModificarFirmado = string.Empty;
            string nombreArchivoModPoa = string.Empty;
            string nombreArchivoNuevo = string.Empty;
            string _campoError = string.Empty;
            //variables para la firma 
            string pathCertificado = string.Empty;
            string contrasena = string.Empty;


            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            DocumentoFirmado documentoFirmadoPdf = new DocumentoFirmado();
            tbCertificadoDigital certificadoFirma = new tbCertificadoDigital();
            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                if (ValidaExisteCargadoFirmaElectronica(SesionUsuario.CodigoUsuario))
                {
                    oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                    direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;

                    urlReporteElectronico = Constantes.poaURL + @"\" + direccionDirectory;
                    nombreArchivo = "IV_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    nombreArchivoModPoa = "MOD_" + oSolicitudPoa.TipoSolicitud.Trim() + "_" + oSolicitudPoa.CodigoDireccionPYGE + "_" + oSolicitudPoa.AnioSolicitud + "_" + oSolicitudPoa.NumeroSolicitud.ToString().Trim() + "_" + oSolicitudPoa.NumeroModificacion.ToString().Trim();

                    string FilePath = urlReporteElectronico + @"\" + nombreArchivo + ".pdf";
                    nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "-signed.pdf";
                    int secuencial = 0;
                    while (existeArchivo(FilePath))
                    {
                        nombreDocumentoModificarFirmado = String.Empty;
                        nombreArchivoNuevo = String.Empty;
                        FilePath = String.Empty;
                        secuencial++;
                        nombreArchivoNuevo = nombreArchivo + "_" + secuencial + ".pdf";
                        FilePath = urlReporteElectronico + @"\" + nombreArchivoNuevo;
                        nombreDocumentoModificarFirmado = urlReporteElectronico + @"\" + nombreArchivoModPoa + "_" + secuencial + "-signed.pdf";
                    }


                    //crear el archivo
                    if (ElaborarInformeViabilidadModificacionPOAArchivoPdf(canio, numSolicitud, FilePath))
                    {
                        //Obtiene los datos del certificado electronico
                        certificadoFirma = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(SesionUsuario.CodigoUsuario);
                        //Firma el documento electronico
                        DocumentoFirmadoFirmaElectronica.Instancia.InsertaFirmaCertificadoPOADocumento(FilePath, nombreDocumentoModificarFirmado, SesionUsuario);
                        message = "ok";
                    }
                    else
                    {
                        message = "No se puedo generar el reporte";
                    }
                }
                else
                {
                    message = "La firma electrónica del Director de DPGE, no está cargado en el sistema";
                }


            }
            catch (Exception ex)
            {
                message = _campoError + " / " + ex.Message;
            }
            return Content(message);
        }


        public iTextSharp.text.Image GetQRCode(string content, int qrSize)
        {
            iTextSharp.text.pdf.BarcodeQRCode qrcode = new iTextSharp.text.pdf.BarcodeQRCode(content, qrSize, qrSize, null);
            iTextSharp.text.Image img = qrcode.GetImage();
            return img;
        }
        #region "Certificado Firma electronica"

        /// <summary>
        /// Metodo firma el documento
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="nombreArchivo"></param>
        /// <param name="ousuario"></param>
        /// <returns></returns>
        private bool FirmaDocumentoTresFirmas(string canio, int numSolicitud, string nombreArchivo, tbUsuario ousuario)
        {
            bool estado = false;
            string urlDocumento = string.Empty;
            string urlDocumentoNuevo = string.Empty;
            string direccionDirectory = string.Empty;
            string certificatePath = String.Empty;
            string _url = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            tbCertificadoDigital oCertificadoAnalista = new tbCertificadoDigital();
            tbCertificadoDigital oCertificadoDirectorArea = new tbCertificadoDigital();
            tbCertificadoDigital oCertificadoFinanciero = new tbCertificadoDigital();
            Pkcs12Store storeAnalista = new Pkcs12Store();
            Pkcs12Store storeDirectorAerea = new Pkcs12Store();
            Pkcs12Store storeFinanciero = new Pkcs12Store();
            string pathCertificado = string.Empty;
            tbSistema oSistema = new tbSistema();
            string aliasAnalista = string.Empty;
            string aliasDirectorArea = string.Empty;
            string aliasFinanciero = string.Empty;
            ModelPagina modelPagina = new ModelPagina();

            BarcodeQRCode barcodeQRCodeFirmaAera;
            BarcodeQRCode barcodeQRCodeFirmaDirectorAerea;
            BarcodeQRCode barcodeQRCodeFirmaFinanciero;
            AsymmetricKeyEntry keyEntryAnalista;
            X509CertificateEntry[] chainAnalista;

            Image codeQRImageFirmaAera;
            Image codeQRImageFirmaDirector;
            Image codeQRImageFirmaFinanciero;


            float x = 115; // Posición X de la esquina inferior izquierda
            float y = 130; // Posición Y de la esquina inferior izquierda
            string textoQRCodeFirma = string.Empty;
            string fechaFirma = string.Empty;
            string documentoAttentamenteFinanciero = string.Empty;
            try
            {
                oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                fechaFirma = fechaDateAs400(oSistema.FechaSistema);
                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;

                oCertificadoAnalista = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(oSolicitudPoa.UsuarioCreaAnalista);
                oCertificadoDirectorArea = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(oSolicitudPoa.UsuarioDirectorAera);
                if (oSolicitudPoa.TipoSolicitud != "MAR")
                    oCertificadoFinanciero = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(oSolicitudPoa.UsuarioCreacionFIN_PRES);
                if (oCertificadoAnalista.PathDocumento.Length > 0 && oCertificadoDirectorArea.PathDocumento.Length > 0)
                {

                    string certificatePath1 = pathCertificado + @"\" + oCertificadoAnalista.PathDocumento;
                    string certificatePassword = SeguridadEncriptar.DesEncriptar(oCertificadoAnalista.Contrasena);

                    storeAnalista = storeCertificado(oCertificadoAnalista, oSolicitudPoa.UsuarioCreaAnalista);
                    _url = Constantes.poaURL + @"\" + direccionDirectory;
                    urlDocumento = _url + @"\" + nombreArchivo;
                    urlDocumentoNuevo = _url + @"\" + nombreArchivo.Substring(0, nombreArchivo.Length - 4) + "-signed.pdf";


                    storeAnalista = storeCertificado(oCertificadoAnalista, oSolicitudPoa.UsuarioCreaAnalista);
                    storeDirectorAerea = storeCertificado(oCertificadoDirectorArea, oSolicitudPoa.UsuarioDirectorAera);
                    if (oSolicitudPoa.TipoSolicitud != "MAR")
                    {
                        storeFinanciero = storeCertificado(oCertificadoFinanciero, oSolicitudPoa.UsuarioCreacionFIN_PRES);
                        aliasFinanciero = storeAnalista.Aliases.Cast<string>().FirstOrDefault(a => storeAnalista.IsKeyEntry(a));
                    }

                    if (storeAnalista != null && storeDirectorAerea != null)
                    {
                        aliasAnalista = storeAnalista.Aliases.Cast<string>().FirstOrDefault(a => storeAnalista.IsKeyEntry(a));
                        aliasDirectorArea = storeAnalista.Aliases.Cast<string>().FirstOrDefault(a => storeAnalista.IsKeyEntry(a));

                        //Analista de Area
                        keyEntryAnalista = storeAnalista.GetKey(aliasAnalista);
                        chainAnalista = storeAnalista.GetCertificateChain(aliasAnalista);


                        //CultureInfo cultures = new CultureInfo("en-US");
                        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
                        iTextSharp.text.Font negrita10 = new iTextSharp.text.Font(bfTimes, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font negritaBlack8 = new iTextSharp.text.Font(bfTimes, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        using (PdfReader pdfReader = new PdfReader(urlDocumento))
                        {

                            //funcion busca una palabra
                            modelPagina = ObtieneNumeroLineaBuscada(urlDocumento, "Total");
                            
                            // Crear un objeto de escritura para el documento modificado
                            using (var newFileStream = new FileStream(urlDocumentoNuevo, FileMode.Create))
                            {
                                // Crear un escritor de PDF
                                var pdfStamper = new PdfStamper(pdfReader, newFileStream);

                                
                                // Obtener la cantidad de páginas en el documento
                                int pageCount = pdfReader.NumberOfPages;
                                // Obtener la última página del documento
                                // var lastPage = pdfReader.GetPageN(pageCount);

                                //Agrega una nueva pagina
                                if(modelPagina.LineaActual > 55)
                                {
                                    //x = 350; // Posición X de la esquina inferior izquierda
                                    y = 750; // Posición Y de la esquina inferior izquierda
                                    pdfStamper.InsertPage(pageCount + 1, pdfReader.GetPageSizeWithRotation(1));
                                    pageCount = pageCount + 1;
                                }
                                

                                 //Firma primera
                                textoQRCodeFirma = "Firmado por." + aliasAnalista + "\n Date: " + DateTime.Parse(fechaFirma);
                                barcodeQRCodeFirmaAera = new BarcodeQRCode(textoQRCodeFirma, 1000, 1000, null);
                                codeQRImageFirmaAera = barcodeQRCodeFirmaAera.GetImage();
                                codeQRImageFirmaAera.ScalePercent(8f);
                                codeQRImageFirmaAera.SetAbsolutePosition(20, 20);
                                PdfContentByte cb = pdfStamper.GetOverContent(pageCount);

                                string documentoAttentamenteAnalista = "\n\nDocumento firmado electrónicamente" + "\n" + aliasAnalista + "\n" + DateTime.Parse(fechaFirma);
                                string documentoAttentamenteDirector = "\n\nDocumento firmado electrónicamente" + "\n" + aliasDirectorArea + "\n" + DateTime.Parse(fechaFirma);
                                if (oSolicitudPoa.TipoSolicitud != "MAR")
                                    documentoAttentamenteFinanciero = "\n\nDocumento firmado electrónicamente" + "\n" + aliasFinanciero + "\n" + DateTime.Parse(fechaFirma);



                                PdfPTable tableFirma = new PdfPTable(new float[] { 8f, 20f, 8f, 20f, 8f, 20f }); //{ WidthPercentage = 80f }

                                var cell1 = new PdfPCell(codeQRImageFirmaAera);
                                var cell2 = new PdfPCell(new Phrase(documentoAttentamenteAnalista, negritaBlack8));
                                cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                cell2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                cell1.Border = 0;
                                cell2.Border = 0;
                                tableFirma.AddCell(cell1);
                                tableFirma.AddCell(cell2);
                                //firma 2
                                //Firma primera
                                textoQRCodeFirma = "Firmado por." + aliasDirectorArea + "\n Date: " + DateTime.Parse(fechaFirma);
                                barcodeQRCodeFirmaDirectorAerea = new BarcodeQRCode(textoQRCodeFirma, 1000, 1000, null);
                                codeQRImageFirmaDirector = barcodeQRCodeFirmaDirectorAerea.GetImage();
                                codeQRImageFirmaDirector.ScalePercent(8f);
                                codeQRImageFirmaDirector.SetAbsolutePosition(80, 70);
                                cell1 = new PdfPCell(codeQRImageFirmaDirector);
                                cell2 = new PdfPCell(new Phrase(documentoAttentamenteDirector, negritaBlack8));
                                cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                cell2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                cell1.Border = 0;
                                cell2.Border = 0;
                                tableFirma.AddCell(cell1);
                                tableFirma.AddCell(cell2);
                                //firma 3
                                if (oSolicitudPoa.TipoSolicitud != "MAR")
                                {
                                    textoQRCodeFirma = "Firmado por." + aliasFinanciero + "\n Date: " + DateTime.Parse(fechaFirma);
                                    barcodeQRCodeFirmaFinanciero = new BarcodeQRCode(aliasFinanciero, 1000, 1000, null);
                                    codeQRImageFirmaFinanciero = barcodeQRCodeFirmaFinanciero.GetImage();
                                    codeQRImageFirmaFinanciero.ScalePercent(8f);
                                    codeQRImageFirmaFinanciero.SetAbsolutePosition(80, 70);
                                    cell1 = new PdfPCell(codeQRImageFirmaFinanciero);
                                    cell2 = new PdfPCell(new Phrase(documentoAttentamenteFinanciero, negritaBlack8));
                                    cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    cell2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                    cell1.Border = 0;
                                    cell2.Border = 0;
                                    tableFirma.AddCell(cell1);
                                    tableFirma.AddCell(cell2);
                                }

                                //agrega a la tabla responsables


                                if (oSolicitudPoa.TipoSolicitud != "MAR")
                                {
                                    cell1 = new PdfPCell(new Phrase("ANALISTA RESPONSABLE", negritaBlack8)) { Colspan = 2 };
                                    //cell2 = new PdfPCell(new Phrase(""));
                                    cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                    cell1.Border = 0;
                                    cell2.Border = 0;
                                    tableFirma.AddCell(cell1);
                                }
                                else
                                {
                                    cell1 = new PdfPCell(new Phrase("", negritaBlack8)) { Colspan = 2 };
                                    cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                    cell1.Border = 0;
                                    cell2.Border = 0;
                                    tableFirma.AddCell(cell1);
                                }

                                //tableFirma.AddCell(cell2);
                                //agrega a la tabla responsables
                                cell1 = new PdfPCell(new Phrase("Aprobado\n" + "DIRECTOR(A) DE " + oSolicitudPoa.UsuarioCreaAnalista, negritaBlack8)) { Colspan = 2 };
                                //cell2 = new PdfPCell(new Phrase(""));
                                cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                cell1.Border = 0;
                                cell2.Border = 0;
                                if (oSolicitudPoa.TipoSolicitud != "MAR")
                                {
                                    tableFirma.AddCell(cell1);
                                    //tableFirma.AddCell(cell2);
                                    //agrega a la tabla responsables
                                    cell1 = new PdfPCell(new Phrase("Verificado\n" + "ANALISTA FINANCIERO", negritaBlack8)) { Colspan = 2 };
                                    //cell2 = new PdfPCell(new Phrase(""));
                                    cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                    cell1.Border = 0;
                                    cell2.Border = 0;
                                    tableFirma.AddCell(cell1);
                                }
                                // Configurar la posición de la tabla en la página (en este caso, en la esquina inferior izquierda)
                                tableFirma.TotalWidth = 1000f; // Ancho total de la tabla
                                tableFirma.WriteSelectedRows(0, -1, x, y, pdfStamper.GetOverContent(pageCount)); // Coordenadas X e Y

                                // Cerrar el escritor de PDF
                                pdfStamper.Close();
                                estado = true;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return estado;
        }
        public JsonResult VerificaExisteFirmasElectronicas(string canio, int numSolicitud)
        {
            bool estado = false;
            string mensajeFirma = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();

            oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);

            SesionUsuario = (tbUsuario)Session["Usuario"];
            if (ValidaExisteCargadoFirmaElectronica(oSolicitudPoa.UsuarioCreaAnalista))
            {
                if (oSolicitudPoa.UsuarioDirectorAera == "")
                    oSolicitudPoa.UsuarioDirectorAera = SesionUsuario.CodigoUsuario;

                if (ValidaExisteCargadoFirmaElectronica(oSolicitudPoa.UsuarioDirectorAera))
                {

                    if (oSolicitudPoa.TipoSolicitud != "MAR")
                    {
                        if (ValidaExisteCargadoFirmaElectronica(oSolicitudPoa.UsuarioCreacionFIN_PRES))
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                            mensajeFirma = "La firma electrónica del Financiero Presupuestario no está cargado en el sistema";
                        }
                    }
                    else
                        estado = true;
                }
                else
                {
                    estado = false;
                    mensajeFirma = "La firma electrónica del Director de Aérea requirente no está cargado en el sistema";
                }

            }
            else
            {
                estado = false;
                mensajeFirma = "La firma electrónica del Analista de Aérea requirente no está cargado en el sistema";
            }
            return Json(new { resultado = estado, mensaje = mensajeFirma }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnviaAprobarSolicitudModificacionPOAaDPGE(string canio, int numSolicitud, string nombreArchivo)
        {
            bool estado = false;
            string mensajeFirma = string.Empty;
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();
            SesionUsuario = (tbUsuario)Session["Usuario"];

            try
            {
                oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);
                if (CD_SolicitudPOA.Instancia.AprobarEnviarSolicitudADPGE(oSolicitudPoa.AnioSolicitud, oSolicitudPoa.NumeroSolicitud, oSolicitudPoa.CodigoUnidadEjecucion, oSolicitudPoa.CodigoDireccionPYGE, SesionUsuario.CodigoUsuario))
                {
                    nombreArchivo = GeneraReporteSolicitudModificacionPoa(canio, numSolicitud);

                    //Procede a generar el reporte y guardar 
                    if (nombreArchivo.Trim().Length > 0)
                    {
                        if (FirmaDocumentoTresFirmas(canio, numSolicitud, nombreArchivo, SesionUsuario))
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                            mensajeFirma = "No se puedo firmar el documento";
                        }
                    }
                    else
                    {
                        estado = false;
                        mensajeFirma = "No se puedo generar el reporte de la solicitud de la modificación POA";
                    }

                }

            }
            catch (Exception ex)
            {
                estado = false;
                mensajeFirma = ex.Message;
            }

            return Json(new { resultado = estado, mensaje = mensajeFirma }, JsonRequestBehavior.AllowGet);
        }


        private bool ValidaExisteCargadoFirmaElectronica(string codUsuario)
        {
            tbSistema osistema = new tbSistema();
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            DateTime dtFechaSistema;
            DateTime dtFechaVigencia;
            bool estado = false;
            try
            {
                if (Session["Usuario"] != null)
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    //var ousuario = (tbUsuario)Session["Usuario"];

                    oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(codUsuario);
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
                                    CD_CertificadoDigital.Instancia.CertificadoDigitalCambiaEstado("IA", codUsuario, oCertificado.Secuencial);
                                    estado = false;
                                }
                            }
                            else
                            {
                                dtFechaVigencia = DateTime.Parse(fechaDateAs400(oCertificado.FechaVencimiento));
                                dtFechaSistema = DateTime.Parse(fechaDateAs400(osistema.FechaSistema));

                                if (dtFechaVigencia <= dtFechaSistema)
                                {
                                    CD_CertificadoDigital.Instancia.CertificadoDigitalCambiaEstado("IA", codUsuario, oCertificado.Secuencial);
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
            return estado;
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
                    System.IO.File.SetAttributes(urlDocumentos,
                        (new FileInfo(urlDocumentos)).Attributes | FileAttributes.ReadOnly);
                    estado = true;
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
        private Pkcs12Store storeCertificado(tbCertificadoDigital ocertificado, string ousuario)
        {
            string pathCertificado = string.Empty;
            string pathAutorizacion = string.Empty;
            tbCertificadoDigital ocertificadoDigital = new tbCertificadoDigital();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store ostoreCertificado = new Pkcs12Store();
            try
            {
                pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + ousuario;

                string certificatePath = pathCertificado + @"\" + ocertificado.PathDocumento;
                string certificatePassword = SeguridadEncriptar.DesEncriptar(ocertificado.Contrasena);
                // Cargar el certificado                
                ostoreCertificado = new Pkcs12Store(new FileStream(@certificatePath, FileMode.Open, FileAccess.Read), certificatePassword.ToCharArray());
                string alias = ostoreCertificado.Aliases.Cast<string>().FirstOrDefault(a => ostoreCertificado.IsKeyEntry(a));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ostoreCertificado;
        }

        #endregion


        #region "Modificación POA Con Asignación de Recursos"
        public ActionResult ListarConAsignacionRecursos()
        {
            string direccionDirectory = string.Empty;

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbSolicitudPOA> listSolicitudPoa = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            Session["Controlador"] = "ModificacionPoa";
            Session["ActionResul"] = "ListarConAsignacionRecursos";
            Session["CodigoRol"] = SesionUsuario.CodigoRol;

            listSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitudModificacionPoaConAsignacionRecursosListar(SesionUsuario.CodigoSubsistema, cAnio);
            foreach (var itemSol in listSolicitudPoa)
            {
                direccionDirectory = @itemSol.CodigoDireccionPYGE + @"\" + @itemSol.TipoSolicitud + @"\" + @itemSol.AnioSolicitud + @"\" + @itemSol.NumeroSolicitud;
                itemSol.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
                if (itemSol.oModelArchivo.Count() > 0)
                {
                    itemSol.numeroDocumentoAdjunto = 1;
                }
                direccionDirectory = string.Empty;
            }
            string ipUsuario = ObtenerIPs();
            return View(listSolicitudPoa);
        }
        #endregion

        private SelectList GetSelectListValoresVerificaPresupuesto(string campo)
        {
            System.Collections.Generic.List<SelectListItem> list = new System.Collections.Generic.List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));
            string[] stringArray = { "DP", "VP" };
            foreach (var item in olistTipoValores)
            {
                var check = Array.Exists(stringArray, x => x == item.Codigo.Trim());
                if (check == true)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Descripcion.Trim(),
                        Value = item.Codigo.Trim()
                    });
                }

            }
            var seleccion = new SelectListItem()
            {
                Value = "",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListValoresAprobarModificacionReforma(string campo)
        {
            System.Collections.Generic.List<SelectListItem> list = new System.Collections.Generic.List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));
            string[] stringArray = { "AP", "DV" };
            foreach (var item in olistTipoValores)
            {
                var check = Array.Exists(stringArray, x => x == item.Codigo.Trim());
                if (check == true)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Descripcion.Trim(),
                        Value = item.Codigo.Trim()
                    });
                }

            }
            var seleccion = new SelectListItem()
            {
                Value = "",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }


        private SelectList GetSelectListValoresSolicitudAutorizacionPoa(string campo)
        {
            System.Collections.Generic.List<SelectListItem> list = new System.Collections.Generic.List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));
            string[] stringArray = { "RA", "RD", "RN" };
            foreach (var item in olistTipoValores)
            {
                var check = Array.Exists(stringArray, x => x == item.Codigo.Trim());
                if (check == true)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Descripcion.Trim(),
                        Value = item.Codigo.Trim()
                    });
                }

            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        /// <summary>
        /// Lista de valores de campos SOLES7
        /// </summary>
        /// <param name="campo"></param>
        /// <returns></returns>
        private SelectList GetSelectListarValoresSolicitudAutorizacionPoaTodos(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));
            foreach (var item in olistTipoValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Descripcion.Trim(),
                    Value = item.Codigo.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        #region "MODIFICACION POA CON AFECTACION PRESUPUESTARIA"
        public ActionResult SolModificacionPoaConAfectacionPresupuestaria()
        {
            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            tbSistema oSistema = new tbSistema();
            string cAnio = string.Empty;

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                cAnio = oSistema.FechaSistema.Substring(0, 4);
                Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
                Session["Controlador"] = "ModificacionPoa";
                Session["ActionResul"] = "SolModificacionPoaConAfectacionPresupuestaria";
                Session["CodigoRol"] = SesionUsuario.CodigoRol;
                listado = CD_SolicitudPOA.Instancia.SolicitudModificacionPoaConAfectacionPresupetariaListarDireccionAnio(SesionUsuario.CodigoSubsistema, cAnio);
                //Verifica que si ya tiene subido los archivos
                foreach (var item in listado)
                {
                    string direccionDirectory = item.CodigoDireccionPYGE + @"\" + item.TipoSolicitud + @"\" + item.AnioSolicitud + @"\" + item.NumeroSolicitud;
                    string pathDirectorio = Constantes.poaURL + @"\" + direccionDirectory;
                    if (GetVerifcaExisteArchivosDirectorio(pathDirectorio))
                        item.numeroDocumentoAdjunto = 1;
                    else
                        item.numeroDocumentoAdjunto = 0;
                }
            }
            catch (Exception ex)
            {
                listado = null;
                ViewBag.DireccionDirectory = "Error, al cargar los datos de la solictud de modificación Poa con afectación presupetaria, " + ex.Message;
            }
            return View(listado);
        }



        public ActionResult ConsultarSolicitudModificacionPoa(string canio, int numSolicitud, string vista)
        {
            string direccionDirectory = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            tbSolicitudPOA oSolicitudPoa = new tbSolicitudPOA();

            string nombreArchivoPrueba = string.Empty;
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            SesionUsuario = (tbUsuario)Session["Usuario"];
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            oSolicitudPoa = CD_SolicitudPOA.Instancia.SolicitarCertificadoPOAPorAnioNumeroSolicitud(canio, numSolicitud);

            direccionDirectory = @oSolicitudPoa.CodigoDireccionPYGE + @"\" + @oSolicitudPoa.TipoSolicitud + @"\" + canio + @"\" + numSolicitud;
            nombreArchivoPrueba = oSolicitudPoa.CodigoDireccionPYGE + "_" + @oSolicitudPoa.TipoSolicitud + "_" + canio + @"_" + numSolicitud;
            Session["ActionResul"] = vista;
            Session["DireccionPath"] = direccionDirectory;
            ViewBag.DireccionDirectory = Constantes.poaURL + @"\" + direccionDirectory;
            oSolicitudPoa.CodigoRolPYGE = SesionUsuario.CodigoRol;
            oSolicitudPoa.oModelArchivo = GetObtenerTodosArchivosPath(Constantes.poaURL + @"\" + direccionDirectory);
            
            if (oSolicitudPoa.oModelArchivo.Count() > 0)
            {
                oSolicitudPoa.numeroDocumentoAdjunto = 1;
            }
            return View(oSolicitudPoa);
        }

        /// <summary>
        /// Metodo descargar el archivo
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="direccion"></param>
        /// <returns></returns>
        public ActionResult DescargarArchivo(string nombreArchivo, string direccion)
        {
            try
            {
                string fullName = Constantes.poaURL.Trim() + @"\" + direccion.Trim() + @"\" + nombreArchivo;
                byte[] fileBytes = GetFile(fullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nombreArchivo);

            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("No se pudo presentar el archivo solicitado." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un problema al descargar el archivo." + ex.Message);
            }

        }

        public ActionResult VisualizarArchivo(string nombreArchivo, string direccion)
        {

            string fullName = Constantes.poaURL + @"\" + direccion.Trim() + @"\" + nombreArchivo.Trim();

            byte[] fileBytes = GetFile(fullName);

            return new FileContentResult(fileBytes, "application/pdf");
        }


        #endregion

        #region "MODIFICACION POA CON ASIGNACION DE RECURSOS"

        public ActionResult SolModificacionPoaConAsignacionRecursos()
        {
            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            tbSistema oSistema = new tbSistema();
            string cAnio = string.Empty;

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            try
            {
                SesionUsuario = (tbUsuario)Session["Usuario"];
                oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                cAnio = oSistema.FechaSistema.Substring(0, 4);
                Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
                Session["Controlador"] = "ModificacionPoa";
                Session["ActionResul"] = "SolModificacionPoaConAsignacionRecursos";
                Session["CodigoRol"] = SesionUsuario.CodigoRol;
                listado = CD_SolicitudPOA.Instancia.SolicitudModificacionPoaConAsignacionRecursosListarDireccionAnio(SesionUsuario.CodigoSubsistema, cAnio);
                //Verifica que si ya tiene subido los archivos
                foreach (var item in listado)
                {
                    string direccionDirectory = item.CodigoDireccionPYGE + @"\" + item.TipoSolicitud + @"\" + item.AnioSolicitud + @"\" + item.NumeroSolicitud;
                    string pathDirectorio = Constantes.poaURL + @"\" + direccionDirectory;
                    if (GetVerifcaExisteArchivosDirectorio(pathDirectorio))
                        item.numeroDocumentoAdjunto = 1;
                    else
                        item.numeroDocumentoAdjunto = 0;
                }
            }
            catch (Exception ex)
            {
                listado = null;
                ViewBag.DireccionDirectory = "Error, al cargar los datos de la solictud de modificación Poa con Asignación de Recursos, " + ex.Message;
            }
            return View(listado);
        }



        #endregion


        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
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

        public string ObtenerIPs()
        {
            StringBuilder sb = new StringBuilder();
            string ipAddresses;

            try
            {
                var hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                foreach (IPAddress address in addresses)
                    sb.Append($"{address}, ");

                ipAddresses = sb.ToString().TrimEnd(", ".ToCharArray());
            }
            catch (Exception ex)
            {
                ipAddresses = "ERROR: " + ex.Message;
            }
            return ipAddresses;
        }


        /// <summary>
        /// Metodo retorna la poseción de la linea encontrada 
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <param name="palabraBuscar"></param>
        /// <returns>Int</returns>
        private ModelPagina ObtieneNumeroLineaBuscada(string pdfPath, string palabraBuscar)
        {
            string linea;
            int numeroLinea = 0;
            ModelPagina modelPagina = new ModelPagina();
            try
            {
                using (PdfReader reader = new PdfReader(pdfPath))
                {
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        string textoPagina = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, page);
                        using (StringReader sr = new StringReader(textoPagina))
                        {
                            linea = string.Empty;
                            if (numeroLinea > 0)
                                break;
                            numeroLinea = 0;
                            while ((linea = sr.ReadLine()) != null)
                            {
                                numeroLinea++;
                                if (linea.Contains(palabraBuscar))
                                {
                                    modelPagina.PaginaActual = page;
                                    modelPagina.LineaActual = numeroLinea;
                                   // Console.WriteLine($"La palabra '{palabraBuscar}' se encontró en la página {page}, línea {numeroLinea}:");
                                    //Console.WriteLine(linea);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                numeroLinea = 0;
            }
            return modelPagina;
        }
    }
}