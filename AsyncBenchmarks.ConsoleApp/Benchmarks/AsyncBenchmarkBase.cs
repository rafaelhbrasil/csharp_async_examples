using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public abstract class AsyncBenchmarkBase: BenchmarkBase
    {
        protected abstract Task InnerRun();

        public override async Task Run()
        {
            Actions.ResetCounter();
            Console.WriteLine($"Running {GetType().Name}...");
            await InnerRun();
            Console.WriteLine($"Finished {GetType().Name}.");
        }
    }
}
