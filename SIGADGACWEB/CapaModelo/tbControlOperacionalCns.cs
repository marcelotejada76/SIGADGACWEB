using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbControlOperacionalCns
    {
        public string FECHACONTROL { get; set; }
        public string IMPRESO { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ELABORADO { get; set; }
        public string NOMBRETECNICO { get; set; }
        public string TURNO { get; set; }
        



        public List<tbDetalleControlOperacionalCns> oDetalleEventosCns { get; set; }

    }
}
