namespace StudentManagementV2.Api.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        public LoginViewModel()
        {
        }

        public LoginViewModel(string email, string password, bool rememberMe, string returnUrl)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
            ReturnUrl = returnUrl;
        }
    }
}
