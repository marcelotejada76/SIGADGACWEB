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
    public class CD_Sorteo
    {
        public static CD_Sorteo _instancia = null;
        private CD_Sorteo()
        {

        }

        public static CD_Sorteo Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Sorteo();
                }
                return _instancia;
            }
        }


       
        public List<tbSorteo> ConsultaSorteo(string Tipo)
        {
            List<tbSorteo> listarSolicitud = new List<tbSorteo>();
            //tbSorteo listarSolicitud = new tbSorteo();
           // List<tbSorteo> listarSolicitud = new List<tbSorteo>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FIPCE1 AS  CEDULARUC,FIPNO1 AS  NOMBRE,FIPTE2 AS TELEFONO,FIPCE2  AS CELULAR,FIPCO5 AS  CORREO,FIPARE AS    AREAPROFESION," +
                "FIPESP AS  ESPECIALIDAD, FIPFE5 AS FECHAINSCRIPCION,FIPFE6 AS  FECHACADUCIDAD,FIPINS AS INSTITUCION,FIPTIP AS TIPO FROM DGACDATPRO.FIPAR2  WHERE FIPES5='' and FIPTIP ='"+Tipo+"' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbSorteo oSolicitud = new tbSorteo();
                        
                        oSolicitud.CEDULARUC = dr["CEDULARUC"].ToString();
                        oSolicitud.NOMBRE = dr["NOMBRE"].ToString();
                        oSolicitud.TELEFONO = dr["TELEFONO"].ToString();
                        oSolicitud.CELULAR = dr["CELULAR"].ToString();
                        oSolicitud.CORREO = dr["CORREO"].ToString();
                        oSolicitud.AREAPROFESION = dr["AREAPROFESION"].ToString();
                        oSolicitud.ESPECIALIDAD = dr["ESPECIALIDAD"].ToString();
                        oSolicitud.FECHAINSCRIPCION = dr["FECHAINSCRIPCION"].ToString();
                        oSolicitud.FECHACADUCIDAD = dr["FECHACADUCIDAD"].ToString();

                        oSolicitud.INSTITUCION = dr["INSTITUCION"].ToString();
                        oSolicitud.TIPOSORTEO = dr["TIPO"].ToString();
                        if (Tipo=="01")
                        {
                            oSolicitud.DESCRIPCION = "SORTEO PERITOS AERONAUTICOS";
                        }
                        if (Tipo == "02")
                        {
                            oSolicitud.DESCRIPCION = "CASA VALORES";
                        }
                        if (Tipo == "03")
                        {
                            oSolicitud.DESCRIPCION = "PERITOS CONTABLES";
                        }
                        //listarSolicitud = oSolicitud;
                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                    oConexion.Close();
                    return listarSolicitud;
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
