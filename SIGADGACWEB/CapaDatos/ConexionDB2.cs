using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ConexionDB2
    {
     //   static string cadenaConexion = "DataSource=190.152.8.185;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDAT;";

        
      static string cadenaConexion = "DataSource=190.152.8.185;UserID=DGACCONEXI;Password=DGACTIC20@;Database=S10a1a05;DataCompression=True;Default Collection = DGACDATPRO;";
        

        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }

        public static string DireccionCorreo()
        {
            // 1. Inicializamos la variable de retorno como vacía o null
            string correoResultado = string.Empty;

            string query = "SELECT OPCD44 FROM OPCA44 WHERE OPCS22 = 1";

            iDB2Connection con = new iDB2Connection(cadenaConexion);
            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {
                con.Open();
                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                // 2. Evaluamos si la tabla tiene al menos un registro devuelto por IBM DB2
                if (dt.Rows.Count > 0)
                {
                    // Tomamos la primera fila [0] y la columna "OPCD44" directamente
                    correoResultado = dt.Rows[0].Field<string>("OPCD44").Trim();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el registro técnico.: " + ex.Message);
            }
            finally
            {
                // 3. Garantizamos el cierre de la conexión pase lo que pase
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            // Retorna el correo puro (ej: "ejemplo@correo.com") o string.Empty si no se encontró nada
            return correoResultado;
        }

    }
}
