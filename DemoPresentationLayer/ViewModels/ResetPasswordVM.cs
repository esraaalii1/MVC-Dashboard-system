using System.ComponentModel.DataAnnotations;

namespace DemoPresentationLayer.ViewModels
{
	public class ResetPasswordVM
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password &Confim Password Do Not Match")]
		public string ConfirmPassword { get; set; }

		public string Email { get; set; }
		public string Token { get; set; }
	}
}
