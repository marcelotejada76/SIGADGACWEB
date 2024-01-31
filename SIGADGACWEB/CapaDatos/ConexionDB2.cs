using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ConexionDB2
    {
        static string cadenaConexion = "DataSource=190.152.8.185;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        //"DataSource=172.20.16.163;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }

    }
}
