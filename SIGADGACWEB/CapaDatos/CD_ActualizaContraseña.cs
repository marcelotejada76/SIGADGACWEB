using CapaModelo;
using cwbx;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ActualizaContraseña
    {
        public static CD_ActualizaContraseña _instancia = null;
        private CD_ActualizaContraseña()
        {

        }

        public static CD_ActualizaContraseña Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ActualizaContraseña();
                }
                return _instancia;
            }
        }

        public string  CambiaClave(string Usuario, string Contraseña, string NuevaContraseña)
        {
            string system = "172.20.16.163";
            //  tbFr3 listarSolicitud = new tbFr3();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            string msgerror = "";
            try
            {
                AS400System as400 = new AS400System();
                as400.Define(system);

                try
                {
                    as400.ChangePassword(Usuario, Contraseña, NuevaContraseña);
                    
                }

                catch (Exception ex)
                {
                    Console.WriteLine("error" + ex.Message);
                     msgerror = ex.Message;
                }

                finally
                {
                    as400.Disconnect(cwbcoServiceEnum.cwbcoServiceAll);
                }

                

            }
            catch (Exception ex)
            {
                //   throw ex;
            }
            return msgerror;

        }



    }
}
