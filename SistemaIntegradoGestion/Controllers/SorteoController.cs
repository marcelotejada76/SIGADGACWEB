using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class SorteoController : Controller
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
        //comentario
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

        public ActionResult Sorteo() {
            return View();
        }
       

        [HttpGet]
        public JsonResult Resultado(string Tipo)
        {
            //Tipo = Tipo.ToUpper();
            
            tbSorteo ConsultaSorteo = new tbSorteo();

            if (Session["Usuario"] == null)
                return Json(ConsultaSorteo, JsonRequestBehavior.AllowGet);

            try
            {
                if (Tipo !="")
                {
                    List<tbSorteo> listado = new List<tbSorteo>();
                    listado = CD_Sorteo.Instancia.ConsultaSorteo(Tipo);

                    //    ConsultaSorteo = CD_Sorteo.Instancia.SeleccionarGanador();

                    string resultado = "";
                  
                    // Crear una instancia de Random
                    Random random = new Random();

                    // Obtener un índice aleatorio
                    int indiceGanador = random.Next(listado.Count);
                    var nombre = listado[indiceGanador].CEDULARUC + " " + listado[indiceGanador].NOMBRE + " " + listado[indiceGanador].CORREO +
                        " " + listado[indiceGanador].CELULAR+" " + listado[indiceGanador].DESCRIPCION;
                    // Devolver el ganador
                    //return resultado = indiceGanador.ToString();
                    //return nombre;

                    // ConsultaSorteo = nombre;
                    Thread.Sleep(5000);

                    return Json(listado[indiceGanador], JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(ConsultaSorteo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ConsultaSorteo, JsonRequestBehavior.AllowGet);
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