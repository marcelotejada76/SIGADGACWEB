using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using IBM.Data.DB2.iSeries;

namespace CapaDatos
{
   public class CD_Servicios
    {
        public static CD_Servicios _instancia = null;
        //string biblioteca = "DGACDAT";
        private CD_Servicios()
        {

        }

        public static CD_Servicios Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Servicios();
                }
                return _instancia;
            }
        }

        public tbServiciosDgac ObtenerServicio(String codServicio)
        {
            tbServiciosDgac oServicio = new tbServiciosDgac();
            string query = "SELECT ifnull(rtrim(ltrim(SERCOD)), '') as CodigoServicio, ifnull(rtrim(ltrim(SERDES)), '') as DescripcionServicio, ifnull(CAST(SERSEC as INTEGER), 0) as SecuencialServicio, "
                + " ifnull(rtrim(ltrim(SERNOM)), '') AS NomenclaturaServicio, ifnull(SERVAL, 0) AS ValorServicio"
                + " FROM SERARC WHERE SERCOD = '" + codServicio + "'";
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oServicio.CodigoServicio = dr["CodigoServicio"].ToString();
                        oServicio.DescripcionServicio = dr["DescripcionServicio"].ToString();
                        oServicio.SecuencialServicio = int.Parse(dr["SecuencialServicio"].ToString());
                        oServicio.NomenclaturaServicio = dr["NomenclaturaServicio"].ToString();
                        oServicio.ValorServicio = Convert.ToDecimal(dr["ValorServicio"].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oServicio;
        }

        public List<tbServiciosDgac> ServicioTodos()
        {
            List<tbServiciosDgac> listServicio = new List<tbServiciosDgac>();
            string query = "SELECT ifnull(rtrim(ltrim(SERCOD)), '') as CodigoServicio, ifnull(rtrim(ltrim(SERDES)), '') as DescripcionServicio, ifnull(CAST(SERSEC as INTEGER), 0) as SecuencialServicio, "
                + " ifnull(rtrim(ltrim(SERNOM)), '') AS NomenclaturaServicio , ifnull(SERVAL, 0) AS ValorServicio"
                + " FROM SERARC ";
            iDB2Command cmd;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbServiciosDgac oServicio = new tbServiciosDgac();
                        oServicio.CodigoServicio = dr["CodigoServicio"].ToString();
                        oServicio.DescripcionServicio = dr["DescripcionServicio"].ToString();
                        oServicio.SecuencialServicio = int.Parse(dr["SecuencialServicio"].ToString());
                        oServicio.NomenclaturaServicio = dr["NomenclaturaServici"].ToString();
                        oServicio.ValorServicio = Convert.ToDecimal(dr["ValorServicio"].ToString());
                        listServicio.Add(oServicio);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listServicio;
        }


        public tbServiciosDgac ObstenerAutorizacion(string codServicio)
        {
            //CAMBIO DE SECUENCIAL SERNUM  -  SERSEC
            string query = "SELECT ifnull(rtrim(ltrim(SERNOM)), '') AS Nomenclatura, ifnull(CAST(SERSEC as INTEGER), 0) + 1 AS Secuencial, VARCHAR_FORMAT(CURRENT DATE, 'YYYY') AS ANIO FROM SERARC WHERE SERCOD = '" + codServicio + "'";
            iDB2Command cmd;
            string autorizacion = string.Empty;
            tbServiciosDgac oservicio = new tbServiciosDgac();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oservicio.CodigoServicio = codServicio;
                        oservicio.NomenclaturaServicio = dr["Nomenclatura"].ToString() + FiltraCaracteresInicio('0', 8, dr["Secuencial"].ToString()) + "-" + dr["ANIO"].ToString();
                        oservicio.SecuencialServicio = int.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oservicio;
        }

        /// <summary>
        /// Obtiene el nuevo codifo de la nueva Matricula de la aeronave
        /// </summary>
        /// <param name="codServicio"></param>
        /// <returns></returns>
        public tbServiciosDgac ObstenerNuevaMatriculaDron(string codServicio)
        {
            string query = "SELECT 'HCD' AS Nomenclatura, ifnull(CAST(SERVAL as INTEGER), 0) + 1 AS Secuencial FROM SERARC WHERE SERCOD = '" + codServicio + "'";
            iDB2Command cmd;
            string autorizacion = string.Empty;
            tbServiciosDgac oservicio = new tbServiciosDgac();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oservicio.CodigoServicio = codServicio;
                        oservicio.NomenclaturaServicio = dr["Nomenclatura"].ToString() + FiltraCaracteresInicio('0', 7, dr["Secuencial"].ToString());
                        oservicio.SecuencialServicio = int.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oservicio;
        }


        /// <summary>
        /// Metodo verifica si el el secuencial del servicio esta en cero
        /// </summary>
        /// <param name="codServicio"></param>
        /// <returns></returns>
        public bool VerificaSecuencialMayorCero(string codServicio)
        {
            //CAMBIO DE SECUENCIAL SERNUM  -  SERSEC
            string query = "SELECT ifnull(CAST(SERSEC as INTEGER), 0) AS Secuencial FROM SERARC WHERE SERCOD = '" + codServicio + "'";
            iDB2Command cmd;
            string autorizacion = string.Empty;
            tbServiciosDgac oservicio = new tbServiciosDgac();
            bool estadoVerificacion = false;
            int secuencialAutorizacion = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencialAutorizacion = int.Parse(dr["Secuencial"].ToString());
                        if (secuencialAutorizacion > 0)
                            estadoVerificacion = true;
                        else
                            estadoVerificacion = false;
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                estadoVerificacion = false;
                throw ex;
            }
            return estadoVerificacion;
        }


        public tbServiciosDgac ObtenerAutorizacionVuelosPrivados(string codServicio)
        {
            string query = "SELECT ifnull(rtrim(ltrim(SERNOM)), '') AS Nomenclatura, ifnull(CAST(SERSEC as INTEGER), 0) + 1 AS Secuencial, VARCHAR_FORMAT(CURRENT DATE, 'YYYY') AS ANIO FROM SERARC WHERE SERCOD = '" + codServicio + "'";
            iDB2Command cmd;
            string autorizacion = string.Empty;
            tbServiciosDgac oservicio = new tbServiciosDgac();
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oservicio.CodigoServicio = codServicio;
                        oservicio.NomenclaturaServicio = dr["Nomenclatura"].ToString() + dr["ANIO"].ToString() + "-" + FiltraCaracteresInicio('0', 8, dr["Secuencial"].ToString()) + "-O";
                        oservicio.SecuencialServicio = int.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oservicio;
        }

        public string FiltraCaracteresInicio(char character, int length, string word)
        {
            string result = word;

            for (int i = word.Length; i < length; i++)
            {
                result = character + result;
            }

            return result;
        }

        /// <summary>
        /// Actualiza el secuencial de Servicio DGAC
        /// </summary>
        /// <param name="secuencial"></param>
        /// <param name="codServicio"></param>
        /// <returns></returns>
        public bool ActualizaAutorizacionServicio(int secuencial, string codServicio)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE SERARC SET SERSEC = @Secuencial WHERE SERCOD = @CodServicio";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Secuencial"].Value = secuencial;
                    cmd.Parameters["@CodServicio"].Value = codServicio;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Actulaiza el secuencial de la matricula del dron
        /// </summary>
        /// <param name="secuencial"></param>
        /// <param name="codServicio"></param>
        /// <returns></returns>
        public bool ActualizaSecuencialMatriculaAutorizacionServicio(int secuencial, string codServicio)
        {
            bool respuesta = true;
            string queryInsertar = string.Empty;
            iDB2Command cmd;
            using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
            {
                try
                {
                    queryInsertar = "UPDATE SERARC SET SERVAL = @Secuencial WHERE SERCOD = @CodServicio";
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Secuencial"].Value = secuencial;
                    cmd.Parameters["@CodServicio"].Value = codServicio;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
