using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ExpedientesPersonales
    {
        public static CD_ExpedientesPersonales _instancia = null;
        private CD_ExpedientesPersonales()
        {

        }

        public static CD_ExpedientesPersonales Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ExpedientesPersonales();
                }
                return _instancia;
            }
        }

        //carga personal
        public List<tbExpedientesPersonales> ListadoExpedientesPersonalesInd()
        {
            List<tbExpedientesPersonales> listarSolicitud = new List<tbExpedientesPersonales>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select *from dgacdatpro.opnar2 where opnco4=6");
                
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbExpedientesPersonales oSolicitud = new tbExpedientesPersonales();
                        oSolicitud.Nombre = dr["OPNNO1"].ToString();
                        oSolicitud.Codigo = Convert.ToInt16(dr["OPNCO4"].ToString());
                        oSolicitud.Secuencia = Convert.ToInt16(dr["OPNCO3"].ToString());
                        
                        listarSolicitud.Add(oSolicitud);
                    }
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
