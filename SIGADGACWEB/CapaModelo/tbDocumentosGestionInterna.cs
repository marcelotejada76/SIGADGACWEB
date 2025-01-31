using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbDocumentosGestionInterna
    {
        public int Codigo { get; set; }
        public string NombreArchivo { get; set; }
        public string Estado { get; set; }

        public int Gestion { get; set; }
        public Int32 Secuencia { get; set; }
        
        public List<tbDocumentosDsnaDescarga> oDetalleDocumentosDescargaDsna { get; set; }

    }
}
