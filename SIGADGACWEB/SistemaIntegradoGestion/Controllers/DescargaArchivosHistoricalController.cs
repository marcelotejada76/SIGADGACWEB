using CapaDatos;
using CapaModelo;
using SistemaIntegradoGestion.Utilitarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class DescargaArchivosHistoricalController : Controller
    {
        private readonly string rutaProcesados = @"\\172.20.19.55\ProcesarHistoricalIndra\PROCESADOS";
        

        public ActionResult Index(string Nombre = "")
        {
            return ListadoDescargaArchivosHistorical(Nombre);
        }

        [HttpGet]
        public ActionResult ListadoArchivosHistorical(string Nombre = "")
        {
            return ListadoDescargaArchivosHistorical(Nombre);
        }

        [HttpPost]
        public ActionResult ListadoArchivosHistorical(string Nombre, HttpPostedFileBase dummy = null)
        {
            return ListadoDescargaArchivosHistorical(Nombre);
        }

        [HttpGet]
        public ActionResult ListadoDescargaArchivosHistorical(string Nombre = "")
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbDocumentosDescarga> listado = ObtenerArchivosProcesados(Nombre);
            return View("ListadoDescargaArchivosHistorical", listado);
        }

        [HttpPost]
        public ActionResult ListadoDescargaArchivosHistorical(string Nombre, HttpPostedFileBase dummy = null)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbDocumentosDescarga> listado = ObtenerArchivosProcesados(Nombre);
            return View("ListadoDescargaArchivosHistorical", listado);
        }

        private List<tbDocumentosDescarga> ObtenerArchivosProcesados(string filtroNombre)
        {
            List<tbDocumentosDescarga> listado = new List<tbDocumentosDescarga>();

            try
            {
                if (Directory.Exists(rutaProcesados))
                {
                    string[] files = Directory.GetFiles(rutaProcesados);
                    int secuencia = 1;

                    foreach (var filePath in files)
                    {
                        string fileName = Path.GetFileName(filePath);

                        if (string.IsNullOrEmpty(filtroNombre) || fileName.IndexOf(filtroNombre.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            listado.Add(new tbDocumentosDescarga
                            {
                                NombreArchivo = fileName,
                                Secuencia = secuencia++,
                                Estado = "A"
                            });
                        }
                    }
                }
                else
                {
                    ViewBag.mensajeError = "No se pudo acceder a la carpeta compartida en " + rutaProcesados;
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al obtener archivos de la carpeta compartida: " + ex.Message;
            }

            return listado;
        }

        public ActionResult DescargaDcto(string Nombre)
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                return HttpNotFound("Nombre de archivo no v\u00E1lido.");
            }

            try
            {
                string remoteUri = Path.Combine(rutaProcesados, Nombre);

                if (!System.IO.File.Exists(remoteUri))
                {
                    return HttpNotFound("El archivo solicitado no existe en la carpeta compartida.");
                }

                byte[] fileBytes = GetFile(remoteUri);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Nombre);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("No se pudo presentar el archivo solicitado.");
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un problema al descargar el archivo: " + ex.Message);
            }
        }

        private byte[] GetFile(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                byte[] data = new byte[fs.Length];
                int br = fs.Read(data, 0, data.Length);
                if (br != fs.Length)
                    throw new System.IO.IOException(path);
                return data;
            }
        }
    }
}