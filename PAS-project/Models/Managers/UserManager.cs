using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Entities.UserTypes;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class UserManager
    {
        private readonly IDataRepository<User> _userRepository;

        public UserManager(IDataRepository<User> userRepository, IDataContext dataContext)
        {
            _userRepository = userRepository;
            if (!(dataContext is null))
            {
                foreach (var u in dataContext.Users)
                {
                    _userRepository.Add(u);
                }
            }
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

        public IEnumerable<User> FilterUsersByEmail(string email)
        {
            bool Filter(User user) => user.Email.ToLower().Contains(email.ToLower());
            return _userRepository.GetAll(Filter);
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
        public void MakeVip(User user)
        {
            _userRepository.GetById(user.Id).UserType = User.VipUserType;
        }
        
        public void MakeStandard(User user)
        {
            _userRepository.GetById(user.Id).UserType = User.StandardUserType;
        }
    }
}