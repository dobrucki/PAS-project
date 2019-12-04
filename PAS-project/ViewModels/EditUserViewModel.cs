using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class EditUserViewModel : User
    {
        public void SetId(int id)
        {
            Id = id;
        }
        
    }
}