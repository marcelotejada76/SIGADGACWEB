using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class ConexionServerConfiar
    {
       // public static string Conexion = "Data Source=win-sqlserv-01;Initial Catalog=iFIS;User ID=adminsql;Password=sistema*-!;";       

        static string cadenaConexion = "Data Source=WIN-SQLSERV-02;Initial Catalog=COMFIAR;;Integrated Security=True;";

        //"DataSource=172.20.16.163;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }
    }
}
