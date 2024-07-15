using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ConexionDB2
    {
        static string cadenaConexion = "DataSource=190.152.8.185;UserID=DGACTIC07;Password=gfl0r3s01;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        //static string cadenaConexion = "DataSource=190.152.8.185;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        //Conexión a la base de datos de PRODICCION
        //static string cadenaConexion = "DataSource=190.152.8.185;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDATPRO;";
        //"DataSource=172.20.16.163;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }

    }
}
