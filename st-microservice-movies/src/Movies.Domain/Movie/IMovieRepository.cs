using Domaincrafters.Domain;

namespace Movies.Domain.Movie; 

public interface IMovieRepository : IRepository<Movie, MovieId>
{
}