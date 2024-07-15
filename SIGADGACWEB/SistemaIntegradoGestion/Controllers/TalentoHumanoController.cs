using CapaDatos;
using CapaModelo;
using Newtonsoft.Json;
using SistemaIntegradoGestion.Models;
using SistemaIntegradoGestion.Utilitarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SistemaIntegradoGestion.Controllers
{
    public class TalentoHumanoController : Controller
    {
        // GET: TalentoHumano
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }

        public ActionResult Detalle_Maestro_Personal()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("login", "Login");

            return View();
        }

        public JsonResult ObtenerMaestroPersonal()
        {
            List<tbMaestroPersonal> oListarMaestroPersonal = new List<tbMaestroPersonal>();
            tbMaestroPersonal oMaestroPersonal = new tbMaestroPersonal();

            if (Session["Usuario"] == null)
                return Json(new { data = oListarMaestroPersonal }, JsonRequestBehavior.AllowGet);

            var ousuario = (tbUsuario)Session["Usuario"];

            oMaestroPersonal = CD_TalentoHumano.Instancia.MaestroPersonalPorCedula(ousuario.CedulaUsuario);
            oListarMaestroPersonal.Add(oMaestroPersonal);
            if (oMaestroPersonal.PathFoto.Length > 0)
            {
                string urlDocumentos = Constantes.MaestroPersonalURL + ousuario.CedulaUsuario + @"\" + oMaestroPersonal.PathFoto;
                // Convert image to byte array
                byte[] byteData = System.IO.File.ReadAllBytes(urlDocumentos);
                //Convert byte arry to base64string
                string imreBase64Data = Convert.ToBase64String(byteData);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //Passing image data in viewbag to view
                //ViewBag.ImageData = imgDataURL;
                oMaestroPersonal.imagenFoto = imgDataURL;
            }
            else
            {
                string imgPath = Server.MapPath("~/Content/imganes/user.png");
                // Convert image to byte array
                byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
                //Convert byte arry to base64string
                string imreBase64Data = Convert.ToBase64String(byteData);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //Passing image data in viewbag to view
                oMaestroPersonal.imagenFoto = imgDataURL;
                //ViewBag.ImageData = imgDataURL;

            }

            VieqBagCombos("SE", oMaestroPersonal.CodigoProviencia, oMaestroPersonal.CodigoCanton, oMaestroPersonal.CodigoParroquia);

            return Json(new { data = oListarMaestroPersonal }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Editar_Maestro_Personal()
        {
            tbMaestroPersonal oMaestroPersonal = new tbMaestroPersonal();
            try
            {
                if (Session["Usuario"] == null)
                    return RedirectToAction("login", "Login");

                var ousuario = (tbUsuario)Session["Usuario"];
                oMaestroPersonal = CD_TalentoHumano.Instancia.MaestroPersonalPorCedula(ousuario.CedulaUsuario);
                oMaestroPersonal.CodigoPais = "SE";
                if (oMaestroPersonal.PathFoto.Length > 0)
                {
                    string urlDocumentos = Constantes.MaestroPersonalURL + ousuario.CedulaUsuario + @"\" + oMaestroPersonal.PathFoto;
                    // Convert image to byte array
                    byte[] byteData = System.IO.File.ReadAllBytes(urlDocumentos);
                    //Convert byte arry to base64string
                    string imreBase64Data = Convert.ToBase64String(byteData);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    //Passing image data in viewbag to view
                    //ViewBag.ImageData = imgDataURL;
                    oMaestroPersonal.imagenFoto = imgDataURL;
                }
                else
                {
                    string imgPath = Server.MapPath("~/Content/imganes/user.png");
                    // Convert image to byte array
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
                    //Convert byte arry to base64string
                    string imreBase64Data = Convert.ToBase64String(byteData);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    //Passing image data in viewbag to view
                    oMaestroPersonal.imagenFoto = imgDataURL;
                    //ViewBag.ImageData = imgDataURL;

                }
                oMaestroPersonal.tabNumero = 1;
                VieqBagCombos(oMaestroPersonal.CodigoPais, oMaestroPersonal.CodigoProviencia, oMaestroPersonal.CodigoCanton, oMaestroPersonal.CodigoParroquia);
            }
            catch (Exception ex)
            {

                ViewBag.MensajeError = "Error al cargar la información: " + ex.Message;
            }


            return View(oMaestroPersonal);
        }

        [HttpPost]
        public ActionResult Editar_Maestro_Personal(tbMaestroPersonal modalPersonal, HttpPostedFileBase FileFoto)
        {
            //tbMaestroPersonal oMaestroPersonal = new tbMaestroPersonal();
            try
            {
                if (Session["Usuario"] == null)
                    return RedirectToAction("login", "Login");

                var ousuario = (tbUsuario)Session["Usuario"];
                modalPersonal.FechaNacimiento = DateTime.Parse(modalPersonal.FechaNacimiento).ToString("yyyyMMdd");
                modalPersonal.UsuarioModificacion = ousuario.CodigoUsuario;
                if (modalPersonal.tabNumero == 1)
                {
                    if (FileFoto != null && FileFoto.ContentLength > 0)
                        modalPersonal.PathFoto = GuardaServidorArchivo(FileFoto, modalPersonal.DocumentoIdentificacion);

                    if (CD_TalentoHumano.Instancia.MaestroPersonalActualizar(modalPersonal))
                    {
                        modalPersonal = CD_TalentoHumano.Instancia.MaestroPersonalPorCedula(ousuario.CedulaUsuario);
                        string urlDocumentos = Constantes.MaestroPersonalURL + ousuario.CedulaUsuario + @"\" + modalPersonal.PathFoto;
                        // Convert image to byte array
                        byte[] byteData = System.IO.File.ReadAllBytes(urlDocumentos);
                        //Convert byte arry to base64string
                        string imreBase64Data = Convert.ToBase64String(byteData);
                        string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                        //Passing image data in viewbag to view
                        //ViewBag.ImageData = imgDataURL;
                        modalPersonal.imagenFoto = imgDataURL;
                        modalPersonal.tabNumero = 1;
                    }

                }
                else if (modalPersonal.tabNumero == 2) {
                    modalPersonal.UsuarioModificacion = ousuario.CodigoUsuario;
                    if (CD_TalentoHumano.Instancia.MaestroPersonalActualizarDatosAdicionales(modalPersonal))
                    {                        
                        modalPersonal.tabNumero = 2;
                    }
                }
                else if (modalPersonal.tabNumero == 3) {                    
                    modalPersonal.tabNumero = 3;
                }


                modalPersonal = CD_TalentoHumano.Instancia.MaestroPersonalPorCedula(ousuario.CedulaUsuario);

                modalPersonal.CodigoPais = "SE";
                VieqBagCombos(modalPersonal.CodigoPais, modalPersonal.CodigoProviencia, modalPersonal.CodigoCanton, modalPersonal.CodigoParroquia);
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = "Error al grabar el registro: " + ex.Message;
            }

            return View(modalPersonal);
        }

        [HttpPost]
        public JsonResult DatosEmpleadoActualizar(string Empleado, HttpPostedFileBase adjuntoFoto)
        {
            bool respuesta = false;
            string message = string.Empty;
            tbMaestroPersonal oDatosEmpleado = new tbMaestroPersonal();
            try
            {

                var ousuario = (tbUsuario)Session["Usuario"];
                oDatosEmpleado = JsonConvert.DeserializeObject<tbMaestroPersonal>(Empleado);
                oDatosEmpleado.FechaNacimiento = DateTime.Parse(oDatosEmpleado.FechaNacimiento).ToString("yyyyMMdd");
                oDatosEmpleado.UsuarioModificacion = ousuario.CodigoUsuario;

                if (adjuntoFoto != null && adjuntoFoto.ContentLength > 0)
                    oDatosEmpleado.PathFoto = GuardaServidorArchivo(adjuntoFoto, oDatosEmpleado.DocumentoIdentificacion);


                respuesta = CD_TalentoHumano.Instancia.MaestroPersonalActualizar(oDatosEmpleado);


                if (respuesta)
                {
                    message = "El registro se grabo correctamente";
                }
                else
                    message = "No se puedo guardar el registro";
            }
            catch (Exception ex)
            {
                respuesta = false;
                message = "Error. " + ex.Message;
            }

            return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public JsonResult DatosAdicionalesMaestroPersonal(tbMaestroPersonal modalPersonal)
        {
            bool respuesta = false;
            string message = string.Empty;
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                modalPersonal.UsuarioModificacion = ousuario.CodigoUsuario;
                respuesta = CD_TalentoHumano.Instancia.MaestroPersonalActualizarDatosAdicionales(modalPersonal);
                if (respuesta)
                    message = "El registro se grabo correctamente";
                else
                    message = "No se puedo guardar el registro";
            }
            catch (Exception ex)
            {
                respuesta = false;
                message = "Error. " + ex.Message;
            }

            return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);


        }


        public JsonResult ObtenerCursosPorCedula(string id)
        {
            List<tbCursoEmpleado> oListaCursos = CD_CursoEmpleado.Instancia.ListarCursosEmpleados(id);
            return Json(new { data = oListaCursos }, JsonRequestBehavior.AllowGet);
        }


        private string GuardaServidorArchivo(HttpPostedFileBase documentFile, string cedulaEmpleado)
        {

            string nombreArchivo = string.Empty;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {
                    string urlDocumentos = Constantes.MaestroPersonalURL + cedulaEmpleado;
                    //Verifica si existe la carpeta creada si no lo crear
                    if (!System.IO.Directory.Exists(urlDocumentos))
                        System.IO.Directory.CreateDirectory(urlDocumentos);

                    nombreArchivo = documentFile.FileName;
                    documentFile.SaveAs(Path.Combine(urlDocumentos, nombreArchivo));

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return nombreArchivo;
        }
        private void VieqBagCombos(string codigoPias, string codigoPrivincia, string codigoCanton, string codigoParroquia)
        {
            ViewBag.SelectComboGenero = GetSelectListValores("MAEGEN");
            ViewBag.SelectComboColorOjos = GetSelectListValores("MAECOL");
            ViewBag.SelectComboCabello = GetSelectListValores("MAECO1");
            ViewBag.SelectComboTipoSangre = GetSelectListValores("MAETI1");
            ViewBag.SelectComboEstaCivil = GetSelectListValores("MAEEST");
            ViewBag.SelectComboPais = GetSelectListPais(codigoPias);
            ViewBag.SelectComboProvincia = GetSelectListProvincia(codigoPias);
            ViewBag.SelectComboCiudades = GetSelectListCiudades(codigoPias);
            ViewBag.SelectComboCantones = GetSelectListCantones(codigoPias, codigoPrivincia);
            ViewBag.SelectComboParroquias = GetSelectListParroquias(codigoPias, codigoPrivincia, codigoCanton);

            ViewBag.SelectComboRegimenLaboral = GetSelectListValores("MAERE1");
            ViewBag.SelectComboTipoHorario = GetSelectListValores("MAETI3");
            ViewBag.SelectComboDecimoTercero = GetSelectListValores("MAEDEC");
            ViewBag.SelectComboDecimoCuarto = GetSelectListValores("MAEDE1 ");
            ViewBag.SelectComboAporteFondoReserva = GetSelectListValores("MAEAPO");
            ViewBag.SelectComboSectorDondeVive = GetSelectListValores("MAESEC");
            ViewBag.SelectComboDiscapacidad = GetSelectListValores("MAEDIS");
            ViewBag.SelectComboEnfermedaCatastrof = GetSelectListValores("MAEENF");
            ViewBag.SelectCombotitulosSelect = GetSelectListTitulosCurso();

        }

        private SelectList GetSelectListValores(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));

            foreach (var item in olistTipoValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Descripcion.Trim(),
                    Value = item.Codigo.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListValores1(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));

            foreach (var item in olistTipoValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Descripcion.Trim(),
                    Value = item.Codigo.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListPais(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistPais = CD_Pais.Instancia.ObtienePaisPorCodigo(campoNull(campo));


            if (olistPais.CodigoPais != null)
            {
                list.Add(new SelectListItem()
                {
                    Text = olistPais.DescripcionPais.Trim(),
                    Value = olistPais.CodigoPais.Trim()
                });
            }
            else
            {
                list.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "0" });
            }
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListProvincia(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var olistProvincia = CD_Pais.Instancia.ObtieneTodosProvincias(campoNull(campo));

            foreach (var item in olistProvincia)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionProvincia.Trim(),
                    Value = item.CodigoProvincia.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListCiudades(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistCiudades = CD_Pais.Instancia.ObtieneTodosCiudad(campoNull(campo));


            foreach (var item in olistCiudades)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionCiudad.Trim(),
                    Value = item.CodigoCiudad.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListCantones(string campo, string codProvincia)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var olistCantones = CD_Pais.Instancia.ObtieneTodosCantones(campo, campoNull(codProvincia));
            foreach (var item in olistCantones)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionCanton.Trim(),
                    Value = item.CodigoCanton.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListParroquias(string codPais, string codProvincia, string codigoCanton)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var olistCProvincias = CD_Pais.Instancia.ObtieneTodosParroquias(codPais, campoNull(codProvincia), campoNull(codigoCanton));

            foreach (var item in olistCProvincias)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionParroquia.Trim(),
                    Value = item.CodigoParroquia.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetSelectListCantonesList(string codPais, string codProvincia)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var olistCantones = CD_Pais.Instancia.ObtieneTodosCantones(codPais, campoNull(codProvincia));
            foreach (var item in olistCantones)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionCanton.Trim(),
                    Value = item.CodigoCanton.Trim()
                });
            }

            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectListParroquiasList(string codPais, string codProvincia, string codCanton)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var olistParroquias = CD_Pais.Instancia.ObtieneTodosParroquias(codPais, campoNull(codProvincia), campoNull(codCanton));

            foreach (var item in olistParroquias)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionParroquia.Trim(),
                    Value = item.CodigoParroquia.Trim()
                });
            }

            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectListTitulosCurso()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistTitulos = CD_TituloCurso.Instancia.ObtieneTodosTitulosCurso();


            foreach (var item in olistTitulos)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionTitulo.Trim(),
                    Value = item.CodigoTitulo.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectListEntinidadEducativa()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistEntidadEducativa = CD_EntidadEducativa.Instancia.ObtieneTodasEntidadesEducativas();


            foreach (var item in olistEntidadEducativa)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionEntidad.Trim(),
                    Value = item.CodigoEntidad.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectListCiudad()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistCiudades = CD_Pais.Instancia.ObtieneTodosCiudad("SE");

            foreach (var item in olistCiudades)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionCiudad.Trim(),
                    Value = item.CodigoCiudad.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectListaValores(string campo)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var olistTipoValores = CD_ListaValor.Instancia.ListaValores(campoNull(campo));

            foreach (var item in olistTipoValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Descripcion.Trim(),
                    Value = item.Codigo.Trim()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "-- Seleccionar --"
            };
            list.Insert(0, seleccion);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region "Curso empleado"       

        [HttpPost]
        public JsonResult CursoEmpleado(string Curso, HttpPostedFileBase adjuntoFile)
        {

            bool respuesta = false;
            string message = string.Empty;
            tbCursoEmpleado oCursoEmpleado = new tbCursoEmpleado();
            try
            {

                var ousuario = (tbUsuario)Session["Usuario"];
                oCursoEmpleado = JsonConvert.DeserializeObject<tbCursoEmpleado>(Curso);
                oCursoEmpleado.FechaCurso = DateTime.Parse(oCursoEmpleado.FechaCurso).ToString("yyyyMMdd");
                oCursoEmpleado.UsuarioCreado = ousuario.CodigoUsuario;
                oCursoEmpleado.UsuarioModificado = ousuario.CodigoUsuario;
                oCursoEmpleado.EstadoCurso = "PE";

                if (adjuntoFile != null && adjuntoFile.ContentLength > 0)
                    oCursoEmpleado.PathDocumentoCurso = GuardaServidorArchivo(adjuntoFile, oCursoEmpleado.DocumentoIdentificacion);

                if (oCursoEmpleado.CodigoCursoEmpleado.Trim().Length > 0)
                    respuesta = CD_CursoEmpleado.Instancia.CursoEmpleadoActualizar(oCursoEmpleado);
                else
                    respuesta = CD_CursoEmpleado.Instancia.CursoEmpleadoNuevo(oCursoEmpleado);
                if (respuesta)
                {
                    message = "El registro se grabo correctamente";
                }
                else
                    message = "No se puedo guardar el registro";
            }
            catch (Exception ex)
            {
                respuesta = false;
                message = "Error. " + ex.Message;
            }

            return Json(new { Success = respuesta, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentoCurso(string fileName, string id)
        {
            string path = string.Empty;
            string pathArchivo = string.Empty;
            byte[] reportData = null;
            try
            {
                if (!fileName.IsEmpty())
                {
                    path = Constantes.MaestroPersonalURL + id;
                    pathArchivo = path + @"\" + fileName;

                    reportData = System.IO.File.ReadAllBytes(pathArchivo);
                }

            }
            catch
            {
            }
            return new FileContentResult(reportData, "application/pdf");
        }


        public JsonResult CursoEmpleadoPorDocumentoCodigo(string idDoc, string codigoCurso)
        {
            tbCursoEmpleado ocursoEmpleado = new tbCursoEmpleado();
            try
            {
                var ousuario = (tbUsuario)Session["Usuario"];
                ocursoEmpleado = CD_CursoEmpleado.Instancia.CursosEmpleadoPorIdentificacionCodigo(idDoc, codigoCurso);

            }
            catch (Exception ex)
            {
                ocursoEmpleado = null;
            }

            return Json(ocursoEmpleado, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ContentResult DownloadAutorizacion(string fileName, string cedulaEmpleado)
        {
            //Set the File Folder Path. 
            string base64 = string.Empty;
            string path = Constantes.MaestroPersonalURL + cedulaEmpleado;
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

        #endregion

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}