using PAS_project.Models.Entities;
using PAS_project.Models.Entities.UserTypes;

namespace PAS_project.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Type { get; set; }
        public bool Activity { get; set; }

    }
}