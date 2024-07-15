
using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class CalculoFr3Controller : Controller
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

        public ActionResult DatosFr3() {
            return View();
        }
       

        [HttpGet]
        public JsonResult CargaDetalleFr3(string Ato, string Matricula, string Origen, string Destino, string Destino1, string Destino2, DateTime FechaI, DateTime FechaF, string HoraI, string HoraF)
        {
            Ato = Ato.ToUpper();
            Matricula = Matricula.ToUpper();
            Origen = Origen.ToUpper();
            Destino = Destino.ToUpper();
            Destino1 = Destino1.ToUpper();
            Destino2 = Destino2.ToUpper();
            if (Origen.Length >4)
            {
                Origen = Origen.Substring(0, 4);
            }
            if (Destino.Length > 4)
            {
                Destino = Destino.Substring(0, 3);
            }
            if (Destino1.Length > 4)
            {
                Destino1 = Destino1.Substring(0, 3);
            }
            if (Destino2.Length > 4)
            {
                Destino2 = Destino2.Substring(0, 3);
            }

            string Fechai = FechaI.ToString("yyyyMMdd");
            string Fechaf = FechaF.ToString("yyyyMMdd");
            tbFr3 DetalleFr3 = new tbFr3();

            if (Session["Usuario"] == null)
                return Json(DetalleFr3, JsonRequestBehavior.AllowGet);

            try
            {
                if (Ato !="")
                {
                    DetalleFr3 = CD_Fr3.Instancia.DetalleFr3(Ato, Matricula, Origen, Destino, Destino1, Destino2, Fechai, Fechaf, HoraI, HoraF);

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

        //[HttpPost]
        //public ActionResult ListadoMatriculaP5(string NombreCompania)
        //{

        //    if (Session["Usuario"] == null)
        //        return RedirectToAction("login", "Login");

        //    List<tbMatriculas> listado = new List<tbMatriculas>();
        //    //Compania.ToUpper();
        //     listado = CD_Matriculas.Instancia.DetalleMatriculasP5Compania(NombreCompania.ToUpper());
        //    if (listado.Count==0)
        //    {
        //        listado = CD_Matriculas.Instancia.DetallePorMatriculasP5(NombreCompania.ToUpper());
        //    }
        //    return View(listado);
        //}

        //[HttpGet]
        //public JsonResult CargaDetalleMatriculaP5(Int32 OidMatricula)
        //{
        //    tbMatriculas DetalleMatriculaP5 = new tbMatriculas();

        //    if (Session["Usuario"] == null)
        //        return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);

        //    try
        //    {
        //        if (OidMatricula > 0)
        //        {
        //            DetalleMatriculaP5 = CD_Matriculas.Instancia.DetalleMatriculaP5(OidMatricula);

        //            return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(DetalleMatriculaP5, JsonRequestBehavior.AllowGet);
        //        throw ex;
        //    }

        //}


    }
}