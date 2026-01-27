using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_LiquidacionCoactivas
    {
        public static CD_LiquidacionCoactivas _instancia = null;
        private CD_LiquidacionCoactivas()
        {

        }

        public static CD_LiquidacionCoactivas Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_LiquidacionCoactivas();
                }
                return _instancia;
            }
        }

        public List<tbLiquidacionCoactiva> ListadoLiquidaciones()
        {
            List<tbLiquidacionCoactiva> listarSolicitud = new List<tbLiquidacionCoactiva>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            //DateTime fecha = System.DateTime.Now;
            //string fechc = fecha.ToString("yyyyMMdd"); //fecha del sistema

            //string fechaProceso = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper();

            try
            {
                sbSol.Append("SELECT *from filarc ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbLiquidacionCoactiva oSolicitud = new tbLiquidacionCoactiva();
                        oSolicitud.RUC = dr["FILRUC"].ToString();
                        oSolicitud.PROCEDIMIENTOCOACTIVO = dr["FILPRO"].ToString();


                        oSolicitud.NOMBRECIA = dr["FILNOM"].ToString();
                        oSolicitud.TITULOCREDITO = dr["FILTIT"].ToString();
                        oSolicitud.TIPODOCUMENTO = dr["FILTIP"].ToString();
                        oSolicitud.ELABORADOPOR = dr["FILELA"].ToString();
                        oSolicitud.CARGOELABORADO = dr["FILCAR"].ToString();
                        oSolicitud.REVISADOPOR = dr["FILREV"].ToString();
                        oSolicitud.CARGOREVISADO = dr["FILCA1"].ToString();
                        oSolicitud.APROBADOPOR = dr["FILAPR"].ToString();
                        oSolicitud.CARGOAPROBADO = dr["FILCA2"].ToString();


                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return listarSolicitud;
        }

        ////descarga liquidacion
        
        public string ImprimeLiquidacion(string Ruc, string Procedimiento, string Mensaje)
        {

            var ListadoEmpresasLiquidacion = LiquidacionCoactiva.Liquidacion(Ruc, Procedimiento);
            if (ListadoEmpresasLiquidacion.Count > 0)
            {


                LiquidacionCoactiva.Pdf(ListadoEmpresasLiquidacion);
                Mensaje = "Proceso Finalizado Correctamente";

            }

           
            return Mensaje;

        }

    }
}
