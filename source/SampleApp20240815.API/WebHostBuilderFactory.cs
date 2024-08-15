using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleApp20240815.API;

public static class WebHostBuilderFactory
{
    public static IHostBuilder CreateSampleAppHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.UseDefaultServiceProvider(opts => opts.ValidateScopes = opts.ValidateOnBuild = true);
        builder.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        return builder;
    }
}
