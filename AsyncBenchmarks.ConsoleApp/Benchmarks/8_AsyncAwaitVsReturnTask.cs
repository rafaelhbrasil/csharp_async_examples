using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class AsyncAwaitVsReturnTask : AsyncBenchmarkBase
    {
        public override int Number => 8;

        protected override async Task InnerRun()
        {
            int times = 500000;
            Console.WriteLine($"Running a Task awaiting the inner task {times} times...");
            var elapsedTime1 = await CustomTimer.ComputeTime(async () => await Actions.DoSomethingAsync(), times, true);

            Console.WriteLine($"Running a Task returning the inner Task {times} times...");
            var elapsedTime2 = await CustomTimer.ComputeTime(() => Actions.DoSomethingAsync(), times, true);
        }
    }
}
