using CapaModelo;
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
    public class CD_PlanIncentivo
    {
        public static CD_PlanIncentivo _instancia = null;
        private CD_PlanIncentivo()
        {

        }

        public static CD_PlanIncentivo Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_PlanIncentivo();
                }
                return _instancia;
            }
        }



        public List<tbPlanIncentivo> ConsultaPlanIncentivo()
        {

            List<tbPlanIncentivo> listarSolicitud = new List<tbPlanIncentivo>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPIOID,OPIOI1,	OPICOM,	OPICON,	OPIPER,	OPITI1,	OPINUM,	OPIVUE,	OPIFRE,	OPIES2,	OPIFE2,	OPIFE3,	" +
                    "OPICO2,	OPIFE4,	OPICAN,	OPINO3,	OPIUS4,	OPIDA4,	OPIHO4, OPIUS5, OPIDA5, OPIHO5,OPIMIG   FROM OPIAR3");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbPlanIncentivo oSolicitud = new tbPlanIncentivo();

                        oSolicitud.OID = Convert.ToInt32(dr["OPIOID"].ToString());
                        oSolicitud.OIDCOMPAÑIA = Convert.ToInt32(dr["OPIOI1"].ToString());
                        oSolicitud.COMPAÑIAOACI = dr["OPICOM"].ToString().Trim();
                        oSolicitud.CONTRATO = dr["OPICON"].ToString().Trim();
                        oSolicitud.PERMISO = dr["OPIPER"].ToString().Trim();
                        oSolicitud.TIPOVUELO = dr["OPITI1"].ToString().Trim();
                        oSolicitud.NUMEROVUELO = dr["OPINUM"].ToString().Trim();
                        oSolicitud.VUELO = dr["OPIVUE"].ToString().Trim();
                        oSolicitud.FRECUENCIA = dr["OPIFRE"].ToString().Trim();
                        oSolicitud.ESTADO = dr["OPIES2"].ToString().Trim();
                        oSolicitud.FECHAINICIO = dr["OPIFE2"].ToString().Trim();
                        oSolicitud.FECHAFIN = dr["OPIFE3"].ToString().Trim();
                        oSolicitud.MIGRADO = dr["OPIMIG"].ToString().Trim();


                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }


        public tbPlanIncentivo DetallePlanIncentivo(Int32 Oid, string NumeroVuelo)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbPlanIncentivo listarSolicitud = new tbPlanIncentivo();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPIAR3 WHERE OPIOID = " + Oid + " AND OPINUM='" + NumeroVuelo + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbPlanIncentivo oSolicitud = new tbPlanIncentivo();

                        oSolicitud.OID = Convert.ToInt32(dr["OPIOID"].ToString());
                        oSolicitud.OIDCOMPAÑIA = Convert.ToInt32(dr["OPIOI1"].ToString());
                        oSolicitud.COMPAÑIAOACI = dr["OPICOM"].ToString().Trim();
                        oSolicitud.CONTRATO = dr["OPICON"].ToString().Trim();
                        oSolicitud.PERMISO = dr["OPIPER"].ToString().Trim();
                        oSolicitud.TIPOVUELO = dr["OPITI1"].ToString().Trim();
                        oSolicitud.NUMEROVUELO = dr["OPINUM"].ToString().Trim();
                        oSolicitud.VUELO = dr["OPIVUE"].ToString().Trim();
                        oSolicitud.FRECUENCIA = dr["OPIFRE"].ToString().Trim();
                        oSolicitud.ESTADO = dr["OPIES2"].ToString().Trim();
                        oSolicitud.FECHAINICIO = dr["OPIFE2"].ToString().Trim();
                        oSolicitud.FECHAINICIO = oSolicitud.FECHAINICIO.Insert(4, "-").Insert(7, "-");

                        oSolicitud.FECHAFIN = dr["OPIFE3"].ToString().Trim();
                        oSolicitud.FECHAFIN = oSolicitud.FECHAFIN.Insert(4, "-").Insert(7, "-");

                        oSolicitud.CONTRATOTXT = dr["OPICO2"].ToString().Trim();
                        oSolicitud.FECHACANCELA = dr["OPIFE4"].ToString().Trim();
                        if (oSolicitud.FECHACANCELA != "")
                        {
                            oSolicitud.FECHACANCELA = oSolicitud.FECHACANCELA.Insert(4, "-").Insert(7, "-");
                        }


                        oSolicitud.CANCELADAPOR = dr["OPICAN"].ToString().Trim();
                        oSolicitud.NOMBRECANCELA = dr["OPINO3"].ToString().Trim();
                        oSolicitud.USERCR = dr["OPIUS4"].ToString().Trim();
                        oSolicitud.DATECR = dr["OPIDA4"].ToString().Trim();
                        oSolicitud.HORACR = dr["OPIHO4"].ToString().Trim();
                        oSolicitud.USERMD = dr["OPIUS5"].ToString().Trim();
                        oSolicitud.DATEMD = dr["OPIDA5"].ToString().Trim();
                        oSolicitud.HORAMD = dr["OPIHO5"].ToString().Trim();

                        string estado = dr["OPIES2"].ToString();
                        switch (estado)
                        {
                            case "A":
                                oSolicitud.ESTADO = "ACTIVO";
                                break;

                            case "N":
                                oSolicitud.ESTADO = "NO ACTIVO";
                                break;

                            default:
                                break;
                        }

                        oSolicitud.MIGRADO = dr["OPIMIG"].ToString().Trim();

                        listarSolicitud = oSolicitud;
                    }

                    dr.Close();
                    oConexion.Close();

                }

                return listarSolicitud;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

        public List<tbPlanIncentivo> DetalleDocumentosporVuelo(string Vuelo)
        {
            List<tbPlanIncentivo> listarSolicitud = new List<tbPlanIncentivo>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPIOID,OPIOI1,	OPICOM,	OPICON,	OPIPER,	OPITI1,	OPINUM,	OPIVUE,	OPIFRE,	OPIES2,	OPIFE2,	OPIFE3,	" +
                    "OPICO2,	OPIFE4,	OPICAN,	OPINO3,	OPIUS4,	OPIDA4,	OPIHO4, OPIUS5, OPIDA5, OPIHO5,OPIMIG   FROM OPIAR3 where OPINUM='" + Vuelo + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbPlanIncentivo oSolicitud = new tbPlanIncentivo();

                        oSolicitud.OID = Convert.ToInt32(dr["OPIOID"].ToString());
                        oSolicitud.OIDCOMPAÑIA = Convert.ToInt32(dr["OPIOI1"].ToString());
                        oSolicitud.COMPAÑIAOACI = dr["OPICOM"].ToString().Trim();
                        oSolicitud.CONTRATO = dr["OPICON"].ToString().Trim();
                        oSolicitud.PERMISO = dr["OPIPER"].ToString().Trim();
                        oSolicitud.TIPOVUELO = dr["OPITI1"].ToString().Trim();
                        oSolicitud.NUMEROVUELO = dr["OPINUM"].ToString().Trim();
                        oSolicitud.VUELO = dr["OPIVUE"].ToString().Trim();
                        oSolicitud.FRECUENCIA = dr["OPIFRE"].ToString().Trim();
                        oSolicitud.ESTADO = dr["OPIES2"].ToString().Trim();
                        oSolicitud.FECHAINICIO = dr["OPIFE2"].ToString().Trim();
                        oSolicitud.FECHAFIN = dr["OPIFE3"].ToString().Trim();
                        oSolicitud.MIGRADO = dr["OPIMIG"].ToString().Trim();

                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;

        }



        public Int32 SecuencialPlanIncentivo()
        {
            string query = "SELECT IFNULL(max(OPIOID), 0) + 1 AS Secuencial FROM OPIAR3";
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
            catch (Exception)
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

        public bool PlanIncentivoNuevo(tbPlanIncentivo oPlan)
        {

            oPlan.FECHAINICIO = oPlan.FECHAINICIO.Replace("-", "");
            oPlan.FECHAFIN = oPlan.FECHAFIN.Replace("-", "");
            if (oPlan.FECHACANCELA != null)
            {
                oPlan.FECHACANCELA = oPlan.FECHACANCELA.Replace("-", "");
            }

            bool respuesta = false;
            iDB2Command cmd;
            string query = "INSERT INTO OPIAR3 (OPIOID, OPIOI1, OPICOM, OPICON, OPIPER, OPITI1, OPINUM, OPIVUE, OPIFRE, OPIES2, OPIFE2, OPIFE3, OPICO2, OPIFE4, OPICAN, OPINO3, OPIUS4, OPIDA4, OPIHO4, OPIUS5, OPIDA5, OPIHO5) " +
                           "VALUES (@OID, @OIDCOMPANIA, @COMPANIAOACI, @CONTRATO, @PERMISO, @TIPOVUELO, @NUMEROVUELO, @VUELO, @FRECUENCIA, @ESTADO, @FECHAINICIO, @FECHAFIN, @CONTRATOTXT, @FECHACANCELA, @CANCELADAPOR, @NOMBRECANCELA, @USERCR, @DATECR, @HORACR, @USERMD, @DATEMD, @HORAMD)";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@OID"].Value = SecuencialPlanIncentivo();
                    cmd.Parameters["@OIDCOMPANIA"].Value = oPlan.OIDCOMPAÑIA;
                    cmd.Parameters["@COMPANIAOACI"].Value = campoNull(oPlan.COMPAÑIAOACI?.ToUpper());
                    cmd.Parameters["@CONTRATO"].Value = campoNull(oPlan.CONTRATO?.ToUpper());
                    cmd.Parameters["@PERMISO"].Value = campoNull(oPlan.PERMISO?.ToUpper());
                    cmd.Parameters["@TIPOVUELO"].Value = campoNull(oPlan.TIPOVUELO);
                    cmd.Parameters["@NUMEROVUELO"].Value = campoNull(oPlan.NUMEROVUELO?.ToUpper());
                    cmd.Parameters["@VUELO"].Value = campoNull(oPlan.VUELO);
                    cmd.Parameters["@FRECUENCIA"].Value = campoNull(oPlan.FRECUENCIA);
                    cmd.Parameters["@ESTADO"].Value = string.IsNullOrEmpty(oPlan.ESTADO) ? "A" : oPlan.ESTADO;
                    cmd.Parameters["@FECHAINICIO"].Value = campoNull(oPlan.FECHAINICIO);
                    cmd.Parameters["@FECHAFIN"].Value = campoNull(oPlan.FECHAFIN);
                    cmd.Parameters["@CONTRATOTXT"].Value = campoNull(oPlan.CONTRATOTXT?.ToUpper());
                    cmd.Parameters["@FECHACANCELA"].Value = campoNull(oPlan.FECHACANCELA);
                    cmd.Parameters["@CANCELADAPOR"].Value = campoNull(oPlan.CANCELADAPOR?.ToUpper());
                    cmd.Parameters["@NOMBRECANCELA"].Value = campoNull(oPlan.NOMBRECANCELA?.ToUpper());
                    cmd.Parameters["@USERCR"].Value = campoNull(oPlan.USERCR?.ToUpper());
                    cmd.Parameters["@DATECR"].Value = campoNull(oPlan.DATECR);
                    cmd.Parameters["@HORACR"].Value = campoNull(oPlan.HORACR);
                    cmd.Parameters["@USERMD"].Value = campoNull(oPlan.USERMD?.ToUpper());
                    cmd.Parameters["@DATEMD"].Value = campoNull(oPlan.DATEMD);
                    cmd.Parameters["@HORAMD"].Value = campoNull(oPlan.HORAMD);

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool PlanIncentivoActualizar(tbPlanIncentivo oPlan)
        {
            oPlan.FECHAINICIO = oPlan.FECHAINICIO.Replace("-", "");
            oPlan.FECHAFIN = oPlan.FECHAFIN.Replace("-", "");
            if (oPlan.FECHACANCELA != null)
            {
                oPlan.FECHACANCELA = oPlan.FECHACANCELA.Replace("-", "");
                oPlan.ESTADO = "N";
            }


            bool respuesta = false;
            iDB2Command cmd;
            string query = "UPDATE OPIAR3 SET OPIOI1=@OIDCOMPANIA, OPICOM=@COMPANIAOACI, OPICON=@CONTRATO, OPIPER=@PERMISO, OPITI1=@TIPOVUELO, OPINUM=@NUMEROVUELO, " +
                "OPIVUE=@VUELO, OPIFRE=@FRECUENCIA, OPIES2=@ESTADO, OPIFE2=@FECHAINICIO, OPIFE3=@FECHAFIN, OPICO2=@CONTRATOTXT, OPIFE4=@FECHACANCELA, " +
                "OPICAN=@CANCELADAPOR, OPINO3=@NOMBRECANCELA, OPIUS5=@USERMD, OPIDA5=@DATEMD, OPIHO5=@HORAMD WHERE OPIOID=@OID";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@OIDCOMPANIA"].Value = oPlan.OIDCOMPAÑIA;
                    cmd.Parameters["@COMPANIAOACI"].Value = campoNull(oPlan.COMPAÑIAOACI.ToUpper());
                    cmd.Parameters["@CONTRATO"].Value = campoNull(oPlan.CONTRATO.ToUpper());
                    cmd.Parameters["@PERMISO"].Value = campoNull(oPlan.PERMISO.ToUpper());
                    cmd.Parameters["@TIPOVUELO"].Value = campoNull(oPlan.TIPOVUELO);
                    cmd.Parameters["@NUMEROVUELO"].Value = campoNull(oPlan.NUMEROVUELO.ToUpper());
                    cmd.Parameters["@VUELO"].Value = campoNull(oPlan.VUELO);
                    cmd.Parameters["@FRECUENCIA"].Value = campoNull(oPlan.FRECUENCIA);
                    cmd.Parameters["@ESTADO"].Value = string.IsNullOrEmpty(oPlan.ESTADO) ? "A" : oPlan.ESTADO;
                    cmd.Parameters["@FECHAINICIO"].Value = campoNull(oPlan.FECHAINICIO);
                    cmd.Parameters["@FECHAFIN"].Value = campoNull(oPlan.FECHAFIN);
                    cmd.Parameters["@CONTRATOTXT"].Value = campoNull(oPlan.CONTRATOTXT.ToUpper());
                    cmd.Parameters["@FECHACANCELA"].Value = campoNull(oPlan.FECHACANCELA);
                    cmd.Parameters["@CANCELADAPOR"].Value = campoNull(oPlan.CANCELADAPOR?.ToUpper());
                    cmd.Parameters["@NOMBRECANCELA"].Value = campoNull(oPlan.NOMBRECANCELA);
                    cmd.Parameters["@USERMD"].Value = campoNull(oPlan.USERMD.Trim());
                    cmd.Parameters["@DATEMD"].Value = campoNull(oPlan.DATEMD.Trim());
                    cmd.Parameters["@HORAMD"].Value = campoNull(oPlan.HORAMD.Trim());
                    cmd.Parameters["@OID"].Value = oPlan.OID;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception EX)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ProcesarPlanIncentivo(tbPlanIncentivo oPlan)
        {
            bool respuesta = false;
            if (oPlan.OID > 0)
            {

                tbPlanIncentivo listarSolicitud = new tbPlanIncentivo();
                StringBuilder sbSol = new StringBuilder();
                string query = string.Empty;

                int mesesARestar = 1;

                // 1. Obtenemos la fecha actual del servidor
                DateTime fechaActual = DateTime.Today;

                // 2. Restamos los meses correspondientes
                // C# maneja automáticamente el cambio de año (ej: Enero menos 1 mes se convierte en Diciembre del año anterior)
                DateTime fechaObjetivo = fechaActual.AddMonths(-mesesARestar);

                // 3. FECHA INICIO: Primer día del mes objetivo
                DateTime fechaInicio = new DateTime(fechaObjetivo.Year, fechaObjetivo.Month, 1);
                fechaInicio = fechaInicio.AddDays(1);
                string fechaInicioTexto = fechaInicio.ToString("yyyyMMdd");
                // 4. FECHA FIN: Sumamos 1 mes a la fecha de inicio y le restamos 1 día
                // Esto calcula de forma exacta el último día (controla bisiestos, meses de 30 o 31 días)
                DateTime fechaFin = fechaInicio.AddMonths(1).AddDays(-1);
                //fechaFin = fechaFin.AddDays(1);
                string fechaFinTexto = fechaFin.ToString("yyyyMMdd");


                try
                {


                    iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
                    con.Open();

                    iDB2Command cm = new iDB2Command();
                    cm.Connection = con;

                    string cadenasp = "PA_CALCULOINCENTIVO";

                    string Est = "0";

                    cm.CommandText = cadenasp;
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@PR_FECHAINICIO",fechaInicioTexto);
                    cm.Parameters.AddWithValue("@PR_FECHAFIN", fechaFinTexto);
                    cm.Parameters.AddWithValue("@PR_VUELO", oPlan.NUMEROVUELO.Trim());
                    cm.Parameters.AddWithValue("@PR_OID", Convert.ToString(oPlan.OID).Trim());
                    cm.Parameters.AddWithValue("@PR_EST", oPlan.ESTADO.Trim());
                    cm.Parameters.AddWithValue("@PR_ESTADO", Est).Direction = ParameterDirection.Output;

                    cm.CommandTimeout = 0;

                    iDB2DataReader dr = cm.ExecuteReader();

                    tbPlanIncentivo oSolicitud = new tbPlanIncentivo();

                    string ESTADO = cm.Parameters[3].iDB2Value.ToString();
                    oSolicitud.MIGRADO = ESTADO;

                    listarSolicitud = oSolicitud;

                    con.Close();

                    //ACTUALIZA EL CODIGO DE MIGRACION

                    ActualizaEstado(oPlan.OID, oPlan.COMPAÑIAOACI, oPlan.NUMEROVUELO);

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    //   throw ex;
                }
               
            }
            return respuesta;
        }
        //actualiza estado
        public static void ActualizaEstado(int Oid,  string Compania, string NumeroVuelo)
        {

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;
            try
            {
                string cadena = ("UPDATE OPIAR3 SET OPIMIG ='T' WHERE OPIOID=" + Oid + "");
                //string cadena = ("UPDATE OPIAR3 SET OPIMIG ='T' WHERE OPIOID=" + Oid + " AND OPICOM='" + Compania.Trim() + "' AND OPINUM='" + NumeroVuelo.Trim() + "'");
                cm.CommandText = cadena;
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine("error.:" + ex);
            }


            con.Close();
        }

    }
}
