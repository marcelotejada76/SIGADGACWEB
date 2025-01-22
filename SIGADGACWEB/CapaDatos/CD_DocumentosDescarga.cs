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
    public class CD_DocumentosDescarga
    {
        public static CD_DocumentosDescarga _instancia = null;
        private CD_DocumentosDescarga()
        {

        }

        public static CD_DocumentosDescarga Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DocumentosDescarga();
                }
                return _instancia;
            }
        }



        public List<tbDocumentosDescarga> ConsultaDocumentos()
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5 ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());
                        
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

        public List<tbDocumentosDescarga> ConsultaDocumentosInstTecnicas()
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='Instrucciones Tecnicas'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

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
        public List<tbDocumentosDescarga> ConsultaDocumentosProcedimientos()
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='Procedimientos Tecnicos'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

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
        public List<tbDocumentosDescarga> ConsultaDocumentosCartasAcuerdo()
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='CartasdeAcuerdoOperacional'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

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
        public List<tbDocumentosDescarga> ConsultaDocumentosManualesAtsp()
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='ManualesATSP'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

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
        public List<tbDocumentosDescarga> ConsultaDocumentosFormulariosAtm()
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='FormulariosAtm'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

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
        public List<tbDocumentosDescarga> DocumentosPorNombre(string NOmbre)
        {

            List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPDAR5  WHERE OPDNOM LIKE  ('%" + NOmbre + "%')");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

                        oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
                        oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
                        oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());


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
