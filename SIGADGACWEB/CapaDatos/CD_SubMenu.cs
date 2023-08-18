using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_SubMenu
    {
        public static CD_SubMenu _instancia = null;        
        private CD_SubMenu()
        {

        }

        public static CD_SubMenu Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_SubMenu();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo obtiene el Submenu
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns></returns>
        public List<tbSubMenu> GetSubMenuPorCodigo(string codigoUsuario, string codigoMenu)
        {
            List<tbSubMenu> listarSubMenu = new List<tbSubMenu>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;

            sb.Append("SELECT ifnull(rtrim(ltrim(SUBC10)), '') AS CodigoSubsistema, ifnull(rtrim(ltrim(SUBC11)), '') AS CodigoGestion, ifnull(rtrim(ltrim(SUBC12)), '') AS CodigoModulo, ifnull(rtrim(ltrim(SUBC13)), '') AS CodigoRol,");
            sb.Append(" ifnull(rtrim(ltrim(SUBC14)), '') AS CodigoMenu, ifnull(SUBID1, 0) AS IdSubMenu, ifnull(rtrim(ltrim(SUBNO1)), '') AS Nombre, ifnull(rtrim(ltrim(SUBC09)), '') AS Controlador,");
            sb.Append(" ifnull(rtrim(ltrim(SUBVI1)), '') AS Vista, ifnull(SUBES4, 0) AS Estado, ifnull(rtrim(ltrim(SUBU03)), '') AS UsuarioCreado, ifnull(rtrim(ltrim(SUBF03)), '') AS FechaCreado,");
            sb.Append(" ifnull(rtrim(ltrim(SUBH03)), '') AS HoraCreado, ifnull(rtrim(ltrim(SUBU04)), '') AS UsuarioModificado, ifnull(rtrim(ltrim(SUBF04)), '') AS FechaModificado, ifnull(rtrim(ltrim(SUBH04)), '') AS HoraModificado,");            
            sb.Append(" ifnull(rtrim(ltrim(SUBSER)), '') as Servidor, ifnull(rtrim(ltrim(SUBURL)), '') as ReporteUrl ");
            sb.Append(" FROM USUARC ");
            sb.Append(" INNER JOIN ROLAR2 ON(USUCO1 = ROLCO2 AND USUCO2 = ROLCO3 AND USUCO3 = ROLCO4 AND USUCO4 = ROLCO1)");
            sb.Append(" INNER JOIN MENAR1 ON(MENCO1 = ROLCO2 AND MENCO2 = ROLCO3 AND MENCO3 = ROLCO4 AND MENCO4 = ROLCO1)");
            sb.Append(" INNER JOIN SUBAR7 ON(MENCO1 = SUBC10 AND MENCO2 = SUBC11 AND MENCO3 = SUBC12 AND MENCO4 = SUBC13 AND MENCOD = SUBC14)");
            sb.Append(" WHERE USUCOD = '" + codigoUsuario.ToUpper() + "' AND SUBC14 = '" + codigoMenu + "' AND SUBES4 = 1");
            query = sb.ToString();
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
                        tbSubMenu oSubMenu = new tbSubMenu();
                        oSubMenu.CodigoSubsistema = dr["CodigoSubsistema"].ToString();
                        oSubMenu.CodigoGestion = dr["CodigoGestion"].ToString();
                        oSubMenu.CodigoModulo = dr["CodigoModulo"].ToString();
                        oSubMenu.CodigoRol = dr["CodigoRol"].ToString();
                        oSubMenu.CodigoMenu = dr["CodigoMenu"].ToString();
                        oSubMenu.IdSubMenu = decimal.Parse(dr["IdSubMenu"].ToString());
                        oSubMenu.Nombre = dr["Nombre"].ToString();
                        oSubMenu.Controlador = dr["Controlador"].ToString();
                        oSubMenu.Vista = dr["Vista"].ToString();
                        oSubMenu.Estado = int.Parse(dr["Estado"].ToString());
                        oSubMenu.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oSubMenu.FechaCreado = dr["FechaCreado"].ToString();
                        oSubMenu.HoraCreado = dr["HoraCreado"].ToString();
                        oSubMenu.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oSubMenu.FechaModificado = dr["FechaModificado"].ToString();
                        oSubMenu.HoraModificado = dr["HoraModificado"].ToString();
                        oSubMenu.Servidor = dr["Servidor"].ToString();
                        oSubMenu.ReporteUrl = dr["ReporteUrl"].ToString();
                        listarSubMenu.Add(oSubMenu);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSubMenu = null;
                throw ex;
            }
            return listarSubMenu;
        }
    }
}
