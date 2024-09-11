using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class Concurrency : AsyncBenchmarkBase
    {
        public override int Number => 4;

        protected override async Task InnerRun()
        {
            var tasks = Enumerable.Range(0, 200)
                                  .Select(x => Task.Run(Actions.DoSomethingAsync));
            await Task.WhenAll(tasks);

            Console.WriteLine($"All tasks completed. Counter={Actions.ExecutionCounter}");
        }
    }
}
