using CapaDatos;
using SistemaIntegradoGestion.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaIntegradoGestion.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult login()
        {
            ModelLogin modelLogin = new ModelLogin();
            return View(modelLogin);
        }

        [HttpGet]
        public ActionResult GetCaptchaImage()
        {
            Random random = new Random();
            // Generar código numérico de 4 dígitos para máxima claridad y legibilidad
            string captchaText = random.Next(1000, 9999).ToString();
            Session["CaptchaText"] = captchaText;

            int width = 160;
            int height = 44;

            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Fondo degradado suave
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, width, height),
                    Color.FromArgb(240, 244, 248),
                    Color.FromArgb(218, 225, 233),
                    45f))
                {
                    g.FillRectangle(brush, 0, 0, width, height);
                }

                // Líneas de ruido visual
                for (int i = 0; i < 4; i++)
                {
                    int x1 = random.Next(width);
                    int y1 = random.Next(height);
                    int x2 = random.Next(width);
                    int y2 = random.Next(height);
                    using (Pen pen = new Pen(Color.FromArgb(120, random.Next(100, 200), random.Next(100, 200), random.Next(100, 200)), 1.5f))
                    {
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                // Puntos de ruido
                for (int i = 0; i < 40; i++)
                {
                    int x = random.Next(width);
                    int y = random.Next(height);
                    bitmap.SetPixel(x, y, Color.FromArgb(random.Next(150, 255), random.Next(100, 180), random.Next(100, 180)));
                }

                // Dibujar el texto del Captcha (4 dígitos bien espaciados)
                using (Font font = new Font("Arial", 22, FontStyle.Bold))
                {
                    for (int i = 0; i < captchaText.Length; i++)
                    {
                        string character = captchaText[i].ToString();
                        float x = 16 + (i * 34);
                        float y = random.Next(3, 8);

                        g.TranslateTransform(x, y);
                        float angle = random.Next(-10, 10);
                        g.RotateTransform(angle);

                        using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(20, 50, 90)))
                        {
                            g.DrawString(character, font, textBrush, 0, 0);
                        }

                        g.RotateTransform(-angle);
                        g.TranslateTransform(-x, -y);
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return File(ms.ToArray(), "image/png");
                }
            }
        }

        [HttpPost]
        public ActionResult login(ModelLogin login)
        {
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

                // 2. Validación del CAPTCHA
                string sessionCaptcha = Session["CaptchaText"] as string;

                if (string.IsNullOrWhiteSpace(login.CaptchaInput))
                {
                    login.Mensaje = "Debe ingresar el código de verificación CAPTCHA..!!";
                    return View(login);
                }

                if (sessionCaptcha == null || !string.Equals(login.CaptchaInput.Trim(), sessionCaptcha, StringComparison.OrdinalIgnoreCase))
                {
                    login.Mensaje = "El código CAPTCHA ingresado es incorrecto..!!";
                    return View(login);
                }

                // Limpiar el código usado de la sesión
                Session["CaptchaText"] = null;

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

    }
}