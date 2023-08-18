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
            TempData["ActionResul"] = "AfectacionPresupuestaria";
            TempData["TituloActionResul"] = "Afectación presupuestaria";
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
            TempData["ActionResul"] = "AsignacionRecursos";
            TempData["TituloActionResul"] = "Asignación recursos";
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
            TempData["ActionResul"] = "ReprogramarSoloPOA";
            TempData["TituloActionResul"] = "Reprogramar solo POA";
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
            

            return View(listArchivo);
        }

        [HttpPost]
        public ActionResult DocumentoHabilitante(HttpPostedFileBase documentFile, string Directory)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            string nombreArchivo = "";
            ViewBag.DireccionDirectory = Directory;
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
    }
}