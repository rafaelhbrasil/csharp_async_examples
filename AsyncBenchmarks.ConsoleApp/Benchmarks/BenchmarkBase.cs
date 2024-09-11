using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public abstract class BenchmarkBase
    {
        public abstract int Number { get; }

        public abstract Task Run();
    }
}
