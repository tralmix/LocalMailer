using System.Net.Mail;

namespace LocalMailer
{
    public class MailSettings
    {
        private const string DefaultLocalEnvironment = "Local";
        private string? _localEnvironment;
        private string? _host;
        private int? _port;

        public string LocalEnvironment
        {
            get
            {
                return _localEnvironment ?? DefaultLocalEnvironment;
            }
            set
            {
                _localEnvironment = value;
            }
        }

        public string Host
        {
            get
            {
                return _host ?? throw new ArgumentNullException(nameof(Host), "A server host must be define in Mail Settings.");
            }
            set
            {
                _host = value;
            }
        }

        public int Port
        {
            get
            {
                return _port ?? throw new ArgumentNullException(nameof(Port), "A server port must be define in Mail Settings.");
            }
            set
            {
                _port = value;
            }
        }

        public int? Timeout { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public string DeliveryMethod { get; set; } = string.Empty;

        public SmtpDeliveryMethod SmtpDeliveryMethod => (SmtpDeliveryMethod)Enum.Parse(typeof(SmtpDeliveryMethod), DeliveryMethod);

        public string? PickupDirectoryLocation { get; set; }

    }
}