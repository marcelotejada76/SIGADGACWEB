using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
   public class tbMaestroPersonal
    {
        public string DocumentoIdentificacion { get; set; }
        public string TipoDocumentoIdent { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaNacimiento { get; set; }
        public string NumeroTelefonoDomicillo { get; set; }
        public string NumeroTelefonoCelular { get; set; }
        public string EmailPersonal { get; set; }
        public decimal Estatura { get; set; }
        public string Genero { get; set; }
        public string ColorOjos { get; set; }
        public string ColorCaballo { get; set; }
        public string TipoSangre { get; set; }
        public string EstadoCivil { get; set; }
        public decimal Peso { get; set; }
        public string DireccionDomicillo { get; set; }
        public string CodigoGeograficoCanton { get; set; }
        public string SectorDondeVive { get; set; }
        public string RegimenLaboral { get; set; }
        public string TipoHorario { get; set; }
        public string DecimoTerceroAculado { get; set; }
        public string DecimoCuartoAculado { get; set; }
        public string AporteFondoReserva { get; set; }
        public string NombreContactoEmergencia { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public string AlergiaMedicina { get; set; }
        public string AlergiaAlimentos { get; set; }
        public string AlergiaMedioAmbiente { get; set; }
        public string Discapacidad { get; set; }
        public string DiscapacidadNombre { get; set; }
        public string Porcentaje { get; set; }
        public string EnfermedadCatrastrofica { get; set; }
        public string EnfermedadCatrastroficaNombre { get; set; }
        public string Sustituto { get; set; }
        public string NombreFamiliarSustituto { get; set; }
        public string ParentescoSubtituto { get; set; }
        public string SenescytNumeroRegistro { get; set; }
        public string UltimoTituloObtenido { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string DispositivoCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
        public string HoraModificacion { get; set; }
        public string DispositivoModificacion { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoProviencia { get; set; }
        public string CodigoCanton { get; set; }
        public string CodigoParroquia { get; set; }
        public string CodigoCiudad { get; set; }
        public decimal OidDiscapacidadEnfermedad { get; set; }
        public string PathFoto { get; set; }
        public string imagenFoto { get; set; }
        public int tabNumero { get; set;}
        public List<tbCursoEmpleado> oCursoEmpleado { get; set; }
    }
}
