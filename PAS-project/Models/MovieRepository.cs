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
            if (movie is null) throw new Exception("Given item is null");
            if (_movies.Any(m => m.Id == movie.Id)) throw new Exception("Given movie already exists!");
            _movies.Add(movie);
            return movie;
        }

        public Movie Get(int id)
        {
            var result = _movies.First(movie => movie.Id == id);
            if (result is null) throw new Exception("Given id does not match any movie!");
            return result;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie Update(Movie updatedMovie)
        {
            if (updatedMovie is null) throw new Exception("Given movie is null!");
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