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
    public class CD_ManifiestoPax
    {
        public static CD_ManifiestoPax _instancia = null;
        private CD_ManifiestoPax()
        {

        }

        public static CD_ManifiestoPax Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ManifiestoPax();
                }
                return _instancia;
            }
        }



        public List<tbManifiestoPax> ConsultaManifiesto()
        {
            string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbManifiestoPax> listarSolicitud = new List<tbManifiestoPax>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPVARC ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbManifiestoPax oSolicitud = new tbManifiestoPax();

                        oSolicitud.VUELO = dr["OPVOPM"].ToString();
                        oSolicitud.FECHAVLO = dr["OPVOP1"].ToString();
                        oSolicitud.OPERADOR = dr["OPVOP2"].ToString();
                        oSolicitud.PAXADULTOS = Convert.ToInt32(dr["OPVPAX"].ToString());
                        oSolicitud.PAXNIÑOS = Convert.ToInt32(dr["OPVPA1"].ToString());
                        oSolicitud.PAXINF = Convert.ToInt32(dr["OPVPA2"].ToString());
                        oSolicitud.TOTAL = Convert.ToInt32(dr["OPVTOT"].ToString());

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
        public List<tbManifiestoPax> ConsultaManifiestoPorFecha(string FECHA)
        {
           
            List<tbManifiestoPax> listarSolicitud = new List<tbManifiestoPax>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPVARC WHERE OPVOP1 = '" + FECHA + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbManifiestoPax oSolicitud = new tbManifiestoPax();

                        oSolicitud.VUELO = dr["OPVOPM"].ToString();
                        oSolicitud.FECHAVLO = dr["OPVOP1"].ToString();
                        oSolicitud.OPERADOR = dr["OPVOP2"].ToString();
                        oSolicitud.PAXADULTOS = Convert.ToInt32(dr["OPVPAX"].ToString());
                        oSolicitud.PAXNIÑOS = Convert.ToInt32(dr["OPVPA1"].ToString());
                        oSolicitud.PAXINF = Convert.ToInt32(dr["OPVPA2"].ToString());
                        oSolicitud.TOTAL = Convert.ToInt32(dr["OPVTOT"].ToString());

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


        //DETALLE DE PASAJEROS
        public List<tbDetallePasajerosManifiestoPax> DetallePaxvLO(string NumeroVlo, string FechaVlo)
        {

            List<tbDetallePasajerosManifiestoPax> listarSolicitud = new List<tbDetallePasajerosManifiestoPax>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPMAR4  WHERE OPMOPM = '" + NumeroVlo + "' AND OPMOP3= '" + FechaVlo + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetallePasajerosManifiestoPax oSolicitud = new tbDetallePasajerosManifiestoPax();

                        oSolicitud.PASAJERO = dr["OPMPAS"].ToString();
                        oSolicitud.RUTAINICIOVLO = dr["OPMRUT"].ToString();
                        string TipoPax = dr["OPMTI5"].ToString();
                        if (TipoPax=="C")
                        {
                            oSolicitud.TIPO = "NIÑO";
                        }
                        else
                        {
                            if (TipoPax == "I")
                            {
                                oSolicitud.TIPO = "INFANTE";
                            }
                            else
                            {
                                oSolicitud.TIPO = "ADULTO";
                            }
                        }
                        //oSolicitud.TIPO = dr["OPMTI5"].ToString();

                        oSolicitud.ORIGEN = dr["OPMOR2"].ToString();
                        oSolicitud.DESTINO = dr["OPMDE2"].ToString();
                        oSolicitud.CLASE = dr["OPMCLA"].ToString();
                        oSolicitud.TIPOB = dr["OPMTI6"].ToString();
                        oSolicitud.ASIENTO = dr["OPMASI"].ToString();
                        oSolicitud.SECUENCIA = oSolicitud.SECUENCIA++;


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

        public tbManifiestoPax DetallePax(string NumeroVlo, string FechaVlo)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbManifiestoPax listarSolicitud = new tbManifiestoPax();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPVARC WHERE OPVOPM='"+NumeroVlo+"' AND OPVOP1 = '" + FechaVlo + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbManifiestoPax oSolicitud = new tbManifiestoPax();

                        oSolicitud.VUELO = dr["OPVOPM"].ToString();
                        oSolicitud.FECHAVLO = dr["OPVOP1"].ToString();
                        oSolicitud.OPERADOR = dr["OPVOP2"].ToString();
                        oSolicitud.PAXADULTOS = Convert.ToInt32(dr["OPVPAX"].ToString());
                        oSolicitud.PAXNIÑOS = Convert.ToInt32(dr["OPVPA1"].ToString());
                        oSolicitud.PAXINF = Convert.ToInt32(dr["OPVPA2"].ToString());
                        oSolicitud.TOTAL = Convert.ToInt32(dr["OPVTOT"].ToString());



                        //LLENA DETALE DE pasajeros
                        oSolicitud.oDetallePasajeroManifiesto = DetallePaxvLO(oSolicitud.VUELO, oSolicitud.FECHAVLO);
                        

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

    }
}
