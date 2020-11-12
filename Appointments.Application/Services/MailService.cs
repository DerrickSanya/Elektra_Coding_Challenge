using Appointments.Application.Configuration.Emails;
using Appointments.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Appointments.Application.Services
{
    /// <summary>
    /// MailService
    /// </summary>
    public class MailService : IMailService
    {
        /// <summary>
        /// _options
        /// </summary>
        private readonly IOptions<MailConfig> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public MailService(IOptions<MailConfig> options)
        {
            _options = options;
        }

        /// <summary>
        /// Sends the mail message.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="mailBody">The mail body.</param>
        public async Task SendEmailMessage(string recipient, string subject, string mailBody, bool isHtmlEmail = false)
        {
            using var mailClient = new SmtpClient(_options.Value.Host);
            using var emailMessage = new MailMessage(_options.Value.Sender, recipient, subject, mailBody);
            emailMessage.IsBodyHtml = isHtmlEmail;

            // await mailClient.SendMailAsync(emailMessage);
            Debug.WriteLine($"Email Sent to {recipient}");
            Debug.WriteLine($"Email Subject: {subject}");
            Debug.WriteLine($"Email Message: {mailBody}");
            // dummy operation to emulate the 
            await Task.CompletedTask;
        }
    }
}
