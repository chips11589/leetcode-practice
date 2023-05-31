using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Coding;
using Xunit;
using Xunit.Abstractions;

namespace CodingTest
{
    public class SolutionBenchmarkTests
    {
        private readonly ITestOutputHelper _output;

        public SolutionBenchmarkTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void FindActivityNotifications()
        {
            var logger = new AccumulationLogger();

            var config = ManualConfig.Create(DefaultConfig.Instance)
                .AddLogger(logger)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);

            BenchmarkRunner.Run<SolutionBenchmark>(config);

            _output.WriteLine(logger.GetLog());
        }
    }
}
