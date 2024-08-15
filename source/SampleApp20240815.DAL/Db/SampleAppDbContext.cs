using Microsoft.EntityFrameworkCore;
using SampleApp20240815.DAL.Db.Configuration;

namespace SampleApp20240815.DAL.Db;

public class SampleAppDbContext : DbContext
{
    public SampleAppDbContext()
    {
    }

    public SampleAppDbContext(DbContextOptions<SampleAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
    }
}
