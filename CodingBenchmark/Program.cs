using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Coding;

var logger = new AccumulationLogger();

var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .AddLogger(logger)
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

BenchmarkRunner.Run<SolutionBenchmark>(config);

Console.Write(logger);