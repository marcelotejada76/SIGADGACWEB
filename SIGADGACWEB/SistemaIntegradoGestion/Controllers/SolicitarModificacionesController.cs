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

namespace SistemaIntegradoGestion.Controllers
{

    public class SolicitarModificacionesController : Controller
    {

        private static tbUsuario SesionUsuario;

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
            Session["ActionResul"] = "EnviarSolicitudCertificacionPOA";
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

        public ActionResult DocumentoHabilitante(string cdireccion, string canio, string tipoSolicitud, string numSolicitud, string estadoAutorizado)
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

            string fullName = Constantes.poaURL + direccion + @"\" + nombreArchivo;

            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nombreArchivo);
        }

        public ActionResult VisualizarFile(string nombreArchivo, string direccion)
        {

            string fullName = Constantes.poaURL + direccion + @"\" + nombreArchivo;

            byte[] fileBytes = GetFile(fullName);

            return new FileContentResult(fileBytes, "application/pdf");
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

        public ActionResult ExportToPDF(string canio, Int32 numSolicitud, string cdireccion, string estAut, string cobservacion, string cobservacion1)
        {
            string ssrsurl = ConfigurationManager.AppSettings["SSRSRReportsUrl"].ToString();
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

                if (CD_SolicitudPOA.Instancia.ApruebaSolicitudCertificadoPOA(canio, numSolicitud, estAut, ousuario.CodigoUsuario, cobservacion, cobservacion1))
                {
                    viewer.ProcessingMode = ProcessingMode.Remote;
                    viewer.SizeToReportContent = true;
                    viewer.AsyncRendering = true;
                    viewer.ServerReport.ReportServerUrl = new Uri(ssrsurl);
                    viewer.ServerReport.ReportPath = "/Report Project1/CertificadoPOA";
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
                                ViewBag.mensajeError = "El certificado ya existe no es necesario cargar de nuevo";
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
                                estado = CD_CertificadoDigital.Instancia.CertificadoDigitalActualizar(model);
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

                    }


                }
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
            try
            {
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                var ousuario = (tbUsuario)Session["Usuario"];
                pathAutorizacion = Constantes.poaURL + directorio;
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
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;


                        //numPage = reader.NumberOfPages;                       
                        // Configurar la apariencia de la firma

                        //appearance.Reason = SignReason;
                        //appearance.Location = SignLocation;
                        string fechaFirma = Utilitarios.Utilitario.Instancia.fechaDateAs400(oSistema.FechaSistema) + " " + oSistema.HoraSistema;  //DateTime.Now.Date.ToString();
                        appearance.SignDate = DateTime.Parse(fechaFirma); // DateTime.Now.Date;
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(40, 150, 300, 190), numPage, null);//.IsInvisible
                                                                                                                        //        Left = 50,//Top = 150,Width = 200, Height = 200 

                        opiedeFirma = "FIRMADO POR:";

                        //

                        //Custom text and background image
                        appearance.Image = GetQRCode(opiedeFirma + alias + "\n" + ousuario.Cargo + "\n Date: " + fechaFirma);
                        appearance.ImageScale = 0.6f;
                        //appearance.Image.Alignment = 250;
                        appearance.Acro6Layers = true;

                        StringBuilder buf = new StringBuilder();
                        buf.Append("Documento firmado electrónicamente\n");
                        String name = alias;
                        string cargo = ousuario.Cargo;
                        buf.Append(name).Append('\n');
                        buf.Append(cargo).Append('\n');

                        buf.Append("Date: ").Append(fechaFirma);

                        string text = buf.ToString();

                        appearance.Layer2Text = text;


                        appearance.Acro6Layers = true;

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
                        estado = true;
                        estado = ValidaCertificadoFirmaExiste(oCertificado.CodigoUsuario, oCertificado.PathDocumento);
                    }
                }
            }
            catch
            {
                estado = false;
            }
            return Json(new { resultado = estado }, JsonRequestBehavior.AllowGet);
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

    }
}