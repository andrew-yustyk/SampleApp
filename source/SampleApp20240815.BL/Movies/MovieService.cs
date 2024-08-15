using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SampleApp20240815.BL.Movies;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<MovieService> _logger;

    public MovieService(IMovieRepository movieRepository, ILogger<MovieService> logger)
    {
        ArgumentNullException.ThrowIfNull(nameof(movieRepository));
        ArgumentNullException.ThrowIfNull(logger);
        _movieRepository = movieRepository;
        _logger = logger;
    }


    public async Task<IEnumerable<Movie>> GetAll(CancellationToken ct = default)
    {
        var result = await _movieRepository.GetAll(ct);
        return result;
    }

    public async Task<Movie?> Get(Guid id, CancellationToken ct = default)
    {
        return await _movieRepository.Get(id, ct);
    }

    public async Task<Movie> Create(Movie movie, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(nameof(movie));

        var result = await _movieRepository.Create(movie, ct);

        return result;
    }

    public async Task<Movie> Upsert(Movie movie, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(nameof(movie));

        Movie result;

        var existing = await _movieRepository.Get(movie.Id, ct);
        if (existing  is null)
        {
            result = await _movieRepository.Create(movie, ct);
        }
        else
        {
            existing.Title = movie.Title;
            existing.Year = movie.Year;
            result = await _movieRepository.Update(existing, ct);
        }

        return result;
    }

    public async Task Delete(Guid id, CancellationToken ct = default)
    {
        var result = await _movieRepository.Delete(id, ct);
        if (result == 0)
        {
            _logger.LogInformation("Movie with ID {Id} does  not exist", id);
        }
    }
}
