using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
  public class tbSolictudVuelo
    {
        public Int32 NumeroSolicitud { get; set; }
        public string TipoSolictud { get; set; } //ES=VUELO ESPECIAL/IN=VUELOS INTERNACIONAL/NA=VUELOS NACIONAL
        public string DescripcionTipoSolictud { get; set; }
        public decimal NumeroVuelos { get; set; }
        public string PropositoVuelo { get; set; }
        public string EspecificarVuelo { get; set; } //P=PASAJEROS, C=CARGA
        public decimal NumeroPasajeros { get; set; }
        public decimal IdFleteador { get; set; }
        public string TipoIdentificacionFleteador { get; set; }
        public string DescripcionTipoIdentificacion { get; set; }
        public string RucFleteador { get; set; }
        public string NombreFleteador { get; set; }
        public string TelefonoFleteador { get; set; }
        public string CelularFleteador { get; set; }
        public string DireccionFleteador { get; set; }
        public string CorreoFleteador { get; set; }
        public decimal IdFactura { get; set; }
        public string RucFactura { get; set; }
        public string NombreFactura { get; set; }
        public string TelefonoFactura { get; set; }
        public string CelularFactura { get; set; }
        public string DireccionFactura { get; set; }
        public string CorreoFactura { get; set; }
        public string PermisoOperacion { get; set; }
        public string EspecificacionesOperacion { get; set; }
        public string FormaPago { get; set; }
        public string DescripcionFormaPago { get; set; }
        public string BancoPago { get; set; }
        public string CertificadoOperador { get; set; }  // Campo Libre
        public string ComprobantePago { get; set; } //Path file adjunto del pago de la solicitud vuelo Charter(NAC-INT) o Especial
        public string VueloItinerario { get; set; } //Almacena cuando el vuelos es Especial
        public string UsuarioCreacion { get; set; }
        public string FechaEnvioSolicitud { get; set; }
        public string HoraEnvioSolicitud { get; set; }
        public string UsuarioSolictudModificacion { get; set; }
        public string FechaSolictudModificacion { get; set; }
        public string HoraSolictudModificacion { get; set; }
        public string EstadoSolicitud { get; set; }
        public string DescripcionSolicitud { get; set; }
        public string UsuarioOperacionesDSOP { get; set; }
        public string FechaOperacionesDSOP { get; set; }
        public string HoraOperacionesDSOP { get; set; }
        public string EstadoOperacionesDSOP { get; set; }
        public string DescripcionOperacionesDSOP { get; set; }
        public string ObservacionOperacionesDSOP { get; set; }
        public string UsuarioFinanciero { get; set; }
        public string FechaFinanciero { get; set; }
        public string HoraFinanciero { get; set; }
        public string EstadoFinanciero { get; set; }
        public string DescripcionEstadoFinanciero { get; set; }
        public string ObservacionFinanciero { get; set; }
        public string UsuarioResponsableATO { get; set; }
        public string FechaResponsableATO { get; set; }
        public string HoraResponsableATO { get; set; }
        public string EstadoResponsableATO { get; set; }
        public string DescripcionResponsableATO { get; set; }
        public string ObservacionResponsableATO { get; set; }
        public string Observacion { get; set; }
        public string CedulaRepresentante { get; set; }
        public string NombreRepresentante { get; set; }
        public string AutorizacionSolicitudVuelo { get; set; }
        public string IdCompaniaOperador { get; set; }
        public string CodigoOaci { get; set; }
        public string CodigoIata { get; set; }
        public string CodigoNumeroCia { get; set; }
        public string NombreCompaniaAviacion { get; set; }
        public string Direccion { get; set; }
        public string RucCompania { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string RepresentanteLegal { get; set; }
        public string CodigoPais { get; set; }
        public string UsuarioTransporteAereo { get; set; }
        public string FechaTransporteAereo { get; set; }
        public string HoraTransporteAereo { get; set; }
        public string EstadoTransporteAereo { get; set; }
        public string DescripcionEstadoTransporteAereo { get; set; }
        public string ObservacionTransporteAereo { get; set; }
        public string NombreResponsableContacto { get; set; }
        public string DireccionResponsableContacto { get; set; }
        public string TelefonoResponsableContacto { get; set; }
        public string CorreoResponsableContacto { get; set; }
        public string MatriculaAeronave { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string VigenciaSeguroAeronave { get; set; }
        public string MtowAeronave { get; set; }
        public bool CheckboxCertificado { get; set; }
        public string CertificadoAeronavegabilidad { get; set; }
        public string IdentificacionVuelo { get; set; }
        public string CboAeropuertoOrigen { get; set; }
        public string CboAeropuertoAterrizaje { get; set; }
        public string CboAeropuertoDestino { get; set; }
        public string Reservationdate { get; set; }
        public string FechaVueloMenos { get; set; }
        public string FechaVueloMas { get; set; }
        public string FechaIdaVuelo { get; set; }
        public string FechaRetornoVuelo { get; set; }
        public string HoraEstimadaVuelo { get; set; }
        public string CboTipoVuelo { get; set; }
        public string DescripcionTipoVuelo { get; set; }
        public string ComentarioVuelo { get; set; }
        public string fechaVigencia { get; set; }
        public bool CheckboxFlota { get; set; }
        public bool CheckboxRutaRetorno { get; set; }
        public string IdentificacionVueloRetorno { get; set; }
        public string CboAeropuertoOrigenRetorno { get; set; }
        public string CboAeropuertoAterrizajeRetorno { get; set; }
        public string CboAeropuertoDestinoRetorno { get; set; }
        public string ReservationdateRetorno { get; set; }
        public string CboTipoVueloRetorno { get; set; }
        public string DescripcionTipoVueloRetorno { get; set; }
        public string ComentarioVueloRetorno { get; set; }
        public string CodigoDependencia { get; set; }
        public Int32 NumeroPermiso { get; set; }
        public string Establecimiento { get; set; }
        public string PuntoEmision { get; set; }
        public string NumeroFactura { get; set; }
        public string NumeroComprobante { get; set; }
        public string FechaTransaccion { get; set; }
        public string EstadoPago { get; set; }
        public decimal CostoCharter { get; set; }
        public decimal TotalPagar { get; set; }
        public bool AceptarTerminos { get; set; }
        public string Referencia { get; set; }
        public string FechaReferenca { get; set; }
        public string DocumentoPersonal { get; set; }
        public decimal EstadoAutorizacion { get; set; }
        public string EstadoCertificado { get; set; }
        public string MensajeCertificado { get; set; }
        public string ContratoFleteamiento { get; set; }
        public string NombreContratoFletamiento { get; set; }
        public string EstadoAutoriza { get; set; }
        public int EstadoHabitaAutorizada { get; set; }
        public int EstadoModificaSolicitud { get; set; }
        public Int32 NumeroSolicitudAnterior { get; set; }
        public string AsuntoModificacion { get; set; }
        public string ObservacionModificacion { get; set; }

        public tbSolictudVuelo()
        {
            this.oDetalleRuta = new List<tbDetalleRuta>();
            this.oDetalleAeronave = new List<tbDetalleAeronave>();            
            this.oPagoSolictud = new List<tbPagoSolicitud>();
            this.oAdjuntoCiaAviacion = new List<tbAdjuntoCiaAviacion>();
            this.oCiaOperadora = new tbCompaniaAviacion();
            this.oCiaFacturar = new tbCompaniaAviacion();
        }
        public tbCompaniaAviacion oCiaOperadora { get; set; }
        public tbCompaniaAviacion oCiaFacturar { get; set; }
        public List<tbDetalleRuta> oDetalleRuta { get; set; }
        public List<tbDetalleAeronave> oDetalleAeronave { get; set; }
        public List<tbDetalleAdjunto> oDetalleAdjunto { get; set; }
        public List<tbPagoSolicitud> oPagoSolictud { get; set; }
        public List<tbAdjuntoCiaAviacion> oAdjuntoCiaAviacion { get; set; }
       
        public List<tbDeudor> oDeudor { get; set; }
    }
}
