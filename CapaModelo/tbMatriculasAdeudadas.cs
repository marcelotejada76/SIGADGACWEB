using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbMatriculasAdeudadas
    {
        public Int32 OIDDEUDOR { get; set; }
        public string RUC { get; set; }
        public string MATRICULA { get; set; }
        public string COMPAÑIA { get; set; }
        public string NUMEROFACTURA { get; set; }
        public decimal VALORFACTURA { get; set; }
        public decimal VALORSALDO { get; set; }
        //public int NUMEROFACTURA { get; set; }
        public string FECHAFACTURA { get; set; }
        public string FECHARECEPCION { get; set; }
        public string FECHAVENCIMIENTO { get; set; }


    }
}
