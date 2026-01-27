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
    public class CD_BasesCoactivas
    {
        public static CD_BasesCoactivas _instancia = null;
        private CD_BasesCoactivas()
        {

        }

        public static CD_BasesCoactivas Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_BasesCoactivas();
                }
                return _instancia;
            }
        }



        public List<tbBasesCoactivas> ConsultaBasesCoactivas()
        {

            List<tbBasesCoactivas> listarSolicitud = new List<tbBasesCoactivas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM FIBARC");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbBasesCoactivas oSolicitud = new tbBasesCoactivas();

                        oSolicitud.NroProceso = dr["FIBNRO"].ToString().Trim();
                        oSolicitud.PersonaJuridica = dr["FIBPER"].ToString().Trim();
                        oSolicitud.RucCedula = dr["FIBRUC"].ToString().Trim();
                        oSolicitud.EstadoEmpresa = dr["FIBES2"].ToString().Trim();
                        oSolicitud.NroTituloCredito = dr["FIBNR1"].ToString().Trim();
                        oSolicitud.FechaEmision = dr["FIBFEC"].ToString().Trim();
                        oSolicitud.Cuantia = dr.GetDecimal(dr.GetOrdinal("FIBCUA"));
                        oSolicitud.EstadoProcesal = dr["FIBEST"].ToString().Trim();
                        oSolicitud.FechaActuacion = dr["FIBFE1"].ToString().Trim();
                        oSolicitud.NroFojas = dr["FIBFOJ"].ToString().Trim();

                        
                        string estado = dr["FIBES1"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            default:
                                break;
                        }
                        oSolicitud.Estado = estado;


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


        public tbBasesCoactivas DetalleDocumentosBaseCoactiva(string Nro, string Persona)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbBasesCoactivas listarSolicitud = new tbBasesCoactivas();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM FIBARC WHERE FIBNRO = '" + Nro + "' AND FIBPER='" + Persona + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbBasesCoactivas oSolicitud = new tbBasesCoactivas();

                        oSolicitud.NroProceso = dr["FIBNRO"].ToString().Trim();
                        oSolicitud.PersonaJuridica = dr["FIBPER"].ToString().Trim();
                        oSolicitud.RucCedula = dr["FIBRUC"].ToString().Trim();
                        oSolicitud.EstadoEmpresa = dr["FIBES2"].ToString().Trim();
                        oSolicitud.NroTituloCredito = dr["FIBNR1"].ToString().Trim();
                        oSolicitud.FechaEmision = dr["FIBFEC"].ToString().Trim();
                        oSolicitud.Cuantia = dr.GetDecimal(dr.GetOrdinal("FIBCUA"));
                        oSolicitud.EstadoProcesal = dr["FIBEST"].ToString().Trim();
                        oSolicitud.FechaActuacion = dr["FIBFE1"].ToString().Trim();
                        oSolicitud.NroFojas = dr["FIBFOJ"].ToString().Trim();


                        string estado = dr["FIBES1"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            default:
                                break;
                        }
                        oSolicitud.Estado = estado;


                        oSolicitud.DetalleAccion = dr["FIBOBS"].ToString().Trim()+ dr["FIBOB1"].ToString().Trim()+ dr["FIBOB2"].ToString().Trim()+ dr["FIBOB3"].ToString().Trim()+
                            dr["FIBOB4"].ToString().Trim()+ dr["FIBOB5"].ToString().Trim()+ dr["FIBOB6"].ToString().Trim()+ dr["FIBOB7"].ToString().Trim()+ dr["FIBOB8"].ToString().Trim()+
                            dr["FIBOB9"].ToString().Trim()+ dr["FIBO01"].ToString().Trim() + dr["FIBO02"].ToString().Trim() + dr["FIBO03"].ToString().Trim() + dr["FIBO04"].ToString().Trim() +
                            dr["FIBO05"].ToString().Trim() + dr["FIBO06"].ToString().Trim();

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

        public List<tbBasesCoactivas> DetalleDocumentosporProceso(string Busqueda)
        {

            List<tbBasesCoactivas> listarSolicitud = new List<tbBasesCoactivas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM FIBARC  WHERE FIBNRO = '" + Busqueda + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbBasesCoactivas oSolicitud = new tbBasesCoactivas();
                        oSolicitud.NroProceso = dr["FIBNRO"].ToString().Trim();
                        oSolicitud.PersonaJuridica = dr["FIBPER"].ToString().Trim();
                        oSolicitud.RucCedula = dr["FIBRUC"].ToString().Trim();
                        oSolicitud.EstadoEmpresa = dr["FIBES2"].ToString().Trim();
                        oSolicitud.NroTituloCredito = dr["FIBNR1"].ToString().Trim();
                        oSolicitud.FechaEmision = dr["FIBFEC"].ToString().Trim();
                        oSolicitud.Cuantia = dr.GetDecimal(dr.GetOrdinal("FIBCUA"));
                        oSolicitud.EstadoProcesal = dr["FIBEST"].ToString().Trim();
                        oSolicitud.FechaActuacion = dr["FIBFE1"].ToString().Trim();
                        oSolicitud.NroFojas = dr["FIBFOJ"].ToString().Trim();


                        string estado = dr["FIBES1"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            default:
                                break;
                        }
                        oSolicitud.Estado = estado;


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

        public List<tbBasesCoactivas> DetalleDocumentosporPersonaJuridica(string Busqueda)
        {

            List<tbBasesCoactivas> listarSolicitud = new List<tbBasesCoactivas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM FIBARC  WHERE  FIBPER like ('%" + Busqueda + "%')"); 

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbBasesCoactivas oSolicitud = new tbBasesCoactivas();
                        oSolicitud.NroProceso = dr["FIBNRO"].ToString().Trim();
                        oSolicitud.PersonaJuridica = dr["FIBPER"].ToString().Trim();
                        oSolicitud.RucCedula = dr["FIBRUC"].ToString().Trim();
                        oSolicitud.EstadoEmpresa = dr["FIBES2"].ToString().Trim();
                        oSolicitud.NroTituloCredito = dr["FIBNR1"].ToString().Trim();
                        oSolicitud.FechaEmision = dr["FIBFEC"].ToString().Trim();
                        oSolicitud.Cuantia = dr.GetDecimal(dr.GetOrdinal("FIBCUA"));
                        oSolicitud.EstadoProcesal = dr["FIBEST"].ToString().Trim();
                        oSolicitud.FechaActuacion = dr["FIBFE1"].ToString().Trim();
                        oSolicitud.NroFojas = dr["FIBFOJ"].ToString().Trim();


                        string estado = dr["FIBES1"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.Estado = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.Estado = "NO ACTIVO";
                                break;

                            default:
                                break;
                        }
                        oSolicitud.Estado = estado;


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
        //public tbAtc ConsultacControladorLicencia(string Licencia)
        //{
        //    // string fECHA = DateTime.Now.ToString("yyyyMMdd");
        //    tbAtc listarSolicitud = new tbAtc();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPCAR9  WHERE OPCLIC = '" + Licencia + "'");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbAtc oSolicitud = new tbAtc();

        //                oSolicitud.Licencia = dr["OPCLIC"].ToString().Trim();
        //                oSolicitud.Nombre = dr["OPCN01"].ToString().Trim();
        //                oSolicitud.ApellidoPaterno = dr["OPCAPE"].ToString().Trim();
        //                oSolicitud.ApellidoMaterno = dr["OPCAP1"].ToString().Trim();
        //                oSolicitud.Institucion = dr["OPCINS"].ToString().Trim();
        //                oSolicitud.Dependencia = dr["OPCD04"].ToString().Trim();
        //                oSolicitud.Ciudad = dr["OPCCIU"].ToString().Trim();
        //                oSolicitud.VigenciaCemac = dr["OPCF07"].ToString().Trim();
        //                oSolicitud.Cedula = dr["OPCCED"].ToString().Trim();
        //                //oSolicitud.Estado = dr["OPCES5"].ToString();
        //                oSolicitud.ApellidoNombre = dr["OPCAPE"].ToString().Trim() + " " + dr["OPCN01"].ToString().Trim();
        //                oSolicitud.Url = @"\\172.20.19.55\TransitoAereo\imagenes\" + Licencia + "jpg";
        //                string estado = dr["OPCES5"].ToString();
        //                switch (estado)
        //                {
        //                    case "AC":
        //                        oSolicitud.Estado = "ACTIVO";
        //                        break;

        //                    case "NO":
        //                        oSolicitud.Estado = "NO ACTIVO";
        //                        break;

        //                    case "EN":
        //                        oSolicitud.Estado = "ENTRENAMIENTO";
        //                        break;

        //                    default:
        //                        break;
        //                }

        //                string coddependencia = dr["OPCD05"].ToString().Trim();
        //                switch (coddependencia)
        //                {
        //                    case "ACC":
        //                        oSolicitud.CodigoDependencia = " CENTRO CONTROL DE AREA";
        //                        break;

        //                    case "AFIS":
        //                        oSolicitud.CodigoDependencia = "SERVICIO DE INFORMACION DE VUELO AD";
        //                        break;

        //                    case "APP":
        //                        oSolicitud.CodigoDependencia = "CONTROL DE APROXIMACION";
        //                        break;
        //                    case "FIC":
        //                        oSolicitud.CodigoDependencia = "CENTRO DE INFORMACION DE VUELO";
        //                        break;
        //                    case "TWR":
        //                        oSolicitud.CodigoDependencia = "TORRE DE CONTROL";
        //                        break;
        //                    default:
        //                        break;
        //                }


        //                listarSolicitud =oSolicitud;
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }

        //        return listarSolicitud;
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}
        ////POR APELLIDO
        //public List<tbAtc> ControladorLicenciaApellido(string Apellido)
        //{

        //    List<tbAtc> listarSolicitud = new List<tbAtc>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPCAR9  WHERE OPCAPE like ('%" + Apellido + "%')");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbAtc oSolicitud = new tbAtc();

        //                oSolicitud.Licencia = dr["OPCLIC"].ToString().Trim();
        //                oSolicitud.Nombre = dr["OPCN01"].ToString().Trim();
        //                oSolicitud.ApellidoPaterno = dr["OPCAPE"].ToString().Trim();
        //                oSolicitud.ApellidoMaterno = dr["OPCAP1"].ToString().Trim();
        //                oSolicitud.Institucion = dr["OPCINS"].ToString().Trim();
        //                oSolicitud.Dependencia = dr["OPCD04"].ToString().Trim();
        //                oSolicitud.Ciudad = dr["OPCCIU"].ToString().Trim();
        //                oSolicitud.VigenciaCemac = dr["OPCF07"].ToString().Trim();
        //                oSolicitud.Cedula = dr["OPCCED"].ToString().Trim();

        //                oSolicitud.ApellidoNombre = dr["OPCAPE"].ToString().Trim() + " " + dr["OPCN01"].ToString().Trim();
        //                //oSolicitud.Estado = dr["OPCES5"].ToString();

        //                string estado = dr["OPCES5"].ToString();
        //                switch (estado)
        //                {
        //                    case "AC":
        //                        oSolicitud.Estado = "ACTIVO";
        //                        break;

        //                    case "NO":
        //                        oSolicitud.Estado = "NO ACTIVO";
        //                        break;

        //                    case "EN":
        //                        oSolicitud.Estado = "ENTRENAMIENTO";
        //                        break;

        //                    default:
        //                        break;
        //                }

        //                string Dependencia = dr["OPCD05"].ToString();
        //                switch (Dependencia)
        //                {
        //                    case "ACC":
        //                        oSolicitud.CodigoDependencia = " CENTRO CONTROL DE AREA";
        //                        break;

        //                    case "AFIS":
        //                        oSolicitud.CodigoDependencia = "SERVICIO DE INFORMACION DE VUELO AD";
        //                        break;

        //                    case "APP":
        //                        oSolicitud.CodigoDependencia = "CONTROL DE APROXIMACION";
        //                        break;
        //                    case "FIC":
        //                        oSolicitud.CodigoDependencia = "CENTRO DE INFORMACION DE VUELO";
        //                        break;
        //                    case "TWR":
        //                        oSolicitud.CodigoDependencia = "TORRE DE CONTROL";
        //                        break;
        //                    default:
        //                        break;
        //                }

        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}

    }
}
