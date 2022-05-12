using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class Role
    {
        public Role()
        {
        }

        public Role(string roleId, string roleName, ICollection<User> users)
        {
            RoleId = roleId;
            RoleName = roleName;
            Users = users;
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
