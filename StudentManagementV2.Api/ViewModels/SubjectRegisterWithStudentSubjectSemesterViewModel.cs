using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementV2.Api.ViewModels
{
    public class SubjectRegisterWithStudentSubjectSemesterViewModel
    {
        public SubjectRegisterWithStudentSubjectSemesterViewModel()
        {
        }

        public SubjectRegisterWithStudentSubjectSemesterViewModel(string studentId, string userName, 
            string subjectId, string subjectName, string semesterId, string semesterName, short year, decimal? score1, decimal? score2)
        {
            StudentId = studentId;
            UserName = userName;
            SubjectId = subjectId;
            SubjectName = subjectName;
            SemesterId = semesterId;
            SemesterName = semesterName;
            Year = year;
            Score1 = score1;
            Score2 = score2;
        }

        public string StudentId { get; set; }
        public string UserName { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SemesterId { get; set; }
        public string SemesterName { get; set; }
        public short Year { get; set; }
        public decimal? Score1 { get; set; }
        public decimal? Score2 { get; set; }

    }
}
