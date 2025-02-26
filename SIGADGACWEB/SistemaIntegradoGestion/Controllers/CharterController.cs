using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDatos;
using CapaModelo;


namespace SistemaIntegradoGestion.Controllers
{
    public class CharterController : Controller
    {
        // GET: Charter
        public ActionResult FinancieroAprueba()
        {
            string mensajeError = null;
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbSolictudVuelo> listSolicitud = new List<tbSolictudVuelo>();
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            var ousuario = (tbUsuario)Session["Usuario"];
            if (ousuario.CodigoCiudad != "")
            {
                if (CD_HorarioAtencion.Instancia.VerificarHorarioAtencionPorUsuario(ousuario.CodigoUsuario))
                {
                    if (ousuario.CentroContable != "0")
                    {
                        int indexD = 0;
                        listSolicitud = CD_SolicitudVueloCharter.Instancia.SolicitudVueloCharterDetalle(oSistema.FechaSistema, 30);
                        foreach (var item in listSolicitud)
                        {
                            if (item.EstadoSolicitud.Equals("EN"))
                            {
                                if (HoraFinSemana(item.FechaEnvioSolicitud))
                                {
                                    listSolicitud[indexD].EstadoAutoriza = "";
                                }
                                else
                                {
                                    if (VerificaHorarioAtencion(ousuario, item.FechaEnvioSolicitud, item.HoraEnvioSolicitud))
                                        listSolicitud[indexD].EstadoAutoriza = "A";
                                    else
                                        listSolicitud[indexD].EstadoAutoriza = "";
                                }

                            }
                            else
                                listSolicitud[indexD].EstadoAutoriza = "";

                            listSolicitud[indexD].FechaEnvioSolicitud = item.FechaEnvioSolicitud + " " + item.HoraEnvioSolicitud;
                            indexD++;
                        }

                    }
                    else
                    {
                        mensajeError = "Usuario no está autorizado por que no tiene ingresado el Centro Contable, comunique con el administrador de sistema.";
                    }
                }
                else
                {
                    mensajeError = "El usuario " + ousuario.CodigoUsuario + ", No tiene asignado el horario de atención comuníquese con el Administrador del Sistema";
                }
            }
            else
            {
                mensajeError = "Usuario no está asignado a una ciudad, comunique con el administrador de sistema.";
            }

            ViewBag.mensajeError = mensajeError;
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            ViewBag.Vista = actionName;
            ViewBag.ListaBancoPago = ToSelectList("OPCBAN");

            return View(listSolicitud);
        }
        [HttpPost]
        public ActionResult FinancieroAprueba(string fechaInicio, string fechaFinal)
        {
            string mensajeError = null;

            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");


            List<tbSolictudVuelo> listSolicitud = new List<tbSolictudVuelo>();
            var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            var ousuario = (tbUsuario)Session["Usuario"];
            if (ousuario.CodigoCiudad != "")
            {
                DateTime fechaSolInicio = DateTime.Parse(fechaInicio);
                DateTime fechaSolFinal = DateTime.Parse(fechaFinal);

                if (CD_HorarioAtencion.Instancia.VerificarHorarioAtencionPorUsuario(ousuario.CodigoUsuario))
                {
                    if (ousuario.CentroContable != "0")
                    {
                        int indexD = 0;
                        listSolicitud = CD_SolicitudVueloCharter.Instancia.SolicitudVueloCharterDetallePorFechas(fechaSolInicio.ToString("yyyyMMdd"), fechaSolFinal.ToString("yyyyMMdd"));
                        //CD_SolicitudVueloCharter.Instancia.SolicitudVueloCharterDetalle(oSistema.FechaSistema, 30);
                        foreach (var item in listSolicitud)
                        {
                            if (item.EstadoSolicitud.Equals("EN"))
                            {
                                if (HoraFinSemana(item.FechaEnvioSolicitud))
                                {
                                    listSolicitud[indexD].EstadoAutoriza = "";
                                }
                                else
                                {
                                    if (VerificaHorarioAtencion(ousuario, item.FechaEnvioSolicitud, item.HoraEnvioSolicitud))
                                        listSolicitud[indexD].EstadoAutoriza = "A";
                                    else
                                        listSolicitud[indexD].EstadoAutoriza = "";
                                }

                            }
                            else
                                listSolicitud[indexD].EstadoAutoriza = "";

                            listSolicitud[indexD].FechaEnvioSolicitud = item.FechaEnvioSolicitud + " " + item.HoraEnvioSolicitud;
                            indexD++;
                        }

                    }
                    else
                    {
                        mensajeError = "Usuario no está autorizado por que no tiene ingresado el Centro Contable, comunique con el administrador de sistema.";
                    }
                }
                else
                {
                    mensajeError = "El usuario " + ousuario.CodigoUsuario + ", No tiene asignado el horario de atención comuníquese con el Administrador del Sistema";
                }
            }
            else
            {
                mensajeError = "Usuario no está asignado a una ciudad, comunique con el administrador de sistema.";
            }

            ViewBag.mensajeError = mensajeError;
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            ViewBag.Vista = actionName;
            ViewBag.ListaBancoPago = ToSelectList("OPCBAN");

            return View(listSolicitud);
        }

