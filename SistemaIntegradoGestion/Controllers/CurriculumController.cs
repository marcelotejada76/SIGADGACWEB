using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static CapaDatos.CD_Curriculum;

namespace SistemaIntegradoGestion.Controllers
{
    public class CurriculumController : Controller
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

        //private readonly Db2Context _context;

        //public CurriculumController(CD_Curriculum.Db2Context context)
        //{
        //    _context = context;
        //}


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
        public ActionResult ListadoCurriculum()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbCabeceraCurriculum> listado = new List<tbCabeceraCurriculum>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_Curriculum.Instancia.ConsultarCurriculum();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }



        public   ActionResult Crear()
        {
            List<tbCabeceraCurriculum> listado = new List<tbCabeceraCurriculum>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();

            return View();
        }

        // Guardar un currículum en la base de datos
        [HttpPost]
        public JsonResult Crear (tbCabeceraCurriculum curriculum)
        {
            bool respuesta = false;
            string message = string.Empty;
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                
                respuesta = CD_Curriculum.Instancia.CurriculumInsertar(curriculum);
                if (respuesta)
                    message = "Registro Insertado correctamente";
                else
                    message = "Error al Registrar";
            }
            catch (Exception ex)
            {
                respuesta = false;
                message = "Error. " + ex.Message;
            }

            return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public JsonResult CargaDetalleCedula(string Cedula)
        {
            tbCabeceraCurriculum DetalleCedula = new tbCabeceraCurriculum();

            if (Session["Usuario"] == null)
                return Json(DetalleCedula, JsonRequestBehavior.AllowGet);

            try
            {
                if (Cedula !="")
                {
                    DetalleCedula = CD_Curriculum.Instancia.ConsultarCurriculumCedula(Cedula);

                    return Json(DetalleCedula, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(DetalleCedula, JsonRequestBehavior.AllowGet);
                throw ex;
            }
            return Json(DetalleCedula, JsonRequestBehavior.AllowGet);
        }



        //[HttpPost]
        //public ActionResult Crear(tbCabeceraCurriculum curriculum)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Agregar el currículum al contexto
        //        _context.Curriculums.Add(curriculum);

        //        // Guardar los cambios en la base de datos
        //        _context.SaveChanges();

        //        // Redirigir a la lista de currículums
        //        return RedirectToAction("Index");
        //    }

        //    // Si hay errores de validación, volver a mostrar el formulario
        //    return View(curriculum);
        //}


        [HttpPost]
        public ActionResult ListadoTut(DateTime FechaEmision)
        {
            string Fecha = FechaEmision.ToString("yyyyMMdd");
            // NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbTut> listado = new List<tbTut>();
            //Compania.ToUpper();
            listado = CD_Tut.Instancia.ConsultaTutPorFecha(Fecha);
            //if (listado.Count==0)
            //{
            //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
            //}
            return View(listado);
        }

        [HttpGet]
        public JsonResult CargaDetalleTutDatos(Int16 NumeroTut, string Ato, string Ano)
        {
            tbTut DetalleTut = new tbTut();

            if (Session["Usuario"] == null)
                return Json(DetalleTut, JsonRequestBehavior.AllowGet);

            try
            {
                if (NumeroTut > 0)
                {
                    DetalleTut = CD_Tut.Instancia.ConsultaTutPorSecuencia(NumeroTut, Ato, Ano);

                    return Json(DetalleTut, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleTut, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleTut, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }

        //[HttpGet]
//        public JsonResult DescargaTasa(Int16 NumeroFr3, string Ato, string Ano, string FechaEmision, string Ruc)
//        {
//            try
//            {
//                string miDirectorio = @"c:\TUTPdf";
//                if (!Directory.Exists(miDirectorio))
//                    Directory.CreateDirectory(miDirectorio);

//                string remoteUri = @"\\172.20.19.55\Tasas\";
//                string fileName = "TasaNo" + NumeroFr3 + "fecha" + FechaEmision + "cliente" + Ruc + "Ato" + Ato + ".pdf", myStringWebResource = null;
//                string rutadescarga = @"c:\TUTPdf\" + fileName;
//                // Create a new WebClient instance.
//                WebClient myWebClient = new WebClient();
//                // Concatenate the domain with the Web resource filename.
//                myStringWebResource = remoteUri + fileName;
//                Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
//                // Download the Web resource and save it into the current filesystem folder.
//                myWebClient.DownloadFile(myStringWebResource, rutadescarga);
//            }
//            catch (Exception ex)
//            {

////                throw;
//            }
//            return null;

//        }

        public ActionResult DescargaTasa(Int16 NumeroTAsa, string Ato, string FechaEmision, string Ruc)
        {

            string remoteUri = @"\\172.20.19.55\Tasas\TasaNo" + NumeroTAsa + "fecha" + FechaEmision + "cliente" + Ruc + "Ato" + Ato + ".pdf";
            string fileName = "TasaNo" + NumeroTAsa + "fecha" + FechaEmision + "cliente" + Ruc + "Ato" + Ato + ".pdf";

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
    }
}