using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    internal static class RandomDataFactory
    {
        public class DataWrapper
        {
            public IEnumerable<User> Users { get; internal set; }
            public IEnumerable<Movie> Movies { get; internal set; }
            public IEnumerable<Seance> Seances { get; internal set; }
            public IEnumerable<CinemaHall> CinemaHalls { get; internal set; }
            public IEnumerable<CinemaEvent> CinemaEvents { get; internal set;}
        }
        
        private static User CreateRandomUser()
        {
            string[] firstNames = {"Michał", "Mateusz", "Kamil", "Alojzy", "Sebastian", "Krystian", "Szymon"};
            string[] lastNames = {"Kowalski", "Wasilewski", "Dobrucki", "Celejewski", "Nowak", "Trzmiel"};
            
            var random = new Random();
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}@example.com";
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }

        private static Movie CreateRandomMovie()
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
                    "Curabitur ullamcorper tempor sem, eu consequat ex. " +
                    "Nunc pellentesque vestibulum facilisis. " +
                    "Duis quam tortor, vulputate nec rhoncus ornare, ultrices vel lorem. " +
                    "Cras consequat nunc dolor, sit amet pulvinar erat ultrices eget. " +
                    "Sed mi dui, accumsan efficitur lorem id, pulvinar varius nulla. " +
                    "Sed ultricies velit eget justo egestas condimentum. " +
                    "Suspendisse in ante vitae dui ultrices pharetra blandit porta eros. " +
                    "Cras urna massa, maximus eget ultrices a, cursus vel enim. " +
                    "Quisque vehicula arcu sed velit fermentum imperdiet. " +
                    "Donec et nunc augue. Proin lacinia fermentum orci quis pulvinar. " +
                    "Sed convallis nulla diam, vitae tempus lectus.",
                DurationTime = random.Next(80, 180),
                Title = titles[random.Next(titles.Length)]
            };
        }

        private static CinemaHall CreateRandomCinemaHall()
        {
            var random = new Random();
            var seats = new List<CinemaHall.Seat>();
            for (var i = 'A'; i < 'A' + random.Next(7, 15); i++)
            {
                for (var j = 0; j < random.Next(12, 28); j++)
                {
                    seats.Add(new CinemaHall.Seat
                    {
                        Row = i,
                        Column = j
                    });
                }
            }

            return new CinemaHall
            {
                Name = "Screening room",
                Seats = seats
            };
        }

        private static Seance CreateRandomSeance(Movie movie, CinemaHall cinemaHall)
        {
            var random = new Random();
            var time = DateTime.UtcNow;
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
            return new Seance
            {
                Movie = movie,
                CinemaHall = cinemaHall,
                StartingTime = time.AddMinutes(15 * random.Next(5, 257))
            };
        }

        private static CinemaEvent CreateRandomCinemaEvent(User user, CinemaHall cinemaHall, Seance seance)
        {
            var random = new Random();
            return new CinemaEvent
            {
                User = user,
                Seat = cinemaHall.Seats.ToList()[random.Next(cinemaHall.Seats.Count())],
                Seance = seance
            };
        }

        public static DataContext GenerateRandomData()
        {
            var random = new Random();
            
            var users = new List<User>();
            for (var i = 0; i < random.Next(20, 32);)
            {
                var user = CreateRandomUser();
                if (users.Any(u => u.Email == user.Email)) continue;
                i++;
                users.Add(user);
            }

            var movies = new List<Movie>();
            for (var i = 0; i < random.Next(3, 8); i++)
            {
                movies.Add(CreateRandomMovie());
            }
            
            var cinemaHalls = new List<CinemaHall>();
            for (var i = 0; i < random.Next(2, 5); i++)
            {
                cinemaHalls.Add(CreateRandomCinemaHall());
            }
            
            var seances = new List<Seance>();
            foreach (var movie in movies)
            {
                for (var i = 0; i < random.Next(2, 5);)
                {
                    var seance = CreateRandomSeance(movie, cinemaHalls[random.Next(cinemaHalls.Count)]);
                    bool TimeOverlapFilter(Seance s) =>
                        s.CinemaHall.Id == seance.CinemaHall.Id
                        && s.StartingTime < seance.StartingTime.AddMinutes(movie.DurationTime)
                        && seance.StartingTime < s.StartingTime.AddMinutes(movie.DurationTime);
                    if (seances.Any(TimeOverlapFilter)) continue;
                    i++;
                    seances.Add(seance);
                }
            }
            
            var cinemaEvents = new List<CinemaEvent>();
            for (var i = 0; i < random.Next(30, 70);)
            {
                var user = users[random.Next(users.Count)];
                var seance = seances[random.Next(seances.Count)];
                var cinemaEvent = CreateRandomCinemaEvent(
                    user,
                    seance.CinemaHall,
                    seance
                );
                if (cinemaEvents.Where(e => e.Seance == cinemaEvent.Seance).Any(e => e.Seat == cinemaEvent.Seat))
                    continue;
                i++;
                cinemaEvents.Add(cinemaEvent);
            }

            return new DataContext
            {
                Users = users,
                Movies = movies,
                Seances = seances,
                CinemaHalls = cinemaHalls,
                CinemaEvents = cinemaEvents
            };
        }
    }
}