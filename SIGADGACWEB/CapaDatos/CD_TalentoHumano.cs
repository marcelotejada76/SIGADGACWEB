using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TalentoHumano
    {
        public static CD_TalentoHumano _instancia = null;
        private CD_TalentoHumano()
        {

        }

        public static CD_TalentoHumano Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_TalentoHumano();
                }
                return _instancia;
            }
        }


        public tbMaestroPersonal MaestroPersonalPorCedula(string cedula)
        {
            tbMaestroPersonal oMaestroPersonal = new tbMaestroPersonal();
            if (cedula.Trim().Length == 0)
                return oMaestroPersonal;
            string sqlQuery = "";
            StringBuilder sbPersonal = new StringBuilder();
            iDB2Command cmd;
            try
            {
                sbPersonal.Append("SELECT ifnull(rtrim(ltrim(MAEDOC)), '') AS DocumentoIdentificacion,  ifnull(rtrim(ltrim(MAENOM)), '') AS NombreCompleto, ifnull(rtrim(ltrim(MAEFEC)), '') AS FechaNacimiento, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAENU1)), '') AS NumeroTelefonoDomicillo, ifnull(rtrim(ltrim(MAENU2)), '') AS NumeroTelefonoCelular, ifnull(rtrim(ltrim(MAEEMA)), '') AS EmailPersonal,  ifnull(rtrim(ltrim(MAEOB2)), '') AS PathFoto, ");
                sbPersonal.Append(" ifnull(MAEES1, 0) AS Estatura, ifnull(rtrim(ltrim(MAEGEN)), '') AS Genero, ifnull(rtrim(ltrim(MAECOL)), '') AS ColorOjos, ifnull(rtrim(ltrim(MAECO1)), '') AS ColorCaballo, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAETI1)), '') AS TipoSangre, ifnull(rtrim(ltrim(MAEEST)), '') AS EstadoCivil, ifnull(MAEPES, 0) AS Peso, ifnull(rtrim(ltrim(MAEDIR)), '') AS DireccionDomicillo, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAECO9)), '') AS CodigoPais, ifnull(rtrim(ltrim(MAEC01)), '') as CodigoProviencia, ifnull(rtrim(ltrim(MAECO8)), '') as CodigoCiudad, ifnull(rtrim(ltrim(MAEC08)), '') AS CodigoGeograficoCanton,");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEC02)), '') as CodigoCanton, ifnull(rtrim(ltrim(MAEC03)), '') as CodigoParroquia, ifnull(rtrim(ltrim(MAESEC)), '') as SectorDondeVive, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAERE1)), '') AS RegimenLaboral, ifnull(rtrim(ltrim(MAETI3)), '') AS TipoHorario, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEDEC)), '') AS DecimoTerceroAculado, ifnull(rtrim(ltrim(MAEDE1)), '') AS DecimoCuartoAculado, ifnull(rtrim(ltrim(MAEAPO)), '') AS AporteFondoReserva, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAENO1)), '') AS NombreContactoEmergencia, ifnull(rtrim(ltrim(MAETEL)), '') AS TelefonoContactoEmergencia, ifnull(rtrim(ltrim(MAEALE)), '') AS AlergiaMedicina, ifnull(rtrim(ltrim(MAEAL1)), '') AS AlergiaAlimentos, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEAL2)), '') AS AlergiaMedioAmbiente, ifnull(rtrim(ltrim(MAEDIS)), '') AS Discapacidad, ifnull(rtrim(ltrim(MAEDI3)), '') AS DiscapacidadNombre, ifnull(rtrim(ltrim(MAEPOR)), '') AS Porcentaje, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEENF)), '') AS EnfermedadCatrastrofica, ifnull(rtrim(ltrim(MAEEN1)), '') AS EnfermedadCatrastroficaNombre, ifnull(rtrim(ltrim(MAESUS)), '') AS Sustituto, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAENO2)), '') AS NombreFamiliarSustituto, ifnull(rtrim(ltrim(MAEPAR)), '') AS ParentescoSubtituto, ifnull(rtrim(ltrim(MAESEN)), '') AS SenescytNumeroRegistro, ifnull(rtrim(ltrim(MAEUL4)), '') AS UltimoTituloObtenido ");
                sbPersonal.Append(" FROM MAEAR1  WHERE MAEDOC = '" + cedula + "' AND MAEES4 = '1'");

                sqlQuery = sbPersonal.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oMaestroPersonal.DocumentoIdentificacion = dr["DocumentoIdentificacion"].ToString();
                        oMaestroPersonal.NombreCompleto = dr["NombreCompleto"].ToString();
                        oMaestroPersonal.FechaNacimiento = fechaDate(dr["FechaNacimiento"].ToString());
                        oMaestroPersonal.NumeroTelefonoDomicillo = dr["NumeroTelefonoDomicillo"].ToString();
                        oMaestroPersonal.NumeroTelefonoCelular = dr["NumeroTelefonoCelular"].ToString();
                        oMaestroPersonal.EmailPersonal = dr["EmailPersonal"].ToString();
                        oMaestroPersonal.PathFoto = dr["PathFoto"].ToString();
                        oMaestroPersonal.Estatura = decimal.Parse(dr["Estatura"].ToString());
                        oMaestroPersonal.Genero = dr["Genero"].ToString();
                        oMaestroPersonal.ColorOjos = dr["ColorOjos"].ToString();
                        oMaestroPersonal.ColorCaballo = dr["ColorCaballo"].ToString();
                        oMaestroPersonal.TipoSangre = dr["TipoSangre"].ToString();
                        oMaestroPersonal.EstadoCivil = dr["EstadoCivil"].ToString();
                        oMaestroPersonal.Peso = decimal.Parse(dr["Peso"].ToString());
                        oMaestroPersonal.DireccionDomicillo = dr["DireccionDomicillo"].ToString();
                        oMaestroPersonal.CodigoPais = dr["CodigoPais"].ToString();
                        oMaestroPersonal.CodigoProviencia = dr["CodigoProviencia"].ToString();
                        oMaestroPersonal.CodigoCiudad = dr["CodigoCiudad"].ToString();
                        oMaestroPersonal.CodigoGeograficoCanton = dr["CodigoGeograficoCanton"].ToString();
                        oMaestroPersonal.CodigoCanton = dr["CodigoCanton"].ToString();
                        oMaestroPersonal.CodigoParroquia = dr["CodigoParroquia"].ToString();
                        oMaestroPersonal.SectorDondeVive = dr["SectorDondeVive"].ToString();
                        oMaestroPersonal.RegimenLaboral = dr["RegimenLaboral"].ToString();
                        oMaestroPersonal.TipoHorario = dr["TipoHorario"].ToString();
                        oMaestroPersonal.DecimoTerceroAculado = dr["DecimoTerceroAculado"].ToString();
                        oMaestroPersonal.DecimoCuartoAculado = dr["DecimoCuartoAculado"].ToString();
                        oMaestroPersonal.DecimoCuartoAculado = dr["DecimoCuartoAculado"].ToString();
                        oMaestroPersonal.AporteFondoReserva = dr["AporteFondoReserva"].ToString();
                        oMaestroPersonal.NombreContactoEmergencia = dr["NombreContactoEmergencia"].ToString();
                        oMaestroPersonal.TelefonoContactoEmergencia = dr["TelefonoContactoEmergencia"].ToString();
                        oMaestroPersonal.AlergiaMedicina = dr["AlergiaMedicina"].ToString();
                        oMaestroPersonal.AlergiaAlimentos = dr["AlergiaAlimentos"].ToString();
                        oMaestroPersonal.AlergiaMedioAmbiente = dr["AlergiaMedioAmbiente"].ToString();
                        oMaestroPersonal.Discapacidad = dr["Discapacidad"].ToString();
                        oMaestroPersonal.DiscapacidadNombre = dr["DiscapacidadNombre"].ToString();
                        oMaestroPersonal.Porcentaje = dr["Porcentaje"].ToString();
                        oMaestroPersonal.EnfermedadCatrastrofica = dr["EnfermedadCatrastrofica"].ToString();
                        oMaestroPersonal.EnfermedadCatrastroficaNombre = dr["EnfermedadCatrastroficaNombre"].ToString();
                        oMaestroPersonal.Sustituto = dr["Sustituto"].ToString();
                        oMaestroPersonal.NombreFamiliarSustituto = dr["NombreFamiliarSustituto"].ToString();
                        oMaestroPersonal.ParentescoSubtituto = dr["ParentescoSubtituto"].ToString();
                        oMaestroPersonal.SenescytNumeroRegistro = dr["SenescytNumeroRegistro"].ToString();
                        oMaestroPersonal.UltimoTituloObtenido = dr["UltimoTituloObtenido"].ToString();

                        //Metodo lista todos los cursos de empleados
                        oMaestroPersonal.oCursoEmpleado = CD_CursoEmpleado.Instancia.ListarCursosEmpleados(oMaestroPersonal.DocumentoIdentificacion);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return oMaestroPersonal;
        }

        public List<tbMaestroPersonal> ListarMaestroPersonalPorCedula(string cedula)
        {
            List<tbMaestroPersonal> ListarMaestroPersonal = new List<tbMaestroPersonal>();
            if (cedula.Trim().Length == 0)
                return ListarMaestroPersonal;

            string sqlQuery = "";
            StringBuilder sbPersonal = new StringBuilder();
            sqlQuery = sqlQuery.ToString();
            iDB2Command cmd;
            try
            {
                sbPersonal.Append("SELECT ifnull(rtrim(ltrim(MAEDOC)), '') AS DocumentoIdentificacion,  ifnull(rtrim(ltrim(MAENOM)), '') AS NombreCompleto, ifnull(rtrim(ltrim(MAEFEC)), '') AS FechaNacimiento, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAENU1)), '') AS NumeroTelefonoDomicillo, ifnull(rtrim(ltrim(MAENU2)), '') AS NumeroTelefonoCelular, ifnull(rtrim(ltrim(MAEEMA)), '') AS EmailPersonal, ");
                sbPersonal.Append(" ifnull(MAEES1, 0) AS Estatura, ifnull(rtrim(ltrim(MAEGEN)), '') AS Genero, ifnull(rtrim(ltrim(MAECOL)), '') AS ColorOjos, ifnull(rtrim(ltrim(MAECO1)), '') AS ColorCaballo, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAETI1)), '') AS TipoSangre, ifnull(rtrim(ltrim(MAEEST)), '') AS EstadoCivil, ifnull(MAEPES, 0) AS Peso, ifnull(rtrim(ltrim(MAEDIR)), '') AS DireccionDomicillo, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEC08)), '') AS CodigoGeograficoCanton, ifnull(rtrim(ltrim(MAESEC)), '') AS SectorDondeVive, ifnull(rtrim(ltrim(MAERE1)), '') AS RegimenLaboral, ifnull(rtrim(ltrim(MAETI3)), '') AS TipoHorario, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEDEC)), '') AS DecimoTerceroAculado, ifnull(rtrim(ltrim(MAEDE1)), '') AS DecimoCuartoAculado, ifnull(rtrim(ltrim(MAEAPO)), '') AS AporteFondoReserva, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAENO1)), '') AS NombreContactoEmergencia, ifnull(rtrim(ltrim(MAETEL)), '') AS TelefonoContactoEmergencia, ifnull(rtrim(ltrim(MAEALE)), '') AS AlergiaMedicina, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEAL1)), '') AS AlergiaAlimentos, ifnull(rtrim(ltrim(MAEAL2)), '') AS AlergiaMedioAmbiente, ifnull(rtrim(ltrim(MAEDIS)), '') AS Discapacidad, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEDI3)), '') AS DiscapacidadNombre, ifnull(rtrim(ltrim(MAEPOR)), '') AS Porcentaje, ifnull(rtrim(ltrim(MAEENF)), '') AS EnfermedadCatrastrofica, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEEN1)), '') AS EnfermedadCatrastroficaNombre, ifnull(rtrim(ltrim(MAESUS)), '') AS Sustituto, ifnull(rtrim(ltrim(MAENO2)), '') AS NombreFamiliarSustituto, ");
                sbPersonal.Append(" ifnull(rtrim(ltrim(MAEPAR)), '') AS ParentescoSubtituto, ifnull(rtrim(ltrim(MAESEN)), '') AS SenescytNumeroRegistro, ifnull(rtrim(ltrim(MAEUL4)), '') AS UltimoTituloObtenido ");
                sbPersonal.Append(" FROM MAEAR1  WHERE MAEDOC = '" + cedula + "' AND MAEES4 = '1'");
                sqlQuery = sbPersonal.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbMaestroPersonal oMaestroPersonal = new tbMaestroPersonal();
                        oMaestroPersonal.DocumentoIdentificacion = dr["DocumentoIdentificacion"].ToString();
                        oMaestroPersonal.NombreCompleto = dr["NombreCompleto"].ToString();
                        oMaestroPersonal.FechaNacimiento = dr["FechaNacimiento"].ToString();
                        oMaestroPersonal.NumeroTelefonoDomicillo = dr["NumeroTelefonoDomicillo"].ToString();
                        oMaestroPersonal.NumeroTelefonoCelular = dr["NumeroTelefonoCelular"].ToString();
                        oMaestroPersonal.EmailPersonal = dr["EmailPersonal"].ToString();
                        oMaestroPersonal.Estatura = decimal.Parse(dr["Estatura"].ToString());
                        oMaestroPersonal.Genero = dr["Genero"].ToString();
                        oMaestroPersonal.ColorOjos = dr["ColorOjos"].ToString();
                        oMaestroPersonal.ColorCaballo = dr["ColorCaballo"].ToString();
                        oMaestroPersonal.TipoSangre = dr["TipoSangre"].ToString();
                        oMaestroPersonal.EstadoCivil = dr["EstadoCivil"].ToString();
                        oMaestroPersonal.Peso = decimal.Parse(dr["Peso"].ToString());
                        oMaestroPersonal.DireccionDomicillo = dr["DireccionDomicillo"].ToString();
                        oMaestroPersonal.CodigoGeograficoCanton = dr["CodigoGeograficoCanton"].ToString();
                        oMaestroPersonal.SectorDondeVive = dr["SectorDondeVive"].ToString();
                        oMaestroPersonal.RegimenLaboral = dr["RegimenLaboral"].ToString();
                        oMaestroPersonal.TipoHorario = dr["TipoHorario"].ToString();
                        oMaestroPersonal.DecimoTerceroAculado = dr["DecimoTerceroAculado"].ToString();
                        oMaestroPersonal.DecimoCuartoAculado = dr["DecimoCuartoAculado"].ToString();
                        oMaestroPersonal.DecimoCuartoAculado = dr["DecimoCuartoAculado"].ToString();
                        oMaestroPersonal.AporteFondoReserva = dr["AporteFondoReserva"].ToString();

                        oMaestroPersonal.NombreContactoEmergencia = dr["NombreContactoEmergencia"].ToString();
                        oMaestroPersonal.TelefonoContactoEmergencia = dr["TelefonoContactoEmergencia"].ToString();
                        oMaestroPersonal.AlergiaMedicina = dr["AlergiaMedicina"].ToString();
                        oMaestroPersonal.AlergiaAlimentos = dr["AlergiaAlimentos"].ToString();
                        oMaestroPersonal.AlergiaMedioAmbiente = dr["AlergiaMedioAmbiente"].ToString();
                        oMaestroPersonal.Discapacidad = dr["Discapacidad"].ToString();
                        oMaestroPersonal.DiscapacidadNombre = dr["DiscapacidadNombre"].ToString();
                        oMaestroPersonal.Porcentaje = dr["Porcentaje"].ToString();
                        oMaestroPersonal.EnfermedadCatrastrofica = dr["EnfermedadCatrastrofica"].ToString();
                        oMaestroPersonal.EnfermedadCatrastroficaNombre = dr["EnfermedadCatrastroficaNombre"].ToString();
                        oMaestroPersonal.Sustituto = dr["Sustituto"].ToString();
                        oMaestroPersonal.NombreFamiliarSustituto = dr["NombreFamiliarSustituto"].ToString();
                        oMaestroPersonal.ParentescoSubtituto = dr["ParentescoSubtituto"].ToString();
                        oMaestroPersonal.SenescytNumeroRegistro = dr["SenescytNumeroRegistro"].ToString();
                        oMaestroPersonal.UltimoTituloObtenido = dr["UltimoTituloObtenido"].ToString();

                        ListarMaestroPersonal.Add(oMaestroPersonal);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ListarMaestroPersonal;
        }


        /// <summary>
        /// metodo actualiza el Maestro Personal
        /// </summary>
        /// <param name="maestroPersonal"></param>
        /// <returns></returns>
        public bool MaestroPersonalActualizar(tbMaestroPersonal maestroPersonal)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            StringBuilder sbPersonal = new StringBuilder();
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sbPersonal.Append("UPDATE MAEAR1 SET MAENOM = @NombreCompleto, MAEFEC= @FechaNacimiento,");                    
                    sbPersonal.Append(" MAENU1 = @NumeroTelefonoDomicillo, MAENU2 = @NumeroTelefonoCelular, MAEEMA = @EmailPersonal, ");
                    sbPersonal.Append(" MAEES1 = @Estatura, MAEGEN = @Genero, MAECOL = @ColorOjos, MAECO1 = @ColorCaballo, ");
                    sbPersonal.Append(" MAETI1 = @TipoSangre, MAEEST = @EstadoCivil, MAEPES = @Peso, MAEDIR = @DireccionDomicillo, ");
                    sbPersonal.Append(" MAECO9 = @CodigoPais, MAEC01 = @CodigoProviencia, MAECO8 = @CodigoCiudad, MAEC08 = @CodigoGeograficoCanton,");
                    sbPersonal.Append(" MAEC02 = @CodigoCanton, MAEC03 = @CodigoParroquia, MAESEC = @SectorDondeVive, ");
                    sbPersonal.Append(" MAERE1 = @RegimenLaboral, MAETI3 = @TipoHorario, ");
                    sbPersonal.Append(" MAEDEC = @DecimoTerceroAculado, MAEDE1 = @DecimoCuartoAculado, MAEAPO = @AporteFondoReserva, ");
                    //sbPersonal.Append(" MAENO1 = @NombreContactoEmergencia, MAETEL = @TelefonoContactoEmergencia, MAEALE = @AlergiaMedicina, MAEAL1 = @AlergiaAlimentos, ");
                    //sbPersonal.Append(" MAEAL2 = @AlergiaMedioAmbiente, MAEDIS = @Discapacidad, MAEDI3 = @DiscapacidadNombre, MAEPOR = @Porcentaje, ");
                    //sbPersonal.Append(" MAEENF = @EnfermedadCatrastrofica, MAEEN1 = @EnfermedadCatrastroficaNombre, MAESUS = @Sustituto, ");
                    //sbPersonal.Append(" MAENO2 = @NombreFamiliarSustituto, MAEPAR = @ParentescoSubtituto, MAESEN = @SenescytNumeroRegistro, MAEUL4 = @UltimoTituloObtenido, ");
                    sbPersonal.Append(" MAEOB2 = @PathFoto, MAEUS1 = @usuarioModifica, MAEDA1 = @fechaModifica, MAETI4 = @horaModifica ");
                    sbPersonal.Append(" WHERE MAEDOC = '" + maestroPersonal.DocumentoIdentificacion + "' AND MAEES4 = '1'");
                    queryUpdate = sbPersonal.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NombreCompleto"].Value = campoNull(maestroPersonal.NombreCompleto);
                    cmd.Parameters["@FechaNacimiento"].Value = campoNull(maestroPersonal.FechaNacimiento);
                    cmd.Parameters["@NumeroTelefonoDomicillo"].Value = campoNull(maestroPersonal.NumeroTelefonoDomicillo);
                    cmd.Parameters["@NumeroTelefonoCelular"].Value = campoNull(maestroPersonal.NumeroTelefonoCelular);
                    cmd.Parameters["@EmailPersonal"].Value = campoNull(maestroPersonal.EmailPersonal);                                    
                    cmd.Parameters["@Estatura"].Value = maestroPersonal.Estatura;
                    cmd.Parameters["@Genero"].Value = campoNull(maestroPersonal.Genero);
                    cmd.Parameters["@ColorOjos"].Value = campoNull(maestroPersonal.ColorOjos);
                    cmd.Parameters["@ColorCaballo"].Value = campoNull(maestroPersonal.ColorCaballo);
                    cmd.Parameters["@TipoSangre"].Value = campoNull(maestroPersonal.TipoSangre);
                    cmd.Parameters["@EstadoCivil"].Value = campoNull(maestroPersonal.EstadoCivil);
                    cmd.Parameters["@Peso"].Value = maestroPersonal.Peso;
                    cmd.Parameters["@DireccionDomicillo"].Value = campoNull(maestroPersonal.DireccionDomicillo);
                    cmd.Parameters["@CodigoPais"].Value = campoNull(maestroPersonal.CodigoPais);
                    cmd.Parameters["@CodigoProviencia"].Value = campoNull(maestroPersonal.CodigoProviencia);
                    cmd.Parameters["@CodigoCiudad"].Value = campoNull(maestroPersonal.CodigoCiudad);
                    cmd.Parameters["@CodigoGeograficoCanton"].Value = campoNull(maestroPersonal.CodigoGeograficoCanton);
                    cmd.Parameters["@CodigoCanton"].Value = campoNull(maestroPersonal.CodigoCanton);
                    cmd.Parameters["@CodigoParroquia"].Value = campoNull(maestroPersonal.CodigoParroquia);
                    cmd.Parameters["@SectorDondeVive"].Value = campoNull(maestroPersonal.SectorDondeVive);
                    cmd.Parameters["@RegimenLaboral"].Value = campoNull(maestroPersonal.RegimenLaboral);
                    cmd.Parameters["@TipoHorario"].Value = campoNull(maestroPersonal.TipoHorario);
                    cmd.Parameters["@DecimoTerceroAculado"].Value = campoNull(maestroPersonal.DecimoTerceroAculado);
                    cmd.Parameters["@DecimoCuartoAculado"].Value = campoNull(maestroPersonal.DecimoCuartoAculado);
                    cmd.Parameters["@AporteFondoReserva"].Value = campoNull(maestroPersonal.AporteFondoReserva);
                    //cmd.Parameters["@NombreContactoEmergencia"].Value = campoNull(maestroPersonal.NombreContactoEmergencia);
                    //cmd.Parameters["@TelefonoContactoEmergencia"].Value = campoNull(maestroPersonal.TelefonoContactoEmergencia);
                    //cmd.Parameters["@AlergiaMedicina"].Value = campoNull(maestroPersonal.AlergiaMedicina);
                    //cmd.Parameters["@AlergiaAlimentos"].Value = campoNull(maestroPersonal.AlergiaAlimentos);
                    //cmd.Parameters["@AlergiaMedioAmbiente"].Value = campoNull(maestroPersonal.AlergiaMedioAmbiente);
                    //cmd.Parameters["@Discapacidad"].Value = campoNull(maestroPersonal.Discapacidad);
                    //cmd.Parameters["@DiscapacidadNombre"].Value = campoNull(maestroPersonal.DiscapacidadNombre);
                    //cmd.Parameters["@Porcentaje"].Value = campoNull(maestroPersonal.Porcentaje);
                    //cmd.Parameters["@EnfermedadCatrastrofica"].Value = campoNull(maestroPersonal.EnfermedadCatrastrofica);
                    //cmd.Parameters["@EnfermedadCatrastroficaNombre"].Value = campoNull(maestroPersonal.EnfermedadCatrastroficaNombre);
                    //cmd.Parameters["@Sustituto"].Value = campoNull(maestroPersonal.Sustituto);
                    //cmd.Parameters["@NombreFamiliarSustituto"].Value = campoNull(maestroPersonal.NombreFamiliarSustituto);
                    //cmd.Parameters["@ParentescoSubtituto"].Value = campoNull(maestroPersonal.ParentescoSubtituto);
                    //cmd.Parameters["@SenescytNumeroRegistro"].Value = campoNull(maestroPersonal.SenescytNumeroRegistro);
                    //cmd.Parameters["@UltimoTituloObtenido"].Value = campoNull(maestroPersonal.UltimoTituloObtenido);
                    cmd.Parameters["@PathFoto"].Value = campoNull(maestroPersonal.PathFoto);
                    cmd.Parameters["@usuarioModifica"].Value = maestroPersonal.UsuarioModificacion;
                    cmd.Parameters["@fechaModifica"].Value = osistema.FechaSistema;
                    cmd.Parameters["@horaModifica"].Value = osistema.HoraSistema;            
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


        public bool MaestroPersonalActualizarDatosAdicionales(tbMaestroPersonal maestroPersonal)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            StringBuilder sbPersonal = new StringBuilder();
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sbPersonal.Append("UPDATE MAEAR1 SET MAENO1 = @NombreContactoEmergencia, MAETEL = @TelefonoContactoEmergencia, MAEALE = @AlergiaMedicina, MAEAL1 = @AlergiaAlimentos, ");
                    sbPersonal.Append(" MAEAL2 = @AlergiaMedioAmbiente, MAEDIS = @Discapacidad, MAEDI3 = @DiscapacidadNombre, MAEPOR = @Porcentaje, ");
                    sbPersonal.Append(" MAEENF = @EnfermedadCatrastrofica, MAEEN1 = @EnfermedadCatrastroficaNombre, MAESUS = @Sustituto, ");
                    sbPersonal.Append(" MAENO2 = @NombreFamiliarSustituto, MAEPAR = @ParentescoSubtituto, MAESEN = @SenescytNumeroRegistro, MAEUL4 = @UltimoTituloObtenido, ");
                    sbPersonal.Append(" MAEUS1 = @usuarioModifica, MAEDA1 = @fechaModifica, MAETI4 = @horaModifica ");
                    sbPersonal.Append(" WHERE MAEDOC = '" + maestroPersonal.DocumentoIdentificacion + "' AND MAEES4 = '1'");
                    queryUpdate = sbPersonal.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                  
                    cmd.Parameters["@NombreContactoEmergencia"].Value = campoNull(maestroPersonal.NombreContactoEmergencia);
                    cmd.Parameters["@TelefonoContactoEmergencia"].Value = campoNull(maestroPersonal.TelefonoContactoEmergencia);
                    cmd.Parameters["@AlergiaMedicina"].Value = campoNull(maestroPersonal.AlergiaMedicina);
                    cmd.Parameters["@AlergiaAlimentos"].Value = campoNull(maestroPersonal.AlergiaAlimentos);
                    cmd.Parameters["@AlergiaMedioAmbiente"].Value = campoNull(maestroPersonal.AlergiaMedioAmbiente);
                    cmd.Parameters["@Discapacidad"].Value = campoNull(maestroPersonal.Discapacidad);
                    cmd.Parameters["@DiscapacidadNombre"].Value = campoNull(maestroPersonal.DiscapacidadNombre);
                    cmd.Parameters["@Porcentaje"].Value = campoNull(maestroPersonal.Porcentaje);
                    cmd.Parameters["@EnfermedadCatrastrofica"].Value = campoNull(maestroPersonal.EnfermedadCatrastrofica);
                    cmd.Parameters["@EnfermedadCatrastroficaNombre"].Value = campoNull(maestroPersonal.EnfermedadCatrastroficaNombre);
                    cmd.Parameters["@Sustituto"].Value = campoNull(maestroPersonal.Sustituto);
                    cmd.Parameters["@NombreFamiliarSustituto"].Value = campoNull(maestroPersonal.NombreFamiliarSustituto);
                    cmd.Parameters["@ParentescoSubtituto"].Value = campoNull(maestroPersonal.ParentescoSubtituto);
                    cmd.Parameters["@SenescytNumeroRegistro"].Value = campoNull(maestroPersonal.SenescytNumeroRegistro);
                    cmd.Parameters["@UltimoTituloObtenido"].Value = campoNull(maestroPersonal.UltimoTituloObtenido);                    
                    cmd.Parameters["@usuarioModifica"].Value = maestroPersonal.UsuarioModificacion;
                    cmd.Parameters["@fechaModifica"].Value = osistema.FechaSistema;
                    cmd.Parameters["@horaModifica"].Value = osistema.HoraSistema;
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

        public string  RetornaCaracteresNombrePorCedula(string cedula)
        {
         
            string sqlQuery = "";
            StringBuilder sbPersonal = new StringBuilder();
            iDB2Command cmd;
            string codigoApellido = string.Empty;
            try
            {
                sbPersonal.Append(" SELECT SUBSTR(MAENOM, 1, 3) AS codigoApellido  FROM MAEAR1 WHERE MAEDOC = '" + cedula + "' AND MAEES4 = '1'");
                sqlQuery = sbPersonal.ToString();
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(sqlQuery, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        codigoApellido = dr["codigoApellido"].ToString();
                        
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return codigoApellido;
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
