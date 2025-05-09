using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using SampleApp20240815.Utils;

namespace SampleApp20240815.Benchmarks;

[MemoryDiagnoser(false)]
[SuppressMessage("ReSharper", "ConvertToConstant.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class Base64IdBenchmarks
{
    public static readonly Guid TestIdAsGuid = Guid.Parse("f7edaebf-2bf6-44fb-9b8d-630168cd5378");
    public static readonly string TestIdAsString = "v67t9_Yr-0SbjWMBaM1TeA";

    [Benchmark] public string GuidToB64IdManualNotOptimized() => Base64Id.GuidToB64IdManualNotOptimized(TestIdAsGuid);
    [Benchmark] public string GuidToB64IdManualOptimized() => Base64Id.GuidToB64IdManualOptimized(TestIdAsGuid);
    [Benchmark] public string GuidToB64IdStd() => Base64Id.GuidToB64IdStd(TestIdAsGuid);

    [Benchmark] public Guid B64IdToGuidManualNotOptimized() => Base64Id.B64IdToGuidManualNotOptimized(TestIdAsString);
    [Benchmark] public Guid B64IdToGuidManualOptimized() => Base64Id.B64IdToGuidManualOptimized(TestIdAsString);
    [Benchmark] public Guid B64IdToGuidStd() => Base64Id.B64IdToGuidStd(TestIdAsString);
}
