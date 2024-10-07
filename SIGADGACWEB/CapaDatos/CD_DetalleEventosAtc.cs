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
    public class CD_DetalleEventosAtc
    {
        public static CD_DetalleEventosAtc _instancia = null;
        private CD_DetalleEventosAtc()
        {

        }

        public static CD_DetalleEventosAtc Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleEventosAtc();
                }
                return _instancia;
            }
        }

        public List<tbDetalleEventosAtc> DetalleEventosAtc(string Lugar, string Dependencia, string Fechaelab, string Turno)
        {
            List<tbDetalleEventosAtc> listarSolicitud = new List<tbDetalleEventosAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select * FROM OPEAR1 WHERE OPELUG = '" + Lugar + "' AND OPEDEP='" + Dependencia + "' AND OPEFEC ='" + Fechaelab + "' AND OPETUR='" + Turno + "' ORDER BY OPEHO2  ");


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
                        tbDetalleEventosAtc oSolicitud = new tbDetalleEventosAtc();

                        oSolicitud.CODIGOEVENTO = dr["OPEEVE"].ToString().Trim();
                        oSolicitud.HORAUTC = dr["OPEHO2"].ToString().Trim();
                        oSolicitud.DESCRIPCION = dr["OPERE2"].ToString().Trim() + dr["OPERE3"].ToString().Trim() + dr["OPERE4"].ToString().Trim() +
                        dr["OPERE5"].ToString().Trim() + dr["OPERE6"].ToString().Trim();
                        oSolicitud.USUARIO = dr["OPEUS2"].ToString()+ dr["OPEHO3"].ToString();
                        //oSolicitud.DESCRIPCION = dr["OPNDES"].ToString(); 
                        //oSolicitud.DESCRIPCION1 = dr["OPNDE1"].ToString();
                        //oSolicitud.DESCRIPCION2 = dr["OPNDE2"].ToString();
                        //oSolicitud.DESCRIPCION3 = dr["OPNDE3"].ToString();
                        //oSolicitud.DESCRIPCION4 = dr["OPNDE4"].ToString();

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
