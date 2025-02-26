using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class ActualizacionContraseñaController : Controller
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

        public ActionResult CambioContraseña()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CambioContraseña(string Usuario, string ContraseñaActual, string NuevaContraseña)
        {

            string Mensaje = "";


            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");
           

            try
            {

                Mensaje = CD_ActualizaContraseña.Instancia.CambiaClave(Usuario, ContraseñaActual, NuevaContraseña);

                string[] charsToRemove = new string[] { "CWBSY", "1", "2", "3", "4", "5", "6", "7","8","9","0","." };
                foreach (var c in charsToRemove)
                {
                    Mensaje = Mensaje.Replace(c, string.Empty);
                }
                ViewBag.estado = false;
                if (Mensaje=="")
                {
                    

                  Salir();
                    ViewBag.Error = "CONTRASEÑA CAMBIADA EXITOSAMENTE...";
                    ViewBag.estado = true;
                   // return RedirectToAction("login", "Login");


                }
                else
                {
                    ViewBag.Error =  Mensaje;
                }
                

                // return Json(DetalleFr3, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //  return Json(DetalleFr3, JsonRequestBehavior.AllowGet);
                throw ex;
            }
            return View();

        }

        private void Salir()
        {
            Session["Usuario"] = null;
            Session.Remove("Usuario");
            Session["name"] = null;
            Session.Remove("name");
            Session["correo"] = null;
            Session.Remove("correo");
            //return RedirectToAction("login", "Login");
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