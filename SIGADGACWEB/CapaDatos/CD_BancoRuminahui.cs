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
            DateTime fecha = DateTime.Now.AddDays(-120);
            string fechaProceso = fecha.ToString("yyyyMMdd"); //fecha del sistema
           

            List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM FICARC where FICFEC >='"+fechaProceso+"' ORDER BY FICFEC DESC ");

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
                        oSolicitud.FECHABCOCENTRAL = dr["FICFE1"].ToString();
                        oSolicitud.NUMCOMPBCOCENTRAL = dr["FICNU5"].ToString();
                        oSolicitud.ESTADO = dr["FICPRO"].ToString();
                        string ZONA = dr["FICENV"].ToString();
                        string deszona = "";
                        if (ZONA == "1")
                        {
                            deszona = "QUITO";
                        }
                        else
                        {
                            deszona = "GUAYAQUIL";
                        }
                        oSolicitud.ZONAL = deszona;
                        oSolicitud.DEPOSITANTE = dr["FICDEP"].ToString();

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
                        oSolicitud.FECHABCOCENTRAL = dr["FICFE1"].ToString();
                        oSolicitud.NUMCOMPBCOCENTRAL = dr["FICNU5"].ToString();
                        oSolicitud.ESTADO = dr["FICPRO"].ToString();
                        string ZONA = dr["FICENV"].ToString();
                        string deszona = "";
                        if (ZONA == "1")
                        {
                            deszona = "QUITO";
                        }
                        else
                        {
                            deszona = "GUAYAQUIL";
                        }
                        oSolicitud.ZONAL = deszona;
                        oSolicitud.DEPOSITANTE = dr["FICDEP"].ToString();
                        //LLENA DETALE DEL DEPOSITO COMPROBANTES
                        oSolicitud.oDetalleDeposito = CD_DetalleComprobanteDeposito.Instancia.DetalleComprobantesDeposito(oSolicitud.FECHAPROCESO,oSolicitud.NUMEROCOMPROBANTE);
                        var detalle = oSolicitud.oDetalleDeposito;
                       int cont = detalle.Count();
                        decimal valor = 0;
                        for (int i = 0; i < cont; i++)
                        {
                            valor = detalle[i].MONTOGENERAL;
                            
                        }
                        oSolicitud.TOTAL = valor;
                        oSolicitud.DIFERENCIA = oSolicitud.MONTO - valor;

                        //oSolicitud.TOTAL=oSolicitud.oDetalleDeposito.FI
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
                        oSolicitud.FECHABCOCENTRAL = dr["FICFE1"].ToString();
                        oSolicitud.NUMCOMPBCOCENTRAL = dr["FICNU5"].ToString();
                        oSolicitud.ESTADO = dr["FICPRO"].ToString();
                        string ZONA = dr["FICENV"].ToString();
                        string deszona = "";
                        if (ZONA == "1")
                        {
                            deszona = "QUITO";
                        }
                        else
                        {
                            deszona = "GUAYAQUIL";
                        }
                        oSolicitud.ZONAL = deszona;
                        oSolicitud.DEPOSITANTE = dr["FICDEP"].ToString();
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
                            oSolicitud.FECHABCOCENTRAL = dr["FICFE1"].ToString();
                            oSolicitud.NUMCOMPBCOCENTRAL = dr["FICNU5"].ToString();
                            oSolicitud.ESTADO = dr["FICPRO"].ToString();
                            //oSolicitud.ZONAL = dr["FICENV"].ToString();
                            string ZONA= dr["FICENV"].ToString();
                            string deszona = "";
                            if (ZONA=="1")
                            {
                                deszona = "QUITO";
                            }
                            else
                            {
                                deszona = "GUAYAQUIL";
                            }
                            oSolicitud.ZONAL = deszona;
                            oSolicitud.DEPOSITANTE = dr["FICDEP"].ToString();
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


        ////busqueda por NUMERO FACTURA
        public List<tbBancoRuminahui> DetalleDepositoFecha(string FechaEmision)
        {
            List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
            if (FechaEmision == "")
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
                    sbSol.Append("SELECT * FROM FICARC WHERE FICFEC= '" + FechaEmision + "' ");

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
                            oSolicitud.FECHABCOCENTRAL = dr["FICFE1"].ToString();
                            oSolicitud.NUMCOMPBCOCENTRAL = dr["FICNU5"].ToString();
                            oSolicitud.ESTADO = dr["FICPRO"].ToString();
                            //oSolicitud.ZONAL = dr["FICENV"].ToString();
                            string ZONA = dr["FICENV"].ToString();
                            string deszona = "";
                            if (ZONA == "1")
                            {
                                deszona = "QUITO";
                            }
                            else
                            {
                                deszona = "GUAYAQUIL";
                            }
                            oSolicitud.ZONAL = deszona;
                            oSolicitud.DEPOSITANTE = dr["FICDEP"].ToString();
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
        //detalle del depositos por comprobantes


        //depositante
        public List<tbBancoRuminahui> DetalleDepositante(string Depositante)
        {
            List<tbBancoRuminahui> listarSolicitud = new List<tbBancoRuminahui>();
            if (Depositante == "")
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
                    sbSol.Append("SELECT * FROM FICARC WHERE upper(FICDEP) like ('%" + Depositante + "%') ");

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
                            oSolicitud.FECHABCOCENTRAL = dr["FICFE1"].ToString();
                            oSolicitud.NUMCOMPBCOCENTRAL = dr["FICNU5"].ToString();
                            oSolicitud.ESTADO = dr["FICPRO"].ToString();
                            //oSolicitud.ZONAL = dr["FICENV"].ToString();
                            string ZONA = dr["FICENV"].ToString();
                            string deszona = "";
                            if (ZONA == "1")
                            {
                                deszona = "QUITO";
                            }
                            else
                            {
                                deszona = "GUAYAQUIL";
                            }
                            oSolicitud.ZONAL = deszona;
                            oSolicitud.DEPOSITANTE = dr["FICDEP"].ToString();
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
