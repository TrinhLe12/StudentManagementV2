using PagedList;
using StudentManagementV2.Core.Models;
using StudentManagementV2.Core.PaginatedLists;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class SubjectRegisterService : Service<SubjectRegister, object>, ISubjectRegisterService
    {
        protected IService<Student, string> StudentService { get; set; }
        protected IService<Subject, string> SubjectService { get; set; }
        protected IService<Semester, string> SemesterService { get; set; }
        protected ISubjectRegisterRepository SubjectRegisterRepository { get; set; }

        public SubjectRegisterService(IRepository<SubjectRegister, object> Repository, 
            IService<Student, string> StudentService,
            IService<Subject, string> SubjectService,
            IService<Semester, string> SemesterService,
            ISubjectRegisterRepository subjectRegisterRepository) : base(Repository)
        {
            this.StudentService = StudentService;
            this.SubjectService = SubjectService;
            this.SemesterService = SemesterService;
            this.SubjectRegisterRepository = subjectRegisterRepository;
        }

        public override void Delete(object id)
        {
            // No need to implement this method for SubjectRegister
            throw new NotImplementedException();
        }

        public override void Save(SubjectRegister entity)
        {
            SubjectRegister subjectRegister = GetByCompositeId(entity.StudentId, entity.SubjectId, entity.SemesterId, entity.Year);

            if (subjectRegister == null)
            {
                entity.Student = StudentService.GetById(entity.StudentId);
                entity.Subject = SubjectService.GetById(entity.SubjectId);
                entity.Semester = SemesterService.GetById(entity.SemesterId);

                Repository.Save(entity);
            }
        }

        public override void Update(SubjectRegister entity)
        {
            SubjectRegister subjectRegister = GetByCompositeId(entity.StudentId, entity.SubjectId, entity.SemesterId, entity.Year );

            if (subjectRegister != null)
            {
                subjectRegister.Score1 = entity.Score1;
                subjectRegister.Score2 = entity.Score2;

                Repository.Update(subjectRegister);
            }
        }

        public SubjectRegister GetByCompositeId(string studentId, string subjectId, string semesterId, short year)
        {
            return SubjectRegisterRepository.GetByCompositeId(studentId, subjectId, semesterId, year);
        }

        public void DeleteByCompositeId(string studentId, string subjectId, string semesterId, short year)
        {
            Repository.Delete(GetByCompositeId(studentId, subjectId, semesterId, year));
        }

        public IEnumerable<SubjectRegister> SearchBySubject(string keyword)
        {
            return SubjectRegisterRepository.SearchBySubject(keyword);
        }

        public IEnumerable<SubjectRegister> SearchBySemester(string keyword)
        {
            return SubjectRegisterRepository.SearchBySemester(keyword);
        }

        public IEnumerable<SubjectRegister> SearchByYear(string keyword)
        {
            return SubjectRegisterRepository.SearchByYear(keyword);
        }

        public IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester()
        {
            return SubjectRegisterRepository.GetAllWithStudentSubjectSemester();
        }

        public IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester(string identityId)
        {
            return SubjectRegisterRepository.GetAllWithStudentSubjectSemester(identityId);
        }

        public IEnumerable<SubjectRegister> SearchBySubject(string keyword, string identityId)
        {
            return SubjectRegisterRepository.SearchBySubject(keyword, identityId);
        }

        public IEnumerable<SubjectRegister> SearchBySemester(string keyword, string identityId)
        {
            return SubjectRegisterRepository.SearchBySemester(keyword, identityId);
        }

        public IEnumerable<SubjectRegister> SearchByYear(string keyword, string identityId)
        {
            return SubjectRegisterRepository.SearchByYear(keyword, identityId);
        }

        public PaginatedList<SubjectRegister> GetAllWithStudentSubjectSemesterPaging(int page)
        {
            return SubjectRegisterRepository.GetAllWithStudentSubjectSemesterPaging(page);

        }
    }
}
