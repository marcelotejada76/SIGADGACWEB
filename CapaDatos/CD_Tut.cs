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
    public class CD_Tut
    {
        public static CD_Tut _instancia = null;
        private CD_Tut()
        {

        }

        public static CD_Tut Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Tut();
                }
                return _instancia;
            }
        }

    

        public List<tbTut> ConsultaTut()
        {
            string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbTut> listarSolicitud = new List<tbTut>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR7 left join opcar1 on opcae2=opcco4 WHERE OPCF01 = '" + fECHA + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbTut oSolicitud = new tbTut();
                        oSolicitud.NUMEROTASA = Convert.ToInt16(dr["OPCSE3"].ToString());
                        oSolicitud.ATO = dr["OPCAE2"].ToString();
                        oSolicitud.FECHAEMISION = dr["OPCF01"].ToString();
                        oSolicitud.AÑO = dr["OPCAN2"].ToString();
                        oSolicitud.AEROPUERTO = dr["OPCNO8"].ToString();
                        oSolicitud.MATRICULA = dr["OPCMA1"].ToString();
                        oSolicitud.ORIGEN = dr["OPCRU3"].ToString();
                        oSolicitud.DESTINO = dr["OPCRU4"].ToString();
                        // oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(VALEST.ToString());
                        oSolicitud.TASATUT = Convert.ToDecimal(dr["OPCTAS"].ToString());
                        oSolicitud.TASAWT = Convert.ToDecimal(dr["OPCTA1"].ToString());
                        oSolicitud.TASACFR = Convert.ToDecimal(dr["OPCTA2"].ToString());
                        
                        oSolicitud.TOTAL = Convert.ToDecimal(dr["OPCTO2"].ToString());
                        
                        oSolicitud.NACINT = dr["OPCNA1"].ToString();
                        
                        oSolicitud.RUC = dr["OPCRU2"].ToString();
                        oSolicitud.RAZONSOCIAL = dr["OPCNO7"].ToString();
                        //oSolicitud.CIA = dr["OPCC08"].ToString();
                        //oSolicitud.NOMBRECIA = dr["OPCNO5"].ToString();
                        //oSolicitud.AUTORIZACION = dr["OPCAUT"].ToString();

                        oSolicitud.TRANSFERENCIA = dr["OPCCT1"].ToString();
                        oSolicitud.EMAIL = dr["OPCEM4"].ToString();
                        oSolicitud.OBSERVACION = dr["OPCOB1"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["OPCNU2"].ToString();
                        oSolicitud.RECAUDACION = dr["OPCC09"].ToString();
                        oSolicitud.USUARIOCREA = dr["OPCUS9"].ToString();
                        oSolicitud.FECHACREA = dr["OPCDA6"].ToString();
                        oSolicitud.HORACREA = dr["OPCH03"].ToString();
                        // oSolicitud.ESTADO = dr["OPCPRO"].ToString();
                        oSolicitud.IMPRESO = dr["OPCES3"].ToString();

                        oSolicitud.FORMAPAGO = dr["OPCFO1"].ToString();
                        string formaPago = dr["OPCFO1"].ToString();
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
                        string banco = dr["OPCBA1"].ToString();
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

                        string estado = dr["OPCPR1"].ToString();
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
        public List<tbTut> ConsultaTutPorFecha(string FECHA)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbTut> listarSolicitud = new List<tbTut>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR7 left join opcar1 on opcae2=opcco4 WHERE OPCF01 = '" + FECHA + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbTut oSolicitud = new tbTut();
                        oSolicitud.NUMEROTASA = Convert.ToInt16(dr["OPCSE3"].ToString());
                        oSolicitud.ATO = dr["OPCAE2"].ToString();
                        oSolicitud.FECHAEMISION = dr["OPCF01"].ToString();
                        oSolicitud.AÑO = dr["OPCAN2"].ToString();
                        oSolicitud.AEROPUERTO = dr["OPCNO8"].ToString();
                        oSolicitud.MATRICULA = dr["OPCMA1"].ToString();
                        oSolicitud.ORIGEN = dr["OPCRU3"].ToString();
                        oSolicitud.DESTINO = dr["OPCRU4"].ToString();
                        // oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(VALEST.ToString());
                        oSolicitud.TASATUT = Convert.ToDecimal(dr["OPCTAS"].ToString());
                        oSolicitud.TASAWT = Convert.ToDecimal(dr["OPCTA1"].ToString());
                        oSolicitud.TASACFR = Convert.ToDecimal(dr["OPCTA2"].ToString());

                        oSolicitud.TOTAL = Convert.ToDecimal(dr["OPCTO2"].ToString());

                        oSolicitud.NACINT = dr["OPCNA1"].ToString();

                        oSolicitud.RUC = dr["OPCRU2"].ToString();
                        oSolicitud.RAZONSOCIAL = dr["OPCNO7"].ToString();
                        //oSolicitud.CIA = dr["OPCC08"].ToString();
                        //oSolicitud.NOMBRECIA = dr["OPCNO5"].ToString();
                        //oSolicitud.AUTORIZACION = dr["OPCAUT"].ToString();

                        oSolicitud.TRANSFERENCIA = dr["OPCCT1"].ToString();
                        oSolicitud.EMAIL = dr["OPCEM4"].ToString();
                        oSolicitud.OBSERVACION = dr["OPCOB1"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["OPCNU2"].ToString();
                        oSolicitud.RECAUDACION = dr["OPCC09"].ToString();
                        oSolicitud.USUARIOCREA = dr["OPCUS9"].ToString();
                        oSolicitud.FECHACREA = dr["OPCDA6"].ToString();
                        oSolicitud.HORACREA = dr["OPCH03"].ToString();
                        // oSolicitud.ESTADO = dr["OPCPRO"].ToString();
                        oSolicitud.IMPRESO = dr["OPCES3"].ToString();

                        oSolicitud.FORMAPAGO = dr["OPCFO1"].ToString();
                        string formaPago = dr["OPCFO1"].ToString();
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
                        string banco = dr["OPCBA1"].ToString();
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

                        string estado = dr["OPCPR1"].ToString();
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
        public tbTut ConsultaTutPorSecuencia(Int16 NumeroTut, string Ato, string Ano)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbTut listarSolicitud = new tbTut();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR7 left join opcar1 on opcae2=opcco4 left join usuarc on usucod=opcus9  WHERE OPCSE3 = " + NumeroTut + " AND OPCAE2= '" + Ato + "' AND OPCAN2 ='" + Ano + "'");

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
                    tbTut oSolicitud = new tbTut();
                    oSolicitud.NUMEROTASA = Convert.ToInt16(dr["OPCSE3"].ToString());
                    oSolicitud.ATO = dr["OPCAE2"].ToString();
                    oSolicitud.FECHAEMISION = dr["OPCF01"].ToString();
                    oSolicitud.AÑO = dr["OPCAN2"].ToString();
                    oSolicitud.AEROPUERTO = dr["OPCNO8"].ToString();
                    oSolicitud.MATRICULA = dr["OPCMA1"].ToString();
                    oSolicitud.ORIGEN = dr["OPCRU3"].ToString();
                    oSolicitud.DESTINO = dr["OPCRU4"].ToString();
                    string nombreusuario = dr["USUNOM"].ToString().Trim();
                    string apellidousuario = dr["USUAPE"].ToString().Trim();
                    oSolicitud.NOMBREUSUARIO = nombreusuario + " " + apellidousuario;

                    // oSolicitud.PESOMAXESTRUCTURAL = Convert.ToDecimal(VALEST.ToString());
                    oSolicitud.TASATUT = Convert.ToDecimal(dr["OPCTAS"].ToString());
                    oSolicitud.TASAWT = Convert.ToDecimal(dr["OPCTA1"].ToString());
                    oSolicitud.TASACFR = Convert.ToDecimal(dr["OPCTA2"].ToString());

                    oSolicitud.TOTAL = Convert.ToDecimal(dr["OPCTO2"].ToString());

                    oSolicitud.NACINT = dr["OPCNA1"].ToString();

                    oSolicitud.RUC = dr["OPCRU2"].ToString();
                    oSolicitud.RAZONSOCIAL = dr["OPCNO7"].ToString();
                    //oSolicitud.CIA = dr["OPCC08"].ToString();
                    //oSolicitud.NOMBRECIA = dr["OPCNO5"].ToString();
                    //oSolicitud.AUTORIZACION = dr["OPCAUT"].ToString();

                    oSolicitud.TRANSFERENCIA = dr["OPCCT1"].ToString();
                    oSolicitud.EMAIL = dr["OPCEM4"].ToString();
                    oSolicitud.OBSERVACION = dr["OPCOB1"].ToString();
                    oSolicitud.NUMEROFACTURA = dr["OPCNU2"].ToString();
                    oSolicitud.RECAUDACION = dr["OPCC09"].ToString();
                    oSolicitud.USUARIOCREA = dr["OPCUS9"].ToString();
                    oSolicitud.FECHACREA = dr["OPCDA6"].ToString();
                    oSolicitud.HORACREA = dr["OPCH03"].ToString();
                    // oSolicitud.ESTADO = dr["OPCPRO"].ToString();
                    oSolicitud.IMPRESO = dr["OPCES3"].ToString();

                    oSolicitud.FORMAPAGO = dr["OPCFO1"].ToString();
                    string formaPago = dr["OPCFO1"].ToString();
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
                    string banco = dr["OPCBA1"].ToString();
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

                    string estado = dr["OPCPR1"].ToString();
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
