using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Garantias
    {
        public static CD_Garantias _instancia = null;
        private CD_Garantias()
        {

        }

        public static CD_Garantias Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Garantias();
                }
                return _instancia;
            }
        }

        public List<tbGarantias> DetalleGarantias()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbGarantias> listarSolicitud = new List<tbGarantias>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FGARUC as  RUC,  CONCAT(FGACOM, FGACO3) as COMPANIA_CONTRATISTA,FGANUM as NUMERO_GARANTIA,FGAFEC as FECHA_EXPEDICION," +
                    "FGAADM as ADMINISTRADOR_CONTRATO,case when FGATIP = '1' then 'GARANTIAS POR CONVENIOS DE PAGO'   when FGATIP = '2' then 'REPONSABILIDAD CIVIL'" +
                    "  when FGATIP = '3' then 'GARANTÍAS TÉCNICAS'     when FGATIP = '4' then 'CONTRATOS ARRIENDOS'     when FGATIP = '5' then 'CONTRATOS Y SERVICIOS' " +
                    "END AS  TIPO_GARANTIA,CONCAT(FGACON, FGACO2) as CONCEPTO_GARANTIA,FGARU1 as RUC_CIA_SEGUROS,FCONOM as RAZON_SOCIAL_ASEGURADORA,FGAVIG as VIGENCIA_DESDE," +
                    "FGAVI1 as VIGENCIA_HASTA,FGADIA as DIAS_A_CADUCAR,case when FGASEG = '1' then 'FIEL CUMPLIMIENTO DE CONTRATO'" +
                    "     when FGASEG = '2' then 'BUEN USO DE ANTICIPO'     when FGASEG = '3' then 'RESPONSABILIDAD CIVIL'" +
                    "     when FGASEG = '4' then 'RESPONSABILIDAD CIVIL SEGURIDAD' END AS  SEGURO_DE, case when FGAEST = '1' then 'VIGENTE'" +
                    "     when FGAEST = '2' then 'VENCIDA'     when FGAEST = '3' then 'EJECUTADA'     when FGAEST = '4' then 'FINALIZADA'END AS ESTADO," +
                    "FGAVAL  AS VALOR_GARANTIA,CONCAT(FGAOBS, CONCAT(FGAOB1, CONCAT(FGAOB2, FGAOB3))) AS OBSERVACIONES " +
                    "FROM DGACDATPRO.FGAARC INNER JOIN DGACDATPRO.FCOARC ON FGARU1=FCORUC");
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
                        tbGarantias oSolicitud = new tbGarantias();
                        oSolicitud.Ruc = dr["RUC"].ToString();
                        oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();
                        
                        oSolicitud.NumeroGarantia = dr["NUMERO_GARANTIA"].ToString();
                        oSolicitud.Fecha_Expedicion = dr["FECHA_EXPEDICION"].ToString();
                        oSolicitud.Administrador_Contrato = dr["ADMINISTRADOR_CONTRATO"].ToString();
                        oSolicitud.Tipo_Garantia = dr["TIPO_GARANTIA"].ToString();
                        oSolicitud.Concepto_Garantia = dr["CONCEPTO_GARANTIA"].ToString();

                        oSolicitud.Ruc_Cia_Seguros = dr["RUC_CIA_SEGUROS"].ToString();
                        oSolicitud.Razon_Social_Aseguradora = dr["RAZON_SOCIAL_ASEGURADORA"].ToString();
                        oSolicitud.Vigencia_Desde = dr["VIGENCIA_DESDE"].ToString();
                        oSolicitud.Vigencia_Hasta = dr["VIGENCIA_HASTA"].ToString();
                        oSolicitud.Dias_a_Caducar = dr["DIAS_A_CADUCAR"].ToString();

                        oSolicitud.Seguro_de = dr["SEGURO_DE"].ToString();
                        oSolicitud.Estado = dr["ESTADO"].ToString();
                        oSolicitud.Valor_Garantia = decimal.Parse(dr["VALOR_GARANTIA"].ToString());
                        oSolicitud.Observaciones = dr["OBSERVACIONES"].ToString();
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

        public tbGarantias DetalleGarantiaRuc(string Ruc)//(string canio, string cdireccion, string tipoSolicitud)
        {
            tbGarantias oSolicitud = new tbGarantias();
            // List<tbGarantias> listarSolicitud = new List<tbGarantias>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FGARUC as  RUC,  CONCAT(FGACOM, FGACO3) as COMPANIA_CONTRATISTA,FGANUM as NUMERO_GARANTIA,FGAFEC as FECHA_EXPEDICION," +
                    "FGAADM as ADMINISTRADOR_CONTRATO,case when FGATIP = '1' then 'GARANTIAS POR CONVENIOS DE PAGO'   when FGATIP = '2' then 'REPONSABILIDAD CIVIL'" +
                    "  when FGATIP = '3' then 'GARANTÍAS TÉCNICAS'     when FGATIP = '4' then 'CONTRATOS ARRIENDOS'     when FGATIP = '5' then 'CONTRATOS Y SERVICIOS' " +
                    "END AS  TIPO_GARANTIA,CONCAT(FGACON, FGACO2) as CONCEPTO_GARANTIA,FGARU1 as RUC_CIA_SEGUROS,FCONOM as RAZON_SOCIAL_ASEGURADORA,FGAVIG as VIGENCIA_DESDE," +
                    "FGAVI1 as VIGENCIA_HASTA,FGADIA as DIAS_A_CADUCAR,case when FGASEG = '1' then 'FIEL CUMPLIMIENTO DE CONTRATO'" +
                    "     when FGASEG = '2' then 'BUEN USO DE ANTICIPO'     when FGASEG = '3' then 'RESPONSABILIDAD CIVIL'" +
                    "     when FGASEG = '4' then 'RESPONSABILIDAD CIVIL SEGURIDAD' END AS  SEGURO_DE, case when FGAEST = '1' then 'VIGENTE'" +
                    "     when FGAEST = '2' then 'VENCIDA'     when FGAEST = '3' then 'EJECUTADA'     when FGAEST = '4' then 'FINALIZADA'END AS ESTADO," +
                    "FGAVAL  AS VALOR_GARANTIA,CONCAT(FGAOBS, CONCAT(FGAOB1, CONCAT(FGAOB2, FGAOB3))) AS OBSERVACIONES " +
                    "FROM DGACDATPRO.FGAARC INNER JOIN DGACDATPRO.FCOARC ON FGARU1=FCORUC where FGARUC='"+Ruc+"'");
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
                     //   tbGarantias oSolicitud = new tbGarantias();
                        oSolicitud.Ruc = dr["RUC"].ToString();
                        oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();

                        oSolicitud.NumeroGarantia = dr["NUMERO_GARANTIA"].ToString();
                        oSolicitud.Fecha_Expedicion = dr["FECHA_EXPEDICION"].ToString();
                        oSolicitud.Administrador_Contrato = dr["ADMINISTRADOR_CONTRATO"].ToString();
                        oSolicitud.Tipo_Garantia = dr["TIPO_GARANTIA"].ToString();
                        oSolicitud.Concepto_Garantia = dr["CONCEPTO_GARANTIA"].ToString();

                        oSolicitud.Ruc_Cia_Seguros = dr["RUC_CIA_SEGUROS"].ToString();
                        oSolicitud.Razon_Social_Aseguradora = dr["RAZON_SOCIAL_ASEGURADORA"].ToString();
                        oSolicitud.Vigencia_Desde = dr["VIGENCIA_DESDE"].ToString();
                        oSolicitud.Vigencia_Hasta = dr["VIGENCIA_HASTA"].ToString();
                        oSolicitud.Dias_a_Caducar = dr["DIAS_A_CADUCAR"].ToString();

                        oSolicitud.Seguro_de = dr["SEGURO_DE"].ToString();
                        oSolicitud.Estado = dr["ESTADO"].ToString();
                        oSolicitud.Valor_Garantia = decimal.Parse(dr["VALOR_GARANTIA"].ToString());
                        oSolicitud.Observaciones = dr["OBSERVACIONES"].ToString();
                       // listarSolicitud.Add(oSolicitud);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oSolicitud;
        }
    }
}
