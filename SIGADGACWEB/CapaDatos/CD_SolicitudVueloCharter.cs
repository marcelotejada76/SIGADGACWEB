using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using CapaDatos;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_SolicitudVueloCharter
    {
        public static CD_SolicitudVueloCharter _instancia = null;
        //string biblioteca = "DGACDAT";
        DateTime fechaAs400 = DateTime.Now;
        private CD_SolicitudVueloCharter()
        {

        }

        public static CD_SolicitudVueloCharter Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_SolicitudVueloCharter();
                }
                return _instancia;
            }
        }


        /// <summary>
        /// Metodo obtine la solictud 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tbSolictudVuelo ObtieneSolicitudVueloPorId(int id)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INTEGER), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(rtrim(ltrim(SOLPRO)), '') AS PropositoVuelo, ifnull(rtrim(ltrim(SOLESP)), '') AS EspecificarVuelo, ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(rtrim(ltrim(f.CIATI1)), '') as TipoIdentificacionFacturar,");
            sb.Append(" ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIACO2)), '') AS CodigoIata, ifnull(rtrim(ltrim(c.CIACO3)), '') AS CodigoNumeroCia, ifnull(rtrim(ltrim(SOLNO2)), '') AS NombreCompaniaAviacion, ifnull(rtrim(ltrim(SOLDI1)), '') AS Direccion, ifnull(rtrim(ltrim(c.CIARUC)), '') AS RucCompania,");
            sb.Append(" ifnull(rtrim(ltrim(SOLCO2)), '') AS Email, ifnull(rtrim(ltrim(SOLTE1)), '') AS Telefono, ifnull(rtrim(ltrim(c.CIACEL)), '') AS Celular, ifnull(rtrim(ltrim(c.CIAREP)), '') AS RepresentanteLegal, ifnull(rtrim(ltrim(c.CIACO4)), '') AS CodigoPais,");
            sb.Append(" ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(SOLNU4)), '') as RucFacturar, ifnull(rtrim(ltrim(SOLNO3)), '') AS NombreFacturar, ifnull(rtrim(ltrim(SOLTE2)), '') AS TelefonoFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(SOLDI2)), '') AS DireccionFacturar, ifnull(rtrim(ltrim(SOLCO3)), '') AS CorreoFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNOM)), '') AS NombreFleteador, ifnull(rtrim(ltrim(SOLDIR)), '') AS DireccionFleteador, ifnull(rtrim(ltrim(SOLTEL)), '') AS TelefonoFleteador, ifnull(rtrim(ltrim(SOLCOR)), '') AS CorreoFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLPER)), '') AS PermisoOperacion, ifnull(rtrim(ltrim(SOLES1)), '') AS EspecificacionesOperacion,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLFOR' and VALVAL = SOLFOR), '') as DescripcionFormaPago, SOLCER AS CertificadoOperador, SOLCOM AS ComprobantePago, ifnull(rtrim(ltrim(SOLUS2)), '') AS UsuarioCreacion, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, (ifnull(rtrim(ltrim(SOLOB1)), '') || ifnull(rtrim(ltrim(SOLO04)), '')) AS ObservacionOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero, ifnull(rtrim(ltrim(SOLOB2)), '') AS ObservacionFinanciero, ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, (ifnull(rtrim(ltrim(SOLOB3)), '') || ifnull(rtrim(ltrim(SOLO07)), ''))  AS ObservacionTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLOB4)), '') AS ObservacionResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO, ifnull(rtrim(ltrim(SOLOBS)), '') AS Observacion,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO1)), '') AS NombreRepresentante, ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(rtrim(ltrim(SOLDOC)), '') AS DocumentoPersonal, ifnull(rtrim(ltrim(SOLACE)), '') as AceptarTerminos, ifnull(rtrim(ltrim(SOLCAM)), '') as VueloItinerario");
            sb.Append(" FROM SOLARC LEFT JOIN SOLAR2 ON(SOLNUM = SOLNU7) LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLNUM = " + id);
            query = sb.ToString();

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
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.PropositoVuelo = dr["PropositoVuelo"].ToString();
                        oSolicitudVuelo.EspecificarVuelo = dr["EspecificarVuelo"].ToString();
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.TipoIdentificacionFleteador = dr["TipoIdentificacionFacturar"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.TelefonoFactura = dr["TelefonoFacturar"].ToString();
                        oSolicitudVuelo.DireccionFactura = dr["DireccionFacturar"].ToString();
                        oSolicitudVuelo.CorreoFactura = dr["CorreoFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.TelefonoFleteador = dr["TelefonoFleteador"].ToString();
                        oSolicitudVuelo.DireccionFleteador = dr["DireccionFleteador"].ToString();
                        oSolicitudVuelo.CorreoFleteador = dr["CorreoFleteador"].ToString();
                        oSolicitudVuelo.PermisoOperacion = dr["PermisoOperacion"].ToString();
                        oSolicitudVuelo.EspecificacionesOperacion = dr["EspecificacionesOperacion"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.ComprobantePago = dr["ComprobantePago"].ToString();
                        oSolicitudVuelo.DescripcionFormaPago = dr["DescripcionFormaPago"].ToString();
                        oSolicitudVuelo.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.ObservacionOperacionesDSOP = dr["ObservacionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.ObservacionTransporteAereo = dr["ObservacionTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.ObservacionFinanciero = dr["ObservacionFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.ObservacionResponsableATO = dr["ObservacionResponsableATO"].ToString();
                        oSolicitudVuelo.Observacion = dr["Observacion"].ToString();
                        oSolicitudVuelo.NombreRepresentante = dr["NombreRepresentante"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = campoNull(dr["AutorizacionSolicitudVuelo"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.CodigoIata = dr["CodigoIata"].ToString();
                        oSolicitudVuelo.CodigoNumeroCia = dr["CodigoNumeroCia"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.Direccion = dr["Direccion"].ToString();
                        oSolicitudVuelo.RucCompania = dr["RucCompania"].ToString();
                        oSolicitudVuelo.Email = dr["Email"].ToString();
                        oSolicitudVuelo.Telefono = dr["Telefono"].ToString();
                        oSolicitudVuelo.Celular = dr["Celular"].ToString();
                        oSolicitudVuelo.RepresentanteLegal = dr["RepresentanteLegal"].ToString();
                        oSolicitudVuelo.CodigoPais = dr["CodigoPais"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.DocumentoPersonal = dr["DocumentoPersonal"].ToString();
                        oSolicitudVuelo.AceptarTerminos = checkBoxBool(dr["AceptarTerminos"].ToString());
                        oSolicitudVuelo.VueloItinerario = dr["VueloItinerario"].ToString();

                        oSolicitudVuelo.oDetalleAeronave = CD_DetalleAeronave.Instancia.ObtieneDetalleAeronavePorNumeroSolicitud(id);
                        oSolicitudVuelo.oDetalleRuta = CD_DetalleRuta.Instancia.ObtieneDetalleRutaPorNumeroSolicitud(id);
                        oSolicitudVuelo.oDetalleAdjunto = CD_DetalleAdjuntocs.Instancia.DetalleAdjuntoPorNumSolicitud(id);
                        //oSolicitudVuelo.obuyer = CD_Personal.Instancia.ObtienePersonaPorDocuento(oSolicitudVuelo.DocumentoPersonal);
                        oSolicitudVuelo.oPagoSolictud = CD_PagoSolicitud.Instancia.PagoSolicitudPorNumeroSolicitudListar(id);
                        oSolicitudVuelo.oCiaOperadora = CD_Compania.Instancia.CompaniaAviacionPorId(decimal.Parse(oSolicitudVuelo.IdCompaniaOperador));
                        oSolicitudVuelo.oCiaFacturar = CD_Compania.Instancia.CompaniaAviacionPorId(oSolicitudVuelo.IdFactura);

                        oSolicitudVuelo.oAdjuntoCiaAviacion = CD_AdjuntoCiaAviacion.Instancia.AdjuntoCiaAviacionListarPorIdCia(decimal.Parse(oSolicitudVuelo.IdCompaniaOperador));
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                oSolicitudVuelo = null;
            }
            return oSolicitudVuelo;
        }

        /// <summary>
        /// Metodo carga todos las solicitudes aprobados y por aprobar de la ciudad 
        /// </summary>
        /// <returns></returns>
        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleATO(tbUsuario osuario)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud,  ifnull(CAST(SOLNU1 AS INTEGER), 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion,");
            sb.Append(" ( SELECT LISTAGG(distinct rtrim(ltrim(DETRU3)), ' / ') WITHIN GROUP(ORDER BY DETRU3) AS RUTA FROM DETAR1 WHERE DETNU1 = SOLNUM GROUP BY DETNU1) AS RUTA");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST <> 'RE' AND SOLEST <> 'AN'");
            query = sb.ToString();
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

                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        if (oSolicitudVuelo.EstadoSolicitud.Equals("EN"))
                        {
                            oSolicitudVuelo.oDetalleRuta = CD_DetalleRuta.Instancia.ObtieneDetalleRutaPorNumeroSolicitud(oSolicitudVuelo.NumeroSolicitud);
                            if (oSolicitudVuelo.oDetalleRuta.Count > 0)
                            {
                                string[] arrayRuta = oSolicitudVuelo.oDetalleRuta[0].RutaVuelo.Split('-');
                                foreach (var item in arrayRuta)
                                {
                                    if (osuario.CodigoCiudad.Equals(item))
                                    {
                                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                                        oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                                        oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                                        listarSolicitud.Add(oSolicitudVuelo);
                                        break;
                                    }

                                }
                            }
                        }
                        else if (oSolicitudVuelo.EstadoSolicitud != "EN")
                        {
                            oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                            oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                            oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                            oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                            oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                            oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                            oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                            oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                            oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                            oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                            oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                            oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                            oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                            oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                            oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                            oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                            oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                            oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                            oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                            oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                            oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                            oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                            oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                            oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                            oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                            oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                            oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                            oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                            oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                            oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                            oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                            oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                            oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                            oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                            oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                            oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                            oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                            oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                            oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                            oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                            oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                            oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                            oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                            listarSolicitud.Add(oSolicitudVuelo);

                        }
                    }
                    dr.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return listarSolicitud;
        }

        /// <summary>
        /// Metodo carga todos las solicitudes aprobados y por aprobar de la ciudad 
        /// </summary>
        /// <returns></returns>
        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleATO(tbUsuario osuario, string fechaSistema, int restadia)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud,  ifnull(CAST(SOLNU1 AS INTEGER), 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion,");
            sb.Append(" ( SELECT LISTAGG(distinct rtrim(ltrim(DETRU3)), ' / ') WITHIN GROUP(ORDER BY DETRU3) AS RUTA FROM DETAR1 WHERE DETNU1 = SOLNUM GROUP BY DETNU1) AS RUTA");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST <> 'RE' AND SOLEST <> 'AN' AND SOLFE1 >= '" + FechaVueloResta(fechaSistema, -restadia) + "' AND SOLFE1 <= '" + fechaSistema + "'");
            query = sb.ToString();
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

                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        if (oSolicitudVuelo.EstadoSolicitud.Equals("EN"))
                        {
                            oSolicitudVuelo.oDetalleRuta = CD_DetalleRuta.Instancia.ObtieneDetalleRutaPorNumeroSolicitud(oSolicitudVuelo.NumeroSolicitud);
                            if (oSolicitudVuelo.oDetalleRuta.Count > 0)
                            {
                                string[] arrayRuta = oSolicitudVuelo.oDetalleRuta[0].RutaVuelo.Split('-');
                                foreach (var item in arrayRuta)
                                {
                                    if (osuario.CodigoCiudad.Equals(item))
                                    {
                                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                                        oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                                        oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                                        listarSolicitud.Add(oSolicitudVuelo);
                                        break;
                                    }

                                }
                            }
                        }
                        else if (oSolicitudVuelo.EstadoSolicitud != "EN")
                        {
                            oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                            oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                            oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                            oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                            oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                            oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                            oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                            oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                            oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                            oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                            oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                            oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                            oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                            oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                            oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                            oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                            oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                            oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                            oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                            oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                            oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                            oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                            oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                            oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                            oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                            oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                            oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                            oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                            oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                            oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                            oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                            oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                            oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                            oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                            oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                            oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                            oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                            oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                            oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                            oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                            oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                            oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                            oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                            listarSolicitud.Add(oSolicitudVuelo);

                        }
                    }
                    dr.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleATO(tbUsuario osuario, string fechaIncio, string fechaFinal)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud,  ifnull(CAST(SOLNU1 AS INTEGER), 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion,");
            sb.Append(" ( SELECT LISTAGG(distinct rtrim(ltrim(DETRU3)), ' / ') WITHIN GROUP(ORDER BY DETRU3) AS RUTA FROM DETAR1 WHERE DETNU1 = SOLNUM GROUP BY DETNU1) AS RUTA");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST <> 'RE' AND SOLEST <> 'AN' AND SOLFE1 >= '" + fechaIncio + "' AND SOLFE1 <= '" + fechaFinal + "'");
            query = sb.ToString();
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

                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        if (oSolicitudVuelo.EstadoSolicitud.Equals("EN"))
                        {
                            oSolicitudVuelo.oDetalleRuta = CD_DetalleRuta.Instancia.ObtieneDetalleRutaPorNumeroSolicitud(oSolicitudVuelo.NumeroSolicitud);
                            if (oSolicitudVuelo.oDetalleRuta.Count > 0)
                            {
                                string[] arrayRuta = oSolicitudVuelo.oDetalleRuta[0].RutaVuelo.Split('-');
                                foreach (var item in arrayRuta)
                                {
                                    if (osuario.CodigoCiudad.Equals(item))
                                    {
                                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                                        oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                                        oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                                        listarSolicitud.Add(oSolicitudVuelo);
                                        break;
                                    }

                                }
                            }
                        }
                        else if (oSolicitudVuelo.EstadoSolicitud != "EN")
                        {
                            oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                            oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                            oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                            oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                            oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                            oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                            oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                            oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                            oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                            oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                            oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                            oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                            oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                            oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                            oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                            oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                            oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                            oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                            oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                            oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                            oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                            oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                            oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                            oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                            oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                            oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                            oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                            oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                            oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                            oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                            oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                            oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                            oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                            oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                            oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                            oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                            oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                            oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                            oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                            oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                            oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                            oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                            oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                            listarSolicitud.Add(oSolicitudVuelo);

                        }
                    }
                    dr.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> SolicitudVueloCharterDetalle()
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(CAST(SOLNU1 AS INTEGER), 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO2)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(SOLNU4)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO3)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion,");
            sb.Append(" ( SELECT LISTAGG(distinct rtrim(ltrim(DETRU3)), ' / ') WITHIN GROUP(ORDER BY DETRU3) AS RUTA FROM DETAR1 WHERE DETNU1 = SOLNUM GROUP BY DETNU1) AS RUTA");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST <> 'RE' AND SOLEST <> 'AN'");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                        oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                        listarSolicitud.Add(oSolicitudVuelo);
                        //oSolicitudVuelo.oDetalleAeronave = CD_DetalleAeronave.Instancia.ObtieneDetalleAeronavePorNumeroSolicitud(oSolicitudVuelo.NumeroSolicitud);
                        //oSolicitudVuelo.oDetalleRuta = CD_DetalleRuta.Instancia.ObtieneDetalleRutaPorNumeroSolicitud(oSolicitudVuelo.NumeroSolicitud);
                        //oSolicitudVuelo.oDetalleAdjunto = CD_DetalleAdjuntocs.Instancia.DetalleAdjuntoPorNumSolicitud(oSolicitudVuelo.NumeroSolicitud);
                        //oSolicitudVuelo.obuyer = CD_Personal.Instancia.ObtienePersonaPorDocuento(oSolicitudVuelo.DocumentoPersonal);
                        //oSolicitudVuelo.oPagoSolictud = CD_PagoSolicitud.Instancia.PagoSolicitudPorNumeroSolicitudListar(oSolicitudVuelo.NumeroSolicitud);
                        //oSolicitudVuelo.oCiaOperadora = CD_Compania.Instancia.CompaniaAviacionPorId(decimal.Parse(oSolicitudVuelo.IdCompaniaOperador));
                        //oSolicitudVuelo.oCiaFacturar = CD_Compania.Instancia.CompaniaAviacionPorId(oSolicitudVuelo.IdFactura);
                        //oSolicitudVuelo.oAdjuntoCiaAviacion = CD_AdjuntoCiaAviacion.Instancia.AdjuntoCiaAviacionListarPorIdCia(oSolicitudVuelo.IdFactura);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> SolicitudVueloCharterDetalle(string fechaSolicitud, int restaDia)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(CAST(SOLNU1 AS INTEGER), 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO2)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(SOLNU4)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO3)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion,");
            sb.Append(" ( SELECT LISTAGG(distinct rtrim(ltrim(DETRU3)), ' / ') WITHIN GROUP(ORDER BY DETRU3) AS RUTA FROM DETAR1 WHERE DETNU1 = SOLNUM GROUP BY DETNU1) AS RUTA");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST <> 'RE' AND SOLEST <> 'AN' AND SOLFE1 >= '" + FechaVueloResta(fechaSolicitud, -restaDia) + "' AND SOLFE1 <= '" + fechaSolicitud + "'");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                        oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                        listarSolicitud.Add(oSolicitudVuelo);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        /// <summary>
        /// Metodo carga los datos de solicitudes filtrado por fecha  
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<tbSolictudVuelo> SolicitudVueloCharterDetallePorFechas(string fechaInicio, string fechaFinal)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(CAST(SOLNU1 AS INTEGER), 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO2)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(SOLNU4)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(SOLNO3)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion,");
            sb.Append(" ( SELECT LISTAGG(distinct rtrim(ltrim(DETRU3)), ' / ') WITHIN GROUP(ORDER BY DETRU3) AS RUTA FROM DETAR1 WHERE DETNU1 = SOLNUM GROUP BY DETNU1) AS RUTA");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST <> 'RE' AND SOLEST <> 'AN' AND SOLFE1 >= '" + fechaInicio + "' AND SOLFE1 <= '" + fechaFinal + "'");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = Int32.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = Int32.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());
                        oSolicitudVuelo.CboAeropuertoAterrizaje = dr["RUTA"].ToString();
                        listarSolicitud.Add(oSolicitudVuelo);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }


        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleDirector()
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLES2 = 'AP' AND SOLES4 = 'AP' AND SOLES3 = 'AP' AND SOLES5 = ''");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        /// <summary>
        /// carga los datos autoriza por Usuario
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleDirector(string codUsuario)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLES2 = 'AP' AND SOLES4 = 'AP' AND SOLES3 = 'AP' AND  (SOLES5 = '' OR SOLUS5 = '" + codUsuario.Trim() + "')");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleConsulta(string codUsuario, string estado)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLES2 = 'AP' AND SOLES4 = 'AP' AND SOLES3 = 'AP' AND SOLES5 = '" + estado + "' ");
            sb.Append(" UNION ");
            sb.Append(" SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2) LEFT JOIN USUAR1 ua on(SOLUS4 = ua.USUCO8)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLES3 = 'NE' AND SOLES5 = '' AND USUCO9 = 'SEQU' ");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> SolicitudVueloCharterDetalleDirector(string codUsuario, string estado)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLES2 = 'AP' AND SOLES4 = 'AP' AND SOLES3 = 'AP' AND SOLES5 = '" + estado + "' ");
            sb.Append(" UNION ");
            sb.Append(" SELECT ifnull(CAST(SOLNUM AS INT), 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2) LEFT JOIN USUAR1 ua on(SOLUS4 = ua.USUCO8)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLES3 = 'NE' AND SOLES5 = '' AND USUCO9 = 'SEQU' ");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);

                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> ObtieneSolicitudVueloCharterPorUsuarioCreado(string oruc, string usuarioCreado)
        {
            List<tbSolictudVuelo> lstSolicitudVuelo = new List<tbSolictudVuelo>();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT SOLNUM AS NumeroSolicitud, SOLCO1 AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" SOLPRO AS PropositoVuelo, SOLESP AS EspecificarVuelo, ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOI1, 0) as IdFactura, ifnull(rtrim(ltrim(SOLNO3)), '') AS NombreFactura, ifnull(rtrim(ltrim(SOLNOM)), '') as NombreFletador, ");
            sb.Append(" ifnull(rtrim(ltrim(SOLPER)), '') AS PermisoOperacion, ifnull(rtrim(ltrim(SOLES1)), '') AS EspecificacionesOperacion, ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago,  ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLFOR' and VALVAL = SOLFOR), '') as DescripcionFormaPago,  ifnull(rtrim(ltrim(SOLCOM)), '') AS ComprobantePago, ");
            sb.Append(" ifnull(rtrim(ltrim(SOLUS2)), '') AS UsuarioCreacion, case when SOLFE1 is null then '' when SOLFE1 ='' then '' else SUBSTR(SOLFE1,7,2) ||'/'|| SUBSTR(SOLFE1,5,2) ||'/'|| SUBSTR(SOLFE1,1,4) ||' '|| SOLHOR  end as FechaEnvioSolicitud, ");
            sb.Append(" SOLEST AS EstadoSolicitud,  (select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLEST' and VALVAL = SOLEST) as Descripcionsolicitud,  ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP,  ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP, ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLOB1)), '') AS ObservacionOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP,  ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero, ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero, ifnull(rtrim(ltrim(SOLOB2)), '') AS ObservacionFinanciero, ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,  ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLOB3)), '') AS ObservacionTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo, ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO, ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO, ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,ifnull(rtrim(ltrim(SOLOBS)), '') AS Observacion, ifnull(rtrim(ltrim(SOLNO1)), '') AS NombreRepresentante,  ifnull(rtrim(ltrim(SOLAUT)), '') as AutorizacionSolicitudVuelo,");
            sb.Append(" SOLOID AS IdCompaniaOperador, ifnull(rtrim(ltrim(SOLNO2)), '') AS NombreCompaniaAviacion,  ifnull(rtrim(ltrim(SOLDIR)), '') AS DireccionResponsableContacto, ifnull(rtrim(ltrim(SOLTEL)), '') AS TelefonoResponsableContacto, ");
            sb.Append(" ifnull(rtrim(ltrim(SOLE01)), '') AS EstadoPago, ifnull((SELECT (SUBSTRING(DETFE3, 1, 4) ||'-'|| SUBSTRING(DETFE3, 5, 2) ||'-'|| SUBSTRING(DETFE3, 7, 2) ||'-'|| DETHO2) FROM DETAR1 WHERE DETNU1 = SOLNUM FETCH FIRST 1 ROWS ONLY) , '') as FechaIdaVuelo ");
            sb.Append(" FROM SOLARC");
            sb.Append(" WHERE SOLCO1 IN('02', '03', '04', '01')");
            sb.Append(" AND SOLEST <> 'AN' AND SOLUS2 = '" + usuarioCreado.ToUpper() + "'");

            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString().Trim();
                        oSolicitudVuelo.DescripcionSolicitud = campoNull(dr["DescripcionSolicitud"].ToString());
                        oSolicitudVuelo.PropositoVuelo = dr["PropositoVuelo"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFactura"].ToString());
                        oSolicitudVuelo.NombreFactura = dr["NombreFactura"].ToString();
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFletador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.DescripcionFormaPago = campoNull(dr["DescripcionFormaPago"].ToString());
                        oSolicitudVuelo.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.DescripcionSolicitud = campoNull(dr["Descripcionsolicitud"].ToString());
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = campoNull(dr["AutorizacionSolicitudVuelo"].ToString());
                        oSolicitudVuelo.EstadoPago = campoNull(dr["EstadoPago"].ToString());
                        oSolicitudVuelo.FechaIdaVuelo = campoNull(dr["FechaIdaVuelo"].ToString());
                        lstSolicitudVuelo.Add(oSolicitudVuelo);


                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                lstSolicitudVuelo = null;
            }
            return lstSolicitudVuelo;
        }



        public bool RegistraSolicitudVueloCharter(tbSolictudVuelo osolicitud)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            string rutaPlanVuelo = string.Empty;
            string matriculaAer = string.Empty;
            string modeloAer = string.Empty;
            string pesoAer = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    bool estadoExisteRegistro = false;
                    var sistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    do
                    {
                        osolicitud.NumeroSolicitud = GeneraNumeroSolicitud();
                        estadoExisteRegistro = SolicitudVueloExistePorNumeroSolitud(osolicitud.NumeroSolicitud);
                        if (estadoExisteRegistro == false)
                            break;


                    } while (estadoExisteRegistro == false);
                    osolicitud.Referencia = "TEST_" + osolicitud.FechaEnvioSolicitud + "_" + osolicitud.NumeroSolicitud.ToString();
                    osolicitud.FechaReferenca = (DateTime.Now.AddMinutes(10)).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
                    queryInsertar = "INSERT INTO SOLARC (SOLNUM, SOLCO1, SOLNU1, SOLPRO, SOLESP, SOLNU2, SOLOI1, SOLNU4, SOLNO3, SOLDI2, SOLTE2, SOLCO3, SOLPER, SOLES1, SOLFOR, SOLCOM, SOLUS2, SOLEST, SOLOBS, SOLNO1, SOLOID, SOLNO2, SOLDI1, SOLTE1, SOLCO2, SOLREF, SOLF07, SOLDOC, SOLSUB, SOLTOT, SOLACE, SOLNOM, SOLDIR, SOLTEL, SOLCOR, SOLCAM)" +
                        " VALUES(@NumeroSolicitud, @TipoSolictud, @NumeroVuelos, @PropositoVuelo, @EspecificarVuelo, @NumeroPasajeros, @IdFactura, @RucFactura, @NombreFactura, @DireccionFactura, @TelefonoFactura, @CorreoFactura, @PermisoOperacion, @EspecificacionesOperacion,"
                        + " @FormaPago, @ComprobantePago, @UsuarioCreacion, @EstadoSolicitud, @Observacion, @NombreRepresentante, @IdCompaniaOperador, @NombreCompaniaAviacion, @Direccion, @Telefono, @Email, @Referencia, @FechaReferencia, @DocumentoPersonal, @CostoCharter, @TotalPagar, @AceptarTerminos,"
                        + " @NombreFleteador, @DireccionFleteador, @TelefonoFleteador, @CorreoFleteador, @VueloItinerario)";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = osolicitud.NumeroSolicitud;
                    cmd.Parameters["@TipoSolictud"].Value = campoNull(osolicitud.TipoSolictud);
                    cmd.Parameters["@NumeroVuelos"].Value = osolicitud.NumeroVuelos;
                    cmd.Parameters["@PropositoVuelo"].Value = campoNull(osolicitud.PropositoVuelo.ToUpper());
                    cmd.Parameters["@EspecificarVuelo"].Value = campoNull(osolicitud.EspecificarVuelo);
                    cmd.Parameters["@NumeroPasajeros"].Value = osolicitud.NumeroPasajeros;
                    cmd.Parameters["@IdFactura"].Value = osolicitud.oCiaFacturar.OidP5;
                    cmd.Parameters["@RucFactura"].Value = osolicitud.oCiaFacturar.RucCompania;
                    cmd.Parameters["@NombreFactura"].Value = campoNull(osolicitud.oCiaFacturar.NombreCompaniaAviacion);
                    cmd.Parameters["@DireccionFactura"].Value = campoNull(osolicitud.oCiaFacturar.Direccion);
                    cmd.Parameters["@TelefonoFactura"].Value = campoNull(osolicitud.oCiaFacturar.Telefono);
                    cmd.Parameters["@CorreoFactura"].Value = campoNull(osolicitud.oCiaFacturar.Email);
                    cmd.Parameters["@PermisoOperacion"].Value = campoNull(osolicitud.PermisoOperacion);
                    cmd.Parameters["@EspecificacionesOperacion"].Value = campoNull(osolicitud.EspecificacionesOperacion);
                    cmd.Parameters["@FormaPago"].Value = campoNull(osolicitud.FormaPago);
                    cmd.Parameters["@ComprobantePago"].Value = campoNull(osolicitud.ComprobantePago);
                    cmd.Parameters["@UsuarioCreacion"].Value = campoNull(osolicitud.UsuarioCreacion);
                    cmd.Parameters["@EstadoSolicitud"].Value = campoNull(osolicitud.EstadoSolicitud);
                    cmd.Parameters["@Observacion"].Value = campoNull(osolicitud.Observacion);
                    cmd.Parameters["@NombreRepresentante"].Value = campoNull(osolicitud.NombreRepresentante);
                    cmd.Parameters["@IdCompaniaOperador"].Value = osolicitud.oCiaOperadora.OidP5;
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(osolicitud.oCiaOperadora.NombreCompaniaAviacion);
                    cmd.Parameters["@Direccion"].Value = campoNull(osolicitud.oCiaOperadora.Direccion);
                    cmd.Parameters["@Telefono"].Value = campoNull(osolicitud.oCiaOperadora.Telefono);
                    cmd.Parameters["@Email"].Value = campoNull(osolicitud.oCiaOperadora.Email);
                    cmd.Parameters["@Referencia"].Value = campoNull(osolicitud.Referencia);
                    cmd.Parameters["@FechaReferencia"].Value = campoNull(osolicitud.FechaReferenca);
                    cmd.Parameters["@DocumentoPersonal"].Value = "";
                    cmd.Parameters["@CostoCharter"].Value = osolicitud.CostoCharter.ToString().Replace(",", ".");
                    cmd.Parameters["@TotalPagar"].Value = osolicitud.TotalPagar.ToString().Replace(",", ".");
                    cmd.Parameters["@AceptarTerminos"].Value = checkBoxString(osolicitud.AceptarTerminos);
                    cmd.Parameters["@NombreFleteador"].Value = campoNull(osolicitud.NombreFleteador.ToUpper());
                    cmd.Parameters["@DireccionFleteador"].Value = campoNull(osolicitud.DireccionFleteador.ToUpper());
                    cmd.Parameters["@TelefonoFleteador"].Value = campoNull(osolicitud.TelefonoFleteador);
                    cmd.Parameters["@CorreoFleteador"].Value = campoNull(osolicitud.CorreoFleteador);
                    cmd.Parameters["@VueloItinerario"].Value = campoNull(osolicitud.VueloItinerario);
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        if (osolicitud.oDetalleAeronave.Count() > 0)
                        {
                            foreach (var rowA in osolicitud.oDetalleAeronave)
                            {
                                rowA.NumeroSolicitud = osolicitud.NumeroSolicitud;
                                rowA.UsuarioCreado = osolicitud.UsuarioCreacion;
                                rowA.CodigoOaciCiaAvc = ""; // solictudVuelo.CodigoOaci.ToUpper();
                                matriculaAer = rowA.Matricula;
                                modeloAer = rowA.Modelo;
                                pesoAer = rowA.PesoWTOKG;
                                rowA.FechaVigenciaSeguro = fechaAS400(osolicitud.VigenciaSeguroAeronave);
                            }

                            if (CD_DetalleAeronave.Instancia.RegistrarDetalleAeronaveCharter(osolicitud.oDetalleAeronave) == "200")
                            {
                                // int filaDetalle = 1;
                                foreach (var rowR in osolicitud.oDetalleRuta)
                                {
                                    rowR.NumeroSolicitud = osolicitud.NumeroSolicitud;
                                    rowR.UsuarioCreacion = osolicitud.UsuarioCreacion;
                                    rowR.FechaIdaVuelo = fechaAS400(rowR.FechaIdaVuelo);
                                    rowR.FechaRetornoVuelo = fechaAS400(rowR.FechaRetornoVuelo);
                                    rowR.DerechoSolicitado = campoNull(rowR.DerechoSolicitado);
                                }

                                if (CD_DetalleRuta.Instancia.RegistrarDetalleRuta(osolicitud.oDetalleRuta))
                                {
                                    //if (osolicitud.obuyer.Document != null)
                                       // CD_Personal.Instancia.RegistraPersona(osolicitud.obuyer, osolicitud.UsuarioCreacion);

                                    if (osolicitud.NumeroComprobante != null)
                                    {
                                        tbPagoSolicitud opagoSol = new tbPagoSolicitud(osolicitud.NumeroSolicitud, osolicitud.NumeroComprobante, fechaAS400(osolicitud.FechaTransaccion), osolicitud.FormaPago,
                                                                               osolicitud.TotalPagar, osolicitud.RucFleteador, osolicitud.BancoPago, "", "", "", "", osolicitud.UsuarioCreacion, "", "", "", "", "");

                                       // CD_PagoSolicitud.Instancia.RegistraPagoSolicitud(opagoSol);
                                    }

                                    respuesta = true;
                                }
                                else
                                {
                                    EliminarSolictud(osolicitud.NumeroSolicitud);
                                    CD_DetalleAeronave.Instancia.EliminaDetalleAeronavePorSolicitud(osolicitud.NumeroSolicitud);
                                    osolicitud.NumeroSolicitud = 0;
                                }

                            }
                            else
                            {
                                EliminarSolictud(osolicitud.NumeroSolicitud);
                                osolicitud.NumeroSolicitud = 0;
                            }
                        }
                    }
                    else
                    {
                        osolicitud.NumeroSolicitud = 0;
                    }

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool AutualizaSolicitudVueloCharter(tbSolictudVuelo osolicitud)
        {
            bool respuesta = true;
            string query = string.Empty;
            string rutaPlanVuelo = string.Empty;
            string matriculaAer = string.Empty;
            string modeloAer = string.Empty;
            string pesoAer = string.Empty;
            int indexAer = 0;
            int indexRuta = 0;
            StringBuilder sb = new StringBuilder();
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {

                    var sistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                    sb.Append(" UPDATE SOLARC SET SOLCO1 = @TipoSolictud, SOLNU1 = @NumeroVuelos, SOLPRO = @PropositoVuelo, ");
                    sb.Append(" SOLESP = @EspecificarVuelo,SOLNU2 = @NumeroPasajeros, SOLNO3 = @NombreFactura, SOLDI2 = @DireccionFactura, ");
                    sb.Append(" SOLTE2 = @TelefonoFactura, SOLCO3 = @CorreoFactura, SOLPER = @PermisoOperacion, SOLES1 = @EspecificacionesOperacion, ");
                    sb.Append(" SOLFOR = @FormaPago, SOLCOM = @ComprobantePago, SOLOBS = @Observacion, ");
                    sb.Append(" SOLNO1 = @NombreRepresentante, SOLOID = @IdCompaniaOperador, SOLNO2 = @NombreCompaniaAviacion, SOLDI1 = @Direccion,  ");
                    sb.Append(" SOLTE1 = @Telefono,SOLCO2 = @Email, SOLSUB = @CostoCharter, ");
                    sb.Append(" SOLTOT = @TotalPagar, SOLNOM = @NombreFleteador, SOLDIR = @DireccionFleteador, SOLTEL = @TelefonoFleteador, SOLCOR = @CorreoFleteador, ");
                    sb.Append(" SOLUS3 = @UsuarioSolictudModificacion, SOLFE4 = @FechaSolictudModificacion, SOLHO3 = @HoraSolictudModificacion, ");
                    sb.Append(" SOLES2 = @EstadoOperacionesDSOP, SOLES4 = @EstadoFinanciero, SOLES3 = @EstadoTransporteAereo, SOLES5 = @EstadoResponsableATO");
                    sb.Append(" WHERE SOLNUM = @NumeroSolicitud");
                    query = sb.ToString();

                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();

                    cmd.Parameters["@TipoSolictud"].Value = campoNull(osolicitud.TipoSolictud);
                    cmd.Parameters["@NumeroVuelos"].Value = osolicitud.NumeroVuelos;
                    cmd.Parameters["@PropositoVuelo"].Value = campoNull(osolicitud.PropositoVuelo.ToUpper());
                    cmd.Parameters["@EspecificarVuelo"].Value = campoNull(osolicitud.EspecificarVuelo);
                    cmd.Parameters["@NumeroPasajeros"].Value = osolicitud.NumeroPasajeros;
                    cmd.Parameters["@NombreFactura"].Value = campoNull(osolicitud.NombreFactura);
                    cmd.Parameters["@DireccionFactura"].Value = campoNull(osolicitud.DireccionFactura);
                    cmd.Parameters["@TelefonoFactura"].Value = campoNull(osolicitud.TelefonoFactura);
                    cmd.Parameters["@CorreoFactura"].Value = campoNull(osolicitud.CorreoFactura);
                    cmd.Parameters["@PermisoOperacion"].Value = campoNull(osolicitud.PermisoOperacion);
                    cmd.Parameters["@EspecificacionesOperacion"].Value = campoNull(osolicitud.EspecificacionesOperacion);
                    cmd.Parameters["@FormaPago"].Value = campoNull(osolicitud.FormaPago);
                    cmd.Parameters["@ComprobantePago"].Value = campoNull(osolicitud.ComprobantePago);
                    cmd.Parameters["@Observacion"].Value = campoNull(osolicitud.Observacion);
                    cmd.Parameters["@NombreRepresentante"].Value = campoNull(osolicitud.NombreRepresentante);
                    cmd.Parameters["@IdCompaniaOperador"].Value = osolicitud.oCiaOperadora.OidP5;
                    cmd.Parameters["@NombreCompaniaAviacion"].Value = campoNull(osolicitud.oCiaOperadora.NombreCompaniaAviacion);
                    cmd.Parameters["@Direccion"].Value = campoNull(osolicitud.Direccion);
                    cmd.Parameters["@Telefono"].Value = campoNull(osolicitud.Telefono);
                    cmd.Parameters["@Email"].Value = campoNull(osolicitud.Email);
                    cmd.Parameters["@CostoCharter"].Value = osolicitud.CostoCharter.ToString().Replace(",", ".");
                    cmd.Parameters["@TotalPagar"].Value = osolicitud.TotalPagar.ToString().Replace(",", ".");
                    cmd.Parameters["@NombreFleteador"].Value = campoNull(osolicitud.NombreFleteador);
                    cmd.Parameters["@DireccionFleteador"].Value = campoNull(osolicitud.DireccionFleteador);
                    cmd.Parameters["@TelefonoFleteador"].Value = campoNull(osolicitud.TelefonoFleteador);
                    cmd.Parameters["@CorreoFleteador"].Value = campoNull(osolicitud.CorreoFleteador);
                    cmd.Parameters["@UsuarioSolictudModificacion"].Value = campoNull(osolicitud.UsuarioSolictudModificacion);
                    cmd.Parameters["@FechaSolictudModificacion"].Value = campoNull(osolicitud.FechaSolictudModificacion);
                    cmd.Parameters["@HoraSolictudModificacion"].Value = campoNull(osolicitud.HoraSolictudModificacion);
                    cmd.Parameters["@EstadoOperacionesDSOP"].Value = campoNull(osolicitud.EstadoOperacionesDSOP);
                    cmd.Parameters["@EstadoFinanciero"].Value = campoNull(osolicitud.EstadoFinanciero);
                    cmd.Parameters["@EstadoTransporteAereo"].Value = campoNull(osolicitud.EstadoTransporteAereo);
                    cmd.Parameters["@EstadoResponsableATO"].Value = campoNull(osolicitud.EstadoResponsableATO);
                    cmd.Parameters["@NumeroSolicitud"].Value = osolicitud.NumeroSolicitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        if (osolicitud.oDetalleAeronave.Count() > 0)
                        {
                            CD_DetalleAeronave.Instancia.EliminaDetalleAeronavePorSolicitud(osolicitud.NumeroSolicitud);
                            foreach (var rowA in osolicitud.oDetalleAeronave)
                            {
                                rowA.NumeroSolicitud = osolicitud.NumeroSolicitud;
                                rowA.UsuarioCreado = osolicitud.UsuarioCreacion;
                                rowA.CodigoOaciCiaAvc = ""; // solictudVuelo.CodigoOaci.ToUpper();
                                matriculaAer = rowA.Matricula;
                                modeloAer = rowA.Modelo;
                                pesoAer = rowA.PesoWTOKG;
                                rowA.FechaVigenciaSeguro = fechaAS400(osolicitud.VigenciaSeguroAeronave);
                                rowA.UsuarioCreado = osolicitud.UsuarioSolictudModificacion;
                            }

                            if (CD_DetalleAeronave.Instancia.RegistrarDetalleAeronaveCharter(osolicitud.oDetalleAeronave) == "200")
                            {
                                foreach (var rowR in osolicitud.oDetalleRuta)
                                {
                                    rowR.NumeroSolicitud = osolicitud.NumeroSolicitud;
                                    rowR.UsuarioCreacion = osolicitud.UsuarioCreacion;
                                    rowR.FechaIdaVuelo = fechaAS400(rowR.FechaIdaVuelo);
                                    rowR.FechaRetornoVuelo = fechaAS400(rowR.FechaRetornoVuelo);
                                    rowR.DerechoSolicitado = campoNull(rowR.DerechoSolicitado);
                                    rowR.UsuarioCreacion = osolicitud.UsuarioSolictudModificacion;
                                }

                                if (CD_DetalleRuta.Instancia.RegistrarDetalleRuta(osolicitud.oDetalleRuta))
                                {
                                    if (osolicitud.NumeroComprobante != null)
                                    {
                                        tbPagoSolicitud opagoSol = new tbPagoSolicitud(osolicitud.NumeroSolicitud, osolicitud.NumeroComprobante, fechaAS400(osolicitud.FechaTransaccion), osolicitud.FormaPago,
                                                                               osolicitud.TotalPagar, osolicitud.RucFactura, osolicitud.BancoPago, "", "", "", "", osolicitud.UsuarioSolictudModificacion, "", "", "", "", "");
                                        CD_PagoSolicitud.Instancia.ActualizaPagoSolicitud(opagoSol);

                                        //Graba los adjuntos de Cia Aviacion
                                        //if (osolicitud.oAdjuntoCiaAviacion.Count > 0)
                                        //{
                                        //    CD_AdjuntoCiaAviacion.Instancia.AdjuntoCiaAviacionEliminar(decimal.Parse(osolicitud.oCiaOperadora.OidP5));
                                        //    foreach (var itemAdj in osolicitud.oAdjuntoCiaAviacion)
                                        //        itemAdj.OidP5 = decimal.Parse(osolicitud.oCiaOperadora.OidP5);

                                        //    CD_AdjuntoCiaAviacion.Instancia.AdjuntoCiaAviacionNuevo(osolicitud.oAdjuntoCiaAviacion);

                                        //}
                                        respuesta = true;
                                    }
                                }
                            }


                        }

                    }
                    else
                    {
                        osolicitud.NumeroSolicitud = 0;
                    }

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public bool ActualizaSolicitudCharterPago(decimal numSolicitud)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            iDB2Command cmd;

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE SOLARC SET SOLEST = 'EN', SOLE01 = 'PEN', SOLFE1 = @fecha, SOLHOR = @hora WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@fecha"].Value = osistema.FechaSistema;
                    cmd.Parameters["@hora"].Value = osistema.HoraSistema;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }

        public bool ModificaSolicitudVueloCharterAutorizada(tbSolictudVuelo osolicitud)
        {
            bool respuesta = true;
            string query = string.Empty;
            string rutaPlanVuelo = string.Empty;
            string matriculaAer = string.Empty;
            string modeloAer = string.Empty;
            string pesoAer = string.Empty;

            StringBuilder sb = new StringBuilder();
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {

                    var sistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                    sb.Append(" UPDATE SOLARC SET SOLEST = @EstadoSolicitud, SOLES3 = @EstadoTransporteAereo, SOLES5 = @EstadoResponsableATO, ");
                    sb.Append("SOLCA3 = @AsuntoModificacion, SOLCA4 = @ObservacionModificacion, ");
                    sb.Append(" SOLUS3 = @UsuarioSolictudModificacion, SOLFE4 = @FechaSolictudModificacion, SOLHO3 = @HoraSolictudModificacion ");
                    sb.Append(" WHERE SOLNUM = @NumeroSolicitud");
                    query = sb.ToString();

                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();

                    cmd.Parameters["@EstadoSolicitud"].Value = campoNull(osolicitud.EstadoSolicitud);
                    cmd.Parameters["@EstadoTransporteAereo"].Value = campoNull(osolicitud.EstadoTransporteAereo);
                    cmd.Parameters["@EstadoResponsableATO"].Value = campoNull(osolicitud.EstadoResponsableATO);
                    cmd.Parameters["@AsuntoModificacion"].Value = campoNull(osolicitud.AsuntoModificacion);
                    cmd.Parameters["@ObservacionModificacion"].Value = campoNull(osolicitud.ObservacionModificacion);
                    cmd.Parameters["@UsuarioSolictudModificacion"].Value = campoNull(osolicitud.UsuarioSolictudModificacion);
                    cmd.Parameters["@FechaSolictudModificacion"].Value = campoNull(osolicitud.FechaSolictudModificacion);
                    cmd.Parameters["@HoraSolictudModificacion"].Value = campoNull(osolicitud.HoraSolictudModificacion);
                    cmd.Parameters["@NumeroSolicitud"].Value = osolicitud.NumeroSolicitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        if (osolicitud.oDetalleAeronave.Count() > 0)
                        {
                            CD_DetalleAeronave.Instancia.EliminaDetalleAeronavePorSolicitud(osolicitud.NumeroSolicitud);
                            foreach (var rowA in osolicitud.oDetalleAeronave)
                            {
                                rowA.NumeroSolicitud = osolicitud.NumeroSolicitud;
                                rowA.UsuarioCreado = osolicitud.UsuarioCreacion;
                                rowA.CodigoOaciCiaAvc = ""; // solictudVuelo.CodigoOaci.ToUpper();
                                matriculaAer = rowA.Matricula;
                                modeloAer = rowA.Modelo;
                                pesoAer = rowA.PesoWTOKG;
                                rowA.FechaVigenciaSeguro = fechaAS400(osolicitud.VigenciaSeguroAeronave);
                                rowA.UsuarioCreado = osolicitud.UsuarioSolictudModificacion;
                            }

                            if (CD_DetalleAeronave.Instancia.RegistrarDetalleAeronaveCharter(osolicitud.oDetalleAeronave) == "200")
                            {
                                foreach (var rowR in osolicitud.oDetalleRuta)
                                {
                                    rowR.NumeroSolicitud = osolicitud.NumeroSolicitud;
                                    rowR.UsuarioCreacion = osolicitud.UsuarioCreacion;
                                    rowR.FechaIdaVuelo = fechaAS400(rowR.FechaIdaVuelo);
                                    rowR.FechaRetornoVuelo = fechaAS400(rowR.FechaRetornoVuelo);
                                    rowR.DerechoSolicitado = campoNull(rowR.DerechoSolicitado);
                                    rowR.UsuarioCreacion = osolicitud.UsuarioSolictudModificacion;
                                }

                                if (CD_DetalleRuta.Instancia.RegistrarDetalleRuta(osolicitud.oDetalleRuta))
                                {
                                    respuesta = true;
                                }
                            }


                        }

                    }
                    else
                    {
                        osolicitud.NumeroSolicitud = 0;
                    }

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ControlFR3RegistrarNuevo(int numSolicitud, tbUsuario osuario)
        {
            bool estado = false;
            NumLetra _numeroLetras = new NumLetra();
            var sistema = CD_Sistema.Instancia.GetFechaHoraSistema();
            tbControlFR3 oControlFr3 = new tbControlFR3();
            string rutaPlanVuelo = string.Empty;
            try
            {
                var osolicitud = ObtieneSolicitudVueloPorId(numSolicitud);
                if (osolicitud != null)
                {
                    var oUbicacionAtoUsuario = CD_UbicacionUsuario.Instancia.UbicacionAeropuertoUsuarioPorCiudad(osuario.CodigoCiudad);
                    var oUbivacionUcuario = CD_UbicacionUsuario.Instancia.UbicacionUsuarioPorCiudad(osuario.CodigoCiudad);
                    oControlFr3.Aeropuerto = osuario.CodigoCiudad;
                    oControlFr3.Anio = RetornoAnio(sistema.FechaSistema);
                    oControlFr3.FechaControlVuelo = sistema.FechaSistema;
                    oControlFr3.TipoOperacion = TipoOperacion(osolicitud.TipoSolictud);
                    oControlFr3.NumAterrizaPais = int.Parse(osolicitud.NumeroVuelos.ToString());
                    oControlFr3.SubTotal = 0;
                    oControlFr3.Total = 0; //osolicitud.TotalPagar;
                    oControlFr3.GranTotal = osolicitud.TotalPagar;
                    oControlFr3.GranTotalLetras = _numeroLetras.Convertir(osolicitud.TotalPagar.ToString(), true).ToUpper();
                    if (osolicitud.AutorizacionSolicitudVuelo.Trim().Length > 0)
                    {
                        string str = osolicitud.AutorizacionSolicitudVuelo.Trim();
                        int startIndex = 6;
                        int endIndex = str.Length - 6;
                        oControlFr3.Autorizacion = osolicitud.AutorizacionSolicitudVuelo.Trim().Substring(startIndex, endIndex);
                    }
                    else
                    {
                        oControlFr3.Autorizacion = ""; // osolicitud.oPagoSolictud[0].NumeroComprobante; //En el caso que no exista en num. autorizacion  
                    }
                    oControlFr3.Observacion = "VUELOS CHARTER" + ", C/PAGO: " + osolicitud.oPagoSolictud[0].ComprobanteTransaccion.Trim() + ", SOL: " + osolicitud.NumeroSolicitud.ToString();
                    oControlFr3.Estado = "S";
                    oControlFr3.Ruc = osolicitud.RucFactura;
                    oControlFr3.NombreCliente = osolicitud.NombreFactura;
                    oControlFr3.Direccion = osolicitud.DireccionFactura;
                    oControlFr3.Telefono = osolicitud.TelefonoFactura;
                    oControlFr3.Email = osolicitud.CorreoFactura;
                    oControlFr3.NacInter = EstadoNacInt(osolicitud.TipoSolictud);
                    oControlFr3.ValorCharter = osolicitud.TotalPagar;
                    oControlFr3.FormaPago = "02"; //Tansferencia / Deposito
                    oControlFr3.OidCiaAviacion = Convert.ToDecimal(osolicitud.IdCompaniaOperador);
                    oControlFr3.NombreCia = osolicitud.NombreCompaniaAviacion;
                    oControlFr3.CodigoOACICia = osolicitud.CodigoOaci;
                    if (osuario.CodigoCiudad.Equals("SEQU"))
                        oControlFr3.NombreAeropuerto = "DGAC-MATRIZ QUITO";
                    else
                        oControlFr3.NombreAeropuerto = oUbicacionAtoUsuario.Estacion;

                    oControlFr3.UsuarioCr = osuario.CodigoUsuario;
                    oControlFr3.EmailUsuarioDGAC = osuario.CorreoUsuario;
                    oControlFr3.OidUbicacionCliente = oUbivacionUcuario.OidUbicacion;
                    oControlFr3.OidUbicacion = decimal.Parse(osuario.CentroContable);
                    oControlFr3.IdAeropuerto = oUbicacionAtoUsuario.OidUbicacion;
                    oControlFr3.FechaRcepcion = osolicitud.FechaResponsableATO;

                    //commos de la forma de pago
                    oControlFr3.CodigoBanco = osolicitud.oPagoSolictud[0].TipoComprobante;
                    oControlFr3.Deposito = osolicitud.oPagoSolictud[0].ComprobanteTransaccion.Trim();


                    Int32 filaDetalle = 1;
                    Int32 filaDetalleAeron = 1;
                    foreach (var rowAer in osolicitud.oDetalleAeronave)
                    {
                        if (filaDetalleAeron == 1)
                        {
                            oControlFr3.Matricula = rowAer.Matricula;
                            oControlFr3.Callsign = rowAer.Matricula;
                            oControlFr3.Modelo = rowAer.Modelo;
                            oControlFr3.PesoMatricula = decimal.Parse(rowAer.PesoWTOKG) / 1000;
                        }
                        filaDetalleAeron++;
                    }

                    foreach (var rowR in osolicitud.oDetalleRuta)
                    {
                        oControlFr3.Origen = rowR.RutaOrigenInicio;
                        oControlFr3.Destino = rowR.RutaDestino;
                        oControlFr3.Retorno = rowR.RutaOrigenFinal;
                        if (filaDetalle == 1)
                        {
                            rutaPlanVuelo = rowR.RutaOrigenInicio + "/" + rowR.RutaDestino + "/" + rowR.RutaOrigenFinal;
                            break;
                        }
                    }
                    oControlFr3.RutaTotalPlanVlo = rutaPlanVuelo;

                    estado = CD_ControlFR3.Instancia.NuevoControlFR3Nuevo(oControlFr3, osolicitud);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return estado;
        }


        /// <summary>
        /// Actualiza la solictud dependiendo si el pago de la tarjetas se efectuo o no.
        /// </summary>
        /// <param name="numSolicitud"></param>
        /// <param name="usuarioMod"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public bool ActualizaSolicitudCharterSesiónPago(decimal numSolicitud, string usuarioMod, string estado)
        {
            bool status = false;
            string queryUpdate = string.Empty;
            iDB2Command cmd;

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE SOLARC SET SOLE01 = @estadoPago, SOLUS3= @usuarioMod, SOLFE4 = @fecha, SOLHO3 = @hora WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@estadoPago"].Value = estado;
                    cmd.Parameters["@usuarioMod"].Value = usuarioMod;
                    cmd.Parameters["@fecha"].Value = osistema.FechaSistema;
                    cmd.Parameters["@hora"].Value = osistema.HoraSistema;
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    status = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return status;
        }


        /// <summary>
        /// Metodo Total solicitudes enviadas
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesEnviadas()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('01','02','03','04') AND SOLEST = 'EN'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        /// <summary>
        /// Metodo Total Anuladas
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesAnuladas()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('01','02','03','04') AND SOLEST = 'NE'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        public decimal TotalSolicitudesPendientesPorAutorizar()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('01','02','03','04') AND SOLEST = 'AP' AND SOLES2 = 'AP' AND SOLES4 = 'AP' AND SOLES3 ='AP' AND SOLES5 = ''";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        /// <summary>
        /// Metodo Total Autorizadas
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesAutorizadas()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('01','02','03','04')  AND SOLEST = 'AP' AND SOLES5 = 'AP'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }


        /// <summary>
        /// Metodo Total de Especial Nacional
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesAutorizadasEspecialNacional()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('04') AND SOLEST = 'AP' AND SOLES5 = 'AP'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        /// <summary>
        /// Metodo Total de Especial Internacional
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesAutorizadasEspecialInternacional()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('01') AND SOLEST = 'AP' AND SOLES5 = 'AP'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        /// <summary>
        /// Metodo Total de Charter Domestico
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesAutorizadasCharterDomestico()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('02') AND SOLEST = 'AP' AND SOLES5 = 'AP'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        /// <summary>
        /// Metodo Total de Charter Internacional
        /// </summary>
        /// <returns></returns>
        public decimal TotalSolicitudesAutorizadasCharterInternacional()
        {
            string query = "SELECT COUNT(*) as Cantidad FROM SOLARC WHERE SOLCO1 IN('03') AND SOLEST = 'AP' AND SOLES5 = 'AP'";
            iDB2Command cmd;
            decimal TotalSolicitud = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TotalSolicitud = Int32.Parse(dr["Cantidad"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                TotalSolicitud = 0;
            }
            return TotalSolicitud;
        }

        /// <summary>
        /// Metodo obtiene todos los registros de solicitud nuevas enviadas para el proceso del tramite de laautorización o negación
        /// </summary>
        /// <param name="estadoSolicitud"></param>
        /// <returns></returns>
        public List<tbSolictudVuelo> ObtieneSolicitudVueloEnviadas(string estado)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(SOLNUM, 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST = '" + estado.Trim() + "'");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> ObtieneSolicitudesAprobadasPorTipoSolicitud(string tipoSolicitud)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(SOLNUM, 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 ='" + tipoSolicitud.Trim() + "' AND SOLES5 = 'AP'");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        public List<tbSolictudVuelo> ObtieneSolicitudesAprobadasPorAutorizarDirector(string estado)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            List<tbSolictudVuelo> listarSolicitud = new List<tbSolictudVuelo>();
            sb.Append("SELECT ifnull(SOLNUM, 0) AS NumeroSolicitud, ifnull(SOLCO1, '') AS TipoSolictud, ifnull((SELECT SERDES FROM SERARC WHERE SERCOD = SOLCO1), '') AS DescripcionTipoSolictud, ifnull(SOLNU1, 0) AS NumeroVuelos,");
            sb.Append(" ifnull(SOLNU2, 0) AS NumeroPasajeros, ifnull(SOLOID, 0) AS IdCompaniaOperador, ifnull(rtrim(ltrim(c.CIACOD)), '') AS CodigoOaciCompania,");
            sb.Append(" ifnull(rtrim(ltrim(c.CIANOM)), '') AS NombreCompaniaAviacion, ifnull(SOLOI1, 0) as IdFacturar, ifnull(rtrim(ltrim(f.CIARUC)), '') as RucFacturar,");
            sb.Append(" ifnull(rtrim(ltrim(f.CIANOM)), '') AS NombreFacturar, SOLNOM AS NombreFleteador,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFOR)), '') AS FormaPago, ifnull(rtrim(ltrim(SOLFE1)), '') AS FechaEnvioSolicitud,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHOR)), '') AS HoraEnvioSolicitud, ifnull(rtrim(ltrim(SOLEST)), '') AS EstadoSolicitud, ifnull(rtrim(ltrim(SOLUSU)), '') AS UsuarioOperacionesDSOP, ifnull(rtrim(ltrim(SOLFE2)), '') AS FechaOperacionesDSOP,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO1)), '') AS HoraOperacionesDSOP, ifnull(rtrim(ltrim(SOLES2)), '') AS EstadoOperacionesDSOP,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES2' and VALVAL = SOLES2), '') as DescripcionOperacionesDSOP, ifnull(rtrim(ltrim(SOLUS1)), '') AS UsuarioFinanciero, ifnull(rtrim(ltrim(SOLFE3)), '') AS FechaFinanciero,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO2)), '') AS HoraFinanciero,  ifnull(rtrim(ltrim(SOLES4)), '') AS EstadoFinanciero,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES4' and VALVAL = SOLES4), '') AS DescripcionEstadoFinanciero, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') AS FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') AS HoraTransporteAereo, ifnull(rtrim(ltrim(SOLES3)), '') AS EstadoTransporteAereo,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES3' and VALVAL = SOLES3), '') AS DescripcionEstadoTransporteAereo, ifnull(rtrim(ltrim(SOLUS5)), '') AS UsuarioResponsableATO, ifnull(rtrim(ltrim(SOLFEC)), '') AS FechaResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLHO5)), '') AS HoraResponsableATO, ifnull(rtrim(ltrim(SOLES5)), '') AS EstadoResponsableATO,");
            sb.Append(" ifnull((select VALDES FROM DGACSYS.TXDGAC where VALDDS = 'SOLES5' and VALVAL = SOLES5), '') AS DescripcionResponsableATO,");
            sb.Append(" ifnull(rtrim(ltrim(SOLAUT)), '') AS AutorizacionSolicitudVuelo, ifnull(rtrim(ltrim(SOLUS4)), '') AS UsuarioTransporteAereo,");
            sb.Append(" ifnull(rtrim(ltrim(SOLFE5)), '') as FechaTransporteAereo, ifnull(rtrim(ltrim(SOLHO4)), '') as HoraTransporteAereo, ifnull(SOLSUB, 0) AS CostoCharter, ifnull(SOLTOT, 0) AS TotalPagar, ifnull(SOLCA1, 0) AS EstadoAutorizacion");
            sb.Append(" FROM SOLARC LEFT JOIN CIAARC c ON(SOLOID = c.CIAOI2) LEFT JOIN CIAARC f ON(SOLOI1 = f.CIAOI2)");
            sb.Append(" WHERE SOLCO1 IN('01','02','03','04') AND SOLEST = '" + estado.Trim() + "'");
            query = sb.ToString();

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
                        tbSolictudVuelo oSolicitudVuelo = new tbSolictudVuelo();
                        oSolicitudVuelo.NumeroSolicitud = Int32.Parse(dr["NumeroSolicitud"].ToString());
                        oSolicitudVuelo.TipoSolictud = dr["TipoSolictud"].ToString();
                        oSolicitudVuelo.DescripcionTipoSolictud = dr["DescripcionTipoSolictud"].ToString();
                        oSolicitudVuelo.NumeroVuelos = decimal.Parse(dr["NumeroVuelos"].ToString());
                        oSolicitudVuelo.NumeroPasajeros = decimal.Parse(dr["NumeroPasajeros"].ToString());
                        oSolicitudVuelo.IdCompaniaOperador = dr["IdCompaniaOperador"].ToString();
                        oSolicitudVuelo.CodigoOaci = dr["CodigoOaciCompania"].ToString();
                        oSolicitudVuelo.NombreCompaniaAviacion = dr["NombreCompaniaAviacion"].ToString();
                        oSolicitudVuelo.IdFactura = decimal.Parse(dr["IdFacturar"].ToString());
                        oSolicitudVuelo.RucFactura = dr["RucFacturar"].ToString();
                        oSolicitudVuelo.NombreFactura = dr["NombreFacturar"].ToString();
                        oSolicitudVuelo.NombreFleteador = dr["NombreFleteador"].ToString();
                        oSolicitudVuelo.FormaPago = dr["FormaPago"].ToString();
                        oSolicitudVuelo.FechaEnvioSolicitud = dr["FechaEnvioSolicitud"].ToString();
                        oSolicitudVuelo.HoraEnvioSolicitud = dr["HoraEnvioSolicitud"].ToString();
                        oSolicitudVuelo.EstadoSolicitud = dr["EstadoSolicitud"].ToString();
                        oSolicitudVuelo.UsuarioOperacionesDSOP = dr["UsuarioOperacionesDSOP"].ToString();
                        oSolicitudVuelo.FechaOperacionesDSOP = dr["FechaOperacionesDSOP"].ToString();
                        oSolicitudVuelo.HoraOperacionesDSOP = dr["HoraOperacionesDSOP"].ToString();
                        oSolicitudVuelo.EstadoOperacionesDSOP = dr["EstadoOperacionesDSOP"].ToString();
                        oSolicitudVuelo.DescripcionOperacionesDSOP = dr["DescripcionOperacionesDSOP"].ToString();
                        oSolicitudVuelo.UsuarioFinanciero = dr["UsuarioFinanciero"].ToString();
                        oSolicitudVuelo.FechaFinanciero = dr["FechaFinanciero"].ToString();
                        oSolicitudVuelo.HoraFinanciero = dr["HoraFinanciero"].ToString();
                        oSolicitudVuelo.EstadoFinanciero = dr["EstadoFinanciero"].ToString();
                        oSolicitudVuelo.DescripcionEstadoFinanciero = dr["DescripcionEstadoFinanciero"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.HoraTransporteAereo = dr["HoraTransporteAereo"].ToString();
                        oSolicitudVuelo.EstadoTransporteAereo = dr["EstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.DescripcionEstadoTransporteAereo = dr["DescripcionEstadoTransporteAereo"].ToString();
                        oSolicitudVuelo.UsuarioResponsableATO = dr["UsuarioResponsableATO"].ToString();
                        oSolicitudVuelo.FechaResponsableATO = dr["FechaResponsableATO"].ToString();
                        oSolicitudVuelo.HoraResponsableATO = dr["HoraResponsableATO"].ToString();
                        oSolicitudVuelo.EstadoResponsableATO = dr["EstadoResponsableATO"].ToString();
                        oSolicitudVuelo.DescripcionResponsableATO = dr["DescripcionResponsableATO"].ToString();
                        oSolicitudVuelo.AutorizacionSolicitudVuelo = dr["AutorizacionSolicitudVuelo"].ToString();
                        oSolicitudVuelo.UsuarioTransporteAereo = dr["UsuarioTransporteAereo"].ToString();
                        oSolicitudVuelo.FechaTransporteAereo = dr["FechaTransporteAereo"].ToString();
                        oSolicitudVuelo.CostoCharter = decimal.Parse(dr["CostoCharter"].ToString());
                        oSolicitudVuelo.TotalPagar = decimal.Parse(dr["TotalPagar"].ToString());
                        oSolicitudVuelo.EstadoAutorizacion = decimal.Parse(dr["EstadoAutorizacion"].ToString());

                        listarSolicitud.Add(oSolicitudVuelo);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                listarSolicitud = null;
            }
            return listarSolicitud;
        }

        private Int32 GeneraNumeroSolicitud()
        {
            string query = "SELECT IFNULL(max(SOLNUM), 0) + 1 AS Secuencial FROM SOLARC";
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

        private bool SolicitudVueloExistePorNumeroSolitud(int numSolictud)
        {
            string query = "SELECT ifnull(SOLNUM, 0) AS NumeroSolicitud FROM SOLARC WHERE SOLNUM = " + numSolictud;
            iDB2Command cmd;
            bool estado = false;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (Int32.Parse(dr["NumeroSolicitud"].ToString()) > 0) ;
                        return estado = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }

        public bool EliminarSolictud(decimal numSolicitud)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "DELETE SOLARC  WHERE SOLNUM = @NumeroSolicitud";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@NumeroSolicitud"].Value = numSolicitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool AnularSolicitudCharter(Int32 id, string usuario, string fecha, string hora)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {

                    queryUpdate = "UPDATE SOLARC SET SOLEST = 'AN', SOLUS3 = @usuario, SOLFE4 = @fecha, SOLHO3 = @hora"
                    + " WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@usuario"].Value = usuario;
                    cmd.Parameters["@fecha"].Value = fecha;
                    cmd.Parameters["@hora"].Value = hora;
                    cmd.Parameters["@NumeroSolicitud"].Value = id;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solitud de vuelo la DSOP 
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudVueloOperaciones(Int32 IdSolitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            string observacion1 = string.Empty;
            string observacion2 = string.Empty;
            iDB2Command cmd;
            tbSistema osistema = new tbSistema();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    if (observacion.Length > 400)
                    {
                        int endIndex = (observacion.Length - 399);
                        observacion1 = observacion.Substring(0, 399);
                        observacion2 = observacion.Substring(399, endIndex);
                    }
                    else
                    {
                        observacion1 = observacion;
                        observacion2 = "";
                    }

                    queryUpdate = "UPDATE SOLARC SET SOLUSU = @UsuarioOperador , SOLFE2 = @FechaOperador, "
                        + "SOLHO1 = @HoraOperador, SOLOB1 = @ObservacionOperador, SOLES2 = @EstadoOperacion"
                    + " WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@UsuarioOperador"].Value = usuario;
                    cmd.Parameters["@FechaOperador"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraOperador"].Value = osistema.HoraSistema;
                    cmd.Parameters["@ObservacionOperador"].Value = observacion1;
                    cmd.Parameters["@EstadoOperacion"].Value = estado;
                    cmd.Parameters["@NumeroSolicitud"].Value = IdSolitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    DatosAdicionalSolicitudVuelo(IdSolitud, observacion2);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool DatosAdicionalSolicitudVuelo(Int32 IdSolitud, string observacion)
        {
            bool estado = false;
            try
            {
                if (VerificaDatosAdicionalSolicitud(IdSolitud))
                {
                    estado = ActualizaSolictudVuelosAdicional(IdSolitud, observacion);
                }
                else
                {
                    estado = NuevaSolictudVuelosAdicional(IdSolitud, observacion);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }

        public bool DatosAdicionalSolicitudVuelotransporteAereo(Int32 IdSolitud, string observacion)
        {
            bool estado = false;
            try
            {
                if (VerificaDatosAdicionalSolicitud(IdSolitud))
                {
                    estado = ActualizaSolictudVuelosAdicionalTranspoteAereo(IdSolitud, observacion);
                }
                else
                {
                    estado = NuevaSolictudVuelosAdicionalTranspoteAereo(IdSolitud, observacion);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }

        public bool NuevaSolictudVuelosAdicional(Int32 IdSolitud, string observacion)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "INSERT INTO SOLAR2(SOLNU7, SOLO04) VALUES(" + IdSolitud + ", '" + observacion + "')";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizaSolictudVuelosAdicional(Int32 IdSolitud, string observacion)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "UPDATE SOLAR2 SET SOLO04 = '" + observacion + "' WHERE SOLNU7 =" + IdSolitud;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool NuevaSolictudVuelosAdicionalTranspoteAereo(Int32 IdSolitud, string observacion)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "INSERT INTO SOLAR2(SOLNU7, SOLO07) VALUES(" + IdSolitud + ", '" + observacion + "')";
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizaSolictudVuelosAdicionalTranspoteAereo(Int32 IdSolitud, string observacion)
        {
            bool respuesta = true;
            iDB2Command cmd;
            string query = "UPDATE SOLAR2 SET SOLO07 = '" + observacion + "' WHERE SOLNU7 =" + IdSolitud;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool VerificaDatosAdicionalSolicitud(Int32 IdSolitud)
        {
            bool estado = false;
            string query = "SELECT * FROM SOLAR2 WHERE SOLNU7 = " + IdSolitud;
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
                        return estado = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }

        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solitud de vuelo- Transpporte Aereo
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudVueloTransporteAereo(Int32 IdSolitud, string tipoSolicitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            string observacion1 = string.Empty;
            string observacion2 = string.Empty;
            iDB2Command cmd;
            string numeroAutorizacion = string.Empty;
            tbSistema osistema = new tbSistema();
            tbServiciosDgac oservicio = new tbServiciosDgac();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    if (estado.Equals("AP") || estado.Equals("NE"))
                    {
                        oservicio = CD_Servicios.Instancia.ObstenerAutorizacion(tipoSolicitud);
                        if (ValidaExisteAutorizaciónSolicitudCharter(tipoSolicitud, oservicio.NomenclaturaServicio))
                        {
                            var servicioAut = CD_Servicios.Instancia.ObtenerServicio(tipoSolicitud);
                            do
                            {
                                oservicio.SecuencialServicio++;
                                oservicio.NomenclaturaServicio = servicioAut.NomenclaturaServicio + FiltraCaracteresInicio('0', 8, oservicio.SecuencialServicio.ToString()) + "-" + osistema.FechaSistema.Substring(0, 4);

                            } while (ValidaExisteAutorizaciónSolicitudCharter(tipoSolicitud, oservicio.NomenclaturaServicio));

                        }
                        numeroAutorizacion = oservicio.NomenclaturaServicio;
                    }

                    if (observacion.Length > 400)
                    {
                        int endIndex = (observacion.Length - 399);
                        observacion1 = observacion.Substring(0, 399);
                        observacion2 = observacion.Substring(399, endIndex);
                    }
                    else
                    {
                        observacion1 = observacion;
                        observacion2 = "";
                    }

                    queryUpdate = "UPDATE SOLARC SET SOLUS4 = '" + usuario + "', SOLFE5 = '" + osistema.FechaSistema + "', "
                        + "SOLHO4 = '" + osistema.HoraSistema + "', SOLOB3 = '" + observacion1 + "', SOLES3 = '" + estado + "', SOLAUT= '" + numeroAutorizacion + "', SOLEST='" + estado + "'"
                    + " WHERE SOLNUM = " + IdSolitud;

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        DatosAdicionalSolicitudVuelotransporteAereo(IdSolitud, observacion2);
                        if (oservicio.SecuencialServicio > 0)
                        {
                            CD_Servicios.Instancia.ActualizaAutorizacionServicio(oservicio.SecuencialServicio, tipoSolicitud);
                        }
                    }

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solitud de vuelo- Transpporte Aereo
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudVueloTransporteAereoDelegaDirectorGeneral(Int32 IdSolitud, string tipoSolicitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            string numeroAutorizacion = string.Empty;
            tbSistema osistema = new tbSistema();
            tbServiciosDgac oservicio = new tbServiciosDgac();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    if (estado.Equals("AP") || estado.Equals("NE"))
                    {
                        oservicio = CD_Servicios.Instancia.ObstenerAutorizacion(tipoSolicitud);
                        numeroAutorizacion = oservicio.NomenclaturaServicio;
                    }
                    queryUpdate = "UPDATE SOLARC SET SOLUS4 = '" + usuario + "', SOLFE5 = '" + osistema.FechaSistema + "', "
                        + "SOLHO4 = '" + osistema.HoraSistema + "', SOLOB3 = '" + observacion + "', SOLES3 = '" + estado + "', SOLAUT= '" + numeroAutorizacion + "', SOLEST='" + estado + "'"
                    + " WHERE SOLNUM = " + IdSolitud;

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        if (oservicio.SecuencialServicio > 0)
                        {
                            CD_Servicios.Instancia.ActualizaAutorizacionServicio(oservicio.SecuencialServicio, tipoSolicitud);
                        }
                    }

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solitud de vuelo- Transpporte Aereo
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudVueloTransporteAereoDirectorGeneral(Int32 IdSolitud, string tipoSolicitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            string numeroAutorizacion = string.Empty;
            tbSistema osistema = new tbSistema();
            tbServiciosDgac oservicio = new tbServiciosDgac();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    if (estado.Equals("AP") || estado.Equals("NE"))
                    {
                        oservicio = CD_Servicios.Instancia.ObstenerAutorizacion(tipoSolicitud);
                        if (ValidaExisteAutorizaciónSolicitudCharter(tipoSolicitud, oservicio.NomenclaturaServicio))
                        {
                            var servicioAut = CD_Servicios.Instancia.ObtenerServicio(tipoSolicitud);
                            do
                            {
                                oservicio.SecuencialServicio++;
                                oservicio.NomenclaturaServicio = servicioAut.NomenclaturaServicio + FiltraCaracteresInicio('0', 8, oservicio.SecuencialServicio.ToString()) + "-" + osistema.FechaSistema.Substring(0, 4);

                            } while (ValidaExisteAutorizaciónSolicitudCharter(tipoSolicitud, oservicio.NomenclaturaServicio));

                        }

                        numeroAutorizacion = oservicio.NomenclaturaServicio;
                    }
                    queryUpdate = "UPDATE SOLARC SET SOLUS4 = '" + usuario + "', SOLFE5 = '" + osistema.FechaSistema + "', "
                        + " SOLHO4 = '" + osistema.HoraSistema + "', SOLOB3 = '" + observacion + "', SOLES3 = '" + estado + "', SOLAUT= '" + numeroAutorizacion + "', SOLEST='" + estado + "',"
                        + " SOLUS5 = '" + usuario + "', SOLFEC = '" + osistema.FechaSistema + "', "
                        + " SOLHO5 = '" + osistema.HoraSistema + "', SOLOB4 = '" + observacion + "', SOLES5 ='" + estado + "'"
                        + " WHERE SOLNUM = " + IdSolitud;

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        if (oservicio.SecuencialServicio > 0)
                        {
                            CD_Servicios.Instancia.ActualizaAutorizacionServicio(oservicio.SecuencialServicio, tipoSolicitud);
                        }
                    }

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solitud de vuelo la DSOP 
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudVueloFinanzas(Int32 IdSolitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            tbSistema osistema = new tbSistema();
            iDB2Command cmd;

            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE SOLARC SET SOLUS1 = @UsuarioFinanciero , SOLFE3 = @FechaFinanciero, "
                        + "SOLHO2 = @HoraFinanciero, SOLOB2 = @ObservacionFinanciero, SOLES4 = @EstadoFinanciero"
                    + " WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@UsuarioFinanciero"].Value = usuario;
                    cmd.Parameters["@FechaFinanciero"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraFinanciero"].Value = osistema.HoraSistema;
                    cmd.Parameters["@ObservacionFinanciero"].Value = observacion;
                    cmd.Parameters["@EstadoFinanciero"].Value = estado;
                    cmd.Parameters["@NumeroSolicitud"].Value = IdSolitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solicitud del vuelo charter - Director
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudCharterDirector(Int32 IdSolitud, string tipoSolicitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            string numeroAutorizacion = string.Empty;
            tbSistema osistema = new tbSistema();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE SOLARC SET SOLUS5 = @UsuarioRespAto , SOLFEC = @FechaRespAto, "
                        + "SOLHO5 = @HoraRespAto, SOLOB4 = @ObservacionRespAto, SOLES5 = @EstadoRespAto, SOLEST=@EstadoRespAto"
                    + " WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@UsuarioRespAto"].Value = usuario;
                    cmd.Parameters["@FechaRespAto"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraRespAto"].Value = osistema.HoraSistema;
                    cmd.Parameters["@ObservacionRespAto"].Value = observacion;
                    cmd.Parameters["@EstadoRespAto"].Value = estado;
                    cmd.Parameters["@NumeroSolicitud"].Value = IdSolitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metod Actualiza los campos de la Autorización de la Solicitud.
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <returns>True o False</returns>
        public bool HabilitaNuevaAutorizacionTransorteAereo(Int32 IdSolitud, string Observacion)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            string numeroAutorizacion = string.Empty;
            tbSistema osistema = new tbSistema();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    queryUpdate = "UPDATE SOLARC SET SOLCON = SOLAUT, SOLNO4 = @Observacion, "
                        + " SOLUS4 = '', SOLFE5 = '', SOLHO4 = '', SOLOB3 = '', SOLES3 = '', SOLAUT= '', SOLEST='EN', "
                        + " SOLUS5 = '', SOLFEC = '', SOLHO5 = '', SOLOB4 = '', SOLES5 = '' "
                        + " WHERE SOLNUM = @NumeroSolicitud";

                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Observacion"].Value = Observacion;
                    cmd.Parameters["@NumeroSolicitud"].Value = IdSolitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo Actualiza el estado de aprobacion de la solitud de vuelo- Transpporte Aereo
        /// </summary>
        /// <param name="IdSolitud"></param>
        /// <param name="estado"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns>True o False</returns>
        public bool ActualizaSolicitudVueloResponsableAto(Int32 IdSolitud, string tipoSolicitud, string estado, string observacion, string usuario)
        {
            bool respuesta = true;
            string queryUpdate = string.Empty;
            iDB2Command cmd;
            string numeroAutorizacion = string.Empty;
            tbSistema osistema = new tbSistema();
            tbServiciosDgac oservicio = new tbServiciosDgac();
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                    if (estado.Equals("AP") || estado.Equals("NE"))
                    {
                        oservicio = CD_Servicios.Instancia.ObstenerAutorizacion(tipoSolicitud);
                        if (oservicio.SecuencialServicio > 0)
                        {
                            if (ValidaExisteAutorizaciónSolicitudCharter(tipoSolicitud, oservicio.NomenclaturaServicio))
                            {
                                var servicioAut = CD_Servicios.Instancia.ObtenerServicio(tipoSolicitud);
                                do
                                {
                                    oservicio.SecuencialServicio++;
                                    oservicio.NomenclaturaServicio = servicioAut.NomenclaturaServicio + FiltraCaracteresInicio('0', 8, oservicio.SecuencialServicio.ToString()) + "-" + osistema.FechaSistema.Substring(0, 4);

                                } while (ValidaExisteAutorizaciónSolicitudCharter(tipoSolicitud, oservicio.NomenclaturaServicio));

                            }

                            numeroAutorizacion = oservicio.NomenclaturaServicio;
                        }

                    }
                    if (estado.Equals("HC"))
                    {
                        queryUpdate = "UPDATE SOLARC SET SOLUS5 = @UsuarioRespAto , SOLFEC = @FechaRespAto,"
                                              + " SOLHO5 = @HoraRespAto, SOLOB4 = @ObservacionRespAto, SOLES5 = @EstadoRespAto, SOLAUT= @Autorizacion, "
                                              + " SOLUSU = @UsuarioRespAto , SOLFE2 = @FechaRespAto, SOLHO1 = @HoraRespAto, SOLOB1 = @ObservacionRespAto, SOLES2 = @EstadoRespAto,"
                                              + " SOLUS1 = @UsuarioRespAto , SOLFE3 = @FechaRespAto, SOLHO2 = @HoraRespAto, SOLOB2 = @ObservacionRespAto, SOLES4 = @EstadoRespAto,"
                                              + " SOLUS4 = @UsuarioRespAto , SOLFE5 = @FechaRespAto, SOLHO4 = @HoraRespAto, SOLOB3 = @ObservacionRespAto, SOLES3 = @EstadoRespAto, SOLCA1 = 1"
                                              + " WHERE SOLNUM = @NumeroSolicitud";
                    }
                    else
                    {
                        queryUpdate = "UPDATE SOLARC SET SOLUS5 = @UsuarioRespAto , SOLFEC = @FechaRespAto,"
                      + " SOLHO5 = @HoraRespAto, SOLOB4 = @ObservacionRespAto, SOLES5 = @EstadoRespAto, SOLAUT= @Autorizacion,  SOLEST=@EstadoRespAto,"
                      + " SOLUSU = @UsuarioRespAto , SOLFE2 = @FechaRespAto, SOLHO1 = @HoraRespAto, SOLOB1 = @ObservacionRespAto, SOLES2 = @EstadoRespAto,"
                      + " SOLUS1 = @UsuarioRespAto , SOLFE3 = @FechaRespAto, SOLHO2 = @HoraRespAto, SOLOB2 = @ObservacionRespAto, SOLES4 = @EstadoRespAto,"
                      + " SOLUS4 = @UsuarioRespAto , SOLFE5 = @FechaRespAto, SOLHO4 = @HoraRespAto, SOLOB3 = @ObservacionRespAto, SOLES3 = @EstadoRespAto, SOLCA1 = 1"
                      + " WHERE SOLNUM = @NumeroSolicitud";
                    }


                    cmd = new iDB2Command(queryUpdate, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@UsuarioRespAto"].Value = usuario;
                    cmd.Parameters["@FechaRespAto"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraRespAto"].Value = osistema.HoraSistema;
                    cmd.Parameters["@ObservacionRespAto"].Value = observacion;
                    cmd.Parameters["@EstadoRespAto"].Value = estado;
                    cmd.Parameters["@Autorizacion"].Value = numeroAutorizacion;
                    cmd.Parameters["@NumeroSolicitud"].Value = IdSolitud;
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                    if (respuesta)
                    {
                        if (oservicio.SecuencialServicio > 0)
                        {
                            CD_Servicios.Instancia.ActualizaAutorizacionServicio(oservicio.SecuencialServicio, tipoSolicitud);
                        }
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Valida el numero de autorizacion si ya existe en la tabla de solicitud de chater
        /// </summary>
        /// <param name="tipoSolcitud"></param>
        /// <param name="numAutorización"></param>
        /// <returns>True o False</returns>
        private bool ValidaExisteAutorizaciónSolicitudCharter(string tipoSolcitud, string numAutorización)
        {
            string query = "SELECT SOLAUT FROM SOLARC WHERE SOLCO1 = '" + tipoSolcitud.Trim() + "' AND SOLAUT = '" + numAutorización.Trim() + "'";

            iDB2Command cmd;
            bool estado = false;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        return estado = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }


        /// <summary>
        /// VALIDA SI YA ESTA REALIZADO EL PAGO POR DEPOSITO O TRASNSFERENCIA
        /// </summary>
        /// <param name="tipoPago"></param>
        /// <param name="numComprobante"></param>
        /// <returns></returns>
        private bool ValidaExistePago(string tipoPago, string numComprobante)
        {
            string query = "SELECT SOLFOR, PAGNU1 FROM SOLARC "
                + " LEFT JOIN PAGARC ON(SOLNUM = PAGNUM)"
                + " WHERE  SOLFOR = '" + tipoPago.Trim().ToUpper() + "' AND PAGNU1 = '" + numComprobante.Trim() + "'";

            iDB2Command cmd;
            bool estado = false;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        return estado = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                estado = false;
            }
            return estado;
        }



        private String fechaAS400(string ofecha)
        {
            string odate = string.Empty;
            try
            {
                if (ofecha.Trim().Length > 0)
                {
                    DateTime ofechaForato = Convert.ToDateTime(ofecha);
                    odate = ofechaForato.ToString("yyyyMMdd");
                }
            }
            catch
            {
                odate = ofecha;
            }

            return odate;
        }

        private String RetornoAnio(string ofecha)
        {
            string odate = string.Empty;
            try
            {
                if (ofecha.Trim().Length > 0)
                {
                    odate = ofecha.Substring(0, 4);
                }
            }
            catch
            {
                odate = ofecha;
            }

            return odate;
        }
        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

        private string EstadoNacInt(string campo)
        {
            string estado = string.Empty;
            if (campo.Equals("02"))
            {
                estado = "N";
            }
            else if (campo.Equals("03"))
            {
                estado = "I";
            }
            else if (campo.Equals("04"))
            {
                estado = "N";
            }
            else if (campo.Equals("01"))
            {
                estado = "I";
            }
            return estado;
        }

        private string TipoOperacion(string campo)
        {
            string estado = string.Empty;
            if (campo.Equals("02"))
            {
                estado = "04";
            }
            else if (campo.Equals("03"))
            {
                estado = "05";
            }
            else if (campo.Equals("04"))
            {
                estado = "04";
            }
            else if (campo.Equals("01"))
            {
                estado = "05";
            }
            return estado;
        }

        private string checkBoxString(bool estado)
        {
            if (estado)
                return "1";
            else
                return "0";
        }

        private bool checkBoxBool(string estado)
        {
            if (estado.Equals("1"))
                return true;
            else
                return false;
        }
        public string FiltraCaracteresInicio(char character, int length, string word)
        {
            string result = word;

            for (int i = word.Length; i < length; i++)
            {
                result = character + result;
            }

            return result;
        }

        private string FechaVueloResta(string fechaVuelo, int dias)
        {
            DateTime nuevaFecha = Convert.ToDateTime(fechaDate(fechaVuelo));
            nuevaFecha = nuevaFecha.AddDays(dias);
            return nuevaFecha.ToString("yyyyMMdd").ToUpper();
        }

        private String fechaDate(string ofecha)
        {

            string odate = string.Empty;

            if (ofecha.Trim().Length > 0)
            {
                if (ofecha.Length > 8)
                {
                    odate = ofecha.Substring(0, 4) + "/" + ofecha.Substring(4, 2) + "/" + ofecha.Substring(6, 2) + " " + ofecha.Substring(9, 8);
                }
                else
                {
                    odate = ofecha.Substring(0, 4) + "/" + ofecha.Substring(4, 2) + "/" + ofecha.Substring(6, 2);
                }

            }
            return odate;
        }
    }
}
