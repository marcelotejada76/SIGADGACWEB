using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaIntegradoGestion.Models
{
    public class ModelLogin
    {
        private string usuario;
        private string clave;
        private string mensaje;

        public ModelLogin() { }
        public ModelLogin(string usuario, string clave)
        {
            this.usuario = usuario;
            this.clave = clave;
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Clave
        {
            get { return clave; }
            set { clave = value; }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }
    }
}