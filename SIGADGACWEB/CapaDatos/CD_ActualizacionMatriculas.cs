using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ActualizacionMatriculas
    {
        public static CD_ActualizacionMatriculas _instancia = null;
        private CD_ActualizacionMatriculas()
        {

        }

        public static CD_ActualizacionMatriculas Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ActualizacionMatriculas();
                }
                return _instancia;
            }
        }

        public List<tbActualizacionMatriculas> DetalleMatriculas()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbActualizacionMatriculas> listarSolicitud = new List<tbActualizacionMatriculas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM AERAR1 where substring(AERMAT,1,2)='HC'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;
                

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbActualizacionMatriculas oSolicitud = new tbActualizacionMatriculas();
                        oSolicitud.MATRICULA = dr["AERMAT"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["AERPES"].ToString());
                        
                        oSolicitud.REGION = dr["AERRE2"].ToString();
                        
                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        //busqueda por matricula
        public List<tbActualizacionMatriculas> DetalleMatriculasUnica(string Matricula)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbActualizacionMatriculas> listarSolicitud = new List<tbActualizacionMatriculas>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM AERAR1 where AERMAT='"+Matricula+"'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbActualizacionMatriculas oSolicitud = new tbActualizacionMatriculas();
                        oSolicitud.MATRICULA = dr["AERMAT"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["AERPES"].ToString());

                        oSolicitud.REGION = dr["AERRE2"].ToString();

                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        public tbActualizacionMatriculas DetalleMatriculaUnica(string Matricula)
        {
            tbActualizacionMatriculas oSolicitud = new tbActualizacionMatriculas();
            // List<tbGarantias> listarSolicitud = new List<tbGarantias>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM AERAR1 where AERMAT='" + Matricula + "'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        //   tbGarantias oSolicitud = new tbGarantias();
                        oSolicitud.MATRICULA = dr["AERMAT"].ToString();
                        oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["AERPES"].ToString());

                        oSolicitud.REGION = dr["AERRE2"].ToString();
                        oSolicitud.OID = Convert.ToInt32(dr["AEROI2"].ToString());

                        // listarSolicitud.Add(oSolicitud);
                    }
                }

            }
            catch (Exception ex)
            {
                //  throw ex;
            }
            return oSolicitud;
        }

        public bool ActualizarDatosMatricula(tbActualizacionMatriculas Matricula)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            StringBuilder sbMatricula = new StringBuilder();
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    sbMatricula.Append("UPDATE AERAR1 SET AERPES = @Peso, AERRE2 = @Region ");
                    //sbPersonal.Append(" MAEAL2 = @AlergiaMedioAmbiente, MAEDIS = @Discapacidad, MAEDI3 = @DiscapacidadNombre, MAEPOR = @Porcentaje, ");
                    sbMatricula.Append(" WHERE AERMAT = '" + Matricula.MATRICULA+ "'");
                    queryUpdate = sbMatricula.ToString();
                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();

                    cmd.Parameters["@Peso"].Value = (Matricula.PESOMAXESTRUCTURAL);
                    cmd.Parameters["@Region"].Value = campoNull(Matricula.REGION);

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
                    //cmd.Parameters["@usuarioModifica"].Value = maestroPersonal.UsuarioModificacion;
                    //cmd.Parameters["@fechaModifica"].Value = osistema.FechaSistema;
                    //cmd.Parameters["@horaModifica"].Value = osistema.HoraSistema;
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
        //ACTUALIZA MATRICULA EN EL P550
        public bool ActualizarDatosMatricula550(tbActualizacionMatriculas Matricula)
        {
            bool status = false;
            string query = "UPDATE AERONAVECOMPANIA SET PESOMAXESTRUCTURAL = "+Matricula.PESOMAXESTRUCTURAL+", REGION = "+Matricula.REGION+"" +
                " where OID="+Matricula.OID+"";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    status = true;
                    cmd.Dispose();
                    dr.Close();
                    oConexion.Close();
                }
                return status;
            }
            catch (Exception ex)
            {
               
            }
            return status;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}
