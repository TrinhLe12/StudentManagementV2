using System.Collections.Generic;

namespace StudentManagementV2.Domain.Repositories
{
    /// <summary>
    /// T: Entity type; 
    /// I: Entity Id type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    public interface IRepository<T, I> where T : class where I : class
    {
        IEnumerable<T> GetAll();

        T GetById (I id);

        void Save(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
