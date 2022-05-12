using System.ComponentModel.DataAnnotations;

namespace StudentManagementV2.Api.ViewModels
{
    public class StudentViewModel : UserViewModel
    {
        //[Required(ErrorMessage = "Class is required")]
        public string ClassId { get; set; }

        public StudentViewModel()
        {
        }

        public StudentViewModel(string classId)
        {
            ClassId = classId;
        }
    }
}
