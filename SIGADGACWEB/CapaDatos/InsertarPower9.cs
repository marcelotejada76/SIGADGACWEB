using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class InsertarPower9
    {

        //AERONAVE
        public static void insertar_transaccionAeronave(List<DatosGlobales> listaMensaje)
        {


            string cadena = "";
            string error = "";
            // AS400

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            con.Open();
            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            // StreamWriter archivo = new StreamWriter("c://Temporal//ArchivosP550//Aeronave.txt", true);
            // archivo.WriteLine("----------------------------------------------------------------------------------------------------------");
            //archivo.WriteLine("Fecha: " + DateTime.Now);
            string tabla = "AERAR3";
            string registro = ValidaDuplicados.EliminaTemporal(tabla);
            foreach (DatosGlobales mensaje in listaMensaje)
            {
                try
                {
                    //valida si ya esta registrado el registro
                    //bool registro = ValidaDuplicados.RegistroInsertadoAeronave(mensaje.CODIGOOACI, mensaje.MATRICULA);

                    //if (registro == false)
                    //{

                    //cadena = "INSERT INTO AERAR1(AERCO1,AERMAT,AERPES,AERRE2,AERAPR,AERNUM,AERDES,AERHO4,AEREST,AERHO5,AERHO6,AERHO7,AERCIC,AERANI,AERCI1,AERPE1,AERESP,AERPE2,AERCO4," +
                    //    "AERNU1,AERNU2,AERPE3,AERAUT,AERCA1,AERNU3,AERNU4,AERCAP,AERGLI,AERPIL, AERVHF, AERHFF, AERADF, AERVOR,AERBCN, AERRAD, AERDME, AERESE,AERELT,AERBOT, AEREMA ,AEREXT ,AEREOX" +
                    //    ",AERSEN,AERFE4,AERFE5,AERULT,AERLUG,AERGAL,AEREXC,AERFE6,AERFE7,AERBAS,AERSIS,AERTRA,AERBRU,AERPI1,AERTIE,AERRE3,AERNU5,AERFE8,AERCA2,AERFE9,AERTIP," +
                    //    "AERES1,AERLIM, AERF01, AERREM, AERNAC, AERUL1, AERDE1, AERINS, AERARR, AERF02, AEROBS, AERGRA,AERAVA, AERUS2, AERFE2,AERUS3, AERFE3,AERMOD,AERFAB,AERMA1,AEROI1,AEROI2  )";

                    cadena = "INSERT INTO AERAR3(AERCO2,AERMA2,AERPE4,AERRE6,AERAP1,AERNU6,AERDE2,AERH01,AERES2,AERH02,AERH03,AERH04,AERCI2,AERAN1,AERCI3,AERPE5,AERES3,AERPE6,AERCO7,AERNU7,AERNU8,AERPE7," +
                        "AERAU1,AERCA3,AERNU9,AERN01,AERCA4,AERGL1,AERPI2,AERVH1,AERHF1,AERAD1,AERVO1,AERBC1,AERRA1,AERDM1,AERES4,AEREL1,AERBO1,AEREM1,AEREX1,AEREO1,AERSE1,AERF06,AERF07," +
                        "AERUL2,AERLU1,AERGA1,AEREX2,AERF08,AERF09,AERBA1,AERSI1,AERTR1,AERBR1,AERPI3,AERTI2,AERRE7,AERN02,AERF10,AERCA5,AERF11,AERTI3,AERES5,AERLI1,AERF12,AERRE8,AERNA1," +
                        "AERUL3,AERDE3,AERIN1,AERAR4,AERF13,AEROB1,AERGR1,AERAV1,AERUS6,AERF14,AERUS7,AERF15,AERMO1,AERFA1,AERMA3,AEROI3,AEROI4)";


                    cadena += "values('" + mensaje.CODIGOOACI.Trim() + "','"
                            + mensaje.MATRICULA.Trim()
                            + "'," + mensaje.PESOMAXESTRUCTURAL.ToString().Replace(",", ".")
                            + "," + mensaje.REGION.ToString().Replace(",", ".") +
                            "," + mensaje.APROBADO.ToString().Replace(",", ".")
                            + ",'" + mensaje.NUMEROSERIE.Trim().Replace("'", "")
                            + "'," + mensaje.DESTINADOAUSO.ToString().Replace(",", ".")
                            + "," + mensaje.HORASTOTALAT.ToString().Replace(",", ".")
                            + "," + mensaje.ESTADO.ToString().Replace(",", ".")
                            + "," + mensaje.HORASTOTALAR.ToString().Replace(",", ".")
                            + "," + mensaje.HORASTOTALR.ToString().Replace(",", ".")
                            + "," + mensaje.HORASTOTALT.ToString().Replace(",", ".")
                            + "," + mensaje.CICLOSTOTAL.ToString().Replace(",", ".")
                            + ",'" + mensaje.ANIOFABRICACION.Trim()
                            + "'," + mensaje.CICLOSTOTALA.ToString().Replace(",", ".")
                            + "," + mensaje.PESOMAXATERRIZAJE.ToString().Replace(",", ".")
                            + ",'" + mensaje.ESPECIFICACION.Trim()
                            + "'," + mensaje.PESOMAXESTRUCTURAL.ToString().Replace(",", ".")
                            + ",'" + mensaje.CONDVUELO.Trim()
                            + "'," + mensaje.NUMPASAJEROS.ToString().Replace(",", ".")
                            + "," + mensaje.NUMPERSEXTRAS.ToString().Replace(",", ".")
                            + "," + mensaje.PESOVACIOKG.ToString().Replace(",", ".")
                            + ",'" + mensaje.AUTONOMIA.Trim()
                            + "'," + mensaje.CATEGORIA.ToString().Replace(",", ".")
                            + "," + mensaje.NUMTRIPULANTES.ToString().Replace(",", ".")
                            + "," + mensaje.NIBRAS.ToString().Replace(",", ".")
                            + "," + mensaje.CAPGALCOMBUSTIBLE.ToString().Replace(",", ".")
                            + ",'" + mensaje.GLIDESLOPE
                            + "','" + mensaje.PILOTOAUTOMATICO
                            + "','" + mensaje.VHF
                            + "','" + mensaje.HF
                            + "','" + mensaje.ADF
                            + "','" + mensaje.VOR
                            + "','" + mensaje.BCN
                            + "','" + mensaje.RADAR
                            + "','" + mensaje.DME
                            + "','" + mensaje.ESELVA
                            + "','" + mensaje.ELT
                            + "','" + mensaje.BOTIQUIN
                            + "','" + mensaje.EMAR
                            + "','" + mensaje.EXTINGUIDOR
                            + "','" + mensaje.EOXIGENO
                            + "','" + mensaje.SENIALES
                            + "','" + mensaje.FECHAINGPAIS.Trim()
                            + "','" + mensaje.FECHAULTINSPECCION.Trim()
                            + "','" + mensaje.ULTIMOPAIS.Trim()
                            + "','" + mensaje.LUGARINSPECCION.Trim()
                            + "'," + mensaje.GALONES_HORA.ToString().Replace(",", ".")
                            + ",'" + mensaje.EXCEPCIONES.Trim()
                            + "','" + mensaje.FECHAULTPB.Trim()
                            + "','" + mensaje.FECHAFINPB.Trim()
                            + "','" + mensaje.BASEOPERACION.Trim()
                            + "','" + mensaje.SISALTIMETRICO.Trim()
                            + "','" + mensaje.TRANSPONDER.Trim()
                            + "','" + mensaje.BRUJULA.Trim()
                            + "','" + mensaje.PITOT.Trim()
                            + "'," + mensaje.TIEMPOTOTAL.ToString().Replace(",", ".")
                            + "," + mensaje.REG.ToString().Replace(",", ".")
                            + ",'" + mensaje.NUMEROREGISTRO.Trim()
                            + "','" + mensaje.FECHAREGISTRO.Trim()
                            + "','" + mensaje.CATEGORIAAERONAVE.Trim()
                            + "','" + mensaje.FECHAEXPEDICION.Trim()
                            + "','" + mensaje.TIPOMATRICULA
                            + "','" + mensaje.ESTADOMATRICULA
                            + "'," + mensaje.LIMITANTEOPERA.ToString().Replace(",", ".")
                            + ",'" + mensaje.FECHACANCELACION.Trim()
                            + "'," + mensaje.REMRELOJ.ToString().Replace(",", ".")
                            + ",'" + mensaje.NACIONALIDAD_MATRICULA
                            + "','" + mensaje.ULTPROPIETARIO.Trim()
                            + "','" + mensaje.DESCANCELACION.Trim()
                            + "','" + mensaje.INSCRIPMATRIEXT.Trim()
                            + "','" + mensaje.ARRENDADO
                            + "','" + mensaje.FECHAVALIDO.Trim()
                            + "','" + mensaje.OBSERVACION.Trim()
                            + "','" + mensaje.GRAVAMEN.Trim()
                            + "'," + mensaje.AVALUO.ToString().Replace(",", ".")
                            + ",'" + mensaje.USUARIOCREA.Trim()
                            + "','" + mensaje.FECHACREA.Trim()
                            + "','" + mensaje.USUARIOMODIFICA.Trim()
                            + "','" + mensaje.FECHAMODIFICA.Trim()
                            + "','" + mensaje.MODELO.Trim().Replace("'", "")
                            + "','" + mensaje.Nombre.Trim().Replace("'", "")
                            + "','" + mensaje.MATRICULAINDRA.Trim().Replace("'", "")
                            + "'," + mensaje.OIDCIA
                            + "," + mensaje.OIDAERONAVE
                            + ")";


                    cm.CommandText = cadena;
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    Console.WriteLine("registro insertado.:" + mensaje.MATRICULA);
                    //   archivo.WriteLine("sql Procesado:  " + cadena);



                    // }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("registro con error.:" + cadena);
                    // archivo.WriteLine("sql error:  " + cadena);
                }
            }
            con.Close();
            //archivo.Close();
            //archivo.Dispose();
        }

        //CLEINTE FACTURA
        public static void insertar_ClienteFactura(List<DatosGlobales> listaMensaje)
        {


            string cadena = "";
            string error = "";
            // AS400

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            con.Open();
            iDB2Command cm = new iDB2Command();
            cm.Connection = con;
            // StreamWriter archivo = new StreamWriter("c://Temporal//ArchivosP550//ClienteFactura.txt", true);
            //** archivo.WriteLine("----------------------------------------------------------------------------------------------------------");
            // archivo.WriteLine("Fecha: " + DateTime.Now);
            string tabla = "OIDA10";
            //string registro = ValidaDuplicados.EliminaTemporal(tabla);
            int sec = 1;
            foreach (DatosGlobales mensaje in listaMensaje)
            {
                try
                {


                    cadena = "INSERT INTO OIDA10(OIDCL1,OIDN02,OIDDI5,OIDTE4,OIDTA1,OIDNI1)";
                    cadena += "values('" + mensaje.CEDULA_RUC.Trim() +
                               "','" + mensaje.NOMBRECLIENTE.Trim() +
                               "','" + mensaje.DIRECCION.Trim() +
                               "','" + mensaje.TELEFONO.Trim() +
                               "','" + mensaje.TABLA +
                               "','" + mensaje.NIVEL +
                                 "')";


                    cm.CommandText = cadena;
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    Console.WriteLine("registro insertado.:" + mensaje.NOMBRECLIENTE);
                    sec++;
                    //archivo.WriteLine("sql Procesado:  " + cadena);
                    //  }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("registro con error.:" + cadena);
                    //archivo.WriteLine("sql error:  " + cadena);
                }
            }
            con.Close();
            //  archivo.Close();
            //  archivo.Dispose();
        }

        //COMPANIA
        public static void insertar_Compania(List<DatosGlobales> listaMensaje)
        {


            string cadena = "";
            string error = "";
            // AS400

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            con.Open();
            iDB2Command cm = new iDB2Command();
            cm.Connection = con;
            
            string tabla = "OIDAR5";
            string registro = ValidaDuplicados.EliminaTemporal(tabla);

            foreach (DatosGlobales mensaje in listaMensaje)
            {
                try
                {
                    //valida si ya esta registrado el registro
                    //bool registro = ValidaDuplicados.RegistroInsertadoCompania(mensaje.OID);

                    //if (registro == false)
                    //{

                    cadena = "INSERT INTO OIDAR5( OIDO11,  OIDO12,  OIDO13,  OIDSIG,  OIDCO7,  OIDNO4,  OIDDIR,  OIDREP,  OIDDI1,  OIDTEL,  OIDO14,   OIDRE6, OIDTE1, OIDFAX, " +
                        "OIDEMA, OIDCUP, OIDCAR, OIDLIS, OIDVIG, OIDCA1, OIDDIA, OIDRE7, OIDO15, OIDCUE, OIDCA2, OIDCOP, OIDNU1, OIDIVA, OIDRET, OIDRE8, OIDNAC,OIDTI1,OIDTI2,OIDU01," +
                        "OIDF02,OIDU02,OIDF03,OIDO16,OIDPR1,OIDO17,OIDEM1,OIDFA3,OIDDI2,OIDTE2,OIDFA4,OIDO18,OIDRUC,OIDAFT,OIDNO5,OIDOB3,OIDCO8)";
                    cadena += "values(" + mensaje.OID +
                               "," + mensaje.OIDPAIS +
                               "," + mensaje.OIDCIUDAD +
                               ",'" + mensaje.SIGLAS_IATA.Trim().Replace("'", "") +
                               "','" + mensaje.CODIGO_OACI.Trim().Replace("'", "") +
                               "','" + mensaje.NOMBRE.Trim().Replace("'", "") +
                               "','" + mensaje.DIRECCION.Trim().Replace("'", "") +
                               "','" + mensaje.REPRESENTANTELEGAL.Trim().Replace("'", "") +
                               "','" + mensaje.DIRECCREPRELEGAL.Trim().Replace("'", "") +
                               "','" + mensaje.TELEFREPRELEGAL.Trim().Replace("'", "") +
                               "'," + mensaje.OIDCIUDADREPLEGAL +
                               ",'" + mensaje.REPRESENTANTETECNI.Trim().Replace("'", "") +
                               "','" + mensaje.TELEFONO.Trim().Replace("'", "") +
                               "','" + mensaje.FAX.Trim().Replace("'", "") +
                               "','" + mensaje.EMAIL.Trim().Replace("'", "") +
                               "'," + mensaje.CUPO.ToString().Replace(",", ".") +
                               ",'" + mensaje.CARGOREPRESENLEGAL.Trim().Replace("'", "") +
                               "'," + mensaje.LISTAPRECIOS.ToString().Replace(",", ".") +
                               ",'" + mensaje.VIGENCIACOA.Trim().Replace("'", "") +
                               "','" + mensaje.CARGOREPRESENTECNI.Trim().Replace("'", "") +
                               "'," + mensaje.DIASCREDITO.ToString().Replace(",", ".") +
                               ",'" + mensaje.REPRESENTANTEPERMI.Trim().Replace("'", "") +
                               "'," + mensaje.OIDCTACONT +
                               ",'" + mensaje.CUENTACONTABLE.Trim().Replace("'", "") +
                               "','" + mensaje.CARGOREPRESENPERMI.Trim().Replace("'", "") +
                               "'," + mensaje.COPIAS.ToString().Replace(",", ".") +
                               "," + mensaje.NUMEROPAGOS.ToString().Replace(",", ".") +
                               "," + mensaje.IVA.ToString().Replace(",", ".") +
                               "," + mensaje.RETENCIONIVA.ToString().Replace(",", ".") +
                               "," + mensaje.RETENCIONFUENTE.ToString().Replace(",", ".") +
                               ",'" + mensaje.NACIONAL_EXTRANJER.Trim().Replace("'", "") +
                               "','" + mensaje.TIPOOBJETO.Trim().Replace("'", "") +
                               "','" + mensaje.TIPOCOMPANIA.Trim().Replace("'", "") +
                               "','" + mensaje.USUARIOCREA.Trim().Replace("'", "") +
                               "','" + mensaje.FECHACREA.Trim().Replace("'", "") +
                               "','" + mensaje.USUARIOMODIFICA.Trim().Replace("'", "") +
                               "','" + mensaje.FECHAMODIFICA.Trim().Replace("'", "") +
                               "'," + mensaje.OIDZONA +
                               ",'" + mensaje.PROPIETARIO.Trim().Replace("'", "") +
                               "'," + mensaje.OIDCTACONTANT +
                               ",'" + mensaje.EMAILREPRELEGAL.Trim().Replace("'", "") +
                               "','" + mensaje.FAXREPRELEGAL.Trim().Replace("'", "") +
                               "','" + mensaje.DIRECREPRETECNI.Trim().Replace("'", "") +
                               "','" + mensaje.TELFREPRETECNI.Trim().Replace("'", "") +
                               "','" + mensaje.FAXREPRETECNI.Trim().Replace("'", "") +
                               "'," + mensaje.OIDCIUDADREPTECNI +
                               ",'" + mensaje.RUC.Trim().Replace("'", "") +
                               "','" + mensaje.AFTN.Trim().Replace("'", "") +
                               "','" + mensaje.NOMBRECOMERCIAL.Trim().Replace("'", "") +
                               "','" + mensaje.OBSERVACION.Trim().Replace("'", "") +
                               "','" + mensaje.CODEMPRESA.Trim().Replace("'", "") +
                                "')";


                    cm.CommandText = cadena;
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    Console.WriteLine("registro insertado.:" + mensaje.OID);
                    //  archivo.WriteLine("sql Procesado:  " + cadena);
                    // }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("registro con error.:" + cadena);
                 
                }
            }
            con.Close();
            
        }

        //inserta ciudad

        public static void insertar_Ciudad(List<DatosGlobales> listaMensaje)
        {


            string cadena = "";
            string error = "";
            // AS400

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
            con.Open();
            iDB2Command cm = new iDB2Command();
            cm.Connection = con;
            
            string tabla = "OIDAR4";
            string registro = ValidaDuplicados.EliminaTemporal(tabla);

            foreach (DatosGlobales mensaje in listaMensaje)
            {
                try
                {
                    //valida si ya esta registrado el registro
                    //bool registro = ValidaDuplicados.RegistroInsertadoCiudad(mensaje.OID);

                    //if (registro == false)
                    //{

                    cadena = "INSERT INTO OIDAR4( OIDO08,OIDO09,OIDO10,OIDCO4,OIDNO3,OIDUS8,OIDFE9,OIDUS9,OIDF01,OIDCO5,OIDCO6)";
                    cadena += "values(" + mensaje.OID +
                               "," + mensaje.OIDREGION +
                               "," + mensaje.OIDPROVINCIA +
                               ",'" + mensaje.CODIGO.Trim().Replace("'", "") +
                               "','" + mensaje.NOMBRE.Trim().Replace("'", "") +
                                "','" + mensaje.USUARIOCREA.Trim().Replace("'", "") +
                                "','" + mensaje.FECHACREA.Trim().Replace("'", "") +
                                "','" + mensaje.USUARIOMODIFICA.Trim().Replace("'", "") +
                                "','" + mensaje.FECHAMODIFICA.Trim().Replace("'", "") +
                                "','" + mensaje.CODIGOIATA.Trim().Replace("'", "") +
                                "','" + mensaje.CODIGOOACI.Trim().Replace("'", "") +
                               "')";


                    cm.CommandText = cadena;
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    Console.WriteLine("registro insertado.:" + mensaje.OID);
                    //  archivo.WriteLine("sql Procesado:  " + cadena);
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine("registro con error.:" + cadena);
            
                }
            }
            con.Close();
            
        }
    }
}
