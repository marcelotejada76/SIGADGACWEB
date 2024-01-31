using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbCertificadoDigital
    {
        public string CodigoUsuario { get; set; }
        public Int32 Secuencial { get; set; }
        public string Ruc { get; set; }
        public string NombresApellidos { get; set; }
        public string Cargo { get; set; }
        public string Serie { get; set; }
        public string Asignado { get; set; }
        public string FechaSubida { get; set; }
        public string FechaVencimiento { get; set; }
        public string Contrasena { get; set; }
        public string PathDocumento { get; set; }
        public string PathImagenFirma { get; set; }
        public string UsuarioCreado { get; set; }
        public string FechaCreado { get; set; }
        public string HoraCreado { get; set; }
        public string DispositivoCreado { get; set; }
        public string UsuarioModificado { get; set; }
        public string FechaModificado { get; set; }
        public string HoraModificado { get; set; }
        public string DispositivoModificado { get; set; }
        public Int32 OidCertificadoPadre { get; set; }
        public List<tbCertificadoDigital> oCertificado { get; set; }


    }
}
