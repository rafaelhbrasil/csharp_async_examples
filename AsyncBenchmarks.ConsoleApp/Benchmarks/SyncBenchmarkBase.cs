using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public abstract class SyncBenchmarkBase: BenchmarkBase
    {
        protected abstract void InnerRun();

        public void Run()
        {
            Actions.ResetCounter();
            Console.WriteLine($"Running {GetType().Name}...");
            InnerRun();
            Console.WriteLine($"Finished {GetType().Name}.");
        }
    }
}
