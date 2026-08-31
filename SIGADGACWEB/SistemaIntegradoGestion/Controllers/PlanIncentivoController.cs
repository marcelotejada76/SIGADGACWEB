using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class PlanIncentivoController : Controller
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
        public ActionResult ListadoPlanIncentivo()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            List<tbPlanIncentivo> listado = new List<tbPlanIncentivo>();
            SesionUsuario = (tbUsuario)Session["Usuario"];
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            // string cAnio = oSistema.FechaSistema.Substring(0, 4);
            listado = CD_PlanIncentivo.Instancia.ConsultaPlanIncentivo();
            return View(listado);
        }



        [HttpPost]
        public ActionResult ListadoPlanIncentivo(string Vuelo)
        {

            Vuelo = Vuelo.ToUpper();

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
            SesionUsuario = (tbUsuario)Session["Usuario"];
            List<tbPlanIncentivo> listado = new List<tbPlanIncentivo>();
            if (Vuelo !="")
            {   
                //listado = CD_PlanIncentivo.Instancia.DetallePlanIncentivo(Vuelo);
                //if (listado.Count == 0)
                //{
                    listado = CD_PlanIncentivo.Instancia.DetalleDocumentosporVuelo(Vuelo);
                //}

            }
            return View(listado);
        }


        [HttpGet]
        public JsonResult CargaPlanIncentivo(Int32 Oid, string NumeroVuelo)
        {
            //NroProceso = NroProceso.ToUpper().TrimStart().TrimEnd();
            //Persona = Persona.ToUpper().TrimStart().TrimEnd();


            tbPlanIncentivo DetalleDepsoito = new tbPlanIncentivo();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (Oid >0)
                {
                    DetalleDepsoito = CD_PlanIncentivo.Instancia.DetallePlanIncentivo(Oid,NumeroVuelo);

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

     //NUEVO PLAN

        [HttpPost]
        public JsonResult GuardarPlanIncentivo(tbPlanIncentivo plan)
        {
            bool resultado = false;
            string mensaje = "";

            if (Session["Usuario"] == null)
            {
                return Json(new { success = false, message = "Sesión expirada" });
            }

            try
            {

                tbUsuario usuario = (tbUsuario)Session["Usuario"];


                string currentUser = usuario?.CodigoUsuario ?? "ADMIN";

                if (plan.OID == 0)
                {
                    plan.USERCR = currentUser;
                    plan.DATECR = DateTime.Now.ToString("yyyyMMdd");
                    plan.HORACR = DateTime.Now.ToString("HH:mm:ss");

                    resultado = CD_PlanIncentivo.Instancia.PlanIncentivoNuevo(plan);
                    mensaje = resultado ? "Plan de incentivo registrado correctamente." : "Error al registrar el plan.";
                }
                else
                {
                    plan.USERMD = currentUser;
                    plan.DATEMD = DateTime.Now.ToString("yyyyMMdd");
                    plan.HORAMD = DateTime.Now.ToString("HH:mm:ss");

                    resultado = CD_PlanIncentivo.Instancia.PlanIncentivoActualizar(plan);
                    mensaje = resultado ? "Plan de incentivo actualizado correctamente." : "Error al actualizar el plan.";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Ocurrió un error: " + ex.Message;
            }

            return Json(new { success = resultado, message = mensaje });
        }


        [HttpGet]
        public JsonResult ProcesarPlanIncentivo(Int32 Oid, string NumeroVuelo)
        {
            //NroProceso = NroProceso.ToUpper().TrimStart().TrimEnd();
            //Persona = Persona.ToUpper().TrimStart().TrimEnd();


            tbPlanIncentivo DetalleDepsoito = new tbPlanIncentivo();

            if (Session["Usuario"] == null)
                return Json(DetalleDepsoito, JsonRequestBehavior.AllowGet);

            try
            {
                if (Oid > 0)
                {
                    DetalleDepsoito = CD_PlanIncentivo.Instancia.DetallePlanIncentivo(Oid, NumeroVuelo);

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

        //
        [HttpPost]
        public JsonResult EjecutaPlan(tbPlanIncentivo plan)
        {
            bool resultado = false;
            string mensaje = "";

            if (Session["Usuario"] == null)
            {
                return Json(new { success = false, message = "Sesión expirada" });
            }

            try
            {

                tbUsuario usuario = (tbUsuario)Session["Usuario"];


                //string currentUser = usuario?.CodigoUsuario ?? "ADMIN";

                //if (plan.OID == 0)
                //{
                //    plan.USERCR = currentUser;
                //    plan.DATECR = DateTime.Now.ToString("yyyyMMdd");
                //    plan.HORACR = DateTime.Now.ToString("HH:mm:ss");

                //    resultado = CD_PlanIncentivo.Instancia.PlanIncentivoNuevo(plan);
                //    mensaje = resultado ? "Plan de incentivo registrado correctamente." : "Error al registrar el plan.";
                //}
                //else
                //{
                //    plan.USERMD = currentUser;
                //    plan.DATEMD = DateTime.Now.ToString("yyyyMMdd");
                //    plan.HORAMD = DateTime.Now.ToString("HH:mm:ss");

                //    resultado = CD_PlanIncentivo.Instancia.PlanIncentivoActualizar(plan);
                //    mensaje = resultado ? "Plan de incentivo actualizado correctamente." : "Error al actualizar el plan.";
                //}

                    resultado = CD_PlanIncentivo.Instancia.ProcesarPlanIncentivo(plan);
                    mensaje = resultado ? "Plan de incentivo Procesado correctamente." : "Error al actualizar el plan.";
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Ocurrió un error: " + ex.Message;
            }

            return Json(new { success = resultado, message = mensaje });
        }

    }
}