using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class EventosDocumentosCnsController : Controller
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
        public ActionResult ListadoEventosCns()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbControlOperacionalCns> listado = new List<tbControlOperacionalCns>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_ControlOperacionalCns.Instancia.DetalleDocumentosEventos();
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoEventosCns(DateTime FechaElab)
        {
            string Fecha = FechaElab.ToString("yyyyMMdd");
            // NombreCompania = NombreCompania.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            List<tbControlOperacionalCns> listado = new List<tbControlOperacionalCns>();
            if (Fecha != "")
            {



               
                //Compania.ToUpper();
                listado = CD_ControlOperacionalCns.Instancia.DetalleDocumentosFechaEventos(Fecha);
                //if (listado.Count==0)
                //{
                //    listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania);
                //}
               
            }
            return View(listado);
        }


        [HttpGet]
        public JsonResult CargaDetalleDiario(string Fechaelab, string Codigo, string Turno)
        {

            Codigo = Codigo.ToUpper().TrimStart().TrimEnd();
            Turno = Turno.ToUpper().TrimStart().TrimEnd();

            tbControlOperacionalCns DetalleDepsoito = new tbControlOperacionalCns();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (Codigo != "")
                {
                    DetalleDepsoito = CD_ControlOperacionalCns.Instancia.DetalleDocumentosClave(Fechaelab, Codigo,Turno);

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

        public ActionResult DescargaEventosBitacora(string Fechaelab, string Codigo, string Turno)
        {

       

            string remoteUri = @"\\172.20.19.55\DocumentosCns\InformeTecnico\BitacoraDel" + Fechaelab.Trim()+"Turno"+Turno.Trim()+".pdf";
            string fileName = "BitacoraDel" + Fechaelab.Trim() +"Turno"+ Turno.Trim() + ".pdf";


            byte[] fileBytes = GetFile(remoteUri);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            

        }
        public ActionResult DescargaEventos(string Fechaelab, string Codigo, string Turno)
        {



            string remoteUri = @"\\172.20.19.55\DocumentosCns\InformeTecnico\RegistroOperacionalDiario" + Fechaelab.Trim() + "Turno" + Turno.Trim() + ".pdf";
            string fileName = "RegistroOperacionalDiario" + Fechaelab.Trim() + "Turno" + Turno.Trim() + ".pdf";


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