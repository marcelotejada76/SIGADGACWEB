
using CapaDatos;
using SistemaIntegradoGestion.Models;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public ActionResult login(ModelLogin login)
        {
            string estado = string.Empty;
            try
            {
                login.Mensaje = string.Empty;
                if ((login.Usuario == null) && (login.Clave == null))
                {
                    login.Mensaje = "Usuario y Password en blanco, debe ingresar ..!!";
                }
                else if ((login.Usuario == null) && (login.Clave != null))
                {
                    login.Mensaje = "El usuario en blanco, debe ingresar ..!!";
                }
                else if ((login.Usuario != null) && (login.Clave == null))
                {
                    login.Mensaje = "La contraseña en blanco, debe ingresar ..!!";
                }

                if ((login.Usuario != null) && (login.Clave != null))
                {
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
            }
            catch (Exception ex)
            {
                login.Mensaje = ex.Message;
                throw ex;
            }

            return View(login);
        }

    }
}