using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public interface IStudentService
    {
        Student GetStudentByIdentityId(string identityId);

        List<Student> GetStudentByAssignClass(string classId);
    }
}
