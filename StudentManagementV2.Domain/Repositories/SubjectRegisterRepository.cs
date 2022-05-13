using Microsoft.EntityFrameworkCore;
using PagedList;
using StudentManagementV2.Core.Constants;
using StudentManagementV2.Core.Models;
using StudentManagementV2.Core.PaginatedLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public class SubjectRegisterRepository : Repository<SubjectRegister, object>, ISubjectRegisterRepository
    {
        public SubjectRegisterRepository(StudentManagementContext context) : base(context)
        {
        }

        public IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester()
        {
            return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .ToList();
        }

        public IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester(string identityId)
        {

                return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.Student.IdentityId.Equals(identityId))
                .ToList();

        }

        public SubjectRegister GetByCompositeId(string studentId, string subjectId, string semesterId, short year)
        {
            return Context.Set<SubjectRegister>().Find(studentId, subjectId, semesterId, year);
        }

        public IEnumerable<SubjectRegister> SearchBySemester(string keyword)
        {
           return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.SemesterId.Contains(keyword) || sr.Semester.SemesterName.Contains(keyword))
                .ToList();
                
        }

        public IEnumerable<SubjectRegister> SearchBySemester(string keyword, string identityId)
        {
            return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.Student.IdentityId.Equals(identityId, StringComparison.InvariantCultureIgnoreCase) && (sr.SemesterId.Contains(keyword) || sr.Semester.SemesterName.Contains(keyword)))
                .ToList();
        }

        public IEnumerable<SubjectRegister> SearchBySubject(string keyword)
        {
            return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.SubjectId.Contains(keyword) || sr.Subject.SubjectName.Contains(keyword))
                .ToList();
        }

        public IEnumerable<SubjectRegister> SearchBySubject(string keyword, string identityId)
        {
            return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.Student.IdentityId.Equals(identityId) && (sr.SubjectId.Contains(keyword) || sr.Subject.SubjectName.Contains(keyword)))
                .ToList();
        }

        public IEnumerable<SubjectRegister> SearchByYear(string keyword)
        {
            return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.Year.ToString().Contains(keyword))
                .ToList();
        }

        public IEnumerable<SubjectRegister> SearchByYear(string keyword, string identityId)
        {
            return Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester)
                .Where(sr => sr.Student.IdentityId.Equals(identityId) && sr.Year.ToString().Contains(keyword))
                .ToList();
        }

        public PaginatedList<SubjectRegister> GetAllWithStudentSubjectSemesterPaging(int page)
        {

            IQueryable<SubjectRegister> source = Context.SubjectRegisters
                .Include(sr => sr.Student)
                .Include(sr => sr.Subject)
                .Include(sr => sr.Semester);

            return PaginatedList<SubjectRegister>.Create(source, page, Constant.PAGE_SIZE); 

        }
    }
}
