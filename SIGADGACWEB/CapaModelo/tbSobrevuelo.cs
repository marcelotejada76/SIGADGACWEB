using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbSobrevuelo
    {
        public string FechaVuelo { get; set; }
        public string CAllSingn { get; set; }
        public string Matricula { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int Distancia { get; set; }
        public string InitialTime { get; set; }
        public string CiaFactura { get; set; }
        public string Ruta { get; set; }
        public double LatitudOrigen { get; set; }
        public double LongitudOrigen { get; set; }
        public double LatitudDestino { get; set; }
        public double LongitudDestino { get; set; }
        public string Operador { get; set; }
        public string MOdelo { get; set; }
    }
}
