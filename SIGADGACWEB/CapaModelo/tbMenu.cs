using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbMenu
    {
        public string CodigoSubsistema { get; set; }
        public string CodigoGestion { get; set; }
        public string CodigoModulo { get; set; }
        public string CodigoRol { get; set; }
        public string CodigoMenu { get; set; }
        public string DescripcionMenu { get; set; }
        public string TipoOpcionMenu { get; set; }
        public string EstadoMenu { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string DispositivoCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
        public string HoraModificacion { get; set; }
        public string DispositivoModificacion { get; set; }
        public List<tbSubMenu> oSubMenu { get; set; }
    }
}
