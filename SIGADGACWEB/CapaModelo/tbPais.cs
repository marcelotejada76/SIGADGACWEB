using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbPais
    {
        public string CodigoPais { get; set; }
        public string DescripcionPais { get; set; }
        public List<tbProvincia> oProvinvia { get; set; }
    }
}
