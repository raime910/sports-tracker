using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StatTrack.BLL.DataManagers.Security
{

	/// <summary>
	/// Email service for Identity Core.
	/// </summary>
	internal class MessageService : IIdentityMessageService
	{
		/// <summary>
		/// Send email to a user.
		/// </summary>
		/// <param name="message">Message information wrapper for identity core.</param>
		/// <returns></returns>
		public async Task SendAsync(IdentityMessage message)
		{
			using (var smtpClient = new SmtpClient())
			{
				var mailMsg = new MailMessage
				{
					Subject = message.Subject,
					Body = message.Body,
					IsBodyHtml = true
				};

				mailMsg.To.Add(new MailAddress(message.Destination));

				await smtpClient.SendMailAsync(mailMsg);
			}
		}
	}
}
