using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_SolicitudPOA
    {
        public static CD_SolicitudPOA _instancia = null;
        private CD_SolicitudPOA()
        {

        }

        public static CD_SolicitudPOA Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_SolicitudPOA();
                }
                return _instancia;
            }
        }

        public List<tbSolicitudPOA> SolicitudModificacionReprogramacionSoloPOA(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbSolicitudPOA> listarSolicitud = new List<tbSolicitudPOA>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT ifnull(rtrim(ltrim(SOLAN1)), '') as AnioSolicitud, ifnull(SOLNU3, 0) as NumeroSolicitud, ifnull(rtrim(ltrim(SOLFE6)), '') as FechaSolicitud, ifnull(rtrim(ltrim(SOLTIP)), '') as TipoSolicitud, ifnull(rtrim(ltrim(SOLES6)), '') AS EstadoSolicitud, ");
                sbSol.Append(" ifnull(rtrim(ltrim(SOLFE9)), '') as FechaRevision,  ifnull(rtrim(ltrim(SOLES7)), '') as EstadoAutorizacion, ifnull(rtrim(ltrim(SOLF01)), '') as FechaAprobacion, ifnull(SOLNU6, 0) as NumeroModificacion ");
                sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSolicitudPOA oSolicitud = new tbSolicitudPOA();
                        oSolicitud.AnioSolicitud = dr["AnioSolicitud"].ToString();
                        oSolicitud.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitud.FechaSolicitud = dr["FechaSolicitud"].ToString();
                        oSolicitud.TipoSolicitud = dr["TipoSolicitud"].ToString();
                        oSolicitud.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitud.FechaRevision = dr["FechaRevision"].ToString();
                        oSolicitud.EstadoAutorizacion = dr["EstadoAutorizacion"].ToString();
                        oSolicitud.FechaAprobacion = dr["FechaAprobacion"].ToString();
                        oSolicitud.NumeroModificacion = decimal.Parse(dr["NumeroModificacion"].ToString());
                        listarSolicitud.Add(oSolicitud);
                    }
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
