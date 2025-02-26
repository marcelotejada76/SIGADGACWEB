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
    public class CD_DocumentosAeropuertosNac
    {
        public static CD_DocumentosAeropuertosNac _instancia = null;
        private CD_DocumentosAeropuertosNac()
        {

        }

        public static CD_DocumentosAeropuertosNac Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DocumentosAeropuertosNac();
                }
                return _instancia;
            }
        }



        public List<tbDocumentosAtoInt> ConsultaDocumentosAeropuertosNac(string usuario, string ciudad)
        {

            List< tbDocumentosAtoInt  > listarSolicitud = new List<tbDocumentosAtoInt>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                if (ciudad == "SEQM" || ciudad == "SEGU")
                {
                    sbSol.Append("SELECT opnco7 AS CODIGO,OPNCO8 AS CODGRUPO,OPNCO9 AS CODNIVEL,OPNCO6 AS CODSUBNIVEL,OPNNOM AS GRUPO, OPNNO1 AS NIVEL," +
                    "OPNNO2 AS SUBNIVEL,OPNEX2 AS ESTADO, OPNC01 AS SECUENCIA FROM OPNAR3 INNER JOIN OPNAR1 ON OPNCO2 = OPNCO7 AND OPNCO1 = OPNCO8 " +
                    "INNER JOIN OPNAR2 ON OPNCO4 = OPNCO7 AND OPNCO5 = OPNCO8 AND OPNCO3 = OPNCO9 WHERE OPNCO7 = 5 ");
                }
                else
                {


                    sbSol.Append("SELECT opnco7 AS CODIGO,OPNCO8 AS CODGRUPO,OPNCO9 AS CODNIVEL,OPNCO6 AS CODSUBNIVEL,OPNNOM AS GRUPO, OPNNO1 AS NIVEL," +
                        "OPNNO2 AS SUBNIVEL,OPNEX2 AS ESTADO, OPNC01 AS SECUENCIA FROM OPNAR3 INNER JOIN OPNAR1 ON OPNCO2 = OPNCO7 AND OPNCO1 = OPNCO8 " +
                        "INNER JOIN OPNAR2 ON OPNCO4 = OPNCO7 AND OPNCO5 = OPNCO8 AND OPNCO3 = OPNCO9 WHERE OPNCO7 = 5  and OPNNOM= '" + ciudad + "' ");
                }
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();


                    while (dr.Read())
                    {
                        tbDocumentosAtoInt oSolicitud = new tbDocumentosAtoInt();

                        oSolicitud.NombreGrupo = dr["GRUPO"].ToString().Trim();
                        oSolicitud.NombreNivel = dr["NIVEL"].ToString().Trim();
                        oSolicitud.NombreSubnivel = dr["SUBNIVEL"].ToString().Trim();
                        if (oSolicitud.NombreNivel== oSolicitud.NombreSubnivel)
                        {
                            oSolicitud.Carpeta =  " / " + dr["NIVEL"].ToString().Trim();
                            //oSolicitud.Carpeta = dr["GRUPO"].ToString().Trim() + " / " + dr["NIVEL"].ToString().Trim();
                        }
                        else
                        {
                            oSolicitud.Carpeta =  " / " + dr["NIVEL"].ToString().Trim() + " /" + dr["SUBNIVEL"].ToString().Trim();
                            //oSolicitud.Carpeta = dr["GRUPO"].ToString().Trim() + " / " + dr["NIVEL"].ToString().Trim() + " /" + dr["SUBNIVEL"].ToString().Trim();
                        }
                        

                        oSolicitud.Estado = dr["ESTADO"].ToString().Trim();
                        oSolicitud.Codigo = Convert.ToInt16(dr["CODIGO"].ToString().Trim());
                        oSolicitud.Grupo = Convert.ToInt16(dr["CODGRUPO"].ToString().Trim());
                        oSolicitud.Nivel = Convert.ToInt16(dr["CODNIVEL"].ToString().Trim());
                        oSolicitud.Subnivel = Convert.ToInt16(dr["CODSUBNIVEL"].ToString().Trim());
                        oSolicitud.Secuencia = Convert.ToInt32(dr["SECUENCIA"].ToString().Trim());

                        if (oSolicitud.Estado == "N")
                        {
                            //LLENA DETALE DE ARCHIVOS A DESCARGAR
                            oSolicitud.oDetalleDocumentosDescargaAto = DetalleAtoDescarga(oSolicitud.Codigo, oSolicitud.Grupo, oSolicitud.Nivel, oSolicitud.Subnivel);

                        }
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


        public List<tbDocumentosAtoDescarga> DetalleAtoDescarga( int Codigo, int Grupo, int Nivel, int Subnivel)
        {

            List<tbDocumentosAtoDescarga> listarSolicitud = new List<tbDocumentosAtoDescarga>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM OPAAR1 where OPAES1='0' AND  OPACO2 = " + Codigo+ " and OPACO3="+Grupo+ " and OPACO4 = "+Nivel + " and OPACO5 =" +Subnivel +"");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbDocumentosAtoDescarga oSolicitud = new tbDocumentosAtoDescarga();
                        oSolicitud.GestionDocumental= Convert.ToInt16(dr["OPACO2"].ToString().Trim());
                        oSolicitud.GrupoGestion = Convert.ToInt16(dr["OPACO3"].ToString().Trim());
                        oSolicitud.NivelGestion = Convert.ToInt16(dr["OPACO4"].ToString().Trim());
                        oSolicitud.SubNivelGestion = Convert.ToInt16(dr["OPACO5"].ToString().Trim());
                        oSolicitud.NombreArchivo = dr["OPANO3"].ToString().Trim();
                        
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

        //public List<tbDocumentosDescarga> ConsultaDocumentosInstTecnicas()
        //{

        //    List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='Instrucciones Tecnicas'");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

        //                oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
        //                oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
        //                oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}


        //public List<tbDocumentosDescarga> ConsultaDocumentosProcedimientos()
        //{

        //    List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='Procedimientos Tecnicos'");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

        //                oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
        //                oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
        //                oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}
        //public List<tbDocumentosDescarga> ConsultaDocumentosCartasAcuerdo()
        //{

        //    List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='CartasdeAcuerdoOperacional'");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

        //                oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
        //                oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
        //                oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}
        //public List<tbDocumentosDescarga> ConsultaDocumentosManualesAtsp()
        //{

        //    List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='ManualesATSP'");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

        //                oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
        //                oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
        //                oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}
        //public List<tbDocumentosDescarga> ConsultaDocumentosFormulariosAtm()
        //{

        //    List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPDAR5 where OPDCAR='FormulariosAtm'");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

        //                oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
        //                oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
        //                oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());

        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}
        //public List<tbDocumentosDescarga> DocumentosPorNombre(string NOmbre)
        //{

        //    List<tbDocumentosDescarga> listarSolicitud = new List<tbDocumentosDescarga>();
        //    StringBuilder sbSol = new StringBuilder();
        //    string query = string.Empty;
        //    try
        //    {
        //        sbSol.Append("SELECT * FROM OPDAR5  WHERE OPDNOM LIKE  ('%" + NOmbre + "%')");

        //        query = sbSol.ToString();
        //        iDB2Command cmd;


        //        using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
        //        {
        //            cmd = new iDB2Command(query, oConexion);
        //            oConexion.Open();
        //            iDB2DataReader dr = cmd.ExecuteReader();



        //            while (dr.Read())
        //            {
        //                tbDocumentosDescarga oSolicitud = new tbDocumentosDescarga();

        //                oSolicitud.NombreArchivo = dr["OPDNOM"].ToString().Trim();
        //                oSolicitud.Estado = dr["OPDEST"].ToString().Trim();
        //                oSolicitud.Secuencia = Convert.ToInt32(dr["OPDSE4"].ToString().Trim());


        //                listarSolicitud.Add(oSolicitud);
        //            }

        //            dr.Close();
        //            oConexion.Close();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return listarSolicitud;
        //}


    }
}
