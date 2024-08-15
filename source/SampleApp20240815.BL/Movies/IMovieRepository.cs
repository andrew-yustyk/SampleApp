﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp20240815.BL.Movies;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAll(CancellationToken ct = default);

    Task<Movie?> Get(Guid id, CancellationToken ct = default);

    Task<Movie> Create(Movie movie, CancellationToken ct = default);

    Task<Movie> Update(Movie movie, CancellationToken ct = default);

    Task<int> Delete(Guid id, CancellationToken ct = default);
}