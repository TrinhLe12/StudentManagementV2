using System.ComponentModel.DataAnnotations;

namespace StudentManagementV2.Api.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Confirm Password must match Password")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }

        public RegisterViewModel()
        {
        }

        public RegisterViewModel(string email, string password, string confirmPassword, string returnUrl)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            ReturnUrl = returnUrl;
        }
    }
}
