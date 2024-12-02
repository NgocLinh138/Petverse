using BenchmarkDotNet.Running;
using Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<MapperAndManualMappingBenchmark>();
    }
}