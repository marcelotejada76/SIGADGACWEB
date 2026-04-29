using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepositosCompanias.Models
{
    public class LoginModel
    {
        private string mensaje;
        ///<summary>
        /// Gets or sets Correo electronico.
        ///</summary>
        public string Username { get; set; }
        ///<summary>
        /// Gets or sets Contraseña.
        ///</summary>
        public string Contrasena { get; set; }

        ///<summary>
        /// Gets or sets Mensaje.
        ///</summary>
        public string Menssaje { get; set; }

        ///<summary>
        /// Gets or sets ImageData.
        ///</summary>
        public string ImageData { get; set; }

        ///<summary>
        /// Gets or sets CaptchaAnswer.
        ///</summary>
        public string CaptchaAnswer { get; set; }
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }
    }
}