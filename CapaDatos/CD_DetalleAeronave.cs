using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;
namespace CapaDatos
{
   public class CD_DetalleAeronave
    {
        public static CD_DetalleAeronave _instancia = null;
        //string biblioteca = "DGACDAT";
        private CD_DetalleAeronave()
        {

        }

        public static CD_DetalleAeronave Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleAeronave();
                }
                return _instancia;
            }
        }

        #region Vuelos Charter
        /// <summary>
        /// Metodo obtiene detalle de Aeronave
        /// </summary>
        /// <param name="numeroSolicitud"></param>
        /// <returns>List</returns>
        public List<tbDetalleAeronave> ObtieneDetalleAeronavePorNumeroSolicitud(decimal numeroSolicitud)
        {
            List<tbDetalleAeronave> lstDetalleAeronave = new List<tbDetalleAeronave>();
            string query = "SELECT  ifnull(DETNUM, 0) AS NumeroSolicitud, ifnull(ltrim(rtrim(DETMAT)), '') AS Matricula, ifnull(ltrim(rtrim(DETMAR)), '') as Marca, "
+ " ifnull(ltrim(rtrim(DETMOD)), '') as Modelo, ifnull(DETPES, 0) as PesoWTOKG, ifnull(ltrim(rtrim(DETFEC)), '') as FechaVegenciaSeguro, IFNULL(DETEST, 0) AS EstadoExpiracion, ifnull(ltrim(rtrim(DETFE7)), '') AS FechaExpiracion,  ifnull(ltrim(rtrim(DETUSU)), '') as UsuarioCreado, "
+ " ifnull(ltrim(rtrim(DETFE1)), '') as FechaCrado, ifnull(ltrim(rtrim(DETHOR)), '') as HoraCreado, ifnull(ltrim(rtrim(DETUS1)), '') as UsuarioModificado, ifnull(ltrim(rtrim(DETFE2)), '') as FechaModificado, "
+ " ifnull(ltrim(rtrim(DETHO1)), '') as HoraModificado FROM DETARC WHERE DETNUM = " + numeroSolicitud;
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
                        tbDetalleAeronave oDetalleAeronave = new tbDetalleAeronave();
                        oDetalleAeronave.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oDetalleAeronave.Matricula = dr["Matricula"].ToString();
                        oDetalleAeronave.Marca = dr["Marca"].ToString();
                        oDetalleAeronave.Modelo = dr["Modelo"].ToString();
                        oDetalleAeronave.PesoWTOKG = dr["PesoWTOKG"].ToString().Replace(",", ".");
                        oDetalleAeronave.FechaVigenciaSeguro = fechaDate(dr["FechaVegenciaSeguro"].ToString());
                        oDetalleAeronave.EstadoExpiracion = decimal.Parse(dr["EstadoExpiracion"].ToString());
                        oDetalleAeronave.FechaExpiracion = fechaDate(dr["FechaExpiracion"].ToString());
                        oDetalleAeronave.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oDetalleAeronave.FechaCrado = dr["FechaCrado"].ToString();
                        oDetalleAeronave.HoraCreado = dr["HoraCreado"].ToString();
                        oDetalleAeronave.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oDetalleAeronave.FechaModificado = dr["FechaModificado"].ToString();
                        oDetalleAeronave.HoraModificado = dr["HoraModificado"].ToString();
                        lstDetalleAeronave.Add(oDetalleAeronave);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstDetalleAeronave = null;
            }
            return lstDetalleAeronave;
        }


        public string RegistrarDetalleAeronave(List<tbDetalleAeronave> detalleAeronave)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            tbSistema sistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            iDB2Command cmd;
            string oPeso = "";
            string mensajeError = "";
            string sqlValores = string.Empty;
            int contInicio = 0;
            int contFin = 1;
            string puntoComa = ",";
            Int32 numSolictud = 0;
            queryInsertar = "INSERT INTO DETARC (DETNUM, DETMAT, DETMAR, DETMOD, DETPES, DETFEC, DETUSU, DETFE1, DETHOR) VALUES " + Environment.NewLine;
            contInicio = detalleAeronave.Count;
            foreach (var rows in detalleAeronave)
            {
                if (contInicio == contFin)
                    puntoComa = "";
                oPeso = rows.PesoWTOKG.Trim();
                sqlValores = "(" + rows.NumeroSolicitud + ",'" + rows.Matricula + "','" + rows.Marca + "','" + rows.Modelo + "'," + oPeso.Replace(",", ".") + ",'" + DateTime.Parse(rows.FechaVigenciaSeguro).ToString("yyyyMMdd") + "','" + rows.UsuarioCreado.Trim() + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HH:mm:ss") + "')" + puntoComa + Environment.NewLine;
                queryInsertar = queryInsertar + sqlValores;
                contFin = contFin + 1;
                oPeso = "";
                numSolictud = Int32.Parse(rows.NumeroSolicitud.ToString());
            }
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    EliminaDetalleAeronavePorSolicitud(numSolictud);
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    queryInsertar = "";
                    mensajeError = "200";
                }
                catch (iDB2Exception ex)
                {
                    mensajeError = ex.MessageCode + "";
                    return mensajeError;
                }

            }

            return mensajeError;
        }

        public string RegistrarDetalleAeronaveCharter(List<tbDetalleAeronave> detalleAeronave)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            tbSistema sistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            iDB2Command cmd;
            string oPeso = "";
            string mensajeError = "";
            string sqlValores = string.Empty;
            int contInicio = 0;
            int contFin = 1;
            string puntoComa = ",";
            Int32 numSolictud = 0;
            queryInsertar = "INSERT INTO DETARC (DETNUM, DETMAT, DETMAR, DETMOD, DETPES, DETFEC, DETUSU, DETFE1, DETHOR) VALUES " + Environment.NewLine;
            contInicio = detalleAeronave.Count;
            foreach (var rows in detalleAeronave)
            {
                if (contInicio == contFin)
                    puntoComa = "";
                oPeso = rows.PesoWTOKG.Trim();
                sqlValores = "(" + rows.NumeroSolicitud + ",'" + rows.Matricula + "','" + rows.Marca + "','" + rows.Modelo + "'," + oPeso.Replace(",", ".") + ",'" + rows.FechaVigenciaSeguro + "','" + rows.UsuarioCreado.Trim() + "','" + sistema.FechaSistema + "','" + sistema.HoraSistema + "')" + puntoComa + Environment.NewLine;
                queryInsertar = queryInsertar + sqlValores;
                contFin = contFin + 1;
                oPeso = "";
                numSolictud = Int32.Parse(rows.NumeroSolicitud.ToString());
            }
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    EliminaDetalleAeronavePorSolicitud(numSolictud);
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    queryInsertar = "";
                    mensajeError = "200";
                }
                catch (iDB2Exception ex)
                {
                    mensajeError = ex.MessageCode + "";
                    return mensajeError;
                }

            }

            return mensajeError;
        }


        /// <summary>
        /// Metodo actualiza el registro de detalle de la Aeronae
        /// </summary>
        /// <param name="detalleAeronave"></param>
        /// <returns>DetalleAeronave</returns>
        public bool ActualizaDetalleAeronave(tbDetalleAeronave detalleAeronave)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE DETARC SET DETMAR = @Marca, DETMOD = @Modelo, DETPES = @PesoWTOKG, " +
                        "DETFEC = @FechaVegenciaSeguro, DETUS1 = @UsuarioModificado , DETFE2 = @FechaModificado, DETHO1 = @HoraModificado " +
                        " WHERE DETNUM = @NumeroSolicitud AND DETMAT = @Matricula";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Marca"].Value = detalleAeronave.Marca;
                    cmd.Parameters["@Modelo"].Value = detalleAeronave.Modelo;
                    cmd.Parameters["@PesoWTOKG"].Value = detalleAeronave.PesoWTOKG;
                    cmd.Parameters["@FechaVigenciaSeguro"].Value = fechaAS400(detalleAeronave.FechaVigenciaSeguro);
                    cmd.Parameters["@UsuarioModificado"].Value = detalleAeronave.UsuarioModificado;
                    cmd.Parameters["@FechaModificado"].Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters["@HoraModificado"].Value = DateTime.Now.ToString("HH:mm:ss");
                    cmd.Parameters["@NumeroSolicitud"].Value = detalleAeronave.NumeroSolicitud;
                    cmd.Parameters["@Matricula"].Value = detalleAeronave.Matricula;
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
        /// Metodo actualiza el registro de detalle de la Aeronae
        /// </summary>
        /// <param name="detalleAeronave"></param>
        /// <returns>DetalleAeronave</returns>
        public bool EliminaDetalleAeronavePorSolicitud(Int32 numeroSolicitud)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM DETARC " +
                        " WHERE DETNUM = @NumeroSolicitud";
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

        /// <summary>
        /// Elimina el registro de Detalle de Aeronave
        /// </summary>
        /// <param name="numeroSolicitud"></param>
        /// <param name="matricula"></param>
        /// <returns>True o false</returns>
        public bool EliminaDetalleAeronavePorSolicitudMatricula(string numeroSolicitud, string matricula)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "DELETE FROM DETARC " +
                        " WHERE DETNUM = @NumeroSolicitud AND DETMAT = @Matricula";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = numeroSolicitud;
                    cmd.Parameters["@Matricula"].Value = matricula;
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



        #region SobreVuelos
        /// <summary>
        /// Metodo registra dettale de aeorave
        /// </summary>
        /// <param name="detalleAeronave"></param>
        /// <returns>DetalleAeronave</returns>
        public bool RegistrarDetalleAeronave(tbDetalleAeronave detalleAeronave)
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
                    queryInsertar = "INSERT INTO DETARC (DETNUM, DETIDD, DETMAT, DETMAR, DETMOD, DETPES, DETFEC, DETEST, DETFE7,  DETUSU, DETFE1, DETHOR) " +
                        "VALUES(" + detalleAeronave.NumeroSolicitud + ", 1 , '" + detalleAeronave.Matricula + "' , '" + detalleAeronave.Marca + "' , '" + detalleAeronave.Modelo + "' , " + detalleAeronave.PesoWTOKG.Replace(",", ".") + ", '"
                        + fechaAS400(detalleAeronave.FechaVigenciaSeguro) + "' , '" + detalleAeronave.EstadoExpiracion + "' , '" + fechaAS400(detalleAeronave.FechaExpiracion) + "' , '" + detalleAeronave.UsuarioCreado + "' , '" + osistema.FechaSistema + "' , '" + osistema.HoraSistema + "')";

                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
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
        /// Metodo Actualiza el detalle de Aeronave por Sobre Vuelos
        /// </summary>
        /// <param name="detalleAeronave"></param>
        /// <returns>True 0 False</returns>
        public bool ActualizaDetalleAeronaveSobreVuelo(tbDetalleAeronave detalleAeronave)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE DETARC"
                        + " SET DETMAT = '" + detalleAeronave.Matricula + "', DETMAR = '" + detalleAeronave.Marca + "' , DETMOD= '" + detalleAeronave.Modelo + "' ,"
                        + " DETPES = " + detalleAeronave.PesoWTOKG.Replace(",", ".") + ", DETFEC = '" + fechaAS400(detalleAeronave.FechaVigenciaSeguro) + "' ,"
                        + " DETEST = '" + detalleAeronave.EstadoExpiracion + "', DETFE7 = '" + fechaAS400(detalleAeronave.FechaExpiracion) + "',"
                        + " DETUS1 = '" + detalleAeronave.UsuarioModificado + "' , DETFE2 = '" + DateTime.Now.ToString("yyyyMMdd") + "', DETHO1 = '" + DateTime.Now.ToString("HH:mm:ss") + "'"
                        + " WHERE DETNUM = " + detalleAeronave.NumeroSolicitud;

                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
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

        #endregion

        #region "Vuelos privados con aeronaves privadas de matriculas extranjetas"
        public List<tbDetalleAeronave> ObtieneDetalleAeronavePrivadasPorNumeroSolicitud(decimal numeroSolicitud)
        {
            List<tbDetalleAeronave> lstDetalleAeronave = new List<tbDetalleAeronave>();
            string query = "SELECT  ifnull(DETNUM, 0) AS NumeroSolicitud, ifnull(DETIDD, 0) as IdDetalleAeronave, ifnull(ltrim(rtrim(DETMAT)), '') AS Matricula, ifnull(ltrim(rtrim(DETMAR)), '') as Marca, "
            + " ifnull(ltrim(rtrim(DETMOD)), '') as Modelo, ifnull(DETPES, 0) as PesoWTOKG, ifnull(ltrim(rtrim(DETCAM)), '') as TipoAeronave, ifnull(ltrim(rtrim(DETUSU)), '') as UsuarioCreado, "
            + " ifnull(ltrim(rtrim(DETFE1)), '') as FechaCrado, ifnull(ltrim(rtrim(DETHOR)), '') as HoraCreado "
            + " FROM DETARC WHERE DETNUM = " + numeroSolicitud;
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
                        tbDetalleAeronave oDetalleAeronave = new tbDetalleAeronave();
                        oDetalleAeronave.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oDetalleAeronave.IdDetalleAeronave = decimal.Parse(dr["IdDetalleAeronave"].ToString());
                        oDetalleAeronave.Matricula = dr["Matricula"].ToString();
                        oDetalleAeronave.Marca = dr["Marca"].ToString();
                        oDetalleAeronave.Modelo = dr["Modelo"].ToString();
                        oDetalleAeronave.TipoAeronave = dr["TipoAeronave"].ToString();
                        oDetalleAeronave.PesoWTOKG = dr["PesoWTOKG"].ToString();
                        oDetalleAeronave.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oDetalleAeronave.FechaCrado = dr["FechaCrado"].ToString();
                        oDetalleAeronave.HoraCreado = dr["HoraCreado"].ToString();
                        lstDetalleAeronave.Add(oDetalleAeronave);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstDetalleAeronave = null;
            }
            return lstDetalleAeronave;
        }


        public bool RegistrarDetalleAeronavePrivadas(List<tbDetalleAeronave> detalleAeronave, Int32 numSolicitud, string usuarioCreado)
        {
            bool respuesta = true;
            int indexDetalle = 1;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            foreach (var rows in detalleAeronave)
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    try
                    {
                        queryInsertar = "INSERT INTO DETARC (DETNUM, DETIDD, DETMAT, DETMAR, DETMOD, DETPES, DETCAM, DETUSU, DETFE1, DETHOR) " +
                            "VALUES(@NumeroSolicitud, @IdDetalleAeronave, @Matricula, @Marca, @Modelo, @PesoWTOKG, @TipoAeronave, @UsuarioCreado, @FechaCrado, @HoraCreado)";

                        cmd = new iDB2Command(queryInsertar, oConexion);
                        oConexion.Open();
                        cmd.DeriveParameters();
                        cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                        cmd.Parameters["@IdDetalleAeronave"].Value = indexDetalle;
                        cmd.Parameters["@Matricula"].Value = campoNull(rows.Matricula.ToUpper());
                        cmd.Parameters["@Marca"].Value = campoNull(rows.Marca.ToUpper());
                        cmd.Parameters["@Modelo"].Value = campoNull(rows.Modelo.ToUpper());
                        cmd.Parameters["@PesoWTOKG"].Value = rows.PesoWTOKG;
                        cmd.Parameters["@TipoAeronave"].Value = campoNull("");
                        cmd.Parameters["@UsuarioCreado"].Value = campoNull(usuarioCreado);
                        cmd.Parameters["@FechaCrado"].Value = campoNull(osistema.FechaSistema);
                        cmd.Parameters["@HoraCreado"].Value = campoNull(osistema.HoraSistema);

                        respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        cmd.Dispose();
                        oConexion.Close();
                    }
                    catch (Exception ex)
                    {
                        respuesta = false;
                        throw ex;
                    }

                }
                indexDetalle++;
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
                if (ofecha.Trim().Length > 0)
                {
                    DateTime ofechaForato = Convert.ToDateTime(ofecha);
                    odate = ofechaForato.ToString("yyyyMMdd");
                }
            }
            catch
            {
                odate = "";
            }

            return odate;
        }
    }
}
