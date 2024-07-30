using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Depositos
    {
        public static CD_Depositos _instancia = null;
        private CD_Depositos()
        {

        }

        public static CD_Depositos Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Depositos();
                }
                return _instancia;
            }
        }

        public List<tbSubirDepositos> DetalleDepositos(string canio, string ruc)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICRU1 AS RUC  from ficar6 where ficano='"+canio+"' and ficru1='"+ruc+"'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;
                

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                      //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();
                        
                       

                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        //public List<tbSubirDepositos> SolicitudModificacionSinAfectacionPresupetariaListarDireccionAnio(string cdireccion, string canio)
        //{
        //    List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
        //        sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
        //        sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera,   ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ");
        //        sbSol.Append(" (SELECT  ifnull(SUM(LINMO8), 0) FROM DGACDAT.LINAR2 WHERE LINES4 = 'DESTINO' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALDESTINO,  (SELECT  ifnull(SUM(LINMO8), 0) FROM DGACDAT.LINAR2 WHERE LINES4 = 'ORIGEN' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALORIGEN");
        //        sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
        //        sbSol.Append(" WHERE  SOLCO5 = '" + cdireccion + "'");
        //        if (canio.Trim().Length > 0)
        //        {
        //            sbSol.Append(" AND SOLAN1 = '" + canio + "'");
        //        }

        //        sbSol.Append(" AND (SOLTIP = 'MDP') ORDER BY SOLAN1, SOLNU3 DESC");
        //        query = sbSol.ToString();
        //        iDB2Command cmd;

        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
        //                oSolicitud.AnioSolicitud = dr["AnioSolicitud"].ToString();
        //                oSolicitud.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
        //                oSolicitud.FechaSolicitud = dr["FechaSolicitud"].ToString();
        //                oSolicitud.TipoSolicitud = dr["TipoSolicitud"].ToString();
        //                oSolicitud.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
        //                oSolicitud.FechaRevision = dr["FechaRevision"].ToString();
        //                oSolicitud.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
        //                oSolicitud.FechaAprobacion = dr["FechaAprobacion"].ToString();
        //                oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
        //                oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
        //                oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
        //                oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
        //                oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
        //                oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
        //                oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
        //                oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
        //                oSolicitud.ValOrigen = decimal.Parse(dr["VALORIGEN"].ToString());
        //                oSolicitud.ValDestino = decimal.Parse(dr["VALDESTINO"].ToString());
        //                listarSolicitud.Add(oSolicitud);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return listarSolicitud;
        //}
    }
}
