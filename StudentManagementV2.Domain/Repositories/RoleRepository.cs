using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public class RoleRepository : Repository<Role, string>, IRoleRepository
    {
        public RoleRepository(StudentManagementContext context) : base(context)
        {
        }
    }
}
