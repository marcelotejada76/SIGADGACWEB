using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class MigracionSobrevuelosController : Controller
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

        public ActionResult DatosSobrevuelos() {
            return View();
        }


        [HttpPost]
        public JsonResult ProcesarFacturacion(DateTime FechaInicial, DateTime FechaFinal)
        {
            string Fechai = FechaInicial.ToString("yyyyMMdd");
            string Fechaf = FechaFinal.ToString("yyyyMMdd");
            string Msg = "";
            try
            {
                // Aquí puedes llamar a tu lógica, SP, etc.
                // Ejemplo:
                // FacturacionService.Procesar(FechaInicial, FechaFinal);
               string Mensaje = CD_MigraSobrevuelos.Instancia.ActualizaSobrevuelosP550(Fechai, Fechaf, Msg);

                

                return Json(new { ok = true, Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mensaje = "Error: " + ex.Message });
            }
        }


      

    }
}