using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class MovieManager
    {
        private readonly IDataRepository<Movie> _movieRepository;

        public MovieManager(IDataRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void AddMovie(Movie movie)
        {
            _movieRepository.Add(movie);
        }

        public Movie GetMovieById(int id)
        {
            return _movieRepository.GetById(id);
        }

        public Movie GetMovieByTitle(string title)
        {
            var movies = _movieRepository.GetAll();
            return movies.FirstOrDefault(entity => entity.Title == title);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetAll();
        }
        
        public void UpdateMovie(Movie movie)
        {
            _movieRepository.Update(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            _movieRepository.Delete(movie);
        }
    }
}