using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class MovieManager
    {
        private readonly IDataRepository<Movie> _movieRepository;

        public MovieManager(IDataRepository<Movie> movieRepository, IDataContext dataContext)
        {
            _movieRepository = movieRepository;
            if (!(dataContext is null))
            {
                foreach (var m in dataContext.Movies)
                {
                    _movieRepository.Add(m);
                }
            }
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
            bool Filter(Movie movie) => movie.Title == title; 
            return _movieRepository.GetAll(Filter).First();
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