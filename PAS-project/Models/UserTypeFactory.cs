namespace PAS_project.Models
{
    public static class UserTypeFactory
    {
        public static IUserType Administrator { get; } = new Administrator();
        public static IUserType Employee { get; } = new Employee();
        public static IUserType Client { get; } = new Client();
    }
}