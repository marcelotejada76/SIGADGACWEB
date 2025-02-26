using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbControlFR3
    {
        public decimal Secuencial { get; set; }
        public string Aeropuerto { get; set; }
        public string Anio { get; set; }
        public string FechaControlVuelo { get; set; }
        public string TipoOperacion { get; set; }
        public string RutaTotalPlanVlo { get; set; }
        public Int32 NumAterrizaPais { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorCharter { get; set; }
        public decimal Total { get; set; }
        public decimal GranTotal { get; set; }
        public string GranTotalLetras { get; set; }
        public string Autorizacion { get; set; }
        public string Observacion { get; set; }
        public decimal OidCiaAviacion { get; set; }
        public decimal OidUbicacion { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Retorno { get; set; }
        public string Callsign { get; set; }
        public string Estado { get; set; }
        public string Ruc { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string FormaPago { get; set; }
        public string NacInter { get; set; }
        public string UsuarioCr { get; set; }
        public string FechaCr { get; set; }
        public string HoraCr { get; set; }
        public decimal IdAeropuerto { get; set; }
        public string NombreCliente { get; set; }
        public decimal OidUbicacionCliente { get; set; }
        public string NombreCia { get; set; }
        public string Modelo { get; set; }
        public decimal PesoMatricula { get; set; }
        public string CodigoOACICia { get; set; }
        public string NombreAeropuerto { get; set; }
        public string EmailUsuarioDGAC { get; set; }
        public string Matricula { get; set; }
        public string FechaRcepcion { get; set; }
        public string CodigoBanco { get; set; }
        public string Deposito { get; set; }

        public tbControlFR3()
        {
            this._odetalleControlFr3 = new tbControlFR3Detalle();
        }
        public tbControlFR3Detalle _odetalleControlFr3 { get; set; }
    }
}
