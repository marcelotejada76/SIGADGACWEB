using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbDocumentoHabilitante
    {
        public string AnioSolicitud { get; set; }
        public string NumeroSolicitud { get; set; }
        public decimal SecuencialDocHblt { get; set; }
        public string TipoDocumento { get; set; }
        public string DireccionIP { get; set; }
        public string PathArchivo1 { get; set; }
        public string PathArchivo2 { get; set; }
        public string NombreArchivo1 { get; set; }
        public string NombreArchivo2 { get; set; }
        public string TipoArchivo { get; set; }
        public string EstadoDocumento { get; set; }
        public string UsuarioCreado { get; set; }
        public string FechaCreado { get; set; }
        public string HoraCreado { get; set; }
        public string DispositivoCreado { get; set; }
        public string UsuarioModificado { get; set; }
        public string FechaModificado { get; set; }
        public string HoraModificado { get; set; }
        public string DispositivoModificado { get; set; }
    }
}
