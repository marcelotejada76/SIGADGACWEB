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
    public class CD_DetalleNotamsAtc
    {
        public static CD_DetalleNotamsAtc _instancia = null;
        private CD_DetalleNotamsAtc()
        {

        }

        public static CD_DetalleNotamsAtc Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleNotamsAtc();
                }
                return _instancia;
            }
        }

        public List<tbDetalleNotamsAtc> DetalleNotamsAtc(string Lugar, string Dependencia, string Fechaelab, string Turno)
        {
            List<tbDetalleNotamsAtc> listarSolicitud = new List<tbDetalleNotamsAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select * FROM OPNARC WHERE OPNLUG = '" + Lugar + "' AND OPNDEP='" + Dependencia + "' AND OPNFEC ='" + Fechaelab + "' AND OPNTUR='" + Turno + "' ");


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
                        tbDetalleNotamsAtc oSolicitud = new tbDetalleNotamsAtc();

                        oSolicitud.CODIGONOTAMS = dr["OPNCOD"].ToString().Trim();
                       // oSolicitud.DESCRIPCION = dr["OPNDES"].ToString()+ dr["OPNDE1"].ToString()+dr["OPNDE2"].ToString()+dr["OPNDE3"].ToString()+dr["OPNDE4"].ToString();
                        oSolicitud.DESCRIPCION = dr["OPNDES"].ToString().Trim(); 
                        oSolicitud.DESCRIPCION1 = dr["OPNDE1"].ToString().Trim();
                        oSolicitud.DESCRIPCION2 = dr["OPNDE2"].ToString().Trim();
                        oSolicitud.DESCRIPCION3 = dr["OPNDE3"].ToString().Trim();
                        oSolicitud.DESCRIPCION4 = dr["OPNDE4"].ToString().Trim();
                        oSolicitud.DESCRIPCION5 = dr["OPNDE5"].ToString().Trim();
                        oSolicitud.DESCRIPCION6 = dr["OPNDE6"].ToString().Trim();
                        oSolicitud.DESCRIPCION7 = dr["OPNDE7"].ToString().Trim();
                        oSolicitud.DESCRIPCION8 = dr["OPNDE8"].ToString().Trim();
                        oSolicitud.DESCRIPCION9 = dr["OPNDE9"].ToString().Trim();
                        oSolicitud.DESCRIPCION10 = dr["OPNDE7"].ToString().Trim();
                        oSolicitud.DESCRIPCION11 = dr["OPNDE8"].ToString().Trim();
                        oSolicitud.DESCRIPCION12 = dr["OPNDE9"].ToString().Trim();

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
