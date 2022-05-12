using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentManagementV2.Api.ViewModels
{
    public class SubjectRegisterViewModel
    {
        public SubjectRegisterViewModel()
        {
        }

        public SubjectRegisterViewModel(string studentId, string subjectId, string semesterId, short year, decimal? score1, decimal? score2)
        {
            StudentId = studentId;
            SubjectId = subjectId;
            SemesterId = semesterId;
            Year = year;
            Score1 = score1;
            Score2 = score2;
        }

        [Required(ErrorMessage = "Student is required")]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string SubjectId { get; set; }

        [Required(ErrorMessage = "Semester is required")]
        public string SemesterId { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1990-2100")]
        public short Year { get; set; }

        [Range(0, 10, ErrorMessage = "Score must be between 0 and 10")]
        public decimal? Score1 { get; set; }

        [Range(0, 10, ErrorMessage = "Score must be between 0 and 10")]
        public decimal? Score2 { get; set; }

    }
}
