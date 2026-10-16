using CapaDatos;
using SistemaIntegradoGestion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SistemaIntegradoGestion.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult login()
        {
            ViewBag.TurnstileSiteKey = ConfigurationManager.AppSettings["CloudflareTurnstile:SiteKey"];
            ModelLogin modelLogin = new ModelLogin();
            return View(modelLogin);
        }

        // Stub de compatibilidad para evitar 404 en navegadores con recursos cacheados
        [HttpGet]
        public ActionResult GetCaptchaImage()
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult login(ModelLogin login)
        {
            ViewBag.TurnstileSiteKey = ConfigurationManager.AppSettings["CloudflareTurnstile:SiteKey"];
            string estado = string.Empty;
            try
            {
                login.Mensaje = string.Empty;

                // 1. Validaciones básicas de campos vacíos
                if (string.IsNullOrWhiteSpace(login.Usuario) && string.IsNullOrWhiteSpace(login.Clave))
                {
                    login.Mensaje = "Usuario y Password en blanco, debe ingresar ..!!";
                    return View(login);
                }
                else if (string.IsNullOrWhiteSpace(login.Usuario))
                {
                    login.Mensaje = "El usuario en blanco, debe ingresar ..!!";
                    return View(login);
                }
                else if (string.IsNullOrWhiteSpace(login.Clave))
                {
                    login.Mensaje = "La contraseña en blanco, debe ingresar ..!!";
                    return View(login);
                }

                // 2. Validación de Cloudflare Turnstile
                string turnstileToken = Request.Form["cf-turnstile-response"];
                string turnstileError;
                if (!ValidateTurnstile(turnstileToken, out turnstileError))
                {
                    login.Mensaje = turnstileError;
                    return View(login);
                }

                // 3. Autenticación de Usuario
                estado = CD_Usuario.Instancia.getValidaUsuarioIDB2(login.Usuario, login.Clave);

                if (estado == "200")
                {
                    if (CD_Usuario.Instancia.GetUsuarioExistePorCodigo(login.Usuario))
                    {
                        var ousuario = CD_Usuario.Instancia.GetUsuarioPorCodigo(login.Usuario);
                        var oMenu = CD_Menu.Instancia.GetMenuPorCodigo(login.Usuario);
                        Session["Usuario"] = ousuario;
                        Session["MenuMaster"] = oMenu;
                        Session["name"] = ousuario.NombresUsuario + " " + ousuario.ApellidosUsuario;
                        Session["correo"] = ousuario.CorreoUsuario.Trim();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        login.Mensaje = "No existe el usuario por favor comunicarse con el administrador del sistema.";
                    }
                }
                else
                {
                    login.Mensaje = estado;
                }
            }
            catch (Exception ex)
            {
                login.Mensaje = ex.Message;
            }

            return View(login);
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