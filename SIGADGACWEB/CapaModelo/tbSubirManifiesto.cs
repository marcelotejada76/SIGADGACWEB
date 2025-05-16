using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbSubirManifiesto
    {
       
        public string UsuarioRuc { get; set; }
        public string RazonSocial { get; set; }
        public string Contraseña { get; set; }
        public string CorreoElectronico { get; set; }
       
        public List<tbModelArchivo> oModelArchivo { get; set; }
    }
}
