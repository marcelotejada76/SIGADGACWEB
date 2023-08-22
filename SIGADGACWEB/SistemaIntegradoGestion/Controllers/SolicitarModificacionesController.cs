using CapaDatos;
using CapaModelo;
using SistemaIntegradoGestion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
   
    public class SolicitarModificacionesController : Controller
    {
        const string urlPoa = @"\\172.20.16.90\Sigpoa";
        private static tbUsuario SesionUsuario;
        // GET: SolicitarModificaciones
        public ActionResult AfectacionPresupuestaria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            List<tbSolicitudPOA> listado = new List<tbSolicitudPOA>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_SolicitudPOA.Instancia.SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MOD");
            Session["ActionResul"]  = "AfectacionPresupuestaria";
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
            ViewBag.DescripcionSubSistema  = SesionUsuario.DescripcionSubSistema;
            Session["ActionResul"] = "ReprogramarSoloPOA";
            Session["TituloActionResul"] = "Reprogramar solo POA";
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            return View(listado);
        }

        public ActionResult DocumentoHabilitante(string cdireccion, string canio, string tipoSolicitud, string numSolicitud)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<ModelArchivo> listArchivo = new List<ModelArchivo>();
            string direccionDirectory = @"\" + cdireccion + @"\" + canio + @"\" + tipoSolicitud + @"\" + numSolicitud;
            listArchivo = GetObtenerTodosArchivos(direccionDirectory);
            ViewBag.DireccionDirectory = direccionDirectory;
            Session["DireccionSubSistema"] = SesionUsuario.DescripcionSubSistema.Trim().ToUpper();
            return View(listArchivo);
        }

        [HttpPost]
        public ActionResult DocumentoHabilitante(HttpPostedFileBase documentFile, string Directory)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            string nombreArchivo = "";
            ViewBag.DireccionDirectory = Directory;
            //TempData["ActionResul"] = actionResul;
            //TempData["TituloActionResul"] = tituloActionResul;  , string actionResul, string tituloActionResul)


            List<ModelArchivo> listArchivo = new List<ModelArchivo>();
            if (documentFile != null && documentFile.ContentLength > 0)
            {
                string  urlDocumentos = urlPoa + Directory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlDocumentos))
                    System.IO.Directory.CreateDirectory(urlDocumentos);

                nombreArchivo = documentFile.FileName;
                documentFile.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));
                listArchivo = GetObtenerTodosArchivos(Directory);
            }

            return View(listArchivo);
        }

        private List<ModelArchivo> GetObtenerTodosArchivos(string directory)
        {
            string carpetaPoa = string.Empty;
            string urlDocumentos = string.Empty;
            List<ModelArchivo> listArchivo = new List<ModelArchivo>();

            if (directory.Length > 0)
            {
                string direccionDirectory = directory;

                ViewBag.DireccionDirectory = direccionDirectory;
               
                urlDocumentos = urlPoa + direccionDirectory;

                if (!System.IO.Directory.Exists(urlDocumentos))
                    System.IO.Directory.CreateDirectory(urlDocumentos);

                if (System.IO.Directory.Exists(urlDocumentos))
                {
                    string[] files = System.IO.Directory.GetFiles(urlDocumentos);

                    if (files.Length > 0)
                    {
                        for (int iFile = 0; iFile < files.Length; iFile++)
                        {
                            ModelArchivo archvo = new ModelArchivo();

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
        public JsonResult EliminarDocumento(string nombreArchivo, string direccion)
        {
            bool respuesta;
            string fullPath = string.Empty;
            if (Session["Usuario"] != null)
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                fullPath = urlPoa + direccion + @"\" + nombreArchivo;
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
            
            string fullName = urlPoa + direccion + @"\" + nombreArchivo;

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fullName);
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


        public FileResult GetDowload(string url)
        {
            byte[] FileBytes = null;
            try
            {
                string ReportURL = @"\\172.20.16.90\vuelos_charter\AdjuntosCharter\" + url;
                FileBytes = System.IO.File.ReadAllBytes(ReportURL);
                return File(FileBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}