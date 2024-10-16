using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbHistoricoAtc
    {

        public string Licencia_ATC { get; set; }
        public string Institucion { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Nombres { get; set; }
        public string Nacimiento { get; set; }
        public string Cédula { get; set; }
        public string Dirección_Domicilio { get; set; }
        public string Teléfono_Domicilio { get; set; }
        public string Ciudad_Domilicio { get; set; }
        public string Teléfono_Celular { get; set; }

        public string Email_Institucional { get; set; }
        public string Email_Particular { get; set; }
        public string Estado_Civil { get; set; }
        public string Conyugue { get; set; }
        public string Fecha_ingreso_DGAC { get; set; }
        public string Cargo_actual { get; set; }
        public string Dependencia_actual { get; set; }
        public string Aeropuerto { get; set; }
        public string Región { get; set; }
        public string Habilitación { get; set; }
        public string Fecha_Habilitación { get; set; }
        public string años { get; set; }
        public string meses { get; set; }
        public string Doble_Habilitación { get; set; }
        public string Otra_Habilitación { get; set; }
        public string Caducidad_CertifMedi { get; set; }
        public string Status { get; set; }
        public string Competencia_Lingüíst { get; set; }
        public string Caducidad_Certificado_Competencia_Lingui { get; set; }

        public string Url { get; set; }

        public List<tbHistoricoDetalleDependenciaAtc> oDetalleHistoricoDependenciaAtc { get; set; }
        public List<tbHistoricoDetalleCursoAtc> oDetalleHistoricoCursoAtc { get; set; }


    }
}
