using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaModelo
{
    public class tbCabeceraCurriculum
    {
        public int SECUENCIAL { get; set; }
        
        [Required(ErrorMessage = "El numero de cedula es obligatorio.")]
        [StringLength(100, ErrorMessage = "El numero de cedula ombre no puede tener más de 100 caracteres.")]
        public string CEDULA { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(60, ErrorMessage = "El nombre no puede tener más de 60 caracteres.")]
        public string NOMBRES { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(60, ErrorMessage = "El apellido no puede tener más de 60 caracteres.")]
        public string APELLIDOS { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
         public string EMAIL { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "El teléfono debe ser un número válido (e.g., +573001234567).")]
        public string CELULAR { get; set; }

        public string PAIS { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
                public string DIRECCION { get; set; }
        [Required(ErrorMessage = "fecha de nacimiento es obligatorio.")]
         public string FECHANACIMIENTO { get; set; }
        public string NACIONALIDAD { get; set; }
        public string BREVEDESCRIPCION { get; set; }
    }
}
