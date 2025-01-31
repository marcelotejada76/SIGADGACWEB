using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbDocumentosAtoInt
    {
        public int Codigo { get; set; }
        public int Grupo { get; set; }
        public int Nivel { get; set; }
        public int Subnivel { get; set; }
        public string Estado { get; set; }
        public string NombreGrupo { get; set; }
        public string NombreNivel { get; set; }
        public string NombreSubnivel { get; set; }
        public string Carpeta { get; set; }
        public int Gestion { get; set; }
        public Int32 Secuencia { get; set; }
        
        public List<tbDocumentosAtoDescarga> oDetalleDocumentosDescargaAto { get; set; }

    }
}
