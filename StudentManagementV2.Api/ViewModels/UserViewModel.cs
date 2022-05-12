using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementV2.Api.ViewModels
{
    public abstract class UserViewModel
    {
        [Required(ErrorMessage = "User Id is required")]
        [RegularExpression("^U[0-9]{4}$", ErrorMessage = "User Id must match pattern Uxxxx")]
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        public string RoleId { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^([0-9]{10}|[0-9]{11})$", ErrorMessage = "Phone must consist of 10-11 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime? Birthdate { get; set; }

        public string IdentityId { get; set; }

        public string ReturnUrl { get; set; }

        protected UserViewModel()
        {
        }

        protected UserViewModel(string userId, string userName, string roleId, string email, string phone, string address, DateTime? birthdate, string identityId, string returnUrl)
        {
            UserId = userId;
            UserName = userName;
            RoleId = roleId;
            Email = email;
            Phone = phone;
            Address = address;
            Birthdate = birthdate;
            IdentityId = identityId;
            ReturnUrl = returnUrl;
        }
    }
}
