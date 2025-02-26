using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;
namespace CapaDatos
{
  public  class CD_HorarioAtencion
    {
        public static CD_HorarioAtencion _instancia = null;

        private CD_HorarioAtencion()
        {

        }

        public static CD_HorarioAtencion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_HorarioAtencion();
                }
                return _instancia;
            }
        }

        public tbHorioAtencion ObtenerHorarioAtencionPorUsuario(string codUsuario)
        {
            tbHorioAtencion oHorarioAtencion = null;
            string query = "SELECT ifnull(rtrim(ltrim(HORCOD)), '') as CodigoUsuario,  ifnull(rtrim(ltrim(HORHOR)), '') AS HorarioInicio, ifnull(rtrim(ltrim(HORHO1)), '') AS HorarioFin "
                            + " FROM HORARC WHERE HORCOD = '" + codUsuario + "' AND HOREST = 'AC'";

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
                        oHorarioAtencion = new tbHorioAtencion();
                        oHorarioAtencion.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oHorarioAtencion.HorarioInicio = dr["HorarioInicio"].ToString();
                        oHorarioAtencion.HorarioFin = dr["HorarioFin"].ToString();
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oHorarioAtencion = null;
            }
            return oHorarioAtencion;
        }

        public bool VerificarHorarioAtencionPorUsuario(string codUsuario)
        {

            bool estadoActivo = false;
            string query = "SELECT ifnull(rtrim(ltrim(HORCOD)), '') as CodigoUsuario,  ifnull(rtrim(ltrim(HORHOR)), '') AS HorarioInicio, ifnull(rtrim(ltrim(HORHO1)), '') AS HorarioFin "
                            + " FROM HORARC WHERE HORCOD = '" + codUsuario + "' AND HOREST = 'AC'";

            IBM.Data.DB2.iSeries.iDB2Command cmd;
            try
            {
                using (IBM.Data.DB2.iSeries.iDB2Connection oConexion = new IBM.Data.DB2.iSeries.iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        estadoActivo = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                estadoActivo = false;
            }
            return estadoActivo;
        }

    }
}
