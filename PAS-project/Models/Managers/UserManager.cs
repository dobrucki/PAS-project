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
        private readonly IDataRepository<ApplicationUser> _userRepository;

        public UserManager(IDataRepository<ApplicationUser> userRepository, IDataContext dataContext)
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

        public void AddUser(ApplicationUser applicationUser)
        {
            _userRepository.Add(applicationUser);
        }

        public ApplicationUser GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            bool Filter(ApplicationUser user) => user.Email == email; 
            return _userRepository.GetAll(Filter).First();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<ApplicationUser> FilterUsersByEmail(string email)
        {
            bool Filter(ApplicationUser user) => user.Email.ToLower().Contains(email.ToLower());
            return _userRepository.GetAll(Filter);
        }
        
        public void UpdateUser(ApplicationUser applicationUser)
        {
            _userRepository.Update(applicationUser);
        }

        public void ActivateUser(ApplicationUser applicationUser)
        {
            _userRepository.GetById(applicationUser.Id).Active = true;
        }
        
        public void DeActivateUser(ApplicationUser applicationUser)
        {
            _userRepository.GetById(applicationUser.Id).Active = false;
        }
        public void MakeVip(ApplicationUser applicationUser)
        {
            _userRepository.GetById(applicationUser.Id).UserType = ApplicationUser.VipUserType;
        }
        
        public void MakeStandard(ApplicationUser applicationUser)
        {
            _userRepository.GetById(applicationUser.Id).UserType = ApplicationUser.StandardUserType;
        }
    }
}