using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_ActualizaCarteraVencida
    {
        public static CD_ActualizaCarteraVencida _instancia = null;
        private CD_ActualizaCarteraVencida()
        {

        }

        public static CD_ActualizaCarteraVencida Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ActualizaCarteraVencida();
                }
                return _instancia;
            }
        }

       

        public string ActualizaCartera(string Mensaje)
        {
            DateTime FechaProceso = DateTime.Now;
            string Fecha = FechaProceso.ToString("yyyyMMdd");
           
            //// DEUDOR CON MATRICULA
            var DeudorMatricula = ValidaRegistros.DeudorMatricula();
            if (DeudorMatricula.Count > 0)
            {
                InsertarTablaP5.insertar_DeudorMatricula(DeudorMatricula);
            }

            Mensaje = "Correcto";
            return Mensaje;
        }

      


        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}
