using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbGarantias
    {
        public string Ruc { get; set; }
        public string NumeroGarantia { get; set; }
        public string Compania_Contratista { get; set; }
        public string Fecha_Expedicion { get; set; }
        public string Administrador_Contrato { get; set; }
        public string Tipo_Garantia { get; set; }
        public string Concepto_Garantia { get; set; }
        public string Ruc_Cia_Seguros { get; set; }
        public string Razon_Social_Aseguradora { get; set; }
        public string Vigencia_Desde { get; set; }
        public string Vigencia_Hasta { get; set; }
        public string Dias_a_Caducar { get; set; }
        public string Seguro_de { get; set; }
        public string Estado { get; set; }
        public decimal Valor_Garantia { get; set; }
        public string Observaciones { get; set; }

        
    }
}
