using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Core.Models
{
    public class Student : User
    {
        public string ClassId { get; set; }
        public virtual AssignClass Class { get; set; }

        public Student()
        {
        }

        public Student(string classId, AssignClass @class)
        {
            ClassId = classId;
            Class = @class;
        }
    }
}
