using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbUsuario
    {
        public string CodigoUsuario { get; set; }
        public string NombresUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string TipoIdentificacion { get; set; }
        public string CedulaUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public string EstadoActividad { get; set; }
        public string TipoestadoActividad { get; set; }
        public string TipoAplicacion { get; set; }
        public string IdentificacionTributaria { get; set; }
        public string NumeroRuc { get; set; }
        public string CodigoCiudad { get; set; }
        public string CodigoDependencia { get; set; }
        public string CodigoSubsistema { get; set; }
        public string CodigoGestion { get; set; }
        public string CodigoModulo { get; set; }
        public string CodigoRol { get; set; }
        public string CentroContable { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string DispositivoCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
        public string HoraModificacion { get; set; }
        public string DispositivoModificacion { get; set; }
        public string DescripcionSubSistema { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string fechaSistema { get; set; }
        public string TipoHorario { get; set; }
        public string NumeroAleatorio { get; set; }
        public string NombreCorto { get; set; }
        public string Cargo { get; set; }
        public tbRol oRol { get; set; }
        public tbHorioAtencion oHorarioAtencion { get; set; }
    }
}
