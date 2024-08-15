using System;

namespace SampleApp20240815.BL.Movies;

public class Movie : IIdentifiable, IVersionable
{
    public Guid Id { get; set; }

    public DateTimeOffset Version { get; set; }

    public int Year { get; set; }

    public string Title { get; set; } = string.Empty;
}
