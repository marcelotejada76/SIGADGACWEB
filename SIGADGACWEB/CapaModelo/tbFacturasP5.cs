using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbFacturasP5
    {
        public Int32 OIDFACTURA { get; set; }
        public Int32 OIDDOCUMENTOCC { get; set; }
        
        public string NOMBRECLIENTE { get; set; }
        public string CEDULA_RUC { get; set; }
        public Int32 NUMEROFACTURA { get; set; }
        public string FECHACREA { get; set; }
        public string ESTADO { get; set; }
        public decimal VALORFACTURA { get; set; }
        public string CODIGO { get; set; }
        public string RESULTADO { get; set; }
        public string AUTORIZACION { get; set; }
        public string FECHARECAUDACION { get; set; }
        public string NUMERODEPOSITO { get; set; }
        public List<tbFacturasDetalleP5> oDetalleFactura { get; set; }
        public List<tbRecaudacionDetalleP5> oDetalleRecaudacion { get; set; }
    }
}
