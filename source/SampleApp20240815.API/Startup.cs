using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleApp20240815.BL.Movies;
using SampleApp20240815.DAL.Db;
using SampleApp20240815.DAL.Movie;

namespace SampleApp20240815.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<SampleAppDbContext>(builder =>
        {
            builder.UseSqlServer(_configuration.GetConnectionString("SampleAppDb"));
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            builder.EnableSensitiveDataLogging(); // for dev only
        });

        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IMovieRepository, MovieRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        if (!env.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(builder => builder.MapControllers());
    }
}
