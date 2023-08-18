using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_Rol
    {
        public static CD_Rol _instancia = null;
        private CD_Rol()
        {

        }

        public static CD_Rol Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Rol();
                }
                return _instancia;
            }
        }
    }
}
