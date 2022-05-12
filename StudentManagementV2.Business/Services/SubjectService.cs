using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public class SubjectService : Service<Subject, string>, ISubjectService
    {
        public SubjectService(IRepository<Subject, string> Repository) : base(Repository)
        {
        }

        public override void Delete(string id)
        {
            Repository.Delete(Repository.GetById(id));
        }

        public override void Save(Subject entity)
        {
            Subject subject = Repository.GetById(entity.SubjectId);

            if (subject == null)
            {
                Repository.Save(entity);
            }
        }

        public override void Update(Subject entity)
        {
            Subject subject = Repository.GetById(entity.SubjectId);

            if (subject != null)
            {
                subject.SubjectName = entity.SubjectName;

                Repository.Update(subject);
            }
        }
    }
}
