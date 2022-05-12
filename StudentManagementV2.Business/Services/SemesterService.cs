using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class SemesterService : Service<Semester, string>, ISemesterService
    {
        public SemesterService(IRepository<Semester, string> Repository) : base(Repository)
        {
        }

        public override void Delete(string id)
        {
            Repository.Delete(Repository.GetById(id));
        }

        public override void Save(Semester entity)
        {
            Semester semester = Repository.GetById(entity.SemesterId);

            if (semester == null)
            {
                Repository.Save(entity);
            }
        }

        public override void Update(Semester entity)
        {
            Semester semester = Repository.GetById(entity.SemesterId);

            if (semester != null)
            {
                semester.SemesterName = entity.SemesterName;

                Repository.Update(semester);
            }
        }
    }
}
