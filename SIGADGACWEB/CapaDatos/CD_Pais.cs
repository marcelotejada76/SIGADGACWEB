using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Pais
    {
        public static CD_Pais _instancia = null;
        private CD_Pais()
        {

        }

        public static CD_Pais Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Pais();
                }
                return _instancia;
            }
        }

        public List<tbPais> ObtieneTodosPais()
        {
            List<tbPais> Listarpais = new List<tbPais>();
            string sqlQuery = "";
            StringBuilder sbPais = new StringBuilder();
            sqlQuery = sqlQuery.ToString();
            iDB2Command cmd;
            try
            {
                sbPais.Append(" SELECT  ifnull(rtrim(ltrim(OPPCOD)), '') AS CodigoPais , ifnull(rtrim(ltrim(OPPDES)), '') as DescripcionPais   ");
                sbPais.Append(" FROM OPPAR1 ORDER BY OPPCOD  ASC");
                sqlQuery = sbPais.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbPais oPais = new tbPais();
                        oPais.CodigoPais = dr["CodigoPais"].ToString();
                        oPais.DescripcionPais = dr["DescripcionPais"].ToString();

                        Listarpais.Add(oPais);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Listarpais;
        }

        public tbPais ObtienePaisPorCodigo(string codigoPais)
        {
            tbPais oPais = new tbPais();
            string sqlQuery = "";
            StringBuilder sbPais = new StringBuilder();
            iDB2Command cmd;
            try
            {
                sbPais.Append(" SELECT  ifnull(rtrim(ltrim(OPPCOD)), '') AS CodigoPais , ifnull(rtrim(ltrim(OPPDES)), '') as DescripcionPais   ");
                sbPais.Append(" FROM OPPAR1 WHERE OPPCOD = '" + codigoPais + "'");
                sqlQuery = sbPais.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        oPais.CodigoPais = dr["CodigoPais"].ToString();
                        oPais.DescripcionPais = dr["DescripcionPais"].ToString();
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return oPais;
        }

        /// <summary>
        /// Metodo Provincias
        /// </summary>
        /// <param name="codigoPais"></param>
        /// <returns>List</returns>
        public List<tbProvincia> ObtieneTodosProvincias(string codigoPais)
        {
            List<tbProvincia> listarProvincia = new List<tbProvincia>();
            string sqlQuery = "";
            StringBuilder sbProvincia = new StringBuilder();
            iDB2Command cmd;
            try
            {
                sbProvincia.Append(" SELECT ifnull(rtrim(ltrim(OPPCO3)), '') as CodigoPais, ifnull(rtrim(ltrim(OPPCO2)), '') as CodigoProvincia, ifnull(rtrim(ltrim(OPPPRO)), '') as DescripcionProvincia ");
                sbProvincia.Append(" FROM OPPAR2 WHERE OPPCO3 = 'SE'  ORDER BY OPPCO2  ASC");
                sqlQuery = sbProvincia.ToString();

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbProvincia oProvincia = new tbProvincia();
                        oProvincia.CodigoPais = dr["CodigoPais"].ToString();
                        oProvincia.CodigoProvincia = dr["CodigoProvincia"].ToString();
                        oProvincia.DescripcionProvincia = dr["DescripcionProvincia"].ToString();

                        listarProvincia.Add(oProvincia);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return listarProvincia;
        }

        /// <summary>
        /// Metodo obtiene todas las ciuades
        /// </summary>
        /// <param name="codigoPais"></param>
        /// <returns>List</returns>
        public List<tbCiudad> ObtieneTodosCiudad(string codigoPais)
        {
            List<tbCiudad> listarCiudades = new List<tbCiudad>();
            string sqlQuery = "";
            StringBuilder sbCiudad = new StringBuilder();
            iDB2Command cmd;
            try
            {
                sbCiudad.Append(" SELECT ifnull(rtrim(ltrim(OPCCO4)), '') as CodigoCiudad, ifnull(rtrim(ltrim(OPCDES)), '') as DescripcionCiudad, ifnull(rtrim(ltrim(OPCCO3)), '') as CodigoPais, ifnull(rtrim(ltrim(OPCCO6)), '') as CodigoProvincia, ifnull(rtrim(ltrim(OPCC04)), '') as CodigoCanton ");
                sbCiudad.Append(" FROM OPCAR1 WHERE OPCCO3 = '" + codigoPais + "' ORDER BY OPCDES  ASC");
                sqlQuery = sbCiudad.ToString();

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCiudad oCiudad = new tbCiudad();
                        oCiudad.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        oCiudad.DescripcionCiudad = dr["DescripcionCiudad"].ToString();
                        oCiudad.CodigoPais = dr["CodigoPais"].ToString();
                        oCiudad.CodigoProvincia = dr["CodigoProvincia"].ToString();
                        oCiudad.CodigoCanton = dr["CodigoCanton"].ToString();

                        listarCiudades.Add(oCiudad);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return listarCiudades;
        }

        /// <summary>
        /// Metodo obtiene todas las Cantones
        /// </summary>
        /// <param name="codigoPais"></param>
        /// <returns>List</returns>
        public List<tbCanton> ObtieneTodosCantones(string codigoPais, string codigoProvincia)
        {
            List<tbCanton> listarCantones = new List<tbCanton>();
            string sqlQuery = "";
            StringBuilder sbCiudad = new StringBuilder();
            iDB2Command cmd;
            try
            {
                sbCiudad.Append(" SELECT ifnull(rtrim(ltrim(OPCC02)), '') AS CodigoPais, ifnull(rtrim(ltrim(OPCC03)), '') as CodigoProvincia, ifnull(rtrim(ltrim(OPCC01)), '') as CodigoCanton, ifnull(rtrim(ltrim(OPCNO3)), '') as DescripcionCanton");
                if (codigoPais.Length > 0 && codigoProvincia.Length > 0)
                    sbCiudad.Append(" FROM OPCAR4 WHERE OPCES2 = 'AC' AND OPCC02 = '" + codigoPais + "' AND OPCC03 = '" + codigoProvincia + "' ORDER BY OPCNO3  ASC");
                else
                    sbCiudad.Append(" FROM OPCAR4 WHERE OPCES2 = 'AC' AND OPCC02 = '" + codigoPais + "' ORDER BY OPCNO3  ASC");
                sqlQuery = sbCiudad.ToString();

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCanton oCanton = new tbCanton();
                        oCanton.CodigoPais = dr["CodigoPais"].ToString();
                        oCanton.CodigoProvincia = dr["CodigoProvincia"].ToString();
                        oCanton.CodigoCanton = dr["CodigoCanton"].ToString();
                        oCanton.DescripcionCanton = dr["DescripcionCanton"].ToString();

                        listarCantones.Add(oCanton);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return listarCantones;
        }

        /// <summary>
        /// Metodo obtiene todas las Parroquias
        /// </summary>
        /// <param name="codigoPais"></param>
        /// <returns>List</returns>
        public List<tbParroquia> ObtieneTodosParroquias(string codigoPais, string codigoProvincia, string CodigoCanton)
        {
            List<tbParroquia> listarParroquias = new List<tbParroquia>();
            string sqlQuery = "";
            StringBuilder sbParroquia = new StringBuilder();
            iDB2Command cmd;
            try
            {
                sbParroquia.Append("SELECT ifnull(rtrim(ltrim(OPPCO7)), '') AS CodigoPais, ifnull(rtrim(ltrim(OPPCO8)), '') as CodigoProvincia, ifnull(rtrim(ltrim(OPPCO9)), '') as CodigoCanton, ifnull(rtrim(ltrim(OPPCO6)), '') as CodigoParroquia, ifnull(rtrim(ltrim(OPPNOM)), '') as DescripcionParroquia FROM OPPAR4 ");
                if (codigoPais.Length > 0 && codigoProvincia.Length > 0 && CodigoCanton.Length > 0)
                    sbParroquia.Append(" WHERE OPPEST = 'AC' AND OPPCO7 = '" + codigoPais + "' AND OPPCO8 = '" + codigoProvincia + "' AND OPPCO9 = '" + CodigoCanton + "' ORDER BY OPPNOM  ASC");
                else
                    sbParroquia.Append(" WHERE OPPEST = 'AC' AND OPPCO7 = '" + codigoPais + "' ORDER BY OPPNOM  ASC");
                sqlQuery = sbParroquia.ToString();

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbParroquia oParroquia = new tbParroquia();
                        oParroquia.CodigoPais = dr["CodigoPais"].ToString();
                        oParroquia.CodigoProvincia = dr["CodigoProvincia"].ToString();
                        oParroquia.CodigoCanton = dr["CodigoCanton"].ToString();
                        oParroquia.CodigoParroquia = dr["CodigoParroquia"].ToString();
                        oParroquia.DescripcionParroquia = dr["DescripcionParroquia"].ToString();

                        listarParroquias.Add(oParroquia);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return listarParroquias;
        }
    }
}
