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
    public class CD_DetalleCertAeronav
    {
        public static CD_DetalleCertAeronav _instancia = null;
        private CD_DetalleCertAeronav()
        {

        }

        public static CD_DetalleCertAeronav Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleCertAeronav();
                }
                return _instancia;
            }
        }

     
        public List<tbAeronavesCertAeronav> DetalleDocumentosCertificadoAero(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbAeronavesCertAeronav> listarSolicitud = new List<tbAeronavesCertAeronav>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA2 AS MATRICULA,OPCF23 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN2) AS NOMBREINSPECTORREGISTRA," +
                    " OPCF33 AS FECHARENOVACION, OPCF34 AS FECHACADUCIDAD, (SELECT OPINO2 FROM OPIAR2 WHERE OPICED = OPCI03) AS NOMBREINSPECTORRENUEVA, OPCES9 AS ESTADO" +
                    " FROM OPCA34 WHERE OPCMA2 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesCertAeronav oSolicitud = new tbAeronavesCertAeronav();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();
                        
                        oSolicitud.NOMBREINSPECTOR = dr["NOMBREINSPECTORREGISTRA"].ToString();
                        oSolicitud.FECHARENOVACON = dr["FECHARENOVACION"].ToString();
                        oSolicitud.FECHACADUCIDAD = dr["FECHACADUCIDAD"].ToString();
                        
                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORRENUEVA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO=="AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }
                        

                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO LICENCIA DE RADIO
        public List<tbDetalleCertLicRadio> DetalleDocumentosCertificadoRadio(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbDetalleCertLicRadio> listarSolicitud = new List<tbDetalleCertLicRadio>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA3 AS MATRICULA,OPCF24 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN3) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCF25 AS FECHARENOVACION, (SELECT OPINO2 FROM OPIAR2 WHERE OPICED = OPCIN4) AS NOMBREINSPECTORRENUEVA, OPCE01 AS ESTADO "  +
                    " FROM OPCA35 WHERE OPCMA3 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCertLicRadio oSolicitud = new tbDetalleCertLicRadio();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();
                        oSolicitud.FECHARENOVACION = dr["FECHARENOVACION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();
                        oSolicitud.NOMBREINSPECTORAIRRENOV = dr["NOMBREINSPECTORRENUEVA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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
        //CERTIFICADO LICENCIA DE RUIDO
        public List<tbDetalleCertHomolRuido> DetalleDocumentosCertificadoRuido(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbDetalleCertHomolRuido> listarSolicitud = new List<tbDetalleCertHomolRuido>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA4 AS MATRICULA,OPCF26 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN5) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE02 AS ESTADO" +
                    " FROM OPCA36 WHERE OPCMA4 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCertHomolRuido oSolicitud = new tbDetalleCertHomolRuido();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO PBN/RNP10
        public List<tbDetalleCerrtAprobaPbnRnp10> DetalleDocumentosCertificadoRNP10(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            List<tbDetalleCerrtAprobaPbnRnp10> listarSolicitud = new List<tbDetalleCerrtAprobaPbnRnp10>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA5 AS MATRICULA,OPCF27 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN6) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE03 AS ESTADO" +
                    " FROM OPCA37 WHERE OPCMA5 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtAprobaPbnRnp10 oSolicitud = new tbDetalleCerrtAprobaPbnRnp10();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO RNAV5
        public List<tbDetalleCerrtAprobRnav5> DetalleDocumentosCertificadoRNAV5(string Matricula)
        {
            
            List<tbDetalleCerrtAprobRnav5> listarSolicitud = new List<tbDetalleCerrtAprobRnav5>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA6 AS MATRICULA,OPCF28 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN7) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE04 AS ESTADO" +
                    " FROM OPCA38 WHERE OPCMA6 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtAprobRnav5 oSolicitud = new tbDetalleCerrtAprobRnav5();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO RNAV2
        public List<tbDetalleCerrtAprobRnav2> DetalleDocumentosCertificadoRNAV2(string Matricula)
        {

            List<tbDetalleCerrtAprobRnav2> listarSolicitud = new List<tbDetalleCerrtAprobRnav2>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA7 AS MATRICULA,OPCF29 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN8) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE05 AS ESTADO" +
                    " FROM OPCA39 WHERE OPCMA7 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtAprobRnav2 oSolicitud = new tbDetalleCerrtAprobRnav2();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO APPROACH
        public List<tbDetalleCerrtAprobRnavArAproach> DetalleDocumentosCertificadoAPPROACH(string Matricula)
        {

            List<tbDetalleCerrtAprobRnavArAproach> listarSolicitud = new List<tbDetalleCerrtAprobRnavArAproach>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA8 AS MATRICULA,OPCF30 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCIN9) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE06 AS ESTADO" +
                    " FROM OPCA40 WHERE OPCMA8 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtAprobRnavArAproach oSolicitud = new tbDetalleCerrtAprobRnavArAproach();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO RVSM
        public List<tbDetalleCerrtAprobRvsm> DetalleDocumentosCertificadoRVSM(string Matricula)
        {

            List<tbDetalleCerrtAprobRvsm> listarSolicitud = new List<tbDetalleCerrtAprobRvsm>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCMA9 AS MATRICULA,OPCF31 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCI01) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE07 AS ESTADO" +
                    " FROM OPCA41 WHERE OPCMA9 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtAprobRvsm oSolicitud = new tbDetalleCerrtAprobRvsm();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO ETOPS
        public List<tbDetalleCerrtAprobEtops> DetalleDocumentosCertificadoETOPS(string Matricula)
        {

            List<tbDetalleCerrtAprobEtops> listarSolicitud = new List<tbDetalleCerrtAprobEtops>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCM01 AS MATRICULA,OPCF32 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCI02) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE08 AS ESTADO" +
                    " FROM OPCA42 WHERE OPCM01 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtAprobEtops oSolicitud = new tbDetalleCerrtAprobEtops();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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

        //CERTIFICADO CATEGORIAS
        public List<tbDetalleCerrtCat2y3> DetalleDocumentosCategorias(string Matricula)
        {

            List<tbDetalleCerrtCat2y3> listarSolicitud = new List<tbDetalleCerrtCat2y3>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPCM02 AS MATRICULA,OPCF35 AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPCI04) AS NOMBREINSPECTORREGISTRA," +
                    "  OPCE09 AS ESTADO" +
                    " FROM OPCA43 WHERE OPCM02 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleCerrtCat2y3 oSolicitud = new tbDetalleCerrtCat2y3();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();

                        string ESTADO = dr["ESTADO"].ToString();
                        if (ESTADO == "AC")
                        {
                            oSolicitud.ESTADO = "ACTIVO";
                        }
                        else
                        {
                            oSolicitud.ESTADO = "NO ACTIVO";
                        }


                        listarSolicitud.Add(oSolicitud);
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


        //DETALLE ACCIDENTE
        public List<tbDetalleAccidenteAeronave> DetalleDocumentosAccidente(string Matricula)
        {

            List<tbDetalleAccidenteAeronave> listarSolicitud = new List<tbDetalleAccidenteAeronave>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT OPAMAT AS MATRICULA,OPAFEC AS FECHAEMISION,(SELECT OPINO2 FROM OPIAR2 WHERE OPICED=OPAINS) AS NOMBREINSPECTORREGISTRA," +
                    "  OPALUG AS LUGAR,(SELECT OPPPRO FROM OPPAR2 WHERE OPPCO3='SE' AND OPPCO2=OPAPRO) AS PROVINCIA" +
                    " FROM OPAAR2 WHERE OPAMAT = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDetalleAccidenteAeronave oSolicitud = new tbDetalleAccidenteAeronave();

                        oSolicitud.FECHAEMISION = dr["FECHAEMISION"].ToString();

                        oSolicitud.NOMBREINSPECTORAIR = dr["NOMBREINSPECTORREGISTRA"].ToString();
                        oSolicitud.LUGAR = dr["LUGAR"].ToString();
                        oSolicitud.PROVINCIA = dr["PROVINCIA"].ToString();


                        listarSolicitud.Add(oSolicitud);
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
