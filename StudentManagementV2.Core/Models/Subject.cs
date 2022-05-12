using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class Subject
    {
        public Subject()
        {
        }

        public Subject(string subjectId, string subjectName, ICollection<SubjectRegister> subjectRegisters)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            SubjectRegisters = subjectRegisters;
        }

        public string SubjectId { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<SubjectRegister> SubjectRegisters { get; set; }
    }
}
