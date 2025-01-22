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
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud,  (SELECT ifnull(rtrim(ltrim(VALDES)), '') FROM DGACSYS.TXDGAC WHERE VALDDS = 'SOLTIP' AND VALVAL = SOLTIP) AS DescripcionTipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion, ");
                sbSol.Append(" ifnull(rtrim(ltrim(PLANOM)) || ' ' || rtrim(ltrim(PLANO1)), '') AS DescripcionActividadEjecutar, ifnull(rtrim(ltrim(SOLES9)), '')  AS EstadoVerificacionFinanciera,");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLO02)), '') AS ObservacionVerificacionPresupuesto1, ifnull(rtrim(ltrim(SOLO03)), '') AS ObservacionVerificacionPresupuesto2, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLU01)), '') as UsuarioCreacionFIN_PRES, ifnull(rtrim(ltrim(SOLUS9)), '') as  UsuarioCreacionPGE, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLUS6)), '') as UsuarioCreaAnalista, ifnull(rtrim(ltrim(SOLUS7)), '') as UsuarioDirectorAera, ifnull(rtrim(ltrim(SOLU01)), '') as UsuarioCreacionFIN_PRES,");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLES8)), '') AS EstadoActualizacionPOA");
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
                        solicitudPOA.DescripcionTipoSolicitud = dr["DescripcionTipoSolicitud"].ToString();
                        solicitudPOA.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        solicitudPOA.FechaRevision = dr["FechaRevision"].ToString();
                        solicitudPOA.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
                        solicitudPOA.FechaAprobacion = dr["FechaAprobacion"].ToString();
                        solicitudPOA.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        solicitudPOA.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        solicitudPOA.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        solicitudPOA.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        solicitudPOA.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        solicitudPOA.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        solicitudPOA.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        solicitudPOA.ObservacionVerificacionPresupuesto1 = dr["ObservacionVerificacionPresupuesto1"].ToString();
                        solicitudPOA.ObservacionVerificacionPresupuesto2 = dr["ObservacionVerificacionPresupuesto2"].ToString();
                        solicitudPOA.UsuarioCreacionFIN_PRES = dr["UsuarioCreacionFIN_PRES"].ToString();
                        solicitudPOA.UsuarioCreacionPGE = dr["UsuarioCreacionPGE"].ToString();
                        solicitudPOA.UsuarioCreaAnalista = campoNull(dr["UsuarioCreaAnalista"].ToString());
                        solicitudPOA.UsuarioDirectorAera = campoNull(dr["UsuarioDirectorAera"].ToString());
                        solicitudPOA.UsuarioCreacionFIN_PRES = campoNull(dr["UsuarioCreacionFIN_PRES"].ToString());
                        solicitudPOA.DescripcionActividadEjecutar = campoNull(dr["DescripcionActividadEjecutar"].ToString());
                        solicitudPOA.EstadoActualizacionPOA = campoNull(dr["EstadoActualizacionPOA"].ToString());

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
                + " join DIRARC ON SOLCO4 = DIRCO1 AND SOLCO5 = DIRCO3"
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
                    + " RTRIM(S.SOLUS6)|| '-' ||YEAR(DATE(TO_DATE(S.SOLFE6,'YYYY/MM/DD')))|| '-' ||INTEGER(L.LINNU1)  AS USUARIO, RTRIM(S.SOLUS6)  AS USUARIO1,"
                    + " (SELECT  VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = 'SOLTIP' AND VALVAL = SOLTIP) AS DescripcionTipoSolicitud"
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

        /// <summary>
        /// Metodo Actualización de POA
        /// </summary>
        /// <param name="cAnio"></param>
        /// <param name="numSolicitud"></param>
        /// <returns>DataSet</returns>
        public DataSet ActualizacionPOAPorAnioNumeroSolicitud(string cAnio, int numSolicitud)
        {
            DataSet dsSolicitud = new DataSet();
            string query = string.Empty;
            try
            {
                query = "SELECT (L.LINAN1 || '-' ||char(L.LINNU1)) AS NO_SOLICITUD,   CAST(L.LINNU1 AS INT) AS N_SOLICITUD, CAST(L.LINAN1 AS INT) AS ANIO_SOLICITUD, S.SOLFE6 AS FECHA_SOLICITUD,"
                    + " (SELECT  VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = 'SOLTIP' AND VALVAL = SOLTIP) AS DescripcionTipoSolicitud,  (trim(CHAR(A.ACTNUM)) || ' / ' ||trim(CHAR (A.ACTSEC))) AS CERTIFICACION_ACTUALIZACON_POA_NO,"
                    + " DATE(TO_DATE(S.SOLF01,'YYYY/MM/DD')) AS FECHAAPRBCERTIFICADO, D.DIRDES AS DIRECCION_SOLICITANTE,"
                    + " (L.LINACT) AS ACTIVIDAD_P, (L.LINACT || ' ' || AC.ACTDES) AS ACTIVIDAD_PRESUPUESTARIA,  (L.LINC43 || ' ' || P.PRODE1) AS PROGRAMA, L.LINC35 AS GEOGRAFICO, (L.LINC42 || ' ' || T.TIPDE2) AS TIPO_ADQUISICION,"
                    + " (L.LINAC1 || L.LINAC2 || L.LINAC3 || L.LINAC4 || L.LINAC5) AS ACTIVIDAD_POA,"
                    + " (L.LINC31 || L.LINC32 || L.LINC33 || L.LINC34) AS PARTIDA_PRESUPUESTARIA, L.LINMON AS MONTO_TOTAL_USD,"
                    + " RTRIM(S.SOLUS6)|| '-' ||YEAR(DATE(TO_DATE(S.SOLFE6,'YYYY/MM/DD')))|| '-' ||INTEGER(L.LINNU1) AS USUARIO1"
                    + " FROM LINARC L"
                    + " JOIN SOLAR1 AS S ON (S.SOLAN1=LINAN1 AND S.SOLNU3=L.LINNU1)"
                    + " JOIN ACTAR1 AS A ON (A.ACTAN2=L.LINAN1 AND A.ACTNU2=L.LINNU1)"
                    + " JOIN DIRARC AS D ON (D.DIRCO3=S.SOLCO5)"
                    + " JOIN ACTARC AS AC ON (AC.ACTCOD=L.LINACT)"
                    + " JOIN PROAR1 AS P ON (P.PROCO6=L.LINC43)"
                    + " JOIN TIPAR2 AS T ON (T.TIPCO1=L.LINC42)"
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

        public DataSet ElaborarInformeViabilidadModificacionPOAPropuesto(string cAnio, int numSolicitud)
        {
            DataSet dsSolicitud = new DataSet();
            string query = string.Empty;
            try
            {
                query = "SELECT M.MODAN2 AS Anio_Solicitud, M.MODNU3 AS Num_Solicitud, M.MODSEC AS Sec_Bloque, S.SOLNU6 AS Num_Modificacion, M.MODEST AS Origen_Destino, (M.MODC15 || '-' || U.UNIDES) AS EOD, "
                        + " (MODC16 || '-' || D.DIRDES) AS Direccion, (M.MODC21 || '-' || GESDES) AS Gestion_Interna, OPPPRO AS Provincia, OPCNO3 AS Canton, OPPNOM AS Ciudad, M.MODC03 AS Geografico, TIPDE3 AS Tipo_Gasto, "
                        + " PRODE2 AS Proyecto, (M.MODC10 || ' ' || P.PRODE1) AS Programa, M.MODC13 as Actividad_Presup, TAD.TIPDE2 AS Tipo_Adq, (M.MODDE2 || M.MODDE3 || M.MODDE4 || M.MODDE5 || M.MODDE6) AS Actividad_POA, "
                        + " (M.MODCO8 || M.MODCO9 || M.MODC01 || M.MODC02) AS Partida, (M.MODCO8 || M.MODCO9) AS Grupo_Gasto, M.MODACT AS PAC, "
                        + " CASE WHEN M.MODEST = 'ORIGEN' THEN(M.MODVAL - M.MODMO3) ELSE(M.MODVAL + M.MODMO3) END AS Valor_total, "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Enero FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '01'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Febrero FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '02'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Marzo FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '03'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Abril FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '04'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Mayo FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '05'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Junio FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '06'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Julio FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '07'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Agosto FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '08'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Septiembre FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '09'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Octubre FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '10'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Noviembre FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '11'), "
                        + " (SELECT CASE WHEN M.MODEST = 'ORIGEN' THEN(MM.MODCO5 - MM.MODMON) ELSE(MM.MODCO5 + MM.MODMON) END AS Diciembre FROM MODAR4 AS MM WHERE MM.MODOI2 = M.MODOI1 AND MM.MODME3 = '12'), "
                        + " M.MODC08 AS OEI, M.MODCE1 AS Valor_Certifcado, CASE WHEN M.MODEST = 'ORIGEN' THEN(M.MODVAL - M.MODMO3 - M.MODCE1) ELSE(M.MODVAL + M.MODMO3 - M.MODCE1) END AS Saldo_por_Certificar "
                        + "  FROM MODAR3 AS M LEFT JOIN SOLAR1 AS S ON S.SOLAN1 = M.MODAN2 AND S.SOLNU3 = M.MODNU3 "
                        + " LEFT JOIN DIRARC AS D ON M.MODC15 = D.DIRCO1  AND M.MODC16 = D.DIRCO3 "
                        + " LEFT JOIN PROAR1 AS P ON M.MODC10 = P.PROCO6 "
                        + " LEFT JOIN ITEARC AS I ON I.ITECO1 = M.MODCO8 AND I.ITECO2 = M.MODCO9 AND I.ITECO3 = M.MODC01 AND M.MODC02 = I.ITECOD "
                        + " LEFT JOIN UNIARC AS U ON U.UNICOD = M.MODC15 "
                        + " LEFT JOIN GESARC AS G ON G.GESCO1 = M.MODC16 AND G.GESCOD = M.MODC21 "
                        + " LEFT JOIN OPPAR2 AS PRV ON PRV.OPPCO3 = M.MODC04 AND PRV.OPPCO2 = M.MODC05 "
                        + " LEFT JOIN OPCAR4 AS CNT ON CNT.OPCC02 = M.MODC04 AND CNT.OPCC03 = M.MODC05 AND CNT.OPCC01 = M.MODC06 "
                        + " LEFT JOIN OPPAR4 AS PRQ ON PRQ.OPPCO7 = M.MODC04 AND PRQ.OPPCO8 = M.MODC05 AND PRQ.OPPCO9 = M.MODC06 AND PRQ.OPPCO6 = M.MODC07 "
                        + " LEFT JOIN TIPAR3 AS TG ON TG.TIPCO2 = M.MODCO7 "
                        + " LEFT JOIN PROAR2 AS PRY ON PRY.PROCO8 = M.MODC10 AND PRY.PROCO9 = M.MODC11 AND PRY.PROCO7 = M.MODC12 "
                        + " LEFT JOIN TIPAR2 AS TAD ON TAD.TIPCO1 = M.MODC09 "
                        + " WHERE M.MODAN2 = '" + cAnio + "' AND M.MODNU3 = " + numSolicitud
                        + " ORDER BY M.MODAN2, M.MODNU3, M.MODSEC, M.MODEST DESC";

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
                    //SOLES6 = 'NS'
                    //SOLES7 = 'RD'

                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sb.Append("UPDATE SOLAR1 SET ");
                    sb.Append(" SOLES7 = @estadoAprobado, SOLFE9 = @fecha, SOLUS8 = @codUsuario , SOLF04 = @fecha, SOLHO8 = @hora, SOLDI4 = '', ");
                    if (estadoAprobacion.Equals("RD"))
                    {
                        sb.Append(" SOLES6 = 'NS', ");
                    }
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

        #endregion

        #region "SIGPOA MODIFICACION POA"
        #region "PROCESO DE MODIFICACION POA SIN AFECTACION PRESUPUESTARIA"
        public List<tbSolicitudPOA> SolicitudModificacionSinAfectacionPresupetariaListarDireccionAnio(string cdireccion, string canio)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera,   ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ");
                sbSol.Append(" (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'DESTINO' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALDESTINO,  (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'ORIGEN' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALORIGEN");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE  SOLCO5 = '" + cdireccion + "'");
                if (canio.Trim().Length > 0)
                {
                    sbSol.Append(" AND SOLAN1 = '" + canio + "'");
                }

                sbSol.Append(" AND (SOLTIP = 'MDP') ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.ValOrigen = decimal.Parse(dr["VALORIGEN"].ToString());
                        oSolicitud.ValDestino = decimal.Parse(dr["VALDESTINO"].ToString());
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
        public List<tbSolicitudPOA> ModificacionPOAParaVerificacionEnPresupuesto()
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera, ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ifnull(rtrim(ltrim(SOLES8)), '') AS EstadoActualizacionPOA ");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE (SOLTIP = 'MOD' OR SOLTIP = 'MDP' OR SOLTIP = 'MAR') AND SOLES6 <> 'NS' ");
                sbSol.Append(" ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.EstadoActualizacionPOA = dr["EstadoActualizacionPOA"].ToString();
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
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

        public List<tbSolicitudPOA> ModificacionPOAParaVerificacionEnPresupuesto(string codSubsistema)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera, ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ifnull(rtrim(ltrim(SOLES8)), '') AS EstadoActualizacionPOA ");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE (SOLTIP = 'MOD' OR SOLTIP = 'MDP' OR SOLTIP = 'MAR') AND SOLES6 <> 'NS' AND SOLCO5 = '" + codSubsistema + "'");
                sbSol.Append(" ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.EstadoActualizacionPOA = dr["EstadoActualizacionPOA"].ToString();
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
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

        public bool EnviaSolictudVerificacionPresupuestaria(string canio, Int32 numSolicitud, string codUnidadEjecutiva, string codDireccionPYCE, string codUsuario)
        {
            bool estadoSolicitud = false;
            string CEstado = "";
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command("PA_MODPOA_ENVIASOLICITUDVERIFICAPRESUPUESTO", oConexion);
                    cmd.Parameters.AddWithValue("@CANIO", canio);
                    cmd.Parameters.AddWithValue("@NSOLIC", numSolicitud);
                    cmd.Parameters.AddWithValue("@CUNIDA", codUnidadEjecutiva);
                    cmd.Parameters.AddWithValue("@CDIREC", codDireccionPYCE);
                    cmd.Parameters.AddWithValue("@CESTA", CEstado).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@CUSUAR", campoNull(codUsuario));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    oConexion.Open();
                    estadoSolicitud = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    string cEstadoModif = cmd.Parameters[4].iDB2Value.ToString();
                    cmd.Dispose();
                    oConexion.Close();
                    if (cEstadoModif.Equals("1"))
                        estadoSolicitud = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estadoSolicitud;
        }
        /// <summary>
        /// Metodo regresa a estado anterior cuando ocurrio un error a sacar el reporte
        /// </summary>
        /// <returns></returns>
        public bool RegresaSolictudVerificacionPresupuestaria(string canio, Int32 numSolicitud, string estadoSolicitud)
        {
            bool estado = false;
            string CEstado = string.Empty;
            StringBuilder sb = new StringBuilder();
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {

                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sb.Append("UPDATE SOLAR1 SET ");
                    sb.Append(" SOLES6 = @estadoSolicitud, SOLES7 = '', SOLES9  = '', ");
                    sb.Append(" SOLUS7 = '', SOLFE6 = '', SOLFE8 = '', SOLHO7 = '', SOLDI3 =''");
                    sb.Append(" WHERE SOLAN1 = @canio AND SOLNU3 = @NumeroSolicitud");
                    queryUpdate = sb.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@estadoSolicitud"].Value = estadoSolicitud;
                    cmd.Parameters["@canio"].Value = canio;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estado;
        }
        /// <summary>
        /// Aprueba envia solicitud a DPGE
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="codUnidadEjecutiva"></param>
        /// <param name="codDireccionPYCE"></param>
        /// <param name="codUsuario"></param>
        /// <returns>True o False</returns>
        public bool AprobarEnviarSolicitudADPGE(string canio, Int32 numSolicitud, string codUnidadEjecutiva, string codDireccionPYCE, string codUsuario)
        {
            bool estadoSolicitud = false;
            string CEstado = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command("PA_MODPOA_APROBARENVIASOLICITUDADPGE", oConexion);
                    cmd.Parameters.AddWithValue("CANIO", canio);
                    cmd.Parameters.AddWithValue("NSOLIC", numSolicitud);
                    cmd.Parameters.AddWithValue("CUNIDA", codUnidadEjecutiva);
                    cmd.Parameters.AddWithValue("CDIREC", codDireccionPYCE);
                    cmd.Parameters.AddWithValue("CUSUAR", campoNull(codUsuario));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    oConexion.Open();
                    estadoSolicitud = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estadoSolicitud;
        }


        public bool ActualizaVerificacionPresupuestaria(tbSolicitudPOA osolicitud)
        {
            bool estadoSolicitud = false;
            string CEstado = string.Empty;
            StringBuilder sb = new StringBuilder();
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {

                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sb.Append("UPDATE SOLAR1 SET ");
                    sb.Append(" SOLES9 =  @estadoFinan, SOLO02 = @observacion1, SOLO03 = @observacion2, ");
                    if (osolicitud.EstadoVerificacionFinanciera.Equals("DP"))
                    {
                        sb.Append(" SOLES6 = 'NS', ");
                    }
                    else if (osolicitud.EstadoVerificacionFinanciera.Equals("DV"))
                    {
                        sb.Append(" SOLES7 = 'RA', ");
                    }

                    sb.Append(" SOLU01 = @usuarioMod, SOLF06 = @fecha, SOLH01 = @hora, SOLDI6 = ''");
                    sb.Append(" WHERE SOLAN1 = @canio AND SOLNU3 = @NumeroSolicitud");
                    queryUpdate = sb.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@estadoFinan"].Value = osolicitud.EstadoVerificacionFinanciera;
                    cmd.Parameters["@observacion1"].Value = campoNull(osolicitud.ObservacionVerificacionPresupuesto1).ToUpper();
                    cmd.Parameters["@observacion2"].Value = campoNull(osolicitud.ObservacionVerificacionPresupuesto2).ToUpper();
                    cmd.Parameters["@usuarioMod"].Value = osolicitud.UsuarioCreacionFIN_PRES;
                    cmd.Parameters["@fecha"].Value = osistema.FechaSistema;
                    cmd.Parameters["@hora"].Value = osistema.HoraSistema;
                    cmd.Parameters["@canio"].Value = osolicitud.AnioSolicitud;
                    cmd.Parameters["@NumeroSolicitud"].Value = osolicitud.NumeroSolicitud;
                    estadoSolicitud = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estadoSolicitud;
        }

        public List<tbSolicitudPOA> SolicitudesModificacionPOA()
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera,   ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLES8)), '') as EstadoActualizacionPOA ");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE SOLES6 = 'SO' AND (SOLTIP = 'MOD' OR SOLTIP = 'MAR' OR SOLTIP = 'MDP') ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.EstadoActualizacionPOA = dr["EstadoActualizacionPOA"].ToString();
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
        /// Metodo Revisar Solicitud y Dcocumentos habilitantes
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="cEstadoAut"></param>
        /// <param name="cObservacion1"></param>
        /// <param name="cObservacion2"></param>
        /// <param name="codUsuario"></param>
        /// <returns>True/False</returns>
        public bool RevisarApruebaSolicitudModificacionPOA(string canio, Int32 numSolicitud, string cEstadoAut, string cObservacion1, string cObservacion2, string codUsuario)
        {
            bool estadoSolicitud = false;
            string CEstado = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command("PA_REVISA_APRUEBA_SOLICITUD_MODIFICACON_POA_WEB", oConexion);
                    cmd.Parameters.AddWithValue("CANIO", canio);
                    cmd.Parameters.AddWithValue("NSOLIC", numSolicitud);
                    cmd.Parameters.AddWithValue("ESTAUT", cEstadoAut);
                    cmd.Parameters.AddWithValue("COBSE1", campoNull(cObservacion1));
                    cmd.Parameters.AddWithValue("COBSE2", campoNull(cObservacion2));
                    cmd.Parameters.AddWithValue("CUSER", campoNull(codUsuario));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    oConexion.Open();
                    estadoSolicitud = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estadoSolicitud;
        }

        /// <summary>
        /// Metodo actualiza estado de SOLES7 = "AP", Aprobar informe de viabilidad
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="cEstadoAut"></param>
        /// <param name="cObsAutorizacion1"></param>
        /// <param name="cObsAutorizacion2"></param>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public bool AprobarInformeViabilidadModificicacionPoa(string canio, Int32 numSolicitud, string cEstadoAut, string cObsAutorizacion1, string cObsAutorizacion2, string codUsuario)
        {
            bool estadoSolicitud = false;
            string CEstado = string.Empty;
            StringBuilder sb = new StringBuilder();
            string queryUpdate = string.Empty;
            tbSistema osistema = new tbSistema();
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sb.Append("UPDATE SOLAR1 SET ");
                    sb.Append(" SOLF01 =  @fecha, SOLES7 = @cEstadoAut, SOLOB9 = @cObsAutorizacion1, SOLO01 = @cObsAutorizacion2, ");
                    sb.Append(" SOLUS8 = @usuarioMod, SOLF04 = @fecha, SOLHO8 = @hora, SOLDI5 = ''");
                    sb.Append(" WHERE SOLAN1 = @canio AND SOLNU3 = @NumeroSolicitud");
                    queryUpdate = sb.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@cEstadoAut"].Value = cEstadoAut;
                    cmd.Parameters["@cObsAutorizacion1"].Value = campoNull(cObsAutorizacion1);
                    cmd.Parameters["@cObsAutorizacion2"].Value = campoNull(cObsAutorizacion2);
                    cmd.Parameters["@usuarioMod"].Value = campoNull(codUsuario);
                    cmd.Parameters["@fecha"].Value = osistema.FechaSistema;
                    cmd.Parameters["@hora"].Value = osistema.HoraSistema;
                    cmd.Parameters["@canio"].Value = canio;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    estadoSolicitud = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estadoSolicitud;
        }


        /// <summary>
        /// Metodo Actaixar POA y emitir Modificacion Aprobada
        /// </summary>
        /// <param name="canio"></param>
        /// <param name="numSolicitud"></param>
        /// <param name="numCur"></param>
        /// <param name="cFechaCur"></param>
        /// <param name="cObservacion1"></param>
        /// <param name="cObservacion2"></param>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public bool ActualizaPOA(string canio, Int32 numSolicitud, Int32 numCur, string cFechaCur, string cObservacion1, string cObservacion2, string codUsuario)
        {
            bool estadoSolicitud = false;
            string CEstado = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    //cFechaCur = DateTime.Parse(cFechaCur).ToString("yyyyMMdd");
                    cmd = new iDB2Command("PA_ACTUALIZACION_POA_WEB", oConexion);
                    cmd.Parameters.AddWithValue("CANIO", canio);
                    cmd.Parameters.AddWithValue("NSOLIC", numSolicitud);
                    cmd.Parameters.AddWithValue("NUMCUR", numCur);
                    cmd.Parameters.AddWithValue("FECCUR", validaFechaAs400(cFechaCur));
                    cmd.Parameters.AddWithValue("COBSE1", campoNull(cObservacion1));
                    cmd.Parameters.AddWithValue("COBSE2", campoNull(cObservacion2));
                    cmd.Parameters.AddWithValue("CUSER", campoNull(codUsuario));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    oConexion.Open();
                    estadoSolicitud = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return estadoSolicitud;
        }

        #endregion
        #region "PROCESO DE MODIFICACION PAO CON AFECTACION PRESUPUESTARIA"
        public List<tbSolicitudPOA> SolicitudModificacionPoaConAfectacionPresupetariaListarDireccionAnio(string cdireccion, string canio)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera,   ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ");
                sbSol.Append(" (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'DESTINO' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALDESTINO,  (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'ORIGEN' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALORIGEN");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE  SOLCO5 = '" + cdireccion + "'");
                if (canio.Trim().Length > 0)
                {
                    sbSol.Append(" AND SOLAN1 = '" + canio + "'");
                }

                sbSol.Append(" AND (SOLTIP = 'MOD') ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.ValOrigen = decimal.Parse(dr["VALORIGEN"].ToString());
                        oSolicitud.ValDestino = decimal.Parse(dr["VALDESTINO"].ToString());
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
        #endregion

        #region "PROCESO MODIFICACION POA ASIGNACION DE RECURSOS"

        /// <summary>
        /// Metodo carga los datos de la Asignación de Recursos
        /// </summary>
        /// <param name="cdireccion"></param>
        /// <param name="canio"></param>
        /// <returns></returns>
        public List<tbSolicitudPOA> SolicitudModificacionPoaConAsignacionRecursosListarDireccionAnio(string cdireccion, string canio)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera,   ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ");
                sbSol.Append(" (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'DESTINO' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALDESTINO,  (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'ORIGEN' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALORIGEN");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE  SOLCO5 = '" + cdireccion + "'");
                if (canio.Trim().Length > 0)
                {
                    sbSol.Append(" AND SOLAN1 = '" + canio + "'");
                }

                sbSol.Append(" AND (SOLTIP = 'MAR') ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.ValOrigen = decimal.Parse(dr["VALORIGEN"].ToString());
                        oSolicitud.ValDestino = decimal.Parse(dr["VALDESTINO"].ToString());
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
        /// Metodo carga los datos de la Asignación de Recursos
        /// </summary>
        /// <param name="cdireccion"></param>
        /// <param name="canio"></param>
        /// <returns></returns>
        public List<tbSolicitudPOA> SolicitudModificacionPoaConAsignacionRecursosListar(string cdireccion, string canio)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion, ifnull(rtrim(ltrim(SOLCO4)), '') as CodigoUnidadEjecucion ,  ifnull(rtrim(ltrim(SOLCO5)), '') as CodigoDireccionPYGE, ");
                sbSol.Append(" ifnull(SOLSEC, 0) as SecuenciaActividad, ifnull(PLANU2, 0) AS NumeroCertificadoPOA , ifnull(ACTSEC, 0) AS SecuencialActualizacion,  case rtrim(ltrim(SOLES9)) when '' then '.' else rtrim(ltrim(SOLES9)) end AS EstadoVerificacionFinanciera,   ifnull(rtrim(ltrim(SOLF06)), '')  as FechaCreacionFIN_PRES, ");
                sbSol.Append(" (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'DESTINO' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALDESTINO,  (SELECT  ifnull(SUM(LINMO8), 0) FROM LINAR2 WHERE LINES4 = 'ORIGEN' AND LINAN6 = SOLAN1 AND LINNU7 = SOLNU3) AS VALORIGEN");
                sbSol.Append(" FROM SOLAR1 left join ACTAR1 on (SOLAN1 = ACTAN2 and SOLNU3 = ACTNU2) LEFT JOIN PLAARC ON(SOLCO4 = PLACO2 AND SOLCO5 = PLACO3 AND SOLAN1 = PLAANI AND  SOLSEC = PLANUM)");
                sbSol.Append(" WHERE  SOLCO5 = '" + cdireccion + "'");
                if (canio.Trim().Length > 0)
                {
                    sbSol.Append(" AND SOLAN1 = '" + canio + "'");
                }

                sbSol.Append(" AND (SOLTIP = 'MAR') ORDER BY SOLAN1, SOLNU3 DESC");
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
                        oSolicitud.EstadoVerificacionFinanciera = campoNull(dr["EstadoVerificacionFinanciera"].ToString());
                        oSolicitud.FechaCreacionFIN_PRES = dr["FechaCreacionFIN_PRES"].ToString();
                        oSolicitud.NumeroModificacion = Int32.Parse(dr["NumeroModificacion"].ToString());
                        oSolicitud.CodigoUnidadEjecucion = dr["CodigoUnidadEjecucion"].ToString();
                        oSolicitud.CodigoDireccionPYGE = dr["CodigoDireccionPYGE"].ToString();
                        oSolicitud.SecuenciaActividad = Int32.Parse(dr["SecuenciaActividad"].ToString());
                        oSolicitud.NumeroCertificadoPOA = Int32.Parse(dr["NumeroCertificadoPOA"].ToString());
                        oSolicitud.SecuencialActualizacion = Int32.Parse(dr["SecuencialActualizacion"].ToString());
                        oSolicitud.ValOrigen = decimal.Parse(dr["VALORIGEN"].ToString());
                        oSolicitud.ValDestino = decimal.Parse(dr["VALDESTINO"].ToString());
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


        #endregion

        #endregion
        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

        private string validaFechaAs400(string fecha)
        {
            string fechaibm = string.Empty;
            try
            {
                fechaibm = DateTime.Parse(fecha).ToString("yyyyMMdd");
            }
            catch
            {
                fechaibm = "";
            }
            return fechaibm;
        }

    }
}
