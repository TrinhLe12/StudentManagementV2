using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public class SubjectRepository : Repository<Subject, string>, ISubjectRepository
    {
        public SubjectRepository(StudentManagementContext context) : base(context)
        {
        }
    }
}
