using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;

namespace DemoPresentationLayer.Utilities
{
	public class Email
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Recipient { get; set; }
		public static class MailSetting
		{
			public static void SendEmail(Email email)
			{
				var client = new SmtpClient("smtp.gmail.com", 587);
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("sa1242257@gmail.com", "qdgqxqkicagfrtdh");
				client.Send("sa1242257@gmail.com", email.Recipient, email.Subject, email.Body);
			}
		}
	}
}
