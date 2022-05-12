using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Services
{
    /// <summary>
    /// T: Entity type;
    /// I: Entity ID type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    public interface IService<T, I> where T : class where I : class
    {
        IEnumerable<T> GetAll();

        T GetById(I id); 

        void Save (T entity);

        void Update(T entity);

        void Delete (I id);
    }
}
