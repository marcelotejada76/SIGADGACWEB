using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbDetalleAeronave
    {
        public decimal NumeroSolicitud { get; set; }
        public decimal IdDetalleAeronave { get; set; }
        public string Matricula { get; set; }
        public string CodigoOaciCiaAvc { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string TipoAeronave { get; set; }
        public string PesoWTOKG { get; set; }
        public string FechaVigenciaSeguro { get; set; }
        public decimal EstadoExpiracion { get; set; }
        public string FechaExpiracion { get; set; }
        public string UsuarioCreado { get; set; }
        public string FechaCrado { get; set; }
        public string HoraCreado { get; set; }
        public string UsuarioModificado { get; set; }
        public string FechaModificado { get; set; }
        public string HoraModificado { get; set; }
        public bool CheckMatricula { get; set; }
    }
}
