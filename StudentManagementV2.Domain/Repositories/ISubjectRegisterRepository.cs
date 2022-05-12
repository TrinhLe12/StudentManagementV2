using PagedList;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public interface ISubjectRegisterRepository
    {
        SubjectRegister GetByCompositeId (string studentId, string subjectId, string semesterId, short year);

        IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester();

        IEnumerable<SubjectRegister> GetAllWithStudentSubjectSemester(string identityId);

        IEnumerable<SubjectRegister> SearchBySubject(string keyword, string identityId);

        IEnumerable<SubjectRegister> SearchBySemester(string keyword, string identityId);

        IEnumerable<SubjectRegister> SearchByYear(string keyword, string identityId);

        IEnumerable<SubjectRegister> SearchBySubject (string keyword);
        
        IEnumerable<SubjectRegister> SearchBySemester (string keyword);

        IEnumerable<SubjectRegister> SearchByYear (string keyword);

        IPagedList<SubjectRegister> GetAllWithStudentSubjectSemesterPaging (string sortOrder, string CurrentSort, int page);
    }
}
