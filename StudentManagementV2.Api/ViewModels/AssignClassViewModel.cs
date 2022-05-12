using System.ComponentModel.DataAnnotations;

namespace StudentManagementV2.Api.ViewModels
{
    public class AssignClassViewModel
    {
        [Required(ErrorMessage = "Class Id is required")]
        [RegularExpression("^C[0-9]{3}$", ErrorMessage = "Class Id must match pattern Cxxx")]
        public string ClassId { get; set; }

        [Required(ErrorMessage = "Class Name is required")]
        public string ClassName { get; set; }

        //[Required(ErrorMessage = "Department is required")]
        public string DeptId { get; set; }

        public AssignClassViewModel()
        {
        }

        public AssignClassViewModel(string classId, string className, string deptId)
        {
            ClassId = classId;
            ClassName = className;
            DeptId = deptId;
        }
    }
}
