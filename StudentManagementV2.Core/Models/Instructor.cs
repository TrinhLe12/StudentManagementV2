using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Core.Models
{
    public class Instructor : User
    {
        public string DeptId { get; set; }
        public virtual Department Dept { get; set; }

        public Instructor()
        {
        }

        public Instructor(string deptId, Department dept)
        {
            DeptId = deptId;
            Dept = dept;
        }
    }
}
