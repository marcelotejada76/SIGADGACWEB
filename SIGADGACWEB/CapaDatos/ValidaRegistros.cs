using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class ValidaRegistros
    {
        public static bool Registro(string callsing, string matricula, string archivo, string FechaRegistro, string origen, string destino)
        {


            // AS400

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            string cadena = "";
            int count = 0;
            bool Registro = false;
            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {
                cadena = "select OIDAR8, OIDF06, OIDCAL, OIDMA1, OIDOR3, OIDDE2  FROM OIDAR3 WHERE  OIDAR8= '" + archivo + "' and oidcal= '" + callsing + "'  and OIDF06= '" + FechaRegistro +
                    "'  and OIDMA1= '" + matricula + "' and OIDOR3= '" + origen + "'  and OIDDE2= '" + destino + "' ";




                cm.CommandText = cadena;
                cm.CommandType = CommandType.Text;

                iDB2DataReader drDB2 = cm.ExecuteReader();
                while (drDB2.Read())
                {
                    count = count + 1;
                    Registro = true;

                }
                con.Close();
                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + cadena);

            }

            con.Close();


            return Registro;
        }

        //VALIDA MOVIMIENTOS
        public static List<CamposUceo> Movimientos(string fechaProcesoI, string fechaProcesoF)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            string query = "select OIDAR8, OIDF06, OIDCAL, OIDMA1, OIDOR3, OIDDE2 from OIDAR3 WHERE OIDOPE='SOBREVUELO' AND  OIDF06 >= '" + fechaProcesoI + "' AND  OIDF06 <= '" + fechaProcesoF + "'";


            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();
                    oUceo.Archivo = dr["OIDAR8"].ToString().Trim();
                    oUceo.fechaProceso = dr["OIDF06"].ToString().Trim();
                    oUceo.callsign = dr["OIDCAL"].ToString().Trim();
                    oUceo.registry = dr["OIDMA1"].ToString().Trim();
                    oUceo.ORIGEN = dr["OIDOR3"].ToString().Trim();
                    oUceo.DESTINO = dr["OIDDE2"].ToString().Trim();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }


        //carga movimientos
        //MOVIMIENTOS AEREOS formato p550
        public static List<CamposUceo> MovimientosS(string FechaInicial, string FechaFinal)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            string query = "select OIDOI7,	OIDOI8,	OIDOI9,	OIDO01,	OIDO02,	OIDFE6,	OIDHO1,	OIDOPE,	OIDUS6,	OIDFE7,OIDO03,OIDFAC,OIDFA1,OIDOR2,	OIDDE1," +
                "OIDO04,	OIDO05,	OIDTOT,	OIDO06,	OIDTIP,	OIDNUM,OIDO07,OIDRUT, OIDOB2, OIDAUT,OIDFA2 from OIDAR3 WHERE OIDOPE='SOBREVUELO' AND  OIDF06 >= '" + FechaInicial + "' and OIDF06 <= '" + FechaFinal + "'";


            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();
                    oUceo.OID = Convert.ToInt32(dr["OIDOI7"].ToString().Trim());

                    // oUceo.OIDITEMAUTORIZACIO = Convert.ToInt32(dr["OIDOI8"].ToString().Trim());
                    if (dr["OIDOI8"].ToString().Trim() == "")
                    {
                        oUceo.OIDITEMAUTORIZACIO = 0;
                    }
                    else
                    {
                        oUceo.OIDITEMAUTORIZACIO = Convert.ToInt32(dr["OIDOI8"].ToString().Trim());
                    }

                    //  oUceo.OIDTIPOVUELO = Convert.ToInt32(dr["OIDOI9"].ToString().Trim());
                    if (dr["OIDOI9"].ToString().Trim() == "")
                    {
                        oUceo.OIDTIPOVUELO = 0;
                    }
                    else
                    {
                        oUceo.OIDTIPOVUELO = Convert.ToInt32(dr["OIDOI9"].ToString().Trim());
                    }


                    //oUceo.OIDAEROVIA = Convert.ToInt32(dr["OIDO01"].ToString().Trim());
                    if (dr["OIDO01"].ToString().Trim() == "")
                    {
                        oUceo.OIDAEROVIA = 0;
                    }
                    else
                    {
                        oUceo.OIDAEROVIA = Convert.ToInt32(dr["OIDO01"].ToString().Trim());
                    }


                    //oUceo.OIDLUGARATERRIZAJE = Convert.ToInt32(dr["OIDO02"].ToString().Trim());
                    if (dr["OIDO02"].ToString().Trim() == "")
                    {
                        oUceo.OIDLUGARATERRIZAJE = 0;
                    }
                    else
                    {
                        oUceo.OIDLUGARATERRIZAJE = Convert.ToInt32(dr["OIDO02"].ToString().Trim());
                    }



                    oUceo.FECHAREAL = dr["OIDFE6"].ToString();
                    oUceo.HORAREAL = dr["OIDHO1"].ToString();
                    oUceo.OPERACION = dr["OIDOPE"].ToString();

                    oUceo.USUARIOCREA = dr["OIDUS6"].ToString();
                    oUceo.FECHACREA = dr["OIDFE7"].ToString().Trim();
                    //try
                    //{
                    //    DateTime dt = DateTime.ParseExact(oUceo.FECHACREA, "dd//MM/yyyy hh:mm:ss ", System.Globalization.CultureInfo.InvariantCulture);

                    //    //oUceo.FECHACREACION = Convert.ToDateTime(oUceo.FECHACREA.ToString("dd/MM/yyyy hh:MM:ss"));
                    //}
                    //catch (Exception ex)
                    //{

                    //    throw;
                    //}

                    //oUceo.OIDAERONAVECOMPANI = Convert.ToInt32(dr["OIDO03"].ToString().Trim());
                    if (dr["OIDO03"].ToString().Trim() == "")
                    {
                        oUceo.OIDAERONAVECOMPANI = 0;
                    }
                    else
                    {
                        oUceo.OIDAERONAVECOMPANI = Convert.ToInt32(dr["OIDO03"].ToString().Trim());
                    }
                    oUceo.FACTSER = dr["OIDFAC"].ToString();
                    oUceo.FACTEST = dr["OIDFA1"].ToString();
                    oUceo.ORIGEN = dr["OIDOR2"].ToString();
                    oUceo.DESTINO = dr["OIDDE1"].ToString();

                    //oUceo.OIDORIGEN = Convert.ToInt32(dr["OIDO04"].ToString().Trim());
                    if (dr["OIDO04"].ToString().Trim() == "")
                    {
                        oUceo.OIDORIGEN = 0;
                    }
                    else
                    {
                        oUceo.OIDORIGEN = Convert.ToInt32(dr["OIDO04"].ToString().Trim());
                    }
                    //oUceo.OIDDESTINO = Convert.ToInt32(dr["OIDO05"].ToString().Trim());
                    if (dr["OIDO05"].ToString().Trim() == "")
                    {
                        oUceo.OIDDESTINO = 0;
                    }
                    else
                    {
                        oUceo.OIDDESTINO = Convert.ToInt32(dr["OIDO05"].ToString().Trim());
                    }

                    if (dr["OIDTOT"].ToString().Trim() == "")
                    {
                        oUceo.TOTALMILLAS = 0;
                    }
                    else
                    {
                        oUceo.TOTALMILLAS = Convert.ToDecimal(dr["OIDTOT"].ToString().Trim());
                    }

                    //oUceo.OIDSOLICITANTE = Convert.ToInt32(dr["OIDO06"].ToString().Trim());
                    if (dr["OIDO06"].ToString().Trim() == "")
                    {
                        oUceo.OIDSOLICITANTE = 0;
                    }
                    else
                    {
                        oUceo.OIDSOLICITANTE = Convert.ToInt32(dr["OIDO06"].ToString().Trim());
                    }

                    oUceo.TIPOAUTORIZACION = dr["OIDTIP"].ToString();
                    oUceo.NUMEROVUELO = dr["OIDNUM"].ToString();

                    if (dr["OIDO07"].ToString().Trim() == "")
                    {
                        oUceo.OIDTIPOOPERACION = 0;
                    }
                    else
                    {
                        oUceo.OIDTIPOOPERACION = Convert.ToInt32(dr["OIDO07"].ToString().Trim());
                    }
                    oUceo.RUTAEROVIA = dr["OIDRUT"].ToString();

                    oUceo.OBSERVACION = dr["OIDOB2"].ToString();

                    oUceo.AUTORIZACION = dr["OIDAUT"].ToString();
                    oUceo.FACTURAR = dr["OIDFA2"].ToString();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            finally
            {
                // 🔹 Cierre manual por seguridad adicional (aunque using ya lo hace)
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }

        //valida datos correctos opmensajes
        //VALIDA MOVIMIENTOS peso
        public static List<CamposUceo> TablaMensajesPeso(string fechaProcesoI, string fechaProcesoF)// (string fechaProceso)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            //VALIDAPESO

            string query = "SELECT OPMFEC , OPMCAL , OPMRE2 ,OPMORI, OPMDES FROM OPMAR1 WHERE OPMPE1 = 0  AND " +
                "OPMORI<>'ZZZZ' AND OPMDES<>'ZZZZ' AND OPMSOB = 'S' AND OPMFAC='S' AND OPMFLI NOT IN ('M') AND OPMFEC >= " + "'" + fechaProcesoI + "'" +
                "AND OPMFEC <= " + "'" + fechaProcesoF + "'" + " ";

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();

                    oUceo.fechaProceso = dr["OPMFEC"].ToString().Trim();
                    oUceo.callsign = dr["OPMCAL"].ToString().Trim();
                    oUceo.registry = dr["OPMRE2"].ToString().Trim();
                    oUceo.ORIGEN = dr["OPMORI"].ToString().Trim();
                    oUceo.DESTINO = dr["OPMDES"].ToString().Trim();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }
        //valida datos correctos opmensajes
        //VALIDA MOVIMIENTOS distancia
        public static List<CamposUceo> TablaMensajesDistancia(string fechaProcesoI, string fechaProcesoF)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            //VALIDAPESO


            string query = "SELECT OPMFEC , OPMCAL , OPMRE2 , OPMORI,OPMDES FROM OPMAR1 WHERE OPMDI1 = 0  AND OPMSOB = 'S' AND OPMFLI NOT IN ('M') AND OPMORI <>'ZZZZ' AND OPMDES <>'ZZZZ' " +
                   "  AND OPMFAC='S'  AND OPMFEC  >=" + "'" + fechaProcesoI + "'" + " AND OPMFEC <= " + "'" + fechaProcesoF + "'" + " ";

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();

                    oUceo.fechaProceso = dr["OPMFEC"].ToString().Trim();
                    oUceo.callsign = dr["OPMCAL"].ToString().Trim();
                    oUceo.registry = dr["OPMRE2"].ToString().Trim();
                    oUceo.ORIGEN = dr["OPMORI"].ToString().Trim();
                    oUceo.DESTINO = dr["OPMDES"].ToString().Trim();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }

        //valida datos correctos opmensajes
        //VALIDA MOVIMIENTOS distancia valores altos
        public static List<CamposUceo> TablaMensajesDistanciaAlto(string fechaProcesoI, string fechaProcesoF)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            //VALIDAPESO

            string query = "SELECT OPMFEC , OPMCAL , OPMRE2 ,OPMORI, OPMDES FROM OPMAR1 " +
                   "WHERE OPMDI1 >= 435  AND OPMSOB = 'S' AND OPMFLI NOT IN ('M') AND OPMORI<>'ZZZZ' AND OPMDES<>'ZZZZ'  AND OPMFEC  >=" + "'" + fechaProcesoI + "'" +
                   " AND OPMFEC <= " + "'" + fechaProcesoF + "'" + " ";


            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();

                    oUceo.fechaProceso = dr["OPMFEC"].ToString().Trim();
                    oUceo.callsign = dr["OPMCAL"].ToString().Trim();
                    oUceo.registry = dr["OPMRE2"].ToString().Trim();
                    oUceo.ORIGEN = dr["OPMORI"].ToString().Trim();
                    oUceo.DESTINO = dr["OPMDES"].ToString().Trim();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }

        //valida datos correctos opmensajes
        //VALIDA MOVIMIENTOS distancia valores inferiores
        public static List<CamposUceo> TablaMensajesDistanciaMinimo(string fechaProcesoI, string fechaProcesoF)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            //VALIDAPESO


            string query = "SELECT OPMFEC , OPMCAL , OPMRE2 ,OPMORI,OPMDES FROM OPMAR1 " +
                   "WHERE OPMDI1 <= 120  AND OPMFAC='S' AND OPMSOB= 'S' AND OPMFLI NOT IN ('M') AND OPMORI<>'ZZZZ' AND OPMDES<>'ZZZZ'  AND OPMFEC  >=" + "'" + fechaProcesoI + "'" +
                   " AND OPMFEC <= " + "'" + fechaProcesoF + "'" + " ";

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();

                    oUceo.fechaProceso = dr["OPMFEC"].ToString().Trim();
                    oUceo.callsign = dr["OPMCAL"].ToString().Trim();
                    oUceo.registry = dr["OPMRE2"].ToString().Trim();
                    oUceo.ORIGEN = dr["OPMORI"].ToString().Trim();
                    oUceo.DESTINO = dr["OPMDES"].ToString().Trim();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }

        //registros sin validar
        public static List<CamposUceo> TablaMensajesRegistrosSinValidar(string fechaProcesoI, string fechaProcesoF)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            //VALIDAPESO


            string query = "SELECT OPMFEC , OPMCAL , OPMRE2 ,OPMORI,OPMDES FROM OPMAR1 " +
                   "WHERE OPMFAC =' '  AND OPMSOB = 'S' AND OPMFLI NOT IN ('M') AND OPMORI<>'ZZZZ' AND OPMDES<>'ZZZZ'  AND OPMFEC  >=" + "'" + fechaProcesoI + "'" +
                   " AND OPMFEC <= " + "'" + fechaProcesoF + "'" + " ";

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    CamposUceo oUceo = new CamposUceo();

                    oUceo.fechaProceso = dr["OPMFEC"].ToString().Trim();
                    oUceo.callsign = dr["OPMCAL"].ToString().Trim();
                    oUceo.registry = dr["OPMRE2"].ToString().Trim();
                    oUceo.ORIGEN = dr["OPMORI"].ToString().Trim();
                    oUceo.DESTINO = dr["OPMDES"].ToString().Trim();


                    lstUceo.Add(oUceo);
                }
                return lstUceo;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.Close();
            con.Close();
            return lstUceo;
            //con.Close();

        }
        //cuenta numero de registros de la tabla mensajes
        public static int TablaMensajesNumregistros(string fechaProceso, int NumReg)
        //public static List<CamposUceo> TablaMensajesNumregistros(string fechaProceso)
        {
            List<CamposUceo> lstUceo = new List<CamposUceo>();
            //VALIDAPESO


            string query = "SELECT COUNT(OPMFEC) as CANTIDAD FROM DGACDATPRO.OPMAR1 " +
                   "WHERE OPMSOB = 'S' AND OPMORI<>'ZZZZ' AND OPMDES<>'ZZZZ'  and OPMFLI <>'M' AND OPMFEC  =" + "'" + fechaProceso + "'" + " ";

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    //CamposUceo oUceo = new CamposUceo();
                    //oUceo.NUMREG = Convert.ToInt16( dr["CANTIDAD"].ToString().Trim());
                    NumReg = Convert.ToInt16(dr["CANTIDAD"].ToString().Trim());

                    // lstUceo.Add(oUceo);
                }
                return NumReg;

                //  return FechaMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            return NumReg;
            //dr.Close();
            con.Close();
            // return lstUceo;
            //con.Close();

        }
    }
}
