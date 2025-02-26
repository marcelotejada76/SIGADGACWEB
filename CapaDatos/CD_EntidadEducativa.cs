using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_EntidadEducativa
    {
        public static CD_EntidadEducativa _instancia = null;
        private CD_EntidadEducativa()
        {

        }

        public static CD_EntidadEducativa Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_EntidadEducativa();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo obtiene lso datos de 
        /// </summary>
        /// <returns></returns>
        public List<tbEntidadEducativa> ObtieneTodasEntidadesEducativas()
        {
            List<tbEntidadEducativa> ListarEntidadEducativa = new List<tbEntidadEducativa>();
            string sqlQuery = "";
            StringBuilder sbPais = new StringBuilder();
            sqlQuery = sqlQuery.ToString();
            iDB2Command cmd;
            try
            {
                sbPais.Append(" SELECT ifnull(rtrim(ltrim(ENTCOD)), '') AS CodigoEntidad , ifnull(rtrim(ltrim(ENTDES)), '') as DescripcionEntidad ");
                sbPais.Append(" FROM ENTAR2 ORDER BY ENTDES ASC");
                sqlQuery = sbPais.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbEntidadEducativa oEntidadEducativa = new tbEntidadEducativa();
                        oEntidadEducativa.CodigoEntidad = dr["CodigoEntidad"].ToString();
                        oEntidadEducativa.DescripcionEntidad = dr["DescripcionEntidad"].ToString();

                        ListarEntidadEducativa.Add(oEntidadEducativa);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ListarEntidadEducativa;
        }

    }
}
