using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbDetalleAdjunto
    {
        public decimal NumeroSolicitud { get; set; }
        public decimal SecuencialAdjunto { get; set; }
        public string CodigoDocumentoRequerido { get; set; }
        public string DescripcionDocumentoRequerido { get; set; }
        public string NombreDocumentoRequerido { get; set; }
        public string ObservacionDocumentoRequerido { get; set; }
    }
}
