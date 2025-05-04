using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SampleApp20240815.API.Movie.DTO;

public class MovieUpsertRequestDto
{
    public int Year { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [return: NotNullIfNotNull(nameof(dto))]
    public static explicit operator BL.Movies.Movie?(MovieUpsertRequestDto? dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new BL.Movies.Movie
        {
            Title = dto.Title,
            Year = dto.Year,
        };
    }
}
