using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class ValueTaskVsTask : AsyncBenchmarkBase
    {
        public override int Number => 9;

        protected override async Task InnerRun()
        {
            int times = 50000;
            Console.WriteLine($"Running Task {times} times...");
            var timeTasks = await CustomTimer.ComputeTime(async () => await Actions.DoSomethingAsync(), times, true);

            Console.WriteLine($"Running ValueTask {times} times...");
            var timeThreads = await CustomTimer.ComputeTime(async () => await Actions.DoSomethingAsyncValueTask(), times, true);
        }
    }
}
