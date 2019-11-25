using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Repositories
{
    internal class MockUserRepository : IDataRepository<User>
    {
        public User Add(User item)
        {
            throw new System.NotImplementedException();
        }

        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User Update(User item)
        {
            throw new System.NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}