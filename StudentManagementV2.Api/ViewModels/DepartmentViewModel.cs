using System.ComponentModel.DataAnnotations;

namespace StudentManagementV2.Api.ViewModels
{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "Department Id is required")]
        [RegularExpression("^D[0-9]{2}$", ErrorMessage = "Department Id must match pattern Dxx")]
        public string DeptId { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public string DeptName { get; set; }

        public DepartmentViewModel()
        {
        }

        public DepartmentViewModel(string deptId, string deptName)
        {
            DeptId = deptId;
            DeptName = deptName;
        }
    }
}