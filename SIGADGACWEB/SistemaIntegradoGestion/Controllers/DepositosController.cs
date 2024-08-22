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

namespace SistemaIntegradoGestion.Controllers
{
    public class DepositosController : Controller
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
          //  Session["CodigoRol"] = SesionUsuario.CodigoRol;
            listado = CD_Depositos.Instancia.DetalleDepositos(cAnio, SesionUsuario.NumeroRuc);

            return View(listado);
        }

        //consulta
        public ActionResult ConsultaDepositosClientes()

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
            listado = CD_Depositos.Instancia.ConsultaDepositos(cAnio);


            return View(listado);
        }

        public ActionResult ConsultaDocumentosDepositos(string Año, string Mes, string UsuarioRuc)
        {
            string direccionDirectory = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            //tbSubirDepositos oArchivo = new tbSubirDepositos();
            tbSubirDepositos CargaArchivo = new tbSubirDepositos();
            CargaArchivo.Año = Año;
            CargaArchivo.Mes = Mes;
            CargaArchivo.UsuarioRuc = UsuarioRuc;
            direccionDirectory = Año + @"\" + Mes + @"\" + UsuarioRuc;
            ViewBag.direccionDirectory = direccionDirectory;
            CargaArchivo.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);

            return View(CargaArchivo);
        }
        public ActionResult SubirDocumentosDepositos(string Año, string Mes, string UsuarioRuc)
        {
            string direccionDirectory = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            //tbSubirDepositos oArchivo = new tbSubirDepositos();
            tbSubirDepositos CargaArchivo = new tbSubirDepositos();
            CargaArchivo.Año = Año;
            CargaArchivo.Mes = Mes;
            CargaArchivo.UsuarioRuc = UsuarioRuc;
            direccionDirectory = Año + @"\" + Mes + @"\" + UsuarioRuc;
            ViewBag.direccionDirectory = direccionDirectory;
            CargaArchivo.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);

            return View(CargaArchivo);
        }

        [HttpPost]

        public ActionResult SubirDocumentosDepositos(string Año, string Mes, string UsuarioRuc, HttpPostedFileBase documentFile)
        {
            string direccionDirectory = string.Empty;
            //List<tbModelArchivo> listArchivo = new List<tbModelArchivo>();
            //tbSubirDepositos oArchivo = new tbSubirDepositos();
            tbSubirDepositos CargaArchivo = new tbSubirDepositos();
            CargaArchivo.Año = Año;
            CargaArchivo.Mes = Mes;
            CargaArchivo.UsuarioRuc = UsuarioRuc;
            guardarDocumento(Año, Mes, UsuarioRuc, documentFile);
            direccionDirectory = Año + @"\" + Mes + @"\" + UsuarioRuc;
            CargaArchivo.oModelArchivo = GetObtenerTodosArchivos(direccionDirectory);
            ViewBag.direccionDirectory = direccionDirectory;

            return View(CargaArchivo);
        }
        public bool guardarDocumento(string Año, string Mes, string UsuarioRuc, HttpPostedFileBase documentFile)
        {
            bool existearchivo = false;
            string direccionDirectory = Año + @"\" + Mes + @"\" + UsuarioRuc;
            string nombreArchivo = "";
            if (documentFile != null && documentFile.ContentLength > 0)
            {
                string urlDocumentos = Constantes.DepositosURL + @"\" + direccionDirectory;
                //Verifica si existe la carpeta creada si no lo crear
                if (!System.IO.Directory.Exists(urlDocumentos))
                    System.IO.Directory.CreateDirectory(urlDocumentos);


                nombreArchivo = documentFile.FileName;
                if (!System.IO.File.Exists(urlDocumentos + @"\" + nombreArchivo))
                {
                    documentFile.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));


                    //graba en la tabla el numero de registros
                    string[] sArchivos; //array con los nombres de archivos y carpetas
                    sArchivos = Directory.GetFiles(urlDocumentos);

                    //número de archivos en el directorio
                    int registros = sArchivos.Length;

                    CD_Depositos.Instancia.ActualizaRegistros(Año, SesionUsuario.NumeroRuc, Mes, registros);
                    existearchivo = true;
                }
                else
                {
                    ViewBag.mensajeError = "El archivo ya existe no puede actualizar, cambie de nombre";
                }

            }
            return existearchivo;
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

                urlDocumentos = Constantes.DepositosURL + @"\" + direccionDirectory;

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



        public ActionResult VisualizarDepositos(string nombreArchivo, string direccion)
        {

            string fullName = Constantes.DepositosURL + @"\" + direccion.Trim() + @"\" + nombreArchivo.Trim();

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

        public ActionResult DownloadFileDeposito(string nombreArchivo, string direccion)
        {
            try
            {
                string fullName = Constantes.DepositosURL + @"\" + direccion.Trim() + @"\" + nombreArchivo;
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



        [HttpGet]
        public JsonResult EliminarDepositoServidor(string nombreArchivo, string direccion)
        {
            char[] delimiterChars = { '\\' };

            string[] words = direccion.Split(delimiterChars);
            string año = "";
            string mes = "";
            string ruc = "";

            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                     año = words[i];
                }
                if (i == 1)
                {
                    mes = words[i];
                }
                if (i == 2)
                {
                     ruc = words[i];
                }

            }

            bool respuesta;
            string fullPath = string.Empty;
            if (Session["Usuario"] != null)
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                fullPath = Constantes.DepositosURL + @"\" + direccion + @"\" + nombreArchivo;
                respuesta = EliminarDeposito(fullPath,año ,mes, ruc);
            }
            else
            {
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        private bool EliminarDeposito(string path,string Año, string Mes, string Ruc)
        {
            // string path = Constantes.DepositosURL + @"\" + direccion.Trim() + @"\" + nombreArchivo;
            if (!System.IO.File.Exists(path)) return false;

            try //Maybe error could happen like Access denied or Presses Already User used
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.IO.File.Delete(path);

                //graba en la tabla el numero de registros
                string direccionDirectory = Año + @"\" + Mes + @"\" + Ruc;
                
                string urlDocumentos = Constantes.DepositosURL + @"\" + direccionDirectory;


                    string[] sArchivos; //array con los nombres de archivos y carpetas
                sArchivos = Directory.GetFiles(urlDocumentos);

                //número de archivos en el directorio
                int registros = sArchivos.Length;

                 CD_Depositos.Instancia.ActualizaRegistros(Año, Ruc,Mes, registros);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }
    }
}