using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Identity
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly IDataRepository<ApplicationRole> _repository;

        public RoleStore(IDataRepository<ApplicationRole> repository)
        {
            _repository = repository;
            var userRole = new ApplicationRole
            {
                Name = "User"
            };
            var adminRole = new ApplicationRole
            {
                Name = "Admin"
            };
            var employeeRole = new ApplicationRole
            {
                Name = "Employee"
            };
            repository.Add(adminRole);
            repository.Add(employeeRole);
            repository.Add(userRole);
        }

        public void Dispose()
        {
            // Nothing to dispose
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            _repository.Add(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            _repository.Delete(role);
            return IdentityResult.Success;
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            int.TryParse(roleId, out var id);
            return _repository.GetById(id);
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return _repository.GetAll().FirstOrDefault(role => role.Name.ToUpper().Equals(normalizedRoleName.ToUpper()));
        }

        public async Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return role.Name.ToUpper();
        }

        public async Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return role.Id.ToString();
        }

        public async Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return role.Name;
        }

        public async Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
        }

        public async Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            _repository.Update(role);
            return IdentityResult.Success;
        }
    }
}