using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbLiquidacionCoactiva
    {
        
       

        public string PROCEDIMIENTOCOACTIVO { get; set; }
        public string TITULOCREDITO { get; set; }
        public string TIPODOCUMENTO { get; set; }
        public string RUC { get; set; }

        public string NOMBRECIA { get; set; }
        public string FECHALIQUIDACION { get; set; }
        public string DOCUMENTO { get; set; }
        public string TIPO { get; set; }
        public string FECHAEMISION { get; set; }
        public string FECHARECEPCION { get; set; }
        public string FECHAVENCIMIENTO { get; set; }

        public string FECHAPAGO { get; set; }

        public int CANTIDAD { get; set; }
        public string USUARIOCREA { get; set; }
        public string DESCRIPCIONCUENTA { get; set; }
        public decimal TOTALMULTA { get; set; }
        public decimal TOTALAJUSTEECONOMICO { get; set; }
        public decimal INTERESES { get; set; }
        public decimal COSTAS { get; set; }

        public decimal TOTALGENERAL { get; set; }
        public decimal GESTIONCOBRO { get; set; }

        public string ELABORADOPOR { get; set; }
        public string CARGOELABORADO { get; set; }
        public string REVISADOPOR { get; set; }

        public string CARGOREVISADO { get; set; }

        public string APROBADOPOR { get; set; }

        public string CARGOAPROBADO { get; set; }
        public string AÑO { get; set; }
    }
}
