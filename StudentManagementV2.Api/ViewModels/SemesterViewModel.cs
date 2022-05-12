using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentManagementV2.Api.ViewModels
{
    public class SemesterViewModel
    {
        public SemesterViewModel()
        {
        }

        public SemesterViewModel(string semesterId, string semesterName)
        {
            SemesterId = semesterId;
            SemesterName = semesterName;
        }

        [Required(ErrorMessage = "Semester Id is required")]
        [RegularExpression("^SE[0-9]{2}$", ErrorMessage = "Semester Id must match pattern SExx")]
        public string SemesterId { get; set; }

        [Required(ErrorMessage = "Semester Name is required")]
        public string SemesterName { get; set; }

    }
}
