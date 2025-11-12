using System.Linq.Expressions;
using Movies.Application.Contracts.Data;

namespace Movies.Application.Contracts.Ports;

public interface IAllMoviesQuery
{
    Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter );
}

public interface IAllMovieEventsQuery
{
    Task<IReadOnlyList<MovieEventData>> Fetch(Expression<Func<MovieEventData, bool>> filter);
}

