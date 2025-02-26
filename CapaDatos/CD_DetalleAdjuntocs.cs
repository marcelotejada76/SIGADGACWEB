using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
    public class CD_DetalleAdjuntocs
    {
        public static CD_DetalleAdjuntocs _instancia = null;
        //string biblioteca = "DGACDAT";
        private CD_DetalleAdjuntocs()
        {

        }

        public static CD_DetalleAdjuntocs Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleAdjuntocs();
                }
                return _instancia;
            }
        }

        public tbDetalleAdjunto DetalleAdjuntoPorID(decimal numSolicitud, decimal Secuencial)
        {
            tbDetalleAdjunto oDetalleAdjunto = new tbDetalleAdjunto();
            string query = "SELECT ifnull(DETNU2, 0) AS NumeroSolicitud, ifnull(DETSEC, 0) AS SecuencialAdjunto, ifnull(rtrim(ltrim(DETCO1)), '') AS CodigoDocumentoRequerido, "
                + " DETCO1 || '-' || ifnull(select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'DETCO1' and VALVAL = DETCO1), '') as DescripcionDocumentoRequerido, "
                + "  ifnull(rtrim(ltrim(DETNOM)), '') AS NombreDocumentoRequerido, ifnull(rtrim(ltrim(DETOBS)), '') AS ObservacionDocumentoRequerido "
                + " FROM DETAR2 WHERE DETNU2 = " + numSolicitud + " AND DETSEC = " + Secuencial;

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
                        oDetalleAdjunto.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oDetalleAdjunto.SecuencialAdjunto = decimal.Parse(dr["SecuencialAdjunto"].ToString());
                        oDetalleAdjunto.CodigoDocumentoRequerido = dr["CodigoDocumentoRequerido"].ToString();
                        oDetalleAdjunto.DescripcionDocumentoRequerido = dr["DescripcionDocumentoRequerido"].ToString();
                        oDetalleAdjunto.NombreDocumentoRequerido = dr["NombreDocumentoRequerido"].ToString();
                        oDetalleAdjunto.ObservacionDocumentoRequerido = dr["ObservacionDocumentoRequerido"].ToString();

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oDetalleAdjunto = null;
            }
            return oDetalleAdjunto;
        }

        public List<tbDetalleAdjunto> DetalleAdjuntoPorNumSolicitud(decimal numSolicitud)
        {
            List<tbDetalleAdjunto> listDetalleAdjunto = new List<tbDetalleAdjunto>();
            string query = "SELECT ifnull(DETNU2, 0) AS NumeroSolicitud, ifnull(DETSEC, 0) AS SecuencialAdjunto, ifnull(rtrim(ltrim(DETCO1)), '') AS CodigoDocumentoRequerido, "
                + " ifnull(DETCO1 || '-' || (select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'DETCO1' and VALVAL = DETCO1), '') as DescripcionDocumentoRequerido, "
                + "  ifnull(rtrim(ltrim(DETNOM)), '') AS NombreDocumentoRequerido, ifnull(rtrim(ltrim(DETOBS)), '') AS ObservacionDocumentoRequerido "
                + " FROM DETAR2 WHERE DETNU2 = " + numSolicitud;

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
                        tbDetalleAdjunto oDetalleAdjunto = new tbDetalleAdjunto();
                        oDetalleAdjunto.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oDetalleAdjunto.SecuencialAdjunto = decimal.Parse(dr["SecuencialAdjunto"].ToString());
                        oDetalleAdjunto.CodigoDocumentoRequerido = dr["CodigoDocumentoRequerido"].ToString();
                        oDetalleAdjunto.DescripcionDocumentoRequerido = dr["DescripcionDocumentoRequerido"].ToString();
                        oDetalleAdjunto.NombreDocumentoRequerido = dr["NombreDocumentoRequerido"].ToString();
                        oDetalleAdjunto.ObservacionDocumentoRequerido = dr["ObservacionDocumentoRequerido"].ToString();
                        listDetalleAdjunto.Add(oDetalleAdjunto);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listDetalleAdjunto = null;
            }
            return listDetalleAdjunto;
        }


        public bool RegistrarDetalleAdjunto(List<tbDetalleAdjunto> detalleAdjunto)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            EliminarDetalleAdjuntoPorSolicitud(Int32.Parse(detalleAdjunto[0].NumeroSolicitud.ToString()));
            foreach (var rows in detalleAdjunto)
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    try
                    {
                        queryInsertar = "INSERT INTO DETAR2 (DETNU2, DETSEC, DETCO1, DETNOM, DETOBS) " +
                            " VALUES(@NumeroSolicitud, @SecuencialAdjunto, @CodigoDocumentoRequerido, @NombreDocumentoRequerido, @ObservacionDocumentoRequerido)";
                        cmd = new iDB2Command(queryInsertar, oConexion);
                        oConexion.Open();
                        cmd.DeriveParameters();
                        cmd.Parameters["@NumeroSolicitud"].Value = rows.NumeroSolicitud;
                        cmd.Parameters["@SecuencialAdjunto"].Value = SecuencialDetalleAdjunto(rows.NumeroSolicitud);
                        cmd.Parameters["@CodigoDocumentoRequerido"].Value = campoNull(rows.CodigoDocumentoRequerido);
                        cmd.Parameters["@NombreDocumentoRequerido"].Value = campoNull(rows.NombreDocumentoRequerido);
                        cmd.Parameters["@ObservacionDocumentoRequerido"].Value = campoNull(rows.ObservacionDocumentoRequerido);
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

            return respuesta;
        }

        public bool ActualizaDetalleAdjunto(tbDetalleAdjunto detalleAdjuto)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE DETAR2 SET DETCO1 = @CodigoDocumentoRequerido, DETNOM = @NombreDocumentoRequerido, DETOBS = @ObservacionDocumentoRequerido "
                    + " WHERE DETNU2 = @NumeroSolicitud AND DETSEC = @SecuencialAdjunto";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@CodigoDocumentoRequerido"].Value = detalleAdjuto.CodigoDocumentoRequerido;
                    cmd.Parameters["@NombreDocumentoRequerido"].Value = detalleAdjuto.NombreDocumentoRequerido;
                    cmd.Parameters["@ObservacionDocumentoRequerido"].Value = detalleAdjuto.ObservacionDocumentoRequerido;
                    cmd.Parameters["@NumeroSolicitud"].Value = detalleAdjuto.NumeroSolicitud;
                    cmd.Parameters["@SecuencialAdjunto"].Value = detalleAdjuto.SecuencialAdjunto;
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

        public bool EliminarDetalleAdjuntoPorSolicitud(Int32 numeroSolicitud)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM DETAR2"
                    + " WHERE DETNU2 = @NumeroSolicitud";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = numeroSolicitud;
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

        public bool EliminarDetalleAdjuntoPorID(Int32 numeroSolicitud, Int32 secuencial)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM DETAR2"
                    + " WHERE DETNU2 = @NumeroSolicitud AND DETSEC = @Secuencial";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = numeroSolicitud;
                    cmd.Parameters["@Secuencial"].Value = secuencial;
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



        private Int32 SecuencialDetalleAdjunto(decimal numSolicitud)
        {
            string query = "SELECT IFNULL(max(DETSEC), 0) + 1 AS Secuencial FROM DETAR2 WHERE DETNU2 = " + numSolicitud;
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
