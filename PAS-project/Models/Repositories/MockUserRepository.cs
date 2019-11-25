using System;
using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Repositories
{
    internal class MockUserRepository : IDataRepository<User>
    {
        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(User item)
        {
            throw new NotImplementedException();
        }
    }
}