using System.ComponentModel.DataAnnotations;

namespace DemoPresentationLayer.ViewModels
{
	public class ForgetPasswordVM
	{
		[EmailAddress]
		public string Email { get; set; }
	}
}
