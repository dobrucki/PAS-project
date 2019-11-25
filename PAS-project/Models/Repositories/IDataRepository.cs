using System.Collections.Generic;

namespace PAS_project.Models.Repositories
{
    public interface IDataRepository<T>
    {
        T Add(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        T Update(T item);
        T Delete(int id);
    }
}