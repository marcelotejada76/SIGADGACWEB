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
    public class CD_ControlOperacionalCns
    {
        public static CD_ControlOperacionalCns _instancia = null;
        private CD_ControlOperacionalCns()
        {

        }

        public static CD_ControlOperacionalCns Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ControlOperacionalCns();
                }
                return _instancia;
            }
        }

        public List<tbControlOperacionalCns> DetalleDocumentos()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbControlOperacionalCns> listarSolicitud = new List<tbControlOperacionalCns>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCA03 LEFT JOIN OPTAR4 ON OPTCED=OPCTEC ORDER BY OPCF08 DESC");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbControlOperacionalCns oSolicitud = new tbControlOperacionalCns();
                        oSolicitud.NOMBRETECNICO = dr["OPTAPE"].ToString().Trim() + " " + dr["OPTNO2"].ToString().Trim();
                        string IMPRESO = dr["OPCIM4"].ToString();
                        if (IMPRESO == "I")
                        {
                            oSolicitud.IMPRESO = "IMPRESO";
                        }
                        oSolicitud.FECHACONTROL = dr["OPCF08"].ToString();
                        oSolicitud.TURNO = dr["OPCTU1"].ToString().Trim();
                        
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
        public List<tbControlOperacionalCns> DetalleDocumentosEventos()//(string canio, string cdireccion, string tipoSolicitud)
        {
            List<tbControlOperacionalCns> listarSolicitud = new List<tbControlOperacionalCns>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM OPEAR3 LEFT JOIN USUARC ON OPEUS4=USUCOD AND USUCOD <>''ORDER BY OPEFE1 DESC");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbControlOperacionalCns oSolicitud = new tbControlOperacionalCns();

                        oSolicitud.CODIGO = dr["OPECO1"].ToString().Trim();
                        oSolicitud.TURNO = dr["OPETU1"].ToString().Trim();
                        string IMPRESO = dr["OPEIMP"].ToString();
                        if (IMPRESO == "I")
                        {
                            oSolicitud.IMPRESO = "IMPRESO";
                        }
                        oSolicitud.FECHACONTROL = dr["OPEFE1"].ToString();
                        oSolicitud.DESCRIPCION = dr["OPEDE2"].ToString() + dr["OPEDE3"].ToString() + dr["OPEDE4"].ToString() + dr["OPEDE5"].ToString() + dr["OPEDE6"].ToString();

                        oSolicitud.ELABORADO = dr["USUNOM"].ToString().Trim() + " " + dr["USUAPE"].ToString().Trim();

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

        //por fecha de emision

        public List<tbControlOperacionalCns> DetalleDocumentosFecha(string Fecha)
        {
            List<tbControlOperacionalCns> listarSolicitud = new List<tbControlOperacionalCns>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPCA03 LEFT JOIN OPTAR4 ON OPTCED=OPCTEC WHERE OPCF08 ='"+Fecha+"'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbControlOperacionalCns oSolicitud = new tbControlOperacionalCns();

                        oSolicitud.NOMBRETECNICO = dr["OPTAPE"].ToString().Trim() + " " + dr["OPTNO2"].ToString().Trim();
                        string IMPRESO = dr["OPCIM4"].ToString();
                        if (IMPRESO == "I")
                        {
                            oSolicitud.IMPRESO = "IMPRESO";
                        }
                        oSolicitud.FECHACONTROL = dr["OPCF08"].ToString();
                        oSolicitud.TURNO = dr["OPCTU1"].ToString().Trim();


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


        //por fecha eventos
        public List<tbControlOperacionalCns> DetalleDocumentosFechaEventos(string Fecha)
        {
            List<tbControlOperacionalCns> listarSolicitud = new List<tbControlOperacionalCns>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM OPEAR3 LEFT JOIN USUARC ON OPEUS4=USUCOD AND USUCOD <>'' WHERE OPEFE1='" + Fecha + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbControlOperacionalCns oSolicitud = new tbControlOperacionalCns();
                        oSolicitud.TURNO = dr["OPETU1"].ToString().Trim();
                        oSolicitud.CODIGO = dr["OPECO1"].ToString().Trim();
                        string IMPRESO = dr["OPEIMP"].ToString();
                        if (IMPRESO == "I")
                        {
                            oSolicitud.IMPRESO = "IMPRESO";
                        }
                        oSolicitud.FECHACONTROL = dr["OPEFE1"].ToString();
                        oSolicitud.DESCRIPCION = dr["OPEDE2"].ToString() + dr["OPEDE3"].ToString() + dr["OPEDE4"].ToString() + dr["OPEDE5"].ToString() + dr["OPEDE6"].ToString();

                        oSolicitud.ELABORADO = dr["USUNOM"].ToString().Trim() + " " + dr["USUAPE"].ToString().Trim();

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

        public tbControlOperacionalCns DetalleDocumentosClave(string Fechaelab, string Codigo, string Turno)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbControlOperacionalCns listarSolicitud = new tbControlOperacionalCns();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM OPEAR3 LEFT JOIN USUARC ON OPEUS4=USUCOD AND USUCOD <>'' WHERE OPEFE1='" + Fechaelab + "' " +
                    "AND OPECO1='" + Codigo + "' AND OPETU1='"+Turno+"' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbControlOperacionalCns oSolicitud = new tbControlOperacionalCns();

                        oSolicitud.TURNO = dr["OPETU1"].ToString().Trim();
                        oSolicitud.CODIGO = dr["OPECO1"].ToString().Trim();
                        string IMPRESO = dr["OPEIMP"].ToString();
                        if (IMPRESO == "I")
                        {
                            oSolicitud.IMPRESO = "IMPRESO";
                        }
                        oSolicitud.FECHACONTROL = dr["OPEFE1"].ToString();
                        oSolicitud.DESCRIPCION = dr["OPEDE2"].ToString() + dr["OPEDE3"].ToString() + dr["OPEDE4"].ToString() + dr["OPEDE5"].ToString() + dr["OPEDE6"].ToString();

                        oSolicitud.ELABORADO = dr["USUNOM"].ToString().Trim() + " " + dr["USUAPE"].ToString().Trim();

                        //LLENA DETALE DE EVENTOS
                        oSolicitud.oDetalleEventosCns = DetalleEventosCna(oSolicitud.FECHACONTROL, oSolicitud.CODIGO,oSolicitud.TURNO);
                        

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

//DETALLE EVENTOS
        public List<tbDetalleControlOperacionalCns> DetalleEventosCna(string Fechaelab, string Codigo,string Turno)
        {
            List<tbDetalleControlOperacionalCns> listarSolicitud = new List<tbDetalleControlOperacionalCns>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT *FROM OPEAR4 where OPEFE2='"+Fechaelab+"' and opeco2='"+Codigo+"' AND OPETU2='"+Turno+"' ");


                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    decimal Valor = 0;
                    while (dr.Read())
                    {
                        // , ,,,,
                        tbDetalleControlOperacionalCns oSolicitud = new tbDetalleControlOperacionalCns();

                        oSolicitud.HORA = dr["OPEHO7"].ToString();
                        oSolicitud.EQUIPO = dr["OPEEQU"].ToString();
                        oSolicitud.DESCRIPCIONEVENTO = dr["OPEDE7"].ToString() + dr["OPEDE8"].ToString() + dr["OPEDE9"].ToString() + dr["OPED01"].ToString() + dr["OPED02"].ToString();

                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }
    }
}
