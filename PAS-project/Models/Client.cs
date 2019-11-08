namespace PAS_project.Models
{
    public abstract class Client : IModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
    }
}