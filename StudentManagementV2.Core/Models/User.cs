using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public abstract partial class User
    {
        public User()
        {
        }

        public User(string userId, string userName, string roleId, string email, string phone, string address, 
            DateTime? birthdate, Role role, ICollection<Loggin> loggins, ICollection<SubjectRegister> subjectRegisters,
            string identityId)
        {
            UserId = userId;
            UserName = userName;
            RoleId = roleId;
            Email = email;
            Phone = phone;
            Address = address;
            Birthdate = birthdate;
            Role = role;
            Loggins = loggins;
            SubjectRegisters = subjectRegisters;
            IdentityId = identityId;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }

        public string IdentityId { get; set; }

        
        
        public virtual Role Role { get; set; }
        public virtual ICollection<Loggin> Loggins { get; set; }
        public virtual ICollection<SubjectRegister> SubjectRegisters { get; set; }
    }
}
