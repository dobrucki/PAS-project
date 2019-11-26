using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Repositories
{
    internal class MockDataRepository<T> : IDataRepository<T> where T : Entity
    {
        private readonly Dictionary<int, T> _entities;

        public MockDataRepository()
        {
            _entities = new Dictionary<int, T>();
        }
        public void Add(T entity)
        {
            _entities.Add(entity.Id, entity);
        }

        public T GetById(int id)
        {
            return _entities.FirstOrDefault(entity => entity.Key == id).Value;
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Values.AsEnumerable();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return _entities.Values.Where(predicate).AsEnumerable();
        }

        public void Update(T entity)
        {
            var actualEntity = GetById(entity.Id);
            if (actualEntity is null || actualEntity.GetType() != entity.GetType()) return;
            var props = actualEntity.GetType().GetProperties();
            var newProps = entity.GetType().GetProperties();
            for (var i = 0; i < props.Length; i++)
            {
                if (props[i].CanWrite)
                {
                    props[i] = newProps[i];
                }
            }
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity.Id);
        }
    }
}