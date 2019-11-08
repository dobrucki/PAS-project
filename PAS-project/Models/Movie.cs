namespace PAS_project.Models
{
    public class Movie : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Movie(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}