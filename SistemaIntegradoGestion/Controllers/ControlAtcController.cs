using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class ControlAtcController : Controller
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
        public ActionResult ListadoAtc()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbAtc> listado = new List<tbAtc>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_Controlador.Instancia.ConsultaControlador();// SolicitudModificacionReprogramacionSoloPOA(cAnio, SesionUsuario.CodigoSubsistema, "MDP");
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoAtc(string Licencia)
        {
            Licencia = Licencia.ToUpper().TrimStart().TrimEnd();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbAtc> listado = new List<tbAtc>();

            //tbAtc listado = new tbAtc();
            //Compania.ToUpper();
            listado = CD_Controlador.Instancia.ControladorLicencia(Licencia);
            if (listado.Count == 0)
            {
                listado = CD_Controlador.Instancia.ControladorLicenciaApellido(Licencia);
                //if (listado.Count == 0)
                //{
                //    listado = CD_BancoRuminahui.Instancia.DetalleDepositante(Licencia);
                //}
            }
            return View(listado);
        }

        [HttpGet]
        public JsonResult CargaDetalleAtc(string Licencia)
        {
            tbAtc DetalleDepsoito = new tbAtc();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (Licencia != "")
                {
                    DetalleDepsoito = CD_Controlador.Instancia.ConsultacControladorLicencia(Licencia);



                    var imagen = CargaImagenAtc(Licencia);
                   
                    DetalleDepsoito.Url = imagen;

                  
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



        public string CargaImagenAtc(string Licencia)
        {
            Licencia = Licencia.ToUpper().TrimStart().TrimEnd();
            string url = @"\\172.20.19.55\TransitoAereo\imagenes\" + Licencia.Trim() + ".jpg";
           
            // Convert image to byte array
            byte[] byteData = System.IO.File.ReadAllBytes(url);
            //Convert byte arry to base64string
            string imreBase64Data = Convert.ToBase64String(byteData);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);

          

            return imgDataURL;

        }

       
    }
}