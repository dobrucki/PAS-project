namespace PAS_project.Models.Entities
{
    public class Movie : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationTime { get; set; }
    }
}