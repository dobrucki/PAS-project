using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class UserManager
    {
        private readonly IDataRepository<User> _userRepository;

        public UserManager(IDataRepository<User> userRepository)
        {
            for (var i = 0; i < 10; i++)
            {
                userRepository.Add(RandomDataFactory.CreateRandomUser());
            }
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserByEmail(string email)
        {
            bool Filter(User user) => user.Email == email; 
            return _userRepository.GetAll(Filter).First();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
        
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void ActivateUser(User user)
        {
            _userRepository.GetById(user.Id).Active = true;
        }
        
        public void DeActivateUser(User user)
        {
            _userRepository.GetById(user.Id).Active = false;
        }
    }
}