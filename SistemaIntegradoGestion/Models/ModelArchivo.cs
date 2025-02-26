using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaIntegradoGestion.Models
{
    public class ModelArchivo
    {
        public string NombreArchivo { get; set; }
        public string FechaModificacion { get; set; }
        public string Tipo { get; set; }
        public string Tamano { get; set; }
        public string Directorio { get; set; }
    }
}