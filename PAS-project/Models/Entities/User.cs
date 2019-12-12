using System.ComponentModel.DataAnnotations;
using PAS_project.Models.Entities.UserTypes;

namespace PAS_project.Models.Entities
{
    public class User : Entity
    {

        internal static readonly IUserType StandardUserType = new StandardUserType();
        internal static readonly IUserType VipUserType = new VipUserType();
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
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([1-9][0-9]{8})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        public IUserType UserType { get; set; }
        public UserAccessLevel AccessLevel { get; set; }
        public bool Active { get; set; }

        public User()
        {
            UserType = StandardUserType;
            PhoneNumber = null;

        }

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
            int MinutesForCancelling { get; set; }
        }

        internal class StandardUserType : IUserType
        {
            public int MinutesForBooking { get; } 
            public int MinutesForCancelling { get; set; }

            public StandardUserType()
            {
                MinutesForBooking = 15;
                MinutesForCancelling = 30;
            }

            public override string ToString()
            {
                return "Standard";
            }
        }

        internal class VipUserType : IUserType
        {
            public int MinutesForBooking { get; }
            public int MinutesForCancelling { get; set; }
            

            public VipUserType()
            {
                MinutesForBooking = 5;
                MinutesForCancelling = 15;
            }
            
            public override string ToString()
            {
                return "Vip";
            }
        }
    }
}