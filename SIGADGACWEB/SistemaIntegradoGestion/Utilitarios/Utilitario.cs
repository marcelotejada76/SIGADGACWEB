using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class Utilitario
    {
        public static Utilitario _instancia = null;
        private Utilitario()
        {

        }

        public static Utilitario Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Utilitario();
                }
                return _instancia;
            }
        }

        public string fechaTexto(string fecha)
        {

            string fechaNueva = string.Empty;
            if (fecha.Length > 0)
            {
                fechaNueva = fecha.Substring(6, 2) + " de " + mestexto(fecha.Substring(4, 2)) + " de " + fecha.Substring(0, 4);

            }

            return fechaNueva;
        }

        public string fechaDate(string fecha)
        {

            string fechaNueva = string.Empty;
            if (fecha != null)
            {
                if (fecha.Length > 0)
                {
                    fechaNueva = fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2);

                }
            }

            return fechaNueva;
        }

        public String fechaDateAs400(string ofecha)
        {
            string odate = string.Empty;

            if (ofecha.Trim().Length > 0)
            {
                odate = ofecha.Substring(6, 2) + "-" + ofecha.Substring(4, 2) + "-" + ofecha.Substring(0, 4);
            }
            return odate;
        }

        public string mestexto(string mes)
        {
            if (mes.Equals("01"))
                return "enero";
            else if (mes.Equals("02"))
                return "febrero";
            else if (mes.Equals("03"))
                return "marzo";
            else if (mes.Equals("04"))
                return "abril";
            else if (mes.Equals("05"))
                return "mayo";
            else if (mes.Equals("06"))
                return "junio";
            else if (mes.Equals("07"))
                return "julio";
            else if (mes.Equals("08"))
                return "agosto";
            else if (mes.Equals("09"))
                return "septiembre";
            else if (mes.Equals("10"))
                return "octubre";
            else if (mes.Equals("11"))
                return "noviembre";
            else if (mes.Equals("12"))
                return "diciembre";
            else
                return "";
        }

        public const string charterURL = @"\\172.20.16.90\vuelos_charter\AdjuntosCharter";
        public const string certificadoURL = @"\\172.20.16.90\vuelos_charter\AdjuntoCertificado\";
        public const string autorizacionURL = @"\\172.20.16.90\vuelos_charter\AdjuntoAutorizacion\";
        public const string certificadoPOAUrl = @"\\172.20.16.90\Sigpoa\Certificado\";

        public string llenaCero(string numero)
        {
            return numero = numero.PadLeft(10, '0');
        }

    }
}