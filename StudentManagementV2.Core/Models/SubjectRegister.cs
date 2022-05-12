using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class SubjectRegister
    {
        public SubjectRegister()
        {
        }

        public SubjectRegister(string studentId, string subjectId, string semesterId, short year, decimal? score1, decimal? score2, Semester semester, User student, Subject subject)
        {
            StudentId = studentId;
            SubjectId = subjectId;
            SemesterId = semesterId;
            Year = year;
            Score1 = score1;
            Score2 = score2;
            Semester = semester;
            Student = student;
            Subject = subject;
        }

        public string StudentId { get; set; }
        public string SubjectId { get; set; }
        public string SemesterId { get; set; }
        public short Year { get; set; }
        public decimal? Score1 { get; set; }
        public decimal? Score2 { get; set; }

        public virtual Semester Semester { get; set; }
        public virtual User Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
