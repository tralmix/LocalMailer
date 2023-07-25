using Microsoft.Extensions.Hosting;
using System.DirectoryServices.AccountManagement;
using System.Net.Mail;

namespace LocalMailer
{
    public class LocalEmailer
    {
        private readonly MailSettings _mailSettings;
        private readonly IHostEnvironment _hostEnvirnoment;
        private readonly SmtpClient _smtpClient;

        public LocalEmailer(MailSettings mailSettings, IHostEnvironment hostEnvironment)
        {
            _mailSettings = mailSettings;
            _hostEnvirnoment = hostEnvironment;

            _smtpClient = NewSmtpClient(mailSettings);
        }

        public async Task SendMailAsync(MailMessage message, CancellationToken cancellationToken = default)
        {
            if (EnvironmentIsLocal())
                UpdateMessageToAddressToLocalUser(message);

            await _smtpClient.SendMailAsync(message, cancellationToken);
        }

        private static void UpdateMessageToAddressToLocalUser(MailMessage message)
        {
            if (OperatingSystem.IsWindows())
            {
                message.To.Clear();
                message.To.Add(UserPrincipal.Current.EmailAddress);
                return;
            }
        }

        private bool EnvironmentIsLocal()
        {
            return _hostEnvirnoment.IsEnvironment(_mailSettings.LocalEnvironment);
        }

        private static SmtpClient NewSmtpClient(MailSettings mailSettings)
        {
            SmtpClient client = new()
            {
                Host = mailSettings.Host,
                Port = mailSettings.Port,
                UseDefaultCredentials = mailSettings.UseDefaultCredentials,
                DeliveryMethod = mailSettings.SmtpDeliveryMethod,
                PickupDirectoryLocation = mailSettings.PickupDirectoryLocation
            };

            if(mailSettings.Timeout is not null) 
                client.Timeout = mailSettings.Timeout.Value;

            return client;
        }
    }
}