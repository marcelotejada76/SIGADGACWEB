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

       
    }
}
