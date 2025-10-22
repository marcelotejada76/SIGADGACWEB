using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.IO;

namespace CapaDatos
{
    class InsertarTablaP5
    {
        public static void insertar_Movimiento(List<CamposUceo> listaMensaje, string FechaProceso, string path)
        {
            string cadena = "";
            string error = "";
            int cont = 0;
            try
            {


                Int32 Secuencial = 0;
                //Int32 sec = ValidaDuplicados.Secuencial(Secuencial);
                //sec = sec + 3;

                //if (sec == 3)
                //{
                //    sec = ValidaDuplicados.Secuencial(Secuencial);
                //    sec = sec + 3;

                //}

                Int32 sec = ValidaDuplicados.Secuencialp9(Secuencial);
                //sec = sec + 1;


                OdbcCommand cmd;
                ////try
                ////{
                OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion);
                //// {
                //// cmd = new OdbcCommand(query, oConexion);
                //oConexion.Open();
                ////  OdbcDataReader dr = cmd.ExecuteReader();

                // AS400

                //        iDB2Connection con = new iDB2Connection(ConexionAs400.Conexion);

                //iDB2Command cm = new iDB2Command();
                //cm.Connection = con;

                // string path = Server.MapPath("~/Temporal/ArchivosP550/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                //FileStream file = new FileStream(path + "StoreProcedureP550" + FechaProceso + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);



                System.IO.StreamWriter archivo = new StreamWriter(path + "InsertaMovimientosUceo" + FechaProceso + ".txt", true);
                // StreamWriter archivo = new StreamWriter("c://Temporal//ArchivosP550//InsertaMovimientosUceo" + FechaProceso + ".txt", true);
                archivo.WriteLine("----------------------------------------------------------------------------------------------------------");
                archivo.WriteLine("Fecha: " + DateTime.Now);

                // using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                oConexion.Open();
                bool ErrorSql = false;
                foreach (CamposUceo mensaje in listaMensaje)
                {
                    try
                    {
                        //valida si ya esta registrado el registro
                        bool Reg = false;
                        bool registro = ValidaDuplicados.DatosEncontrados(Reg, mensaje.ORIGEN, mensaje.DESTINO, mensaje.NUMEROVUELO, mensaje.FECHAREAL, mensaje.HORAREAL, mensaje.OIDAERONAVECOMPANI);
                        // bool registro = false;
                        if (registro == false)
                        {

                            //FECHACREA
                            cadena = "INSERT INTO MOVIMIENTOAERONAVE(OID,OIDITEMAUTORIZACIO,	OIDTIPOVUELO,OIDAEROVIA,OIDLUGARATERRIZAJE,FECHAREAL,HORAREAL,OPERACION," +
                                "USUARIOCREA,OIDAERONAVECOMPANI,FACTSER,FACTEST,ORIGEN,DESTINO,OIDORIGEN,OIDDESTINO,TOTALMILLAS,OIDSOLICITANTE," +
                                "TIPOAUTORIZACION,NUMEROVUELO,OIDTIPOOPERACION,RUTAEROVIA,OBSERVACION,AUTORIZACION,FACTURAR)";
                            cadena += "values(" + sec +
                                       "," + mensaje.OIDITEMAUTORIZACIO +
                                       "," + mensaje.OIDTIPOVUELO +
                                       "," + mensaje.OIDAEROVIA +
                                       "," + mensaje.OIDLUGARATERRIZAJE +
                                       ",'" + mensaje.FECHAREAL.Trim().Replace("'", "") +
                                       "','" + mensaje.HORAREAL.Trim().Replace("'", "") +
                                       "','" + mensaje.OPERACION.Trim().Replace("'", "") +
                                        "','" + mensaje.USUARIOCREA.Trim().Replace("'", "") +
                                        // "','" + mensaje.FECHACREA.Trim().Replace("'", "") +
                                        "'," + mensaje.OIDAERONAVECOMPANI +
                                        ",'" + mensaje.FACTSER.Trim().Replace("'", "") +
                                        "','" + mensaje.FACTEST.Trim().Replace("'", "") +
                                        "','" + mensaje.ORIGEN.Trim().Replace("'", "") +
                                        "','" + mensaje.DESTINO.Trim().Replace("'", "") +
                                        "'," + mensaje.OIDORIGEN +
                                        "," + mensaje.OIDDESTINO +
                                        "," + mensaje.TOTALMILLAS.ToString().Replace(",", ".") +
                                        "," + mensaje.OIDSOLICITANTE +
                                        ",'" + mensaje.TIPOAUTORIZACION.Trim().Replace("'", "") +
                                        "','" + mensaje.NUMEROVUELO.Trim().Replace("'", "") +
                                        "'," + mensaje.OIDTIPOOPERACION +
                                        ",'" + mensaje.RUTAEROVIA.Trim().Replace("'", "") +
                                        "','" + mensaje.OBSERVACION.Trim().Replace("'", "") +
                                        "','" + mensaje.AUTORIZACION.Trim().Replace("'", "") +
                                        "','" + mensaje.FACTURAR.Trim().Replace("'", "") +
                                       "')";

                            cmd = new OdbcCommand(cadena, oConexion);
                            cmd.ExecuteReader();
                            Console.WriteLine("registro insertado.:" + mensaje.OID);
                            cont++;

                            // archivo.WriteLine("sql Procesado:  " + cadena);
                            sec++;
                        }
                        else
                        {
                            archivo.WriteLine("sql Duplicado:  " + cadena);
                        }
                    }
                    catch (Exception ex)
                    {


                        Console.WriteLine("registro con error.:" + cadena);
                        archivo.WriteLine("sql error:  " + cadena);
                        sec++;
                        ErrorSql = true;
                    }
                }

                //ACTUALIZA SECUENCIA
                var secuencia = ValidaDuplicados.ActualizaSecuencialP9(sec);

                if (ErrorSql == false)
                {
                    archivo.WriteLine("Registros Procesados Fecha Real Vuelo:  " + cont);

                }
                //  con.Close();
                archivo.Close();
                archivo.Dispose();
                // }

            }
            catch (Exception Ex)
            {

                StreamWriter archivo = new StreamWriter("c://Temporal//ArchivosP550//InsertaMovimientosUceo" + FechaProceso + ".txt", true);
                archivo.WriteLine("----------------------------------------------------------------------------------------------------------");
                archivo.WriteLine("Fecha: " + DateTime.Now);
                archivo.WriteLine("error P550:  " + Ex);
                archivo.Close();
                archivo.Dispose();
            }

        }
    }
}
