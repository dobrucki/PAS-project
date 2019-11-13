using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class UserRepository : IDataRepository<User>
    {
        private readonly List<User> _users = new List<User>();
        
        public User Add(User user)
        {
            var id = _users.Count == 0 ? 1 : _users.Last().Id+1;
            user.Id = id;
            _users.Add(user);
            return user;
        }

        public User Get(int id)
        {
            var result = _users.FirstOrDefault(user => user.Id == id);
            if (result is null) throw new Exception("Given id does not match any user!");
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User Update(User updatedUser)
        {
            
            var actualUser = _users.FirstOrDefault(c => c.Id == updatedUser.Id);
            if (actualUser is null) throw new Exception("Given id does not match any user!");
            actualUser.Email = updatedUser.Email;
            actualUser.Sex = updatedUser.Sex;
            actualUser.FirstName = updatedUser.FirstName;
            actualUser.LastName = updatedUser.LastName;
            actualUser.PhoneNumber = updatedUser.PhoneNumber;
            actualUser.ZipCode = updatedUser.PhoneNumber;
            return actualUser;
        }

        public User Delete(int id)
        {
            var foundUser = _users.FirstOrDefault(c => c.Id == id);
            if(foundUser is null) throw new Exception("Given id does not match any user!");
            _users.Remove(foundUser);
            return foundUser;
        }
    }
}