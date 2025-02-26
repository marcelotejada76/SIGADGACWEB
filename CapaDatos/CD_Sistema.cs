using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Sistema
    {
        public static CD_Sistema _instancia = null;

        private CD_Sistema()
        {

        }

        public static CD_Sistema Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Sistema();
                }
                return _instancia;
            }
        }

        public tbSistema GetFechaHoraSistema()
        {
            tbSistema osistema = new tbSistema();

            string query = "select varchar_format(current timestamp, 'YYYYMMDD') as FechaSistema, CHAR(TIME(CURRENT TIMESTAMP),JIS) as HoraSistema "
                    + " from sysibm.sysdummy1";

            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        osistema.FechaSistema = dr["FechaSistema"].ToString();
                        osistema.HoraSistema = dr["HoraSistema"].ToString();
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                osistema = null;
            }
            return osistema;
        }
    }
}
