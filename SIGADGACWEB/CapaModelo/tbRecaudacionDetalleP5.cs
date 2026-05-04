using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbRecaudacionDetalleP5
    {
        public string FECHARECAUDACION { get; set; }
        public string FECHAANULACION { get; set; }
        public string TIPO { get; set; }
        public string CODIGO { get; set; }
        public string CONCEPTO { get; set; }
        
        public decimal VALORRECAUDADO { get; set; }
        public decimal VALOREFECTIVO { get; set; }
        public decimal VALORCHEQUE { get; set; }
        public decimal VALORABONO { get; set; }
        public decimal VALORNOTACREDITO { get; set; }
        public decimal VALORDEPOSITO { get; set; }

        public string FECHACREACION { get; set; }
        public string USUARIOCREACION { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string DEPARTAMENTO { get; set; }

    }
}
