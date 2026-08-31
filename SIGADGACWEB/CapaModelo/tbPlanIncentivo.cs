using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbPlanIncentivo
    {
      

      public Int32 OID { get; set; }
        public Int32 OIDCOMPAÑIA { get; set; }
        public string COMPAÑIAOACI { get; set; }
        public string CONTRATO { get; set; }
        public string PERMISO { get; set; }
        public string TIPOVUELO { get; set; }
        public string NUMEROVUELO { get; set; }
        public string VUELO { get; set; }
        public string FRECUENCIA { get; set; }
        public string ESTADO { get; set; }
        public string MIGRADO { get; set; }
        public string FECHAINICIO { get; set; }
        public string FECHAFIN { get; set; }
        public string CONTRATOTXT { get; set; }
        public string FECHACANCELA { get; set; }
        public string CANCELADAPOR { get; set; }
        public string NOMBRECANCELA { get; set; }
        public string MOTIVO { get; set; }
        public string OBSERVACIONES{ get; set; }
        public string USERCR { get; set; }
        public string DATECR { get; set; }
        public string HORACR { get; set; }
        public string USERMD { get; set; }
        public string DATEMD { get; set; }
        public string HORAMD { get; set; }
    }
}
