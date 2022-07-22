namespace UIHRMP.Mailing.FakeMailSender
{
    public class FakeMailSender : IMailService
    {
        public void SendEmail(MailTemplate mailTemplate)
        {
            Console.WriteLine(mailTemplate);
        }
    }
}
