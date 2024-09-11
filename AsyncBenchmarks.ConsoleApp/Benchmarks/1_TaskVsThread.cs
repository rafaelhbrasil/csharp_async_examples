using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class TaskVsThread: AsyncBenchmarkBase
    {
        public override int Number => 1;

        protected override async Task InnerRun()
        {
            int times = 500;
            Console.WriteLine($"Running Task {times} times...");
            var timeTasks = await CustomTimer.ComputeTime(async () => await Actions.DoSomethingAsync(), times, true);

            Console.WriteLine($"Running Thread {times} times...");
            var timeThreads = CustomTimer.ComputeTime(() => Actions.DoSomethingWithNewThread(), times, true);
        }
    }
}
