using System.Linq.Expressions;

namespace Movies.Application.Contracts.Data.Filters;

public static class MovieDataExpressions
{
    public static Expression<Func<MovieData, bool>> FilterByTitleAndGenre(string title, string genre)
    {
        return movieData => movieData.Title.ToLower().Contains(title.ToLower()) &&
                            movieData.Genre.ToLower().Contains(genre.ToLower());
    }

    public static Expression<Func<MovieData, bool>> FilterById(string id)
    {
        return movieData => movieData.Id == id;
    }

    public static Expression<Func<MovieData, bool>> FilterByTitleAndGenreFull(string title, string genre)
    {
        return movieData => movieData.Title.ToLower() == title.ToLower() &&
                            movieData.Genre.ToLower() == genre.ToLower();
    }
    
}