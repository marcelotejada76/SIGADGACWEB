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
            sb.Append(" ifnull(rtrim(ltrim(USUCO3)), '') as CodigoModulo, ifnull(rtrim(ltrim(USUCO4)), '') as CodigoRol, ifnull(rtrim(ltrim(USUCO9)), '') as CodigoCiudad, ifnull(rtrim(ltrim(USUCO6)), '') as CodigoDependencia, ifnull(rtrim(ltrim(DIRDES)), rtrim(ltrim(SUBDES))) AS DescripcionSubSistema,");
            sb.Append(" ifnull(rtrim(ltrim(USUNO1)), '') AS NOMBRECORTO, ifnull(rtrim(ltrim(USUCAR)), '') AS CARGO, ifnull(rtrim(ltrim(USUOID)), '') AS CentroContable");
            sb.Append(" FROM USUARC LEFT JOIN SUBAR2 ON (USUCO1 = SUBCOD) LEFT JOIN USUAR1 ON(USUCO8 = USUCOD) LEFT JOIN DIRARC ON(USUCO1 = DIRCO3)  WHERE USUCOD = '" + codigoUsuario.ToUpper() + "'");

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
                        oUsuario.NombreCorto = dr["NombreCorto"].ToString();
                        oUsuario.Cargo = dr["Cargo"].ToString();
                        oUsuario.CentroContable = dr["CentroContable"].ToString();

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
                CadenaConexion = "DataSource = 190.152.8.185; UserID = " + usuario.ToUpper() + "; Password = " + contrasena.ToUpper() + "; Database = S10a1a05; DataCompression = True; ConnectionTimeout=30;";
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
                     //mensajeError = ex.MessageCode + ex.Errors.ToString() +"; "+ ex.MessageDetails + "; " + ex.Message + "; " + ex.InnerException + "; " + ex.SqlState;

                    string codigo = Convert.ToString(ex.MessageCode);

                    switch (codigo)
                    {
                        case "8001":
                            mensajeError = "El usuario no existe.";
                            break;
                        case "8002":
                            mensajeError = "La contraseña es incorrecta.";
                            break;
                        case "8003":
                            mensajeError = "La contraseña ha caducado.";
                            break;
                        case "8011":
                            mensajeError = "El usuario ha sido inhabilitado.";
                            break;
                        case "8270":
                            mensajeError = "El perfil de usuario se inhabilitará con la próxima contraseña incorrecta.";
                            break;
                        default:
                            // Para cualquier otro error, mostramos un mensaje genérico sin datos técnicos
                            mensajeError = "Error de comunicación con el servidor. Intente más tarde.";
                            break;
                    }


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

        public tbUsuario ObtenerUsuario(string usuario, string clave)
        {
            tbUsuario ioUsuario = null;
            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUAPE)), '') AS ApellidoUsuario, "
                + " ifnull(rtrim(ltrim(USUCOR)), '') AS Correo, ifnull(rtrim(ltrim(USUCLA)), '') AS Clave, ifnull(rtrim(ltrim(USUEST)), '') as EstadoActividad, ifnull(rtrim(ltrim(USUTI1)), '') as TipoAplicacion, "
                + " ifnull(rtrim(ltrim(USUNUM)), '') as NumeroRuc, ifnull(rtrim(ltrim(USUUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(USUFEC)), '') as FechaCreado, ifnull(rtrim(ltrim(USUHOR)), '') as HoraCreado, "
                + " ifnull(rtrim(ltrim(USUUS1)), '') as UsuarioModificado, ifnull(rtrim(ltrim(USUFE1)), '') as FechaModificado,	ifnull(rtrim(ltrim(USUHO1)), '') as HoraModificado, ifnull(rtrim(ltrim(USUTIP)), '') as TipoIdentificacion, "
                + " ifnull(rtrim(ltrim(USUCO5)), '') AS CodigoCiudad, ifnull(rtrim(ltrim(USUCO6)), '') AS CodigoDependencia, ifnull(USUAUX, 0) AS NumeroAleatorio  FROM USUARC WHERE  UPPER(USUCOR) = UPPER('" + usuario + "') AND USUCLA = '" + clave + "'";
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
                        ioUsuario = new tbUsuario();
                        ioUsuario.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        ioUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                        ioUsuario.ApellidoUsuario = dr["ApellidoUsuario"].ToString();
                        ioUsuario.Correo = dr["Correo"].ToString();
                        ioUsuario.Clave = dr["Clave"].ToString();
                        ioUsuario.EstadoActividad = dr["EstadoActividad"].ToString();
                        ioUsuario.TipoAplicacion = dr["TipoAplicacion"].ToString();
                        ioUsuario.NumeroRuc = dr["NumeroRuc"].ToString();
                        ioUsuario.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        ioUsuario.CodigoDependencia = dr["CodigoDependencia"].ToString();
                        ioUsuario.TipoIdentificacion = dr["TipoIdentificacion"].ToString();
                        ioUsuario.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioUsuario.FechaCreado = dr["FechaCreado"].ToString();
                        ioUsuario.HoraCreado = dr["HoraCreado"].ToString();
                        ioUsuario.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioUsuario.FechaModificado = dr["FechaModificado"].ToString();
                        ioUsuario.HoraModificado = dr["HoraModificado"].ToString();
                        ioUsuario.NumeroAleatorio = dr["NumeroAleatorio"].ToString();
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                    //if (ioUsuario != null)
                    //    ioUsuario.oListaRepresentanteLegal = CD_DetalleRepresentanteLegal.Instancia.DetalleRepresentanteLegalPorUsuario(ioUsuario.CodigoUsuario);

                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }

            return ioUsuario;
        }

        public bool ValidaExiteClave(string password)
        {
            bool respuesta = false;

            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUCOR)), '') AS Correo "
                + " FROM USUARC WHERE  USUCLA = '" + password + "'";
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
                        respuesta = true;
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;

        }


        public bool ValidaNuevaContrasenaIgualDB(string correo, int nuevaClave)
        {
            bool respuesta = false;

            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUCOR)), '') AS Correo "
                + " FROM USUARC WHERE UPPER(USUCOR) = UPPER('" + correo + "') AND USUAUX = " + nuevaClave;
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
                        respuesta = true;
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;

        }

        /// <summary>
        /// Metodo verifca si ya esta registrado el correo 
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public bool ValidaExiteCorreo(string correo)
        {
            bool respuesta = false;

            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUAPE)), '') as ApellidoUsuario, "
                + " ifnull(rtrim(ltrim(USUCOR)), '') AS Correo FROM USUARC WHERE  UPPER(USUCOR) = UPPER('" + correo + "')";
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
                        respuesta = true;
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public tbUsuario UsuarioPorCorreo(string correo)
        {

            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUAPE)), '') AS ApellidoUsuario, "
                + " ifnull(rtrim(ltrim(USUCOR)), '') AS Correo, ifnull(rtrim(ltrim(USUCLA)), '') AS Clave, ifnull(rtrim(ltrim(USUEST)), '') as EstadoActividad, ifnull(rtrim(ltrim(USUTI1)), '') as TipoAplicacion, "
                + " ifnull(rtrim(ltrim(USUNUM)), '') as NumeroRuc, ifnull(rtrim(ltrim(USUUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(USUFEC)), '') as FechaCreado, ifnull(rtrim(ltrim(USUHOR)), '') as HoraCreado, "
                + " ifnull(rtrim(ltrim(USUUS1)), '') as UsuarioModificado, ifnull(rtrim(ltrim(USUFE1)), '') as FechaModificado,	ifnull(rtrim(ltrim(USUHO1)), '') as HoraModificado, ifnull(rtrim(ltrim(USUTIP)), '') as TipoIdentificacion, "
                + " ifnull(rtrim(ltrim(USUCO5)), '') AS CodigoCiudad, ifnull(rtrim(ltrim(USUCO6)), '') AS CodigoDependencia, ifnull(USUAUX, 0) AS NumeroAleatorio FROM USUARC WHERE  USUCOR = '" + correo + "'";
            iDB2Command cmd;
            tbUsuario ioUsuario = new tbUsuario();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ioUsuario.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        ioUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                        ioUsuario.ApellidoUsuario = dr["ApellidoUsuario"].ToString();
                        ioUsuario.Correo = dr["Correo"].ToString();
                        ioUsuario.Clave = dr["Clave"].ToString();
                        ioUsuario.EstadoActividad = dr["EstadoActividad"].ToString();
                        ioUsuario.TipoAplicacion = dr["TipoAplicacion"].ToString();
                        ioUsuario.NumeroRuc = dr["NumeroRuc"].ToString();
                        ioUsuario.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        ioUsuario.CodigoDependencia = dr["CodigoDependencia"].ToString();
                        ioUsuario.TipoIdentificacion = dr["TipoIdentificacion"].ToString();
                        ioUsuario.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioUsuario.FechaCreado = dr["FechaCreado"].ToString();
                        ioUsuario.HoraCreado = dr["HoraCreado"].ToString();
                        ioUsuario.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioUsuario.FechaModificado = dr["FechaModificado"].ToString();
                        ioUsuario.HoraModificado = dr["HoraModificado"].ToString();
                        ioUsuario.NumeroAleatorio = dr["NumeroAleatorio"].ToString();
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ioUsuario;
        }

        public bool CrearoNumeroCodigoVerificador(string correo, int numeroAleatorio)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "UPDATE USUARC SET USUAUX = @NumeroAleatorio,"
                + " USUFE1 = @FechaModificado, USUHO1 = @HoraModificado WHERE USUCOR = @Correo";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroAleatorio"].Value = numeroAleatorio;
                    cmd.Parameters["@FechaModificado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificado"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@Correo"].Value = correo;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    oConexion.Close();
                    oConexion.Dispose();
                    cmd.Dispose();


                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool CrearoNumeroCodigoVerificadorClave(string correo, int numeroAleatorio, string clave)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "UPDATE USUARC SET USUAUX = @NumeroAleatorio, USUCLA = @clave"
                + " USUFE1 = @FechaModificado, USUHO1 = @HoraModificado WHERE USUCOR = @Correo";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroAleatorio"].Value = numeroAleatorio;
                    cmd.Parameters["@FechaModificado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificado"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@clave"].Value = clave;
                    cmd.Parameters["@Correo"].Value = correo;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    oConexion.Close();
                    oConexion.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo verifca si ya esta registrado el correo 
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public bool ValidaExiteCedula(string cedula)
        {
            bool respuesta = false;

            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUAPE)), '') as ApellidoUsuario, "
                + " ifnull(rtrim(ltrim(USUCOR)), '') AS Correo FROM USUARC WHERE  USUCED = '" + cedula + "'";
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
                        respuesta = true;
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;

        }

        /// <summary>
        /// Metodo Inserta un registro de usuario la Solicitud de Vuelos charter nacional e internacional y vuelos especiales
        /// </summary>
        /// <param name="oUsuario"></param>
        /// <returns>Usuario</returns>
        public bool RegistrarUsuario(tbUsuario oUsuario)
        {
            bool respuesta = true;
            string _usuariocreado = string.Empty;
            //string _rucPassaporte = string.Empty;
            string _Cedula = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    oUsuario.CodigoUsuario = GenerarCodigoUsuario();
                    oUsuario.UsuarioCreado = "DBADMIN1";
                    oUsuario.CedulaIdentificacion = "RUC";
                    //oUsuario.NumeroRuc = oUsuario.CedulaIdentificacion;

                    //if (oUsuario.CedulaIdentificacion != null)
                    //{
                    //    if (oUsuario.CedulaIdentificacion.Length > 10)
                    //        _Cedula = oUsuario.CedulaIdentificacion.Substring(0, 10);
                    //    else
                    //    {
                    //        _Cedula = oUsuario.CedulaIdentificacion;
                    //    }
                    //}
                    string subsistema = "DFIN";
                    string sistema = "GTES";
                    string nivel = "CIAS";
                    string grupo = "CIAS";

                    cmd = new iDB2Command("INSERT INTO USUARC (USUCOD, USUTIP, USUNUM, USUCED, USUNOM, USUAPE, USUCOR, USUCLA, USUEST, USUTI1, USUUSU, USUFEC, USUHOR,USUCO1,USUCO2,USUCO3,USUCO4) " +
                        "VALUES(@CodigoUsuario, @TipoIdentificacion, @NumeroRuc, @CedulaIdentificacion, @NombreUsuario, @ApellidoUsuario, @Correo, @Clave, @EstadoActividad,  @TipoAplicacion, " +
                        "@UsuarioCreado, @FechaCreado, @HoraCreado,@Subsistema, @Sistema, @Nivel, @Grupo)", oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@CodigoUsuario"].Value = oUsuario.CodigoUsuario.Trim();
                    cmd.Parameters["@TipoIdentificacion"].Value = campoNull(oUsuario.TipoIdentificacion).Trim();
                    cmd.Parameters["@NumeroRuc"].Value = campoNull(oUsuario.NumeroRuc).Trim();
                    cmd.Parameters["@CedulaIdentificacion"].Value = campoNull(_Cedula).Trim();
                    cmd.Parameters["@NombreUsuario"].Value = campoNull(oUsuario.NombreUsuario).Trim().ToUpper();
                    cmd.Parameters["@ApellidoUsuario"].Value = campoNull(oUsuario.ApellidoUsuario).Trim().ToUpper();
                    cmd.Parameters["@Correo"].Value = campoNull(oUsuario.Correo.Trim());
                    cmd.Parameters["@Clave"].Value = campoNull(oUsuario.Clave);
                    cmd.Parameters["@EstadoActividad"].Value = oUsuario.EstadoActividad;
                    cmd.Parameters["@TipoAplicacion"].Value = oUsuario.TipoAplicacion;
                    cmd.Parameters["@UsuarioCreado"].Value = oUsuario.UsuarioCreado;
                    cmd.Parameters["@FechaCreado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraCreado"].Value = osistema.HoraSistema;
                    cmd.Parameters["@Subsistema"].Value = subsistema;
                    cmd.Parameters["@Sistema"].Value = sistema;
                    cmd.Parameters["@Nivel"].Value = nivel;
                    cmd.Parameters["@Grupo"].Value = grupo;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    oConexion.Close();
                    oConexion.Dispose();
                    cmd.Dispose();

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Metodo Inserta un registro de usuario la Solicitud de Vuelos charter nacional e internacional y vuelos especiales
        /// </summary>
        /// <param name="oUsuario"></param>
        /// <returns>Usuario</returns>
        public tbUsuario RegistrarUsuarioWeb(tbUsuario oUsuario)
        {
            bool respuesta = true;
            string _usuariocreado = string.Empty;
            iDB2Command cmd;
            tbUsuario usuarioWeb = new tbUsuario();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    _usuariocreado = GenerarCodigoUsuario();
                    cmd = new iDB2Command("INSERT INTO USUARC (USUCOD, USUNOM, USUAPE, USUCOR, USUCLA, USUEST, USUTI1, USUUSU, USUFEC, USUHOR) VALUES(@CodigoUsuario, @NombreUsuario, @ApellidoUsuario, @Correo, @Clave, @EstadoActividad,  @TipoAplicacion, @UsuarioCreado, @FechaCreado, @HoraCreado)", oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@CodigoUsuario"].Value = _usuariocreado.Trim();
                    cmd.Parameters["@NombreUsuario"].Value = oUsuario.NombreUsuario.Trim();
                    cmd.Parameters["@ApellidoUsuario"].Value = oUsuario.ApellidoUsuario.Trim();
                    cmd.Parameters["@Correo"].Value = oUsuario.Correo.Trim();
                    cmd.Parameters["@Clave"].Value = oUsuario.Clave;
                    cmd.Parameters["@EstadoActividad"].Value = oUsuario.EstadoActividad;
                    cmd.Parameters["@TipoAplicacion"].Value = oUsuario.TipoAplicacion;
                    cmd.Parameters["@UsuarioCreado"].Value = "DBADMIN1";
                    cmd.Parameters["@FechaCreado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraCreado"].Value = DateTime.Now.ToString("HH:mm:ss");

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    oConexion.Close();
                    oConexion.Dispose();
                    cmd.Dispose();

                    if (respuesta)
                        oUsuario = ObtenerUsuarioPorCodigo(_usuariocreado);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return usuarioWeb;
        }

        public tbUsuario ObtenerUsuarioPorCodigo(string codUsuario)
        {
            tbUsuario ioUsuario = null;
            string query = "SELECT ifnull(rtrim(ltrim(USUCOD)), '') AS CodigoUsuario, ifnull(rtrim(ltrim(USUNOM)), '') AS NombreUsuario, ifnull(rtrim(ltrim(USUAPE)), '') AS ApellidoUsuario, "
                + " ifnull(rtrim(ltrim(USUCOR)), '') AS Correo, ifnull(rtrim(ltrim(USUCLA)), '') AS Clave, ifnull(rtrim(ltrim(USUEST)), '') as EstadoActividad, ifnull(rtrim(ltrim(USUTI1)), '') as TipoAplicacion, "
                + " ifnull(rtrim(ltrim(USUNUM)), '') as NumeroRuc, ifnull(rtrim(ltrim(USUUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(USUFEC)), '') as FechaCreado, ifnull(rtrim(ltrim(USUHOR)), '') as HoraCreado, "
                + " ifnull(rtrim(ltrim(USUUS1)), '') as UsuarioModificado, ifnull(rtrim(ltrim(USUFE1)), '') as FechaModificado,	ifnull(rtrim(ltrim(USUHO1)), '') as HoraModificado, ifnull(rtrim(ltrim(USUTIP)), '') as TipoIdentificacion, "
                + " ifnull(rtrim(ltrim(USUCO9)), '') AS CodigoCiudad, ifnull(rtrim(ltrim(USUCED)), '') AS CedulaIdentificacion, ifnull(rtrim(ltrim(USUCO6)), '') AS CodigoDependencia, USUOID AS CentroContable, ifnull(rtrim(ltrim(USUNO1)), '') AS NOMBRECORTO, ifnull(rtrim(ltrim(USUCAR)), '') AS CARGO, "
                + " ifnull(rtrim(ltrim(CERES4)), '') AS EstadoCertificado FROM USUARC LEFT JOIN USUAR1 ON(USUCO8 = USUCOD) LEFT JOIN CERAR4 ON(USUCOD = CERC10)  WHERE USUCOD = '" + codUsuario.ToUpper() + "'";
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
                        ioUsuario = new tbUsuario();
                        ioUsuario.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        ioUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                        ioUsuario.ApellidoUsuario = dr["ApellidoUsuario"].ToString();
                        ioUsuario.Correo = dr["Correo"].ToString();
                        ioUsuario.Clave = dr["Clave"].ToString();
                        ioUsuario.EstadoActividad = dr["EstadoActividad"].ToString();
                        ioUsuario.TipoAplicacion = dr["TipoAplicacion"].ToString();
                        ioUsuario.NumeroRuc = dr["NumeroRuc"].ToString();
                        ioUsuario.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        ioUsuario.CodigoDependencia = dr["CodigoDependencia"].ToString();
                        ioUsuario.CentroContable = dr["CentroContable"].ToString();
                        ioUsuario.TipoIdentificacion = dr["TipoIdentificacion"].ToString();
                        ioUsuario.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioUsuario.FechaCreado = dr["FechaCreado"].ToString();
                        ioUsuario.HoraCreado = dr["HoraCreado"].ToString();
                        ioUsuario.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioUsuario.FechaModificado = dr["FechaModificado"].ToString();
                        ioUsuario.HoraModificado = dr["HoraModificado"].ToString();
                        ioUsuario.NombreCorto = dr["NombreCorto"].ToString();
                        ioUsuario.Cargo = dr["Cargo"].ToString();
                        ioUsuario.CedulaIdentificacion = dr["CedulaIdentificacion"].ToString();
                        ioUsuario.EstadoCertificado = dr["EstadoCertificado"].ToString();
                      //  ioUsuario.oListaMenu = ObtenerDetalleMenuXUsuario(ioUsuario.CodigoUsuario);
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                ioUsuario = null;
            }
            return ioUsuario;
        }

        private string GenerarCodigoUsuario()
        {
            string query = "SELECT ('DGACFIN' || (COUNT(*) + 1)) AS CodigoUsuario FROM USUARC WHERE USUCO4 = 'CIAS'";
            iDB2Command cmd;
            string CodigoUsuario = string.Empty;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CodigoUsuario = dr["CodigoUsuario"].ToString();
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                CodigoUsuario = string.Empty;
            }
            return CodigoUsuario;
        }

        /// <summary>
        /// Metodo obtiene el numero aleatorio
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public string ObtenerCodigoVerifivacion(string correo)
        {
            string numeroAleatorio = "0";
            string query = "SELECT ifnull(USUAUX, 0) as NumeroAleatorio FROM USUARC WHERE USUCOR = '" + correo.Trim() + "'";
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
                        numeroAleatorio = dr["NumeroAleatorio"].ToString();
                    }
                    oConexion.Close();
                    oConexion.Dispose();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                numeroAleatorio = "0";
            }
            return numeroAleatorio;
        }



        /// <summary>
        /// Metodo Actualiza el registro de usuario
        /// </summary>
        /// <param name="oUsuario"></param>
        /// <returns>True o False</returns>
        public bool CambiaContrasenaUsuario(string correo, string nuevaClave)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "UPDATE USUARC SET USUCLA = @Clave,"
                + " USUFE1 = @FechaModificado, USUHO1 = @HoraModificado, USUAUX = @Verificacion WHERE USUCOR = @Correo";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Clave"].Value = nuevaClave;
                    cmd.Parameters["@FechaModificado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificado"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@Verificacion"].Value = 0;
                    cmd.Parameters["@Correo"].Value = correo;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    oConexion.Close();
                    oConexion.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

    }
}
