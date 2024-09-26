using System.ComponentModel.DataAnnotations;

namespace DemoPresentationLayer.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
