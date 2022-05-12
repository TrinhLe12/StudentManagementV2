using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementV2.Core.Models;

namespace StudentManagementV2.Domain.Repositories
{
    public class AssignClassRepository : Repository<AssignClass, string>, IAssignClassRepository
    {
        public AssignClassRepository(StudentManagementContext context) : base(context)
        {
        }
    }
}
