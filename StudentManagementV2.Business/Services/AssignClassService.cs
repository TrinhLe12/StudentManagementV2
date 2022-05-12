using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class AssignClassService : Service<AssignClass, string>, IAssignClassService
    {
        // Inject additional service for Department through Constructor
        protected IService<Department, string> DepartmentService { get; set; }

        public AssignClassService(IRepository<AssignClass, string> Repository, IService<Department, string> DepartmentService) : base(Repository)
        {
            this.DepartmentService = DepartmentService;
        }

        public override void Delete(string id)
        {
            Repository.Delete(Repository.GetById(id));
        }

        public override void Save(AssignClass entity)
        {
            
            if (Repository.GetById(entity.ClassId) == null && DepartmentService.GetById(entity.DeptId) != null)
            {
                AssignClass assignClass = new AssignClass();

                assignClass.ClassId = entity.ClassId;
                assignClass.ClassName = entity.ClassName;
                assignClass.Dept = DepartmentService.GetById(entity.DeptId);

                Repository.Save(entity);
            }

        }

        public override void Update(AssignClass entity)
        {
            AssignClass assignClass = Repository.GetById(entity.ClassId);

            if (assignClass != null)
            {
                assignClass.ClassName = entity.ClassName;

                Department department = DepartmentService.GetById(entity.DeptId);

                if (department != null)
                {
                    
                    assignClass.Dept = department;

                }

                Repository.Update(assignClass);

            }
        }

    }
}
