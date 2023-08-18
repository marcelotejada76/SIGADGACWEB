using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbSubMenu
    {
        public string CodigoSubsistema { get; set; }
        public string CodigoGestion { get; set; }
        public string CodigoModulo { get; set; }
        public string CodigoRol { get; set; }
        public string CodigoMenu { get; set; }
        public decimal IdSubMenu { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string Vista { get; set; }
        public int Estado { get; set; }
        public string UsuarioCreado { get; set; }
        public string FechaCreado { get; set; }
        public string HoraCreado { get; set; }
        public string UsuarioModificado { get; set; }
        public string FechaModificado { get; set; }
        public string HoraModificado { get; set; }
        public string Servidor { get; set; }
        public string ReporteUrl { get; set; }
    }
}
