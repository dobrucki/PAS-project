using System.Collections.Generic;

namespace PAS_project.Models
{
    public interface IDataRepository<T> where T : IModel
    {
        T Add(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        T Update(T item);
        T Delete(int id);
    }
    
}