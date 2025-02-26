using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;
namespace CapaDatos
{
  public class CD_Compania
    {
        public static CD_Compania _instancia = null;
        private CD_Compania()
        {

        }

        public static CD_Compania Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Compania();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo obtine la Compañia
        /// </summary>
        /// <param name="numeroRuc"></param>
        /// <returns>Compania</returns>
        public tbCompaniaAviacion ObtenerCompania(string numeroRuc)
        {
            tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO2)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO3)), '') as CodigoNumeroCia, ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion, ");
            sb.Append(" ifnull(rtrim(ltrim(CIATI1)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ifnull(rtrim(ltrim(CIARUC)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
            sb.Append(" ifnull(rtrim(ltrim(CIAREP)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO1)), '') as NombreCorto, ifnull(rtrim(ltrim(CIACO4)), '') as CodigoPais, ifnull(rtrim(ltrim(CIATIP)), '') as TipoCompania, ifnull(rtrim(ltrim(CIAEST)), '') as EstadoCompania, ifnull(rtrim(ltrim(CIACIU)), '') as CodigoCiudad, ");
            sb.Append(" ifnull(rtrim(ltrim(CIAUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFEC)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHOR)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS1)), '') as UsuarioModificado, ifnull(rtrim(ltrim(CIAFE1)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO1)), '') as HoraModificado  ");
            sb.Append(" FROM CIAARC WHERE CIARUC = '" + numeroRuc.Trim() + "'");
            string query = string.Empty;
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                        ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                        ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                        ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                        ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                        ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                        ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                        ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                        ioCompania.Email = dr["Email"].ToString().Trim();
                        ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                        ioCompania.Celular = dr["Celular"].ToString().Trim();
                        ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                        ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                        ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                        ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                        ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                        ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                        ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioCompania.FechaCrado = dr["UsuarioCreado"].ToString();
                        ioCompania.HoraCreado = dr["UsuarioCreado"].ToString();
                        ioCompania.UsuarioModificado = dr["UsuarioCreado"].ToString();
                        ioCompania.FechaModificado = dr["UsuarioCreado"].ToString();
                        ioCompania.HoraModificado = dr["UsuarioCreado"].ToString();
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                ioCompania = null;
            }
            return ioCompania;
        }

        /// <summary>
        /// Metodo obtine la Compañia
        /// </summary>
        /// <param name="numeroRuc"></param>
        /// <returns>Compania</returns>
        public tbCompaniaAviacion ObtenerCompaniaPorOACI(string codigoOaci)
        {
            tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (codigoOaci != null)
            {
                if (codigoOaci.Trim().Length > 0)
                {
                    sb.Append("SELECT CIAOID AS IdCompania, CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion, ");
                    sb.Append(" CIATI1 as TipoIdentificacion, CIADIR as Direccion, CIARUC as RucCompania, CIAEMA as Email, CIATEL as Telefono, CIACEL as Celular, ");
                    sb.Append(" CIAREP as RepresentanteLegal, CIANO1 as NombreCorto, CIACO4 as CodigoPais, CIATIP as TipoCompania, CIAEST as EstadoCompania, CIACIU as CodigoCiudad, ");
                    sb.Append(" CIAUSU as UsuarioCreado, CIAFEC as FechaCrado, CIAHOR as HoraCreado,  CIAUS1 as UsuarioModificado, CIAFE1 as FechaModificado,CIAHO1 as HoraModificado  ");
                    sb.Append(" FROM CIAARC WHERE CIACOD = '" + codigoOaci.Trim() + "'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                                ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                                ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaCrado = dr["UsuarioCreado"].ToString();
                                ioCompania.HoraCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.UsuarioModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.HoraModificado = dr["UsuarioCreado"].ToString();
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        ioCompania = null;
                    }
                }
            }

            return ioCompania;
        }

        /// <summary>
        /// Metodo Obtiene la compania
        /// </summary>
        /// <param name="codigoOaci"></param>
        /// <returns>CompañiaAviacion</returns>
        public tbCompaniaAviacion ObtenerCompaniaOACI(string codigoOaci)
        {
            tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (codigoOaci != null)
            {
                if (codigoOaci.Trim().Length > 0)
                {
                    sb.Append("SELECT CIAOID AS IdCompania, CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion, ");
                    sb.Append(" CIATI1 as TipoIdentificacion, CIADIR as Direccion, CIARUC as RucCompania, CIAEMA as Email, CIATEL as Telefono, CIACEL as Celular, ");
                    sb.Append(" CIAREP as RepresentanteLegal, CIANO1 as NombreCorto, CIACO4 as CodigoPais, CIATIP as TipoCompania, CIAEST as EstadoCompania, CIACIU as CodigoCiudad, ");
                    sb.Append(" CIAUSU as UsuarioCreado, CIAFEC as FechaCrado, CIAHOR as HoraCreado,  CIAUS1 as UsuarioModificado, CIAFE1 as FechaModificado,CIAHO1 as HoraModificado  ");
                    sb.Append(" FROM CIAARC WHERE CIACOD = '" + codigoOaci.Trim() + "'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        ioCompania = null;
                    }
                }
            }

            return ioCompania;
        }

        public tbCompaniaAviacion CompaniaAviacionPorId(decimal id)
        {
            tbCompaniaAviacion oCompania = new tbCompaniaAviacion();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            sb.Append("SELECT CIAOID AS IdCompania, CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion, ");
            sb.Append(" CIATI1 as TipoIdentificacion, CIADIR as Direccion, CIARUC as RucCompania, CIAEMA as Email, CIATEL as Telefono, CIACEL as Celular, ");
            sb.Append(" CIAREP as RepresentanteLegal, CIANO1 as NombreCorto, CIACO4 as CodigoPais, CIATIP as TipoCompania, CIAEST as EstadoCompania, CIACIU as CodigoCiudad, ");
            sb.Append(" CIAUSU as UsuarioCreado, CIAFEC as FechaCrado, CIAHOR as HoraCreado,  CIAUS1 as UsuarioModificado, CIAFE1 as FechaModificado, CIAHO1 as HoraModificado, CIAOI2 as OidP5");
            sb.Append(" FROM CIAARC WHERE CIAOI2 = " + id);

            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                        oCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                        oCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                        oCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                        oCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                        oCompania.Direccion = dr["Direccion"].ToString().Trim();
                        oCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                        oCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                        oCompania.Email = dr["Email"].ToString().Trim();
                        oCompania.Telefono = dr["Telefono"].ToString().Trim();
                        oCompania.Celular = dr["Celular"].ToString().Trim();
                        oCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                        oCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                        oCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                        oCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                        oCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                        oCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                        oCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCompania.FechaCrado = dr["UsuarioCreado"].ToString();
                        oCompania.HoraCreado = dr["UsuarioCreado"].ToString();
                        oCompania.UsuarioModificado = dr["UsuarioCreado"].ToString();
                        oCompania.FechaModificado = dr["UsuarioCreado"].ToString();
                        oCompania.HoraModificado = dr["UsuarioCreado"].ToString();
                        oCompania.OidP5 = dr["OidP5"].ToString();
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oCompania;
        }

        /// <summary>
        /// Metodo busca Compañia Operadora de la Avicion que tiene aeronaves
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns>List</returns>
        public List<tbCompaniaAviacion> CompaniaOperadoraAviacionPorDescripcion(string descripcion)
        {
            List<tbCompaniaAviacion> listCompania = new List<tbCompaniaAviacion>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (descripcion != null)
            {
                if (descripcion.Trim().Length > 0)
                {
                    sb.Append("SELECT DISTINCT ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO2)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO3)), '') as CodigoNumeroCia, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI1)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIARUC)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIAREP)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO1)), '') as NombreCorto,  ");
                    sb.Append(" ifnull(rtrim(ltrim(CIACO4)), '') as CodigoPais, ifnull(rtrim(ltrim(CIATIP)), '') as TipoCompania, ifnull(rtrim(ltrim(CIAEST)), '') as EstadoCompania, ifnull(rtrim(ltrim(CIACIU)), '') as CodigoCiudad,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFEC)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHOR)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS1)), '') as UsuarioModificado,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAFE1)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO1)), '') as HoraModificado");
                    sb.Append(" FROM CIAARC LEFT JOIN AERAR1 ON(AEROID = CIAOID) WHERE CIANOM LIKE '%" + descripcion.Trim() + "%' AND CIAEST = 'AC' ORDER BY NombreCompaniaAviacion ASC");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();

                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                                ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                                ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaCrado = dr["UsuarioCreado"].ToString();
                                ioCompania.HoraCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.UsuarioModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.HoraModificado = dr["UsuarioCreado"].ToString();
                                listCompania.Add(ioCompania);
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        listCompania = null;
                    }
                }
            }

            return listCompania;
        }


        /// <summary>
        /// Metodo busca Compañia de Avicion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns>List</returns>
        public List<tbCompaniaAviacion> BuscarCompaniaAviacionPorDescripcion(string descripcion)
        {
            List<tbCompaniaAviacion> listCompania = new List<tbCompaniaAviacion>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (descripcion != null)
            {
                if (descripcion.Trim().Length > 0)
                {
                    sb.Append("SELECT ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO2)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO3)), '') as CodigoNumeroCia, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI1)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIARUC)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIAREP)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO1)), '') as NombreCorto,  ");
                    sb.Append(" ifnull(rtrim(ltrim(CIACO4)), '') as CodigoPais, ifnull(rtrim(ltrim(CIATIP)), '') as TipoCompania, ifnull(rtrim(ltrim(CIAEST)), '') as EstadoCompania, ifnull(rtrim(ltrim(CIACIU)), '') as CodigoCiudad,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFEC)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHOR)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS1)), '') as UsuarioModificado,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAFE1)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO1)), '') as HoraModificado , ifnull(rtrim(ltrim(CERNUM)), '') AS NumeroCertificado, ifnull(rtrim(ltrim(CERFE3)), '') as fechaVigencia, ifnull(CIAOI2, 0) as OidP5");
                    sb.Append(" FROM CIAARC LEFT JOIN CERARC ON(CEROI1 = CIAOID) WHERE  CIANOM LIKE '%" + descripcion.Trim() + "%' AND CIAEST = 'AC'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();

                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                                ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                                ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                                ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                                ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                                ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                                ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                                ioCompania.NumeroCertificado = dr["NumeroCertificado"].ToString();
                                ioCompania.FechaVigencia = dr["FechaVigencia"].ToString();
                                ioCompania.OidP5 = dr["OidP5"].ToString();
                                listCompania.Add(ioCompania);
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        listCompania = null;
                    }
                }
            }

            return listCompania;
        }

        /// <summary>
        /// Metodo busca Compañia de Avicion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns>List</returns>
        public List<tbCompaniaAviacion> BuscarCompaniaAviacionPorDescripcionCharter(string codigoUsuario)
        {
            List<tbCompaniaAviacion> listCompania = new List<tbCompaniaAviacion>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (codigoUsuario != null)
            {
                if (codigoUsuario.Trim().Length > 0)
                {
                    sb.Append("SELECT ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO2)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO3)), '') as CodigoNumeroCia, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI1)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIARUC)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIAREP)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO1)), '') as NombreCorto,  ");
                    sb.Append(" ifnull(rtrim(ltrim(CIACO4)), '') as CodigoPais, ifnull(rtrim(ltrim(CIATIP)), '') as TipoCompania, ifnull(rtrim(ltrim(CIAEST)), '') as EstadoCompania, ifnull(rtrim(ltrim(CIACIU)), '') as CodigoCiudad,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFEC)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHOR)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS1)), '') as UsuarioModificado,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAFE1)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO1)), '') as HoraModificado , ifnull(rtrim(ltrim(CERNUM)), '') AS NumeroCertificado, ifnull(rtrim(ltrim(CERFE3)), '') as fechaVigencia, ifnull(CIAOI2, 0) as OidP5");
                    sb.Append(" FROM CIAARC INNER JOIN DETAR4 ON(CIAOI2 = DETOID) LEFT JOIN CERARC ON(CEROI1 = CIAOID) WHERE  DETCO3 = '" + codigoUsuario + "' AND CIAEST = 'AC'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();

                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                                ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                                ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                                ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                                ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                                ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                                ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                                ioCompania.NumeroCertificado = dr["NumeroCertificado"].ToString();
                                ioCompania.FechaVigencia = dr["FechaVigencia"].ToString();
                                ioCompania.OidP5 = dr["OidP5"].ToString();
                                listCompania.Add(ioCompania);
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        listCompania = null;
                    }
                }
            }

            return listCompania;
        }


        /// <summary>
        /// Metodo busca Compañia de Avicion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns>List</returns>
        public List<tbCompaniaAviacion> CompaniaAviacionPorDescripcionAOC(string descripcion)
        {
            List<tbCompaniaAviacion> listCompania = new List<tbCompaniaAviacion>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (descripcion != null)
            {
                if (descripcion.Trim().Length > 0)
                {
                    sb.Append("SELECT ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO2)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO3)), '') as CodigoNumeroCia, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI1)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIARUC)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIAREP)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO1)), '') as NombreCorto,  ");
                    sb.Append(" ifnull(rtrim(ltrim(CIACO4)), '') as CodigoPais, ifnull(rtrim(ltrim(CIATIP)), '') as TipoCompania, ifnull(rtrim(ltrim(CIAEST)), '') as EstadoCompania, ifnull(rtrim(ltrim(CIACIU)), '') as CodigoCiudad,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFEC)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHOR)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS1)), '') as UsuarioModificado,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAFE1)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO1)), '') as HoraModificado , ifnull(rtrim(ltrim(CERNUM)), '') AS NumeroCertificado, ifnull(rtrim(ltrim(CERFE3)), '') as fechaVigencia, ifnull(CIAOI2, 0) as OidP5");
                    sb.Append(" FROM CIAARC LEFT JOIN CERARC ON(CEROI1 = CIAOID) WHERE CIANOM LIKE '%" + descripcion.Trim() + "%' AND CIAEST = 'AC'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();

                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                                ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                                ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                                ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                                ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                                ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                                ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                                ioCompania.NumeroCertificado = dr["NumeroCertificado"].ToString();
                                ioCompania.FechaVigencia = dr["FechaVigencia"].ToString();
                                ioCompania.OidP5 = dr["OidP5"].ToString();
                                listCompania.Add(ioCompania);
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        listCompania = null;
                    }
                }
            }

            return listCompania;
        }



        /// <summary>
        /// Metodo busca Compañia de Avicion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns>List</returns>
        public List<tbCompaniaAviacion> CompaniaAviacionPorDescripcion(string descripcion)
        {
            List<tbCompaniaAviacion> listCompania = new List<tbCompaniaAviacion>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (descripcion != null)
            {
                if (descripcion.Trim().Length > 0)
                {
                    sb.Append("SELECT ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO2)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO3)), '') as CodigoNumeroCia, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI1)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIARUC)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIAREP)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO1)), '') as NombreCorto,  ");
                    sb.Append(" ifnull(rtrim(ltrim(CIACO4)), '') as CodigoPais, ifnull(rtrim(ltrim(CIATIP)), '') as TipoCompania, ifnull(rtrim(ltrim(CIAEST)), '') as EstadoCompania, ifnull(rtrim(ltrim(CIACIU)), '') as CodigoCiudad,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAUSU)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFEC)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHOR)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS1)), '') as UsuarioModificado,");
                    sb.Append(" ifnull(rtrim(ltrim(CIAFE1)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO1)), '') as HoraModificado, ifnull(CIAOI2, 0) as OidP5");
                    sb.Append(" FROM CIAARC WHERE CIANOM LIKE '%" + descripcion.Trim() + "%' AND CIAEST = 'AC'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();

                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                                ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                                ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.EstadoCompania = dr["EstadoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaCrado = dr["UsuarioCreado"].ToString();
                                ioCompania.HoraCreado = dr["UsuarioCreado"].ToString();
                                ioCompania.UsuarioModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.FechaModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.HoraModificado = dr["UsuarioCreado"].ToString();
                                ioCompania.OidP5 = dr["OidP5"].ToString();
                                listCompania.Add(ioCompania);
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        listCompania = null;
                    }
                }
            }

            return listCompania;
        }

        /// <summary>
        /// Metodo busca Compañia de Avicion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns>List</returns>
        public List<tbCompaniaAviacion> GetCompaniaAviacionPorDescripcionSobreVuelos(string descripcion)
        {
            List<tbCompaniaAviacion> listCompania = new List<tbCompaniaAviacion>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (descripcion != null)
            {
                if (descripcion.Trim().Length > 0)
                {
                    sb.Append("SELECT ifnull(CIAOID, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACOD)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIANOM)), '') as NombreCompaniaAviacion, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIADIR)), '') as Direccion, ifnull(rtrim(ltrim(CIAEMA)), '') as Email, ifnull(rtrim(ltrim(CIATEL)), '') as Telefono, ifnull(rtrim(ltrim(CIACEL)), '') as Celular, ");
                    sb.Append(" CIAREP as RepresentanteLegal, CIACO4 as CodigoPais, CIATIP as TipoCompania, CIACIU as CodigoCiudad, ");
                    sb.Append(" ifnull(rtrim(ltrim(CIADI3)), '') as DireccionBilling, ifnull(rtrim(ltrim(CIATE2)), '') as TelefonoBilling, ifnull(rtrim(ltrim(CIAEM1)), '') as EmailBilling, ifnull(rtrim(ltrim(CIARE1)), '') as RepresentanteLegalBilling, ifnull(CIAOI2, 0) AS OidP5");
                    sb.Append(" FROM CIAARC WHERE CIANOM LIKE '%" + descripcion.Trim() + "%' AND CIAEST = 'AC'  AND CIANOM NOT LIKE '%ANTERIOR%' ORDER BY CIANOM ASC");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                tbCompaniaAviacion ioCompania = new tbCompaniaAviacion();

                                ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                                ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                                ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                                ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                                ioCompania.Email = dr["Email"].ToString().Trim();
                                ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                                ioCompania.Celular = dr["Celular"].ToString().Trim();
                                ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                                ioCompania.TipoCompania = dr["TipoCompania"].ToString().Trim();
                                ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                                ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                                ioCompania.DireccionBilling = dr["DireccionBilling"].ToString().Trim();
                                ioCompania.EmailBilling = dr["EmailBilling"].ToString().Trim();
                                ioCompania.TelefonoBilling = dr["TelefonoBilling"].ToString().Trim();
                                ioCompania.RepresentanteLegalBilling = dr["RepresentanteLegalBilling"].ToString().Trim();
                                ioCompania.OidP5 = dr["OidP5"].ToString().Trim();
                                listCompania.Add(ioCompania);
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        listCompania = null;
                    }
                }
            }

            return listCompania;
        }

        public bool ExisteCompaniaPorId(decimal id)
        {
            bool estado = false;
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (id > 0)
            {
                sb.Append("SELECT CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion ");
                sb.Append(" FROM CIAARC WHERE CIAOID = " + id);

                iDB2Command cmd;
                try
                {
                    using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        query = sb.ToString();
                        cmd = new iDB2Command(query, oConexion);
                        oConexion.Open();
                        iDB2DataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            estado = true;
                        }
                        dr.Close();
                    }

                }
                catch (Exception ex)
                {
                    estado = false;
                }
            }

            return estado;
        }


        /// <summary>
        /// Verifica si existe Compañia de Aviación
        /// </summary>
        /// <param name="codigoOaci"></param>
        /// <returns>True o False</returns>
        public bool ExisteCompaniaPorOACI(string codigoOaci)
        {
            bool estado = false;
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (codigoOaci != null)
            {
                if (codigoOaci.Trim().Length > 0)
                {
                    sb.Append("SELECT CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion ");
                    sb.Append(" FROM CIAARC WHERE CIACOD = '" + codigoOaci.Trim() + "'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                estado = true;
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        estado = false;
                    }
                }
            }

            return estado;
        }

        /// <summary>
        /// Verifica si existe el nombre de compania
        /// </summary>
        /// <param name="nombreCompania"></param>
        /// <returns>True o False</returns>
        public bool ExisteCompaniaPorNombre(string nombreCompania)
        {
            bool estado = false;
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (nombreCompania != null)
            {
                if (nombreCompania.Trim().Length > 0)
                {
                    sb.Append("SELECT CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion ");
                    sb.Append(" FROM CIAARC WHERE CIANOM = '" + nombreCompania.Trim().ToUpper() + "'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                estado = true;
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        estado = false;
                    }
                }
            }

            return estado;
        }

        /// <summary>
        /// Valida si existe ruc o nit existe
        /// </summary>
        /// <param name="codigoOaci"></param>
        /// <returns>True o False</returns>
        public bool ExisteCompaniaPorRuc(string rucNit)
        {
            bool estado = false;
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            if (rucNit != null)
            {
                if (rucNit.Trim().Length > 0)
                {
                    sb.Append("SELECT CIACOD as CodigoOaci, CIACO2 as CodigoIata, CIACO3 as CodigoNumeroCia, CIANOM as NombreCompaniaAviacion ");
                    sb.Append(" FROM CIAARC WHERE CIARUC = '" + rucNit.Trim().ToUpper() + "'");

                    iDB2Command cmd;
                    try
                    {
                        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                        {
                            query = sb.ToString();
                            cmd = new iDB2Command(query, oConexion);
                            oConexion.Open();
                            iDB2DataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                estado = true;
                            }
                            dr.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        estado = false;
                    }
                }
            }

            return estado;
        }

        /// <summary>
        /// Metodo inserta una Compañia
        /// </summary>
        /// <param name="oCompania"></param>
        /// <returns>Tru o False</returns>
        public bool RegistrarCompania(tbCompaniaAviacion oCompania)
        {
            bool respuesta = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "INSERT INTO CIAARC (CIAOID, CIACOD, CIANOM, CIACO4, CIACIU, CIADIR, CIARUC, CIAEMA, CIATEL, CIAREP, CIAEST, CIAUSU, CIAFEC, CIAHOR)"
                        + " VALUES(@IdCompania, @CodigoOaci, @NombreCompaniaAviacion, @CodigoPais,  @CodigoCiudad, @Direccion, @RucCompania, @Email, @Telefono, @RepresentanteLegal, @EstadoCompania, @UsuarioCreado, @FechaCreado, @HoraCreado)";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@IdCompania"].Value = GeneraSecuencialCompaniaAviacion();
                    cmd.Parameters["@CodigoOaci"].Value = campoNull(oCompania.CodigoOaci).Trim().ToUpper();
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(oCompania.NombreCompaniaAviacion).ToUpper();
                    cmd.Parameters["@CodigoPais"].Value = campoNull(oCompania.CodigoPais);
                    cmd.Parameters["@CodigoCiudad"].Value = campoNull(oCompania.CodigoCiudad);
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).ToUpper();
                    cmd.Parameters["@RucCompania"].Value = campoNull(oCompania.RucCompania).ToUpper();
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Telefono"].Value = campoNull(oCompania.Telefono).Trim();
                    cmd.Parameters["@RepresentanteLegal"].Value = campoNull(oCompania.RepresentanteLegal).ToUpper();
                    cmd.Parameters["@EstadoCompania"].Value = oCompania.EstadoCompania.ToUpper();
                    cmd.Parameters["@UsuarioCreado"].Value = campoNull(oCompania.UsuarioCreado);
                    cmd.Parameters["@FechaCreado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraCreado"].Value = DateTime.Now.ToString("HH:mm:ss");

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return respuesta;
        }
        public bool RegistrarCompaniaBilling(tbCompaniaAviacion oCompania)
        {
            bool respuesta = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "INSERT INTO CIAARC (CIAOID, CIACOD, CIANOM, CIACO4, CIACIU, CIADI3, CIARUC, CIAEM1, CIATE2, CIARE1, CIAEST, CIAUSU, CIAFEC, CIAHOR)"
                        + " VALUES(@IdCompania, @CodigoOaci, @NombreCompaniaAviacion, @CodigoPais, @CodigoCiudad, @Direccion, @RucCompania, @Email, @Telefono, @RepresentanteLegal, @EstadoCompania, @UsuarioCreado, @FechaCreado, @HoraCreado)";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@IdCompania"].Value = GeneraSecuencialCompaniaAviacion();
                    cmd.Parameters["@CodigoOaci"].Value = campoNull(oCompania.CodigoOaci).Trim().ToUpper();
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(oCompania.NombreCompaniaAviacion).Trim().ToUpper();
                    cmd.Parameters["@CodigoPais"].Value = campoNull(oCompania.CodigoPais);
                    cmd.Parameters["@CodigoCiudad"].Value = campoNull(oCompania.CodigoCiudad);
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).Trim().ToUpper();
                    cmd.Parameters["@RucCompania"].Value = campoNull(oCompania.RucCompania).Trim().ToUpper();
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Telefono"].Value = campoNull(oCompania.Telefono).Trim();
                    cmd.Parameters["@RepresentanteLegal"].Value = campoNull(oCompania.RepresentanteLegal).Trim().ToUpper();
                    cmd.Parameters["@EstadoCompania"].Value = oCompania.EstadoCompania.ToUpper();
                    cmd.Parameters["@UsuarioCreado"].Value = campoNull(oCompania.UsuarioCreado);
                    cmd.Parameters["@FechaCreado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraCreado"].Value = DateTime.Now.ToString("HH:mm:ss");

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return respuesta;
        }


        public bool ActualizaCompania(tbCompaniaAviacion oCompania)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE CIAARC "
                        + " SET CIACOD = @CodigoOaci, CIANOM = @NombreCompaniaAviacion, CIADIR = @Direccion, CIARUC = @RucCompania, CIAEMA = @Email, CIACEL = @Celular, "
                        + " CIATEL = @Telefono, CIAREP = @RepresentanteLegal, CIAUS1 = @UsuarioModificacion, CIAFE1 = @FechaModificacion, CIAHO1 = @HoraModificaciono"
                        + " WHERE CIAOID = @IdCompania";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@CodigoOaci"].Value = campoNull(oCompania.CodigoOaci).ToUpper();
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(oCompania.NombreCompaniaAviacion).Trim().ToUpper();
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).Trim();
                    cmd.Parameters["@RucCompania"].Value = campoNull(oCompania.RucCompania);
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Celular"].Value = campoNull(oCompania.Celular).Trim();
                    cmd.Parameters["@Telefono"].Value = oCompania.Telefono.Trim();
                    cmd.Parameters["@RepresentanteLegal"].Value = campoNull(oCompania.RepresentanteLegal.Trim());
                    cmd.Parameters["@UsuarioModificacion"].Value = oCompania.UsuarioModificado;
                    cmd.Parameters["@FechaModificacion"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificaciono"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@IdCompania"].Value = oCompania.IdCompania;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizaCompaniaOperadoraSobreVuelo(tbCompaniaAviacion oCompania)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE CIAARC "
                        + " SET CIADIR = @Direccion, CIAEMA = @Email, "
                        + " CIATEL = @Telefono, CIAUS1 = @UsuarioModificacion, CIAFE1 = @FechaModificacion, CIAHO1 = @HoraModificaciono"
                        + " WHERE CIAOID = @IdCompania";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).Trim();
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Telefono"].Value = oCompania.Telefono.Trim();
                    cmd.Parameters["@UsuarioModificacion"].Value = oCompania.UsuarioModificado;
                    cmd.Parameters["@FechaModificacion"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificaciono"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@IdCompania"].Value = oCompania.IdCompania;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizaCompaniaBillingSobreVuelo(tbCompaniaAviacion oCompania)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE CIAARC "
                        + " SET CIADI3 = @Direccion, CIAEM1 = @Email, "
                        + " CIATE2 = @Telefono, CIAUS1 = @UsuarioModificacion, CIAFE1 = @FechaModificacion, CIAHO1 = @HoraModificaciono"
                        + " WHERE CIAOID = @IdCompania";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).Trim();
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Telefono"].Value = oCompania.Telefono.Trim();
                    cmd.Parameters["@UsuarioModificacion"].Value = oCompania.UsuarioModificado;
                    cmd.Parameters["@FechaModificacion"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificaciono"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@IdCompania"].Value = oCompania.IdCompania;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo Graba Compañia de Aviación
        /// </summary>
        /// <param name="oCompania"></param>
        /// <returns></returns>
        public bool GrabarCompaniaAviacion(tbCompaniaAviacion oCompania)
        {
            bool respuesta = true;

            try
            {
                if (ExisteCompaniaPorId(decimal.Parse(oCompania.IdCompania)))
                {
                    respuesta = ActualizaCompania(oCompania);
                }
                else
                {
                    respuesta = RegistrarCompania(oCompania);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }

        private Int32 GeneraSecuencialCompaniaAviacion()
        {
            string query = "SELECT IFNULL(max(CIAOID), 0) + 1 AS Secuencial FROM CIAARC";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }

        private Int32 GeneraOACICompaniaAviacion()
        {
            string query = "SELECT IFNULL(max(CIAOID), 0) + 1 AS Secuencial FROM CIAARC";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }

        #region "Compañia Temporal"

        public tbCompaniaAviacionTemporal CompaniaAviacionTemporalPorId(int idCompania)
        {
            tbCompaniaAviacionTemporal ioCompania = new tbCompaniaAviacionTemporal();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            try
            {
                sb.Append("SELECT ifnull(CIAOI3, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACO1)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO5)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO6)), '') as CodigoNumeroCia, ");
                sb.Append(" ifnull(rtrim(ltrim(CIANO2)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI2)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADI4)), '') as Direccion, ");
                sb.Append(" ifnull(rtrim(ltrim(CIARU1)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEM2)), '') as Email, ifnull(rtrim(ltrim(CIATE3)), '') as Telefono, ifnull(rtrim(ltrim(CIACE2)), '') as Celular, ");
                sb.Append(" ifnull(rtrim(ltrim(CIACAR)), '') as Cargo, ifnull(rtrim(ltrim(CIARE3)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO3)), '') as NombreCorto,  ");
                sb.Append(" ifnull(rtrim(ltrim(CIACO8)), '') as CodigoPais, ifnull(rtrim(ltrim(CIAOI5)), '') as OidPais , ifnull(rtrim(ltrim(CIACI1)), '') as CodigoCiudad,");
                sb.Append(" ifnull(rtrim(ltrim(CIAUS2)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFE2)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHO2)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS3)), '') as UsuarioModificado,");
                sb.Append(" ifnull(rtrim(ltrim(CIAFE3)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO3)), '') as HoraModificado, ifnull(rtrim(ltrim(CIAES3)), '') as EstadoTramite,");
                sb.Append(" UPPER(ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'CIAES3' and VALVAL = CIAES3), '')) as DescripcionEstadoTramite ");
                sb.Append(" FROM CIAAR1 WHERE CIAOI3 = " + idCompania);
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                        ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                        ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                        ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                        ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                        ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                        ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                        ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                        ioCompania.Email = dr["Email"].ToString().Trim();
                        ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                        ioCompania.Celular = dr["Celular"].ToString().Trim();
                        ioCompania.Cargo = dr["Cargo"].ToString().Trim();
                        ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                        ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                        ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                        ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                        ioCompania.OidPais = Int32.Parse(dr["OidPais"].ToString());
                        ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                        ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                        ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                        ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                        ioCompania.EstadoTramite = dr["EstadoTramite"].ToString();
                        ioCompania.DescripcionEstadoTramite = dr["DescripcionEstadoTramite"].ToString();

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ioCompania;
        }

        public List<tbCompaniaAviacionTemporal> ListarCompaniaAviacionTemporalTodos(string codUsuario)
        {
            List<tbCompaniaAviacionTemporal> listCompania = new List<tbCompaniaAviacionTemporal>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            try
            {
                sb.Append("SELECT ifnull(CIAOI3, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACO1)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO5)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO6)), '') as CodigoNumeroCia, ");
                sb.Append(" ifnull(rtrim(ltrim(CIANO2)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI2)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADI4)), '') as Direccion, ");
                sb.Append(" ifnull(rtrim(ltrim(CIARU1)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEM2)), '') as Email, ifnull(rtrim(ltrim(CIATE3)), '') as Telefono, ifnull(rtrim(ltrim(CIACE2)), '') as Celular, ");
                sb.Append(" ifnull(rtrim(ltrim(CIACAR)), '') as Cargo, ifnull(rtrim(ltrim(CIARE3)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO3)), '') as NombreCorto,  ");
                sb.Append(" ifnull(rtrim(ltrim(CIACO8)), '') as CodigoPais, (SELECT ifnull(rtrim(ltrim(OPPDES)), '') AS DESCRIPCIONPAIS FROM OPPAR1 WHERE  OPPCOD = CIACO8) AS DescripcionPais, ifnull(rtrim(ltrim(CIACI1)), '') as CodigoCiudad,");
                sb.Append(" ifnull(rtrim(ltrim(CIAUS2)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFE2)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHO2)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS3)), '') as UsuarioModificado,");
                sb.Append(" ifnull(rtrim(ltrim(CIAFE3)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO3)), '') as HoraModificado, ifnull(rtrim(ltrim(CIAES3)), '') as EstadoTramite,");
                sb.Append(" UPPER(ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'CIAES3' and VALVAL = CIAES3), '')) as DescripcionEstadoTramite, (SELECT ifnull(rtrim(ltrim(OPPDES)), '') FROM OPPAR1 WHERE OPPCOD = CIACO8) as DescripcionPais ");
                sb.Append(" FROM CIAAR1 WHERE CIAUS2 = '" + codUsuario + "'");
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCompaniaAviacionTemporal ioCompania = new tbCompaniaAviacionTemporal();

                        ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                        ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                        ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                        ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                        ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                        ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                        ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                        ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                        ioCompania.Email = dr["Email"].ToString().Trim();
                        ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                        ioCompania.Celular = dr["Celular"].ToString().Trim();
                        ioCompania.Cargo = dr["Cargo"].ToString().Trim();
                        ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                        ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                        ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                        ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                        ioCompania.DescripcionPais = dr["DescripcionPais"].ToString().Trim();
                        ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                        ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                        ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                        ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                        ioCompania.EstadoTramite = dr["EstadoTramite"].ToString();
                        ioCompania.DescripcionEstadoTramite = dr["DescripcionEstadoTramite"].ToString();
                        ioCompania.DescripcionPais = dr["DescripcionPais"].ToString();
                        listCompania.Add(ioCompania);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCompania;
        }

        public Int32 CantidadCompaniaAviacionTemporalPorEnviadoPendientes()
        {
            Int32 cantidadPendientes = 0;
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            try
            {
                sb.Append("SELECT COUNT(*) AS Cantidad ");
                sb.Append(" FROM CIAAR1 WHERE CIAES3 IN('02', '03')");
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cantidadPendientes = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidadPendientes;
        }


        public List<tbCompaniaAviacionTemporal> ListarCompaniaAviacionTemporalPorEnviado()
        {
            List<tbCompaniaAviacionTemporal> listCompania = new List<tbCompaniaAviacionTemporal>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            try
            {
                sb.Append("SELECT ifnull(CIAOI3, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACO1)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO5)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO6)), '') as CodigoNumeroCia, ");
                sb.Append(" ifnull(rtrim(ltrim(CIANO2)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI2)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADI4)), '') as Direccion, ");
                sb.Append(" ifnull(rtrim(ltrim(CIARU1)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEM2)), '') as Email, ifnull(rtrim(ltrim(CIATE3)), '') as Telefono, ifnull(rtrim(ltrim(CIACE2)), '') as Celular, ");
                sb.Append(" ifnull(rtrim(ltrim(CIACAR)), '') as Cargo, ifnull(rtrim(ltrim(CIARE3)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO3)), '') as NombreCorto,  ");
                sb.Append(" ifnull(rtrim(ltrim(CIACO8)), '') as CodigoPais, ifnull(rtrim(ltrim(CIACI1)), '') as CodigoCiudad,");
                sb.Append(" ifnull(rtrim(ltrim(CIAUS2)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFE2)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHO2)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS3)), '') as UsuarioModificado,");
                sb.Append(" ifnull(rtrim(ltrim(CIAFE3)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO3)), '') as HoraModificado, ifnull(rtrim(ltrim(CIAES3)), '') as EstadoTramite,");
                sb.Append(" UPPER(ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'CIAES3' and VALVAL = CIAES3), '')) as DescripcionEstadoTramite, (SELECT ifnull(rtrim(ltrim(OPPDES)), '') FROM OPPAR1 WHERE OPPCOD = CIACO8) as DescripcionPais, ");
                sb.Append(" ifnull(rtrim(ltrim(CIAOB1)), '') as ObservacionTramite ");
                sb.Append(" FROM CIAAR1 WHERE CIAES3  <> '01' AND  CIAES3  <> '04'");
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCompaniaAviacionTemporal ioCompania = new tbCompaniaAviacionTemporal();

                        ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                        ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                        ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                        ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                        ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                        ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                        ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                        ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                        ioCompania.Email = dr["Email"].ToString().Trim();
                        ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                        ioCompania.Celular = dr["Celular"].ToString().Trim();
                        ioCompania.Cargo = dr["Cargo"].ToString().Trim();
                        ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                        ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                        ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                        ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                        ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                        ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                        ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                        ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                        ioCompania.EstadoTramite = dr["EstadoTramite"].ToString();
                        ioCompania.DescripcionEstadoTramite = dr["DescripcionEstadoTramite"].ToString();
                        ioCompania.DescripcionPais = dr["DescripcionPais"].ToString();
                        ioCompania.ObservacionTramite = dr["ObservacionTramite"].ToString();
                        listCompania.Add(ioCompania);
                    }
                    dr.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCompania;
        }

        public List<tbCompaniaAviacionTemporal> ListarCompaniaAviacionTemporalPorEstado(string estado)
        {
            List<tbCompaniaAviacionTemporal> listCompania = new List<tbCompaniaAviacionTemporal>();
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            try
            {
                sb.Append("SELECT ifnull(CIAOI3, 0) AS IdCompania, ifnull(rtrim(ltrim(CIACO1)), '') as CodigoOaci, ifnull(rtrim(ltrim(CIACO5)), '') as CodigoIata, ifnull(rtrim(ltrim(CIACO6)), '') as CodigoNumeroCia, ");
                sb.Append(" ifnull(rtrim(ltrim(CIANO2)), '') as NombreCompaniaAviacion,  ifnull(rtrim(ltrim(CIATI2)), '') as TipoIdentificacion, ifnull(rtrim(ltrim(CIADI4)), '') as Direccion, ");
                sb.Append(" ifnull(rtrim(ltrim(CIARU1)), '') as RucCompania, ifnull(rtrim(ltrim(CIAEM2)), '') as Email, ifnull(rtrim(ltrim(CIATE3)), '') as Telefono, ifnull(rtrim(ltrim(CIACE2)), '') as Celular, ");
                sb.Append(" ifnull(rtrim(ltrim(CIACAR)), '') as Cargo, ifnull(rtrim(ltrim(CIARE3)), '') as RepresentanteLegal, ifnull(rtrim(ltrim(CIANO3)), '') as NombreCorto,  ");
                sb.Append(" ifnull(rtrim(ltrim(CIACO8)), '') as CodigoPais, ifnull(rtrim(ltrim(CIACI1)), '') as CodigoCiudad,");
                sb.Append(" ifnull(rtrim(ltrim(CIAUS2)), '') as UsuarioCreado, ifnull(rtrim(ltrim(CIAFE2)), '') as FechaCrado, ifnull(rtrim(ltrim(CIAHO2)), '') as HoraCreado,  ifnull(rtrim(ltrim(CIAUS3)), '') as UsuarioModificado,");
                sb.Append(" ifnull(rtrim(ltrim(CIAFE3)), '') as FechaModificado, ifnull(rtrim(ltrim(CIAHO3)), '') as HoraModificado, ifnull(rtrim(ltrim(CIAES3)), '') as EstadoTramite,");
                sb.Append(" UPPER(ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'CIAES3' and VALVAL = CIAES3), '')) as DescripcionEstadoTramite, (SELECT ifnull(rtrim(ltrim(OPPDES)), '') FROM OPPAR1 WHERE OPPCOD = CIACO8) as DescripcionPais ");
                sb.Append(" FROM CIAAR1 WHERE CIAES3 = '" + estado + "'");
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCompaniaAviacionTemporal ioCompania = new tbCompaniaAviacionTemporal();

                        ioCompania.IdCompania = dr["IdCompania"].ToString().Trim();
                        ioCompania.CodigoOaci = dr["CodigoOaci"].ToString().Trim();
                        ioCompania.CodigoIata = dr["CodigoIata"].ToString().Trim();
                        ioCompania.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString().Trim();
                        ioCompania.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString().Trim();
                        ioCompania.Direccion = dr["Direccion"].ToString().Trim();
                        ioCompania.TipoIdentificacion = dr["TipoIdentificacion"].ToString().Trim();
                        ioCompania.RucCompania = dr["RucCompania"].ToString().Trim();
                        ioCompania.Email = dr["Email"].ToString().Trim();
                        ioCompania.Telefono = dr["Telefono"].ToString().Trim();
                        ioCompania.Celular = dr["Celular"].ToString().Trim();
                        ioCompania.Cargo = dr["Cargo"].ToString().Trim();
                        ioCompania.RepresentanteLegal = dr["RepresentanteLegal"].ToString().Trim();
                        ioCompania.NombreCorto = dr["NombreCorto"].ToString().Trim();
                        ioCompania.CodigoCiudad = dr["CodigoCiudad"].ToString().Trim();
                        ioCompania.CodigoPais = dr["CodigoPais"].ToString().Trim();
                        ioCompania.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        ioCompania.FechaCrado = dr["FechaCrado"].ToString();
                        ioCompania.HoraCreado = dr["HoraCreado"].ToString();
                        ioCompania.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        ioCompania.FechaModificado = dr["FechaModificado"].ToString();
                        ioCompania.HoraModificado = dr["HoraModificado"].ToString();
                        ioCompania.EstadoTramite = dr["EstadoTramite"].ToString();
                        ioCompania.DescripcionEstadoTramite = dr["DescripcionEstadoTramite"].ToString();
                        ioCompania.DescripcionPais = dr["DescripcionPais"].ToString();
                        listCompania.Add(ioCompania);
                    }
                    dr.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCompania;
        }

        /// <summary>
        /// Metodo guarda un registro nuevo
        /// </summary>
        /// <param name="oCompania"></param>
        /// <returns>True o False</returns>
        public bool InsertarCompaniaTemporal(tbCompaniaAviacionTemporal oCompania)
        {
            bool resultado = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            tbSistema oSistema = new tbSistema();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    oCompania.IdCompania = GeneraSecuencialCompaniaAviacionTemporal().ToString();
                    queryInsertar = "INSERT INTO CIAAR1 (CIAOI3, CIANO2, CIANO3, CIARU1, CIACO5, CIACO1, CIACO8, CIADI4, CIAEM2, CIATE3, CIACE2, CIACAR, CIARE3, CIAES3, CIAUS2, CIAFE2, CIAHO2, CIAOI5)"
                        + " VALUES(@IdCompania, @NombreCompaniaAviacion, @NombreCorto, @RucCompania,  @CodigoIata, @CodigoOaci, @CodigoPais, @Direccion, @Email, @Telefono, @Celular, @Cargo, @RepresentanteLegal, @EstadoTramite, @UsuarioCreado, @FechaCreado, @HoraCreado, @OidPais)";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@IdCompania"].Value = oCompania.IdCompania;
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(oCompania.NombreCompaniaAviacion).ToUpper();
                    cmd.Parameters["@NombreCorto"].Value = campoNull(oCompania.NombreCorto).ToUpper();
                    cmd.Parameters["@RucCompania"].Value = campoNull(oCompania.RucCompania).Trim().ToUpper();
                    cmd.Parameters["@CodigoIata"].Value = campoNull(oCompania.CodigoIata).Trim().ToUpper();
                    cmd.Parameters["@CodigoOaci"].Value = campoNull(oCompania.CodigoOaci).Trim().ToUpper();
                    cmd.Parameters["@CodigoPais"].Value = campoNull(oCompania.CodigoPais);
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).ToUpper();
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Telefono"].Value = campoNull(oCompania.Telefono).Trim();
                    cmd.Parameters["@Celular"].Value = campoNull(oCompania.Celular).Trim();
                    cmd.Parameters["@Cargo"].Value = campoNull(oCompania.Cargo).Trim();
                    cmd.Parameters["@RepresentanteLegal"].Value = campoNull(oCompania.RepresentanteLegal).ToUpper();
                    cmd.Parameters["@EstadoTramite"].Value = campoNull(oCompania.EstadoTramite);
                    cmd.Parameters["@UsuarioCreado"].Value = campoNull(oCompania.UsuarioCreado);
                    cmd.Parameters["@FechaCreado"].Value = oSistema.FechaSistema;
                    cmd.Parameters["@HoraCreado"].Value = oSistema.HoraSistema;
                    cmd.Parameters["@OidPais"].Value = oCompania.OidPais;

                    resultado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public bool ActualizaCompaniaTemporal(tbCompaniaAviacionTemporal oCompania)
        {
            bool resultado = false;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            tbSistema oSistema = new tbSistema();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE CIAAR1 "
                        + " SET CIANO2 = @NombreCompaniaAviacion, CIANO3 = @NombreCorto, CIARU1 = @RucCompania, CIACO5 = @CodigoIata, "
                        + " CIACO1 = @CodigoOaci, CIACO8 = @CodigoPais, CIADI4 = @Direccion, CIAEM2 = @Email, CIATE3 = @Telefono, CIACE2 = @Celular, "
                        + " CIACAR = @Cargo, CIARE3 = @RepresentanteLegal, CIAUS3 = @UsuarioModificado, CIAFE3 = @FechaModificado, CIAHO3 = @HoraModificado, CIAOI5 = @OidPais"
                        + " WHERE CIAOI3 = @IdCompania";
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(oCompania.NombreCompaniaAviacion).ToUpper();
                    cmd.Parameters["@NombreCorto"].Value = campoNull(oCompania.NombreCorto).ToUpper();
                    cmd.Parameters["@RucCompania"].Value = campoNull(oCompania.RucCompania).Trim().ToUpper();
                    cmd.Parameters["@CodigoIata"].Value = campoNull(oCompania.CodigoIata).Trim().ToUpper();
                    cmd.Parameters["@CodigoOaci"].Value = campoNull(oCompania.CodigoOaci).Trim().ToUpper();
                    cmd.Parameters["@CodigoPais"].Value = campoNull(oCompania.CodigoPais);
                    cmd.Parameters["@Direccion"].Value = campoNull(oCompania.Direccion).ToUpper();
                    cmd.Parameters["@Email"].Value = campoNull(oCompania.Email).Trim();
                    cmd.Parameters["@Telefono"].Value = campoNull(oCompania.Telefono).Trim();
                    cmd.Parameters["@Celular"].Value = campoNull(oCompania.Celular).Trim();
                    cmd.Parameters["@Cargo"].Value = campoNull(oCompania.Cargo).Trim();
                    cmd.Parameters["@RepresentanteLegal"].Value = campoNull(oCompania.RepresentanteLegal).ToUpper();
                    cmd.Parameters["@UsuarioModificado"].Value = campoNull(oCompania.UsuarioModificado);
                    cmd.Parameters["@FechaModificado"].Value = oSistema.FechaSistema;
                    cmd.Parameters["@HoraModificado"].Value = oSistema.HoraSistema;
                    cmd.Parameters["@OidPais"].Value = oCompania.OidPais;
                    cmd.Parameters["@IdCompania"].Value = Int32.Parse(oCompania.IdCompania);
                    resultado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }


        /// <summary>
        /// Anula la solicitud 
        /// </summary>
        /// <param name="idCompania"></param>
        /// <param name="estadoTramite"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool AnulaSolicitudCompaniaTemporal(int idCompania, string estadoTramite, string usuario)
        {
            bool resultado = false;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            tbSistema oSistema = new tbSistema();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE CIAAR1 "
                        + " SET CIAES3 = @estadoTramite, CIAUS3 = @UsuarioModificado, CIAFE3 = @FechaModificado, CIAHO3 = @HoraModificado "
                        + " WHERE CIAOI3 = @IdCompania";
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@estadoTramite"].Value = campoNull(estadoTramite).ToUpper();
                    cmd.Parameters["@UsuarioModificado"].Value = campoNull(usuario);
                    cmd.Parameters["@FechaModificado"].Value = oSistema.FechaSistema;
                    cmd.Parameters["@HoraModificado"].Value = oSistema.HoraSistema;
                    cmd.Parameters["@IdCompania"].Value = idCompania;
                    resultado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Envia la solicitud para su tramite
        /// </summary>
        /// <param name="idCompania"></param>
        /// <param name="observacionTramite"></param>
        /// <param name="estadoTramite"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ActualizaEstadoCompaniaTemporal(int idCompania, string observacionTramite, string estadoTramite, string usuario, tbCompaniaAviacionTemporal ociaTemporal)
        {
            bool resultado = false;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            tbSistema oSistema = new tbSistema();
            string codigoAoci = string.Empty;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    if (ociaTemporal.CodigoOaci == "")
                    {
                        codigoAoci = GeneraCodigoOaci("COMPANIA", "CODOACI", usuario);
                    }
                    else
                        codigoAoci = ociaTemporal.CodigoOaci;
                    queryUpdate = "UPDATE CIAAR1 "
                        + " SET CIAOB1 = @observacionTramite, CIAES3 = @estadoTramite, CIAUS4 = @UsuarioAutoriza, CIAFE4 = @FechaAutoriza, CIAHO4 = @HoraAutoriza, CIACO1 = @codigoAoci "
                    + " WHERE CIAOI3 = @IdCompania";
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@observacionTramite"].Value = campoNull(observacionTramite).ToUpper();
                    cmd.Parameters["@estadoTramite"].Value = campoNull(estadoTramite).ToUpper();
                    cmd.Parameters["@UsuarioAutoriza"].Value = campoNull(usuario);
                    cmd.Parameters["@FechaAutoriza"].Value = oSistema.FechaSistema;
                    cmd.Parameters["@HoraAutoriza"].Value = oSistema.HoraSistema;
                    cmd.Parameters["@IdCompania"].Value = idCompania;
                    cmd.Parameters["@codigoAoci"].Value = codigoAoci;
                    resultado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public bool GrabaCompaniaP5(tbCompaniaAviacionTemporal ocompaniaTemporal)
        {
            bool estadoGrabar = false;
            Int32 secuencial = 0;
            StringBuilder sbCia = new StringBuilder();
            string query = string.Empty;
            try
            {
                secuencial = ObtieneSecuencialApp("COMPANIA", "OID", ocompaniaTemporal.UsuarioCreado);
                sbCia.Append("INSERT INTO COMPANIA (OID, OIDPAIS, OIDCIUDAD, SIGLAS_IATA, CODIGO_OACI, NOMBRE, DIRECCION, TELEFONO, FAX, EMAIL, REPRESENTANTEPERMI, OIDCTACONT, CARGOREPRESENPERMI, ");
                sbCia.Append(" NACIONAL_EXTRANJER, TIPOOBJETO, TIPOCOMPANIA, USUARIOCREA, FECHACREA, PROPIETARIO, OIDCTACONTANT, NOMBRECOMERCIAL, RUC, OIDCIUDADREPLEGAL, CODEMPRESA)");
                sbCia.Append(" VALUES (" + secuencial + ", " + ocompaniaTemporal.OidPais + ", 0, '" + ocompaniaTemporal.CodigoIata + "', '" + ocompaniaTemporal.CodigoOaci + "', '" + ocompaniaTemporal.NombreCompaniaAviacion + "', '" + ocompaniaTemporal.Direccion + "', '" + ocompaniaTemporal.Telefono + "', ");
                sbCia.Append("'" + ocompaniaTemporal.Celular + "', '" + ocompaniaTemporal.Email + "', '" + ocompaniaTemporal.RepresentanteLegal + "'," + 3220 + ", '" + ocompaniaTemporal.Cargo + "', 'E', 'OE', 'C', '" + ocompaniaTemporal.UsuarioCreado + "', ");
                sbCia.Append(" CURRENT TIMESTAMP" + ", 'N', 3220, '" + ocompaniaTemporal.NombreCorto + "', '" + campoNull(ocompaniaTemporal.RucCompania) + "', 0, '" + ocompaniaTemporal.CodigoOaci + "')");
                query = sbCia.ToString();
                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    ocompaniaTemporal.OidP5 = secuencial.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }

        public bool VerificaCompaniaExisteSeciencialP5(int secuencial)
        {
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            bool estadoCia = false;
            try
            {
                sbSol.Append("SELECT * FROM COMPANIA WHERE OID = " + secuencial);

                query = sbSol.ToString();
                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        estadoCia = true;
                    }
                    dr.Close();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoCia;
        }

        /// <summary>
        /// Inserta el registro en Power 9 de Cia
        /// </summary>
        /// <param name="ocompaniaTemporal"></param>
        /// <returns></returns>
        public bool GrabaCompaniaAeronave(tbCompaniaAviacionTemporal ocompaniaTemporal)
        {
            bool resultado = false;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            tbSistema oSistema = new tbSistema();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "INSERT INTO CIAARC(CIAOID,	CIACOD, CIACO2,	CIANOM,	CIADIR,	CIARUC,	CIAEMA,	CIATEL,	CIACEL,	CIANO1,	CIATIP,	CIAEST,	CIAOI1,	CIAOI2,	CIACO4, CIAUSU, CIAFEC, CIAHOR)"
                        + "VALUES ((SELECT MAX(ifnull(CIAOID, 0)) + 1 AS SECUENCIAL FROM DGACDAT.CIAARC), @CodigoOaci, @CodigoIata, @NombreCompaniaAviacion, @Direccion, @RucCompania, "
                        + "@Email, @Telefono, @Celular, @NombreCorto, '01',  'AC', 3220, @OidP5, @CodigoPais, @UsuarioCreado, @FechaAutoriza, @HoraAutoriza)";
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@CodigoOaci"].Value = campoNull(ocompaniaTemporal.CodigoOaci).ToUpper();
                    cmd.Parameters["@CodigoIata"].Value = campoNull(ocompaniaTemporal.CodigoIata).ToUpper();
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(ocompaniaTemporal.NombreCompaniaAviacion);
                    cmd.Parameters["@Direccion"].Value = campoNull(ocompaniaTemporal.Direccion);
                    cmd.Parameters["@RucCompania"].Value = campoNull(ocompaniaTemporal.RucCompania);
                    cmd.Parameters["@Email"].Value = campoNull(ocompaniaTemporal.Email);
                    cmd.Parameters["@Telefono"].Value = campoNull(ocompaniaTemporal.Telefono);
                    cmd.Parameters["@Celular"].Value = campoNull(ocompaniaTemporal.Celular);
                    cmd.Parameters["@NombreCorto"].Value = campoNull(ocompaniaTemporal.NombreCorto);
                    cmd.Parameters["@OidP5"].Value = campoNull(ocompaniaTemporal.OidP5);
                    cmd.Parameters["@CodigoPais"].Value = campoNull(ocompaniaTemporal.CodigoPais);
                    cmd.Parameters["@UsuarioCreado"].Value = campoNull(ocompaniaTemporal.UsuarioCreado);
                    cmd.Parameters["@FechaAutoriza"].Value = oSistema.FechaSistema;
                    cmd.Parameters["@HoraAutoriza"].Value = oSistema.HoraSistema;
                    resultado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public Int32 ObtieneSecuencialApp(string tabla, string campo, string usuarioMod)
        {
            Int32 secuencial = 0;
            bool estado = false;
            try
            {
                do
                {
                    secuencial = SecuencialOACIP5(tabla, campo);
                    if (ActualizaSecuencialApp(tabla, campo, usuarioMod, secuencial))
                        estado = VerificaCompaniaExisteSeciencialP5(secuencial);

                } while (estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return secuencial;
        }


        public tbSecuencialAppP5 SecuencialAppP5(string tabla, string campo)
        {
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            tbSecuencialAppP5 oSecuencialAppP5 = new tbSecuencialAppP5();
            try
            {
                sbSol.Append("SELECT OID, TABLA, CAMPO, USUARIOMODIFICA, FECHAMODFICA, NUMERO, TIPO FROM SECUENCIALAPP");
                sbSol.Append(" WHERE TABLA = '" + tabla + "' AND CAMPO = '" + campo + "'");
                query = sbSol.ToString();
                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oSecuencialAppP5.Oid = Int32.Parse(dr["OID"].ToString());
                        oSecuencialAppP5.Tabla = dr["TABLA"].ToString();
                        oSecuencialAppP5.Campo = dr["CAMPO"].ToString();
                        oSecuencialAppP5.UsuarioModifica = dr["USUARIOMODIFICA"].ToString();
                        oSecuencialAppP5.FechaModifica = dr["FECHAMODFICA"].ToString();
                        oSecuencialAppP5.Numero = Int32.Parse(dr["NUMERO"].ToString());
                        oSecuencialAppP5.Tipo = dr["TIPO"].ToString();
                    }
                    dr.Close();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oSecuencialAppP5;
        }

        public string GeneraCodigoOaci(string tabla, string campo, string usuarioMod)
        {
            Int32 secuencial = 0;
            bool estado = false;
            string codOaci = string.Empty;

            try
            {
                do
                {
                    secuencial = SecuencialOACIP5(tabla, campo);
                    if (ActualizaSecuencialApp(tabla, campo, usuarioMod, secuencial))
                    {
                        var oSecuencialApp = CD_SecuencialAppP5.Instancia.SecuencialAppPorTablaCampo(tabla, campo);
                        codOaci = oSecuencialApp.Tipo.Trim() + secuencial.ToString();
                        estado = VerificaCoidgoOACIExisteP5(codOaci);
                    }


                } while (estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return codOaci;
        }

        /// <summary>
        /// Verifica si el codigo OACI existe 
        /// </summary>
        /// <param name="codOaci"></param>
        /// <returns></returns>
        public bool VerificaCoidgoOACIExisteP5(string codOaci)
        {
            StringBuilder sb = new StringBuilder();
            string query = string.Empty;
            bool resultado = false;
            try
            {
                sb.Append("SELECT CIACOD FROM CIAARC  WHERE CIACOD = '" + codOaci + "'");
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    query = sb.ToString();
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultado = true;
                    }
                    dr.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public Int32 SecuencialOACIP5(string tabla, string campo)
        {
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            Int32 secuencial = 0;
            try
            {
                sbSol.Append("SELECT (COALESCE(NUMERO, 0) + 1) AS Secuencial FROM SECUENCIALAPP ");
                sbSol.Append(" WHERE TABLA = '" + tabla + "' AND CAMPO = '" + campo + "'");
                query = sbSol.ToString();
                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return secuencial;
        }

        public bool ActualizaSecuencialApp(string tabla, string campo, string usuarioMod, Int32 numero)
        {
            StringBuilder sbMat = new StringBuilder();
            string query = string.Empty;

            bool estadoGrabar = false;
            try
            {
                sbMat.Append("UPDATE SECUENCIALAPP ");
                sbMat.Append(" SET USUARIOMODIFICA = '" + usuarioMod + "', FECHAMODFICA = CURRENT TIMESTAMP, NUMERO = " + numero);
                sbMat.Append(" WHERE TABLA = '" + tabla + "' AND CAMPO = '" + campo + "'");
                query = sbMat.ToString();
                OdbcCommand cmd;
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    estadoGrabar = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadoGrabar;
        }

        private Int32 GeneraSecuencialCompaniaAviacionTemporal()
        {
            string query = "SELECT IFNULL(max(CIAOI3), 0) + 1 AS Secuencial FROM CIAAR1";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }
        #endregion


        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

    }
}
