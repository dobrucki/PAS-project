using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Identity
{
    public class UserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, 
        IUserRoleStore<ApplicationUser>
    {
        private readonly IDataRepository<ApplicationUser> _repository;
        private readonly IDataRepository<ApplicationRole> _roleRepository;
        
        public UserStore(IDataRepository<ApplicationUser> repository, IDataRepository<ApplicationRole> roleRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
        }

        public void Dispose()
        {
            // Nothing to dispose
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            _repository.Add(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            _repository.Delete(user);
            return IdentityResult.Success;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            int.TryParse(userId, out var id);
            return _repository.GetById(id);
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _repository.GetAll()
                .FirstOrDefault(u => string
                    .Equals(u.UserName, normalizedUserName, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.UserName.ToUpper();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        public async Task SetNormalizedUserNameAsync(ApplicationUser user, 
            string normalizedName, CancellationToken cancellationToken)
        {
        }

        public async Task SetUserNameAsync(ApplicationUser user, 
            string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            _repository.Update(user);
            return IdentityResult.Success;
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.Password;
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(user.Password);
        }

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, 
            CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var role = _roleRepository.GetAll().FirstOrDefault(r => string.Equals(roleName, r.Name));
            user.ApplicationRole = role;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.ApplicationRole is null ? null : new List<string> {user.ApplicationRole.Name};
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        { 
            var role = _roleRepository.GetAll().FirstOrDefault(r => string.Equals(roleName, r.Name));
            return _repository.GetAll().Where(u => u.ApplicationRole.Equals(role)).ToList();
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            return string.Equals(user.ApplicationRole.Name, roleName);
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            if (string.Equals(user.ApplicationRole.Name, roleName))
                user.ApplicationRole = null;
        }

    }
}