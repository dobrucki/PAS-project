using System.Collections.Generic;

namespace PAS_project.Models
{
    public interface IDataRepository<T> where T : IModel
    {
        void Add(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T item, int id);
        void Remove(int id);
        void Remove(T item);
    }
    
}