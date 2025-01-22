using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class DocumentosDescargaProcedimientosController : Controller
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
        public ActionResult ListadoDocumentosDescargaProcedimientos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbDocumentosDescarga> listado = new List<tbDocumentosDescarga>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_DocumentosDescarga.Instancia.ConsultaDocumentosProcedimientos();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }

        //public JsonResult DescargaFr3(Int16 NumeroFr3, string Ato, string Ano, string FechaEmision, string Ruc)
        public ActionResult DescargaDcto( string Nombre)
        {


            //string remoteUri = @"\\172.20.19.55\DocumentosDescarga\" + Nombre + ".pdf";
            string remoteUri = @"\\172.20.19.55\DocumentosDescarga\ProcedimientosTecnicos\" + Nombre+"";
            //string fileName = Nombre + ".pdf";
            string fileName = Nombre + "";

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

        [HttpPost]
        public ActionResult ListadoDocumentosDescarga(string  Nombre)

        {
           


            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbDocumentosDescarga> listado = new List<tbDocumentosDescarga>();

            //tbAtc listado = new tbAtc();
            //Compania.ToUpper();
            listado = CD_DocumentosDescarga.Instancia.DocumentosPorNombre(Nombre);
            //if (listado.Count == 0)
            //{
            //    listado = CD_Controlador.Instancia.ControladorLicenciaApellido(Licencia);
            //    //if (listado.Count == 0)
            //    //{
            //    //    listado = CD_BancoRuminahui.Instancia.DetalleDepositante(Licencia);
            //    //}
            //}
            return View(listado);
        }

    }
}