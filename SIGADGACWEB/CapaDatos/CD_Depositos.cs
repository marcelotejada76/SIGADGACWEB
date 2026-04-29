using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Depositos
    {
        public static CD_Depositos _instancia = null;
        private CD_Depositos()
        {

        }

        public static CD_Depositos Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Depositos();
                }
                return _instancia;
            }
        }

        //carga clientes todos consulta
        public List<tbSubirDepositos> DetalleDepositosVista()
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS from ficar6 " +
                    "where ficnu9 >0 ORDER BY FICANO,FICMES,ficemp");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.Mes = dr["MES"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }



        //carga clientes todos cliente
        public List<tbSubirDepositos> DetalleDepositosVistaCliente(string Cliente)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {



                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS from ficar6 " +
                    "where ficnu9 >0 and ficemp LIKE ('%" + Cliente + "%')ORDER BY FICANO,FICMES,ficemp");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString();
                        oSolicitud.Mes = dr["MES"].ToString();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }




        public List<tbSubirDepositos> DetalleDepositos(string canio, string ruc)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS from ficar6 " +
                    "where ficano='" + canio + "' and ficru1='" + ruc + "' and fices2='1' ORDER BY FICANO,FICMES");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString().Trim();
                        oSolicitud.Mes = dr["MES"].ToString().Trim();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString().Trim();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString().Trim();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }

        //eliminar registro
        public tbSubirDepositos EliminarRegistros(string canio, string ruc, string mes, int registros, string nombreArchivo)
        {

            string Ruc = Regex.Match(ruc, @"^\d+").Value;
            Ruc = Ruc.Trim();
            int numreg = 0;

            var registrosTotales = ConsultaDepositosClientecarpeta(canio, Ruc, mes);

            foreach (var item in registrosTotales)
            {
                numreg = item.Registros;

            }

            numreg = numreg - registros;

            //List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {

                //actualiza registros


                sbSol.Append("update ficar6 set ficnu9= " + numreg + " where ficano='" + canio + "' and ficru1='" + Ruc + "' and ficmes='" + mes + "'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    oConexion.Close();
                }

                //graba datos depositos
                sbSol.Clear();

                sbSol.Append("delete from fidar7 where FIDANO ='" + canio + "' and fidru3='" + Ruc + "' and fidmes='" + mes + "' and FIDNO7= '" + nombreArchivo + "'");

                query = sbSol.ToString();
                //    iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        //actualiza numero de registros en la taba
        public tbSubirDepositos ActualizaRegistros(string canio, string ruc, string mes, int registros, string nombreArchivo, string comprobante, string fechadeposito, string concepto)
        {
            string fecha = "";
            if (fechadeposito != "")
            {
                fecha = DateTime.Parse(fechadeposito).ToString("yyyyMMdd");

                //fecha = fechadeposito.ToString("yyyyMMdd");
            }
            //List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("update ficar6 set ficnu9= " + registros + " where ficano='" + canio + "' and ficru1='" + ruc + "' and ficmes='" + mes + "'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    oConexion.Close();
                }

                //graba datos depositos
                sbSol.Clear();

                sbSol.Append("INSERT INTO fidar7 (FIDANO,FIDMES,FIDRU3,FIDNO7,FIDNU3,FIDF11,FIDCO5) values('"
                    + canio +
                           "','" + mes.Trim() +
                           "','" + ruc.Trim() +
                           "','" + nombreArchivo.Trim() +
                           "','" + comprobante.Trim() +
                           "','" + fecha.Trim() +
                           "','" + concepto.Trim() + "')");

                query = sbSol.ToString();
                //    iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        //consulta todos los clientes
        public List<tbSubirDepositos> ConsultaDepositos(string canio)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS  from ficar6 " +
                    "where ficano='" + canio + "' and fices2='1' and ficnu9 >=1 ");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString().Trim();
                        oSolicitud.Mes = dr["MES"].ToString().Trim();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString().Trim();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString().Trim();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }
        //consulta datos por carpeta y cliente
        public List<tbSubirDepositos> ConsultaDepositosClientecarpeta(string canio, string ruc, string mes)
        {
            List<tbSubirDepositos> listarSolicitud = new List<tbSubirDepositos>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT FICANO AS AÑO,FICMES AS MES,FICRU1 AS RUC,FICEMP AS RAZONSOCIAL, FICNU9 AS REGISTROS  from ficar6 " +
                    "where ficano='" + canio + "' and ficmes='" + mes + "' and ficru1='" + ruc + "'");
                //sbSol.Append("FROM DGACDAT.SOLAR1 WHERE SOLAN1 = '" + canio + "' AND SOLTIP='" + tipoSolicitud + "' AND SOLCO5 = '" + cdireccion + "'");
                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbSubirDepositos oSolicitud = new tbSubirDepositos();
                        oSolicitud.Año = dr["AÑO"].ToString().Trim();
                        oSolicitud.Mes = dr["MES"].ToString().Trim();
                        oSolicitud.UsuarioRuc = dr["RUC"].ToString().Trim();
                        oSolicitud.RazonSocial = dr["RAZONSOCIAL"].ToString().Trim();
                        oSolicitud.Registros = Convert.ToInt16(dr["REGISTROS"].ToString());
                        //  oSolicitud.Compania_Contratista = dr["COMPANIA_CONTRATISTA"].ToString();



                        listarSolicitud.Add(oSolicitud);
                    }
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }
        //obtener datos del deposito
        public List<tbModelArchivo> DatosDeposito(string canio, string ruc, string mes, string nombrearchivo)
        {
            List<tbModelArchivo> listarSolicitud = new List<tbModelArchivo>();
            // string query = "SELECT IFNULL(max(OPSSEC), 0) + 1 AS Secuencial FROM OPSARC WHERE OPSAER = '" + aeropuerto + "' AND OPSANO = '" + anio + "'";
            string query = "SELECT *from fidar7 WHERE fidano= '" + canio + "' and fidmes='" + mes + "' and fidru3='" + ruc + "' and fidno7='" + nombrearchivo.Trim() + "'";
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
                        tbModelArchivo oSolicitud = new tbModelArchivo();

                        oSolicitud.Comprobante = dr["FIDNU3"].ToString().Trim();
                        oSolicitud.FechaDeposito = dr["FIDF11"].ToString().Trim();
                        oSolicitud.Concepto = dr["FIDCO5"].ToString().Trim();


                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                }

            }
            catch (Exception ex)
            {

            }
            return listarSolicitud;
        }

        //recupera correos usuarios
        public static string CorreosUsuario(string Correos)
        {
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            try
            {
                sbSol.Append("select USUCOR from dgacdatpro.envarc LEFT JOIN USUARC ON USUCOD = ENVCOD " +
                    " LEFT JOIN USUAR1 ON USUCO8 = ENVCOD  where ENVCO3 = 'DEPO' AND ENVEST = 'AC'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Correos = Correos + dr.GetString(0).Trim() + ",";

                    }
                    Correos = Correos.TrimEnd(',').ToLower();
                    oConexion.Close();
                }
            }
            catch (Exception e)
            {


            }


            return Correos;

        }

        //recupera correos empresa
        public static string CorreoEmpresa(string Correos, string Ruc)
        {
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;

            try
            {
                sbSol.Append("select USUCOR from USUARC WHERE USUNUM='"+ Ruc+"'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Correos = Correos + dr.GetString(0).Trim() + ",";

                    }
                    Correos = Correos.TrimEnd(',').ToLower();
                    oConexion.Close();
                }
            }
            catch (Exception e)
            {


            }


            return Correos;

        }
        //envio correo
        public static void EnviarCorreo(string canio, string ruc, string mes, int registros, string nombreArchivo, string comprobante, string fechadeposito, string concepto, string RazonSocial)
        {
            string fechadep = DateTime.Parse(fechadeposito).ToString("yyyyMMdd");
            //string fechadep = fechadeposito.ToString("yyyyMMdd");

            string sTextoMail = string.Empty;

            //sTextoMail += " <br/> Estimad@ : " + "<b>" + nombreagente + "</b>";
            sTextoMail += " <br/> Estimad@s : ";
            sTextoMail += " <br/><br/> Se adjunta Informacion del documento deposito  por la Cia.  "+ruc+" / "+ RazonSocial+ "  con fecha : " + fechadeposito + "";

            //  sTextoMail += " <br/><br/> Año " + canio + " Mes " + mes + "NombreArchivo " + nombreArchivo + " Comprobante " + comprobante + "Fecha Deposito" + fechadeposito + "Concepto " + concepto + "</b></FONT>";

            sTextoMail += @"
<br/><br/>
<table border='0' cellpadding='8' cellspacing='0' style='border-collapse:collapse;font-family:Arial;font-size:13px;width:100%;'>
    
    <tr style='background-color:#003366; color:white; text-align:left;'>
        <th>Año</th>
        <th>Mes</th>
        <th>Nombre Archivo</th>
        <th>Comprobante</th>
        <th>Fecha Depósito</th>
        <th>Concepto</th>
    </tr>

    <tr style='background-color:#f9f9f9; color:#333;'>
        <td>" + canio + @"</td>
        <td>" + mes + @"</td>
        <td>" + nombreArchivo + @"</td>
        <td>" + comprobante + @"</td>
        <td>" + fechadep + @"</td>
        <td style='max-width:300px; word-wrap:break-word;'>" + concepto + @"</td>
    </tr>

</table>";



            string noreply = "no_reply@aviacioncivil.gob.ec";
            string asunto = "Nuevo Deposito. " + nombreArchivo+" Cia "+RazonSocial;

            //sTextoMail += "<br/><br/><br/> Debe ingresar por la Opción Anulación Tarjetas de Crédito Switch, ingresar el código de tarjeta y Número de Referencia.";
            //sTextoMail += " <br/><br/>  Si desea consultar el Numero de Tarjeta de Credito, Ingresar en el menu SIGETAME, CONSULTAS, TRANSACCIONES SWITCH T/C,F7 Busqueda por Referencia";
            sTextoMail += "<br/><br/><br/> Por favor no responda a este correo.";
            sTextoMail += "<br/><br/> Saludos Cordiales";
            sTextoMail += "<br/><br/><br/><br/>";

            try
            {
                //RECUPERA CORREOS
                string Correousuario1 = "";
                var Correos = CorreosUsuario(Correousuario1);

                //  var Correos = email,emailusuario;// "marcelo.tejada@aviacioncivil.gob.ec;jimmy.sandoval@aviacioncivil.gob.ec,cesar.maldonado@aviacioncivil.gob.ec";
                // var Correos = "marcelo.tejada@aviacioncivil.gob.ec";
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(noreply); // Correo electronico que usara nuestra aplicacion mvc para enviar correos

                try
                {
                    //correo.To.Add(email);
                    // correo.To.Add(Correos);
                    correo.To.Add("MARCELO.TEJADA@aviacioncivil.gob.ec");
                }
                catch (Exception ex)
                {

                    //throw;
                }

               

                //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                //Attachment a = new Attachment(fs, NombreArchivo, MediaTypeNames.Application.Pdf);
                //correo.Attachments.Add(a);


                correo.Subject = asunto;
                correo.Body = sTextoMail;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //Configuracion del servidor smtp
                // SmtpClient smtp = new SmtpClient("172.20.16.21");
                SmtpClient smtp = new SmtpClient("172.20.17.87");
                smtp.Send(correo);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }



        }//fin funcion
         //envio correo A LA COMPAÑIA
        public static void EnviarCorreoRuc(string canio, string ruc, string mes, int registros, string nombreArchivo, string comprobante, string fechadeposito, string concepto, string RazonSocial)
        {
            string fechadep = DateTime.Parse(fechadeposito).ToString("yyyyMMdd");
            //string fechadep = fechadeposito.ToString("yyyyMMdd");

            string sTextoMail = string.Empty;

            //sTextoMail += " <br/> Estimad@ : " + "<b>" + nombreagente + "</b>";
            sTextoMail += " <br/> Estimad@s : " + ruc + " / " + RazonSocial;
            sTextoMail += " <br/><br/> Se adjunta Informacion del documento Recaudado";

            //  sTextoMail += " <br/><br/> Año " + canio + " Mes " + mes + "NombreArchivo " + nombreArchivo + " Comprobante " + comprobante + "Fecha Deposito" + fechadeposito + "Concepto " + concepto + "</b></FONT>";

            sTextoMail += @"
<br/><br/>
<table border='0' cellpadding='8' cellspacing='0' style='border-collapse:collapse;font-family:Arial;font-size:13px;width:100%;'>
    
    <tr style='background-color:#003366; color:white; text-align:left;'>
        <th>Año</th>
        <th>Mes</th>
        <th>Nombre Archivo</th>
        <th>Comprobante Recaudado</th>
        <th>Fecha Recaudación</th>
        <th>Concepto</th>
    </tr>

    <tr style='background-color:#f9f9f9; color:#333;'>
        <td>" + canio + @"</td>
        <td>" + mes + @"</td>
        <td>" + nombreArchivo + @"</td>
        <td>" + comprobante + @"</td>
        <td>" + fechadep + @"</td>
        <td style='max-width:300px; word-wrap:break-word;'>" + concepto + @"</td>
    </tr>

</table>";



            string noreply = "no_reply@aviacioncivil.gob.ec";
            string asunto = "Nueva Recaudacion. " + nombreArchivo + " DGAC" ;

            //sTextoMail += "<br/><br/><br/> Debe ingresar por la Opción Anulación Tarjetas de Crédito Switch, ingresar el código de tarjeta y Número de Referencia.";
            //sTextoMail += " <br/><br/>  Si desea consultar el Numero de Tarjeta de Credito, Ingresar en el menu SIGETAME, CONSULTAS, TRANSACCIONES SWITCH T/C,F7 Busqueda por Referencia";
            sTextoMail += "<br/><br/><br/> Por favor no responda a este correo.";
            sTextoMail += "<br/><br/> Saludos Cordiales";
            sTextoMail += "<br/><br/><br/><br/>";

            try
            {
                //RECUPERA CORREOS
                string Correousuario1 = "";
                var Correos = CorreoEmpresa(Correousuario1, ruc);

                //  var Correos = email,emailusuario;// "marcelo.tejada@aviacioncivil.gob.ec;jimmy.sandoval@aviacioncivil.gob.ec,cesar.maldonado@aviacioncivil.gob.ec";
                // var Correos = "marcelo.tejada@aviacioncivil.gob.ec";
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(noreply); // Correo electronico que usara nuestra aplicacion mvc para enviar correos

                try
                {
                    //correo.To.Add(email);
                    correo.To.Add(Correos);
                }
                catch (Exception ex)
                {

                    //throw;
                }



                //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                //Attachment a = new Attachment(fs, NombreArchivo, MediaTypeNames.Application.Pdf);
                //correo.Attachments.Add(a);


                correo.Subject = asunto;
                correo.Body = sTextoMail;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //Configuracion del servidor smtp
                // SmtpClient smtp = new SmtpClient("172.20.16.21");
                SmtpClient smtp = new SmtpClient("172.20.17.87");
                smtp.Send(correo);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }



        }//fin funcion


    }
}
