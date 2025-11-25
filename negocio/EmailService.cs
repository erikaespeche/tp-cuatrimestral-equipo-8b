using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("maiianaiir@gmail.com", "zrji wqtl xevf xcqb");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host= "smtp.gmail.com";

        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@programacion.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml=true;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ;

        }
    }
}
