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
   public class CD_MigraVlosNacIn
    {
        public static CD_MigraVlosNacIn _instancia = null;
        private CD_MigraVlosNacIn()
        {

        }

        public static CD_MigraVlosNacIn Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_MigraVlosNacIn();
                }
                return _instancia;
            }
        }

       

        public string ActualizaVuelosNacInP550(string FechaProcesoI, string FechaProcesoF ,string Aeropuerto, string Mensaje)
        {
            DateTime FechaProceso = DateTime.Now;
            string Fecha = FechaProceso.ToString("yyyyMMdd");
            bool estado = false;

            string path = @"\\172.20.19.55\Temporal\ArchivosP550NacInt\";
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.IO.StreamWriter archivoPA = new StreamWriter(path + "StoreProcedureP550NacInt" + FechaProcesoI + ".txt", true);
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

                var listaDatosMovimientosPeso = ValidaRegistros.TablaMensajesPeso(FechaProcesoI, FechaProcesoF, Aeropuerto);
                peso = listaDatosMovimientosPeso.Count;
                var listaDatosMovimientosDistancia = ValidaRegistros.TablaMensajesDistancia(FechaProcesoI, FechaProcesoF, Aeropuerto);
                distancia = listaDatosMovimientosDistancia.Count;
                //var listaDatosMovimientosDistanciaSuperior = ValidaRegistros.TablaMensajesDistanciaAlto(FechaProceso);
                //distanciaSup = listaDatosMovimientosDistanciaSuperior.Count;
                //var listaDatosMovimientosDistanciaInferior = ValidaRegistros.TablaMensajesDistanciaMinimo(FechaProceso);
                //distanciaInf = listaDatosMovimientosDistanciaInferior.Count;

                var listaDatosMovimientosSinValidar = ValidaRegistros.TablaMensajesRegistrosSinValidar(FechaProcesoI, FechaProcesoF, Aeropuerto);
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
               
                if (DocSinValidar > 0)
                {
                    //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " con incosnsistencias documentos sin validar, revise antes de enviar a procesar";
                    //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    //Button1.Enabled = true;
                }

                if (peso == 0 && distancia == 0 && DocSinValidar == 0)
                {

                    Int32 OidAto = 0;
                    Int32 DatoAeropuertoOid = ValidaRegistros.OidAeropuerto(Aeropuerto, OidAto);

                    //var listaDatosMovimientosRec = ValidaRegistros.Movimientos(FechaProceso, DatoAeropuertoOid);
                    //bool registroValido = false;
                    //foreach (CamposUceo mensaje in listaDatosMovimientosRec)
                    //{
                    //    registroValido = ValidaRegistros.Registro(mensaje.OIDAERONAVECOMPANI, mensaje.OIDAEROPUERTO, mensaje.OIDAEROVIA, FechaProceso,
                    //           mensaje.ORIGEN, mensaje.DESTINO,mensaje.OPERACION);

                    //    //registroValido = ValidaRegistros.Registro(mensaje.callsign, mensaje.registry, mensaje.Archivo, mensaje.fechaProceso,
                    //    //       mensaje.ORIGEN, mensaje.DESTINO, mensaje.OIDAEROPUERTO);
                    //    break;
                    //}
                    bool registroValido = false;
                    if (registroValido == false)
                    {

                        iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
                        con.Open();

                        iDB2Command cm = new iDB2Command();
                        cm.Connection = con;

                        string cadena = "PA_ACTUALIZAP550";

                        String Tipo = "I";
                        cm.CommandText = cadena;
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@PR_FECHAINICIO", FechaProcesoI);
                        cm.Parameters.AddWithValue("@PR_FECHAFIN", FechaProcesoF);
                        cm.Parameters.AddWithValue("@PR_TIPO", Tipo);
                        cm.Parameters.AddWithValue("@PR_AEROPUERTO", Aeropuerto);
                        cm.CommandTimeout = 0;
                        cm.ExecuteNonQuery();
                        archivoPA.WriteLine("SP Procesado con Exito del  " + FechaProcesoI + "Al  " + FechaProcesoI);
                        //NUMERO DE REGISTROS
                        int Registros = 0;
                        int RegFecha = ValidaRegistros.TablaMensajesNumregistros(FechaProcesoI, Registros, Aeropuerto);
                        archivoPA.WriteLine("Registros Fecha proceso Indra: " + RegFecha);

                        //CARGA AL P550
                        try
                        {
                            var listaDatosMovimientos = ValidaRegistros.Movimientos(FechaProcesoI, FechaProcesoF, DatoAeropuertoOid);

                            if (listaDatosMovimientos.Count > 0)
                            {
                                
                                InsertarTablaP5.insertar_MovimientoNacIn(listaDatosMovimientos, FechaProcesoI, path, Aeropuerto);

                            }
                            int Numeroregistros = listaDatosMovimientos.Count;
                            Mensaje = "Proceso Finalizado Correctamente";

                            //EnvioCorreo.EnviarCorreo(Numeroregistros, FechaProceso, RegFecha, path,Aeropuerto);
                            //this.lblMensaje.Text = "Procesado con Exito";
                            //lblMensaje.Attributes["style"] = "color:blue; font-weight:bold;";
                            //Button1.Enabled = true;

                        }
                        catch (Exception EX)
                         
                        {
                            Mensaje = "Datos del: " + FechaProceso + " Error al Insertar Registros";
                            //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " error al procesar envio al p550 ,Error en Datos";
                            //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                            ////throw;
                            //Button1.Enabled = true;

                        }



                    }
                    else
                    {
                        //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " ya se encuentra procesado, seleccione otra fecha...";
                        //Button1.Enabled = true;
                        //lblMensaje.Attributes["style"] = "color:red; font-weight:bold;";
                    }
                }
                else
                {
                    Mensaje = "Datos del: " + FechaProceso + " con incosnsistencias revise antes de enviar a procesar";
                    //// MessageBox.Show("Datos del: " + FechaProceso + " con incosnsistencias revise antes de enviar a procesar", "Error en Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Button1.Enabled = true;
                }
            }




            catch (Exception EX)
            {
                //MessageBox.Show("SP no Ejecutado del  " + FechaProceso + "Al  " + FechaProceso);
                archivoPA.WriteLine("SP no Ejecutado del  " + FechaProcesoI + "Al  " + FechaProcesoF);
                //this.lblMensaje.Text = "Datos del: " + FechaProcesoI + " error al procesar envio al p550 ,Error en Datos";
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
