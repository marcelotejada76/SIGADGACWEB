using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbPagoSolicitud
    {
        private decimal numeroSolicitud { get; set; }
        private string comprobanteTransaccion { get; set; }
        private string fechaComprobanteTransaccion { get; set; }
        private string formaPago { get; set; }
        private decimal valorPagar { get; set; }
        private string ruc { get; set; }
        private string tipoComprobante { get; set; }
        private string establecimiento { get; set; }
        private string puntoEmision { get; set; }
        private string numeroComprobante { get; set; }
        private string numeroAutorizacion { get; set; }
        private string usuarioCreado { get; set; }
        private string fechaCreado { get; set; }
        private string horaCreado { get; set; }
        private string usuarioModificado { get; set; }
        private string fechaModificado { get; set; }
        private string horaModificado { get; set; }

        public tbPagoSolicitud()
        {

        }
        public tbPagoSolicitud(decimal numeroSolicitud, string comprobanteTransaccion, string fechaComprobanteTransaccion, string formaPago, decimal valorPagar,
            string ruc, string tipoComprobante, string establecimiento, string puntoEmision, string numeroComprobante, string numeroAutorizacion, string usuarioCreado, string fechaCreado, string horaCreado, string usuarioModificado, string fechaModificado, string horaModificado)
        {
            this.numeroSolicitud = numeroSolicitud;
            this.comprobanteTransaccion = comprobanteTransaccion;
            this.fechaComprobanteTransaccion = fechaComprobanteTransaccion;
            this.formaPago = formaPago;
            this.valorPagar = valorPagar;
            this.ruc = ruc;
            this.tipoComprobante = tipoComprobante;
            this.establecimiento = establecimiento;
            this.puntoEmision = puntoEmision;
            this.numeroComprobante = numeroComprobante;
            this.numeroAutorizacion = numeroAutorizacion;
            this.usuarioCreado = usuarioCreado;
            this.fechaCreado = fechaCreado;
            this.horaCreado = horaCreado;
            this.usuarioModificado = usuarioModificado;
            this.fechaModificado = fechaModificado;
            this.horaModificado = horaModificado;
        }
        public decimal NumeroSolicitud
        {
            get { return numeroSolicitud; }
            set { numeroSolicitud = value; }
        }

        public string ComprobanteTransaccion
        {
            get { return comprobanteTransaccion; }
            set { comprobanteTransaccion = value; }
        }
        public string FechaComprobanteTransaccion
        {
            get { return fechaComprobanteTransaccion; }
            set { fechaComprobanteTransaccion = value; }
        }
        public string FormaPago
        {
            get { return formaPago; }
            set { formaPago = value; }
        }
        public decimal ValorPagar
        {
            get { return valorPagar; }
            set { valorPagar = value; }
        }
        public string Ruc
        {
            get { return ruc; }
            set { ruc = value; }
        }
        public string TipoComprobante
        {
            get { return tipoComprobante; }
            set { tipoComprobante = value; }
        }
        public string Establecimiento
        {
            get { return establecimiento; }
            set { establecimiento = value; }
        }
        public string PuntoEmision
        {
            get { return puntoEmision; }
            set { puntoEmision = value; }
        }
        public string NumeroComprobante
        {
            get { return numeroComprobante; }
            set { numeroComprobante = value; }
        }
        public string NumeroAutorizacion
        {
            get { return numeroAutorizacion; }
            set { numeroAutorizacion = value; }
        }
        public string UsuarioCreado
        {
            get { return usuarioCreado; }
            set { usuarioCreado = value; }
        }
        public string FechaCreado
        {
            get { return fechaCreado; }
            set { fechaCreado = value; }
        }
        public string HoraCreado
        {
            get { return horaCreado; }
            set { horaCreado = value; }
        }
        public string UsuarioModificado
        {
            get { return usuarioModificado; }
            set { usuarioModificado = value; }
        }
        public string FechaModificado
        {
            get { return fechaModificado; }
            set { fechaModificado = value; }
        }
        public string HoraModificado
        {
            get { return horaModificado; }
            set { horaModificado = value; }
        }
    }
}