        [HttpGet]
        public JsonResult CargaSolicitudVuelo(int id)
        {
            tbSolictudVuelo osilicitud = new tbSolictudVuelo();
            DateTime dateFechaActual;
            DateTime dateFechaVencimiento;
            tbUsuario ousuario = new tbUsuario();

            if (Session["Usuario"] == null)
                return Json(osilicitud, JsonRequestBehavior.AllowGet);

            try
            {
                ousuario = (tbUsuario)Session["Usuario"];

                tbCertificadoDigital ocertificado = new tbCertificadoDigital();
                tbCertificadoDigital ocertificadoAutorizado = new tbCertificadoDigital();
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                if (id > 0)
                {
                    osilicitud = CD_SolicitudVueloCharter.Instancia.ObtieneSolicitudVueloPorId(id);
                    ocertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                    ocertificadoAutorizado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorId(ocertificado.OidCertificadoPadre);
                    if (ocertificado != null)
                    {
                        if (ocertificado.CodigoUsuario != null)
                        {
                            osilicitud.EstadoCertificado = ocertificado.Asignado;
                            if (ocertificado.Asignado.Equals("AC"))
                            {
                                //Prueba
                                // CreaAutorizacion(osilicitud, "", oSistema, ousuario, ocertificado, ocertificadoAutorizado);

                                //Fin Prueba

                                dateFechaActual = DateTime.Parse(fechaDateAs400(oSistema.FechaSistema));
                                dateFechaVencimiento = DateTime.Parse(fechaDateAs400(ocertificado.FechaVencimiento));
                                if (dateFechaVencimiento < dateFechaActual)
                                {
                                    CD_CertificadoDigital.Instancia.CertificadoDigitalCambiaEstado("IA", ousuario.CodigoUsuario, ocertificado.Secuencial);
                                    osilicitud.EstadoCertificado = "";
                                    osilicitud.MensajeCertificado = "El Certificado de la Firma Electrónica está caducado, carge una nueva ";
                                }
                            }
                            else if (ocertificado.Asignado.Equals("IA"))
                            {
                                osilicitud.MensajeCertificado = "El Certificado de la Firma Electrónica está caducado, carge una nueva ";
                            }
                        }
                        else
                        {
                            osilicitud.EstadoCertificado = "";
                            osilicitud.MensajeCertificado = "No tiene cargado el Certificado de la Firma Electrónica";

                        }
                    }


                    return Json(osilicitud, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(osilicitud, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(osilicitud, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DownloadFile(string nombreArchivo)
        {
            string base64 = string.Empty;
            string fullName = string.Empty;
            byte[] bytes = null;
            try
            {
                
                fullName =  SistemaIntegradoGestion.Utilitarios.Utilitario.charterURL + @"\" + nombreArchivo;


                bytes = System.IO.File.ReadAllBytes(fullName);

                //Convert File to Base64 string and send to Client.
                base64 = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            catch
            {
                base64 = "";
            }

            return Content(base64);
        }

        [HttpPost]
        public ContentResult DownloadAutorizacion(string fileName)
        {
            //Set the File Folder Path. 
            string base64 = string.Empty;
            string path = SistemaIntegradoGestion.Utilitarios.Utilitario.autorizacionURL;
            string pathArchivo = path + fileName;
            try
            {
                //Read the File as Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(pathArchivo);

                //Convert File to Base64 string and send to Client.
                base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            }
            catch
            {
            }

            return Content(base64);
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

        public SelectList ToSelectList(string valueCampo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<tbListaValor> listValores = new List<tbListaValor>();
            listValores = CD_ListaValor.Instancia.ListaValores(valueCampo);
            foreach (var item in listValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Descripcion.Trim(),
                    Value = item.Codigo.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = null,
                Text = "---SELECCIONE  ..."
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private bool VerificaHorarioAtencion(tbUsuario ousuario, string fechaSolictud, string horaSolicitud)
        {
            bool estado = false;
            if (ousuario.oHorarioAtencion == null)
                return estado = false;

            TimeSpan _tsHoraAtInicio = TimeSpan.Parse(ousuario.oHorarioAtencion.HorarioInicio);
            TimeSpan _tsHoraAtFin = TimeSpan.Parse(ousuario.oHorarioAtencion.HorarioFin);
            TimeSpan _tsHoraSol = TimeSpan.Parse(horaSolicitud);

            if (_tsHoraAtInicio < _tsHoraSol && _tsHoraAtFin > _tsHoraSol && ousuario.CodigoCiudad == "SEQU")
            {
                estado = true;
            }
            else if (_tsHoraSol > _tsHoraAtInicio && _tsHoraAtFin < _tsHoraSol && ousuario.CodigoCiudad != "SEQU")
            {

                estado = true;
            }
            else if (_tsHoraAtInicio > _tsHoraSol && _tsHoraSol < _tsHoraAtFin && ousuario.CodigoCiudad != "SEQU")
            {

                estado = true;
            }

            return estado;
        }

        private bool HoraFinSemana(string fecha)
        {
            bool estado = false;

            DateTime fechaSistema = DateTime.Parse(fechaDateAs400(fecha));

            byte dia = (byte)fechaSistema.DayOfWeek;
            if (dia == 6 || dia == 0)
            {
                estado = true;
            }

            return estado;
        }

        private String fechaDateAs400(string ofecha)
        {
            string odate = string.Empty;

            if (ofecha.Trim().Length > 0)
            {
                odate = ofecha.Substring(6, 2) + "-" + ofecha.Substring(4, 2) + "-" + ofecha.Substring(0, 4);
            }
            return odate;
        }

        [HttpPost]
        public JsonResult ComprobarSession()
        {
            bool respuesta = true;
            if (Session["Usuario"] == null)
                respuesta = false;

            return Json(respuesta, JsonRequestBehavior.AllowGet);


        }


    }
}