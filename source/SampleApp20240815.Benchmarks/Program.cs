using BenchmarkDotNet.Running;

namespace SampleApp20240815.Benchmarks;

public static class Program
{
    public static void Main()
    {
        // System.Console.WriteLine(Guider.GuidToB64IdManualNotOptimized(GuiderBenchmarks.TestIdAsGuid));
        // System.Console.WriteLine(Guider.GuidToB64IdManualOptimized(GuiderBenchmarks.TestIdAsGuid));
        // System.Console.WriteLine(Guider.GuidToB64IdStd(GuiderBenchmarks.TestIdAsGuid));
        // System.Console.WriteLine(Guider.GuidToB64IdStdOptimized(GuiderBenchmarks.TestIdAsGuid));
        // System.Console.WriteLine(Guider.GuidToB64IdStdOptimized2(GuiderBenchmarks.TestIdAsGuid));
        //
        // System.Console.WriteLine(Guider.B64IdToGuidManualNotOptimized(GuiderBenchmarks.TestIdAsString));
        // System.Console.WriteLine(Guider.B64IdToGuidManualOptimized(GuiderBenchmarks.TestIdAsString));
        // System.Console.WriteLine(Guider.B64IdToGuidStd(GuiderBenchmarks.TestIdAsString));
        BenchmarkRunner.Run<Base64IdBenchmarks>();
    }
}
