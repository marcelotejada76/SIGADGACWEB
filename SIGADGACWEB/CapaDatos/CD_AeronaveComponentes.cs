using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_AeronaveComponentes
    {
        public static CD_AeronaveComponentes _instancia = null;
        private CD_AeronaveComponentes()
        {

        }

        public static CD_AeronaveComponentes Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_AeronaveComponentes();
                }
                return _instancia;
            }
        }

        public List<tbAeronavesComponentes> DetalleDocumentos()
        {
            List<tbAeronavesComponentes> listarSolicitud = new List<tbAeronavesComponentes>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            string Codigo = "HC";
            try
            {
                sbSol.Append("SELECT * FROM AERAR1 WHERE SUBSTRING(AERMAT,1,2)='"+Codigo+"'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMAT"].ToString();
                        oSolicitud.CODIFOOACI = dr["AERCO1"].ToString().Trim();
                        oSolicitud.MARCA = dr["AERFAB"].ToString();
                        oSolicitud.MODELO = dr["AERMOD"].ToString();
                        
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

        //busqueda por maricula
        public List<tbAeronavesComponentes> DetalleDocumentosMatricula(string Matricula)
        {
            List<tbAeronavesComponentes> listarSolicitud = new List<tbAeronavesComponentes>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            string Codigo = "HC";
            try
            {
                sbSol.Append("SELECT * FROM AERAR1 WHERE AERMAT='" + Matricula + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMAT"].ToString();
                        oSolicitud.CODIFOOACI = dr["AERCO1"].ToString().Trim();
                        oSolicitud.MARCA = dr["AERFAB"].ToString();
                        oSolicitud.MODELO = dr["AERMOD"].ToString();

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

        public tbAeronavesComponentes DetalleDocumentosClave(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbAeronavesComponentes listarSolicitud = new tbAeronavesComponentes();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT  * FROM AERAR9 LEFT JOIN CIAARC01 ON CIACOD=AERC06  LEFT JOIN OPCAR1 ON OPCCO4=AERBA3 WHERE AERMA8 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMA8"].ToString();
                        oSolicitud.EXPLOTADOR = dr["AERC06"].ToString().Trim()+"/"+ dr["CIANOM"].ToString().Trim();
                        oSolicitud.PROPIETARIO = dr["AERPRO"].ToString();
                        oSolicitud.MARCA = dr["AERAE1"].ToString();
                        oSolicitud.MODELO = dr["AERAE2"].ToString();
                        oSolicitud.SERIE = dr["AERAE3"].ToString();
                        oSolicitud.AÑOFAB = dr["AERANO"].ToString();

                        oSolicitud.PESOVACIO = decimal.Parse(dr["AERP05"].ToString());
                        oSolicitud.PMP = decimal.Parse(dr["AERP06"].ToString());
                        oSolicitud.TECHO = decimal.Parse(dr["AERTEC"].ToString());
                        oSolicitud.NUMEROPAX = Int16.Parse(dr["AERN11"].ToString());

                        oSolicitud.PESOVACIODESIGNACION = dr["AERP07"].ToString();
                        oSolicitud.PMPDESIGNACION = dr["AERP08"].ToString();
                        oSolicitud.TECHODESIGNACION = dr["AERTE1"].ToString();

                        oSolicitud.MOTOR1MARCA = dr["AERMOT"].ToString();
                        oSolicitud.MOTOR2MARCA = dr["AERMO6"].ToString();
                        oSolicitud.MOTOR1MODELO = dr["AERMO5"].ToString();
                        oSolicitud.MOTOR2MODELO = dr["AERMO7"].ToString();
                        oSolicitud.HELICE1MARCA = dr["AERHEL"].ToString();
                        oSolicitud.HELICE2MARCA = dr["AERHE2"].ToString();
                        oSolicitud.HELICE1MODELO = dr["AERHE1"].ToString();
                        oSolicitud.HELICE2MODELO = dr["AERHE3"].ToString();

                        oSolicitud.ELTMARCA = dr["AEREL3"].ToString();
                        oSolicitud.ELTMODELO = dr["AEREL4"].ToString();
                        oSolicitud.ELTSERIE = dr["AEREL5"].ToString();
                        oSolicitud.ELTCODIGOHEX = dr["AEREL6"].ToString();

                        oSolicitud.ELTPORTATILMARCA = dr["AEREL8"].ToString();
                        oSolicitud.ELTPORTATILMODELO = dr["AEREL9"].ToString();
                        oSolicitud.ELTPORTATILCODIGOHEX = dr["AERE05"].ToString();

                        oSolicitud.CODIGOMODOSS = dr["AEREL7"].ToString();
                        oSolicitud.TIPOAPROBACION = dr["AERTI8"].ToString();

                        //oSolicitud.CONDICION = dr["AERC05"].ToString();
                        string Condicion = dr["AERC05"].ToString();
                        switch (Condicion)
                        {
                            case "0":
                                oSolicitud.CONDICION = "OPERABLE";
                                break;

                            case "1":
                                oSolicitud.CONDICION = "MANTENIMIENTO";
                                break;
                            case "2":
                                oSolicitud.CONDICION = "ACCIDENTADO";
                                break;
                            case "3":
                                oSolicitud.CONDICION = "CANCELADO";
                                break;
                            case "4":
                                oSolicitud.CONDICION = "SALIO DEL PAIS";
                                break;
                            case "5":
                                oSolicitud.CONDICION = "INACTIVO";
                                break;

                            default:
                                break;
                        }

                        string Region = dr["AERR08"].ToString();
                        switch (Region)
                        {
                            case "0":
                                oSolicitud.REGION = "COSTA";
                                break;

                            case "1":
                                oSolicitud.REGION = "SIERRA";
                                break;
                            
                            default:
                                break;
                        }

                        oSolicitud.BASEOPERACION = dr["OPCDES"].ToString();
                        oSolicitud.FECHAMONITOREORVSM = dr["AERMON"].ToString();
                        oSolicitud.ERRORASE = dr["AERMO8"].ToString();
                        oSolicitud.OBSERVACIONES = dr["AEROB7"].ToString();



                        //LLENA DETALE DE CERTIFICADOS AERONAVEGABILIDAD
                        oSolicitud.oDetalleCertAeronavegabilidad= CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoAero(oSolicitud.AERONAVE);
                        //LLENA DETALE DE CERTIFICADOS DE RADIO
                        oSolicitud.oDetalleCertRadio = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRadio(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS DE RUIDO
                        oSolicitud.oDetalleCerHomolRuido = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRuido(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS PBN RNP10
                        oSolicitud.oDetalleCertPbnRnp10 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNP10(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RNAV5
                        oSolicitud.oDetalleCertRnav5 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNAV5(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RNAV2
                        oSolicitud.oDetalleCertRnav2 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNAV2(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS APPROACH
                        oSolicitud.oDetalleCertRnavApproach = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoAPPROACH(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RVSM
                        oSolicitud.oDetalleCertRvsm = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRVSM(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS ETOPS
                        oSolicitud.oDetalleCertEtops = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoETOPS(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS CAT II/III
                        oSolicitud.oDetalleCategorias = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCategorias(oSolicitud.AERONAVE);

                        //LLENA DETALE DE ACCIDENTE
                        oSolicitud.oDetalleAccidenteAeronave = CD_DetalleCertAeronav.Instancia.DetalleDocumentosAccidente(oSolicitud.AERONAVE);


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

      
    }
}
