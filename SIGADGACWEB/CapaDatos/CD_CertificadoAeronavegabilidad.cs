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
    public class CD_CertificadoAeronavegabilidad
    {
        public static CD_CertificadoAeronavegabilidad _instancia = null;
        private CD_CertificadoAeronavegabilidad()
        {

        }

        public static CD_CertificadoAeronavegabilidad Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_CertificadoAeronavegabilidad();
                }
                return _instancia;
            }
        }

        public List<tbCertificadoAeronavegabilidad> DetalleDocumentos()
        {
            string fechaRegistro = DateTime.Now.ToString("yyyyMMdd");
            DateTime fechaInicial = DateTime.Now;
            DateTime fechaFinal = DateTime.Now.AddDays(15);
            string CfechaI = fechaInicial.ToString("dd/MM/yyyy");
            string CfechaF = fechaFinal.ToString("dd/MM/yyyy");

            List<tbCertificadoAeronavegabilidad> listarSolicitud = new List<tbCertificadoAeronavegabilidad>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                    sbSol.Append("SELECT DISTINCT NOMBRECOMPANIA, MATRICULA, REGION, APROBADO, DESTINADOAUSO, NUMEROSERIE, ESTADO, CATEGORIA, NOMBREAERONAVE, MODELOAERONAVE," +
                "  CODCERTIFICADO, FECHAOTORGAMIENTO, FECHAVENCIMIENTO, CLASIFICACIONNOMBRE FROM AERONAVEGABILIDAD where FECHAVENCIMIENTO  >='" + CfechaI + "' and FECHAVENCIMIENTO <='" + CfechaF + "' ");


                query = sbSol.ToString();
                OdbcCommand cmd;



                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();

                    OdbcDataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbCertificadoAeronavegabilidad oSolicitud = new tbCertificadoAeronavegabilidad();
                        oSolicitud.NOMBRECOMPANIA = dr["NOMBRECOMPANIA"].ToString().Trim();
                        oSolicitud.MATRICULA = dr["MATRICULA"].ToString().Trim();
                        oSolicitud.REGION = dr["REGION"].ToString().Trim();
                        oSolicitud.APROBADO = dr["APROBADO"].ToString().Trim();
                        oSolicitud.DESTINADOAUSO = dr["DESTINADOAUSO"].ToString().Trim();
                        oSolicitud.NUMEROSERIE = dr["NUMEROSERIE"].ToString().Trim();
                        oSolicitud.CATEGORIA = dr["CATEGORIA"].ToString().Trim();
                        oSolicitud.NOMBREAERONAVE = dr["NOMBREAERONAVE"].ToString().Trim();
                        oSolicitud.MODELOAERONAVE = dr["MODELOAERONAVE"].ToString().Trim();
                        oSolicitud.CODIGOCERTIFICADO = dr["CODCERTIFICADO"].ToString().Trim();
                        oSolicitud.FECHAOTORGAMIENTO = dr["FECHAOTORGAMIENTO"].ToString().Trim();
                        if (oSolicitud.FECHAOTORGAMIENTO!="")
                        {
                            oSolicitud.FECHAOTORGAMIENTO = oSolicitud.FECHAOTORGAMIENTO.Substring(0, 10);
                        }
                       
                        oSolicitud.FECHAVENCIMIENTO = dr["FECHAVENCIMIENTO"].ToString().Trim();
                        if (oSolicitud.FECHAVENCIMIENTO != "")
                        {
                            oSolicitud.FECHAVENCIMIENTO = oSolicitud.FECHAVENCIMIENTO.Substring(0, 10);
                        }
                        
                        oSolicitud.CLASIFICACION = dr["CLASIFICACIONNOMBRE"].ToString().Trim();

                       
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



        public tbCertificadoAeronavegabilidad DetalleDocumentosClave(string Matricula,string FechaI)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbCertificadoAeronavegabilidad listarSolicitud = new tbCertificadoAeronavegabilidad();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT DISTINCT NOMBRECOMPANIA, MATRICULA, REGION, APROBADO, DESTINADOAUSO, NUMEROSERIE, ESTADO, CATEGORIA, NOMBREAERONAVE, MODELOAERONAVE," +
                "  CODCERTIFICADO, FECHAOTORGAMIENTO, FECHAVENCIMIENTO, CLASIFICACIONNOMBRE FROM AERONAVEGABILIDAD where MATRICULA  ='" + Matricula + "' and FECHAOTORGAMIENTO ='" + FechaI + "' ");

                query = sbSol.ToString();
                OdbcCommand cmd;

                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();

                    OdbcDataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbCertificadoAeronavegabilidad oSolicitud = new tbCertificadoAeronavegabilidad();
                        oSolicitud.NOMBRECOMPANIA = dr["NOMBRECOMPANIA"].ToString().Trim();
                        oSolicitud.MATRICULA = dr["MATRICULA"].ToString().Trim();
                        oSolicitud.REGION = dr["REGION"].ToString().Trim();
                        oSolicitud.APROBADO = dr["APROBADO"].ToString().Trim();
                        oSolicitud.DESTINADOAUSO = dr["DESTINADOAUSO"].ToString().Trim();
                        oSolicitud.ESTADO = dr["ESTADO"].ToString().Trim();
                        oSolicitud.NUMEROSERIE = dr["NUMEROSERIE"].ToString().Trim();
                        oSolicitud.CATEGORIA = dr["CATEGORIA"].ToString().Trim();
                        oSolicitud.NOMBREAERONAVE = dr["NOMBREAERONAVE"].ToString().Trim();
                        oSolicitud.MODELOAERONAVE = dr["MODELOAERONAVE"].ToString().Trim();
                        oSolicitud.CODIGOCERTIFICADO = dr["CODCERTIFICADO"].ToString().Trim();
                        oSolicitud.FECHAOTORGAMIENTO = dr["FECHAOTORGAMIENTO"].ToString().Trim();
                        if (oSolicitud.FECHAOTORGAMIENTO != "")
                        {
                            oSolicitud.FECHAOTORGAMIENTO = oSolicitud.FECHAOTORGAMIENTO.Substring(0, 10);
                        }

                        oSolicitud.FECHAVENCIMIENTO = dr["FECHAVENCIMIENTO"].ToString().Trim();
                        if (oSolicitud.FECHAVENCIMIENTO != "")
                        {
                            oSolicitud.FECHAVENCIMIENTO = oSolicitud.FECHAVENCIMIENTO.Substring(0, 10);
                        }
                        oSolicitud.CLASIFICACION = dr["CLASIFICACIONNOMBRE"].ToString().Trim();
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
