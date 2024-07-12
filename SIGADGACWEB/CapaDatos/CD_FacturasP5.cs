using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_FacturasP5
    {
        public static CD_FacturasP5 _instancia = null;
        private CD_FacturasP5()
        {

        }

        public static CD_FacturasP5 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_FacturasP5();
                }
                return _instancia;
            }
        }

        public List<tbFacturasP5> DetalleFacturasP5()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
                sbSol.Append("SELECT FA.OID, FA.NOMBRECLIENTE, FA.CEDULA_RUC,FA.NUMEROFACTURA,CAST(FA.FECHA AS date) AS FECHA,FA.VALORFACTURA,FA.ESTADO,RE.CODIGO,FA.USUARIOCREA,FA.FECHACREA " +
                    "FROM FACTURA AS FA LEFT JOIN RECAUDACION AS RE ON RE.OIDDOCUMENTOCC = FA.OIDDOCUMENTOCC  " +
                    "where FA.NUMEROFACTURA IS NOT NULL AND CAST(FA.FECHACREA AS date) = '" + fecha + "'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
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
                        tbFacturasP5 oSolicitud = new tbFacturasP5();
                        oSolicitud.OIDFACTURA = Int32.Parse(dr["OID"].ToString());
                        oSolicitud.NOMBRECLIENTE = dr["NOMBRECLIENTE"].ToString();
                        oSolicitud.CEDULA_RUC = dr["CEDULA_RUC"].ToString();
                        string NumFac = dr["NUMEROFACTURA"].ToString();
                        if (NumFac != "")
                        {
                            oSolicitud.NUMEROFACTURA = Int32.Parse(dr["NUMEROFACTURA"].ToString());
                        }
                        else
                        {
                            oSolicitud.NUMEROFACTURA = 0;
                        }
                        string FechaCrea= dr["FECHA"].ToString();
                        string Fechaf = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy");
                        oSolicitud.FECHACREA = Fechaf;
                        string valor = dr["VALORFACTURA"].ToString();
                        if (valor != null)
                        {
                            oSolicitud.VALORFACTURA = decimal.Parse(dr["VALORFACTURA"].ToString());
                            oSolicitud.VALORFACTURA = oSolicitud.VALORFACTURA / 100;
                        }
                        else
                        {
                            oSolicitud.VALORFACTURA = 0;
                        }
                        string estado = dr["ESTADO"].ToString();
                        if (estado=="P")
                        {
                            oSolicitud.ESTADO = "PROCESADO";
                        }
                        if (estado == "S")
                        {
                            oSolicitud.ESTADO = "PRE-FACTURADO";
                        }


                        oSolicitud.CODIGO= dr["CODIGO"].ToString();
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                        oSolicitud.FECHACREACION = dr["FECHACREA"].ToString();

                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
               // throw ex;
            }
            return listarSolicitud;
        }

        public tbFacturasP5 DetalleFacturasRucP5(Int32 OidFactura)//(string canio, string cdireccion, string tipoSolicitud)
        {
            //List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            tbFacturasP5 oSolicitud = new tbFacturasP5();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FA.OID, FA.OIDDOCUMENTOCC,NOMBRECLIENTE, FA.CEDULA_RUC,FA.NUMEROFACTURA,CAST(FA.FECHA AS date) AS FECHA,FA.VALORFACTURA,FA.ESTADO,RE.CODIGO,FA.USUARIOCREA,FA.FECHACREA " +
                    "FROM FACTURA AS FA  LEFT JOIN RECAUDACION AS RE ON RE.OIDDOCUMENTOCC = FA.OIDDOCUMENTOCC " +
                    "where FA.OID=" + OidFactura + "");

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


                        oSolicitud.NOMBRECLIENTE = dr["NOMBRECLIENTE"].ToString();
                        oSolicitud.CEDULA_RUC = dr["CEDULA_RUC"].ToString();
                        oSolicitud.NUMEROFACTURA = Int32.Parse(dr["NUMEROFACTURA"].ToString());
                        string FechaCrea = dr["FECHA"].ToString();
                        string Fechaf = Convert.ToDateTime(FechaCrea).ToString("dd/MM/yyyy");
                        oSolicitud.FECHACREA = Fechaf;
                        oSolicitud.VALORFACTURA = decimal.Parse(dr["VALORFACTURA"].ToString());
                        oSolicitud.VALORFACTURA = oSolicitud.VALORFACTURA / 100;
                        string estado = dr["ESTADO"].ToString();
                        if (estado == "P")
                        {
                            oSolicitud.ESTADO = "PROCESADO";
                        }
                        if (estado == "S")
                        {
                            oSolicitud.ESTADO = "PRE-FACTURADO";
                        }
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                        oSolicitud.FECHACREACION = dr["FECHACREA"].ToString();

                        oSolicitud.OIDFACTURA = Int32.Parse(dr["OID"].ToString());
                        oSolicitud.OIDDOCUMENTOCC = Int32.Parse(dr["OIDDOCUMENTOCC"].ToString());
                        oSolicitud.CODIGO = dr["CODIGO"].ToString();
                        //LLENA DETALEL FACTURA
                        oSolicitud.oDetalleFactura = CD_FacturasDetalleP5.Instancia.DetalleFacturasDTP5(oSolicitud.OIDFACTURA);
                        //LLENA DETALLE DE RECAUDACION
                        oSolicitud.oDetalleRecaudacion = CD_FacturasDetalleP5.Instancia.DetalleRecaudacionDTP5(oSolicitud.OIDDOCUMENTOCC);
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
        //OBTIENE NUMERO DE DEPOSITO DE LA FACTURA RE
        public tbFacturasP5 DetalleDepositoFacturasP5(Int32 NumeroFactura)//(string canio, string cdireccion, string tipoSolicitud)
        {
            //List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            tbFacturasP5 oSolicitud = new tbFacturasP5();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT NUMERO,FEC_REC FROM V_REC03  where NUMFAC= '" + NumeroFactura + "'");

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


                        oSolicitud.NUMERODEPOSITO = dr["NUMERO"].ToString();
                        oSolicitud.FECHARECAUDACION = dr["FEC_REC"].ToString();
                        
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
        public tbFacturasP5 DetalleFacturaP5Confiar(Int32 NumeroFactura)//(string canio, string cdireccion, string tipoSolicitud)
        {
            //List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            tbFacturasP5 oSolicitud = new tbFacturasP5();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT EP.pathPDF,EP.NroCbte FROM Comprobante AS C JOIN EstadoPublicacion AS EP ON C.NroCbte = EP.NroCbte and C.Resultado = 'A'" +
                    " AND  EP.NroCbte = " + NumeroFactura + "");

                query = sbSol.ToString();
                //SqlConnection cn = new SqlConnection(ConexionSql.Conexion);

                //cn.Open();
                SqlCommand cmd;

                //OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion);

                // iDB2Command cmd;


                // using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                using (SqlConnection oConexion = new SqlConnection(ConexionServerConfiar.CadenaConexion))
                {
                    cmd = new SqlCommand(query, oConexion);
                    //cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();

                    // iDB2DataReader dr = cmd.ExecuteReader();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // , ,,,,


                        oSolicitud.RESULTADO = dr["pathPDF"].ToString();
                        //oSolicitud.AUTORIZACION = dr["AUTORIZACION"].ToString();
                        oSolicitud.NUMEROFACTURA = Int32.Parse(dr["NroCbte"].ToString());
                        
                    }
                    dr.Close();
                    oConexion.Close();
                }

                //string url = oSolicitud.RESULTADO;
                //url = url.Substring(url, 0, 2);
                ////  #region copiamanual
                //string RutaInicial = @"\\172.25.9.55\RECORD\";
                //string RutaFinal = @"C:\temporal\";
                ////RutaFinal = @"D:\proyectos\DGAC\informacion billing\RECORD\";
                //// string NombreArchivo = ".RECORD." + año + "." + mes + "." + dia;

                //try
                //{

                //    if (System.IO.File.Exists(RutaInicial + NombreArchivo))//Se verifica si existe
                //    {
                //        if (System.IO.File.Exists(RutaFinal + NombreArchivo))
                //        {
                //            File.Delete(RutaFinal + NombreArchivo);
                //            System.IO.File.Copy(RutaInicial + NombreArchivo, RutaFinal + NombreArchivo, true);
                //        }
                //        else
                //        {
                //            System.IO.File.Copy(RutaInicial + NombreArchivo, RutaFinal + NombreArchivo, true);
                //        }

                //    }
                //    else
                //    {
                //        Console.WriteLine("error no se puede copiar archivo ");
                //        NombreArchivo = "";
                //    }


                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}




            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oSolicitud;
        }


        //detalle de facturas por cliente

        public List<tbFacturasP5> DetalleFacturasP5Cliente(String Cliente)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OID, NOMBRECLIENTE, CEDULA_RUC,NUMEROFACTURA,FECHA,VALORFACTURA,ESTADO,USUARIOCREA,FECHACREA  " +
                    "FROM FACTURA where NUMEROFACTURA IS NOT NULL AND  NOMBRECLIENTE Like ('" + Cliente + "%') ORDER BY FECHA DESC ");
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
                        // , ,,,,
                        tbFacturasP5 oSolicitud = new tbFacturasP5();
                        oSolicitud.OIDFACTURA = Int32.Parse(dr["OID"].ToString());
                        oSolicitud.NOMBRECLIENTE = dr["NOMBRECLIENTE"].ToString();
                        oSolicitud.CEDULA_RUC = dr["CEDULA_RUC"].ToString();
                        string NumFac = dr["NUMEROFACTURA"].ToString();
                        if (NumFac != "")
                        {
                            oSolicitud.NUMEROFACTURA = Int32.Parse(dr["NUMEROFACTURA"].ToString());
                        }
                        else
                        {
                            oSolicitud.NUMEROFACTURA = 0;
                        }

                        string FechaCrea = dr["FECHA"].ToString();
                        string Fechaf = Convert.ToDateTime(FechaCrea).ToString("dd/MM/yyyy");
                        oSolicitud.FECHACREA = Fechaf;

                        //oSolicitud.FECHACREA = DateTime.Parse(dr["FECHA"].ToString());
                        string valor = dr["VALORFACTURA"].ToString();
                        if (valor != null)
                        {
                            oSolicitud.VALORFACTURA = decimal.Parse(dr["VALORFACTURA"].ToString());
                            oSolicitud.VALORFACTURA = oSolicitud.VALORFACTURA / 100;
                        }
                        else
                        {
                            oSolicitud.VALORFACTURA = 0;
                        }
                        
                        string estado = dr["ESTADO"].ToString();
                        if (estado == "P")
                        {
                            oSolicitud.ESTADO = "PROCESADO";
                        }
                        if (estado == "S")
                        {
                            oSolicitud.ESTADO = "PRE-FACTURADO";
                        }
                        oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                        oSolicitud.FECHACREACION = dr["FECHACREA"].ToString();

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
        //NUMERO DE FACTURA MAS CLIENTE
        public List<tbFacturasP5> DetalleFacturaCliente(String Cliente)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbFacturasP5> listarSolicitud = new List<tbFacturasP5>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OID, NOMBRECLIENTE, CEDULA_RUC,NUMEROFACTURA,FECHA,VALORFACTURA,ESTADO,USUARIOCREA,FECHACREA  " +
                    "FROM FACTURA where NUMEROFACTURA =" + Cliente + " ");
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
                        // , ,,,,
                        tbFacturasP5 oSolicitud = new tbFacturasP5();
                        oSolicitud.OIDFACTURA = Int32.Parse(dr["OID"].ToString());
                        oSolicitud.NOMBRECLIENTE = dr["NOMBRECLIENTE"].ToString();
                        oSolicitud.CEDULA_RUC = dr["CEDULA_RUC"].ToString();
                        string NumFac = dr["NUMEROFACTURA"].ToString();
                        if (NumFac != "")
                        {
                            oSolicitud.NUMEROFACTURA = Int32.Parse(dr["NUMEROFACTURA"].ToString());
                        }
                        else
                        {
                            oSolicitud.NUMEROFACTURA = 0;
                        }

                        string FechaCrea = dr["FECHA"].ToString();
                        
                        string Fechaf = Convert.ToDateTime(FechaCrea).ToString("dd/MM/yyyy");
                        oSolicitud.FECHACREA = Fechaf;

                        //oSolicitud.FECHACREA = DateTime.Parse(dr["FECHA"].ToString());
                        string valor = dr["VALORFACTURA"].ToString();
                        if (valor != null)
                        {
                            oSolicitud.VALORFACTURA = decimal.Parse(dr["VALORFACTURA"].ToString());
                            oSolicitud.VALORFACTURA = oSolicitud.VALORFACTURA / 100;
                        }
                        else
                        {
                            oSolicitud.VALORFACTURA = 0;
                        }

                        string estado = dr["ESTADO"].ToString();
                        if (estado == "P")
                        {
                            oSolicitud.ESTADO = "PROCESADO";
                        }
                        if (estado == "S")
                        {
                            oSolicitud.ESTADO = "PRE-FACTURADO";
                        }
                        oSolicitud.USUARIOCREA= dr["USUARIOCREA"].ToString();
                        oSolicitud.FECHACREACION = dr["FECHACREA"].ToString();
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
