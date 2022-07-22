namespace UIHRMP.Mailing.SmtpMailSender
{
    public class MailOptions
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}
