using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_MigraSobrevuelos
    {
        public static CD_MigraSobrevuelos _instancia = null;
        private CD_MigraSobrevuelos()
        {

        }

        public static CD_MigraSobrevuelos Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_MigraSobrevuelos();
                }
                return _instancia;
            }
        }

       

        public string ActualizaSobrevuelosP550(string FechaProcesoI, string FechaProcesoF ,string Mensaje)
        {
            DateTime FechaProceso = DateTime.Now;
            string Fecha = FechaProceso.ToString("yyyyMMdd");
            bool estado = false;

            string path = @"\\172.20.19.55\Temporal\ArchivosP550\";
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.IO.StreamWriter archivoPA = new StreamWriter(path + "StoreProcedureP550" + Fecha + ".txt", true);
            //System.IO.StreamWriter archivoPA = new StreamWriter("c://Temporal//ArchivosP550//StoreProcedureP550" + FechaProceso + ".txt", true);
            archivoPA.WriteLine("----------------------------------------------------------------------------------------------------------");
            archivoPA.WriteLine("Fecha: " + DateTime.Now);

            try
            {
                //valida si todo esta correcto inconsistencias
                int peso = 0;
                int distancia = 0;
                int distanciaSup = 0;
                int distanciaInf = 0;
                int DocSinValidar = 0;

                var listaDatosMovimientosPeso = ValidaRegistros.TablaMensajesPeso(FechaProcesoI, FechaProcesoF);// (FechaProceso);
                peso = listaDatosMovimientosPeso.Count;
                var listaDatosMovimientosDistancia = ValidaRegistros.TablaMensajesDistancia(FechaProcesoI, FechaProcesoF);
                distancia = listaDatosMovimientosDistancia.Count;
                var listaDatosMovimientosDistanciaSuperior = ValidaRegistros.TablaMensajesDistanciaAlto(FechaProcesoI, FechaProcesoF);
                //  distanciaSup = listaDatosMovimientosDistanciaSuperior.Count;
                var listaDatosMovimientosDistanciaInferior = ValidaRegistros.TablaMensajesDistanciaMinimo(FechaProcesoI, FechaProcesoF);
                distanciaInf = listaDatosMovimientosDistanciaInferior.Count;

                var listaDatosMovimientosSinValidar = ValidaRegistros.TablaMensajesRegistrosSinValidar(FechaProcesoI, FechaProcesoF);
                DocSinValidar = listaDatosMovimientosSinValidar.Count;

                if (peso > 0)
                {

                    //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " con incosnsistencias Peso, revise antes de enviar a procesar, Error en Datos";
                    //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    //Button1.Enabled = true;
                }
                if (distancia > 0)
                {
                    //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " con incosnsistencias Distancia, revise antes de enviar a procesar Error en Datos";
                    //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    //Button1.Enabled = true;
                }
                if (distanciaSup > 0)
                {
                    //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " con incosnsistencias Distancia Superior a 435, revise antes de enviar a procesar";
                    //Button1.Enabled = true;
                    //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                }
                if (distanciaInf > 0)
                {
                    //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " con incosnsistencias Distancia Inferior a 125, revise antes de enviar a procesar";
                    //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    //Button1.Enabled = true;
                }
                if (DocSinValidar > 0)
                {
                    //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " con incosnsistencias documentos sin validar, revise antes de enviar a procesar";
                    //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    //Button1.Enabled = true;
                }

                if (peso == 0 && distancia == 0 && distanciaSup == 0 && distanciaInf == 0 && DocSinValidar == 0)
                {



                    //var listaDatosMovimientosRec = ValidaRegistros.Movimientos(FechaProcesoI, FechaProcesoF);
                    //bool registroValido = false;
                    //foreach (CamposUceo mensaje in listaDatosMovimientosRec)
                    //{
                    //    registroValido = ValidaRegistros.Registro(mensaje.callsign, mensaje.registry, mensaje.Archivo, mensaje.fechaProceso,
                    //           mensaje.ORIGEN, mensaje.DESTINO);
                    //    break;
                    //}
                    bool registroValido = false;
                    if (registroValido == false)
                    {



                        iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
                        con.Open();
                        try
                        {
                            iDB2Command cm = new iDB2Command();
                            cm.Connection = con;

                            string cadena = "PA_ACTUALIZAP550";

                            String Tipo = "S";
                            String Aeropuerto = "1";
                            cm.CommandText = cadena;
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.AddWithValue("@PR_FECHAINICIO", FechaProcesoI);
                            cm.Parameters.AddWithValue("@PR_FECHAFIN", FechaProcesoF);
                            cm.Parameters.AddWithValue("@PR_TIPO", Tipo);
                            cm.Parameters.AddWithValue("@PR_AEROPUERTO", Aeropuerto);
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                            archivoPA.WriteLine("SP Procesado con Exito del  " + FechaProcesoI + "Al  " + FechaProcesoF);
                            //NUMERO DE REGISTROS
                            int Registros = 0;
                            int RegFecha = ValidaRegistros.TablaMensajesNumregistros(FechaProcesoI, Registros);
                            archivoPA.WriteLine("Registros Fecha proceso Indra: " + RegFecha);
                            con.Close();
                        }
                        catch (Exception)
                        {


                        }
                        finally
                        {
                            // 🔹 Cierre manual por seguridad adicional (aunque using ya lo hace)
                            if (con.State != ConnectionState.Closed)
                            {
                                con.Close();
                            }
                        }

                        //CARGA AL P550
                        try
                        {
                            var listaDatosMovimientos = ValidaRegistros.MovimientosS(FechaProcesoI, FechaProcesoF);

                            if (listaDatosMovimientos.Count > 0)
                            {
                                //this.lblMensaje.Text = "Procesando espere por favor....";
                                InsertarTablaP5.insertar_Movimiento(listaDatosMovimientos, FechaProcesoI, path);

                            }
                            int Numeroregistros = listaDatosMovimientos.Count;


                            Mensaje = "Proceso Finalizado Correctamente";

                        }
                        catch (Exception ex)
                        {
                            //this.lblMensaje.Text = "Datos del: " + FechaProceso + " error al procesar envio al p550 ,Error en Datos";
                            //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                            ////throw;
                            //Button1.Enabled = true;
                            //ScriptManager.RegisterStartupScript(this, GetType(), "ocultar", "ocultar_procesar();", true);
                            //lblMensaje.Text = "❌ Error: " + ex.Message;

                        }



                    }
                    else
                    {
                        //this.lblMensaje.Text = "Datos del: " + FechaProceso + " ya se encuentra procesado, seleccione otra fecha...";
                        //Button1.Enabled = true;
                        //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    }
                }
                else
                {
                    Mensaje= "Datos del: " + FechaProceso + " con incosnsistencias revise antes de enviar a procesar";
                    // MessageBox.Show("Datos del: " + FechaProceso + " con incosnsistencias revise antes de enviar a procesar", "Error en Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Button1.Enabled = true;


                }
            }




            catch (Exception EX)
            {
                //MessageBox.Show("SP no Ejecutado del  " + FechaProceso + "Al  " + FechaProceso);
                //archivoPA.WriteLine("SP no Ejecutado del  " + FechaProceso + "Al  " + FechaProceso);
                //this.lblMensaje.Text = "Datos del: " + FechaProceso + " error al procesar envio al p550 ,Error en Datos";
                //Button1.Enabled = true;
                //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
            }

            archivoPA.Close();
            archivoPA.Dispose();



            return Mensaje;
        }


       
        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}
