using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class RoleService : Service<Role, string>, IRoleService
    {
        public RoleService(IRepository<Role, string> Repository) : base(Repository)
        {
        }

        public override void Delete(string id)
        {
            Repository.Delete(Repository.GetById(id));
        }

        public override void Save(Role entity)
        {
            if(Repository.GetById(entity.RoleId) == null)
            {
                Repository.Save(entity);
            }
        }

        public override void Update(Role entity)
        {
            Role role = Repository.GetById(entity.RoleId);
            
            if(role != null)
            {
                role.RoleName = entity.RoleName;

                Repository.Update(role);
            }
        }
    }
}
