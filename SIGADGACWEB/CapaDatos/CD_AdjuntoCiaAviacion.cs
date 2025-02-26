using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_AdjuntoCiaAviacion
    {
        public static CD_AdjuntoCiaAviacion _instancia = null;
        private CD_AdjuntoCiaAviacion()
        {

        }

        public static CD_AdjuntoCiaAviacion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_AdjuntoCiaAviacion();
                }
                return _instancia;
            }
        }


        /// <summary>
        /// Metodo lista todos los documentos adjuntos a la compañia
        /// </summary>
        /// <returns></returns>
        public List<tbAdjuntoCiaAviacion> AdjuntoCiaAviacionListarTodos()
        {
            List<tbAdjuntoCiaAviacion> lstAdjuntoCia = new List<tbAdjuntoCiaAviacion>();
            string query = "SELECT ifnull(ADJOID, 0) AS OidP5, ifnull(ADJSEC, 0) AS SecuencialAdjunto, ifnull(rtrim(ltrim(ADJCOD)), '') AS CodigoDocumento, "
                + " ifnull((SELECT  VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = 'ADJCOD' AND VALVAL = ADJCOD), '') AS DescripcionDocumento, ifnull(rtrim(ltrim(ADJNOM)), '') AS NombreDocumento,"
                + " ifnull(rtrim(ltrim(ADJOBS)), '') AS Observacion, ADJEST AS EstadoDocumento FROM ADJARC ORDER BY ADJCOD ASC";
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
                        tbAdjuntoCiaAviacion oAdjuntoCia = new tbAdjuntoCiaAviacion();
                        oAdjuntoCia.OidP5 = decimal.Parse(dr["OidP5"].ToString());
                        oAdjuntoCia.SecuencialAdjunto = decimal.Parse(dr["SecuencialAdjunto"].ToString());
                        oAdjuntoCia.CodigoDocumento = dr["CodigoDocumento"].ToString();
                        oAdjuntoCia.DescripcionDocumento = dr["DescripcionDocumento"].ToString();
                        oAdjuntoCia.NombreDocumento = dr["NombreDocumento"].ToString();
                        oAdjuntoCia.Observacion = dr["Observacion"].ToString();
                        oAdjuntoCia.EstadoDocumento = int.Parse(dr["EstadoDocumento"].ToString());
                        lstAdjuntoCia.Add(oAdjuntoCia);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstAdjuntoCia = null;
            }
            return lstAdjuntoCia;
        }


        public tbAdjuntoCiaAviacion AdjuntoCiaAviacionPorIdSecuencial(int oidSecuencial)
        {
            tbAdjuntoCiaAviacion oAdjuntoCia = new tbAdjuntoCiaAviacion();
            string query = "SELECT ifnull(ADJOID, 0) AS OidP5, ifnull(ADJSEC, 0) AS SecuencialAdjunto, ifnull(rtrim(ltrim(ADJCOD)), '') AS CodigoDocumento, "
                + " ifnull((SELECT  VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = 'ADJCOD' AND VALVAL = ADJCOD), '') AS DescripcionDocumento, ifnull(rtrim(ltrim(ADJNOM)), '') AS NombreDocumento,"
                + " ifnull(rtrim(ltrim(ADJOBS)), '') AS Observacion, ADJEST AS EstadoDocumento FROM ADJARC WHERE ADJSEC = " + oidSecuencial + " ORDER BY ADJCOD ASC";
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

                        oAdjuntoCia.OidP5 = decimal.Parse(dr["OidP5"].ToString());
                        oAdjuntoCia.SecuencialAdjunto = decimal.Parse(dr["SecuencialAdjunto"].ToString());
                        oAdjuntoCia.CodigoDocumento = dr["CodigoDocumento"].ToString();
                        oAdjuntoCia.DescripcionDocumento = dr["DescripcionDocumento"].ToString();
                        oAdjuntoCia.NombreDocumento = dr["NombreDocumento"].ToString();
                        oAdjuntoCia.Observacion = dr["Observacion"].ToString();
                        oAdjuntoCia.EstadoDocumento = int.Parse(dr["EstadoDocumento"].ToString());

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oAdjuntoCia = null;
            }
            return oAdjuntoCia;
        }


        public List<tbAdjuntoCiaAviacion> AdjuntoCiaAviacionListarPorIdCia(decimal OidP5)
        {
            List<tbAdjuntoCiaAviacion> lstAdjuntoCia = new List<tbAdjuntoCiaAviacion>();
            string query = "SELECT ifnull(ADJOID, 0) AS OidP5, ifnull(ADJSEC, 0) AS SecuencialAdjunto, ifnull(rtrim(ltrim(ADJCOD)), '') AS CodigoDocumento, "
                + " ifnull((SELECT  VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = 'ADJCOD' AND VALVAL = ADJCOD), '') AS DescripcionDocumento, ifnull(rtrim(ltrim(ADJNOM)), '') AS NombreDocumento,"
                + " ifnull(rtrim(ltrim(ADJOBS)), '') AS Observacion, ADJEST AS EstadoDocumento FROM ADJARC WHERE ADJOID = " + OidP5 + " ORDER BY ADJCOD ASC";
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
                        tbAdjuntoCiaAviacion oAdjuntoCia = new tbAdjuntoCiaAviacion();
                        oAdjuntoCia.OidP5 = decimal.Parse(dr["OidP5"].ToString());
                        oAdjuntoCia.SecuencialAdjunto = decimal.Parse(dr["SecuencialAdjunto"].ToString());
                        oAdjuntoCia.CodigoDocumento = dr["CodigoDocumento"].ToString();
                        oAdjuntoCia.DescripcionDocumento = dr["DescripcionDocumento"].ToString();
                        oAdjuntoCia.NombreDocumento = dr["NombreDocumento"].ToString();
                        oAdjuntoCia.Observacion = dr["Observacion"].ToString();
                        oAdjuntoCia.EstadoDocumento = int.Parse(dr["EstadoDocumento"].ToString());
                        lstAdjuntoCia.Add(oAdjuntoCia);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstAdjuntoCia = null;
            }
            return lstAdjuntoCia;
        }

        public bool AdjuntoCiaAviacionNuevo(List<tbAdjuntoCiaAviacion> listarAdjunto)
        {
            bool respuesta = false;
            iDB2Command cmd;
            if (listarAdjunto.Count() > 0)
            {
                foreach (var item in listarAdjunto)
                {
                    string query = "INSERT INTO ADJARC (ADJOID, ADJSEC, ADJCOD, ADJNOM, ADJOBS ,ADJEST)" +
                                                       " VALUES(@OidP5, @SecuencialAdjunto, @CodigoDocumento, @NombreDocumento, @Observacion, @EstadoDocumento)";
                    using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        try
                        {
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            cmd.DeriveParameters();
                            cmd.Parameters["@OidP5"].Value = item.OidP5;
                            cmd.Parameters["@SecuencialAdjunto"].Value = secuencialaAdjunto();
                            cmd.Parameters["@CodigoDocumento"].Value = campoNull(item.CodigoDocumento);
                            cmd.Parameters["@NombreDocumento"].Value = campoNull(item.NombreDocumento);
                            cmd.Parameters["@Observacion"].Value = campoNull(item.Observacion);
                            cmd.Parameters["@EstadoDocumento"].Value = "1";

                            respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                            cmd.Dispose();
                            oConexion.Close();
                        }
                        catch (Exception ex)
                        {
                            respuesta = false;
                        }
                    }


                }
            }

            return respuesta;
        }

        public bool AdjuntoCiaAviacionNuevo(tbAdjuntoCiaAviacion AdjuntoCia)
        {
            bool respuesta = false;
            iDB2Command cmd;
            string query = "INSERT INTO ADJARC (ADJOID, ADJSEC, ADJCOD, ADJNOM, ADJOBS ,ADJEST)" +
                                                       " VALUES(@OidP5, @SecuencialAdjunto, @CodigoDocumento, @NombreDocumento, @Observacion, @EstadoDocumento)";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@OidP5"].Value = AdjuntoCia.OidP5;
                    cmd.Parameters["@SecuencialAdjunto"].Value = secuencialaAdjunto();
                    cmd.Parameters["@CodigoDocumento"].Value = campoNull(AdjuntoCia.CodigoDocumento);
                    cmd.Parameters["@NombreDocumento"].Value = campoNull(AdjuntoCia.NombreDocumento);
                    cmd.Parameters["@Observacion"].Value = campoNull(AdjuntoCia.Observacion);
                    cmd.Parameters["@EstadoDocumento"].Value = "1";

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }


            return respuesta;
        }


        public bool AdjuntoCiaAviacionEliminar(decimal idCompania)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM  ADJARC"
                    + " WHERE ADJOID = @IdCompania";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@IdCompania"].Value = idCompania;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool AdjuntoCiaAviacionEliminarDocumento(decimal idSecuencial)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM ADJARC"
                    + " WHERE ADJSEC = @IdSecuencial";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@IdSecuencial"].Value = idSecuencial;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }




        public bool AdjuntoCiaAviacionActualizar(decimal OidP5, string codigo, string documento)
        {
            bool respuesta = false;
            iDB2Command cmd;
            string query = "UPDATE ADJARC SET ADJNOM = @NombreDocumento WHERE ADJOID = @OidP5 AND ADJCOD =  @CodigoDocumento";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NombreDocumento"].Value = documento;
                    cmd.Parameters["@OidP5"].Value = OidP5;
                    cmd.Parameters["@CodigoDocumento"].Value = codigo;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool AdjuntoCiaAviacionExiste(decimal OidP5, string codigo, string nombreDocumento)
        {
            bool estado = false;
            string query = "SELECT ifnull(ADJOID, 0) AS OidP5 FROM ADJARC WHERE ADJOID = " + OidP5 + " AND ADJCOD = '" + codigo.ToUpper() + "' AND ADJNOM = '" + nombreDocumento.Trim() + "'";
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
                        estado = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }
        public decimal secuencialaAdjunto()
        {
            string query = "SELECT IFNULL(max(ADJSEC), 0) + 1 AS Secuencial FROM ADJARC";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}
