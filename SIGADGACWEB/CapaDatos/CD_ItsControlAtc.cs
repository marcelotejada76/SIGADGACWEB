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
    public class CD_ItsControlAtc
    {
        public static CD_ItsControlAtc _instancia = null;
        private CD_ItsControlAtc()
        {

        }

        public static CD_ItsControlAtc Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ItsControlAtc();
                }
                return _instancia;
            }
        }

        public List<tbItsControlAtc> DetalleDocumentos()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbItsControlAtc> listarSolicitud = new List<tbItsControlAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPIARC ORDER BY OPILUG");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbItsControlAtc oSolicitud = new tbItsControlAtc();
                        oSolicitud.LUGAR = dr["OPILUG"].ToString();
                        oSolicitud.DEPENDENCIA = dr["OPIDEP"].ToString();
                        oSolicitud.FECHAELABORACION = dr["OPIFEC"].ToString();
                        oSolicitud.TURNO = dr["OPITUR"].ToString();
                        oSolicitud.LICENCIARESPONSABLE = dr["OPILIC"].ToString();
                        oSolicitud.NOMBRERESPONSABLE = dr["OPINOM"].ToString();
                        oSolicitud.LICENCIARESPSALIEN = dr["OPILI1"].ToString();
                        oSolicitud.NOMBRERESSALIDA = dr["OPINO1"].ToString();
                        

                        oSolicitud.IFRDEP = Int16.Parse(dr["OPIIFR"].ToString());
                        oSolicitud.IFRARR = Int16.Parse(dr["OPIIF1"].ToString());
                        oSolicitud.VFRDEP = Int16.Parse(dr["OPIVFR"].ToString());
                        oSolicitud.VFRARR = Int16.Parse(dr["OPIVF1"].ToString());
                        oSolicitud.OVR = Int16.Parse(dr["OPIOVR"].ToString());
                        oSolicitud.TGL = Int16.Parse(dr["OPITGL"].ToString());
                        oSolicitud.SOBSEGU = Int16.Parse(dr["OPISOB"].ToString());
                        oSolicitud.TOTGENSEGU = Int16.Parse(dr["OPITOT"].ToString());

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



        public tbItsControlAtc DetalleDocumentosClave(string Lugar, string Dependencia, string Fechaelab, string Turno)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbItsControlAtc listarSolicitud = new tbItsControlAtc();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPIARC WHERE OPILUG = '"+Lugar+"' AND OPIDEP='"+Dependencia+"' AND OPIFEC ='"+Fechaelab+"' AND OPITUR='"+Turno+"' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbItsControlAtc oSolicitud = new tbItsControlAtc();
                        oSolicitud.LUGAR = dr["OPILUG"].ToString();
                        oSolicitud.DEPENDENCIA = dr["OPIDEP"].ToString();
                        oSolicitud.FECHAELABORACION = dr["OPIFEC"].ToString();
                        oSolicitud.TURNO = dr["OPITUR"].ToString();
                        oSolicitud.LICENCIARESPONSABLE = dr["OPILIC"].ToString();
                        oSolicitud.NOMBRERESPONSABLE = dr["OPINOM"].ToString();
                        oSolicitud.LICENCIARESPSALIEN = dr["OPILI1"].ToString();
                        oSolicitud.NOMBRERESSALIDA = dr["OPINO1"].ToString();


                        oSolicitud.IFRDEP = Int16.Parse(dr["OPIIFR"].ToString());
                        oSolicitud.IFRARR = Int16.Parse(dr["OPIIF1"].ToString());
                        oSolicitud.VFRDEP = Int16.Parse(dr["OPIVFR"].ToString());
                        oSolicitud.VFRARR = Int16.Parse(dr["OPIVF1"].ToString());
                        oSolicitud.OVR = Int16.Parse(dr["OPIOVR"].ToString());
                        oSolicitud.TGL = Int16.Parse(dr["OPITGL"].ToString());
                        oSolicitud.SOBSEGU = Int16.Parse(dr["OPISOB"].ToString());
                        oSolicitud.TOTGENSEGU = Int16.Parse(dr["OPITOT"].ToString());

                        //LLENA DETALE DE CONTROLADORES
                        oSolicitud.oDetalleControladorAtc = CD_DetalleControladoresAtc.Instancia.DetalleControladoresAtc(oSolicitud.LUGAR, oSolicitud.DEPENDENCIA,oSolicitud.FECHAELABORACION,oSolicitud.TURNO);
                        //LLENA DETALE DE NOTAMS
                        oSolicitud.oDetalleNotamsAtc = CD_DetalleNotamsAtc.Instancia.DetalleNotamsAtc(oSolicitud.LUGAR, oSolicitud.DEPENDENCIA, oSolicitud.FECHAELABORACION, oSolicitud.TURNO);
                        //LLENA DETALE DE EVENTOS
                        oSolicitud.oDetalleEventosAtc = CD_DetalleEventosAtc.Instancia.DetalleEventosAtc(oSolicitud.LUGAR, oSolicitud.DEPENDENCIA, oSolicitud.FECHAELABORACION, oSolicitud.TURNO);

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

        //por fecha de emision

        public List<tbItsControlAtc> DetalleDocumentosFecha(string Fecha)//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbItsControlAtc> listarSolicitud = new List<tbItsControlAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPIARC where  OPIFEC ='" + Fecha + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbItsControlAtc oSolicitud = new tbItsControlAtc();
                        oSolicitud.LUGAR = dr["OPILUG"].ToString();
                        oSolicitud.DEPENDENCIA = dr["OPIDEP"].ToString();
                        oSolicitud.FECHAELABORACION = dr["OPIFEC"].ToString();
                        oSolicitud.TURNO = dr["OPITUR"].ToString();
                        oSolicitud.LICENCIARESPONSABLE = dr["OPILIC"].ToString();
                        oSolicitud.NOMBRERESPONSABLE = dr["OPINOM"].ToString();
                        oSolicitud.LICENCIARESPSALIEN = dr["OPILI1"].ToString();
                        oSolicitud.NOMBRERESSALIDA = dr["OPINO1"].ToString();


                        oSolicitud.IFRDEP = Int16.Parse(dr["OPIIFR"].ToString());
                        oSolicitud.IFRARR = Int16.Parse(dr["OPIIF1"].ToString());
                        oSolicitud.VFRDEP = Int16.Parse(dr["OPIVFR"].ToString());
                        oSolicitud.VFRARR = Int16.Parse(dr["OPIVF1"].ToString());
                        oSolicitud.OVR = Int16.Parse(dr["OPIOVR"].ToString());
                        oSolicitud.TGL = Int16.Parse(dr["OPITGL"].ToString());
                        oSolicitud.SOBSEGU = Int16.Parse(dr["OPISOB"].ToString());
                        oSolicitud.TOTGENSEGU = Int16.Parse(dr["OPITOT"].ToString());

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
