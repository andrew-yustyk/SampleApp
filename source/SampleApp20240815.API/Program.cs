using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SampleApp20240815.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebHostBuilderFactory.CreateSampleAppHostBuilder(args);
        var app = builder.Build();

        await app.RunAsync();
    }
}
