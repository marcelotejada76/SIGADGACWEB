using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_UbicacionUsuario
    {
        public static CD_UbicacionUsuario _instancia = null;
        //string biblioteca = "DGACDAT";
        private CD_UbicacionUsuario()
        {

        }

        public static CD_UbicacionUsuario Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_UbicacionUsuario();
                }
                return _instancia;
            }
        }

        public tbUbicacionUsuario UbicacionUsuarioPorCiudad(string codCiudad)
        {
            tbUbicacionUsuario oUbicacionUsuario = new tbUbicacionUsuario();
            string query = "SELECT ifnull(OPUOID, 0) AS OidUbicacion, ifnull(rtrim(ltrim(OPUEST)), '') AS Estacion,  ifnull(rtrim(ltrim(OPUCOD)), '') AS CodigoCiudad  "
                    + " FROM OPUARC01 WHERE OPUCOD = '" + codCiudad + "' AND OPUOID > 2 ";
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oUbicacionUsuario.OidUbicacion = decimal.Parse(dr["OidUbicacion"].ToString());
                        oUbicacionUsuario.Estacion = dr["Estacion"].ToString();
                        oUbicacionUsuario.CodigoCiudad = dr["CodigoCiudad"].ToString();

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                return oUbicacionUsuario = null;
            }
            return oUbicacionUsuario;
        }

        public tbUbicacionUsuario UbicacionAeropuertoUsuarioPorCiudad(string codCiudad)
        {
            tbUbicacionUsuario oUbicacionUsuario = new tbUbicacionUsuario();
            string query = "SELECT ifnull(OIDOI2, 0) AS OidUbicacion, ifnull(rtrim(ltrim(OIDNO2)), '') AS Estacion,  ifnull(rtrim(ltrim(OIDCO3)), '') AS CodigoCiudad  "
                    + " FROM Oidar2 WHERE OIDCO3 = '" + codCiudad + "'";
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oUbicacionUsuario.OidUbicacion = decimal.Parse(dr["OidUbicacion"].ToString());
                        oUbicacionUsuario.Estacion = dr["Estacion"].ToString();
                        oUbicacionUsuario.CodigoCiudad = dr["CodigoCiudad"].ToString();

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                return oUbicacionUsuario = null;
            }
            return oUbicacionUsuario;
        }
    }
}
