using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class Semester
    {
        public Semester()
        {
        }

        public Semester(string semesterId, string semesterName, ICollection<SubjectRegister> subjectRegisters)
        {
            SemesterId = semesterId;
            SemesterName = semesterName;
            SubjectRegisters = subjectRegisters;
        }

        public string SemesterId { get; set; }
        public string SemesterName { get; set; }

        public virtual ICollection<SubjectRegister> SubjectRegisters { get; set; }
    }
}
