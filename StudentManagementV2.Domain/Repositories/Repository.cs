using Microsoft.EntityFrameworkCore;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentManagementV2.Domain.Repositories
{
    public abstract class Repository<T, I> : IRepository<T, I> where T : class where I : class
    {
        protected readonly StudentManagementContext Context;

        public Repository(StudentManagementContext context)
        {
            this.Context = context;
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().AsEnumerable();
        }

        public T GetById (I id)
        {
            var entity = Context.Set<T>().Find(id);
            //Context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public void Save(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            //var model = Context.Entry(entity);
            //model.State = EntityState.Modified;

            Context.Set<T>().Update(entity);
            Context.SaveChanges();
        }
    }
}
