using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbDetalleComprobantesDeposito
    {
    
        public string FECHAPROCESO { get; set; }
        public string NUMEROCOMPROBANTE { get; set; }
        public int secuencia { get; set; }
        public string OFICINA { get; set; }
        public string CODIGORECAUDACION { get; set; }
        public string CLIENTE { get; set; }
        public string NUMEROFACTURA { get; set; }
        public string CTACONTABLE { get; set; }
        public string DESCCUENTA { get; set; }
        public string LUGAR { get; set; }
        public string TIPO { get; set; }
        public decimal MONTO { get; set; }
        public decimal MONTOGENERAL { get; set; }


    }
}
