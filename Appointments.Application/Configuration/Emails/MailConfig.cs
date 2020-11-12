namespace Appointments.Application.Configuration.Emails
{
    /// <summary>
    /// 
    /// </summary>
    public class MailConfig
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public string Domain { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        public string Sender { get; set; }
         
    }
}
