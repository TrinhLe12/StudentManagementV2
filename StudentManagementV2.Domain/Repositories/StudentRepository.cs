using Microsoft.EntityFrameworkCore;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public class StudentRepository : Repository<Student, string>, IStudentRepository
    {
        public StudentRepository(StudentManagementContext context) : base(context)
        {
        }

        public List<Student> GetStudentByAssignClass(string classId)
        {
            return Context.Students.Where(s => s.ClassId.Equals(classId)).ToList();
        }

        public Student GetStudentByIdentityId(string identityId)
        {
            return Context.Students.Where(s => s.IdentityId.Equals(identityId)).FirstOrDefault();
        }

    }
}
