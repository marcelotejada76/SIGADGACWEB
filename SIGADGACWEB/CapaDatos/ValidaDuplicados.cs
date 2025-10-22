using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class ValidaDuplicados
    {
        public static Int32 Secuencial(Int32 sec)
        {
            string query = "select INDICE from OIDKEEPER where CODIGO='OID' ";//"select MAX(OID) as OID from MOVIMIENTOAERONAVE ";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        sec = Convert.ToInt32(dr["INDICE"].ToString().Trim());
                        //sec = Convert.ToInt32(dr["OID"].ToString().Trim());


                        //lstUceo.Add(oUceo);
                    }
                    dr.Close();
                }
                return sec;
            }
            catch (Exception ex)
            {

            }
            return sec;

        }

        //SECUENCIAS P9
        public static Int32 Secuencialp9(Int32 sec)
        {
            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            string cadena = "";

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {
                cadena = "select OPSSE4 from OPSAR2 ";

                cm.CommandText = cadena;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    sec = Convert.ToInt32(dr["OPSSE4"].ToString().Trim());

                }
                con.Close();
                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + cadena);

            }

            con.Close();

            return sec;

        }

        //ACTUALIZA SECUENCIA P9
        public static Int32 ActualizaSecuencialP9(Int32 sec)
        {

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            string cadena = "";

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {
                cadena = "update OPSAR2 set OPSSE4= " + sec + "";

                cm.CommandText = cadena;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + cadena);

            }

            con.Close();

            return sec;

        }
        public static bool DatosEncontrados(bool datos, string ORIGEN, string DESTINO, string NUMEROVUELO, string FECHAREAL, string HoraReal, Int32 OIDAERONAVECOMPANI)
        {

            string query = "SELECT ORIGEN,DESTINO,NUMEROVUELO,FECHAREAL,OIDAERONAVECOMPANI FROM  MOVIMIENTOAERONAVE WHERE " +
                "ORIGEN= '" + ORIGEN.Trim() + "' AND DESTINO= '" + DESTINO.Trim() + "' AND NUMEROVUELO= '" + NUMEROVUELO.Trim() + "' AND FECHAREAL= '" + FECHAREAL + "' AND HORAREAL = '" + HoraReal + "' AND OIDAERONAVECOMPANI= " + OIDAERONAVECOMPANI;
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string ORIGEND = dr["ORIGEN"].ToString().Trim();
                        //sec = Convert.ToInt32(dr["OID"].ToString().Trim());
                        if (ORIGEND != "")
                        {
                            datos = true;
                            break;
                        }
                    }
                    dr.Close();
                }
                return datos;
            }
            catch (Exception ex)
            {

            }
            return datos;

        }

    }
}
