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
    public class LiquidacionCoactivasController : Controller
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
        public ActionResult LiquidacionCoactivas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbLiquidacionCoactiva> listado = new List<tbLiquidacionCoactiva>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_LiquidacionCoactivas.Instancia.ListadoLiquidaciones();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }



        //[HttpPost]
        //public ActionResult ListadoTut(DateTime FechaEmision)
        //{
        //    string Fecha = FechaEmision.ToString("yyyyMMdd");
        //    // NombreCompania = NombreCompania.ToUpper();

        //    if (Session["Usuario"] == null)
        //        return RedirectToAction("login", "Login");

        //    List<tbTut> listado = new List<tbTut>();
        //    //Compania.ToUpper();
        //    listado = CD_Tut.Instancia.ConsultaTutPorFecha(Fecha);
        //    //if (listado.Count==0)
        //    //{
        //    //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
        //    //}
        //    return View(listado);
        //}

        //[HttpGet]
        //public JsonResult DescargaLiquidacion(string Ruc, string Procedimiento)
        //{
        //    tbLiquidacionCoactiva Detalle = new tbLiquidacionCoactiva();
        //    string Msg = "";
        //    if (Session["Usuario"] == null)
        //        return Json(Detalle, JsonRequestBehavior.AllowGet);

        //    try
        //    {
        //        if (Ruc != "")
        //        {
        //            string Mensaje = CD_LiquidacionCoactivas.Instancia.ImprimeLiquidacion(Ruc, Procedimiento, Msg);
        //            //Detalle = CD_LiquidacionCoactivas.Instancia.ImprimeLiquidacion(Ruc, Procedimiento, Mensaje);

        //            return Json(new { ok = true, Mensaje });
        //            //return Json(Detalle, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //             Msg = "No Existe Ruc";
        //            return Json(new { ok = true, Msg });
        //        //  return Json(Detalle, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { ok = false, mensaje = "Error: " + ex.Message });
        //        throw ex;
        //    }

        //}

        [HttpPost]
        public JsonResult DescargaLiquidacion(string Ruc, string Procedimiento)
        {   
            string Msg = "";
            try
            {
                
                string Mensaje = CD_LiquidacionCoactivas.Instancia.ImprimeLiquidacion(Ruc, Procedimiento, Msg);


                return Json(new { ok = true, Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mensaje = "Error: " + ex.Message });
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