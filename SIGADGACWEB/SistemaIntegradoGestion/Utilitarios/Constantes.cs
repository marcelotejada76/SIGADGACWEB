using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class Constantes
    {
        public static Constantes _instancia = null;
        private Constantes()
        {

        }

        public static Constantes Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Constantes();
                }
                return _instancia;
            }
        }

        public const string poaURL = @"\\172.20.16.90\Sigpoa";
        public const string modificacionpoaURL = @"\\172.20.16.90\Sigpoa\ModificacionSinAfectacion\";
        public const string MaestroPersonalURL = @"\\172.20.16.90\vuelos_charter\MaestroPersonal\";
        public const string ReporteElectronicoPOAURL = @"\\172.20.16.90\Sigpoa\Certificado\";
        public const string ConfiarUrl = @"\\172.20.16.149\d$\";

    }
}