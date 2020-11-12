namespace Appointments.Application.Services.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// IMailService
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends the mail message.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="mailBody">The mail body.</param>
        /// <returns></returns>
        Task SendEmailMessage(string recipient, string subject, string mailBody, bool isHtmlEmail = false);
    }
}