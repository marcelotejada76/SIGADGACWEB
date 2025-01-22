using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbBancoRuminahui
    {
        public string FECHAPROCESO { get; set; }
        public string NUMEROCOMPROBANTE { get; set; }
        public string FECHABCOCENTRAL { get; set; }
        public string NUMCOMPBCOCENTRAL { get; set; }
        public string ESTADO { get; set; }
        public string ZONAL { get; set; }
        public string DEPOSITANTE { get; set; }
        public string HORA { get; set; }
        public string OFICINA { get; set; }
        public string CONCEPTO { get; set; }
        public decimal MONTO { get; set; }
        public string NUMEROFACTURA { get; set; }
        public string CODIGOCOMPROBANTE { get; set; }
        public List<tbDetalleComprobantesDeposito> oDetalleDeposito { get; set; }
        public decimal TOTAL { get; set; }
        public decimal DIFERENCIA { get; set; }

    }
}
