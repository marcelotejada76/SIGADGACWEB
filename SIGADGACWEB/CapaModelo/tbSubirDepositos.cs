using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbSubirDepositos
    {
        public string Año { get; set; }
        public string Mes { get; set; }
        public string UsuarioRuc { get; set; }
        public string RazonSocial { get; set; }
        public string Contraseña { get; set; }
        public string CorreoElectronico { get; set; }
        public int Registros { get; set; }
        public string FechaSistema { get; set; }
        public string HoraSistema { get; set; }
        public List<tbModelArchivo> oModelArchivo { get; set; }
    }
}
