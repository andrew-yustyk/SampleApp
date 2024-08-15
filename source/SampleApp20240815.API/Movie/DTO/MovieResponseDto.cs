using System;
using System.Diagnostics.CodeAnalysis;

namespace SampleApp20240815.API.Movie.DTO;

public class MovieResponseDto
{
    public Guid Id { get; set; }

    public DateTimeOffset Version { get; set; }

    public int Year { get; set; }

    public string Title { get; set; } = string.Empty;

    [return: NotNullIfNotNull(nameof(movie))]
    public static explicit operator MovieResponseDto?(BL.Movies.Movie? movie)
    {
        if (movie is null)
        {
            return null;
        }

        return new MovieResponseDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Version = movie.Version,
            Year = movie.Year
        };
    }
}
