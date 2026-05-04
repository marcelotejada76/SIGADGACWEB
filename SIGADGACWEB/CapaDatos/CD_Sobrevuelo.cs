using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Sobrevuelo
    {
        public static CD_Sobrevuelo _instancia = null;
        private CD_Sobrevuelo()
        {

        }

        public static CD_Sobrevuelo Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Sobrevuelo();
                }
                return _instancia;
            }
        }

        public List<tbSobrevuelo> ListadoSobrevuelos()
        {
            List<tbSobrevuelo> listarSolicitud = new List<tbSobrevuelo>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            DateTime fecha = System.DateTime.Now;
            string fechc = fecha.ToString("yyyyMMdd"); //fecha del sistema

            string fechaProceso = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper();

            try
            {
                sbSol.Append("SELECT opmfec,opmcal,opmre2,opmori,opmdes,opmini,opmfix,opmdis,ori.opilat AS ORIGENLATITUD, ori.opilon AS ORIGENLONGITUD, " +
                    "des.opilat AS DESTINOLATITUD, des.opilon AS DESTINOLONGITUD, OPERA.CIANOM AS OPERADOR, FACTURA.CIANOM AS FACTURA,OPMFAC AS FACTURABLE," +
                    "concat(trim(AERFAB),concat('/',trim(AERMOD))) as modelo " +
                    "FROM OPMAR1  LEFT JOIN CIAARC  AS OPERA ON  OPMAER = OPERA.CIACOD " +
                    "LEFT JOIN CIAARC  AS FACTURA ON  OPMAER = FACTURA.CIACOD " +
                    "left join opiar1 as ori on opmori = ori.opiica " +
                    "left join opiar1 as des on opmdes = des.opiica " +
                    "left join aerar101 on opmre2=aerma1 " +
                    "WHERE opmfec = '" + fechaProceso + "' AND OPMSOB = 'S' ORDER BY OPMCAL ");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSobrevuelo oSolicitud = new tbSobrevuelo();
                        oSolicitud.FechaVuelo = dr["opmfec"].ToString();
                        oSolicitud.CAllSingn = dr["OPMCAL"].ToString();


                        oSolicitud.Matricula = dr["OPMRE2"].ToString();
                        oSolicitud.Origen = dr["OPMORI"].ToString();
                        oSolicitud.Destino = dr["OPMDES"].ToString();
                        oSolicitud.InitialTime = dr["OPMINI"].ToString();
                        oSolicitud.Ruta = dr["OPMFIX"].ToString();

                        oSolicitud.Distancia = Convert.ToInt16(dr["OPMDIS"].ToString());

                        oSolicitud.CiaFactura = dr["FACTURA"].ToString();
                        oSolicitud.Operador = dr["OPERADOR"].ToString();
                        oSolicitud.MOdelo = dr["MODELO"].ToString();
                        oSolicitud.Facturable = dr["FACTURABLE"].ToString();

                        if (dr["ORIGENLATITUD"].ToString() != "")
                        {
                            oSolicitud.LatitudOrigen = Convert.ToDouble(dr["ORIGENLATITUD"].ToString());
                        }

                        if (dr["ORIGENLONGITUD"].ToString() != "")
                        {
                            oSolicitud.LongitudOrigen = Convert.ToDouble(dr["ORIGENLONGITUD"].ToString());
                        }

                        if (dr["DESTINOLATITUD"].ToString() != "")
                        {
                            oSolicitud.LatitudDestino = Convert.ToDouble(dr["DESTINOLATITUD"].ToString());
                        }

                        if (dr["DESTINOLONGITUD"].ToString() != "")
                        {
                            oSolicitud.LongitudDestino = Convert.ToDouble(dr["DESTINOLONGITUD"].ToString());
                        }

                        //oSolicitud.LatitudOrigen = -12.0219;
                        //oSolicitud.LongitudOrigen = -77.1143;
                        //oSolicitud.LatitudDestino = 25.7959;
                        //oSolicitud.LongitudDestino = -80.2870;

                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return listarSolicitud;
        }

        //porfecha

        public List<tbSobrevuelo> ListadoSobrevuelosFecha(string Fecha)
        {
            List<tbSobrevuelo> listarSolicitud = new List<tbSobrevuelo>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT opmfec,opmcal,opmre2,opmori,opmdes,opmini,opmfix,opmdis,ori.opilat AS ORIGENLATITUD, ori.opilon AS ORIGENLONGITUD, " +
                    "des.opilat AS DESTINOLATITUD, des.opilon AS DESTINOLONGITUD, OPERA.CIANOM AS OPERADOR, FACTURA.CIANOM AS FACTURA,OPMFAC AS FACTURABLE," +
                    "concat(trim(AERFAB),concat('/',trim(AERMOD))) as modelo " +
                    "FROM OPMAR1  LEFT JOIN CIAARC  AS OPERA ON  OPMAER = OPERA.CIACOD " +
                    "LEFT JOIN CIAARC  AS FACTURA ON  OPMAER = FACTURA.CIACOD " +
                    "left join opiar1 as ori on opmori = ori.opiica " +
                    "left join opiar1 as des on opmdes = des.opiica  " +
                    "left join aerar101 on opmre2 = aerma1 " +
                    " WHERE opmfec = '" + Fecha + "' AND OPMSOB = 'S' ");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSobrevuelo oSolicitud = new tbSobrevuelo();
                        oSolicitud.FechaVuelo = dr["opmfec"].ToString();
                        oSolicitud.CAllSingn = dr["OPMCAL"].ToString();


                        oSolicitud.Matricula = dr["OPMRE2"].ToString();
                        oSolicitud.Origen = dr["OPMORI"].ToString();
                        oSolicitud.Destino = dr["OPMDES"].ToString();
                        oSolicitud.InitialTime = dr["OPMINI"].ToString();
                        oSolicitud.Ruta = dr["OPMFIX"].ToString();

                        oSolicitud.Distancia = Convert.ToInt16(dr["OPMDIS"].ToString());
                        oSolicitud.CiaFactura = dr["FACTURA"].ToString();
                        oSolicitud.Operador = dr["OPERADOR"].ToString();
                        oSolicitud.Facturable = dr["FACTURABLE"].ToString();

                        if (dr["ORIGENLATITUD"].ToString() != "")
                        {
                            oSolicitud.LatitudOrigen = Convert.ToDouble(dr["ORIGENLATITUD"].ToString());
                        }

                        if (dr["ORIGENLONGITUD"].ToString() != "")
                        {
                            oSolicitud.LongitudOrigen = Convert.ToDouble(dr["ORIGENLONGITUD"].ToString());
                        }

                        if (dr["DESTINOLATITUD"].ToString() != "")
                        {
                            oSolicitud.LatitudDestino = Convert.ToDouble(dr["DESTINOLATITUD"].ToString());
                        }

                        if (dr["DESTINOLONGITUD"].ToString() != "")
                        {
                            oSolicitud.LongitudDestino = Convert.ToDouble(dr["DESTINOLONGITUD"].ToString());
                        }




                        //oSolicitud.LatitudOrigen = -12.0219;
                        //oSolicitud.LongitudOrigen = -77.1143;
                        //oSolicitud.LatitudDestino = 25.7959;
                        //oSolicitud.LongitudDestino = -80.2870;

                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        //detalle por vuelo
        public tbSobrevuelo DetalleSobrevuelo(string FechaVuelo, string CAllSingn, string Matricula)
        {
            tbSobrevuelo listarSolicitud = new tbSobrevuelo();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT opmfec,opmcal,opmre2,opmori,opmdes,opmini,opmdis,ori.opilat AS ORIGENLATITUD, ori.opilon AS ORIGENLONGITUD, " +
                    "des.opilat AS DESTINOLATITUD, des.opilon AS DESTINOLONGITUD, OPERA.CIANOM AS OPERADOR, FACTURA.CIANOM AS FACTURA,OPMFAC AS FACTURABLE, " +
                    "OPMFEC AS FECHAREAL,OPMHO1 AS HORAREAL,OPMAUT AS AUTORIZACION,OPMNO2 AS NOMBREORIGEN,OPMNO3 AS NOMBREDESTINO,OPMNUM AS NUMEROVLO," +
                    "OPMAER AS OACIOPERA,OPMCIA AS OACIFACTURA,OPMFIX AS RUTA,OPMAE1 AS AEROVIA," +
                    "concat(trim(AERFAB),concat('/',trim(AERMOD))) as modelo, opmpe1 as PESO,  " +
                    "(SELECT  SERDES FROM DGACDATPRO.SERARC WHERE SERCOD = SOLCO1) AS TIPO_SERVICIO ,SOLNUM as NUMEROSOLICITUD,SOLFE1 AS FECHASOLICITUD," +
                    "SOLNO2 AS CIAOPERADOR, SOLDI1 AS DIRECCIONOPERADOR, SOLTE1 AS TELEFONOOPERADOR, SOLCO2 AS CORREOOPERADOR," +
                    "SOLNO3 AS CIAFACTURA, SOLDI2 AS DIRECCIONFACTURA, SOLTE2 AS TELEFONOFACTURA, SOLCO3 AS COORREOFACTURA," +
                    "SOLNOM AS NOMBREAPLICANTE, SOLDIR AS DIRECCIONAPLICANTE, SOLTEL AS TELEFONOAPLICANTE, SOLCOR AS CORREOAPLICANTE," +
                    "SOLAUT AS AUTORIZACION, SOLES2 AS ESTADOAUTORIZACION" +
                    "  FROM OPMAR1  LEFT JOIN CIAARC  AS OPERA ON  OPMAER = OPERA.CIACOD " +
                    "LEFT JOIN CIAARC  AS FACTURA ON  OPMAER = FACTURA.CIACOD " +
                    "left join opiar1 as ori on opmori = ori.opiica " +
                    "left join opiar1 as des on opmdes = des.opiica  " +
                    "left join aerar101 on opmre2 = aerma1  LEFT JOIN SOLARC ON OPMAUT=SOLAUT " +
                    " WHERE opmfec = '" + FechaVuelo + "' AND OPMCAL='" + CAllSingn + "' AND OPMRE2='" + Matricula + "'  AND OPMSOB = 'S'  ");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    tbSobrevuelo oSolicitud = new tbSobrevuelo();

                    oSolicitud.FechaVuelo = dr["opmfec"].ToString();
                    oSolicitud.CAllSingn = dr["OPMCAL"].ToString();

                    oSolicitud.Matricula = dr["OPMRE2"].ToString();
                    oSolicitud.Origen = dr["OPMORI"].ToString();
                    oSolicitud.Destino = dr["OPMDES"].ToString();
                    oSolicitud.InitialTime = dr["OPMINI"].ToString();
                    oSolicitud.Ruta = dr["RUTA"].ToString();
                    oSolicitud.MOdelo = dr["modelo"].ToString();

                    oSolicitud.Distancia = Convert.ToInt16(dr["OPMDIS"].ToString());
                    oSolicitud.CiaFactura = dr["FACTURA"].ToString();
                    oSolicitud.Operador = dr["OPERADOR"].ToString();
                    oSolicitud.Facturable = dr["FACTURABLE"].ToString();

                    oSolicitud.NombreOrigen = dr["NOMBREORIGEN"].ToString();
                    oSolicitud.NombreDestino = dr["NOMBREDESTINO"].ToString();
                    oSolicitud.Autorizacion = dr["AUTORIZACION"].ToString();

                    oSolicitud.NumeroVlo = dr["NUMEROVLO"].ToString();
                    oSolicitud.FechaReal = dr["FECHAREAL"].ToString();
                    oSolicitud.HoraReal = dr["HORAREAL"].ToString();
                    oSolicitud.OaciVuela = dr["OACIOPERA"].ToString();
                    oSolicitud.OaciFactura = dr["OACIFACTURA"].ToString();
                    oSolicitud.Aerovia = dr["AEROVIA"].ToString();
                    oSolicitud.Peso = dr["PESO"].ToString();

                    //DATOPS DE LA AUTORIZACION
                    oSolicitud.TIPO_SERVICIO = dr["TIPO_SERVICIO"].ToString().Trim();
                    oSolicitud.NUMEROSOLICITUD = dr["NUMEROSOLICITUD"].ToString().Trim();
                    oSolicitud.FECHASOLICITUD = dr["FECHASOLICITUD"].ToString().Trim();
                    oSolicitud.CIAOPERADOR = dr["CIAOPERADOR"].ToString().Trim();
                    oSolicitud.DIRECCIONOPERADOR = dr["DIRECCIONOPERADOR"].ToString().Trim();
                    oSolicitud.TELEFONOOPERADOR = dr["TELEFONOOPERADOR"].ToString().Trim();
                    oSolicitud.CORREOOPERADOR = dr["CORREOOPERADOR"].ToString().Trim();
                    oSolicitud.CIAFACTURA = dr["CIAFACTURA"].ToString().Trim();
                    oSolicitud.DIRECCIONFACTURA = dr["DIRECCIONFACTURA"].ToString().Trim();
                    oSolicitud.TELEFONOFACTURA = dr["TELEFONOFACTURA"].ToString().Trim();
                    oSolicitud.COORREOFACTURA = dr["COORREOFACTURA"].ToString().Trim();
                    oSolicitud.NOMBREAPLICANTE = dr["NOMBREAPLICANTE"].ToString().Trim();
                    oSolicitud.DIRECCIONAPLICANTE = dr["DIRECCIONAPLICANTE"].ToString().Trim();
                    oSolicitud.TELEFONOAPLICANTE = dr["TELEFONOAPLICANTE"].ToString().Trim();
                    oSolicitud.CORREOAPLICANTE = dr["CORREOAPLICANTE"].ToString().Trim();
                    oSolicitud.AUTORIZACION = dr["AUTORIZACION"].ToString().Trim();
                    oSolicitud.ESTADOAUTORIZACION = dr["ESTADOAUTORIZACION"].ToString().Trim();



                    if (dr["ORIGENLATITUD"].ToString() != "")
                    {
                        oSolicitud.LatitudOrigen = Convert.ToDouble(dr["ORIGENLATITUD"].ToString());
                    }

                    if (dr["ORIGENLONGITUD"].ToString() != "")
                    {
                        oSolicitud.LongitudOrigen = Convert.ToDouble(dr["ORIGENLONGITUD"].ToString());
                    }

                    if (dr["DESTINOLATITUD"].ToString() != "")
                    {
                        oSolicitud.LatitudDestino = Convert.ToDouble(dr["DESTINOLATITUD"].ToString());
                    }

                    if (dr["DESTINOLONGITUD"].ToString() != "")
                    {
                        oSolicitud.LongitudDestino = Convert.ToDouble(dr["DESTINOLONGITUD"].ToString());
                    }

                    listarSolicitud= oSolicitud;

                    dr.Close();
                    oConexion.Close();
                    return listarSolicitud;
                }

            }
            catch (Exception ex)
            {
               // throw ex;
            }

            return listarSolicitud;
            
        }

    }
}
