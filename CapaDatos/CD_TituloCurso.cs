using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TituloCurso
    {
        public static CD_TituloCurso _instancia = null;
        private CD_TituloCurso()
        {

        }

        public static CD_TituloCurso Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_TituloCurso();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Obtine todos los titulos
        /// </summary>
        /// <returns></returns>
        public List<tbTituloCurso> ObtieneTodosTitulosCurso()
        {
            List<tbTituloCurso> ListarTitulosCurso = new List<tbTituloCurso>();
            string sqlQuery = "";
            StringBuilder sbPais = new StringBuilder();
            sqlQuery = sqlQuery.ToString();
            iDB2Command cmd;
            try
            {
                sbPais.Append(" SELECT  ifnull(rtrim(ltrim(TITCOD)), '') AS CodigoTitulo , ifnull(rtrim(ltrim(TITDES)), '') as DescripcionTitulo ");
                sbPais.Append(" FROM TITARC ORDER BY TITDES ASC");
                sqlQuery = sbPais.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbTituloCurso oTituloCurso = new tbTituloCurso();
                        oTituloCurso.CodigoTitulo = dr["CodigoTitulo"].ToString();
                        oTituloCurso.DescripcionTitulo = dr["DescripcionTitulo"].ToString();

                        ListarTitulosCurso.Add(oTituloCurso);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ListarTitulosCurso;
        }

    }
}
