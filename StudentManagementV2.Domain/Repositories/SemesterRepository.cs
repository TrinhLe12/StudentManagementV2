using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public class SemesterRepository : Repository<Semester, string>, ISemesterRepository
    {
        public SemesterRepository(StudentManagementContext context) : base(context)
        {
        }
    }
}
