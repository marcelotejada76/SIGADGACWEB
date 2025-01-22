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
    public class CD_DetalleHistoricoDependenciaAtc
    {
        public static CD_DetalleHistoricoDependenciaAtc _instancia = null;
        private CD_DetalleHistoricoDependenciaAtc()
        {

        }

        public static CD_DetalleHistoricoDependenciaAtc Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleHistoricoDependenciaAtc();
                }
                return _instancia;
            }
        }

        public List<tbHistoricoDetalleDependenciaAtc> HistoricoDetalleDependenciaAtc(string Licencia)
        {
            List<tbHistoricoDetalleDependenciaAtc> listarSolicitud = new List<tbHistoricoDetalleDependenciaAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select * FROM OPHAR1 WHERE OPHLI1 = '" + Licencia + "' ORDER BY OPHSEC  ");


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

                        tbHistoricoDetalleDependenciaAtc oSolicitud = new tbHistoricoDetalleDependenciaAtc();


                        oSolicitud.Dependencia = dr["OPHD15"].ToString().Trim();
                        oSolicitud.Sec = Convert.ToInt32(dr["OPHSEC"].ToString().Trim());
                        oSolicitud.LICENCIACONTROLADOR = dr["OPHLI1"].ToString().Trim();
                        oSolicitud.Fecha_Desde = dr["OPHFE2"].ToString().Trim();

                        oSolicitud.Fecha_Hasta = dr["OPHFE3"].ToString().Trim();

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
