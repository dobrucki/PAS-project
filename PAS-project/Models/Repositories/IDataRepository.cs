using System;
using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Repositories
{
    public interface IDataRepository<T> where T : Entity
    {
        void Add(T item);
        T GetById(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Func<T, bool> predicate);
        void Update(T item);
        void Delete(T item);
    }
}