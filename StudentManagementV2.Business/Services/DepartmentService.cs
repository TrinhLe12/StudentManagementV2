using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class DepartmentService : Service<Department, string>, IDepartmentService
    {

        public DepartmentService(IRepository<Department, string> Repository) : base(Repository)
        {
        }

        public override void Delete(string id)
        {
            Repository.Delete(GetById(id));
        }

        public override void Save(Department entity)
        {
            // Validate
            Repository.Save(entity);
        }

        public override void Update(Department entity)
        {
            Department department = GetById(entity.DeptId);

            if (department != null)
            {
                department.DeptName = entity.DeptName;
                Repository.Update(department);
            }
            
        }

        
    }
}
