using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentManagementV2.Api.ViewModels
{
    public class SubjectViewModel
    {
        public SubjectViewModel()
        {
        }

        public SubjectViewModel(string subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            
        }

        [Required(ErrorMessage = "Subject Id is required")]
        [RegularExpression("^S[0-9]{3}$", ErrorMessage = "Subject Id must match pattern Sxxx")]
        public string SubjectId { get; set; }

        [Required(ErrorMessage = "Subject Name is required")]
        public string SubjectName { get; set; }

    }
}
