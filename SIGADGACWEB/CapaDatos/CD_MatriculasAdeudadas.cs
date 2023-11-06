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
    public class CD_MatriculasAdeudadas
    {
        public static CD_MatriculasAdeudadas _instancia = null;
        private CD_MatriculasAdeudadas()
        {

        }

        public static CD_MatriculasAdeudadas Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_MatriculasAdeudadas();
                }
                return _instancia;
            }
        }

        public List<tbMatriculasAdeudadas> DetalleMatriculas()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbMatriculasAdeudadas> listarSolicitud = new List<tbMatriculasAdeudadas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM DEUARC ORDER BY DEUNOM");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbMatriculasAdeudadas oSolicitud = new tbMatriculasAdeudadas();
                        oSolicitud.OIDDEUDOR = Convert.ToInt32(dr["DEUIDD"].ToString());
                        oSolicitud.RUC = dr["DEURUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["DEUNOM"].ToString();

                        oSolicitud.MATRICULA = dr["DEUMA1"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["DEUNUM"].ToString();
                        oSolicitud.FECHAFACTURA = dr["DEUFEC"].ToString();
                        oSolicitud.FECHARECEPCION = dr["DEUFE1"].ToString();
                        oSolicitud.FECHAVENCIMIENTO = dr["DEUFE3"].ToString();

                        oSolicitud.VALORFACTURA = decimal.Parse(dr["DEUVAL"].ToString());
                        oSolicitud.VALORSALDO = decimal.Parse(dr["DEUVA1"].ToString());

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

        public tbMatriculasAdeudadas DetalleMatriculasOid(Int32 OidMatricula)
        {
            tbMatriculasAdeudadas oSolicitud = new tbMatriculasAdeudadas();
            //List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            // tbMatriculas oSolicitud = new tbMatriculas();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            try
            {
                sbSol.Append("SELECT * FROM DEUARC WHERE DEUIDD=" + OidMatricula + "");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            //  tbMatriculasAdeudadas oSolicitud = new tbMatriculasAdeudadas();
                            oSolicitud.OIDDEUDOR = Convert.ToInt32(dr["DEUIDD"].ToString());
                            oSolicitud.RUC = dr["DEURUC"].ToString();
                            oSolicitud.COMPAÑIA = dr["DEUNOM"].ToString();

                            oSolicitud.MATRICULA = dr["DEUMA1"].ToString();
                            oSolicitud.NUMEROFACTURA = dr["DEUNUM"].ToString();
                            oSolicitud.FECHAFACTURA = dr["DEUFEC"].ToString();
                        oSolicitud.FECHARECEPCION = dr["DEUFE1"].ToString();
                        oSolicitud.FECHAVENCIMIENTO = dr["DEUFE3"].ToString();

                        oSolicitud.VALORFACTURA = decimal.Parse(dr["DEUVAL"].ToString());
                            oSolicitud.VALORSALDO = decimal.Parse(dr["DEUVA1"].ToString());

                            // listarSolicitud.Add(oSolicitud);
                        }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
               // throw ex;
            }
            return oSolicitud;
        }

        ////busqueda por compania
        public List<tbMatriculasAdeudadas> DetalleMatriculasDeudorasCompania(String Compania)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbMatriculasAdeudadas> listarSolicitud = new List<tbMatriculasAdeudadas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM DEUARC where DEUNOM Like ('" + Compania + "%') ORDER BY DEUNOM ");
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
                        tbMatriculasAdeudadas oSolicitud = new tbMatriculasAdeudadas();
                        oSolicitud.OIDDEUDOR = Convert.ToInt32(dr["DEUIDD"].ToString());
                        oSolicitud.RUC = dr["DEURUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["DEUNOM"].ToString();

                        oSolicitud.MATRICULA = dr["DEUMA1"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["DEUNUM"].ToString();
                        oSolicitud.FECHAFACTURA = dr["DEUFEC"].ToString();
                        oSolicitud.FECHARECEPCION = dr["DEUFE1"].ToString();
                        oSolicitud.FECHAVENCIMIENTO = dr["DEUFE3"].ToString();

                        oSolicitud.VALORFACTURA = decimal.Parse(dr["DEUVAL"].ToString());
                        oSolicitud.VALORSALDO = decimal.Parse(dr["DEUVA1"].ToString());

                        listarSolicitud.Add(oSolicitud);

                        
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }


        ////busqueda por matricula

 
        public List<tbMatriculasAdeudadas> DetalleMatriculasDeudorasMatricula(String Compania)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbMatriculasAdeudadas> listarSolicitud = new List<tbMatriculasAdeudadas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM DEUARC where DEUMA1 Like ('" + Compania + "%') ORDER BY DEUMA1 ");
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
                        tbMatriculasAdeudadas oSolicitud = new tbMatriculasAdeudadas();
                        oSolicitud.OIDDEUDOR = Convert.ToInt32(dr["DEUIDD"].ToString());
                        oSolicitud.RUC = dr["DEURUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["DEUNOM"].ToString();

                        oSolicitud.MATRICULA = dr["DEUMA1"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["DEUNUM"].ToString();
                        oSolicitud.FECHAFACTURA = dr["DEUFEC"].ToString();
                        oSolicitud.FECHARECEPCION = dr["DEUFE1"].ToString();
                        oSolicitud.FECHAVENCIMIENTO = dr["DEUFE3"].ToString();

                        oSolicitud.VALORFACTURA = decimal.Parse(dr["DEUVAL"].ToString());
                        oSolicitud.VALORSALDO = decimal.Parse(dr["DEUVA1"].ToString());

                        listarSolicitud.Add(oSolicitud);


                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }
        
    }
}
