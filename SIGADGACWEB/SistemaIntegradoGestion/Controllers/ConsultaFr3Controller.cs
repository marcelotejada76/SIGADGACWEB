using CapaDatos;
using CapaModelo;
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
    public class ConsultaFr3Controller : Controller
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
        public ActionResult ListadoFr3()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbFr3> listado = new List<tbFr3>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_Fr3.Instancia.ConsultaFr3();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

     

        [HttpPost]
        public ActionResult ListadoFr3(DateTime FechaEmision)
        {
            string Fecha = FechaEmision.ToString("yyyyMMdd");
           // NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbFr3> listado = new List<tbFr3>();
            //Compania.ToUpper();
             listado = CD_Fr3.Instancia.ConsultaFr3PorFecha(Fecha);
            //if (listado.Count==0)
            //{
            //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
            //}
            return View(listado);
        }
      
        [HttpGet]
        public JsonResult CargaDetalleFr3Datos(Int16 NumeroFr3, string Ato, string Ano)
        {
            tbFr3 DetalleFr3 = new tbFr3();

            if (Session["Usuario"] == null)
                return Json(DetalleFr3, JsonRequestBehavior.AllowGet);

            try
            {
                if (NumeroFr3 > 0)
                {
                   


                    DetalleFr3 = CD_Fr3.Instancia.ConsultaFr3PorSecuencia(NumeroFr3, Ato,Ano);

                    return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }

       
        //public JsonResult DescargaFr3(Int16 NumeroFr3, string Ato, string Ano, string FechaEmision, string Ruc)
            public ActionResult DescargaFr3(Int16 NumeroFr3, string Ato,  string FechaEmision, string Ruc)
        {

            //string miDirectorio = @"c:\Fr3Pdf";
            //if (!Directory.Exists(miDirectorio))
            //    Directory.CreateDirectory(miDirectorio);


            string remoteUri = @"\\172.20.19.55\PdfFr3\fr3No" + NumeroFr3 + "fecha" + FechaEmision + "cliente" + Ruc + "Ato" + Ato + ".pdf";
            string fileName = "fr3No"+NumeroFr3+"fecha"+FechaEmision+"cliente"+Ruc+"Ato"+ Ato + ".pdf";

            byte[] fileBytes = GetFile(remoteUri);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


            //string rutadescarga = @"c:\Fr3Pdf\" + fileName;
            //// Create a new WebClient instance.
            //WebClient myWebClient = new WebClient();
            //// Concatenate the domain with the Web resource filename.
            //myStringWebResource = remoteUri + fileName;
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            //// Download the Web resource and save it into the current filesystem folder.
            //myWebClient.DownloadFile(myStringWebResource, rutadescarga);

            //return null;





            //tbFr3 DetalleFr3 = new tbFr3();

            //if (Session["Usuario"] == null)
            //    return Json(DetalleFr3, JsonRequestBehavior.AllowGet);

            //try
            //{
            //    if (NumeroFr3 > 0)
            //    {
            //        DetalleFr3 = CD_Fr3.Instancia.ConsultaFr3PorSecuencia(NumeroFr3, Ato, Ano);

            //        return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //        return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
            //    throw ex;
            //}

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