using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;
namespace CapaDatos
{
  public class CD_PagoSolicitud
    {
        public static CD_PagoSolicitud _instancia = null;
        private CD_PagoSolicitud()
        {

        }

        public static CD_PagoSolicitud Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_PagoSolicitud();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Verifica si existe numero de comprobante
        /// </summary>
        /// <param name="formaPago"></param>
        /// <param name="numComprobante"></param>
        /// <returns>True-False</returns>
        public bool VerificaExistePagoSolicitud(string formaPago, string numComprobante)
        {
            bool estado = false;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            string query = "SELECT  PAGNUM FROM PAGARC WHERE PAGFOR = '" + formaPago.ToUpper().Trim() + "' AND PAGNU1 = '" + numComprobante.Trim() + "'";

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                cmd = new iDB2Command(query, oConexion);
                oConexion.Open();
                iDB2DataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    estado = true;
                }
                dr.Close();
            }
            return estado;
        }

        public tbPagoSolicitud PagoSolicitudPorNumeroSolicitudConsulta(decimal numSolicitud)
        {
            tbPagoSolicitud oPagoSol = new tbPagoSolicitud();
            string query = "SELECT PAGNUM as NumeroSolicitud, PAGNU1 as ComprobanteTransaccion, PAGFEC as FechaComprobanteTransaccion, PAGFOR as FormaPago, PAGVAL as ValorPagar, PAGRU1 as Ruc, PAGTIP as TipoComprobante, "
                        + " PAGEST as Establecimiento, PAGPUN as PuntoEmision, PAGNU2 as NumeroComprobante, PAGNU3 as NumeroAutorizacion, PAGUSU AS UsuarioCreado, PAGFE1 AS FechaCreado, PAGHOR as HoraCreado, PAGUS1 AS UsuarioModificado, PAGFE2 AS FechaModificado, PAGHO1 as HoraModificado "
                        + " FROM PAGARC WHERE PAGNUM = " + numSolicitud;
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oPagoSol.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oPagoSol.ComprobanteTransaccion = dr["ComprobanteTransaccion"].ToString();
                        oPagoSol.FechaComprobanteTransaccion = dr["FechaComprobanteTransaccion"].ToString();
                        oPagoSol.FormaPago = dr["FormaPago"].ToString();
                        oPagoSol.ValorPagar = decimal.Parse(dr["ValorPagar"].ToString());
                        oPagoSol.Ruc = dr["Ruc"].ToString();
                        oPagoSol.TipoComprobante = dr["TipoComprobante"].ToString();
                        oPagoSol.Establecimiento = dr["Establecimiento"].ToString();
                        oPagoSol.PuntoEmision = dr["PuntoEmision"].ToString();
                        oPagoSol.NumeroComprobante = dr["NumeroComprobante"].ToString();
                        oPagoSol.NumeroAutorizacion = dr["NumeroAutorizacion"].ToString();
                        oPagoSol.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oPagoSol.FechaCreado = dr["FechaCreado"].ToString();
                        oPagoSol.HoraCreado = dr["HoraCreado"].ToString();
                        oPagoSol.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oPagoSol.FechaModificado = dr["FechaModificado"].ToString();
                        oPagoSol.HoraModificado = dr["HoraModificado"].ToString();

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oPagoSol = null;
            }
            return oPagoSol;
        }

        public List<tbPagoSolicitud> PagoSolicitudPorNumeroSolicitudListar(decimal numSolicitud)
        {
            List<tbPagoSolicitud> listarPagoSolicitud = new List<tbPagoSolicitud>();
            string query = "SELECT PAGNUM as NumeroSolicitud, PAGNU1 as ComprobanteTransaccion, PAGFEC as FechaComprobanteTransaccion, PAGFOR as FormaPago, PAGVAL as ValorPagar, PAGRU1 as Ruc, PAGTIP as TipoComprobante, "
                        + " PAGEST as Establecimiento, PAGPUN as PuntoEmision, PAGNU2 as NumeroComprobante, PAGNU3 as NumeroAutorizacion, PAGUSU AS UsuarioCreado, PAGFE1 AS FechaCreado, PAGHOR as HoraCreado, PAGUS1 AS UsuarioModificado, PAGFE2 AS FechaModificado, PAGHO1 as HoraModificado "
                        + " FROM PAGARC WHERE PAGNUM = " + numSolicitud;
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbPagoSolicitud oPagoSol = new tbPagoSolicitud();
                        oPagoSol.NumeroSolicitud = decimal.Parse(dr["NumeroSolicitud"].ToString());
                        oPagoSol.ComprobanteTransaccion = dr["ComprobanteTransaccion"].ToString();
                        oPagoSol.FechaComprobanteTransaccion = dr["FechaComprobanteTransaccion"].ToString();
                        oPagoSol.FormaPago = dr["FormaPago"].ToString();
                        oPagoSol.ValorPagar = decimal.Parse(dr["ValorPagar"].ToString());
                        oPagoSol.Ruc = dr["Ruc"].ToString();
                        oPagoSol.TipoComprobante = dr["TipoComprobante"].ToString();
                        oPagoSol.Establecimiento = dr["Establecimiento"].ToString();
                        oPagoSol.PuntoEmision = dr["PuntoEmision"].ToString();
                        oPagoSol.NumeroComprobante = dr["NumeroComprobante"].ToString();
                        oPagoSol.NumeroAutorizacion = dr["NumeroAutorizacion"].ToString();
                        oPagoSol.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oPagoSol.FechaCreado = dr["FechaCreado"].ToString();
                        oPagoSol.HoraCreado = dr["HoraCreado"].ToString();
                        oPagoSol.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oPagoSol.FechaModificado = dr["FechaModificado"].ToString();
                        oPagoSol.HoraModificado = dr["HoraModificado"].ToString();
                        listarPagoSolicitud.Add(oPagoSol);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarPagoSolicitud = null;
            }
            return listarPagoSolicitud;
        }
        public bool RegistraPagoSolicitud(tbPagoSolicitud opagoS)
        {
            bool estado = false;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            string query = "INSERT INTO PAGARC(PAGNUM, PAGNU1, PAGFEC, PAGFOR, PAGVAL, PAGRU1, PAGTIP, PAGEST, PAGPUN, PAGNU2, PAGNU3, PAGUSU, PAGFE1, PAGHOR)" +
                " VALUES(@NumeroSolicitud, @ComprobanteTransaccion, @FechaComprobanteTransaccion, @FormaPago, @ValorPagar, @Ruc, @TipoComprobante, "
                + " @Establecimiento, @PuntoEmision, @NumeroComprobante, @NumeroAutorizacion, @UsuarioCreado, @FechaCreado, @HoraCreado)";

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = opagoS.NumeroSolicitud;
                    cmd.Parameters["@ComprobanteTransaccion"].Value = campoNull(opagoS.ComprobanteTransaccion);
                    cmd.Parameters["@FechaComprobanteTransaccion"].Value = campoNull(opagoS.FechaComprobanteTransaccion);
                    cmd.Parameters["@FormaPago"].Value = campoNull(opagoS.FormaPago);
                    cmd.Parameters["@ValorPagar"].Value = opagoS.ValorPagar.ToString().Replace(".", ",");
                    cmd.Parameters["@Ruc"].Value = campoNull(opagoS.Ruc);
                    cmd.Parameters["@TipoComprobante"].Value = campoNull(opagoS.TipoComprobante);
                    cmd.Parameters["@Establecimiento"].Value = campoNull(opagoS.Establecimiento);
                    cmd.Parameters["@PuntoEmision"].Value = campoNull(opagoS.PuntoEmision);
                    cmd.Parameters["@NumeroComprobante"].Value = campoNull(opagoS.NumeroComprobante);
                    cmd.Parameters["@NumeroAutorizacion"].Value = campoNull(opagoS.NumeroAutorizacion);
                    cmd.Parameters["@UsuarioCreado"].Value = opagoS.UsuarioCreado;
                    cmd.Parameters["@FechaCreado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraCreado"].Value = osistema.HoraSistema;
                    estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oConexion.Close();
                }
            }

            return estado;
        }

        public bool ActualizaPagoSolicitud(tbPagoSolicitud opagoS)
        {
            bool estado = false;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            string query = "UPDATE PAGARC "
                    + " SET PAGNU1 = @ComprobanteTransaccion, PAGFEC = @FechaComprobanteTransaccion, PAGFOR = @FormaPago, "
                    + " PAGVAL = @ValorPagar, PAGRU1 = @Ruc, PAGTIP = @TipoComprobante, PAGEST = @Establecimiento, PAGPUN = @PuntoEmision, "
                    + " PAGNU2 = @NumeroComprobante, PAGNU3 = @NumeroAutorizacion, PAGUSU = @UsuarioCreado, PAGFE1 = @FechaCreado, "
                    + " PAGHOR = @HoraCreado"
                    + " WHERE PAGNUM = @NumeroSolicitud";

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@ComprobanteTransaccion"].Value = campoNull(opagoS.ComprobanteTransaccion);
                    cmd.Parameters["@FechaComprobanteTransaccion"].Value = campoNull(opagoS.FechaComprobanteTransaccion);
                    cmd.Parameters["@FormaPago"].Value = campoNull(opagoS.FormaPago);
                    cmd.Parameters["@ValorPagar"].Value = opagoS.ValorPagar.ToString().Replace(".", ",");
                    cmd.Parameters["@Ruc"].Value = campoNull(opagoS.Ruc);
                    cmd.Parameters["@TipoComprobante"].Value = campoNull(opagoS.TipoComprobante);
                    cmd.Parameters["@Establecimiento"].Value = campoNull(opagoS.Establecimiento);
                    cmd.Parameters["@PuntoEmision"].Value = campoNull(opagoS.PuntoEmision);
                    cmd.Parameters["@NumeroComprobante"].Value = campoNull(opagoS.NumeroComprobante);
                    cmd.Parameters["@NumeroAutorizacion"].Value = campoNull(opagoS.NumeroAutorizacion);
                    cmd.Parameters["@UsuarioCreado"].Value = opagoS.UsuarioCreado;
                    cmd.Parameters["@FechaCreado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraCreado"].Value = osistema.HoraSistema;
                    cmd.Parameters["@NumeroSolicitud"].Value = opagoS.NumeroSolicitud;

                    estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oConexion.Close();
                }
            }

            return estado;
        }

        public bool ActualizaPagoSolicitudFactura(Int32 numSolicitud, string IDControlFr3, string numFactura, string rucFactura, string usuarioModifica)
        {
            bool estado = false;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            string query = "UPDATE PAGARC "
                    + " SET PAGRU1 = @IDControlFr3, PAGNU2 =  @numFactura, PAGRUC = @RucFactura,"
                    + " PAGUS1 = @UsuarioModifica, PAGFE2 = @FechaModifica, "
                    + " PAGHO1 = @HoraModifica"
                    + " WHERE PAGNUM = @NumeroSolicitud";

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@IDControlFr3"].Value = campoNull(IDControlFr3);
                    cmd.Parameters["@numFactura"].Value = campoNull(numFactura);
                    cmd.Parameters["@RucFactura"].Value = campoNull(rucFactura);
                    cmd.Parameters["@UsuarioModifica"].Value = usuarioModifica;
                    cmd.Parameters["@FechaModifica"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraModifica"].Value = osistema.HoraSistema;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;

                    estado = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oConexion.Close();
                }
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
