using System.Linq.Expressions;
using Movies.Application.Contracts.Data;
using Movies.Application.Contracts.Ports;
using Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class AllMoviesQuery(
    QueryContextBase context
) : IAllMoviesQuery
{
    private readonly QueryContextBase _context = context;
    public async Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter)
    {
        return await _context.Movies
            .Where(filter)
            .ToListAsync();
    }
}
