using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_CursoEmpleado
    {
        public static CD_CursoEmpleado _instancia = null;
        private CD_CursoEmpleado()
        {

        }

        public static CD_CursoEmpleado Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_CursoEmpleado();
                }
                return _instancia;
            }
        }


        public List<tbCursoEmpleado> ListarCursosEmpleados(string docIdentifiacion)
        {
            List<tbCursoEmpleado> ListarCursoEmpleado = new List<tbCursoEmpleado>();
            if (docIdentifiacion.Trim().Length == 0)
                return ListarCursoEmpleado;

            string sqlQuery = "";
            StringBuilder sbCurso= new StringBuilder();
            sqlQuery = sqlQuery.ToString();
            iDB2Command cmd;
            try
            {
                sbCurso.Append("SELECT  ifnull(rtrim(ltrim(CURDOC)), '') as DocumentoIdentificacion, ifnull(rtrim(ltrim(CURCOD)), '') as CodigoCursoEmpleado, ifnull(rtrim(ltrim(CURCO1)), '') AS CodigoTitulo, ");
                sbCurso.Append(" ifnull(rtrim(ltrim(CURDES)), '') AS DescripcionCurso, ifnull(rtrim(ltrim(CURNUM)), '') AS NumeroIdentidicacion, ifnull(rtrim(ltrim(CURFEC)), '') AS FechaCurso, ");
                sbCurso.Append(" ifnull(rtrim(ltrim(CURDUR)), '') AS DuracionCurso, ifnull(rtrim(ltrim(CURTIE)), '') AS TiempoCurso, ifnull(rtrim(ltrim(CURAPR)), '') AS AprobacionCurso,");
                sbCurso.Append(" ifnull(rtrim(ltrim(CURASI)), '') AS AsistenciaCurso, ifnull(rtrim(ltrim(CURCO2)), '') AS CodigoCiudad, ifnull(rtrim(ltrim(CURCO3)), '') AS CodigoEntidadEducativa, ");
                sbCurso.Append(" ifnull(rtrim(ltrim(CUREST)), '') AS EstadoCurso, ifnull(rtrim(ltrim(CURPAT)), '') AS PathDocumentoCurso ");
                sbCurso.Append(" FROM CURARC WHERE CURDOC = '"+ docIdentifiacion + "'");

                sqlQuery = sbCurso.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCursoEmpleado oCursoEmpleado = new tbCursoEmpleado();
                        oCursoEmpleado.DocumentoIdentificacion = dr["DocumentoIdentificacion"].ToString();
                        oCursoEmpleado.CodigoCursoEmpleado = dr["CodigoCursoEmpleado"].ToString();
                        oCursoEmpleado.CodigoTitulo = dr["CodigoTitulo"].ToString();
                        oCursoEmpleado.DescripcionCurso = dr["DescripcionCurso"].ToString();
                        oCursoEmpleado.NumeroIdentidicacion = dr["NumeroIdentidicacion"].ToString();
                        oCursoEmpleado.FechaCurso = fechaDate(dr["FechaCurso"].ToString());
                        oCursoEmpleado.DuracionCurso = dr["DuracionCurso"].ToString();
                        oCursoEmpleado.TiempoCurso = dr["TiempoCurso"].ToString();
                        oCursoEmpleado.AprobacionCurso = dr["AprobacionCurso"].ToString();
                        oCursoEmpleado.AsistenciaCurso = dr["AsistenciaCurso"].ToString();
                        oCursoEmpleado.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        oCursoEmpleado.CodigoEntidadEducativa = dr["CodigoEntidadEducativa"].ToString();
                        oCursoEmpleado.EstadoCurso = dr["EstadoCurso"].ToString();
                        oCursoEmpleado.PathDocumentoCurso = dr["PathDocumentoCurso"].ToString();

                        ListarCursoEmpleado.Add(oCursoEmpleado);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ListarCursoEmpleado;
        }

        /// <summary>
        /// Metodo obtiene Curso del empleado
        /// </summary>
        /// <param name="docIdentifiacion"></param>
        /// <param name="codigoCurso"></param>
        /// <returns></returns>
        public tbCursoEmpleado CursosEmpleadoPorIdentificacionCodigo(string docIdentifiacion, string codigoCurso)
        {
            tbCursoEmpleado oCursoEmpleado = new tbCursoEmpleado();
            if (docIdentifiacion.Trim().Length == 0)
                return oCursoEmpleado;

            string sqlQuery = "";
            StringBuilder sbCurso = new StringBuilder();
            sqlQuery = sqlQuery.ToString();
            iDB2Command cmd;
            try
            {
                sbCurso.Append("SELECT  ifnull(rtrim(ltrim(CURDOC)), '') as DocumentoIdentificacion, ifnull(rtrim(ltrim(CURCOD)), '') as CodigoCursoEmpleado, ifnull(rtrim(ltrim(CURCO1)), '') AS CodigoTitulo, ");
                sbCurso.Append(" ifnull(rtrim(ltrim(CURDES)), '') AS DescripcionCurso, ifnull(rtrim(ltrim(CURNUM)), '') AS NumeroIdentidicacion, ifnull(rtrim(ltrim(CURFEC)), '') AS FechaCurso, ");
                sbCurso.Append(" ifnull(rtrim(ltrim(CURDUR)), '') AS DuracionCurso, ifnull(rtrim(ltrim(CURTIE)), '') AS TiempoCurso, ifnull(rtrim(ltrim(CURAPR)), '') AS AprobacionCurso,");
                sbCurso.Append(" ifnull(rtrim(ltrim(CURASI)), '') AS AsistenciaCurso, ifnull(rtrim(ltrim(CURCO2)), '') AS CodigoCiudad, ifnull(rtrim(ltrim(CURCO3)), '') AS CodigoEntidadEducativa, ");
                sbCurso.Append(" ifnull(rtrim(ltrim(CUREST)), '') AS EstadoCurso, ifnull(rtrim(ltrim(CURPAT)), '') AS PathDocumentoCurso ");
                sbCurso.Append(" FROM CURARC WHERE CURDOC = '" + docIdentifiacion + "' AND CURCOD = '" + codigoCurso + "'");

                sqlQuery = sbCurso.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        
                        oCursoEmpleado.DocumentoIdentificacion = dr["DocumentoIdentificacion"].ToString();
                        oCursoEmpleado.CodigoCursoEmpleado = dr["CodigoCursoEmpleado"].ToString();
                        oCursoEmpleado.CodigoTitulo = dr["CodigoTitulo"].ToString();
                        oCursoEmpleado.DescripcionCurso = dr["DescripcionCurso"].ToString();
                        oCursoEmpleado.NumeroIdentidicacion = dr["NumeroIdentidicacion"].ToString();
                        oCursoEmpleado.FechaCurso = fechaDate(dr["FechaCurso"].ToString());
                        oCursoEmpleado.DuracionCurso = dr["DuracionCurso"].ToString();
                        oCursoEmpleado.TiempoCurso = dr["TiempoCurso"].ToString();
                        oCursoEmpleado.AprobacionCurso = dr["AprobacionCurso"].ToString();
                        oCursoEmpleado.AsistenciaCurso = dr["AsistenciaCurso"].ToString();
                        oCursoEmpleado.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        oCursoEmpleado.CodigoEntidadEducativa = dr["CodigoEntidadEducativa"].ToString();
                        oCursoEmpleado.EstadoCurso = dr["EstadoCurso"].ToString();
                        oCursoEmpleado.PathDocumentoCurso = dr["PathDocumentoCurso"].ToString();
                       
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return oCursoEmpleado;
        }


        /// <summary>
        /// Metodo nuevo Curso
        /// </summary>
        /// <param name="curso Empleado nuevo"></param>
        /// <returns></returns>
        public bool CursoEmpleadoNuevo(tbCursoEmpleado cursoEmpleado)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            StringBuilder sbCurso = new StringBuilder();
            iDB2Command cmd;
            string codigoCurso = string.Empty;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    codigoCurso = campoNull(CD_TalentoHumano.Instancia.RetornaCaracteresNombrePorCedula(cursoEmpleado.DocumentoIdentificacion) + GeneraNumeroSolicitud(cursoEmpleado.DocumentoIdentificacion));
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sbCurso.Append("INSERT INTO CURARC (CURDOC, CURCOD, CURCO1, CURDES, CURNUM, CURFEC, CURDUR, CURTIE, CURAPR, CURASI, CURCO2, CURCO3, CURPAT, CUREST, CURUSE, CURDAT, CURTIM)");
                    sbCurso.Append(" VALUES(@DocumentoIdentificacion, @CodigoCursoEmpleado, @CodigoTitulo, @DescripcionCurso, @NumeroIdentidicacion, @FechaCurso, @DuracionCurso,");
                    sbCurso.Append(" @TiempoCurso, @AprobacionCurso, @AsistenciaCurso, @CodigoCiudad, @CodigoEntidadEducativa, @PathDocumentoCurso, @EstadoCurso, @UsuarioCreado, @FechaCreado, @HoraCreado)");

                    queryUpdate = sbCurso.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();

                    cmd.Parameters["@DocumentoIdentificacion"].Value = campoNull(cursoEmpleado.DocumentoIdentificacion);
                    cmd.Parameters["@CodigoCursoEmpleado"].Value = codigoCurso;
                    cmd.Parameters["@CodigoTitulo"].Value = campoNull(cursoEmpleado.CodigoTitulo);
                    cmd.Parameters["@DescripcionCurso"].Value = campoNull(cursoEmpleado.DescripcionCurso);
                    cmd.Parameters["@NumeroIdentidicacion"].Value = campoNull(cursoEmpleado.NumeroIdentidicacion);
                    cmd.Parameters["@FechaCurso"].Value = campoNull(cursoEmpleado.FechaCurso);
                    cmd.Parameters["@DuracionCurso"].Value = campoNull(cursoEmpleado.DuracionCurso);
                    cmd.Parameters["@TiempoCurso"].Value = campoNull(cursoEmpleado.TiempoCurso);
                    cmd.Parameters["@AprobacionCurso"].Value = campoNull(cursoEmpleado.AprobacionCurso);
                    cmd.Parameters["@AsistenciaCurso"].Value = campoNull(cursoEmpleado.AsistenciaCurso);
                    cmd.Parameters["@CodigoCiudad"].Value = campoNull(cursoEmpleado.CodigoCiudad);
                    cmd.Parameters["@CodigoEntidadEducativa"].Value = campoNull(cursoEmpleado.CodigoEntidadEducativa);
                    cmd.Parameters["@PathDocumentoCurso"].Value = campoNull(cursoEmpleado.PathDocumentoCurso);
                    cmd.Parameters["@EstadoCurso"].Value = campoNull(cursoEmpleado.EstadoCurso);
                    cmd.Parameters["@UsuarioCreado"].Value = campoNull(cursoEmpleado.UsuarioCreado);
                    cmd.Parameters["@FechaCreado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraCreado"].Value = osistema.HoraSistema;
                    
                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }

        /// <summary>
        /// Metodo nuevo Curso
        /// </summary>
        /// <param name="curso Empleado modificar"></param>
        /// <returns></returns>
        public bool CursoEmpleadoActualizar(tbCursoEmpleado cursoEmpleado)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            StringBuilder sbCurso = new StringBuilder();
            iDB2Command cmd;
            string codigoCurso = string.Empty;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {                   
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sbCurso.Append("UPDATE CURARC SET  CURCO1 = @CodigoTitulo, CURDES = @DescripcionCurso, CURNUM = @NumeroIdentidicacion, CURFEC =  @FechaCurso, CURDUR = @DuracionCurso, ");
                    sbCurso.Append(" CURTIE = @TiempoCurso,  CURAPR = @AprobacionCurso, CURASI = @AsistenciaCurso, CURCO2 = @CodigoCiudad, CURCO3 = @CodigoEntidadEducativa, CURPAT = @PathDocumentoCurso, ");
                    sbCurso.Append(" CUREST = @EstadoCurso, CURUSE = @UsuarioModificado, CURDAT = @FechaModificado, CURTIM = @HoraModificado");
                    sbCurso.Append(" WHERE CURDOC = @DocumentoIdentificacion AND CURCOD = @CodigoCursoEmpleado");
                    queryUpdate = sbCurso.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    
                    cmd.Parameters["@CodigoTitulo"].Value = campoNull(cursoEmpleado.CodigoTitulo);
                    cmd.Parameters["@DescripcionCurso"].Value = campoNull(cursoEmpleado.DescripcionCurso);
                    cmd.Parameters["@NumeroIdentidicacion"].Value = campoNull(cursoEmpleado.NumeroIdentidicacion);
                    cmd.Parameters["@FechaCurso"].Value = campoNull(cursoEmpleado.FechaCurso);
                    cmd.Parameters["@DuracionCurso"].Value = campoNull(cursoEmpleado.DuracionCurso);
                    cmd.Parameters["@TiempoCurso"].Value = campoNull(cursoEmpleado.TiempoCurso);
                    cmd.Parameters["@AprobacionCurso"].Value = campoNull(cursoEmpleado.AprobacionCurso);
                    cmd.Parameters["@AsistenciaCurso"].Value = campoNull(cursoEmpleado.AsistenciaCurso);
                    cmd.Parameters["@CodigoCiudad"].Value = campoNull(cursoEmpleado.CodigoCiudad);
                    cmd.Parameters["@CodigoEntidadEducativa"].Value = campoNull(cursoEmpleado.CodigoEntidadEducativa);
                    cmd.Parameters["@PathDocumentoCurso"].Value = campoNull(cursoEmpleado.PathDocumentoCurso);
                    cmd.Parameters["@EstadoCurso"].Value = campoNull(cursoEmpleado.EstadoCurso);
                    cmd.Parameters["@UsuarioModificado"].Value = campoNull(cursoEmpleado.UsuarioModificado);
                    cmd.Parameters["@FechaModificado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraModificado"].Value = osistema.HoraSistema;
                    cmd.Parameters["@DocumentoIdentificacion"].Value = campoNull(cursoEmpleado.DocumentoIdentificacion);
                    cmd.Parameters["@CodigoCursoEmpleado"].Value = campoNull(cursoEmpleado.CodigoCursoEmpleado); 

                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }

        /// <summary>
        /// Metodo eliminar el registro
        /// </summary>
        /// <param name="DocumentoIdentificacion"></param>
        /// <param name="CodigoCurso"></param>
        /// <returns>true o false</returns>
        public bool CursoEmpleadoEliminar(string DocumentoIdentificacion, string CodigoCurso)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            StringBuilder sbCurso = new StringBuilder();
            iDB2Command cmd;
            string codigoCurso = string.Empty;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sbCurso.Append("DELETE FROM CURARC");                  
                    sbCurso.Append(" WHERE CURDOC = @DocumentoIdentificacion AND CURCOD = @CodigoCursoEmpleado");
                    queryUpdate = sbCurso.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    
                    cmd.Parameters["@DocumentoIdentificacion"].Value = campoNull(DocumentoIdentificacion);
                    cmd.Parameters["@CodigoCursoEmpleado"].Value = campoNull(CodigoCurso);

                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }


        private string GeneraNumeroSolicitud(string cedula)
        {
            string query = "SELECT IFNULL(count(*), 0) + 1 AS Secuencial FROM CURARC WHERE CURDOC = '" + cedula + "'";
            iDB2Command cmd;
            string secuencial = string.Empty;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = llenaCero(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = "";
            }
            return secuencial;
        }

        private string llenaCero(string numero)
        {
            return numero = numero.PadLeft(3, '0');
        }

        private string fechaDate(string fecha)
        {

            string fechaNueva = string.Empty;
            if (fecha != null)
            {
                if (fecha.Length > 0)
                {
                    fechaNueva = fecha.Substring(0, 4) + "-" + fecha.Substring(4, 2) + "-" + fecha.Substring(6, 2);

                }
            }


            return fechaNueva;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

       
    }
}
