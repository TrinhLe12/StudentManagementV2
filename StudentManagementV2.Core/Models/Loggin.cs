using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class Loggin
    {
        public Loggin()
        {
        }

        public Loggin(string logginId, string logginName, string password, string userId, User user)
        {
            LogginId = logginId;
            LogginName = logginName;
            Password = password;
            UserId = userId;
            User = user;
        }

        public string LogginId { get; set; }
        public string LogginName { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
