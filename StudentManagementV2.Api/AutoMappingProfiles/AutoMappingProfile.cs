using AutoMapper;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.Core.Models;

namespace StudentManagementV2.Api.AutoMappingProfiles
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<DepartmentViewModel, Department>();
            CreateMap<Department, DepartmentViewModel>();

            CreateMap<AssignClassViewModel, AssignClass>();
            CreateMap<AssignClass, AssignClassViewModel>();

            CreateMap<StudentViewModel, Student>();
            CreateMap<Student, StudentViewModel>();

            CreateMap<SemesterViewModel, Semester>();
            CreateMap<Semester, SemesterViewModel>();

            CreateMap<SubjectViewModel, Subject>();
            CreateMap<Subject, SubjectViewModel>();

            CreateMap<SubjectRegisterViewModel, SubjectRegister>();
            CreateMap<SubjectRegister, SubjectRegisterViewModel>();

            CreateMap<SubjectRegister, SubjectRegisterWithStudentSubjectSemesterViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Student.UserName))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
                .ForMember(dest => dest.SemesterName, opt => opt.MapFrom(src => src.Semester.SemesterName));
        }
    }
}
