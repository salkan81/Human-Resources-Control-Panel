using System.Net;
using System.Net.Mail;

namespace UIHRMP.Mailing.SmtpMailSender
{
    public class SmtpMailServer : IMailService
    {
        private readonly MailOptions _mailOptions;
        public SmtpMailServer(IConfiguration configuration)
        {
            _mailOptions = configuration.GetSection("MailConfig").Get<MailOptions>();
        }

        public void SendEmail(MailTemplate mailTemplate)
        {
            for (int i = 0; i < mailTemplate.To.Length; i++)
            {
                MailMessage message = new(mailTemplate.From,
                                          mailTemplate.To[i],
                                          mailTemplate.Subject,
                                          mailTemplate.Body);

                NetworkCredential netCred = new(_mailOptions.Email, _mailOptions.Password);
                SmtpClient smtpobj = new(_mailOptions.SmtpHost, _mailOptions.SmtpPort);
                smtpobj.EnableSsl = true;
                smtpobj.Credentials = netCred;
                smtpobj.Send(message);
            }
        }
    }
}