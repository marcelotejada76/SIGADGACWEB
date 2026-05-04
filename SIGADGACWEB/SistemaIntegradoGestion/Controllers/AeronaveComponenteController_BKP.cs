using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.IO;
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
                    //  CD_AeronaveComponentes.Instancia.ImprimeDocumento(Aeronave);
                  
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

        public ActionResult DescargaPdf(string Aeronave)
        {
            Aeronave = Aeronave.Trim();
          
                CD_AeronaveComponentes.Instancia.ImprimeDocumento(Aeronave);


                string remoteUri = @"\\172.20.19.55\Aeronaves\aeronave_" + Aeronave.Trim() + ".pdf";
                //string remoteUri = @"\\172.20.19.55\TransitoAereo\ITS_" + Lugar.Trim() + "_" + Dependencia.Trim() + "_" + Turno.Trim() + "_" + Fechaelab + ".pdf";
                string fileName = "aeronave_" + Aeronave.Trim() + ".pdf";


            byte[] fileBytes = GetFile(remoteUri);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            
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




        public ActionResult ObtenerArchivos(string matricula)
        {
            // Combinamos la ruta base con la matrícula (ajusta según tu estructura de carpetas)
            string folderPath = Path.Combine(@"\\172.20.19.55\DocumentosAeronave\"+ matricula);

            if (Directory.Exists(folderPath))
            {
                // Obtiene las rutas completas de todos los archivos
                string[] files = Directory.GetFiles(folderPath);

                // Solo los nombres de los archivos para mostrar en la vista
                var fileNames = files.Select(f => Path.GetFileName(f)).ToList();

                return Json(fileNames, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound("El directorio no existe.");
        }


        public ActionResult DescargaDcto(string matricula, string archivo)
        {
            matricula = matricula.Trim().ToUpper();

            string basePath = @"\\172.20.19.55\DocumentosAeronave\";
            string fullPath = Path.Combine(basePath, matricula, archivo);

            if (!System.IO.File.Exists(fullPath))
                return HttpNotFound("Archivo no encontrado");

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes,
                        System.Net.Mime.MediaTypeNames.Application.Octet,
                        archivo);
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