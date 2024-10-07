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
    public class CD_Controlador
    {
        public static CD_Controlador _instancia = null;
        private CD_Controlador()
        {

        }

        public static CD_Controlador Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Controlador();
                }
                return _instancia;
            }
        }



        public List<tbAtc> ConsultaControlador()
        {

            List<tbAtc> listarSolicitud = new List<tbAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR9");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAtc oSolicitud = new tbAtc();

                        oSolicitud.Licencia = dr["OPCLIC"].ToString().Trim();
                        oSolicitud.Nombre = dr["OPCN01"].ToString().Trim();
                        oSolicitud.ApellidoPaterno = dr["OPCAPE"].ToString().Trim();
                        oSolicitud.ApellidoMaterno = dr["OPCAP1"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPCINS"].ToString().Trim();
                        oSolicitud.Dependencia = dr["OPCD04"].ToString().Trim();
                        oSolicitud.Ciudad = dr["OPCCIU"].ToString().Trim();
                        oSolicitud.VigenciaCemac = dr["OPCF07"].ToString().Trim();

                        oSolicitud.ApellidoNombre = dr["OPCAPE"].ToString().Trim() +" "+ dr["OPCN01"].ToString().Trim();
                        //oSolicitud.Estado = dr["OPCES5"].ToString();

                        string estado = dr["OPCES5"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            case "EN":
                                oSolicitud.Estado = "ENTRENAMIENTO";
                                break;

                            default:
                                break;
                        }
                        string Dependencia = dr["OPCD05"].ToString();
                        switch (Dependencia)
                        {
                            case "ACC":
                                oSolicitud.CodigoDependencia = " CENTRO CONTROL DE AREA";
                                break;

                            case "AFIS":
                                oSolicitud.CodigoDependencia = "SERVICIO DE INFORMACION DE VUELO AD";
                                break;

                            case "APP":
                                oSolicitud.CodigoDependencia = "CONTROL DE APROXIMACION";
                                break;
                            case "FIC":
                                oSolicitud.CodigoDependencia = "CENTRO DE INFORMACION DE VUELO";
                                break;
                            case "TWR":
                                oSolicitud.CodigoDependencia = "TORRE DE CONTROL";
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

        public List<tbAtc> ControladorLicencia(string Licencia)
        {

            List<tbAtc> listarSolicitud = new List<tbAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR9  WHERE OPCLIC = '" + Licencia + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAtc oSolicitud = new tbAtc();

                        oSolicitud.Licencia = dr["OPCLIC"].ToString().Trim();
                        oSolicitud.Nombre = dr["OPCN01"].ToString().Trim();
                        oSolicitud.ApellidoPaterno = dr["OPCAPE"].ToString().Trim();
                        oSolicitud.ApellidoMaterno = dr["OPCAP1"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPCINS"].ToString().Trim();
                        oSolicitud.Dependencia = dr["OPCD04"].ToString().Trim();
                        oSolicitud.Ciudad = dr["OPCCIU"].ToString().Trim();
                        oSolicitud.VigenciaCemac = dr["OPCF07"].ToString().Trim();

                        oSolicitud.ApellidoNombre = dr["OPCAPE"].ToString().Trim() + " " + dr["OPCN01"].ToString().Trim();
                        //oSolicitud.Estado = dr["OPCES5"].ToString();

                        string estado = dr["OPCES5"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            case "EN":
                                oSolicitud.Estado = "ENTRENAMIENTO";
                                break;

                            default:
                                break;
                        }

                        string Dependencia = dr["OPCD05"].ToString();
                        switch (Dependencia)
                        {
                            case "ACC":
                                oSolicitud.CodigoDependencia = " CENTRO CONTROL DE AREA";
                                break;

                            case "AFIS":
                                oSolicitud.CodigoDependencia = "SERVICIO DE INFORMACION DE VUELO AD";
                                break;

                            case "APP":
                                oSolicitud.CodigoDependencia = "CONTROL DE APROXIMACION";
                                break;
                            case "FIC":
                                oSolicitud.CodigoDependencia = "CENTRO DE INFORMACION DE VUELO";
                                break;
                            case "TWR":
                                oSolicitud.CodigoDependencia = "TORRE DE CONTROL";
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
        public tbAtc ConsultacControladorLicencia(string Licencia)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbAtc listarSolicitud = new tbAtc();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR9  WHERE OPCLIC = '" + Licencia + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAtc oSolicitud = new tbAtc();

                        oSolicitud.Licencia = dr["OPCLIC"].ToString().Trim();
                        oSolicitud.Nombre = dr["OPCN01"].ToString().Trim();
                        oSolicitud.ApellidoPaterno = dr["OPCAPE"].ToString().Trim();
                        oSolicitud.ApellidoMaterno = dr["OPCAP1"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPCINS"].ToString().Trim();
                        oSolicitud.Dependencia = dr["OPCD04"].ToString().Trim();
                        oSolicitud.Ciudad = dr["OPCCIU"].ToString().Trim();
                        oSolicitud.VigenciaCemac = dr["OPCF07"].ToString().Trim();
                        //oSolicitud.Estado = dr["OPCES5"].ToString();
                        oSolicitud.ApellidoNombre = dr["OPCAPE"].ToString().Trim() + " " + dr["OPCN01"].ToString().Trim();

                        string estado = dr["OPCES5"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            case "EN":
                                oSolicitud.Estado = "ENTRENAMIENTO";
                                break;

                            default:
                                break;
                        }

                        string coddependencia = dr["OPCD05"].ToString().Trim();
                        switch (coddependencia)
                        {
                            case "ACC":
                                oSolicitud.CodigoDependencia = " CENTRO CONTROL DE AREA";
                                break;

                            case "AFIS":
                                oSolicitud.CodigoDependencia = "SERVICIO DE INFORMACION DE VUELO AD";
                                break;

                            case "APP":
                                oSolicitud.CodigoDependencia = "CONTROL DE APROXIMACION";
                                break;
                            case "FIC":
                                oSolicitud.CodigoDependencia = "CENTRO DE INFORMACION DE VUELO";
                                break;
                            case "TWR":
                                oSolicitud.CodigoDependencia = "TORRE DE CONTROL";
                                break;
                            default:
                                break;
                        }

                       
                        listarSolicitud =oSolicitud;
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
        //POR APELLIDO
        public List<tbAtc> ControladorLicenciaApellido(string Apellido)
        {

            List<tbAtc> listarSolicitud = new List<tbAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCAR9  WHERE OPCAPE like ('%" + Apellido + "%')");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAtc oSolicitud = new tbAtc();

                        oSolicitud.Licencia = dr["OPCLIC"].ToString().Trim();
                        oSolicitud.Nombre = dr["OPCN01"].ToString().Trim();
                        oSolicitud.ApellidoPaterno = dr["OPCAPE"].ToString().Trim();
                        oSolicitud.ApellidoMaterno = dr["OPCAP1"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPCINS"].ToString().Trim();
                        oSolicitud.Dependencia = dr["OPCD04"].ToString().Trim();
                        oSolicitud.Ciudad = dr["OPCCIU"].ToString().Trim();
                        oSolicitud.VigenciaCemac = dr["OPCF07"].ToString().Trim();

                        oSolicitud.ApellidoNombre = dr["OPCAPE"].ToString().Trim() + " " + dr["OPCN01"].ToString().Trim();
                        //oSolicitud.Estado = dr["OPCES5"].ToString();

                        string estado = dr["OPCES5"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            case "EN":
                                oSolicitud.Estado = "ENTRENAMIENTO";
                                break;

                            default:
                                break;
                        }

                        string Dependencia = dr["OPCD05"].ToString();
                        switch (Dependencia)
                        {
                            case "ACC":
                                oSolicitud.CodigoDependencia = " CENTRO CONTROL DE AREA";
                                break;

                            case "AFIS":
                                oSolicitud.CodigoDependencia = "SERVICIO DE INFORMACION DE VUELO AD";
                                break;

                            case "APP":
                                oSolicitud.CodigoDependencia = "CONTROL DE APROXIMACION";
                                break;
                            case "FIC":
                                oSolicitud.CodigoDependencia = "CENTRO DE INFORMACION DE VUELO";
                                break;
                            case "TWR":
                                oSolicitud.CodigoDependencia = "TORRE DE CONTROL";
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
      
    }
}
