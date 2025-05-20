using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbManifiestoPax
    {

        public string VUELO { get; set; }
        public string FECHAVLO { get; set; }
        public string OPERADOR { get; set; }
        public Int32 PAXADULTOS { get; set; }
        public Int32 PAXINF { get; set; }
        public Int32 PAXNIÑOS { get; set; }
        public Int32 TOTAL { get; set; }

        public List<tbDetallePasajerosManifiestoPax> oDetallePasajeroManifiesto { get; set; }
    }
}
