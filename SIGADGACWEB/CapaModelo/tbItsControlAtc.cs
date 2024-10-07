using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbItsControlAtc
    {
        public string LUGAR { get; set; }
        public string DEPENDENCIA { get; set; }
        public string FECHAELABORACION { get; set; }
        public string TURNO { get; set; }
        public string ATCORESPONSABLETUR { get; set; }
        public string LICENCIARESPONSABLE { get; set; }
        public string ATCORESPTURNOSAL { get; set; }
        public string LICENCIARESPSALIEN { get; set; }
        public string NOMBRERESPONSABLE { get; set; }
        public string NOMBRERESSALIDA { get; set; }
        public Int16 IFRDEP { get; set; }
        public Int16 IFRARR { get; set; }
        public Int16 VFRDEP { get; set; }
        public Int16 VFRARR { get; set; }
        public Int16 OVR { get; set; }
        public Int16 TGL { get; set; }
        public Int16 SOBSEGU { get; set; }
        public Int16 TOTGENSEGU        { get; set; }


       
        public List<tbDetalleControladoresAtc> oDetalleControladorAtc { get; set; }
        public List<tbDetalleNotamsAtc> oDetalleNotamsAtc { get; set; }
        public List<tbDetalleEventosAtc> oDetalleEventosAtc { get; set; }

    }
}
