using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Depositos
    {
        public static CD_Depositos _instancia = null;
        private CD_Depositos()
        {

        }

        public static CD_Depositos Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Depositos();
                }
                return _instancia;
            }
        }

        //carga clientes todos consulta
        public List<tbSubirDepositos> DetalleDepositosVista()
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS from ficar6 " +
                    "where ficnu9 >0 ORDER BY FICANO,FICMES,ficemp");
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
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.Mes = dr["MES"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }



        //carga clientes todos cliente
        public List<tbSubirDepositos> DetalleDepositosVistaCliente(string Cliente)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
             


                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS from ficar6 " +
                    "where ficnu9 >0 and ficemp LIKE ('%"+Cliente+"%')ORDER BY FICANO,FICMES,ficemp");
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
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.Mes = dr["MES"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }




        public List<tbSubirDepositos> DetalleDepositos(string canio, string ruc)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS from ficar6 " +
                    "where ficano='"+canio+"' and ficru1='"+ruc+"' and fices2='1' ORDER BY FICANO,FICMES");
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
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.Mes = dr["MES"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        //actualiza numero de registros en la taba
        public tbSubirDepositos ActualizaRegistros(string canio, string ruc, string mes, int registros)
        {
            //List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("update ficar6 set ficnu9= "+registros+ " where ficano='" + canio + "' and ficru1='" + ruc + "' and ficmes='"+mes+"'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        //consulta todos los clientes
        public List<tbSubirDepositos> ConsultaDepositos(string canio)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS  from ficar6 " +
                    "where ficano='" + canio + "' and fices2='1' and ficnu9 >=1 ");
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
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.Mes = dr["MES"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

    }
}
