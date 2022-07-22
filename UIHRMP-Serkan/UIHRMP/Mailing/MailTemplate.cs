using System.Text.Json;

namespace UIHRMP.Mailing
{
    public class MailTemplate
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] To { get; set; }

        public MailTemplate(string from, string subject, string body, params string[] to)
        {
            From = from;
            Subject = subject;
            Body = body;
            To = to;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}