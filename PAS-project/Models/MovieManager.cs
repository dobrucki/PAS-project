using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class MovieManager
    {
        private readonly IDataRepository<Movie> _repository;

        public MovieManager(IDataRepository<Movie> repository)
        {
            _repository = repository;
        }
        
        public Movie AddMovie(Movie movie)
        {
            if(movie is null) throw new Exception("Given movie is null");
            if (_repository.GetAll().Any(mo => mo.Title == movie.Title)) 
                throw new Exception("Given movie already exists!");
            _repository.Add(movie);
            return movie;
        }
        
        public Movie GetMovie(int id)
        {
            var result = _repository.Get(id);
            if (result is null) throw new Exception("Given id does not match any movie!");
            return result;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _repository.GetAll();
        }

        public Movie UpdateMovie(Movie updatedMovie)
        {
            if (updatedMovie is null) throw new Exception("Given movie is null!");

            return _repository.Update(updatedMovie);
            
        }
        public Movie DeleteMovie(int id)
        {
            return _repository.Delete(id);
        }
        
        public Movie FilterByTitle(string title)
        {
            var result = _repository.GetAll().FirstOrDefault(mo => mo.Title == title);
            if(result == null) throw new Exception("Given title does not match any movie!");
            return result;
        }
    }
}