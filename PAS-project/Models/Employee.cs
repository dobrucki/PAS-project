namespace PAS_project.Models
{
    public class Employee : IUserType
    {
        public int MaxReservationsCount { get; }
        public int MinRequiredMinutesToCancelBooking { get; } 
        public int MinRequiredMinutesToMakeBooking { get; }
    }
}