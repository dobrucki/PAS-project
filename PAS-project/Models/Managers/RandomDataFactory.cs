using System;
using System.Runtime.CompilerServices;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Managers
{
    internal static class RandomDataFactory
    {
        public static User CreateRandomUser()
        {
            string[] firstNames = {"Michał", "Mateusz", "Kamil", "Alojzy", "Sebastian", "Krystian", "Szymon"};
            string[] lastNames = {"Kowalski", "Wasilewski", "Dobrucki", "Celejewski", "Nowak", "Trzmiel"};
            
            var random = new Random();
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];
            var email = $"{firstName}.{lastName}@example.com";
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }

        public static Movie CreateRandomMovie()
        {
            var random = new Random();
            string[] titles =
            {
                "Skazani na Shawshank", "Zielona mila", "Król lew", "Gwiezdne wojny", "Lessie wróć",
                "Władca pierścieni", "Darknight rises", "The Town", "Robotnicy wychodzący z fabryki",
                "Kraina Lodu 2", "Shrek", "Rio", "Joker"
            };
            return new Movie
            {
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Donec pretium faucibus tortor, at mollis ante pulvinar ac" +
                    ". Pellentesque habitant morbi.",
                DurationTime = random.Next(80, 180),
                Title = titles[random.Next(titles.Length)]
            };
        }
        
        

        public static Seance CreateRandomSeance(Movie movie)
        {
            var random = new Random();
            var time = DateTime.UtcNow;
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
            return new Seance
            {
                Movie = movie,
                StartingTime = time.AddMinutes(15 * random.Next(5, 23))
            };
        }
    }
}