using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbDetalleRuta
    {
        public decimal NumeroSolicitud { get; set; }
        public decimal IdRuta { get; set; }
        public string RutaOrigenInicio { get; set; }
        public string RutaDestino { get; set; }
        public string RutaOrigenFinal { get; set; }
        public string FechaIdaVuelo { get; set; }
        public string FechaRetornoVuelo { get; set; }
        public string HoraEstimadaVuelo { get; set; }
        public string DerechoSolicitado { get; set; }
        public string ObservacionRuta { get; set; }
        public string RutaAtoAterrizajeEC { get; set; }
        public string IdentificacionVuelo { get; set; }
        public string TipoVuelo { get; set; }
        public string DescripcionTipoVuelo { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string UsuarioModificado { get; set; }
        public string FechaModificado { get; set; }
        public string HoraModificado { get; set; }
        public string RutaVuelo { get; set; }
    }
}
