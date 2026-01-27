using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class BasesCoactivasController : Controller
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
        public ActionResult ListadoBasesCoactivas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbBasesCoactivas> listado = new List<tbBasesCoactivas>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_BasesCoactivas.Instancia.ConsultaBasesCoactivas();
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoBasesCoactivas(string Comprobante)
        {

            Comprobante = Comprobante.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            SesionUsuario = (tbUsuario)Session["Usuario"];
            List<tbBasesCoactivas> listado = new List<tbBasesCoactivas>();
            if (Comprobante != "")
            {

                //Compania.ToUpper();
                listado = CD_BasesCoactivas.Instancia.DetalleDocumentosporProceso(Comprobante);
                if (listado.Count == 0)
                {
                    listado = CD_BasesCoactivas.Instancia.DetalleDocumentosporPersonaJuridica(Comprobante);
                }

            }
            return View(listado);
        }


        [HttpGet]
        public JsonResult CargaBasesCoactivas(string NroProceso, string Persona)
        {
            NroProceso = NroProceso.ToUpper().TrimStart().TrimEnd();
            Persona = Persona.ToUpper().TrimStart().TrimEnd();


            tbBasesCoactivas DetalleDepsoito = new tbBasesCoactivas();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (NroProceso != "")
                {
                    DetalleDepsoito = CD_BasesCoactivas.Instancia.DetalleDocumentosBaseCoactiva(NroProceso,Persona);

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

        public ActionResult DescargaIts(string Lugar, string Dependencia, string Fechaelab, string Turno)
        {

        //string miDirectorio = @"c:\Fr3Pdf";
        //if (!Directory.Exists(miDirectorio))
        //    Directory.CreateDirectory(miDirectorio);
        

            string remoteUri = @"\\172.20.19.55\TransitoAereo\ITS_" + Lugar.Trim() + "_" + Dependencia.Trim() + "_" + Turno.Trim() + "_" + Fechaelab + ".pdf";
            string fileName = "ITS_" + Lugar.Trim() + "_" + Dependencia.Trim() + "_" + Turno.Trim() + "_" + Fechaelab + ".pdf";

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