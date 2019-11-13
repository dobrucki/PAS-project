namespace PAS_project.Models
{
    public interface IUserType
    {
        int MaxReservationsCount { get; }
        int MinRequiredMinutesToCancelBooking { get; } 
        int MinRequiredMinutesToMakeBooking { get; }
    }
}