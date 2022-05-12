
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    public abstract class Service<T, I> : IService<T, I> where T : class where I : class
    {
        protected IRepository<T, I> Repository;

        public Service(IRepository<T, I> Repository)
        {
            this.Repository = Repository;
        }

        public abstract void Delete (I id);

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = Repository.GetAll();
            return entities;
        }

        public T GetById(I id)
        {
            T entity = Repository.GetById(id);
            return entity;
        }

        public abstract void Save(T entity);

        public abstract void Update(T entity);
        
    }
}
