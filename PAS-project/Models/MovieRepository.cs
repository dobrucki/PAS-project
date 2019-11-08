using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class MovieRepository : IDataRepository<Movie>
    {
    private readonly List<Movie> _movies = new List<Movie>();

    public void Add(Movie item)
    {
        if (item is null) throw new Exception("Given item is null");
        if (_movies.Any(movie => movie.Id == item.Id)) throw new Exception("Given movie already exists!");
        _movies.Add(item);
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

    public void Update(Movie item, int id)
    {
        if (item is null) throw new Exception("Given movie is null!");
        try
        {
            var index = _movies.FindIndex(movie => movie.Id == id);
            _movies[index] = item;
        }
        catch (ArgumentNullException)
        {
            throw new Exception("Given id does not match any movie!");
        }
    }

    public void Remove(int id)
    {
        _movies.Remove(Get(id));
    }

    public void Remove(Movie item)
    {
        _movies.Remove(item);
    }
    }
}