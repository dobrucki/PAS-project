namespace PAS_project.Models
{
    public class Seat : IModel
    {
        public int Id { get; set; }
        public char Row { get; set; }
        public int Number { get; set; }
        public Hall ScreeningRoom { get; set; }
    }
}