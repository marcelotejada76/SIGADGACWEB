using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaIntegradoGestion.Models
{
    public class CursoViewModel
    {

        public string DocumentoIdentificacion { get; set; }
        public string CodigoCursoEmpleado { get; set; }
        public string DescripcionCurso { get; set; }
        public string DescripcionCursoUno { get; set; }
        public string ObservacionCurso { get; set; }
        public string UbservacionCursoUno { get; set; }
        public string FechaCurso { get; set; }
        public string AprobacionCurso { get; set; }
        public string AsistenciaCurso { get; set; }
        public string DuracionCurso { get; set; }
        public string TiempoCurso { get; set; }
        public string EstadoCurso { get; set; }
        public string NumeroIdentidicacion { get; set; }
        public string PathDocumentoCurso { get; set; }
        public string CodigoTitulo { get; set; }
        public string CodigoCiudad { get; set; }
        public string CodigoEntidadEducativa { get; set; }
        public HttpPostedFileBase DocumentoAdjunto { get; set; }
    }
}