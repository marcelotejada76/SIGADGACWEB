using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DepositosCompanias.Utilitario
{
    public class EnviarCorreo
    {
        public bool enviaMensajeCorreo(string coreoPara, string asunto, string mensajeDetalle)
        {
            bool estado = false;
            try
            {
                MailMessage correo = new MailMessage();
                //no_reply@aviacioncivil.gob.ec
                //opsuio.sobrevuelos@aviacioncivil.gob.ec
                correo.From = new MailAddress("no_reply@aviacioncivil.gob.ec"); // Correo electronico que usara nuestra aplicacion mvc para enviar correos
                correo.To.Add(coreoPara);
                correo.Subject = asunto;
                correo.Body = mensajeDetalle;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                //Configuracion del servidor smtp
                SmtpClient smtp = new SmtpClient("172.20.16.21");
                //SmtpClient smtp = new SmtpClient("172.20.17.87");
                smtp.Send(correo);
                estado = true;
            }
            catch
            {
                estado = false;
            }
            return estado;
        }

        //Correo institucional
        public bool enviaMensajeCorreo(string correoPara, string correoDestino, string asunto, string mensajeDetalle)
        {
            bool estado = false;
            try
            {
                MailMessage correo = new MailMessage();
                //no_reply@aviacioncivil.gob.ec
                //opsuio.sobrevuelos@aviacioncivil.gob.ec
                correo.From = new MailAddress(correoPara);
                correo.To.Add(correoDestino);
                correo.Subject = asunto;
                correo.Body = mensajeDetalle;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                //Configuracion del servidor smtp
                SmtpClient smtp = new SmtpClient("172.20.16.21");
                //SmtpClient smtp = new SmtpClient("172.20.17.87");
                smtp.Send(correo);
                estado = true;
            }
            catch
            {
                estado = false;
            }
            return estado;
        }

        private bool enviaMensajeCorreoGmail(string correoPara, string correoDestino, string asunto, string mensajeDetalle)
        {
            bool estado = false;
            try
            {
                //Parte 1
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("EMAIL", "PASSWORD");

                //Parte 2

                MailMessage mm = new MailMessage();
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.Normal;
                mm.From = new MailAddress(correoPara);
                mm.Subject = asunto;
                mm.Body = mensajeDetalle;
                //mm.Body += "<p>Este es un mensaje de prueba</p>";
                mm.To.Add(new MailAddress(correoDestino));
                smtp.Send(mm); // Enviar el mensaje

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }
    }
}