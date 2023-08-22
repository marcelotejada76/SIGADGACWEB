using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_Usuario
    {
        public static CD_Usuario _instancia = null;       
        private string CadenaConexion;
        private CD_Usuario()
        {

        }

        public static CD_Usuario Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Usuario();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo obtiene el usuario
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns></returns>
        public tbUsuario GetUsuarioPorCodigo(string codigoUsuario)
        {
            tbUsuario oUsuario = null;
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;

            sb.Append("SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombresUsuario, ifnull(rtrim(ltrim(USUAPE)), '') AS ApellidosUsuario, ifnull(rtrim(ltrim(USUTIP)), '') AS TipoIdentificacion,");
            sb.Append(" ifnull(rtrim(ltrim(USUCED)), '') AS CedulaUsuario , ifnull(rtrim(ltrim(USUCOR)), '') AS CorreoUsuario, ifnull(rtrim(ltrim(USUCLA)), '') AS ClaveUsuario, ifnull(rtrim(ltrim(USUEST)), '') AS EstadoActividad, ifnull(rtrim(ltrim(USUTI1)), '') AS TipoAplicacion,");
            sb.Append(" ifnull(rtrim(ltrim(USUIDE)), '') as IdentificacionTributaria, ifnull(rtrim(ltrim(USUNUM)), '') as NumeroRuc, ifnull(rtrim(ltrim(USUUSU)), '') as UsuarioCreacion, ifnull(rtrim(ltrim(USUFEC)), '') as FechaCreacion,");
            sb.Append(" ifnull(rtrim(ltrim(USUHOR)), '') as HoraCreacion , ifnull(rtrim(ltrim(USUDIS)), '') as DispositivoCreacion, ifnull(rtrim(ltrim(USUUS1)), '') as UsuarioModificacion, ifnull(rtrim(ltrim(USUFE1)), '') as FechaModificacion,");
            sb.Append(" ifnull(rtrim(ltrim(USUHO1)), '') as HoraModificacion, ifnull(rtrim(ltrim(USUDI1)), '') as DispositivoModificacion, ifnull(rtrim(ltrim(USUCO1)), '') as CodigoSubsistema, ifnull(rtrim(ltrim(USUCO2)), '') as CodigoGestion,");
            sb.Append(" ifnull(rtrim(ltrim(USUCO3)), '') as CodigoModulo, ifnull(rtrim(ltrim(USUCO4)), '') as CodigoRol, ifnull(rtrim(ltrim(USUCO5)), '') as CodigoCiudad, ifnull(rtrim(ltrim(USUCO6)), '') as CodigoDependencia, ifnull(rtrim(ltrim(SUBDES)), '') AS DescripcionSubSistema");
            sb.Append(" FROM USUARC LEFT JOIN SUBAR2 ON (USUCO1 = SUBCOD) WHERE USUCOD = '" + codigoUsuario.ToUpper() + "'");
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
                        oUsuario = new tbUsuario();
                        oUsuario.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oUsuario.NombresUsuario = dr["NombresUsuario"].ToString();
                        oUsuario.ApellidosUsuario = dr["ApellidosUsuario"].ToString();
                        oUsuario.TipoIdentificacion = dr["TipoIdentificacion"].ToString();
                        oUsuario.CedulaUsuario = dr["CedulaUsuario"].ToString();
                        oUsuario.CorreoUsuario = dr["CorreoUsuario"].ToString();
                        oUsuario.ClaveUsuario = dr["ClaveUsuario"].ToString();
                        oUsuario.EstadoActividad = dr["EstadoActividad"].ToString();
                        oUsuario.TipoAplicacion = dr["TipoAplicacion"].ToString();
                        oUsuario.IdentificacionTributaria = dr["IdentificacionTributaria"].ToString();
                        oUsuario.NumeroRuc = dr["NumeroRuc"].ToString();
                        oUsuario.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oUsuario.FechaCreacion = dr["FechaCreacion"].ToString();
                        oUsuario.HoraCreacion = dr["HoraCreacion"].ToString();
                        oUsuario.DispositivoCreacion = dr["DispositivoCreacion"].ToString();
                        oUsuario.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oUsuario.FechaModificacion = dr["FechaModificacion"].ToString();
                        oUsuario.HoraModificacion = dr["HoraModificacion"].ToString();
                        oUsuario.DispositivoModificacion = dr["DispositivoModificacion"].ToString();
                        oUsuario.CodigoSubsistema = dr["CodigoSubsistema"].ToString();
                        oUsuario.CodigoGestion = dr["CodigoGestion"].ToString();
                        oUsuario.CodigoModulo = dr["CodigoModulo"].ToString();
                        oUsuario.CodigoRol = dr["CodigoRol"].ToString();
                        oUsuario.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        oUsuario.CodigoDependencia = dr["CodigoDependencia"].ToString();
                        oUsuario.DescripcionSubSistema = dr["DescripcionSubSistema"].ToString();
                        // oUsuario.oMenu = CD_Menu.Instancia.GetMenuPorCodigo(oUsuario.CodigoUsuario);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oUsuario = null;
                throw ex;
            }
            return oUsuario;
        }

        /// <summary>
        /// Metodo verifica si existe Usuario
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns></returns>
        public bool GetUsuarioExistePorCodigo(string codigoUsuario)
        {
          
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            bool estadoExiste = false;
            sb.Append("SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombresUsuario, ifnull(rtrim(ltrim(USUAPE)), '') AS ApellidosUsuario, ifnull(rtrim(ltrim(USUTIP)), '') AS TipoIdentificacion,");
            sb.Append(" ifnull(rtrim(ltrim(USUCED)), '') AS CedulaUsuario , ifnull(rtrim(ltrim(USUCOR)), '') AS CorreoUsuario, ifnull(rtrim(ltrim(USUCLA)), '') AS ClaveUsuario, ifnull(rtrim(ltrim(USUEST)), '') AS EstadoActividad, ifnull(rtrim(ltrim(USUTI1)), '') AS TipoAplicacion");
            sb.Append(" FROM USUARC WHERE USUCOD = '" + codigoUsuario.ToUpper() + "'");
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
                        estadoExiste = true;
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoExiste;

            
        }

        /// <summary>
        /// Metodo verifica si existe la contraseña
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public bool GetUsuarioExistePorCodigoContrasena(string codigoUsuario, string contrasena)
        {

            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            bool estadoExiste = false;
            sb.Append("SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombresUsuario, ifnull(rtrim(ltrim(USUAPE)), '') AS ApellidosUsuario, ifnull(rtrim(ltrim(USUTIP)), '') AS TipoIdentificacion,");
            sb.Append(" ifnull(rtrim(ltrim(USUCED)), '') AS CedulaUsuario , ifnull(rtrim(ltrim(USUCOR)), '') AS CorreoUsuario, ifnull(rtrim(ltrim(USUCLA)), '') AS ClaveUsuario, ifnull(rtrim(ltrim(USUEST)), '') AS EstadoActividad, ifnull(rtrim(ltrim(USUTI1)), '') AS TipoAplicacion");
            sb.Append(" FROM USUARC WHERE USUCOD = '" + codigoUsuario + "' AND USUCLA ='" + contrasena + "'");
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
                        estadoExiste = true;
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoExiste;
        }


        /// <summary>
        /// Metodo valida el usuario del as400
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasena"></param>
        /// <returns>String</returns>
        public string getValidaUsuarioIDB2(string usuario, string contrasena)
        {

            string mensajeError = "";
            if ((usuario.Trim().Length > 0) && (contrasena.Trim().Length > 0))
            {
                CadenaConexion = "DataSource = 190.152.8.185; UserID = " + usuario.ToUpper() + "; Password = " + contrasena.ToUpper() + "; Database = S10a1a05; DataCompression = True; EnablePreFetch = false; Pooling= false; ConnectionTimeout=0; MaximumPoolSize=-1;";
                iDB2Connection objetoConexion = new iDB2Connection(CadenaConexion);
                try
                {
                    objetoConexion.Open();
                    if (objetoConexion.State.ToString().Contains("Open"))
                    {
                        mensajeError = "200";
                    }
                }
                catch (iDB2Exception ex)
                {
                    //8001  El usuario en el sistema 10.1.1.2 no existe
                    //8002  La contraseña del usuario en el sistema 10.1.1.2 no es correcta
                    //8003  La contraseña del usuario en el sistema 10.1.1.2 ha caducado
                    //8011  El usuario en el sistema 10.1.1.2 ha sido inhabilitado
                    //8270  El perfil de usuario se inhabilitará con la próxima contraseña incorrecta
                     mensajeError = ex.MessageCode + ex.Errors.ToString() +"; "+ ex.MessageDetails + "; " + ex.Message + "; " + ex.InnerException + "; " + ex.SqlState;                   
                                       
                }
                finally
                {
                    objetoConexion.Close();
                }
            }
            else
                mensajeError = "400";

            return mensajeError;
        }
    }
}
