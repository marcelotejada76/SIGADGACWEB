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
    public class CD_DetalleHistoricoCursosAtc
    {
        public static CD_DetalleHistoricoCursosAtc _instancia = null;
        private CD_DetalleHistoricoCursosAtc()
        {

        }

        public static CD_DetalleHistoricoCursosAtc Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleHistoricoCursosAtc();
                }
                return _instancia;
            }
        }

        public List<tbHistoricoDetalleCursoAtc> HistoricoDetalleCursosAtc(string Licencia)
        {
            List<tbHistoricoDetalleCursoAtc> listarSolicitud = new List<tbHistoricoDetalleCursoAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select * FROM OPHAR2 WHERE OPHLI2 = '" + Licencia + "' ORDER BY OPHSE1  ");


                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {

                        tbHistoricoDetalleCursoAtc oSolicitud = new tbHistoricoDetalleCursoAtc();


                        oSolicitud.Nombre_Curso = dr["OPHN10"].ToString().Trim();
                        oSolicitud.Centro_Capacitación = dr["OPHC35"].ToString().Trim();
                        oSolicitud.Ciudad = dr["OPHC36"].ToString().Trim();
                        oSolicitud.Fecha_Desde = dr["OPHFE4"].ToString().Trim();

                        oSolicitud.Fecha_Hasta = dr["OPHFE5"].ToString().Trim();
                        oSolicitud.Titulo = dr["OPHTI4"].ToString().Trim();
                        oSolicitud.Certificado = dr["OPHC37"].ToString().Trim();
                        oSolicitud.Duracion = dr["OPHDUR"].ToString().Trim();

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
