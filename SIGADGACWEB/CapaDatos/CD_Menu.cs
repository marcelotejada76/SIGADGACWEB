using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_Menu
    {
        public static CD_Menu _instancia = null;        
        private CD_Menu()
        {

        }

        public static CD_Menu Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Menu();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo obtiene el menu
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns></returns>
        public List<tbMenu> GetMenuPorCodigo(string codigoUsuario)
        {
            List<tbMenu> listarMenu = new List<tbMenu>();           
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;

            sb.Append("SELECT ifnull(rtrim(ltrim(MENCO1)), '') AS CodigoSubsistema, ifnull(rtrim(ltrim(MENCO2)), '') AS CodigoGestion, ifnull(rtrim(ltrim(MENCO3)), '') AS CodigoModulo, ifnull(rtrim(ltrim(MENCO4)), '') AS CodigoRol,");
            sb.Append(" ifnull(rtrim(ltrim(MENCOD)), '') AS CodigoMenu, ifnull(rtrim(ltrim(MENDE1)), '') AS DescripcionMenu, ifnull(rtrim(ltrim(MENTIP)), '') AS TipoOpcionMenu, ifnull(rtrim(ltrim(MENEST)), '') AS EstadoMenu,");
            sb.Append(" ifnull(rtrim(ltrim(MENUS2)), '') AS UsuarioCreacion, ifnull(rtrim(ltrim(MENFE2)), '') AS FechaCreacion, ifnull(rtrim(ltrim(MENHO2)), '') AS HoraCreacion, ifnull(rtrim(ltrim(MENDIS)), '') AS DispositivoCreacion,");
            sb.Append(" ifnull(rtrim(ltrim(MENUS3)), '') AS UsuarioModificacion, ifnull(rtrim(ltrim(MENFE3)), '') AS FechaModificacion, ifnull(rtrim(ltrim(MENHO3)), '') AS HoraModificacion,");
            sb.Append(" ifnull(rtrim(ltrim(MENDI1)), '') AS DispositivoModificacion, ifnull(rtrim(ltrim(MENAU1)), '') as codigoServidorReport, ifnull(rtrim(ltrim(MENAU1)), '') as codigoServidorReport, ifnull((select rtrim(ltrim(VALDES)) FROM DGACSYS.TXDGAC where VALDDS = 'MENAU1' and VALVAL = MENAU1), '') as DescripcionServidorReport ");            
            sb.Append(" FROM USUARC ");
            sb.Append(" INNER JOIN ROLAR2 ON(USUCO1 = ROLCO2 AND USUCO2 = ROLCO3 AND USUCO3 = ROLCO4 AND USUCO4 = ROLCO1)");
            sb.Append(" INNER JOIN MENAR1 ON(MENCO1 = ROLCO2 AND MENCO2 = ROLCO3 AND MENCO3 = ROLCO4 AND MENCO4 = ROLCO1)");
            sb.Append(" WHERE USUCOD = '" + codigoUsuario.ToUpper() + "'");
            sb.Append(" AND (SELECT COUNT(*) FROM SUBAR7 WHERE MENCO1 = SUBC10 AND MENCO2 = SUBC11 AND MENCO3 = SUBC12 AND MENCO4 = SUBC13 AND MENCOD = SUBC14) > 0");
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
                        tbMenu oMenu = new tbMenu();
                        oMenu.CodigoSubsistema = dr["CodigoSubsistema"].ToString();
                        oMenu.CodigoGestion = dr["CodigoGestion"].ToString();
                        oMenu.CodigoModulo = dr["CodigoModulo"].ToString();
                        oMenu.CodigoRol = dr["CodigoRol"].ToString();
                        oMenu.CodigoMenu = dr["CodigoMenu"].ToString();
                        oMenu.DescripcionMenu = dr["DescripcionMenu"].ToString();
                        oMenu.TipoOpcionMenu = dr["TipoOpcionMenu"].ToString();
                        oMenu.EstadoMenu = dr["EstadoMenu"].ToString();
                        oMenu.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oMenu.FechaCreacion = dr["FechaCreacion"].ToString();
                        oMenu.HoraCreacion = dr["HoraCreacion"].ToString();
                        oMenu.DispositivoCreacion = dr["DispositivoCreacion"].ToString();
                        oMenu.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oMenu.FechaModificacion = dr["FechaModificacion"].ToString();
                        oMenu.HoraModificacion = dr["HoraModificacion"].ToString();
                        oMenu.DispositivoModificacion = dr["DispositivoModificacion"].ToString();  
                        oMenu.codigoServidorReport = dr["codigoServidorReport"].ToString();
                        oMenu.DescripcionServidorReport = dr["DescripcionServidorReport"].ToString();
                        listarMenu.Add(oMenu);
                    }
                    dr.Close();
                    foreach (var item in listarMenu)
                    {
                        item.oSubMenu = CD_SubMenu.Instancia.GetSubMenuPorCodigo(codigoUsuario, item.CodigoMenu);
                    }
                }

            }
            catch (Exception ex)
            {
                listarMenu = null;
                throw ex;
            }
            return listarMenu;
        }

    }
}
