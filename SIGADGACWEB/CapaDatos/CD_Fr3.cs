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
    public class CD_Fr3
    {
        public static CD_Fr3 _instancia = null;
        private CD_Fr3()
        {

        }

        public static CD_Fr3 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Fr3();
                }
                return _instancia;
            }
        }

        public tbFr3 DetalleFr3(string Ato, string Matricula, string Origen, string Destino, string Destino1, string Destino2, string FechaI, string FechaF, string HoraI, string HoraF)
        {
            tbFr3 listarSolicitud = new tbFr3();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
           
            try
            {
               

                iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
                con.Open();

                iDB2Command cm = new iDB2Command();
                cm.Connection = con;

                string cadena = "PA_CALCULOFR3";


                //String Aeropuerto = "SEMT";
                //String FechaInicial= "20231011";
                //String FechaFinal = "20231011";
                //String HoraInicial = "12:00:00";
                //String HoraFinal = "13:00:00";
                //String Matricula = "AC080";
                //String Origen = "UGUP";
                //String Destino = "SEQM";
                //String Destino1 = "";
                //String Destino2 = "";
                decimal v1 =1;
                decimal v2 = 1;
                decimal v3 = 1;
                decimal v4 = 1;
                decimal v5 = 1;
                decimal v6 = 1;
                decimal v7 = 1;
                string Perio="";

                cm.CommandText = cadena;
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@PR_ATO", Ato);
                cm.Parameters.AddWithValue("@PR_MATRICULA", Matricula);
                cm.Parameters.AddWithValue("@PR_ORIGEN", Origen);
                cm.Parameters.AddWithValue("@PR_DESTINO", Destino);
                cm.Parameters.AddWithValue("@PR_DESTINO1", Destino1);
                cm.Parameters.AddWithValue("@PR_DESTINO2", Destino2);
                cm.Parameters.AddWithValue("@PR_FECHAINICIO", FechaI);
                cm.Parameters.AddWithValue("@PR_FECHAFIN", FechaF);
                cm.Parameters.AddWithValue("@PR_HORAINICIO", HoraI);
                cm.Parameters.AddWithValue("@PR_HORAFIN", HoraF);
                cm.Parameters.AddWithValue("@PR_VALEST", v1).Direction=ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALATE", v2).Direction = ParameterDirection.Output; 
                cm.Parameters.AddWithValue("@PR_VALILU", v3).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_VALCHA", v4).Direction = ParameterDirection.Output; 
                cm.Parameters.AddWithValue("@PR_VALPRO", v5).Direction = ParameterDirection.Output; 
                cm.Parameters.AddWithValue("@PR_VALANT", v6).Direction = ParameterDirection.Output; 
                cm.Parameters.AddWithValue("@PR_TOTAL", v7).Direction = ParameterDirection.Output;
                cm.Parameters.AddWithValue("@PR_PERIO", Perio).Direction = ParameterDirection.Output;
                //string valor = cm.Parameters[16].iDB2Value.ToString();
                cm.CommandTimeout = 0;
               // cm.ExecuteNonQuery();

                iDB2DataReader dr = cm.ExecuteReader();

                string VALEST = cm.Parameters[10].iDB2Value.ToString();
                string VALATE = cm.Parameters[11].iDB2Value.ToString();
                string VALILU = cm.Parameters[12].iDB2Value.ToString();
                string VALCHA = cm.Parameters[13].iDB2Value.ToString();
                string VALPRO = cm.Parameters[14].iDB2Value.ToString();
                string VALANT = cm.Parameters[15].iDB2Value.ToString();
                string TOTAL = cm.Parameters[16].iDB2Value.ToString();
                string NPERIODO = cm.Parameters[17].iDB2Value.ToString();
                tbFr3 oSolicitud = new tbFr3();
                oSolicitud.PERIODO = NPERIODO;
                oSolicitud.AEROPUERTO = Ato;
                oSolicitud.MATRICULA = Matricula;
                oSolicitud.ORIGEN = Origen;
                oSolicitud.DESTINO = Destino;
                oSolicitud.DESTINO1 = Destino1;
                oSolicitud.DESTINO2 = Destino2;
                oSolicitud.FECHAINGRESOPLATAFORMA = FechaI;
                oSolicitud.FECHASALIDAPLATAFORMA = FechaF;
                oSolicitud.HORAINGRESOPLATAFORMA = HoraI;
                oSolicitud.HORASALIDAPLATAFORMA = HoraF;

                if ( VALEST !="0")
                {
                    oSolicitud.DERECHOESTACIONAMIENTO = Convert.ToDecimal(VALEST.ToString());
                }
                if (VALATE != "0")
                {
                    oSolicitud.DERECHOATERRIZAJEDIURNO = Convert.ToDecimal(VALATE.ToString());
                }
                if (VALILU != "0")
                {
                    oSolicitud.DERECHOILUMINACION = Convert.ToDecimal(VALILU.ToString());
                }
                if (VALCHA != "0")
                {
                    oSolicitud.CHARTER = Convert.ToDecimal(VALCHA.ToString());
                }
                if (VALPRO != "0")
                {
                    oSolicitud.DERECHOPROTVLO = Convert.ToDecimal(VALPRO.ToString());
                }
                if (VALANT != "0")
                {
                    oSolicitud.DERECHOPROTANTESYDESPUES = Convert.ToDecimal(VALANT.ToString());
                }
                if (TOTAL != "0")
                {
                    oSolicitud.TOTAL = Convert.ToDecimal(TOTAL.ToString());
                }
                listarSolicitud = oSolicitud;
                //while (dr.Read())
                //    {
                       
                //        //oSolicitud.DERECHOESTACIONAMIENTO = Convert.ToDecimal( dr["OID"].ToString());
                //        //oSolicitud.RUC = dr["RUC"].ToString();
                //        //oSolicitud.COMPAÑIA = dr["COMPANIA"].ToString();
                        
                //        //oSolicitud.MATRICULA = dr["MATRICULAO"].ToString();
                //        //oSolicitud.MARCA = dr["MARCA"].ToString();
                //        //oSolicitud.MODELO = dr["MODELO"].ToString();
                //        //oSolicitud.ESTADO = dr["ESTADO"].ToString();
                //        //oSolicitud.USO = dr["USO"].ToString();

                //        //oSolicitud.FECHACREA = dr["FECHACREA"].ToString();
                //        //oSolicitud.USUARIOCREA = dr["USUARIOCREA"].ToString();
                //        //oSolicitud.PESOMAXESTRUCTURAL = decimal.Parse(dr["PESOMAXESTRUCTURAL"].ToString());
                        
                //      //  listarSolicitud.Add(oSolicitud);
                //    }
                   // dr.Close();
                    con.Close();
               

            }
            catch (Exception ex)
            {
             //   throw ex;
            }
            return listarSolicitud;
        }

    }
}
