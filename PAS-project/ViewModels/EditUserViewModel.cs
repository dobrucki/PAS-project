using PAS_project.Models.Entities;
using PAS_project.Models.Entities.UserTypes;

namespace PAS_project.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public User User { get; set; }
        public IUserType UserType { get; set; }

    }
}