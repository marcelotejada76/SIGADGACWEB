using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using System.Data.Odbc;
namespace CapaDatos
{
   public class CD_SecuencialAppP5
    {
        public static CD_SecuencialAppP5 _instancia = null;

        private CD_SecuencialAppP5()
        {

        }

        public static CD_SecuencialAppP5 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_SecuencialAppP5();
                }
                return _instancia;
            }
        }

        public tbSecuencialAppP5 SecuencialAppPorTablaCampo(string tabla, string campo)
        {
            tbSecuencialAppP5 osecuencial = new tbSecuencialAppP5();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT COALESCE(OID, 0) AS OID, COALESCE(TABLA, '') AS TABLA, COALESCE(CAMPO, '') AS CAMPO, COALESCE(USUARIOMODIFICA, '') AS USUARIOMODIFICA , COALESCE(FECHAMODFICA, CURRENT TIMESTAMP) AS FECHAMODFICA, COALESCE(NUMERO, 0) AS NUMERO, COALESCE(TIPO, '') AS TIPO");
                sbSol.Append(" FROM SECUENCIALAPP WHERE TABLA = '" + tabla + "' AND CAMPO = '" + campo + "'");

                query = sbSol.ToString();
                OdbcCommand cmd;


                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();

                    // iDB2DataReader dr = cmd.ExecuteReader();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        osecuencial.Oid = Convert.ToInt32(dr["OID"].ToString());
                        osecuencial.Tabla = dr["TABLA"].ToString();
                        osecuencial.Campo = dr["CAMPO"].ToString();
                        osecuencial.UsuarioModifica = dr["USUARIOMODIFICA"].ToString();
                        osecuencial.FechaModifica = dr["FECHAMODFICA"].ToString();
                        osecuencial.Numero = Convert.ToInt32(dr["NUMERO"].ToString());
                        osecuencial.Tipo = dr["TIPO"].ToString();
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return osecuencial;
        }

        /// <summary>
        /// Crea un nuevo registro de la tabla SecuencialApp
        /// </summary>
        /// <param name="osecuencial"></param>
        /// <returns>True o False</returns>
        public bool SecuencialAppNuevoRegistro(tbSecuencialAppP5 osecuencial)
        {
            bool estadoGrabar = false;
            StringBuilder sbSecuencial = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSecuencial.Append("INSERT INTO SECUENCIALAPP(OID, TABLA, CAMPO, USUARIOMODIFICA, FECHAMODFICA, NUMERO, TIPO)");
                sbSecuencial.Append(" VALUES((SELECT MAX(OID)+ 1 FROM SECUENCIALAPP), '" + osecuencial.Tabla + "', '" + osecuencial.Campo + "', '" + osecuencial.UsuarioModifica + "',  CURRENT TIMESTAMP, " + osecuencial.Numero + ",'" + osecuencial.Tipo + "')");

                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }

        /// <summary>
        /// Modifica el registro de la tabla de SecuencialApp
        /// Por parametro Oid
        /// </summary>
        /// <param name="osecuencial"></param>
        /// <returns>True o False</returns>
        public bool SecuencialAppActualizaRegistro(tbSecuencialAppP5 osecuencial)
        {
            bool estadoGrabar = false;
            StringBuilder sbSecuencial = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSecuencial.Append("UPDATE SECUENCIALAPP ");
                sbSecuencial.Append(" SET TABLA = '" + osecuencial.Tabla + "', CAMPO = '" + osecuencial.Campo + "', USUARIOMODIFICA = '" + osecuencial.UsuarioModifica + "', FECHAMODFICA = CURRENT TIMESTAMP, NUMERO = " + osecuencial.Numero + ", TIPO = '" + osecuencial.Tipo + "'");
                sbSecuencial.Append(" WHERE OID = " + osecuencial.Oid);

                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }

        /// <summary>
        /// Actualiza los campos de numero, usuario, fecha
        /// </summary>
        /// <param name="secuencial"></param>
        /// <param name="tabla"></param>
        /// <param name="campo"></param>
        /// <param name="usuarioModifica"></param>
        /// <returns>True o False</returns>
        public bool SecuencialAppActualizaSecuencialTablaCampo(int secuencial, string tabla, string campo, string usuarioModifica)
        {
            bool estadoGrabar = false;
            StringBuilder sbSecuencial = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSecuencial.Append("UPDATE SECUENCIALAPP");
                sbSecuencial.Append("SET NUMERO = " + secuencial + ", USUARIOMODIFICA = '" + usuarioModifica + "', FECHAMODFICA = CURRENT TIMESTAMP");
                sbSecuencial.Append("WHERE TABLA = '" + tabla + "' AND CAMPO = '" + campo + "'");

                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }

        /// <summary>
        /// Elimina el registro de la tabla de SecuencialAPP
        /// </summary>
        /// <param name="secuencial"></param>
        /// <returns>True o False</returns>
        public bool SecuencialAppEliminarRegistro(int secuencial)
        {
            bool estadoGrabar = false;
            StringBuilder sbSecuencial = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSecuencial.Append("DELETE SECUENCIALAPP");
                sbSecuencial.Append("WHERE OID = " + secuencial);

                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }

        public bool SecuencialAppActualizaTipo(int secuencial, string tipo)
        {
            bool estadoGrabar = false;
            StringBuilder sbSecuencial = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSecuencial.Append("UPDATE SECUENCIALAPP");
                sbSecuencial.Append(" SET TIPO = '" + tipo + "'");
                sbSecuencial.Append("WHERE OID = " + secuencial);

                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }
    }
}
