using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApp20240815.BL.Movies;
using SampleApp20240815.DAL.Db;

namespace SampleApp20240815.DAL.Movie;

public class MovieRepository : IMovieRepository
{
    private readonly SampleAppDbContext _context;

    private DbSet<BL.Movies.Movie> Movies => _context.Set<BL.Movies.Movie>();

    public MovieRepository(SampleAppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbContext));
        _context = dbContext;
    }

    public async Task<IEnumerable<BL.Movies.Movie>> GetAll(CancellationToken ct = default)
    {
        return await Movies.ToListAsync(ct);
    }

    public async Task<BL.Movies.Movie?> Get(Guid id, CancellationToken ct = default)
    {
        return await Movies.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<BL.Movies.Movie> Create(BL.Movies.Movie movie, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(movie);

        movie.Version = DateTimeOffset.UtcNow;
        var entry = Movies.Add(movie);
        await _context.SaveChangesAsync(ct);

        return entry.Entity;
    }

    public async Task<BL.Movies.Movie> Update(BL.Movies.Movie movie, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(movie);

        movie.Version = DateTimeOffset.UtcNow;
        var entry = Movies.Update(movie);
        await _context.SaveChangesAsync(ct);

        return entry.Entity;
    }

    public async Task<int> Delete(Guid id, CancellationToken ct = default)
    {
        return await _context.Set<BL.Movies.Movie>().Where(x => x.Id == id).ExecuteDeleteAsync(ct);
    }
}
