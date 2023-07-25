using System.Net.Mail;

namespace LocalMailerExample
{
    public class ExampleMailMessage : MailMessage
    {
        public ExampleMailMessage()
        {
            From = new MailAddress("noreply@mydomain.com");
            Body = "This is an example email. The to address of this email should be only your user email.";
        }
    }
}
