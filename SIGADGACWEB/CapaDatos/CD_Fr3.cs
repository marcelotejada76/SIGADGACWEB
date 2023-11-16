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
    public class CD_Fr3
    {
        public static CD_Fr3 _instancia = null;
        private CD_Fr3()
        {

        }

        public static CD_Fr3 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Fr3();
                }
                return _instancia;
            }
        }

        public tbFr3 DetalleFr3(string Ato, string Matricula, string Origen, string Destino, string Destino1, string Destino2, string FechaI, string FechaF, string HoraI, string HoraF)
        {
            tbFr3 listarSolicitud = new tbFr3();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            try
            {


                iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
                con.Open();

                iDB2Command cm = new iDB2Command();
                cm.Connection = con;

                string cadena = "PA_CALCULOFR3";


                //String Aeropuerto = "SEMT";
                //String FechaInicial= "20231011";
                //String FechaFinal = "20231011";
                //String HoraInicial = "12:00:00";
                //String HoraFinal = "13:00:00";
                //String Matricula = "AC080";
                //String Origen = "UGUP";
                //String Destino = "SEQM";
                //String Destino1 = "";
                //String Destino2 = "";
                decimal v1 = 1;
                decimal v2 = 1;
                decimal v3 = 1;
                decimal v4 = 1;
                decimal v5 = 1;
                decimal v6 = 1;
                decimal v7 = 1;
                string Perio = "";

                cm.CommandText = cadena;
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@PR_ATO", Ato);
                cm.Parameters.AddWithValue("@PR_MATRICULA", Matricula);
                cm.Parameters.AddWithValue("@PR_ORIGEN", Origen);
                cm.Parameters.AddWithValue("@PR_DESTINO", Destino);
                cm.Parameters.AddWithValue("@PR_DESTINO1", Destino1);
                cm.Parameters.AddWithValue("@PR_DESTINO2", Destino2);
                cm.Parameters.AddWithValue("@PR_FECHAINICIO", FechaI);
                cm.Parameters.AddWithValue("@PR_FECHAFIN", FechaF);
                cm.Parameters.AddWithValue("@PR_HORAINICIO", HoraI);
                cm.Parameters.AddWithValue("@PR_HORAFIN", HoraF);
                cm.Parameters.AddWithValue("@PR_VALEST", v1).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALATE", v2).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALILU", v3).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALCHA", v4).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALPRO", v5).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALANT", v6).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_TOTAL", v7).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_PERIO", Perio).Direction = ParameterDirection.Output;
                //string valor = cm.Parameters[16].iDB2Value.ToString();
                cm.CommandTimeout = 0;
                // cm.ExecuteNonQuery();

                iDB2DataReader dr = cm.ExecuteReader();

                string VALEST = cm.Parameters[10].iDB2Value.ToString();
                string VALATE = cm.Parameters[11].iDB2Value.ToString();
                string VALILU = cm.Parameters[12].iDB2Value.ToString();
                string VALCHA = cm.Parameters[13].iDB2Value.ToString();
                string VALPRO = cm.Parameters[14].iDB2Value.ToString();
                string VALANT = cm.Parameters[15].iDB2Value.ToString();
                string TOTAL = cm.Parameters[16].iDB2Value.ToString();
                string NPERIODO = cm.Parameters[17].iDB2Value.ToString();
                tbFr3 oSolicitud = new tbFr3();
                oSolicitud.PERIODO = NPERIODO;
                oSolicitud.AEROPUERTO = Ato;
                oSolicitud.MATRICULA = Matricula;
                oSolicitud.ORIGEN = Origen;
                oSolicitud.DESTINO = Destino;
                oSolicitud.DESTINO1 = Destino1;
                oSolicitud.DESTINO2 = Destino2;
                oSolicitud.FECHAINGRESOPLATAFORMA = FechaI;
                oSolicitud.FECHASALIDAPLATAFORMA = FechaF;
                oSolicitud.HORAINGRESOPLATAFORMA = HoraI;
                oSolicitud.HORASALIDAPLATAFORMA = HoraF;

                if (VALEST != "0")
                {
                    oSolicitud.DERECHOESTACIONAMIENTO = Convert.ToDecimal(VALEST.ToString());
                }
                if (VALATE != "0")
                {
                    oSolicitud.DERECHOATERRIZAJEDIURNO = Convert.ToDecimal(VALATE.ToString());
                }
                if (VALILU != "0")
                {
                    oSolicitud.DERECHOILUMINACION = Convert.ToDecimal(VALILU.ToString());
                }
                if (VALCHA != "0")
                {
                    oSolicitud.CHARTER = Convert.ToDecimal(VALCHA.ToString());
                }
                if (VALPRO != "0")
                {
                    oSolicitud.DERECHOPROTVLO = Convert.ToDecimal(VALPRO.ToString());
                }
                if (VALANT != "0")
                {
                    oSolicitud.DERECHOPROTANTESYDESPUES = Convert.ToDecimal(VALANT.ToString());
                }
                if (TOTAL != "0")
                {
                    oSolicitud.TOTAL = Convert.ToDecimal(TOTAL.ToString());
                }
                listarSolicitud = oSolicitud;

                con.Close();


            }
            catch (Exception ex)
            {
                //   throw ex;
            }
            return listarSolicitud;
        }


        public List<tbFr3> ConsultaFr3()
        {
            string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbFr3> listarSolicitud = new List<tbFr3>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR5 left join opcar1 on opcaer=opcco4 WHERE OPCFE4 = '" + fECHA + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbFr3 oSolicitud = new tbFr3();
                        oSolicitud.NUMEROFR3 = Convert.ToInt16(dr["OPCSEC"].ToString());
                        oSolicitud.ATO = dr["OPCAER"].ToString();
                        oSolicitud.FECHAEMISION = dr["OPCFE4"].ToString();
                        oSolicitud.AÑO = dr["OPCANO"].ToString();
                        oSolicitud.AEROPUERTO = dr["opcdes"].ToString();
                        oSolicitud.MATRICULA = dr["OPCMAT"].ToString();
                        oSolicitud.ORIGEN = dr["OPCORI"].ToString();
                        oSolicitud.DESTINO = dr["OPCDE7"].ToString();
                        oSolicitud.DESTINO1 = dr["OPCRET"].ToString();
                        oSolicitud.DESTINO2 = dr["OPCRE8"].ToString();

                        oSolicitud.FECHAINGRESOPLATAFORMA = dr["OPCFE5"].ToString();
                        oSolicitud.FECHASALIDAPLATAFORMA = dr["OPCFE6"].ToString();
                        oSolicitud.HORAINGRESOPLATAFORMA = dr["OPCHO8"].ToString();
                        oSolicitud.HORASALIDAPLATAFORMA = dr["OPCHO9"].ToString();

                        // oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(VALEST.ToString());
                        oSolicitud.DERECHOESTACIONAMIENTO = Convert.ToDecimal(dr["OPCDE4"].ToString());
                        oSolicitud.DERECHOATERRIZAJEDIURNO = Convert.ToDecimal(dr["OPCDER"].ToString());
                        oSolicitud.DERECHOPROTVLO = Convert.ToDecimal(dr["OPCDE3"].ToString());
                        oSolicitud.DERECHOPROTANTESYDESPUES = Convert.ToDecimal(dr["OPCTOT"].ToString());
                        oSolicitud.DERECHOILUMINACION = Convert.ToDecimal(dr["OPCDE2"].ToString());
                        oSolicitud.CHARTER = Convert.ToDecimal(dr["OPCVA6"].ToString());

                        oSolicitud.TOTAL = Convert.ToDecimal(dr["OPCGRA"].ToString());
                        oSolicitud.PERIODO = dr["OPCPER"].ToString();


                        oSolicitud.NACINT = dr["OPCNAC"].ToString();
                        oSolicitud.RUTA = dr["OPCRUT"].ToString();
                        oSolicitud.DISTANCIA = dr["OPCDI2"].ToString();
                        oSolicitud.RUC = dr["OPCRU1"].ToString();
                        oSolicitud.RAZONSOCIAL = dr["OPCNO4"].ToString();
                        oSolicitud.CIA = dr["OPCC08"].ToString();
                        oSolicitud.NOMBRECIA = dr["OPCNO5"].ToString();
                        oSolicitud.AUTORIZACION = dr["OPCAUT"].ToString();

                        oSolicitud.TRANSFERENCIA = dr["OPCCHE"].ToString();
                        oSolicitud.EMAIL = dr["OPCEM1"].ToString();
                        oSolicitud.OBSERVACION = dr["OPCOBS"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["OPCNUM"].ToString();
                        oSolicitud.RECAUDACION = dr["OPCC07"].ToString();
                        oSolicitud.USUARIOCREA = dr["OPCUS7"].ToString();
                        oSolicitud.FECHACREA = dr["OPCDA4"].ToString();
                        oSolicitud.HORACREA = dr["OPCH01"].ToString();
                        // oSolicitud.ESTADO = dr["OPCPRO"].ToString();

                        oSolicitud.FORMAPAGO = dr["OPCFOR"].ToString();
                        string formaPago = dr["OPCFOR"].ToString();
                        switch (formaPago)
                        {
                            case "01":
                                oSolicitud.FORMAPAGO = "EFECTIVO";
                                break;

                            case "02":
                                oSolicitud.FORMAPAGO = "TRANSF / DEPOSITO";
                                break;

                            case "03":
                                oSolicitud.FORMAPAGO = "ABONO";
                                break;
                            case "04":
                                oSolicitud.FORMAPAGO = "PAGO POSTERIOR";
                                break;

                            default:
                                break;
                        }
                        string banco = dr["OPCBAN"].ToString();
                        switch (banco)
                        {
                            case "01":
                                oSolicitud.BANCO = "RUMIÑAHUI";
                                break;

                            case "02":
                                oSolicitud.BANCO = "INTERNACIONAL";
                                break;

                            case "03":
                                oSolicitud.BANCO = "BANECUADOR";
                                break;

                            default:
                                break;
                        }

                        string estado = dr["OPCPRO"].ToString();
                        switch (estado)
                        {
                            case "P":
                                oSolicitud.ESTADO = "PROCESADO";
                                break;

                            case "E":
                                oSolicitud.ESTADO = "ENVIADO A FACTURAR";
                                break;

                            case "A":
                                oSolicitud.ESTADO = "ANULADO";
                                break;

                            default:
                                break;
                        }
                        string tipo = dr["OPCTIP"].ToString();
                        switch (tipo)
                        {
                            case "01":
                                oSolicitud.TIPOOPERACION = "PRIVADO";
                                break;

                            case "02":
                                oSolicitud.TIPOOPERACION = "CHARTER NACIONAL";
                                break;

                            case "03":
                                oSolicitud.TIPOOPERACION = "CHARTER INTERNACIONAL";
                                break;
                            case "04":
                                oSolicitud.TIPOOPERACION = "ESPECIAL NACIONAL";
                                break;
                            case "05":
                                oSolicitud.TIPOOPERACION = "ESPECIAL INTERNACIONAL";
                                break;

                            default:
                                break;
                        }
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
        public List<tbFr3> ConsultaFr3PorFecha(string FECHA)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbFr3> listarSolicitud = new List<tbFr3>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR5 left join opcar1 on opcaer=opcco4 WHERE OPCFE4 = '" + FECHA + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbFr3 oSolicitud = new tbFr3();
                        oSolicitud.NUMEROFR3 = Convert.ToInt16(dr["OPCSEC"].ToString());
                        oSolicitud.FECHAEMISION = dr["OPCFE4"].ToString();
                        oSolicitud.AÑO = dr["OPCANO"].ToString();
                        oSolicitud.AEROPUERTO = dr["opcdes"].ToString();
                        oSolicitud.MATRICULA = dr["OPCMAT"].ToString();
                        oSolicitud.ORIGEN = dr["OPCORI"].ToString();
                        oSolicitud.DESTINO = dr["OPCDE7"].ToString();
                        oSolicitud.DESTINO1 = dr["OPCRET"].ToString();
                        oSolicitud.DESTINO2 = dr["OPCRE8"].ToString();

                        oSolicitud.FECHAINGRESOPLATAFORMA = dr["OPCFE5"].ToString();
                        oSolicitud.FECHASALIDAPLATAFORMA = dr["OPCFE6"].ToString();
                        oSolicitud.HORAINGRESOPLATAFORMA = dr["OPCHO8"].ToString();
                        oSolicitud.HORASALIDAPLATAFORMA = dr["OPCHO9"].ToString();

                        // oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(VALEST.ToString());
                        oSolicitud.DERECHOESTACIONAMIENTO = Convert.ToDecimal(dr["OPCDE4"].ToString());
                        oSolicitud.DERECHOATERRIZAJEDIURNO = Convert.ToDecimal(dr["OPCDER"].ToString());
                        oSolicitud.DERECHOPROTVLO = Convert.ToDecimal(dr["OPCDE3"].ToString());
                        oSolicitud.DERECHOPROTANTESYDESPUES = Convert.ToDecimal(dr["OPCTOT"].ToString());
                        oSolicitud.DERECHOILUMINACION = Convert.ToDecimal(dr["OPCDE2"].ToString());
                        oSolicitud.CHARTER = Convert.ToDecimal(dr["OPCVA6"].ToString());

                        oSolicitud.TOTAL = Convert.ToDecimal(dr["OPCGRA"].ToString());
                        oSolicitud.PERIODO = dr["OPCPER"].ToString();


                        oSolicitud.NACINT = dr["OPCNAC"].ToString();
                        oSolicitud.RUTA = dr["OPCRUT"].ToString();
                        oSolicitud.DISTANCIA = dr["OPCDI2"].ToString();
                        oSolicitud.RUC = dr["OPCRU1"].ToString();
                        oSolicitud.RAZONSOCIAL = dr["OPCNO4"].ToString();
                        oSolicitud.CIA = dr["OPCC08"].ToString();
                        oSolicitud.NOMBRECIA = dr["OPCNO5"].ToString();
                        oSolicitud.AUTORIZACION = dr["OPCAUT"].ToString();

                        oSolicitud.TRANSFERENCIA = dr["OPCCHE"].ToString();
                        oSolicitud.EMAIL = dr["OPCEM1"].ToString();
                        oSolicitud.OBSERVACION = dr["OPCOBS"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["OPCNUM"].ToString();
                        oSolicitud.RECAUDACION = dr["OPCC07"].ToString();
                        oSolicitud.USUARIOCREA = dr["OPCUS7"].ToString();
                        oSolicitud.FECHACREA = dr["OPCDA4"].ToString();
                        oSolicitud.HORACREA = dr["OPCH01"].ToString();
                        // oSolicitud.ESTADO = dr["OPCPRO"].ToString();

                        oSolicitud.FORMAPAGO = dr["OPCFOR"].ToString();
                        string formaPago = dr["OPCFOR"].ToString();
                        switch (formaPago)
                        {
                            case "01":
                                oSolicitud.FORMAPAGO = "EFECTIVO";
                                break;

                            case "02":
                                oSolicitud.FORMAPAGO = "TRANSF / DEPOSITO";
                                break;

                            case "03":
                                oSolicitud.FORMAPAGO = "ABONO";
                                break;
                            case "04":
                                oSolicitud.FORMAPAGO = "PAGO POSTERIOR";
                                break;

                            default:
                                break;
                        }
                        string banco = dr["OPCBAN"].ToString();
                        switch (banco)
                        {
                            case "01":
                                oSolicitud.BANCO = "RUMIÑAHUI";
                                break;

                            case "02":
                                oSolicitud.BANCO = "INTERNACIONAL";
                                break;

                            case "03":
                                oSolicitud.BANCO = "BANECUADOR";
                                break;

                            default:
                                break;
                        }

                        string estado = dr["OPCPRO"].ToString();
                        switch (estado)
                        {
                            case "P":
                                oSolicitud.ESTADO = "PROCESADO";
                                break;

                            case "E":
                                oSolicitud.ESTADO = "ENVIADO A FACTURAR";
                                break;

                            case "A":
                                oSolicitud.ESTADO = "ANULADO";
                                break;

                            default:
                                break;
                        }
                        string tipo = dr["OPCTIP"].ToString();
                        switch (tipo)
                        {
                            case "01":
                                oSolicitud.TIPOOPERACION = "PRIVADO";
                                break;

                            case "02":
                                oSolicitud.TIPOOPERACION = "CHARTER NACIONAL";
                                break;

                            case "03":
                                oSolicitud.TIPOOPERACION = "CHARTER INTERNACIONAL";
                                break;
                            case "04":
                                oSolicitud.TIPOOPERACION = "ESPECIAL NACIONAL";
                                break;
                            case "05":
                                oSolicitud.TIPOOPERACION = "ESPECIAL INTERNACIONAL";
                                break;

                            default:
                                break;
                        }
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
        public tbFr3 ConsultaFr3PorSecuencia(Int16 NumeroFr3, string Ato, string Ano)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbFr3 listarSolicitud = new tbFr3();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR5 left join opcar1 on opcaer=opcco4 left join usuarc on usucod=opcus7  WHERE OPCSEC = " + NumeroFr3 + " AND OPCAER= '" + Ato + "' AND OPCANO ='" + Ano + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    dr.Read();

                    //while (dr.Read())
                    //{
                    tbFr3 oSolicitud = new tbFr3();
                    oSolicitud.NUMEROFR3 = Convert.ToInt16(dr["OPCSEC"].ToString());
                    oSolicitud.FECHAEMISION = dr["OPCFE4"].ToString();
                    oSolicitud.AÑO = dr["OPCANO"].ToString();
                    oSolicitud.AEROPUERTO = dr["opcdes"].ToString();
                    oSolicitud.MATRICULA = dr["OPCMAT"].ToString();
                    oSolicitud.CALLSIGN = dr["OPCCAL"].ToString();
                    oSolicitud.ORIGEN = dr["OPCORI"].ToString();
                    oSolicitud.DESTINO = dr["OPCDE7"].ToString();
                    oSolicitud.DESTINO1 = dr["OPCRET"].ToString();
                    oSolicitud.DESTINO2 = dr["OPCRE8"].ToString();
                    oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(dr["OPCPES"].ToString());

                    oSolicitud.FECHAINGRESOPLATAFORMA = dr["OPCFE5"].ToString();
                    oSolicitud.FECHASALIDAPLATAFORMA = dr["OPCFE6"].ToString();
                    oSolicitud.HORAINGRESOPLATAFORMA = dr["OPCHO8"].ToString();
                    oSolicitud.HORASALIDAPLATAFORMA = dr["OPCHO9"].ToString();

                    // oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(VALEST.ToString());
                    oSolicitud.DERECHOESTACIONAMIENTO = Convert.ToDecimal(dr["OPCDE4"].ToString());
                    oSolicitud.DERECHOATERRIZAJEDIURNO = Convert.ToDecimal(dr["OPCDER"].ToString());
                    oSolicitud.DERECHOPROTVLO = Convert.ToDecimal(dr["OPCDE3"].ToString());
                    oSolicitud.DERECHOPROTANTESYDESPUES = Convert.ToDecimal(dr["OPCTOT"].ToString());
                    oSolicitud.DERECHOILUMINACION = Convert.ToDecimal(dr["OPCDE2"].ToString());
                    oSolicitud.CHARTER = Convert.ToDecimal(dr["OPCVA6"].ToString());

                    oSolicitud.TOTAL = Convert.ToDecimal(dr["OPCGRA"].ToString());
                    oSolicitud.PERIODO = dr["OPCPER"].ToString();


                    oSolicitud.NACINT = dr["OPCNAC"].ToString();
                    oSolicitud.RUTA = dr["OPCRUT"].ToString();
                    oSolicitud.DISTANCIA = dr["OPCDI2"].ToString();
                    oSolicitud.RUC = dr["OPCRU1"].ToString();
                    oSolicitud.RAZONSOCIAL = dr["OPCNO4"].ToString();
                    oSolicitud.CIA = dr["OPCC08"].ToString();
                    oSolicitud.NOMBRECIA = dr["OPCNO5"].ToString();
                    oSolicitud.AUTORIZACION = dr["OPCAUT"].ToString();

                    oSolicitud.TRANSFERENCIA = dr["OPCCHE"].ToString();
                    oSolicitud.EMAIL = dr["OPCEM1"].ToString();
                    oSolicitud.OBSERVACION = dr["OPCOBS"].ToString();
                    oSolicitud.NUMEROFACTURA = dr["OPCNUM"].ToString();
                    oSolicitud.RECAUDACION = dr["OPCC07"].ToString();
                    oSolicitud.USUARIOCREA = dr["OPCUS7"].ToString();
                    oSolicitud.FECHACREA = dr["OPCDA4"].ToString();
                    oSolicitud.HORACREA = dr["OPCH01"].ToString();
                    string nombreusuario = dr["USUNOM"].ToString().Trim();
                    string apellidousuario = dr["USUAPE"].ToString().Trim();
                    oSolicitud.NOMBREUSUARIO = nombreusuario + " " + apellidousuario;
                    // oSolicitud.ESTADO = dr["OPCPRO"].ToString();

                    oSolicitud.FORMAPAGO = dr["OPCFOR"].ToString();
                    string formaPago = dr["OPCFOR"].ToString();
                    switch (formaPago)
                    {
                        case "01":
                            oSolicitud.FORMAPAGO = "EFECTIVO";
                            break;

                        case "02":
                            oSolicitud.FORMAPAGO = "TRANSF / DEPOSITO";
                            break;

                        case "03":
                            oSolicitud.FORMAPAGO = "ABONO";
                            break;
                        case "04":
                            oSolicitud.FORMAPAGO = "PAGO POSTERIOR";
                            break;

                        default:
                            break;
                    }
                    string banco = dr["OPCBAN"].ToString();
                    switch (banco)
                    {
                        case "01":
                            oSolicitud.BANCO = "RUMIÑAHUI";
                            break;

                        case "02":
                            oSolicitud.BANCO = "INTERNACIONAL";
                            break;

                        case "03":
                            oSolicitud.BANCO = "BANECUADOR";
                            break;

                        default:
                            break;
                    }

                    string estado = dr["OPCPRO"].ToString();
                    switch (estado)
                    {
                        case "P":
                            oSolicitud.ESTADO = "PROCESADO";
                            break;

                        case "E":
                            oSolicitud.ESTADO = "ENVIADO A FACTURAR";
                            break;

                        case "A":
                            oSolicitud.ESTADO = "ANULADO";
                            break;

                        default:
                            break;
                    }
                    string tipo = dr["OPCTIP"].ToString();
                    switch (tipo)
                    {
                        case "01":
                            oSolicitud.TIPOOPERACION = "PRIVADO";
                            break;

                        case "02":
                            oSolicitud.TIPOOPERACION = "CHARTER NACIONAL";
                            break;

                        case "03":
                            oSolicitud.TIPOOPERACION = "CHARTER INTERNACIONAL";
                            break;
                        case "04":
                            oSolicitud.TIPOOPERACION = "ESPECIAL NACIONAL";
                            break;
                        case "05":
                            oSolicitud.TIPOOPERACION = "ESPECIAL INTERNACIONAL";
                            break;

                        default:
                            break;
                    }
                    listarSolicitud = oSolicitud;



                    dr.Close();
                    oConexion.Close();
                    return listarSolicitud;
                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
           
        }
    }
}
