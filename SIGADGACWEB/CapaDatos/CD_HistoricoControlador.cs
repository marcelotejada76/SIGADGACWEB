using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_HistoricoControlador
    {
        public static CD_HistoricoControlador _instancia = null;
        private CD_HistoricoControlador()
        {

        }

        public static CD_HistoricoControlador Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_HistoricoControlador();
                }
                return _instancia;
            }
        }



        public List<tbHistoricoAtc> ConsultaHistoricoControlador()
        {

            List<tbHistoricoAtc> listarSolicitud = new List<tbHistoricoAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPHARC");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbHistoricoAtc oSolicitud = new tbHistoricoAtc();


                        oSolicitud.Licencia_ATC = dr["OPHLIC"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPHINS"].ToString().Trim();
                        oSolicitud.Apellido_Paterno = dr["OPHAPE"].ToString().Trim();
                        oSolicitud.Apellido_Materno = dr["OPHAP1"].ToString().Trim();
                        oSolicitud.Nombres = dr["OPHNOM"].ToString().Trim();
                        oSolicitud.Nacimiento = dr["OPHNAC"].ToString().Trim();
                        oSolicitud.Cédula = dr["OPHCED"].ToString().Trim();
                        oSolicitud.Dirección_Domicilio = dr["OPHDIR"].ToString().Trim();
                        oSolicitud.Teléfono_Domicilio = dr["OPHTEL"].ToString().Trim();
                        oSolicitud.Ciudad_Domilicio = dr["OPHCIU"].ToString().Trim();
                        oSolicitud.Teléfono_Celular = dr["OPHTE1"].ToString().Trim();
                        oSolicitud.Email_Institucional = dr["OPHEMA"].ToString().Trim();
                        oSolicitud.Email_Particular = dr["OPHEM1"].ToString().Trim();
                        oSolicitud.Estado_Civil = dr["OPHEST"].ToString().Trim();
                        oSolicitud.Conyugue = dr["OPHCON"].ToString().Trim();
                        oSolicitud.Fecha_ingreso_DGAC = dr["OPHFEC"].ToString().Trim();
                        oSolicitud.Cargo_actual = dr["OPHCAR"].ToString().Trim();
                        oSolicitud.años = dr["OPHANO"].ToString().Trim();
                        oSolicitud.meses = dr["OPHMES"].ToString().Trim();
                        oSolicitud.Dependencia_actual = dr["OPHDEP"].ToString().Trim();
                        oSolicitud.Aeropuerto = dr["OPHAER"].ToString().Trim();
                        oSolicitud.Región = dr["OPHRE1"].ToString().Trim();
                        oSolicitud.Habilitación = dr["OPHHAB"].ToString().Trim();
                        oSolicitud.Fecha_Habilitación = dr["OPHFE1"].ToString().Trim();
                        oSolicitud.Doble_Habilitación = dr["OPHDOB"].ToString().Trim();
                        oSolicitud.Otra_Habilitación = dr["OPHOTR"].ToString().Trim();
                        oSolicitud.Caducidad_CertifMedi = dr["OPHCAD"].ToString().Trim();
                        oSolicitud.Status = dr["OPHSTA"].ToString().Trim();
                        oSolicitud.Competencia_Lingüíst = dr["OPHCOM"].ToString().Trim();
                        oSolicitud.Caducidad_Certificado_Competencia_Lingui = dr["OPHCA1"].ToString().Trim();

                        //LLENA DETALE DE dependencias
                        oSolicitud.oDetalleHistoricoDependenciaAtc = CD_DetalleHistoricoDependenciaAtc.Instancia.HistoricoDetalleDependenciaAtc(oSolicitud.Licencia_ATC);

                        //LLENA DETALE DE cursos
                        oSolicitud.oDetalleHistoricoCursoAtc = CD_DetalleHistoricoCursosAtc.Instancia.HistoricoDetalleCursosAtc(oSolicitud.Licencia_ATC);

                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

        public List<tbHistoricoAtc> HistoricoControladorLicencia(string Licencia)
        {

            List<tbHistoricoAtc> listarSolicitud = new List<tbHistoricoAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPHARC  WHERE OPHLIC = '" + Licencia + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbHistoricoAtc oSolicitud = new tbHistoricoAtc();


                        oSolicitud.Licencia_ATC = dr["OPHLIC"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPHINS"].ToString().Trim();
                        oSolicitud.Apellido_Paterno = dr["OPHAPE"].ToString().Trim();
                        oSolicitud.Apellido_Materno = dr["OPHAP1"].ToString().Trim();
                        oSolicitud.Nombres = dr["OPHNOM"].ToString().Trim();
                        oSolicitud.Nacimiento = dr["OPHNAC"].ToString().Trim();
                        oSolicitud.Cédula = dr["OPHCED"].ToString().Trim();
                        oSolicitud.Dirección_Domicilio = dr["OPHDIR"].ToString().Trim();
                        oSolicitud.Teléfono_Domicilio = dr["OPHTEL"].ToString().Trim();
                        oSolicitud.Ciudad_Domilicio = dr["OPHCIU"].ToString().Trim();
                        oSolicitud.Teléfono_Celular = dr["OPHTE1"].ToString().Trim();
                        oSolicitud.Email_Institucional = dr["OPHEMA"].ToString().Trim();
                        oSolicitud.Email_Particular = dr["OPHEM1"].ToString().Trim();
                        oSolicitud.Estado_Civil = dr["OPHEST"].ToString().Trim();
                        oSolicitud.Conyugue = dr["OPHCON"].ToString().Trim();
                        oSolicitud.Fecha_ingreso_DGAC = dr["OPHFEC"].ToString().Trim();
                        oSolicitud.Cargo_actual = dr["OPHCAR"].ToString().Trim();
                        oSolicitud.Dependencia_actual = dr["OPHDEP"].ToString().Trim();
                        oSolicitud.años = dr["OPHANO"].ToString().Trim();
                        oSolicitud.meses = dr["OPHMES"].ToString().Trim();
                        oSolicitud.Aeropuerto = dr["OPHAER"].ToString().Trim();
                        oSolicitud.Región = dr["OPHRE1"].ToString().Trim();
                        oSolicitud.Habilitación = dr["OPHHAB"].ToString().Trim();
                        oSolicitud.Fecha_Habilitación = dr["OPHFE1"].ToString().Trim();
                        oSolicitud.Doble_Habilitación = dr["OPHDOB"].ToString().Trim();
                        oSolicitud.Otra_Habilitación = dr["OPHOTR"].ToString().Trim();
                        oSolicitud.Caducidad_CertifMedi = dr["OPHCAD"].ToString().Trim();
                        oSolicitud.Status = dr["OPHSTA"].ToString().Trim();
                        oSolicitud.Competencia_Lingüíst = dr["OPHCOM"].ToString().Trim();
                        oSolicitud.Caducidad_Certificado_Competencia_Lingui = dr["OPHCA1"].ToString().Trim();
                        //LLENA DETALE DE dependencias
                        oSolicitud.oDetalleHistoricoDependenciaAtc = CD_DetalleHistoricoDependenciaAtc.Instancia.HistoricoDetalleDependenciaAtc(oSolicitud.Licencia_ATC);

                        //LLENA DETALE DE cursos
                        oSolicitud.oDetalleHistoricoCursoAtc = CD_DetalleHistoricoCursosAtc.Instancia.HistoricoDetalleCursosAtc(oSolicitud.Licencia_ATC);


                        listarSolicitud.Add(oSolicitud);
                    }


                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }
        public tbHistoricoAtc HistoricoConsultacControladorLicencia(string Licencia)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbHistoricoAtc listarSolicitud = new tbHistoricoAtc();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPHARC WHERE OPHLIC = '" + Licencia + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbHistoricoAtc oSolicitud = new tbHistoricoAtc();


                        oSolicitud.Licencia_ATC = dr["OPHLIC"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPHINS"].ToString().Trim();
                        oSolicitud.Apellido_Paterno = dr["OPHAPE"].ToString().Trim();
                        oSolicitud.Apellido_Materno = dr["OPHAP1"].ToString().Trim();
                        oSolicitud.Nombres = dr["OPHNOM"].ToString().Trim();
                        oSolicitud.Nacimiento = dr["OPHNAC"].ToString().Trim();
                        oSolicitud.Cédula = dr["OPHCED"].ToString().Trim();
                        oSolicitud.Dirección_Domicilio = dr["OPHDIR"].ToString().Trim();
                        oSolicitud.Teléfono_Domicilio = dr["OPHTEL"].ToString().Trim();
                        oSolicitud.Ciudad_Domilicio = dr["OPHCIU"].ToString().Trim();
                        oSolicitud.Teléfono_Celular = dr["OPHTE1"].ToString().Trim();
                        oSolicitud.Email_Institucional = dr["OPHEMA"].ToString().Trim();
                        oSolicitud.Email_Particular = dr["OPHEM1"].ToString().Trim();
                        oSolicitud.Estado_Civil = dr["OPHEST"].ToString().Trim();
                        oSolicitud.Conyugue = dr["OPHCON"].ToString().Trim();
                        oSolicitud.Fecha_ingreso_DGAC = dr["OPHFEC"].ToString().Trim();
                        oSolicitud.años = dr["OPHANO"].ToString().Trim();
                        oSolicitud.meses = dr["OPHMES"].ToString().Trim();
                        oSolicitud.Cargo_actual = dr["OPHCAR"].ToString().Trim();
                        oSolicitud.Dependencia_actual = dr["OPHDEP"].ToString().Trim();
                        oSolicitud.Aeropuerto = dr["OPHAER"].ToString().Trim();
                        oSolicitud.Región = dr["OPHRE1"].ToString().Trim();
                        oSolicitud.Habilitación = dr["OPHHAB"].ToString().Trim();
                        oSolicitud.Fecha_Habilitación = dr["OPHFE1"].ToString().Trim();
                        oSolicitud.Doble_Habilitación = dr["OPHDOB"].ToString().Trim();
                        oSolicitud.Otra_Habilitación = dr["OPHOTR"].ToString().Trim();
                        oSolicitud.Caducidad_CertifMedi = dr["OPHCAD"].ToString().Trim();
                        oSolicitud.Status = dr["OPHSTA"].ToString().Trim();
                        oSolicitud.Competencia_Lingüíst = dr["OPHCOM"].ToString().Trim();
                        oSolicitud.Caducidad_Certificado_Competencia_Lingui = dr["OPHCA1"].ToString().Trim();
                        //LLENA DETALE DE dependencias
                        oSolicitud.oDetalleHistoricoDependenciaAtc = CD_DetalleHistoricoDependenciaAtc.Instancia.HistoricoDetalleDependenciaAtc(oSolicitud.Licencia_ATC);

                        //LLENA DETALE DE cursos
                        oSolicitud.oDetalleHistoricoCursoAtc = CD_DetalleHistoricoCursosAtc.Instancia.HistoricoDetalleCursosAtc(oSolicitud.Licencia_ATC);




                        listarSolicitud = oSolicitud;
                    }

                    dr.Close();
                    oConexion.Close();

                }

                return listarSolicitud;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }
        //POR APELLIDO
        public List<tbHistoricoAtc> HistoricoControladorLicenciaApellido(string Apellido)
        {

            List<tbHistoricoAtc> listarSolicitud = new List<tbHistoricoAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPHARC  WHERE OPHAPE like ('%" + Apellido + "%')");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbHistoricoAtc oSolicitud = new tbHistoricoAtc();

                        oSolicitud.Licencia_ATC = dr["OPHLIC"].ToString().Trim();
                        oSolicitud.Institucion = dr["OPHINS"].ToString().Trim();
                        oSolicitud.Apellido_Paterno = dr["OPHAPE"].ToString().Trim();
                        oSolicitud.Apellido_Materno = dr["OPHAP1"].ToString().Trim();
                        oSolicitud.Nombres = dr["OPHNOM"].ToString().Trim();
                        oSolicitud.Nacimiento = dr["OPHNAC"].ToString().Trim();
                        oSolicitud.Cédula = dr["OPHCED"].ToString().Trim();
                        oSolicitud.Dirección_Domicilio = dr["OPHDIR"].ToString().Trim();
                        oSolicitud.Teléfono_Domicilio = dr["OPHTEL"].ToString().Trim();
                        oSolicitud.Ciudad_Domilicio = dr["OPHCIU"].ToString().Trim();
                        oSolicitud.Teléfono_Celular = dr["OPHTE1"].ToString().Trim();
                        oSolicitud.Email_Institucional = dr["OPHEMA"].ToString().Trim();
                        oSolicitud.Email_Particular = dr["OPHEM1"].ToString().Trim();
                        oSolicitud.Estado_Civil = dr["OPHEST"].ToString().Trim();
                        oSolicitud.Conyugue = dr["OPHCON"].ToString().Trim();
                        oSolicitud.Fecha_ingreso_DGAC = dr["OPHFEC"].ToString().Trim();
                        oSolicitud.años = dr["OPHANO"].ToString().Trim();
                        oSolicitud.meses = dr["OPHMES"].ToString().Trim();
                        oSolicitud.Cargo_actual = dr["OPHCAR"].ToString().Trim();
                        oSolicitud.Dependencia_actual = dr["OPHDEP"].ToString().Trim();
                        oSolicitud.Aeropuerto = dr["OPHAER"].ToString().Trim();
                        oSolicitud.Región = dr["OPHRE1"].ToString().Trim();
                        oSolicitud.Habilitación = dr["OPHHAB"].ToString().Trim();
                        oSolicitud.Fecha_Habilitación = dr["OPHFE1"].ToString().Trim();
                        oSolicitud.Doble_Habilitación = dr["OPHDOB"].ToString().Trim();
                        oSolicitud.Otra_Habilitación = dr["OPHOTR"].ToString().Trim();
                        oSolicitud.Caducidad_CertifMedi = dr["OPHCAD"].ToString().Trim();
                        oSolicitud.Status = dr["OPHSTA"].ToString().Trim();
                        oSolicitud.Competencia_Lingüíst = dr["OPHCOM"].ToString().Trim();
                        oSolicitud.Caducidad_Certificado_Competencia_Lingui = dr["OPHCA1"].ToString().Trim();
                        //LLENA DETALE DE dependencias
                        oSolicitud.oDetalleHistoricoDependenciaAtc = CD_DetalleHistoricoDependenciaAtc.Instancia.HistoricoDetalleDependenciaAtc(oSolicitud.Licencia_ATC);

                        //LLENA DETALE DE cursos
                        oSolicitud.oDetalleHistoricoCursoAtc = CD_DetalleHistoricoCursosAtc.Instancia.HistoricoDetalleCursosAtc(oSolicitud.Licencia_ATC);


                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

    }
}
