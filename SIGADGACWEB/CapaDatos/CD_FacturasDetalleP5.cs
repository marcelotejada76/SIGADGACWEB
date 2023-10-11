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
    public class CD_FacturasDetalleP5
    {
        public static CD_FacturasDetalleP5 _instancia = null;
        private CD_FacturasDetalleP5()
        {

        }

        public static CD_FacturasDetalleP5 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_FacturasDetalleP5();
                }
                return _instancia;
            }
        }

        public List<tbFacturasDetalleP5> DetalleFacturasDTP5(Int32 OidFactura)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbFacturasDetalleP5> listarSolicitud = new List<tbFacturasDetalleP5>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT CODIGOCONTABLE, DESCRIPCION,CANTIDAD,VALOR,USUARIOCREA,DESCRIPCIONCUENTA  " +
                    "FROM DETALLEFACTURA WHERE OIDFACTURA = "+OidFactura+" ");
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
                        tbFacturasDetalleP5 oSolicitud = new tbFacturasDetalleP5();

                        oSolicitud.CODIGOCONTABLE = dr["CODIGOCONTABLE"].ToString();
                        oSolicitud.DESCRIPCION = dr["DESCRIPCION"].ToString();
                        oSolicitud.CANTIDAD = int.Parse(dr["CANTIDAD"].ToString());
                        oSolicitud.VALOR = decimal.Parse(dr["VALOR"].ToString());
                        oSolicitud.VALOR = oSolicitud.VALOR / 100;
                        oSolicitud.USUARIOCREA =dr["USUARIOCREA"].ToString();
                        oSolicitud.DESCRIPCIONCUENTA = dr["DESCRIPCIONCUENTA"].ToString();
                        

                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }
        //llena detalle de la recaudacion
        public List<tbRecaudacionDetalleP5> DetalleRecaudacionDTP5(Int32 OidDocumentoCc)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbRecaudacionDetalleP5> listarSolicitud = new List<tbRecaudacionDetalleP5>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM RECAUDACION WHERE OIDDOCUMENTOCC = " + OidDocumentoCc + " order by FECHACREA ");
                
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
                        tbRecaudacionDetalleP5 oSolicitud = new tbRecaudacionDetalleP5();

                        string FechaCrea = dr["FECHARECAUDACION"].ToString();
                        if (FechaCrea !="")
                        {
                            string Fechaf = Convert.ToDateTime(FechaCrea).ToString("dd/MM/yyyy");
                            oSolicitud.FECHARECAUDACION = Fechaf;

                        }

                        FechaCrea = dr["FECHAANULACION"].ToString();
                        if (FechaCrea != "")
                        {

                         string  Fechaf = Convert.ToDateTime(FechaCrea).ToString("dd/MM/yyyy");
                            oSolicitud.FECHAANULACION = Fechaf;
                        }
                        
                        oSolicitud.TIPO = dr["TIPO"].ToString();
                        oSolicitud.CODIGO = dr["CODIGO"].ToString();
                        oSolicitud.CONCEPTO = dr["CONCEPTO"].ToString();
                        oSolicitud.VALORRECAUDADO = decimal.Parse(dr["VALORRECAUDADO"].ToString());
                        if (oSolicitud.VALORRECAUDADO >0)
                        {
                            oSolicitud.VALORRECAUDADO = oSolicitud.VALORRECAUDADO / 100;
                        }

                        oSolicitud.VALOREFECTIVO = decimal.Parse(dr["VALOREFECTIVO"].ToString());
                        if (oSolicitud.VALOREFECTIVO > 0)
                        {
                            oSolicitud.VALOREFECTIVO = oSolicitud.VALOREFECTIVO / 100;
                        }

                        oSolicitud.VALORCHEQUE = decimal.Parse(dr["VALORCHEQUE"].ToString());
                        if (oSolicitud.VALORCHEQUE > 0)
                        {
                            oSolicitud.VALORCHEQUE = oSolicitud.VALORCHEQUE / 100;
                        }
                        oSolicitud.VALORABONO = decimal.Parse(dr["VALORABONO"].ToString());
                        if (oSolicitud.VALORABONO > 0)
                        {
                            oSolicitud.VALORABONO = oSolicitud.VALORABONO / 100;
                        }
                        oSolicitud.VALORNOTACREDITO = decimal.Parse(dr["VALORNOTACREDITO"].ToString());
                        if (oSolicitud.VALORNOTACREDITO > 0)
                        {
                            oSolicitud.VALORNOTACREDITO = oSolicitud.VALORNOTACREDITO / 100;
                        }
                        oSolicitud.VALORDEPOSITO = decimal.Parse(dr["VALORDEPOSITO"].ToString());
                        if (oSolicitud.VALORDEPOSITO > 0)
                        {
                            oSolicitud.VALORDEPOSITO = oSolicitud.VALORDEPOSITO / 100;
                        }

                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
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
