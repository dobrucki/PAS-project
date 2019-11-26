using PAS_project.Models.Entities.UserTypes;

namespace PAS_project.Models.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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