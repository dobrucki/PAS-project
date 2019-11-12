using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class MovieRepository : IDataRepository<Movie>
    {
        private readonly List<Movie> _movies = new List<Movie>();

        public Movie Add(Movie movie)
        {
            var id = _movies.Count == 0 ? 1 : _movies.Last().Id+1;
            movie.Id = id;
            _movies.Add(movie);
            return movie;
        }

        public Movie Get(int id)
        {
            var result = _movies.FirstOrDefault(movie => movie.Id == id);
            return result;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie Update(Movie updatedMovie)
        {
            var actualMovie = _movies.FirstOrDefault(m => m.Id == updatedMovie.Id);
            if (actualMovie is null) throw new Exception("Given id does not match any movie!");
            actualMovie.Description = updatedMovie.Description;
            actualMovie.Title = updatedMovie.Title;
            return actualMovie;
        }

        public Movie Delete(int id)
        {
            var foundMovie = _movies.FirstOrDefault(c => c.Id == id);
            if(foundMovie is null) throw new Exception("Given id does not match any movie!");
            _movies.Remove(foundMovie);
            return foundMovie;
        }
    }
}