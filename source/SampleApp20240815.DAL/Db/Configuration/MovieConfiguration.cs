﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleApp20240815.DAL.Db.Configuration;

public class MovieConfiguration : IEntityTypeConfiguration<BL.Movies.Movie>
{
    public void Configure(EntityTypeBuilder<BL.Movies.Movie> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Version).ValueGeneratedOnAdd().HasValueGenerator<DateTimeOffsetUtcGenerator>();
        builder.Property(x => x.Title).IsRequired();

        builder.HasKey(x => x.Id).IsClustered(false);
        builder.HasIndex(x => x.Version).IsClustered(true);
    }
}
