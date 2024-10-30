using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApp20240815.API.Movie.DTO;
using SampleApp20240815.BL.Movies;

namespace SampleApp20240815.API.Movie;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
    {
        ArgumentNullException.ThrowIfNull(movieService);
        ArgumentNullException.ThrowIfNull(logger);

        _movieService = movieService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<MovieResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        return Catch(async () =>
        {
            var movies = await _movieService.GetAll(ct);
            var result = movies.Select(x => (MovieResponseDto)x).ToList();
            return Ok(result);
        });
    }

    [HttpPost]
    [ProducesResponseType<MovieResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Post([Required] [FromBody] MovieUpsertRequestDto dto, CancellationToken ct = default)
    {
        return Catch(async () =>
        {
            var movie = await _movieService.Create((BL.Movies.Movie)dto, ct);
            var result = (MovieResponseDto)movie;
            return Ok(result);
        });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<MovieResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Get(Guid id, CancellationToken ct = default)
    {
        return Catch(async () =>
        {
            if (!ValidateId(id))
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var movie = await _movieService.Get(id, ct);
            var result = (MovieResponseDto?)movie;

            return result is null
                ? NotFound()
                : Ok(result);
        });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<MovieResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Put(Guid id, [Required] [FromBody] MovieUpsertRequestDto dto, CancellationToken ct = default)
    {
        return Catch(async () =>
        {
            var movie = (BL.Movies.Movie)dto;
            movie.Id = id;
            movie = await _movieService.Upsert(movie, ct);

            var result = (MovieResponseDto)movie;

            return Ok(result);
        });
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        return Catch(async () =>
        {
            if (!ValidateId(id))
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            await _movieService.Delete(id, ct);
            return NoContent();
        });
    }

    private bool ValidateId(Guid id, [CallerArgumentExpression(nameof(id))] string name = "id")
    {
        if (id == Guid.Empty)
        {
            ModelState.AddModelError(name, $"{name} can not be an empty Guid");
            return false;
        }

        return true;
    }

    [NonAction]
    private async Task<IActionResult> Catch(Func<Task<IActionResult>> action, [CallerMemberName] string actionName = "Unknown")
    {
        try
        {
            return await action.Invoke();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on MoviesController {ActionName} action", actionName);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
