using PagedList;
using StudentManagementV2.Core.Models;
using StudentManagementV2.Core.PaginatedLists;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
     public interface ISubjectRegisterService
    {
        SubjectRegister GetByCompositeId (string studentId, string subjectId, string semesterId, short year);

        void DeleteByCompositeId(string studentId, string subjectId, string semesterId, short year);

        IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester();

        IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester(string identityId);

        IEnumerable<SubjectRegister> SearchBySubject(string keyword);

        IEnumerable<SubjectRegister> SearchBySemester(string keyword);

        IEnumerable<SubjectRegister> SearchByYear(string keyword);

        IEnumerable<SubjectRegister> SearchBySubject(string keyword, string identityId);

        IEnumerable<SubjectRegister> SearchBySemester(string keyword, string identityId);

        IEnumerable<SubjectRegister> SearchByYear(string keyword, string identityId);

        PaginatedList<SubjectRegister> GetAllWithStudentSubjectSemesterPaging(int page);
    }
}
