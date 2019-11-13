using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class UserManager
    {
        private readonly IDataRepository<User> _repository;

        public UserManager(IDataRepository<User> repository)
        {
            _repository = repository;
        }

        public User AddUser(User user)
        {
            if(user is null) throw new Exception("Given user is null");
            if (_repository.GetAll().Any(m => m.Email == user.Email)) 
                throw new Exception("Given user already exists!");
            _repository.Add(user);
            return user;
        }

        public User GetUser(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAll();
        }

        public User UpdateUser(User updatedUser)
        {
            if (updatedUser is null) throw new Exception("Given user is null!");

            return _repository.Update(updatedUser);
            
        }
        public User DeleteUser(int id)
        {
            return _repository.Delete(id);
        }

        public void ActivateUser(User user)
        {
            if(user.Activity) throw new Exception("Given user is already activated!");
            user.Activity = true;
        }

        public void DeactivateUser(User user)
        {
            if(user.Activity == false) throw new Exception("Given user is already deactivated!");
            user.Activity = false;
        }

        public User FilterByEmails(string email)
        {
            var result = _repository.GetAll().FirstOrDefault(cl => cl.Email == email);
            if(result == null) throw new Exception("Given email does not match any user!");
            return result;
        }

    }
}