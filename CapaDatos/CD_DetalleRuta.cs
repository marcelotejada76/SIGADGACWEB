using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_DetalleRuta
    {
        public static CD_DetalleRuta _instancia = null;
        //string biblioteca = "DGACDAT";
        private CD_DetalleRuta()
        {

        }

        public static CD_DetalleRuta Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleRuta();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo obtiene el detallede ruta
        /// </summary>
        /// <param name="numeroSolicitud"></param>
        /// <returns></returns>
        public List<tbDetalleRuta> ObtieneDetalleRutaPorNumeroSolicitud(decimal numeroSolicitud)
        {
            List<tbDetalleRuta> lstDetalleRuta = new List<tbDetalleRuta>();
            string query = "SELECT ifnull(DETNU1, 0) AS NumeroSolicitud, ifnull(DETIDR, 0) AS IDRuta,  ifnull(rtrim(ltrim(DETRUT)), '') AS RutaOrigenInicio, ifnull(rtrim(ltrim(DETRU1)), '') AS RutaDestino, ifnull(rtrim(ltrim(DETRU2)), '') AS RutaOrigenFinal, " +
                " ifnull(rtrim(ltrim(DETFE3)), '') AS FechaIdaVuelo, ifnull(rtrim(ltrim(DETFE4)), '') AS FechaRetornoVuelo, ifnull(rtrim(ltrim(DETHO2)), '') AS HoraEstimadaVuelo, " +
                " ifnull(rtrim(ltrim(DETDER)), '') AS DerechoSolicitado,  ifnull(rtrim(ltrim(DETRU3)), '') AS RutaVuelo, ifnull(rtrim(ltrim(DETUS2)), '') AS UsuarioCreacion, ifnull(rtrim(ltrim(DETFE5)), '') AS FechaCreacion, ifnull(rtrim(ltrim(DETHO3)), '') AS HoraCreacion, ifnull(rtrim(ltrim(DETUS3)), '') AS UsuarioModificado, ifnull(rtrim(ltrim(DETFE6)), '') AS FechaModificado, " +
                " ifnull(rtrim(ltrim(DETHO4)), '') AS HoraModificado,  ifnull(rtrim(ltrim(DETOB1)), '') AS ObservacionRuta, ifnull(rtrim(ltrim(DETIDE)), '') AS IdentificacionVuelo, ifnull(rtrim(ltrim(DETTIP)), '') AS TipoVuelo,  ifnull(rtrim(ltrim(TIPDE1)), '') AS DescripcionTipoVuelo, ifnull(rtrim(ltrim(DETATO)), '') AS RutaAtoAterrizajeEC  " +
                " FROM  DETAR1 LEFT JOIN  TIPAR1 ON(DETTIP = TIPTIP) WHERE DETNU1 = " + numeroSolicitud + " ORDER BY DETIDR ASC";
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
                        tbDetalleRuta detalleRuta = new tbDetalleRuta();
                        detalleRuta.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        detalleRuta.IdRuta = decimal.Parse(dr["IDRuta"].ToString());
                        detalleRuta.RutaOrigenInicio = dr["RutaOrigenInicio"].ToString();
                        detalleRuta.RutaDestino = dr["RutaDestino"].ToString();
                        detalleRuta.RutaOrigenFinal = dr["RutaOrigenFinal"].ToString();
                        detalleRuta.FechaIdaVuelo = fechaDate(dr["FechaIdaVuelo"].ToString());
                        detalleRuta.FechaRetornoVuelo = fechaDate(dr["FechaRetornoVuelo"].ToString());
                        detalleRuta.HoraEstimadaVuelo = dr["HoraEstimadaVuelo"].ToString();
                        detalleRuta.DerechoSolicitado = dr["DerechoSolicitado"].ToString();
                        detalleRuta.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        detalleRuta.HoraCreacion = dr["HoraCreacion"].ToString();
                        detalleRuta.HoraCreacion = dr["HoraCreacion"].ToString();
                        detalleRuta.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        detalleRuta.FechaModificado = dr["FechaModificado"].ToString();
                        detalleRuta.HoraModificado = dr["HoraModificado"].ToString();
                        detalleRuta.ObservacionRuta = dr["ObservacionRuta"].ToString();
                        detalleRuta.IdentificacionVuelo = dr["IdentificacionVuelo"].ToString();
                        detalleRuta.TipoVuelo = dr["TipoVuelo"].ToString();
                        detalleRuta.DescripcionTipoVuelo = dr["DescripcionTipoVuelo"].ToString();
                        detalleRuta.RutaVuelo = dr["RutaVuelo"].ToString();
                        detalleRuta.RutaAtoAterrizajeEC = dr["RutaAtoAterrizajeEC"].ToString();
                        lstDetalleRuta.Add(detalleRuta);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstDetalleRuta = null;
            }
            return lstDetalleRuta;
        }

        public bool VerificaExisteDetalleRutaPorNumeroSolicitud(decimal numeroSolicitud, decimal idRuta)
        {
            bool estado = false;
            List<tbDetalleRuta> lstDetalleRuta = new List<tbDetalleRuta>();
            string query = "SELECT ifnull(DETNU1, 0) AS NumeroSolicitud, ifnull(DETIDR, 0) AS IDRuta"
                + " FROM  DETAR1 INNER JOIN  TIPAR1 ON(DETTIP = TIPTIP) WHERE DETNU1 = " + numeroSolicitud + " AND DETIDR = " + idRuta;
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
                        estado = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }


        public List<tbDetalleRuta> ObtieneDetalleRutaPorNumeroSolicitudRuta(Int32 numeroSolicitud, string origenInicio, string destino, string origenFinal)
        {
            List<tbDetalleRuta> lstDetalleRuta = new List<tbDetalleRuta>();
            string query = "SELECT DETNU1 AS NumeroSolicitud,  DETIDR AS IDRuta, DETRUT AS RutaOrigenInicio, DETRU1 AS RutaDestino, DETRU2 AS RutaOrigenFinal, " +
                " DETFE3 AS FechaIdaVuelo, DETFE4 AS FechaRetornoVuelo, DETHO2 AS HoraEstimadaVuelo, " +
                " DETDER AS DerechoSolicitado, DETUS2 AS UsuarioCreacion, DETFE5 AS FechaCreacion, DETHO3 AS HoraCreacion, DETUS3 AS UsuarioModificado, DETFE6 AS FechaModificado, " +
                " DETHO4 AS HoraModificado FROM  DETAR1 WHERE DETNU1 = " + numeroSolicitud + " AND DETRUT = '" + origenInicio + "' AND DETRU1 = '" + destino + "' AND " +
                " DETRU2 = '" + origenFinal + "'";

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
                        tbDetalleRuta detalleRuta = new tbDetalleRuta();
                        detalleRuta.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        detalleRuta.IdRuta = decimal.Parse(dr["IdRuta"].ToString());
                        detalleRuta.RutaOrigenInicio = dr["RutaOrigenInicio"].ToString();
                        detalleRuta.RutaDestino = dr["RutaDestino"].ToString();
                        detalleRuta.RutaOrigenFinal = dr["RutaOrigenFinal"].ToString();
                        detalleRuta.FechaIdaVuelo = dr["FechaIdaVuelo"].ToString();
                        detalleRuta.FechaRetornoVuelo = dr["FechaRetornoVuelo"].ToString();
                        detalleRuta.HoraEstimadaVuelo = dr["HoraEstimadaVuelo"].ToString();
                        detalleRuta.DerechoSolicitado = dr["DerechoSolicitado"].ToString();
                        detalleRuta.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        detalleRuta.HoraCreacion = dr["HoraCreacion"].ToString();
                        detalleRuta.HoraCreacion = dr["HoraCreacion"].ToString();
                        detalleRuta.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        detalleRuta.FechaModificado = dr["FechaModificado"].ToString();
                        detalleRuta.HoraModificado = dr["HoraModificado"].ToString();
                        lstDetalleRuta.Add(detalleRuta);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstDetalleRuta = null;
            }
            return lstDetalleRuta;
        }

        #region DetalleSobreVuelos

        /// <summary>
        /// Metodo registra el detalle de la ruta
        /// </summary>
        /// <param name="detalleRuta"></param>
        /// <returns>True/False</returns>
        public bool RegistrarDetalleRuta(tbDetalleRuta detalleRuta)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryInsertar = "INSERT INTO  DETAR1 (DETNU1, DETIDR, DETRUT, DETRU1, DETRU2, DETFE3, DETFE4, DETHO2, DETDER, DETOB1, DETIDE, DETATO, DETTIP, DETUS2, DETFE5, DETHO3) " +
                        " VALUES(@NumeroSolicitud, @IdRuta, @RutaOrigenInicio, @RutaDestino, @RutaOrigenFinal, @FechaIdaVuelo, @FechaRetornoVuelo, @HoraEstimadaVuelo," +
                        " @DerechoSolicitado, @ObservacionRuta, @IdentificacionVuelo, @RutaAtoAterrizajeEC, @TipoVuelo, @UsuarioCreacion, @FechaCreacion, @HoraCreacion)";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = detalleRuta.NumeroSolicitud;
                    cmd.Parameters["@IdRuta"].Value = detalleRuta.IdRuta;    //SecuencialDetalleRuta(detalleRuta.IdRuta);
                    cmd.Parameters["@RutaOrigenInicio"].Value = campoNull(detalleRuta.RutaOrigenInicio);
                    cmd.Parameters["@RutaDestino"].Value = campoNull(detalleRuta.RutaDestino);
                    cmd.Parameters["@RutaOrigenFinal"].Value = campoNull(detalleRuta.RutaOrigenFinal);
                    cmd.Parameters["@RutaDestino"].Value = campoNull(detalleRuta.RutaDestino);
                    cmd.Parameters["@FechaIdaVuelo"].Value = campoNull(detalleRuta.FechaIdaVuelo); //fechaAS400(campoNull(detalleRuta.FechaIdaVuelo));
                    cmd.Parameters["@FechaRetornoVuelo"].Value = campoNull(detalleRuta.FechaRetornoVuelo); //fechaAS400(campoNull(detalleRuta.FechaRetornoVuelo));
                    cmd.Parameters["@HoraEstimadaVuelo"].Value = campoNull(detalleRuta.HoraEstimadaVuelo);
                    cmd.Parameters["@DerechoSolicitado"].Value = campoNull(detalleRuta.DerechoSolicitado);
                    cmd.Parameters["@ObservacionRuta"].Value = campoNull(detalleRuta.ObservacionRuta);
                    cmd.Parameters["@IdentificacionVuelo"].Value = campoNull(detalleRuta.IdentificacionVuelo).ToUpper();
                    cmd.Parameters["@RutaAtoAterrizajeEC"].Value = campoNull(detalleRuta.RutaAtoAterrizajeEC);
                    cmd.Parameters["@TipoVuelo"].Value = campoNull(detalleRuta.TipoVuelo);
                    cmd.Parameters["@UsuarioCreacion"].Value = detalleRuta.UsuarioCreacion;
                    cmd.Parameters["@FechaCreacion"].Value = osistema.FechaSistema;  //  DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraCreacion"].Value = osistema.HoraSistema;  //DateTime.Now.ToString("HH:mm:ss");

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
        /// Metodo registra el detalle de la ruta
        /// </summary>
        /// <param name="detalleRuta"></param>
        /// <returns>True/False</returns>
        public bool ActualizaDetalleRutaSobreVuelo(tbDetalleRuta detalleRuta)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE  DETAR1"
                        + " SET DETRUT = @RutaOrigenInicio, DETRU1 = @RutaDestino, DETRU2 = @RutaOrigenFinal, DETFE3 = @FechaIdaVuelo, DETFE4 = @FechaRetornoVuelo, DETHO2 = @HoraEstimadaVuelo,"
                        + " DETDER = @DerechoSolicitado, DETOB1 = @ObservacionRuta, DETIDE = @IdentificacionVuelo, DETATO = @RutaAtoAterrizajeEC, DETTIP = @TipoVuelo,"
                        + " DETUS3 = @UsuarioModificacion, DETFE6 = @FechaModificacion, DETHO4 = @HoraModificacion"
                        + " WHERE DETNU1 = @NumeroSolicitud AND DETIDR = @IdRuta";

                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@RutaOrigenInicio"].Value = campoNull(detalleRuta.RutaOrigenInicio);
                    cmd.Parameters["@RutaDestino"].Value = campoNull(detalleRuta.RutaDestino);
                    cmd.Parameters["@RutaOrigenFinal"].Value = campoNull(detalleRuta.RutaOrigenFinal);
                    cmd.Parameters["@RutaDestino"].Value = campoNull(detalleRuta.RutaDestino);
                    cmd.Parameters["@FechaIdaVuelo"].Value = fechaAS400(campoNull(detalleRuta.FechaIdaVuelo));
                    cmd.Parameters["@FechaRetornoVuelo"].Value = fechaAS400(campoNull(detalleRuta.FechaRetornoVuelo));
                    cmd.Parameters["@HoraEstimadaVuelo"].Value = campoNull(detalleRuta.HoraEstimadaVuelo);
                    cmd.Parameters["@DerechoSolicitado"].Value = campoNull(detalleRuta.DerechoSolicitado);
                    cmd.Parameters["@ObservacionRuta"].Value = campoNull(detalleRuta.ObservacionRuta);
                    cmd.Parameters["@IdentificacionVuelo"].Value = campoNull(detalleRuta.IdentificacionVuelo);
                    cmd.Parameters["@RutaAtoAterrizajeEC"].Value = campoNull(detalleRuta.RutaAtoAterrizajeEC);
                    cmd.Parameters["@TipoVuelo"].Value = campoNull(detalleRuta.TipoVuelo);
                    cmd.Parameters["@UsuarioModificacion"].Value = detalleRuta.UsuarioModificado;
                    cmd.Parameters["@FechaModificacion"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificacion"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@NumeroSolicitud"].Value = detalleRuta.NumeroSolicitud;
                    cmd.Parameters["@IdRuta"].Value = detalleRuta.IdRuta;

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

        #endregion


        public bool RegistrarDetalleRuta(List<tbDetalleRuta> detalleRuta)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            EliminarDetalleRutaPorSolicitud(Int32.Parse(detalleRuta[0].NumeroSolicitud.ToString()));
            foreach (var rows in detalleRuta)
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    try
                    {
                        queryInsertar = "INSERT INTO  DETAR1 (DETNU1, DETIDR, DETRUT, DETRU1, DETRU2, DETFE3, DETFE4, DETHO2, DETDER, DETRU3, DETUS2, DETFE5, DETHO3) " +
                            " VALUES(@NumeroSolicitud, @IdRuta, @RutaOrigenInicio, @RutaDestino, @RutaOrigenFinal, @FechaIdaVuelo, @FechaRetornoVuelo, @HoraEstimadaVuelo," +
                            " @DerechoSolicitado, @RutaVuelo, @UsuarioCreacion, @FechaCreacion, @HoraCreacion)";
                        cmd = new iDB2Command(queryInsertar, oConexion);
                        oConexion.Open();
                        cmd.DeriveParameters();
                        cmd.Parameters["@NumeroSolicitud"].Value = rows.NumeroSolicitud;
                        cmd.Parameters["@IdRuta"].Value = SecuencialDetalleRuta(rows.NumeroSolicitud);
                        cmd.Parameters["@RutaOrigenInicio"].Value = rows.RutaOrigenInicio;
                        cmd.Parameters["@RutaDestino"].Value = rows.RutaDestino;
                        cmd.Parameters["@RutaOrigenFinal"].Value = rows.RutaOrigenFinal;
                        cmd.Parameters["@FechaIdaVuelo"].Value = rows.FechaIdaVuelo;
                        cmd.Parameters["@FechaRetornoVuelo"].Value = rows.FechaRetornoVuelo;
                        cmd.Parameters["@HoraEstimadaVuelo"].Value = rows.HoraEstimadaVuelo;
                        cmd.Parameters["@DerechoSolicitado"].Value = rows.DerechoSolicitado;
                        cmd.Parameters["@RutaVuelo"].Value = rows.RutaVuelo;
                        cmd.Parameters["@UsuarioCreacion"].Value = rows.UsuarioCreacion.Trim();
                        cmd.Parameters["@FechaCreacion"].Value = DateTime.Now.ToString("yyyyMMdd");
                        cmd.Parameters["@HoraCreacion"].Value = DateTime.Now.ToString("HH:mm:ss");

                        respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        cmd.Dispose();
                        oConexion.Close();
                    }
                    catch (Exception ex)
                    {
                        respuesta = false;
                    }
                }
            }

            return respuesta;
        }

        public bool AutualizaDetalleRutaSolicitud(List<tbDetalleRuta> detalleRuta)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            foreach (var rows in detalleRuta)
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    try
                    {
                        queryInsertar = "INSERT INTO  DETAR1 (DETNU1, DETIDR, DETRUT, DETRU1, DETRU2, DETFE3, DETFE4, DETHO2, DETDER, DETRU3, DETUS2, DETFE5, DETHO3) " +
                            " VALUES(@NumeroSolicitud, @IdRuta, @RutaOrigenInicio, @RutaDestino, @RutaOrigenFinal, @FechaIdaVuelo, @FechaRetornoVuelo, @HoraEstimadaVuelo," +
                            " @DerechoSolicitado, @RutaVuelo, @UsuarioCreacion, @FechaCreacion, @HoraCreacion)";
                        cmd = new iDB2Command(queryInsertar, oConexion);
                        oConexion.Open();
                        cmd.DeriveParameters();
                        cmd.Parameters["@NumeroSolicitud"].Value = rows.NumeroSolicitud;
                        cmd.Parameters["@IdRuta"].Value = SecuencialDetalleRuta(rows.NumeroSolicitud);
                        cmd.Parameters["@RutaOrigenInicio"].Value = rows.RutaOrigenInicio;
                        cmd.Parameters["@RutaDestino"].Value = rows.RutaDestino;
                        cmd.Parameters["@RutaOrigenFinal"].Value = rows.RutaOrigenFinal;
                        cmd.Parameters["@FechaIdaVuelo"].Value = rows.FechaIdaVuelo;
                        cmd.Parameters["@FechaRetornoVuelo"].Value = rows.FechaRetornoVuelo;
                        cmd.Parameters["@HoraEstimadaVuelo"].Value = rows.HoraEstimadaVuelo;
                        cmd.Parameters["@DerechoSolicitado"].Value = rows.DerechoSolicitado;
                        cmd.Parameters["@RutaVuelo"].Value = rows.RutaVuelo;
                        cmd.Parameters["@UsuarioCreacion"].Value = rows.UsuarioCreacion.Trim();
                        cmd.Parameters["@FechaCreacion"].Value = DateTime.Now.ToString("yyyyMMdd");
                        cmd.Parameters["@HoraCreacion"].Value = DateTime.Now.ToString("HH:mm:ss");

                        respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        cmd.Dispose();
                        oConexion.Close();
                    }
                    catch (Exception ex)
                    {
                        respuesta = false;
                    }
                }
            }

            return respuesta;
        }

        public bool ActualizaDetalleRuta(tbDetalleRuta detalleRuta)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE  DETAR1 SET DETFE3 = @FechaIdaVuelo, DETFE4 = @FechaRetornoVuelo, DETHO2 = @HoraEstimadaVuelo, "
                    + " DETDER = @DerechoSolicitado, DETUS3 = @UsuarioModificado, DETFE6 = @FechaModificado, DETHO4 = @HoraModificado "
                    + " WHERE DETNU1 = @NumeroSolicitud AND DETRUT = @RutaOrigenInicio AND DETRU1 = @RutaDestino AND DETRU2 = @RutaOrigenFinal";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@FechaIdaVuelo"].Value = detalleRuta.FechaIdaVuelo;
                    cmd.Parameters["@FechaRetornoVuelo"].Value = detalleRuta.FechaRetornoVuelo;
                    cmd.Parameters["@HoraEstimadaVuelo"].Value = detalleRuta.HoraEstimadaVuelo;
                    cmd.Parameters["@DerechoSolicitado"].Value = detalleRuta.DerechoSolicitado;
                    cmd.Parameters["@UsuarioModificado"].Value = detalleRuta.UsuarioModificado;
                    cmd.Parameters["@FechaModificado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificado"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@NumeroSolicitud"].Value = detalleRuta.NumeroSolicitud;
                    cmd.Parameters["@RutaOrigenInicio"].Value = detalleRuta.RutaOrigenInicio;
                    cmd.Parameters["@RutaDestino"].Value = detalleRuta.RutaDestino;
                    cmd.Parameters["@RutaOrigenFinal"].Value = detalleRuta.RutaOrigenFinal;

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

        public bool EliminarDetalleRutaPorSolicitud(Int32 numeroSolicitud)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM  DETAR1"
                    + " WHERE DETNU1 = @NumeroSolicitud";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = numeroSolicitud;
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



        public bool EliminarDetalleRutaPorSolicitudRuta(Int32 numeroSolicitud, string origenInicio, string destion, string origenFinal)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM  DETAR1"
                    + " WHERE DETNU1 = @NumeroSolicitud AND DETRUT = @RutaOrigenInicio AND DETRU1 = @RutaDestino AND DETRU2 = @RutaOrigenFinal";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = numeroSolicitud;
                    cmd.Parameters["@RutaOrigenInicio"].Value = origenInicio;
                    cmd.Parameters["@RutaDestino"].Value = destion;
                    cmd.Parameters["@RutaOrigenFinal"].Value = origenFinal;
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


        private Int32 SecuencialDetalleRuta(decimal numSolitud)
        {
            string query = "SELECT IFNULL(max(DETIDR), 0) + 1 AS Secuencial FROM   DETAR1 WHERE DETNU1 = " + numSolitud;
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

        #region "Solictud vuelos privados con aeronaves privadas de matricula extrajeta"
        public bool RegistraDetalleRutaPrivadas(List<tbDetalleRuta> detalleRuta, Int32 numSolicitud, string usuarioCreado)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            tbSistema osistema = new tbSistema();
            iDB2Command cmd;
            string fechaFin = string.Empty;
            //EliminarDetalleRutaPorSolicitud(numSolicitud);
            osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            foreach (var rows in detalleRuta)
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    try
                    {
                        fechaFin = rows.FechaIdaVuelo;
                        queryInsertar = "INSERT INTO DETAR1 (DETNU1, DETIDR, DETRUT, DETRU1, DETDER,  DETFE3, DETFE4, DETUS2, DETFE5, DETHO3) " +
                            " VALUES(@NumeroSolicitud, @IdRuta, @RutaOrigenInicio, @RutaDestino, @DerechoSolicitado , @FechaIdaVuelo, @FechaRetornoVuelo, @UsuarioCreacion, @FechaCreacion, @HoraCreacion)";
                        cmd = new iDB2Command(queryInsertar, oConexion);
                        oConexion.Open();
                        cmd.DeriveParameters();
                        cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                        cmd.Parameters["@IdRuta"].Value = rows.IdRuta;
                        cmd.Parameters["@RutaOrigenInicio"].Value = campoNull(rows.RutaOrigenInicio.ToUpper());
                        cmd.Parameters["@RutaDestino"].Value = campoNull(rows.RutaDestino.ToUpper());
                        cmd.Parameters["@DerechoSolicitado"].Value = campoNull(rows.RutaDestino.ToUpper());  //ruta Alternas
                        cmd.Parameters["@FechaIdaVuelo"].Value = campoNull(fechaAS400(rows.FechaIdaVuelo));
                        cmd.Parameters["@FechaRetornoVuelo"].Value = campoNull(fechaAS400(rows.FechaRetornoVuelo));  ///campoNull(FechaVueloMasDias(fechaFin, 30));
                        cmd.Parameters["@UsuarioCreacion"].Value = campoNull(usuarioCreado);
                        cmd.Parameters["@FechaCreacion"].Value = campoNull(osistema.FechaSistema);
                        cmd.Parameters["@HoraCreacion"].Value = campoNull(osistema.HoraSistema);

                        respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        cmd.Dispose();
                        oConexion.Close();
                    }
                    catch (Exception ex)
                    {
                        respuesta = false;
                    }
                }
            }

            return respuesta;
        }
        #endregion

        private string fechaDate(string fecha)
        {

            string fechaNueva = string.Empty;
            if (fecha.Length > 0)
            {
                fechaNueva = fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2);
            }

            return fechaNueva;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
        private string fechaAS400(string ofecha)
        {
            string odate = string.Empty;
            try
            {
                //24/02/2023
                if (ofecha.Trim().Length > 0)
                {
                    string[] fecha = ofecha.Split(new Char[] { '/', '-' });
                    //DateTime ofechaForato = Convert.ToDateTime(ofecha);
                    odate = fecha[2] + fecha[1] + fecha[0];       ///    ofechaForato.ToString("yyyyMMdd");
                }
            }
            catch
            {
                odate = "";
            }

            return odate;
        }

        private string FechaVueloMasDias(string fechaVuelo, int dias)
        {
            string fechaFinal = string.Empty;
            try
            {

                string[] fecha = fechaVuelo.Split(new Char[] { '/', '-' });
                //fechaFinal = fecha[2] + fecha[1] + fecha[0];        ///    ofechaForato.ToString("yyyyMMdd");


                int ndia = int.Parse(fecha[2]);
                int nmes = int.Parse(fecha[1]);
                int nanio = int.Parse(fecha[2]);
                string cdia = string.Empty;
                string cmes = string.Empty;

                DateTime dateTime = new DateTime(nanio, nmes, ndia);
                fechaFinal = dateTime.ToString("yyyyMMdd");
            }
            catch
            {
                fechaFinal = ""; ;
            }
            return fechaFinal;
        }

    }
}
