using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SistemaIntegradoGestion.Utilitario
{
    public class CertificadoFirma
    {
        public static CertificadoFirma _instancia = null;
        private CertificadoFirma()
        {

        }

        public static CertificadoFirma Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CertificadoFirma();
                }
                return _instancia;
            }
        }

        public byte[] LeerArchivo(string nombreArchivo)
        {
            FileStream f = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
    }
}