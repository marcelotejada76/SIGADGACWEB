using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_SolicitudPOA
    {
        public static CD_SolicitudPOA _instancia = null;
        private CD_SolicitudPOA()
        {

        }

        public static CD_SolicitudPOA Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_SolicitudPOA();
                }
                return _instancia;
            }
        }

        #region "Certficado POA"

        /// <summary>
        /// Metodo Solicitar Certificado POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="cdireccion"></param>
        /// <returns>List</returns>
        public List<tbSolicitudPOA> SolicitarCertificadoSoloPOA(string canio, string cdireccion)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE  SOLCO5 = '" + cdireccion + "'");
                if (canio.Trim().Length > 0)
                {
                    sbSol.Append(" AND SOLAN1 = '" + canio + "'");
                }

                sbSol.Append(" AND (SOLTIP = 'CER' OR SOLTIP = 'ACT') ORDER BY SOLAN1, SOLNU3 DESC");
                query = sbSol.ToString();
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
                        oSolicitud.AnioSolicitud = dr["AnioSolicitud"].ToString();
                        oSolicitud.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitud.FechaSolicitud = dr["FechaSolicitud"].ToString();
                        oSolicitud.TipoSolicitud = dr["TipoSolicitud"].ToString();
                        oSolicitud.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitud.FechaRevision = dr["FechaRevision"].ToString();
                        oSolicitud.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
                        oSolicitud.FechaAprobacion = dr["FechaAprobacion"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        listarSolicitud.Add(oSolicitud);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        /// <summary>
        /// Metodo retorna un registro de la Solicitud de Certificación POA
        /// </summary>
        /// <param name="cAnio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns>tbSolicitudPOA</returns>
        public tbSolicitudPOA SolicitarCertificadoPOAPorAnioNumeroSolicitud(string cAnio, int numSolicitud)
        {
            tbSolicitudPOA solicitudPOA = new tbSolicitudPOA();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion, ");
                sbSol.Append(" ifnull((PLANOM || ' ' ||PLANO1), '') AS DescripcionActividadEjecutar");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE SOLAN1 = '" + cAnio + "' AND SOLNU3 = " + numSolicitud);
                query = sbSol.ToString();
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        solicitudPOA.AnioSolicitud = dr["AnioSolicitud"].ToString();
                        solicitudPOA.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        solicitudPOA.FechaSolicitud = dr["FechaSolicitud"].ToString();
                        solicitudPOA.TipoSolicitud = dr["TipoSolicitud"].ToString();
                        solicitudPOA.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        solicitudPOA.FechaRevision = dr["FechaRevision"].ToString();
                        solicitudPOA.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
                        solicitudPOA.FechaAprobacion = dr["FechaAprobacion"].ToString();
                        solicitudPOA.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        solicitudPOA.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        solicitudPOA.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        solicitudPOA.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        solicitudPOA.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        solicitudPOA.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        solicitudPOA.DescripcionActividadEjecutar = dr["DescripcionActividadEjecutar"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return solicitudPOA;
        }

        public DataSet SolicitudCertificadoPOAPorAnioNumeroSolicitud(string cAnio, int numSolicitud)
        {
            DataSet dsSolicitud = new DataSet();
            string query = string.Empty;
            try
            {
                query = "SELECT ('Solicitud de ' || ifnull(rtrim(ltrim((SELECT  VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = 'SOLTIP' AND VALVAL = SOLTIP))), '') || ' No: ' || (SOLAN1 || '-' || SOLNU3)) AS TituloSolicitud,"
                + " (SOLAN1 || '-' || SOLNU3) as anioSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud,"
                + " 'Con la finalidad de cumplir con las actividades programadas en el POA ' ||SOLAN1 || ' de la Dirección ' ||  TRIM(DIRDES) || ' para la consecución de los Objetivos Estratégicos Insititucionales de la DGAC, agradeceré disponer a quien corresponda se emita la Certificación POA de la actividad ' as DescripcionSolicitud1,"
                + " ' como consta en el POA, misma que se encuentra registrada en el POA ' || SOLAN1 || ' de la DGAC.'as DescripcionSolicitud2,"
                + " TRIM(LINAC1 || LINAC2 || LINAC3 || LINAC4 || LINAC5) as Actividad,   SOLAN1 as anio,  'Atenamente' as Atentamente, ('DIRECTOR(A) DE' || ' ' || TRIM(DIRDES)) as Direccion "                   
                + " FROM solar1 join LINARC on SOLAN1 = LINAN1 and SOLNU3 = LINNU1"
                + " join dgacdat.DIRARC ON SOLCO4 = DIRCO1 AND SOLCO5 = DIRCO3"
                + " WHERE SOLAN1 = '" + cAnio + "' AND SOLNU3 = " + numSolicitud;
              
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataAdapter da = new iDB2DataAdapter(cmd);
                    da.Fill(dsSolicitud);
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dsSolicitud;
        }

        /// <summary>
        /// Metodo Certificado Poa
        /// </summary>
        /// <param name="cAnio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns>DataSet</returns>
        public DataSet CertificadoPOAPorAnioNumeroSolicitud(string cAnio, int numSolicitud)
        {
            DataSet dsSolicitud = new DataSet();
            string query = string.Empty;
            try
            {
                query = "SELECT (L.LINAN1 || '-' ||CHAR(L.LINNU1)) as NO_SOLICITUD, INTEGER(L.LINNU1) AS N_SOLICITUD, YEAR(DATE(TO_DATE(S.SOLFE6,'YYYY/MM/DD'))) AS ANIO_SOLICITUD, "
                    + " S.SOLFE6 AS FECHA_SOLICITUD, CAST(C.CERNU2 AS INT) AS CERTIFICACION_POA_NO, S.SOLF01 AS FECHAAPRBCERTIFICADO, D.DIRDES AS DIRECCION_SOLICITANTE,"
                    + " (L.LINACT) AS ACTIVIDAD_P, (L.LINACT || ' ' || A.ACTDES) AS ACTIVIDAD_PRESUPUESTARIA, (L.LINC43 || ' ' || P.PRODE1) AS PROGRAMA, L.LINC35 AS GEOGRAFICO,"
                    + " (L.LINC42 || ' ' || T.TIPDE2) AS TIPO_ADQUISICION, (L.LINAC1 || L.LINAC2 || L.LINAC3 || L.LINAC4 || L.LINAC5) AS ACTIVIDAD_POA,"
                    + " (L.LINC31 || L.LINC32 || L.LINC33 || L.LINC34) AS PARTIDA_PRESUPUESTARIA, L.LINMON AS MONTO_TOTAL_USD, "
                    + " RTRIM(S.SOLUS6)|| '-' ||YEAR(DATE(TO_DATE(S.SOLFE6,'YYYY/MM/DD')))|| '-' ||INTEGER(L.LINNU1)  AS USUARIO"
                    + " FROM LINARC L"
                    + " JOIN CERAR2 C ON (L.LINAN1=C.CERANI AND L.LINNU1=C.CERNU4)"
                    + " JOIN SOLAR1 S ON (S.SOLAN1=L.LINAN1 AND S.SOLNU3=L.LINNU1)"
                    + " JOIN DIRARC D ON (D.DIRCO3=S.SOLCO5)"
                    + " JOIN ACTARC A ON (A.ACTCOD=L.LINACT)"
                    + " JOIN PROAR1 P ON (P.PROCO6=L.LINC43)"
                    + " JOIN TIPAR2 T ON (T.TIPCO1=L.LINC42)"
                    + " WHERE SUBSTRING(S.SOLFE6, 1, 4) = '" + cAnio + "' AND L.LINNU1 = " + numSolicitud;

                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataAdapter da = new iDB2DataAdapter(cmd);
                    da.Fill(dsSolicitud);
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dsSolicitud;
        }

        public bool ApruebaEnviaSolicitudCertificadoPOA(string canio, int numSolicitud, string codUsuario)
        {
            bool status = false;
            StringBuilder sb = new StringBuilder();
            string queryUpdate = string.Empty;
            iDB2Command cmd;

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sb.Append("UPDATE SOLAR1 SET ");
                    sb.Append(" SOLES6 =  'SO',  SOLES7 = '', SOLFE6 = @fecha, SOLUS7 = @codDirector , SOLFE8 = @fecha, SOLHO7 = @hora, SOLDI3 = '' ");
                    sb.Append(" WHERE SOLAN1 = @canio AND SOLNU3 = @NumeroSolicitud");
                    queryUpdate = sb.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@fecha"].Value = osistema.FechaSistema;
                    cmd.Parameters["@codDirector"].Value = codUsuario;
                    cmd.Parameters["@hora"].Value = osistema.HoraSistema;
                    cmd.Parameters["@canio"].Value = canio;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }

        /// <summary>
        /// Metodo actualiza el estado de Revisado
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="codUsuario"></param>
        /// <param name="estadoAprobacion"></param>
        /// <param name="observacion"></param>
        /// <param name="observacion1"></param>
        /// <returns></returns>
        public bool RevisaSolicitudCertificadoPOA(string canio, int numSolicitud, string codUsuario, string estadoAprobacion, string observacion, string observacion1, string observacion2)
        {
            bool status = false;
            StringBuilder sb = new StringBuilder();
            string queryUpdate = string.Empty;
            iDB2Command cmd;

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sb.Append("UPDATE SOLAR1 SET ");
                    sb.Append(" SOLES7 = @estadoAprobado, SOLFE9 = @fecha, SOLUS8 = @codUsuario , SOLF04 = @fecha, SOLHO8 = @hora, SOLDI4 = '', ");
                    sb.Append(" SOLOB6 = @Observacion, SOLOB7 = @Observacion1, SOLOB8 = @Observacion2 ");
                    sb.Append(" WHERE SOLAN1 = @canio AND SOLNU3 = @NumeroSolicitud");
                    queryUpdate = sb.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@estadoAprobado"].Value = estadoAprobacion;
                    cmd.Parameters["@fecha"].Value = osistema.FechaSistema;
                    cmd.Parameters["@codUsuario"].Value = codUsuario;
                    cmd.Parameters["@hora"].Value = osistema.HoraSistema;
                    cmd.Parameters["@Observacion"].Value = campoNull(observacion);
                    cmd.Parameters["@Observacion1"].Value = campoNull(observacion1);
                    cmd.Parameters["@Observacion2"].Value = campoNull(observacion2);
                    cmd.Parameters["@canio"].Value = canio;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }


        /// <summary>
        /// Metodo Carga los datos de las solicitudes que estan por revisar o aprobar
        /// </summary>
        /// <param name="canio"></param>
        /// <returns></returns>
        public List<tbSolicitudPOA> RevisarAutualizarSolicitudCertificadoPOA(string canio)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE");
                if (canio.Trim().Length > 0)
                {
                    sbSol.Append(" SOLAN1 = '" + canio + "' AND ");
                }

                sbSol.Append(" SOLTIP <> 'MAR' AND SOLTIP <> 'MDP' AND SOLTIP <> 'MOD' AND SOLES6 = 'SO'  ORDER BY SOLAN1, SOLNU3 DESC");
                query = sbSol.ToString();
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
                        oSolicitud.AnioSolicitud = dr["AnioSolicitud"].ToString();
                        oSolicitud.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitud.FechaSolicitud = dr["FechaSolicitud"].ToString();
                        oSolicitud.TipoSolicitud = dr["TipoSolicitud"].ToString();
                        oSolicitud.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitud.FechaRevision = dr["FechaRevision"].ToString();
                        oSolicitud.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
                        oSolicitud.FechaAprobacion = dr["FechaAprobacion"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        listarSolicitud.Add(oSolicitud);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        /// <summary>
        /// Metodo Aprueba Solicitud de Certificado POA
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="estAut"></param>
        /// <param name="codUsuario"></param>
        /// <param name="observacion"></param>
        /// <returns></returns>
        public bool ApruebaSolicitudCertificadoPOA(string canio, int numSolicitud, string estAut, string codUsuario, string observacion, string observacion1)
        {
            bool respuesta = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command("PA_APRUEBASOLICITUDCERTIFICADOPOA", oConexion);
                    cmd.Parameters.AddWithValue("CANIO", canio);
                    cmd.Parameters.AddWithValue("NUMSOL", numSolicitud);
                    cmd.Parameters.AddWithValue("ESTAUT", estAut);
                    cmd.Parameters.AddWithValue("CUSER", codUsuario);
                    cmd.Parameters.AddWithValue("COBSER", campoNull(observacion));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }
                catch (iDB2Exception ex)
                {
                    throw ex;
                }
            }
            return respuesta;
        }

        #endregion

        public List<tbSolicitudPOA> SolicitudModificacionReprogramacionSoloPOA(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE ");
                sbSol.Append("FROM SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
                        oSolicitud.AnioSolicitud = dr["AnioSolicitud"].ToString();
                        oSolicitud.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitud.FechaSolicitud = dr["FechaSolicitud"].ToString();
                        oSolicitud.TipoSolicitud = dr["TipoSolicitud"].ToString();
                        oSolicitud.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitud.FechaRevision = dr["FechaRevision"].ToString();
                        oSolicitud.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
                        oSolicitud.FechaAprobacion = dr["FechaAprobacion"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        listarSolicitud.Add(oSolicitud);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

    }
}
