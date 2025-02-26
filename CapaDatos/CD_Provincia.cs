using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_Provincia
    {
        public static CD_Provincia _instancia = null;
        private CD_Provincia()
        {

        }

        public static CD_Provincia Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Provincia();
                }
                return _instancia;
            }
        }

        //public List<tbProvincia> 
    }
}
