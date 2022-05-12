using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class AssignClass
    {
        public AssignClass()
        {
        }

        public AssignClass(string classId, string className, string deptId, Department dept, ICollection<Student> students)
        {
            ClassId = classId;
            ClassName = className;
            DeptId = deptId;
            Dept = dept;
            Students = students;
        }

        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string DeptId { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
