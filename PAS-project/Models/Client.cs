namespace PAS_project.Models
{
    public class Client : IModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public bool Activity { get; set; }

        public Client(string firstName, string lastName, bool sex, string email, string phoneNumber, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Email = email;
            PhoneNumber = phoneNumber;
            ZipCode = zipCode;
            Activity = true;
        }
    }
}