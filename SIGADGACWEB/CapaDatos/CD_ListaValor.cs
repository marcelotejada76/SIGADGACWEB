using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ListaValor
    {
        public static CD_ListaValor _instancia = null;
        //string biblioteca = "DGACDAT";
        private CD_ListaValor()
        {

        }

        public static CD_ListaValor Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ListaValor();
                }
                return _instancia;
            }
        }

        public List<tbListaValor> ListaValores(string campo)
        {
            List<tbListaValor> lstValores = new List<tbListaValor>();
            string query = "SELECT VALVAL, VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = '" + campo + "'";
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
                        tbListaValor oValor = new tbListaValor();
                        oValor.Codigo = dr["VALVAL"].ToString().Trim();
                        oValor.Descripcion = dr["VALDES"].ToString().Trim();
                        lstValores.Add(oValor);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstValores = null;
            }
            return lstValores;
        }

        public tbListaValor ListaValor(string campo, string valor)
        {
            tbListaValor oValor = new tbListaValor();
            string query = "SELECT VALDDS, VALDES FROM DGACSYS.TXDGAC WHERE VALDDS = '" + campo + "' AND VALVAL = '" + valor + "'";
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

                        oValor.Codigo = dr["VALDDS"].ToString().Trim();
                        oValor.Descripcion = dr["VALDES"].ToString().Trim();
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oValor = null;
            }
            return oValor;
        }
    }
}
