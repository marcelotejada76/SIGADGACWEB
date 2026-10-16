using CapaDatos;
using CapaModelo;
using DepositosCompanias.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASPSnippets.Captcha;
using DepositosCompanias.Utilitario;
using Newtonsoft.Json;

namespace DepositosCompanias.Controllers
{
    public class LoginController : Controller
    {

        public Captcha Captcha
        {
            get
            {
                return (Captcha)TempData["Captcha"];
            }
            set
            {
                TempData["Captcha"] = value;
            }
        }

        // GET: Login
        public ActionResult login()
        {
            ViewBag.TurnstileSiteKey = ConfigurationManager.AppSettings["CloudflareTurnstile:SiteKey"];
            LoginModel login = new LoginModel();
            return View(login);
        }

        [HttpPost]
        public ActionResult login(LoginModel oLogin)
        {
            ViewBag.TurnstileSiteKey = ConfigurationManager.AppSettings["CloudflareTurnstile:SiteKey"];
            tbUsuario oUsuario = new tbUsuario();
            try
            {
                string turnstileToken = Request.Form["cf-turnstile-response"];
                string turnstileError;
                if (!ValidateTurnstile(turnstileToken, out turnstileError))
                {
                    ViewBag.Error = turnstileError;
                    return View(oLogin);
                }

                oUsuario = CD_Usuario.Instancia.ObtenerUsuario(oLogin.Username, SeguridadEncriptar.GetSHA256(oLogin.Contrasena));

                if (oUsuario == null)
                {
                    ViewBag.Error = "Usuario y/o Password Incorrectos..!!";
                    return View(oLogin);
                }

                Session["Usuario"] = oUsuario;
                if ((oUsuario.NombreUsuario != null) && (oUsuario.ApellidoUsuario != null))
                    Session["name"] = oUsuario.NombreUsuario.Trim() + " " + oUsuario.ApellidoUsuario.Trim();
                else
                    Session["name"] = oUsuario.NombreUsuario.Trim();

                Session["correo"] = oUsuario.Correo.Trim();

                return RedirectToAction("CargaDepositosClientes", "Depositos");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(oLogin);
        }
        //public ActionResult login(LoginModel oLogin)
        //{

        //    try
        //    {
        //        tbUsuario oUsuario = new tbUsuario();
        //        List<tbSubirDepositos> listado = new List<tbSubirDepositos>();
        //        if (oLogin.CaptchaAnswer != null)
        //        {
        //            if (this.Captcha.IsValid(oLogin.CaptchaAnswer))
        //            {
        //                oUsuario = CD_Usuario.Instancia.ObtenerUsuario(oLogin.Username, SeguridadEncriptar.GetSHA256(oLogin.Contrasena));

        //                if (oUsuario == null)
        //                {
        //                    ViewBag.Error = "Usuario y/o Password Incorrectos..!!";
        //                    return View();
        //                }

        //                        Session["Usuario"] = oUsuario;
        //                if ((oUsuario.NombreUsuario != null) && (oUsuario.ApellidoUsuario != null))
        //                    Session["name"] = oUsuario.NombreUsuario.Trim() + " " + oUsuario.ApellidoUsuario.Trim();
        //                else
        //                    Session["name"] = oUsuario.NombreUsuario.Trim();

        //                Session["correo"] = oUsuario.Correo.Trim();

        //                return RedirectToAction("SolicitudVuelo", "VueloPrivado");

        //                //if (CD_Usuario.Instancia.GetUsuarioExistePorCodigo(oUsuario.CodigoUsuario))
        //                //{
        //                //    var ousuario = CD_Usuario.Instancia.GetUsuarioPorCodigo(oUsuario.CodigoUsuario);
        //                //    var oMenu = CD_Menu.Instancia.GetMenuPorCodigo(oUsuario.CodigoUsuario);
        //                //    Session["Usuario"] = ousuario;
        //                //    Session["MenuMaster"] = oMenu;
        //                //    Session["name"] = ousuario.NombresUsuario + " " + ousuario.ApellidosUsuario;
        //                //    Session["correo"] = ousuario.CorreoUsuario.Trim();
        //                //    //return RedirectToAction("Index", "Home");
        //                //    //return RedirectToAction("login2", "Login2");
        //                //    // return RedirectToAction("SolicitudVuelo", "Depositos");

        //                //    //string cAnio = "2026";
        //                //    //listado = CD_Depositos.Instancia.DetalleDepositos(cAnio, ousuario.NumeroRuc);
        //                //    return RedirectToAction("SolicitudVuelo", "VueloPrivado");
        //                //    //return RedirectToAction("CargaDepositosClientes", "Depositos");
        //                //    // return View(listado);
        //                //}
        //                //else
        //                //{
        //                //    oLogin. Mensaje = "No existe el usuario por favor comunicarse con el administrador del sistema.";
        //                //}

        //                //return RedirectToAction("Contact", "Home");
        //               return RedirectToAction("SolicitudVuelo", "VueloPrivado");
        //            }
        //            else
        //            {
        //                this.Captcha = new Captcha(125, 40, 20f, "#FFFFFF", "#36759C", Mode.AlphaNumeric);
        //                ViewBag.Error = "Captcha invalid";
        //            }



        //            if ((oLogin.Username == null) && (oLogin.Contrasena == null))
        //            {
        //                return View(oLogin);
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Captcha invalid";
        //        }


        //    }
        //    catch (Exception ex)
        //    {                
        //        ViewBag.Error = ex.Message;
        //    }

        //    oLogin.ImageData = this.Captcha.ImageData;
        //    return View(oLogin);
        //}

        [HttpGet]
        // El Json recibido será serializado automáticamente al objeto nuevo cocche teniendo en cuenta que las propiedades han de tener el mismo nombre
        public JsonResult ValidaExisteEmailCodigoVerificador(string correo)
        {
            bool respuesta = false;
            string numeroAleatorio = "";
            try
            {
                respuesta = CD_Usuario.Instancia.ValidaExiteCorreo(correo.Trim());

                if (respuesta)
                {
                    numeroAleatorio = CD_Usuario.Instancia.ObtenerCodigoVerifivacion(correo.Trim());
                    return Json(new { success = true, responseText = numeroAleatorio }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = false, responseText = "La dirección del correo electrónico ingresado no existe!" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { success = false, responseText = "La dirección del correo electrónico ingresado no existe!" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult ValidaExisteEmail(string correo)
        {
            bool respuesta = false;
            try
            {
                respuesta = CD_Usuario.Instancia.ValidaExiteCorreo(correo.Trim());
            }
            catch
            {
                respuesta = false;
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        // El Json recibido será serializado automáticamente al objeto nuevo cocche teniendo en cuenta que las propiedades han de tener el mismo nombre
        public JsonResult EnviaCorreoNumeroCodigoVerificador(string correo)
        {
            bool respuesta = false;
            string numAleatorio = "";
            string numeroAleatorio = "";
            string _cuerpoCorreo = string.Empty;
            EnviarCorreo ocorreo = new EnviarCorreo();
            StringBuilder stbCuerpoMensaje = new StringBuilder();
            Aleatorios a = new Aleatorios();
            try
            {
                //numAleatorio = Validador.Instancia.GenerarAleatorio();
                int[] numeros = a.generarNumerosAleatoriosNoRepetidos(5, 5, 9);
                for (int i = 0; i < numeros.Length; i++)
                {
                    numAleatorio = numAleatorio + numeros[i].ToString();
                }


                respuesta = CD_Usuario.Instancia.CrearoNumeroCodigoVerificador(correo.Trim(), int.Parse(numAleatorio));
                tbUsuario ousuario = new tbUsuario();

                if (respuesta)
                {
                    ousuario = CD_Usuario.Instancia.UsuarioPorCorreo(correo.Trim());
                    //Envía el correo el mensaje                    
                    stbCuerpoMensaje.Append("Reinicio de Contraseña");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append("Estimado(a):");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append(ousuario.NombreUsuario.Trim() + " " + ousuario.ApellidoUsuario.Trim());
                    stbCuerpoMensaje.Append("<br/>");
                    stbCuerpoMensaje.Append("<br/>");
                    stbCuerpoMensaje.Append("<h4>Contraseña Temporal: " + numAleatorio + " </h4>");
                    stbCuerpoMensaje.Append("<br/>");
                    stbCuerpoMensaje.Append("Se ha reiniciado la contraseña por parte del sistema automático.");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append("<b>Atentamente,</b>");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append("<b>Dirección General de Aviación Civil</b>");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append("<br />");
                    stbCuerpoMensaje.Append("<h5>Por favor no responder a este correo.</h5>");
                    _cuerpoCorreo = stbCuerpoMensaje.ToString();
                    respuesta = ocorreo.enviaMensajeCorreo(correo, "Clave temporal", _cuerpoCorreo);

                    //Fin correo
                    numeroAleatorio = CD_Usuario.Instancia.ObtenerCodigoVerifivacion(correo.Trim());
                    return Json(new { success = true, responseText = numeroAleatorio }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = false, responseText = "La dirección del correo electrónico ingresado no existe!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult NuevoUsuario(tbUsuario model)
        {
            bool respuesta = false;
            try
            {
                if (model.CodigoUsuario == null)
                {
                    if (CD_Usuario.Instancia.ValidaExiteCorreo(model.Correo.Trim()))
                    {
                        return Json(new { success = false, responseText = "La dirección del correo electrónico o email ya existe" }, JsonRequestBehavior.AllowGet);
                    }

                    model.Clave = Utilitario.SeguridadEncriptar.GetSHA256(model.Clave);
                    model.TipoIdentificacion = "OT";
                    model.EstadoActividad = "AC";
                    model.TipoAplicacion = "WEB";

                    respuesta = CD_Usuario.Instancia.RegistrarUsuario(model);
                    if (respuesta)
                    {
                        return Json(new { success = true, responseText = "El Usuario fue creado correctamente." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = respuesta, responseText = "No se puedo crear el usuario." }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Codigo Usuario ya existe" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CambiaContrasenaUsuario(string ocorreo, string oclave)
        {

            bool respuesta = false;
            try
            {
                respuesta = CD_Usuario.Instancia.CambiaContrasenaUsuario(ocorreo.Trim(), SeguridadEncriptar.GetSHA256(oclave));
            }
            catch
            {
                respuesta = false;
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFile()
        {
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Content/Documentos/"));
            string fileName = "";

            //Copy File names to Model collection.
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                fileName = Path.GetFileName(filePath);
                files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }

            //Build the File Path.
            string path = Server.MapPath("~/Content/Documentos/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        private bool ValidateTurnstile(string token, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(token))
            {
                errorMessage = "Por favor, complete la verificación de seguridad (Cloudflare).";
                return false;
            }

            string secretKey = ConfigurationManager.AppSettings["CloudflareTurnstile:SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                errorMessage = "La clave secreta de Cloudflare Turnstile no está configurada en Web.config.";
                return false;
            }

            try
            {
                // Habilitar soporte TLS 1.2 requerido por Cloudflare
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                using (var client = new WebClient())
                {
                    var values = new System.Collections.Specialized.NameValueCollection();
                    values["secret"] = secretKey;
                    values["response"] = token;

                    string remoteIp = Request.UserHostAddress;
                    if (!string.IsNullOrEmpty(remoteIp))
                    {
                        values["remoteip"] = remoteIp;
                    }

                    byte[] responseBytes = client.UploadValues("https://challenges.cloudflare.com/turnstile/v0/siteverify", "POST", values);
                    string responseString = System.Text.Encoding.UTF8.GetString(responseBytes);

                    var verifyResult = JsonConvert.DeserializeObject<TurnstileVerifyResponse>(responseString);
                    if (verifyResult != null && verifyResult.Success)
                    {
                        return true;
                    }

                    errorMessage = "La verificación de seguridad de Cloudflare ha fallado. Por favor, intente nuevamente.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error de comunicación con el servicio de verificación de Cloudflare: " + ex.Message;
                return false;
            }
        }
    }

    public class TurnstileVerifyResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

        [JsonProperty("challenge_ts")]
        public string ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }
    }
}