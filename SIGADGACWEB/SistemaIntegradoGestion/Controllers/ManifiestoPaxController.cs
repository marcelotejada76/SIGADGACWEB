using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class ManifiestoPaxController : Controller
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
        public ActionResult ListadoManifiestoPax()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbManifiestoPax> listado = new List<tbManifiestoPax>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_ManifiestoPax.Instancia.ConsultaManifiesto();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoManifiestoPax(DateTime FechaEmision)
        {
            string Fecha = FechaEmision.ToString("yyyyMMdd");
            // NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbManifiestoPax> listado = new List<tbManifiestoPax>();
            //Compania.ToUpper();
            listado = CD_ManifiestoPax.Instancia.ConsultaManifiestoPorFecha(Fecha);
            //if (listado.Count==0)
            //{
            //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
            //}
            return View(listado);
        }

        [HttpGet]
        public JsonResult CargaDetalleManifiesto(string NumeroVlo, string FechaVlo)
        {
           

            tbManifiestoPax DetalleDepsoito = new tbManifiestoPax();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (NumeroVlo != "")
                {
                    DetalleDepsoito = CD_ManifiestoPax.Instancia.DetallePax(NumeroVlo,FechaVlo);

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

     
    }
}