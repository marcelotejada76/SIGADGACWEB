using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_ControlFR3
    {
        public static CD_ControlFR3 _instancia = null;
        private CD_ControlFR3()
        {

        }

        public static CD_ControlFR3 Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ControlFR3();
                }
                return _instancia;
            }
        }

        public decimal secuencialFR3(string aeropuerto, string anio)
        {
            // string query = "SELECT IFNULL(max(OPSSEC), 0) + 1 AS Secuencial FROM OPSARC WHERE OPSAER = '" + aeropuerto + "' AND OPSANO = '" + anio + "'";
            string query = "SELECT IFNULL(max(OPSSEC), 0) + 1 AS Secuencial FROM OPSARC WHERE OPSAER = '" + aeropuerto + "'";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }

        public decimal secuencialFR3Detalle(decimal OidSecuncial, string aeropuerto, string anio)
        {
            string query = "SELECT IFNULL(max(OPCSE1), 0) + 1 AS Secuencial FROM OPCAR6 WHERE OPCSE2 = " + OidSecuncial + " AND OPCAE1 = '" + aeropuerto + "' AND OPCAE1 = '" + anio + "'";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }


        public bool ActualizaSecuencialFR3(string aeropuerto, string anio, decimal secuencial)
        {
            bool estado = false;
            string query = "UPDATE  OPSARC SET OPSSEC = @Secuencial WHERE OPSAER = @Aeropuerto";
            iDB2Command cmd;

            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Aeropuerto"].Value = aeropuerto;
                    cmd.Parameters["@Secuencial"].Value = secuencial;
                    estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                // cmd.Parameters["@Anio"].Value = anio;
            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }


        public bool NuevoControlFR3(tbControlFR3 control)
        {
            bool estado = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                control.Secuencial = secuencialFR3(control.Aeropuerto, control.Anio);
                queryInsertar = "  INSERT INTO  OPCAR5 (OPCSEC, OPCAER, OPCANO, OPCFE4, OPCTIP, OPCRUT, OPCNRO, "
                + "OPCTOT, OPCGRA, OPCSON, OPCAUT, OPCOBS, OPCOID, OPCORI, OPCDE7, OPCRET, "
                  + "OPCCAL, OPCEST, OPCRU1, OPCEM1, OPCNAC, OPCUS7, OPCDA4, OPCH01, OPCOI1, "
                  + "OPCTE1, OPCNO4, OPCDI3, OPCOI2, OPCVA6,  OPCFOR, OPCNO5, "
                  + "OPCMOD, OPCPES, OPCC08, OPCNO6, OPCEM2, OPCMAT, OPCPRO, OPCSUB, OPCOI3, OPCDI2) "
                  + "VALUES(@Secuencial, @Aeropuerto, @Anio, @FechaControlVuelo, @TipoOperacion, @RutaTotalPlanVlo, @NumAterrizaPais, "
                  + "@Total, @GranTotal, @GranTotalLetras, @Autorizacion, @Observacion, @OidCiaAviacion, @Origen, @Destino, @Retorno, "
                  + "@Callsign, @Estado, @Ruc, @Email, @NacInter, @UsuarioCr, @FechaCr, @HoraCr, @IdAeropuerto, "
                  + "@Telefono, @NombreCliente, @Direccion, @OidUbicacionCliente, @ValorCharter, @FormaPago, @NombreCia, "
                  + "@Modelo, @PesoMatricula, @CodigoOACICia, @NombreAeropuerto, @EmailUsuarioDGAC, @Matricula, @Procesado, @SubTotal, @OidUbicacion, @ValorTotalMillas)";
                cmd = new iDB2Command(queryInsertar, oConexion);
                oConexion.Open();
                cmd.DeriveParameters();
                cmd.Parameters["@Secuencial"].Value = control.Secuencial;
                cmd.Parameters["@Aeropuerto"].Value = control.Aeropuerto;
                cmd.Parameters["@Anio"].Value = control.Anio;
                cmd.Parameters["@FechaControlVuelo"].Value = control.FechaControlVuelo;
                cmd.Parameters["@TipoOperacion"].Value = control.TipoOperacion;
                cmd.Parameters["@RutaTotalPlanVlo"].Value = control.RutaTotalPlanVlo;
                cmd.Parameters["@ValorCharter"].Value = control.ValorCharter;
                cmd.Parameters["@NumAterrizaPais"].Value = control.NumAterrizaPais;
                cmd.Parameters["@Total"].Value = control.Total;
                cmd.Parameters["@GranTotal"].Value = control.GranTotal;
                cmd.Parameters["@GranTotalLetras"].Value = control.GranTotalLetras;
                cmd.Parameters["@Autorizacion"].Value = control.Autorizacion;
                cmd.Parameters["@Observacion"].Value = control.Observacion;
                cmd.Parameters["@OidCiaAviacion"].Value = control.OidCiaAviacion;
                cmd.Parameters["@Origen"].Value = control.Origen;
                cmd.Parameters["@Destino"].Value = control.Destino;
                cmd.Parameters["@Retorno"].Value = control.Retorno;
                cmd.Parameters["@Callsign"].Value = control.Callsign;
                cmd.Parameters["@Estado"].Value = control.Estado;
                cmd.Parameters["@Ruc"].Value = control.Ruc;
                cmd.Parameters["@Email"].Value = control.Email;
                cmd.Parameters["@NacInter"].Value = control.NacInter;
                cmd.Parameters["@UsuarioCr"].Value = control.UsuarioCr;
                cmd.Parameters["@FechaCr"].Value = osistema.FechaSistema;
                cmd.Parameters["@HoraCr"].Value = osistema.HoraSistema;
                cmd.Parameters["@IdAeropuerto"].Value = control.IdAeropuerto;
                cmd.Parameters["@Telefono"].Value = control.Telefono;
                cmd.Parameters["@NombreCliente"].Value = control.NombreCliente;
                cmd.Parameters["@Direccion"].Value = control.Direccion;
                cmd.Parameters["@OidUbicacionCliente"].Value = control.OidUbicacionCliente;
                cmd.Parameters["@ValorCharter"].Value = control.ValorCharter;
                cmd.Parameters["@FormaPago"].Value = control.FormaPago;
                cmd.Parameters["@NombreCia"].Value = control.NombreCia;
                cmd.Parameters["@Modelo"].Value = control.Modelo;
                cmd.Parameters["@PesoMatricula"].Value = control.PesoMatricula.ToString().Replace(",", ".");
                cmd.Parameters["@CodigoOACICia"].Value = control.CodigoOACICia;
                cmd.Parameters["@NombreAeropuerto"].Value = control.NombreAeropuerto;
                cmd.Parameters["@EmailUsuarioDGAC"].Value = control.EmailUsuarioDGAC;
                cmd.Parameters["@Matricula"].Value = control.Matricula;
                cmd.Parameters["@Procesado"].Value = "E";
                cmd.Parameters["@SubTotal"].Value = control.SubTotal;
                cmd.Parameters["@OidUbicacion"].Value = control.OidUbicacion;
                cmd.Parameters["@ValorTotalMillas"].Value = control.GranTotal;
                estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                cmd.Dispose();
                oConexion.Close();
                if (estado)
                {
                    ActualizaSecuencialFR3(control.Aeropuerto, control.Anio, control.Secuencial);
                    control._odetalleControlFr3.Secuencial = control.Secuencial;
                    control._odetalleControlFr3.Aeropuerto = control.Aeropuerto;
                    control._odetalleControlFr3.Anio = control.Anio;
                    control._odetalleControlFr3.SecuencialDetalle = 1;
                    control._odetalleControlFr3.OidFormulario = 14134812;
                    control._odetalleControlFr3.CodigoContable = "623.01.11.02";
                    control._odetalleControlFr3.HacerDescuento = "N";
                    control._odetalleControlFr3.CobrarImpuesto = "N";
                    control._odetalleControlFr3.IngresarCantidad = "S";
                    control._odetalleControlFr3.Codigo = "FITEM";
                    if (control.NacInter.Equals("N"))
                    {
                        control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO NACIONAL, NO.COMPROBANTE. " + control.Secuencial + ", RUTA " + control.RutaTotalPlanVlo;
                    }
                    else
                    {
                        control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO INTERNACIONAL, NO.COMPROBANTE. " + control.Secuencial + ", RUTA " + control.RutaTotalPlanVlo;
                    }

                    //control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO NACIONAL NO.COMPROBANTE. " + control.Secuencial + " NA/IN " + control._odetalleControlFr3.DescripcionCuenta + " CIA NAC MAT SERSA RUTA " + control.RutaTotalPlanVlo;
                    estado = NuevoControlFr3Detalle(control._odetalleControlFr3);
                }
            }

            return estado;
        }

        public bool NuevoControlFR3(tbControlFR3 control, tbSolictudVuelo oSolicitud)
        {
            bool estado = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                control.Secuencial = secuencialFR3(control.Aeropuerto, control.Anio);
                queryInsertar = "  INSERT INTO  OPCAR5 (OPCSEC, OPCAER, OPCANO, OPCFE4, OPCTIP, OPCRUT, OPCNRO, "
                + "OPCTOT, OPCGRA, OPCSON, OPCAUT, OPCOBS, OPCOID, OPCORI, OPCDE7, OPCRET, "
                  + "OPCCAL, OPCEST, OPCRU1, OPCEM1, OPCNAC, OPCUS7, OPCDA4, OPCH01, OPCOI1, "
                  + "OPCTE1, OPCNO4, OPCDI3, OPCOI2, OPCVA6,  OPCFOR, OPCNO5, "
                  + "OPCMOD, OPCPES, OPCC08, OPCNO6, OPCEM2, OPCMAT, OPCPRO, OPCSUB, OPCOI3, OPCDI2, OPCFE9) "
                  + "VALUES(@Secuencial, @Aeropuerto, @Anio, @FechaControlVuelo, @TipoOperacion, @RutaTotalPlanVlo, @NumAterrizaPais, "
                  + "@Total, @GranTotal, @GranTotalLetras, @Autorizacion, @Observacion, @OidCiaAviacion, @Origen, @Destino, @Retorno, "
                  + "@Callsign, @Estado, @Ruc, @Email, @NacInter, @UsuarioCr, @FechaCr, @HoraCr, @IdAeropuerto, "
                  + "@Telefono, @NombreCliente, @Direccion, @OidUbicacionCliente, @ValorCharter, @FormaPago, @NombreCia, "
                  + "@Modelo, @PesoMatricula, @CodigoOACICia, @NombreAeropuerto, @EmailUsuarioDGAC, @Matricula, @Procesado, @SubTotal, @OidUbicacion, @ValorTotalMillas, @FechaRcepcion)";
                cmd = new iDB2Command(queryInsertar, oConexion);
                oConexion.Open();
                cmd.DeriveParameters();
                cmd.Parameters["@Secuencial"].Value = control.Secuencial;
                cmd.Parameters["@Aeropuerto"].Value = control.Aeropuerto;
                cmd.Parameters["@Anio"].Value = control.Anio;
                cmd.Parameters["@FechaControlVuelo"].Value = control.FechaControlVuelo;
                cmd.Parameters["@TipoOperacion"].Value = control.TipoOperacion;
                cmd.Parameters["@RutaTotalPlanVlo"].Value = control.RutaTotalPlanVlo;
                cmd.Parameters["@ValorCharter"].Value = control.ValorCharter;
                cmd.Parameters["@NumAterrizaPais"].Value = control.NumAterrizaPais;
                cmd.Parameters["@Total"].Value = control.Total;
                cmd.Parameters["@GranTotal"].Value = control.GranTotal;
                cmd.Parameters["@GranTotalLetras"].Value = control.GranTotalLetras;
                cmd.Parameters["@Autorizacion"].Value = control.Autorizacion;
                cmd.Parameters["@Observacion"].Value = control.Observacion;
                cmd.Parameters["@OidCiaAviacion"].Value = control.OidCiaAviacion;
                cmd.Parameters["@Origen"].Value = control.Origen;
                cmd.Parameters["@Destino"].Value = control.Destino;
                cmd.Parameters["@Retorno"].Value = control.Retorno;
                cmd.Parameters["@Callsign"].Value = control.Callsign;
                cmd.Parameters["@Estado"].Value = control.Estado;
                cmd.Parameters["@Ruc"].Value = control.Ruc;
                cmd.Parameters["@Email"].Value = control.Email;
                cmd.Parameters["@NacInter"].Value = control.NacInter;
                cmd.Parameters["@UsuarioCr"].Value = control.UsuarioCr;
                cmd.Parameters["@FechaCr"].Value = osistema.FechaSistema;
                cmd.Parameters["@HoraCr"].Value = osistema.HoraSistema;
                cmd.Parameters["@IdAeropuerto"].Value = control.IdAeropuerto;
                cmd.Parameters["@Telefono"].Value = control.Telefono;
                cmd.Parameters["@NombreCliente"].Value = control.NombreCliente;
                cmd.Parameters["@Direccion"].Value = control.Direccion;
                cmd.Parameters["@OidUbicacionCliente"].Value = control.OidUbicacionCliente;
                cmd.Parameters["@ValorCharter"].Value = control.ValorCharter;
                cmd.Parameters["@FormaPago"].Value = control.FormaPago;
                cmd.Parameters["@NombreCia"].Value = control.NombreCia;
                cmd.Parameters["@Modelo"].Value = control.Modelo;
                cmd.Parameters["@PesoMatricula"].Value = control.PesoMatricula.ToString().Replace(",", ".");
                cmd.Parameters["@CodigoOACICia"].Value = control.CodigoOACICia;
                cmd.Parameters["@NombreAeropuerto"].Value = control.NombreAeropuerto;
                cmd.Parameters["@EmailUsuarioDGAC"].Value = control.EmailUsuarioDGAC;
                cmd.Parameters["@Matricula"].Value = control.Matricula;
                cmd.Parameters["@Procesado"].Value = "E";
                cmd.Parameters["@SubTotal"].Value = control.SubTotal;
                cmd.Parameters["@OidUbicacion"].Value = control.OidUbicacion;
                cmd.Parameters["@ValorTotalMillas"].Value = control.GranTotal;
                cmd.Parameters["@FechaRcepcion"].Value = campoNull(control.FechaRcepcion);
                estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                cmd.Dispose();
                oConexion.Close();
                if (estado)
                {
                    ActualizaSecuencialFR3(control.Aeropuerto, control.Anio, control.Secuencial);
                    control._odetalleControlFr3.Secuencial = control.Secuencial;
                    control._odetalleControlFr3.Aeropuerto = control.Aeropuerto;
                    control._odetalleControlFr3.Anio = control.Anio;
                    control._odetalleControlFr3.SecuencialDetalle = 1;
                    control._odetalleControlFr3.CodigoContable = "623.01.11.02";
                    control._odetalleControlFr3.HacerDescuento = "N";
                    control._odetalleControlFr3.CobrarImpuesto = "N";
                    control._odetalleControlFr3.IngresarCantidad = "S";
                    control._odetalleControlFr3.Codigo = "FITEM";
                    if (control.NacInter.Equals("N"))
                    {
                        control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO NACIONAL" + " RUTA " + control.RutaTotalPlanVlo.Trim() + " FECHA " + oSolicitud.oDetalleRuta[0].FechaIdaVuelo.Trim() + " No SOL. " + oSolicitud.NumeroSolicitud.ToString().Trim();
                        control._odetalleControlFr3.OidFormulario = 14134812;
                    }
                    else
                    {
                        control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO INTERNACIONAL" + " RUTA " + control.RutaTotalPlanVlo.Trim() + " FECHA " + oSolicitud.oDetalleRuta[0].FechaIdaVuelo.Trim() + " No SOL. " + oSolicitud.NumeroSolicitud.ToString().Trim();
                        control._odetalleControlFr3.OidFormulario = 14134842;
                    }

                    //control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO NACIONAL NO.COMPROBANTE. " + control.Secuencial + " NA/IN " + control._odetalleControlFr3.DescripcionCuenta + " CIA NAC MAT SERSA RUTA " + control.RutaTotalPlanVlo;
                    estado = NuevoControlFr3Detalle(control._odetalleControlFr3);
                }
            }

            return estado;
        }

        /// <summary>
        /// Metodo envía a grabar el fr3 y detalles CHARTER
        /// </summary>
        /// <param name="control"></param>
        /// <param name="oSolicitud"></param>
        /// <returns></returns>
        public bool NuevoControlFR3Nuevo(tbControlFR3 control, tbSolictudVuelo oSolicitud)
        {
            bool estado = false;
            string queryInsertar = string.Empty;
            string cfechaVuelo = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                control.Secuencial = secuencialFR3(control.Aeropuerto, control.Anio);
                queryInsertar = "  INSERT INTO  OPCAR5 (OPCSEC, OPCAER, OPCANO, OPCFE4, OPCTIP, OPCRUT, OPCNRO, "
                + "OPCTOT, OPCGRA, OPCSON, OPCAUT, OPCOBS, OPCOID, OPCORI, OPCDE7, OPCRET, "
                  + "OPCCAL, OPCEST, OPCRU1, OPCEM1, OPCNAC, OPCUS7, OPCDA4, OPCH01, OPCOI1, "
                  + "OPCTE1, OPCNO4, OPCDI3, OPCOI2, OPCVA6,  OPCFOR, OPCNO5, "
                  + "OPCMOD, OPCPES, OPCC08, OPCNO6, OPCEM2, OPCMAT, OPCPRO, OPCSUB, OPCOI3, OPCDI2, OPCFE9, OPCBAN, OPCCHE) "
                  + "VALUES(@Secuencial, @Aeropuerto, @Anio, @FechaControlVuelo, @TipoOperacion, @RutaTotalPlanVlo, @NumAterrizaPais, "
                  + "@Total, @GranTotal, @GranTotalLetras, @Autorizacion, @Observacion, @OidCiaAviacion, @Origen, @Destino, @Retorno, "
                  + "@Callsign, @Estado, @Ruc, @Email, @NacInter, @UsuarioCr, @FechaCr, @HoraCr, @IdAeropuerto, "
                  + "@Telefono, @NombreCliente, @Direccion, @OidUbicacionCliente, @ValorCharter, @FormaPago, @NombreCia, "
                  + "@Modelo, @PesoMatricula, @CodigoOACICia, @NombreAeropuerto, @EmailUsuarioDGAC, @Matricula, @Procesado, @SubTotal, @OidUbicacion, @ValorTotalMillas, @FechaRcepcion, "
                  + "@CodigoBanco, @Deposito)";
                cmd = new iDB2Command(queryInsertar, oConexion);
                oConexion.Open();
                cmd.DeriveParameters();
                cmd.Parameters["@Secuencial"].Value = control.Secuencial;
                cmd.Parameters["@Aeropuerto"].Value = control.Aeropuerto;
                cmd.Parameters["@Anio"].Value = control.Anio;
                cmd.Parameters["@FechaControlVuelo"].Value = control.FechaControlVuelo;
                cmd.Parameters["@TipoOperacion"].Value = control.TipoOperacion;
                cmd.Parameters["@RutaTotalPlanVlo"].Value = control.RutaTotalPlanVlo;
                cmd.Parameters["@ValorCharter"].Value = control.ValorCharter;
                cmd.Parameters["@NumAterrizaPais"].Value = control.NumAterrizaPais;
                cmd.Parameters["@Total"].Value = control.Total;
                cmd.Parameters["@GranTotal"].Value = control.GranTotal;
                cmd.Parameters["@GranTotalLetras"].Value = control.GranTotalLetras;
                cmd.Parameters["@Autorizacion"].Value = control.Autorizacion;
                cmd.Parameters["@Observacion"].Value = control.Observacion;
                cmd.Parameters["@OidCiaAviacion"].Value = control.OidCiaAviacion;
                cmd.Parameters["@Origen"].Value = control.Origen;
                cmd.Parameters["@Destino"].Value = control.Destino;
                cmd.Parameters["@Retorno"].Value = control.Retorno;
                cmd.Parameters["@Callsign"].Value = control.Callsign;
                cmd.Parameters["@Estado"].Value = control.Estado;
                cmd.Parameters["@Ruc"].Value = control.Ruc;
                cmd.Parameters["@Email"].Value = control.Email;
                cmd.Parameters["@NacInter"].Value = control.NacInter;
                cmd.Parameters["@UsuarioCr"].Value = control.UsuarioCr;
                cmd.Parameters["@FechaCr"].Value = osistema.FechaSistema;
                cmd.Parameters["@HoraCr"].Value = osistema.HoraSistema;
                cmd.Parameters["@IdAeropuerto"].Value = control.IdAeropuerto;
                cmd.Parameters["@Telefono"].Value = control.Telefono;
                cmd.Parameters["@NombreCliente"].Value = control.NombreCliente;
                cmd.Parameters["@Direccion"].Value = control.Direccion;
                cmd.Parameters["@OidUbicacionCliente"].Value = control.OidUbicacionCliente;
                cmd.Parameters["@ValorCharter"].Value = control.ValorCharter;
                cmd.Parameters["@FormaPago"].Value = control.FormaPago;
                cmd.Parameters["@NombreCia"].Value = control.NombreCia;
                cmd.Parameters["@Modelo"].Value = control.Modelo;
                cmd.Parameters["@PesoMatricula"].Value = control.PesoMatricula.ToString().Replace(",", ".");
                cmd.Parameters["@CodigoOACICia"].Value = control.CodigoOACICia;
                cmd.Parameters["@NombreAeropuerto"].Value = control.NombreAeropuerto;
                cmd.Parameters["@EmailUsuarioDGAC"].Value = control.EmailUsuarioDGAC;
                cmd.Parameters["@Matricula"].Value = control.Matricula;
                cmd.Parameters["@Procesado"].Value = "E";
                cmd.Parameters["@SubTotal"].Value = control.SubTotal;
                cmd.Parameters["@OidUbicacion"].Value = control.OidUbicacion;
                cmd.Parameters["@ValorTotalMillas"].Value = 0; // control.GranTotal;
                cmd.Parameters["@FechaRcepcion"].Value = control.FechaRcepcion;
                cmd.Parameters["@CodigoBanco"].Value = control.CodigoBanco;
                cmd.Parameters["@Deposito"].Value = control.Deposito.Trim();
                estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                cmd.Dispose();
                oConexion.Close();
                if (estado)
                {
                    //Actualizar el pago der solicitud                    
                    ActualizaSecuencialFR3(control.Aeropuerto, control.Anio, control.Secuencial);
                    foreach (var item in oSolicitud.oDetalleRuta)
                    {
                        if (item.FechaIdaVuelo != item.FechaRetornoVuelo)
                            cfechaVuelo = "F/IDA " + item.FechaIdaVuelo + " F/VUELTA " + item.FechaRetornoVuelo;
                        else
                            cfechaVuelo = item.FechaIdaVuelo;

                        tbControlFR3Detalle odetalleControlFr3 = new tbControlFR3Detalle();
                        control._odetalleControlFr3.Secuencial = control.Secuencial;
                        control._odetalleControlFr3.Aeropuerto = control.Aeropuerto;
                        control._odetalleControlFr3.Anio = control.Anio;
                        control._odetalleControlFr3.SecuencialDetalle = secuencialFR3Detalle(control.Secuencial, control.Aeropuerto, control.Anio);
                        control._odetalleControlFr3.CodigoContable = "623.01.11.02";
                        control._odetalleControlFr3.HacerDescuento = "N";
                        control._odetalleControlFr3.CobrarImpuesto = "N";
                        control._odetalleControlFr3.IngresarCantidad = "S";
                        control._odetalleControlFr3.Codigo = "FITEM";
                        control._odetalleControlFr3.Cantidad = 1;
                        control._odetalleControlFr3.Valor = oSolicitud.CostoCharter;
                        control._odetalleControlFr3.Total = oSolicitud.CostoCharter;

                        if (control.NacInter.Equals("N"))
                        {
                            control._odetalleControlFr3.DescripcionCuenta = "VUELOS CHARTER O ESPECIALES NACIONAL";
                            control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES NACIONAL" + " RUTA " + item.RutaVuelo.Trim() + cfechaVuelo.Trim() + " SOL. " + oSolicitud.NumeroSolicitud.ToString().Trim() + " C/P " + oSolicitud.oPagoSolictud[0].ComprobanteTransaccion.Trim() + " CIA " + oSolicitud.NombreCompaniaAviacion.Trim();
                            control._odetalleControlFr3.OidFormulario = 14134812;
                        }
                        else
                        {
                            control._odetalleControlFr3.DescripcionCuenta = "VUELOS CHARTER O ESPECIALES INTERNACIONAL";
                            control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES INTERNACIONAL" + " RUTA " + item.RutaVuelo.Trim() + cfechaVuelo.Trim() + " SOL. " + oSolicitud.NumeroSolicitud.ToString().Trim() + " C/P " + oSolicitud.oPagoSolictud[0].ComprobanteTransaccion.Trim() + " CIA " + oSolicitud.NombreCompaniaAviacion.Trim();
                            control._odetalleControlFr3.OidFormulario = 14134842;
                        }
                        control._odetalleControlFr3.Codigo = "FITEM";
                        control._odetalleControlFr3.TipoCobro = "01"; //FR3
                        estado = NuevoControlFr3Detalle(control._odetalleControlFr3);
                        cfechaVuelo = string.Empty;
                    }

                    string idControlFr3 = control.Secuencial.ToString() + "-" + control.Aeropuerto.Trim() + "-" + control.Anio;
                    CD_PagoSolicitud.Instancia.ActualizaPagoSolicitudFactura(oSolicitud.NumeroSolicitud, idControlFr3, "", control.Ruc, control.UsuarioCr);
                    //control._odetalleControlFr3.Descripcion = "VUELOS CHARTER O ESPECIALES PARA SERVICIO NACIONAL NO.COMPROBANTE. " + control.Secuencial + " NA/IN " + control._odetalleControlFr3.DescripcionCuenta + " CIA NAC MAT SERSA RUTA " + control.RutaTotalPlanVlo;

                }
            }

            return estado;
        }


        public bool NuevoControlFr3Detalle(tbControlFR3Detalle detalle)
        {
            bool estado = false;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                queryInsertar = "  INSERT INTO OPCAR6 (OPCSE2, OPCAE1, OPCAN1, OPCSE1, OPCTI1, OPCOI4, OPCC05, OPCDE8, OPCCAN, OPCVA1, "
                    + " OPCHAC, OPCCOB, OPCING, OPCD01, OPCC06, OPCTO1) "
                  + " VALUES(@Secuencial, @Aeropuerto, @Anio, @SecuencialDetalle, @TipoCobro, @OidFormulario, @CodigoContable, @Descripcion, @Cantidad, @Valor, "
                  + " @HacerDescuento, @CobrarImpuesto, @ingresarCantidad, @DescripcionCuenta, @Codigo, @Total)";
                cmd = new iDB2Command(queryInsertar, oConexion);
                oConexion.Open();
                cmd.DeriveParameters();
                cmd.Parameters["@Secuencial"].Value = detalle.Secuencial;
                cmd.Parameters["@Aeropuerto"].Value = detalle.Aeropuerto;
                cmd.Parameters["@Anio"].Value = detalle.Anio;
                cmd.Parameters["@SecuencialDetalle"].Value = detalle.SecuencialDetalle;
                cmd.Parameters["@TipoCobro"].Value = detalle.TipoCobro;
                cmd.Parameters["@OidFormulario"].Value = detalle.OidFormulario;
                cmd.Parameters["@CodigoContable"].Value = detalle.CodigoContable;
                cmd.Parameters["@Descripcion"].Value = detalle.Descripcion;
                cmd.Parameters["@Cantidad"].Value = detalle.Cantidad;
                cmd.Parameters["@Valor"].Value = detalle.Valor;
                cmd.Parameters["@HacerDescuento"].Value = detalle.HacerDescuento;
                cmd.Parameters["@CobrarImpuesto"].Value = detalle.CobrarImpuesto;
                cmd.Parameters["@ingresarCantidad"].Value = detalle.IngresarCantidad;
                cmd.Parameters["@DescripcionCuenta"].Value = detalle.DescripcionCuenta;
                cmd.Parameters["@Codigo"].Value = detalle.Codigo;
                cmd.Parameters["@Total"].Value = detalle.Total;

                estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                cmd.Dispose();
                oConexion.Close();
            }

            return estado;
        }

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}
