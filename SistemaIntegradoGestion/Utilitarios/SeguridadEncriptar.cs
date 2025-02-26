using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SistemaIntegradoGestion.Utilitario
{
    public class SeguridadEncriptar
    {
        /// Encripta una cadena
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            if (_cadenaAencriptar.Trim().Length > 0 || _cadenaAencriptar != null)
            {
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
                result = Convert.ToBase64String(encryted);
            }

            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            if (_cadenaAdesencriptar.Trim().Length > 0 || _cadenaAdesencriptar != null)
            {
                byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
                result = System.Text.Encoding.Unicode.GetString(decryted);
            }

            return result;
        }

        public static string GetSHA256(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] stream = null;
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            try
            {
                stream = sha256.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sb.ToString();
        }
    }
}