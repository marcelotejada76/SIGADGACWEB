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
    public class CD_BancoRuminahui
    {
        public static CD_BancoRuminahui _instancia = null;
        private CD_BancoRuminahui()
        {

        }

        public static CD_BancoRuminahui Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_BancoRuminahui();
                }
                return _instancia;
            }
        }

        public List<tbBancoRuminahui> DetalleDepositos()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM FICARC ORDER BY FICFEC");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbBancoRuminahui oSolicitud = new tbBancoRuminahui();
                        oSolicitud.FECHAPROCESO = dr["FICFEC"].ToString();
                        oSolicitud.NUMEROCOMPROBANTE = dr["FICNUM"].ToString();
                        oSolicitud.HORA = dr["FICHOR"].ToString();
                        oSolicitud.OFICINA = dr["FICOFI"].ToString();
                        oSolicitud.CONCEPTO = dr["FICCON"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["FICNU1"].ToString();
                        oSolicitud.CODIGOCOMPROBANTE = dr["FICCOD"].ToString();
                        oSolicitud.MONTO = decimal.Parse(dr["FICMON"].ToString());


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


        //fecha de deposito
        public tbBancoRuminahui DetalleDepositoPorFecha(string Fechadeposito, string NumeroComprobante)
        {
            tbBancoRuminahui oSolicitud = new tbBancoRuminahui();
            //List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            // tbMatriculas oSolicitud = new tbMatriculas();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            try
            {
                sbSol.Append("SELECT * FROM FICARC WHERE FICFEC= '" + Fechadeposito + "' AND FICNUM = '" + NumeroComprobante + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        oSolicitud.FECHAPROCESO = dr["FICFEC"].ToString();
                        oSolicitud.NUMEROCOMPROBANTE = dr["FICNUM"].ToString();
                        oSolicitud.HORA = dr["FICHOR"].ToString();
                        oSolicitud.OFICINA = dr["FICOFI"].ToString();
                        oSolicitud.CONCEPTO = dr["FICCON"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["FICNU1"].ToString();
                        oSolicitud.CODIGOCOMPROBANTE = dr["FICCOD"].ToString();
                        oSolicitud.MONTO = decimal.Parse(dr["FICMON"].ToString());

                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                return oSolicitud = null;
                // throw ex;
            }
            return oSolicitud;
        }

        //por numero de comprobante
        public List<tbBancoRuminahui> DetalleDepositoComprobante(string NumeroComprobante)
        {

            List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
            // tbMatriculas oSolicitud = new tbMatriculas();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            try
            {
                sbSol.Append("SELECT * FROM FICARC WHERE FICNUM= '" + NumeroComprobante + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        tbBancoRuminahui oSolicitud = new tbBancoRuminahui();
                        oSolicitud.FECHAPROCESO = dr["FICFEC"].ToString();
                        oSolicitud.NUMEROCOMPROBANTE = dr["FICNUM"].ToString();
                        oSolicitud.HORA = dr["FICHOR"].ToString();
                        oSolicitud.OFICINA = dr["FICOFI"].ToString();
                        oSolicitud.CONCEPTO = dr["FICCON"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["FICNU1"].ToString();
                        oSolicitud.CODIGOCOMPROBANTE = dr["FICCOD"].ToString();
                        oSolicitud.MONTO = decimal.Parse(dr["FICMON"].ToString());
                        listarSolicitud.Add(oSolicitud);

                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
                // throw ex;
            }
            return listarSolicitud;
        }

        ////busqueda por NUMERO FACTURA
        public List<tbBancoRuminahui> DetalleDepositoNumeroFactura(string NumeroComprobante)
        {
            List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
            if (NumeroComprobante == "")
            {
              
                listarSolicitud = DetalleDepositos();
                return listarSolicitud;
            }
            else
            {


               // List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
                // tbMatriculas oSolicitud = new tbMatriculas();
                StringBuilder sbSol = new StringBuilder();
                string query = string.Empty;

                try
                {
                    sbSol.Append("SELECT * FROM FICARC WHERE FICNU1= '" + NumeroComprobante + "' ");

                    query = sbSol.ToString();
                    iDB2Command cmd;


                    using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        cmd = new iDB2Command(query, oConexion);
                        oConexion.Open();
                        iDB2DataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            tbBancoRuminahui oSolicitud = new tbBancoRuminahui();
                            oSolicitud.FECHAPROCESO = dr["FICFEC"].ToString();
                            oSolicitud.NUMEROCOMPROBANTE = dr["FICNUM"].ToString();
                            oSolicitud.HORA = dr["FICHOR"].ToString();
                            oSolicitud.OFICINA = dr["FICOFI"].ToString();
                            oSolicitud.CONCEPTO = dr["FICCON"].ToString();
                            oSolicitud.NUMEROFACTURA = dr["FICNU1"].ToString();
                            oSolicitud.CODIGOCOMPROBANTE = dr["FICCOD"].ToString();
                            oSolicitud.MONTO = decimal.Parse(dr["FICMON"].ToString());
                            listarSolicitud.Add(oSolicitud);

                        }
                        dr.Close();
                        oConexion.Close();
                    }
                    return listarSolicitud;
                }
                catch (Exception ex)
                {
                    listarSolicitud = null;
                    // throw ex;
                }

            }
            return listarSolicitud;

        }
    }
}
