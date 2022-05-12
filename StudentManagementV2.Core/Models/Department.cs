using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class Department
    {
        public Department()
        {
        }

        public Department(string deptId, string deptName, ICollection<AssignClass> assignClasses, ICollection<Instructor> instructors)
        {
            DeptId = deptId;
            DeptName = deptName;
            AssignClasses = assignClasses;
            Instructors = instructors;
        }

        public string DeptId { get; set; }
        public string DeptName { get; set; }

        public virtual ICollection<AssignClass> AssignClasses { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
