using System.ComponentModel.DataAnnotations;
using PAS_project.Models.Entities.UserTypes;

namespace PAS_project.Models.Entities
{
    public class User : Entity
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name length can't be more than 50.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name length can't be more than 50.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid.")]
        public string Email { get; set; }
        public IUserType UserType { get; set; }
        public UserAccessLevel AccessLevel { get; set; }
        public bool Active { get; set; }
        
    }

    namespace UserTypes
    {
        public enum UserAccessLevel
        {
            Basic,
            Employee,
            Administrator
        }
        
        public interface IUserType
        {
            int MinutesForBooking { get; }
            int MinutesForCancelling { get; }
        }

        internal class StandardUserType : IUserType
        {
            public int MinutesForBooking { get; } 
            public int MinutesForCancelling { get; }

            public StandardUserType()
            {
                MinutesForBooking = 15;
                MinutesForCancelling = 30;
            }
        }

        internal class VipUserType : IUserType
        {
            public int MinutesForBooking { get; }
            public int MinutesForCancelling { get; }

            public VipUserType()
            {
                MinutesForBooking = 5;
                MinutesForCancelling = 15;
            }
        }
    }
}