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
    public class CD_Matriculas
    {
        public static CD_Matriculas _instancia = null;
        private CD_Matriculas()
        {

        }

        public static CD_Matriculas Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Matriculas();
                }
                return _instancia;
            }
        }

        public List<tbMatriculas> DetalleMatriculas()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbMatriculas> listarSolicitud = new List<tbMatriculas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM V_AERONAVE01  FETCH FIRST 100 ROWS ONLY");
                
                query = sbSol.ToString();
                OdbcCommand cmd;



                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbMatriculas oSolicitud = new tbMatriculas();
                        oSolicitud.OID = Convert.ToInt32( dr["OID"].ToString());
                        oSolicitud.RUC = dr["RUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["COMPANIA"].ToString();
                        
                        oSolicitud.MATRICULA = dr["MATRICULAO"].ToString();
                        oSolicitud.MARCA = dr["MARCA"].ToString();
                        oSolicitud.MODELO = dr["MODELO"].ToString();
                        oSolicitud.ESTADO = dr["ESTADO"].ToString();
                        oSolicitud.USO = dr["USO"].ToString();

                        oSolicitud.FECHACREA = dr["FECHACREA"].ToString();
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["PESOMAXESTRUCTURAL"].ToString());
                        
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

        public tbMatriculas DetalleMatriculaP5(Int32 OidMatricula)
        {
            tbMatriculas oSolicitud = new tbMatriculas();
            //List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
           // tbMatriculas oSolicitud = new tbMatriculas();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM V_AERONAVE01 WHERE OID=" + OidMatricula + "");

                query = sbSol.ToString();
                OdbcCommand cmd;

                //OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion);

                // iDB2Command cmd;


                // using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                   
                    // iDB2DataReader dr = cmd.ExecuteReader();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // , ,,,,


                        oSolicitud.OID =Convert.ToInt32(dr["OID"].ToString());
                        oSolicitud.RUC = dr["RUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["COMPANIA"].ToString();

                        oSolicitud.MATRICULA = dr["MATRICULAO"].ToString();
                        oSolicitud.MARCA = dr["MARCA"].ToString();
                        oSolicitud.MODELO = dr["MODELO"].ToString();
                        oSolicitud.ESTADO = dr["ESTADO"].ToString();
                        oSolicitud.USO = dr["USO"].ToString();
                        oSolicitud.DESIGNADOR = dr["DESIGNADOR"].ToString();
                        oSolicitud.FECHACREA = dr["FECHACREA"].ToString();
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();

                        oSolicitud.FECHAMODIFICA = dr["FECHAMODIFICA"].ToString();
                        oSolicitud.USUARIOMODIFICA = dr["USUARIOMODIFICA"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["PESOMAXESTRUCTURAL"].ToString());

                       // listarSolicitud.Add(oSolicitud);

                        //listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oSolicitud;
        }

        //busqueda por compania
        public List<tbMatriculas> DetalleMatriculasP5Compania(String Compania)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbMatriculas> listarSolicitud = new List<tbMatriculas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM V_AERONAVE01 where COMPANIA Like ('" + Compania + "%') ORDER BY ESTADO ");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                OdbcCommand cmd;


                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();

                    // iDB2DataReader dr = cmd.ExecuteReader();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbMatriculas oSolicitud = new tbMatriculas();
                        oSolicitud.OID = Convert.ToInt32(dr["OID"].ToString());
                        oSolicitud.RUC = dr["RUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["COMPANIA"].ToString();

                        oSolicitud.MATRICULA = dr["MATRICULAO"].ToString();
                        oSolicitud.MARCA = dr["MARCA"].ToString();
                        oSolicitud.MODELO = dr["MODELO"].ToString();
                        oSolicitud.ESTADO = dr["ESTADO"].ToString();
                        oSolicitud.USO = dr["USO"].ToString();

                        oSolicitud.FECHACREA = dr["FECHACREA"].ToString();
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["PESOMAXESTRUCTURAL"].ToString());

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
        //busqueda por matricula
        public List<tbMatriculas> DetallePorMatriculasP5(String Compania)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbMatriculas> listarSolicitud = new List<tbMatriculas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM V_AERONAVE01 where MATRICULAO Like ('" + Compania + "%') ORDER BY ESTADO ");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                OdbcCommand cmd;


                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();

                    // iDB2DataReader dr = cmd.ExecuteReader();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbMatriculas oSolicitud = new tbMatriculas();
                        oSolicitud.OID = Convert.ToInt32(dr["OID"].ToString());
                        oSolicitud.RUC = dr["RUC"].ToString();
                        oSolicitud.COMPAÑIA = dr["COMPANIA"].ToString();

                        oSolicitud.MATRICULA = dr["MATRICULAO"].ToString();
                        oSolicitud.MARCA = dr["MARCA"].ToString();
                        oSolicitud.MODELO = dr["MODELO"].ToString();
                        oSolicitud.ESTADO = dr["ESTADO"].ToString();
                        oSolicitud.USO = dr["USO"].ToString();

                        oSolicitud.FECHACREA = dr["FECHACREA"].ToString();
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["PESOMAXESTRUCTURAL"].ToString());

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
