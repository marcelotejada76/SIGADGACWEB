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
    public class CD_LogUsuarioAircon
    {
        public static CD_LogUsuarioAircon _instancia = null;
        private CD_LogUsuarioAircon()
        {

        }

        public static CD_LogUsuarioAircon Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_LogUsuarioAircon();
                }
                return _instancia;
            }
        }



        public List<tbLogUsuarioAircon> ConsultaLogUsuarioAircon()
        {

            List<tbLogUsuarioAircon> listarSolicitud = new List<tbLogUsuarioAircon>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPRARC");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbLogUsuarioAircon oSolicitud = new tbLogUsuarioAircon();

                        oSolicitud.NombreArchivo = dr["OPRNOM"].ToString().Trim();
                        oSolicitud.Siglas = dr["OPRSIG"].ToString().Trim();
                        oSolicitud.NombreControlador = dr["OPRNO1"].ToString().Trim();
                        oSolicitud.FechaIngreso = dr["OPRFEC"].ToString().Trim();
                        oSolicitud.FechaSalida = dr["OPRFE1"].ToString().Trim();
                        oSolicitud.HoraIngreso = dr["OPRHOR"].ToString().Trim();
                        oSolicitud.HoraSalida = dr["OPRHO1"].ToString().Trim();

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

        public List<tbLogUsuarioAircon> LogUsuarioEstacion(string NombreEstacion)
        {

            List<tbLogUsuarioAircon> listarSolicitud = new List<tbLogUsuarioAircon>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPRARC  WHERE OPRNOM LIKE ('%" + NombreEstacion + "%')");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbLogUsuarioAircon oSolicitud = new tbLogUsuarioAircon();

                        oSolicitud.NombreArchivo = dr["OPRNOM"].ToString().Trim();
                        oSolicitud.Siglas = dr["OPRSIG"].ToString().Trim();
                        oSolicitud.NombreControlador = dr["OPRNO1"].ToString().Trim();
                        oSolicitud.FechaIngreso = dr["OPRFEC"].ToString().Trim();
                        oSolicitud.FechaSalida = dr["OPRFE1"].ToString().Trim();
                        oSolicitud.HoraIngreso = dr["OPRHOR"].ToString().Trim();
                        oSolicitud.HoraSalida = dr["OPRHO1"].ToString().Trim();


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

        public List<tbLogUsuarioAircon> LogUsuarioFecha(string Fecha)
        {

            List<tbLogUsuarioAircon> listarSolicitud = new List<tbLogUsuarioAircon>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPRARC  WHERE OPRFEC ='" + Fecha + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbLogUsuarioAircon oSolicitud = new tbLogUsuarioAircon();

                        oSolicitud.NombreArchivo = dr["OPRNOM"].ToString().Trim();
                        oSolicitud.Siglas = dr["OPRSIG"].ToString().Trim();
                        oSolicitud.NombreControlador = dr["OPRNO1"].ToString().Trim();
                        oSolicitud.FechaIngreso = dr["OPRFEC"].ToString().Trim();
                        oSolicitud.FechaSalida = dr["OPRFE1"].ToString().Trim();
                        oSolicitud.HoraIngreso = dr["OPRHOR"].ToString().Trim();
                        oSolicitud.HoraSalida = dr["OPRHO1"].ToString().Trim();


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

        public List<tbLogUsuarioAircon> LogUsuarioControlador(string NombreControlador)
        {

            List<tbLogUsuarioAircon> listarSolicitud = new List<tbLogUsuarioAircon>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPRARC  WHERE  OPRno1 LIKE ('%" + NombreControlador + "%')");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbLogUsuarioAircon oSolicitud = new tbLogUsuarioAircon();

                        oSolicitud.NombreArchivo = dr["OPRNOM"].ToString().Trim();
                        oSolicitud.Siglas = dr["OPRSIG"].ToString().Trim();
                        oSolicitud.NombreControlador = dr["OPRNO1"].ToString().Trim();
                        oSolicitud.FechaIngreso = dr["OPRFEC"].ToString().Trim();
                        oSolicitud.FechaSalida = dr["OPRFE1"].ToString().Trim();
                        oSolicitud.HoraIngreso = dr["OPRHOR"].ToString().Trim();
                        oSolicitud.HoraSalida = dr["OPRHO1"].ToString().Trim();


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
