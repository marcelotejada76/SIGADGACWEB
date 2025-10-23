using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class ValidaCampos
    {
        public static string ValidaFecha(Object fecha)
        {
            string Fechaf = "";
            try
            {


                if (fecha != null)
                {
                    Fechaf = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy");
                }
                else
                    Fechaf = "";
            }
            catch (Exception)
            {

                Fechaf = "";
            }

            return Fechaf;

        }

        public static string ValidaFechaCreacion(Object fecha)
        {
            string Fechaf = "";
            try
            {


                if (fecha != null)
                {
                    Fechaf = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                }
                else
                    Fechaf = "";
            }
            catch (Exception)
            {

                Fechaf = "";
            }

            return Fechaf;

        }
        public static string ValidaBlancos(Object variable)
        {
            string res = "";
            try
            {


                if (variable != null)
                {
                    //oRecaudacion.LUGAR = item.Field<string>("LUGAR").ToString().Trim();
                    res = variable.ToString().Trim();
                }
                else
                    res = "";
            }
            catch (Exception)
            {

                res = "";
            }

            return res;

        }

        public static double ValidaDecimal(Object variable)
        {
            double res = 0;
            try
            {


                if (variable != null)
                {
                    //oRecaudacion.LUGAR = item.Field<string>("LUGAR").ToString().Trim();

                    res = Convert.ToDouble(variable);
                }
                else
                    res = 0;
            }
            catch (Exception)
            {

                res = 0;
            }

            return res;

        }

        public static Int32 ValidaEntero(Object variable)
        {
            Int32 res = 0;
            try
            {


                if (variable != null)
                {
                    //oRecaudacion.LUGAR = item.Field<string>("LUGAR").ToString().Trim();

                    res = Convert.ToInt32(variable);
                }
                else
                    res = 0;
            }
            catch (Exception Ex)
            {

                res = 0;
            }

            return res;

        }
        public static Char ValidaCaracter(Object variable)
        {
            char res = ' ';
            try
            {


                if (variable != null)
                {
                    //oRecaudacion.LUGAR = item.Field<string>("LUGAR").ToString().Trim();
                    res = Convert.ToChar(variable);
                }
                else
                    res = ' ';
            }
            catch (Exception)
            {

                res = ' ';
            }

            return res;

        }
    }
}
