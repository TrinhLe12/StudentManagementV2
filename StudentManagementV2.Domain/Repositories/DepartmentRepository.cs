using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StudentManagementV2.Domain.Repositories
{
    public class DepartmentRepository : Repository<Department, string>, IDepartmentRepository
    {

        public DepartmentRepository(StudentManagementContext context) : base(context)
        {
        }
    }
}
