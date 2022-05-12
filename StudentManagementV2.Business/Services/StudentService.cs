using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class StudentService : Service<Student, string>, IStudentService
    {
        protected IService<AssignClass, string> AssignClassService { get; set; }
        protected IService<Role, string> RoleService { get; set; }
        protected IStudentRepository StudentRepository { get; set; }

        public StudentService(IRepository<Student, string> Repository,
            IService<Role, string> RoleService,
            IStudentRepository StudentRepository,
            IService<AssignClass, string> AssignClassService) : base(Repository)
        {
            this.AssignClassService = AssignClassService;
            this.RoleService = RoleService;
            this.StudentRepository = StudentRepository;
        }

        public override void Delete(string id)
        {
            Repository.Delete(Repository.GetById(id));
        }

        public override void Save(Student entity)
        {
            // This only check with discriminator = student --> Add service of Instructor and Admin later
            if (Repository.GetById(entity.UserId) == null 
                && AssignClassService.GetById(entity.ClassId) != null 
                && RoleService.GetById(entity.RoleId) != null)
            {
                entity.Class = AssignClassService.GetById(entity.ClassId);
                entity.Role = RoleService.GetById(entity.RoleId);

                Repository.Save(entity);
            }
            
        }

        public override void Update(Student entity)
        {
            Student student = Repository.GetById(entity.UserId);

            if (student != null
                && RoleService.GetById(entity.RoleId) != null
                && AssignClassService.GetById(entity.ClassId) != null)
            {
                student.UserName = entity.UserName;
                student.RoleId = entity.RoleId;
                student.Role = RoleService.GetById(entity.RoleId);
                student.Email = entity.Email;
                student.Phone = entity.Phone;
                student.Address = entity.Address;
                student.Birthdate = entity.Birthdate;
                student.ClassId = entity.ClassId;
                student.Class = AssignClassService.GetById(entity.ClassId);

                Repository.Update(student);
            }
        }

        public Student GetStudentByIdentityId(string identityId)
        {
            return StudentRepository.GetStudentByIdentityId(identityId);
        }

        public List<Student> GetStudentByAssignClass(string classId)
        {
            return StudentRepository.GetStudentByAssignClass(classId);
        }
    }
}
