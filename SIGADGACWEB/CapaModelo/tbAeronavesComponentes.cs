using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class tbAeronavesComponentes
    {
        public string AERONAVE { get; set; }
        public string CODIFOOACI { get; set; }
        public string EXPLOTADOR { get; set; }
        public string PROPIETARIO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string AÑOFAB { get; set; }
        public Decimal PESOVACIO { get; set; }
        public Decimal PMP { get; set; }
        public Decimal TECHO { get; set; }
        public string PESOVACIODESIGNACION { get; set; }
        public string PMPDESIGNACION { get; set; }
        public string TECHODESIGNACION { get; set; }
        public Int16 NUMEROPAX { get; set; }

        public string MOTOR1MARCA { get; set; }
        public string MOTOR1MODELO { get; set; }
        public string MOTOR2MARCA { get; set; }
        public string MOTOR2MODELO { get; set; }
        public string HELICE1MARCA { get; set; }
        public string HELICE1MODELO { get; set; }
        public string HELICE2MARCA { get; set; }
        public string HELICE2MODELO { get; set; }

        public string ELTMARCA { get; set; }
        public string ELTMODELO { get; set; }
        public string ELTSERIE { get; set; }
        public string ELTCODIGOHEX { get; set; }
        public string ELTPORTATILMARCA { get; set; }
        public string ELTPORTATILMODELO { get; set; }
        public string ELTPORTATILSERIE { get; set; }
        public string ELTPORTATILCODIGOHEX { get; set; }

        public string CODIGOMODOSS { get; set; }
        public string TIPOAPROBACION { get; set; }
        public string CONDICION { get; set; }
        public string REGION { get; set; }
        public string BASEOPERACION { get; set; }
        public string ESTADO { get; set; }
        public string FECHAMONITOREORVSM { get; set; }
        public string ERRORASE { get; set; }
        public string OBSERVACIONES { get; set; }



        public List<tbAeronavesCertAeronav> oDetalleCertAeronavegabilidad { get; set; }
        public List<tbDetalleCertLicRadio> oDetalleCertRadio { get; set; }
        public List<tbDetalleCertHomolRuido> oDetalleCerHomolRuido { get; set; }
        public List<tbDetalleCerrtAprobaPbnRnp10> oDetalleCertPbnRnp10 { get; set; }
        public List<tbDetalleCerrtAprobRnav5> oDetalleCertRnav5 { get; set; }

        public List<tbDetalleCerrtAprobRnav2> oDetalleCertRnav2 { get; set; }
        public List<tbDetalleCerrtAprobRnavArAproach> oDetalleCertRnavApproach { get; set; }
        public List<tbDetalleCerrtAprobRvsm> oDetalleCertRvsm { get; set; }
        public List<tbDetalleCerrtAprobEtops> oDetalleCertEtops { get; set; }
        public List<tbDetalleCerrtCat2y3> oDetalleCategorias { get; set; }
        public List<tbDetalleAccidenteAeronave> oDetalleAccidenteAeronave { get; set; }

    }
}
