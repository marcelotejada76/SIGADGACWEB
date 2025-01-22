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
    public class CD_DetalleComprobanteDeposito
    {
        public static CD_DetalleComprobanteDeposito _instancia = null;
        private CD_DetalleComprobanteDeposito()
        {

        }

        public static CD_DetalleComprobanteDeposito Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleComprobanteDeposito();
                }
                return _instancia;
            }
        }

        public List<tbDetalleComprobantesDeposito> DetalleComprobantesDeposito(string FechaEmision, string NumeroComprobante)
        {
            List<tbDetalleComprobantesDeposito> listarSolicitud = new List<tbDetalleComprobantesDeposito>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select FIDFEC AS FECHADEPOSITO,FIDNUM AS NUMERODEPOSITO,FIDSEC AS SECUENCIA,FIDCOD AS CODIGORECAUDACION," +
                    "FIDCLI AS CLIENTE,FIDFAC AS FACTURA,FIDVAL AS VALOR, FIDCTA AS CTACONTABLE, FIDDES AS DESCCTA, FIDLUG AS LUGAR, " +
                    "FIDTI1 AS TIPO FROM FIdarc  WHERE fidfec='"+FechaEmision+"' and FIDNUM='"+NumeroComprobante+"'");
                
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    decimal Valor = 0;
                    while (dr.Read())
                    {
                        // , ,,,,
                        tbDetalleComprobantesDeposito oSolicitud = new tbDetalleComprobantesDeposito();

                        oSolicitud.FECHAPROCESO = dr["FECHADEPOSITO"].ToString();
                        oSolicitud.NUMEROCOMPROBANTE = dr["NUMERODEPOSITO"].ToString();
                        oSolicitud.secuencia = int.Parse(dr["SECUENCIA"].ToString());
                        oSolicitud.MONTO = decimal.Parse(dr["VALOR"].ToString());
                        //oSolicitud.MONTO = oSolicitud.MONTO / 100;
                        oSolicitud.CODIGORECAUDACION = dr["CODIGORECAUDACION"].ToString();
                        oSolicitud.CLIENTE =dr["CLIENTE"].ToString();
                        oSolicitud.NUMEROFACTURA = dr["FACTURA"].ToString();
                        oSolicitud.CTACONTABLE = dr["CTACONTABLE"].ToString();
                        oSolicitud.DESCCUENTA = dr["DESCCTA"].ToString();
                        oSolicitud.LUGAR = dr["LUGAR"].ToString();
                        oSolicitud.TIPO = dr["TIPO"].ToString();
                        
                        Valor=Valor+ oSolicitud.MONTO;
                        oSolicitud.MONTOGENERAL = Valor;
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
        

    }
}
