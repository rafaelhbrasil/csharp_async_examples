using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSynchronizationContext;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class Deadlock: SyncBenchmarkBase
    {
        public override int Number => 5;

        protected override void InnerRun()
        {
            SingleThreadSynchronizationContext.Run(async () =>
            {
                Console.WriteLine($"Running on Thread {Thread.CurrentThread.ManagedThreadId}");
                var task1 = Actions.DoLongRunningOperation();
                Console.WriteLine($"Task started. Main code still on Thread {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(200); // pretend to do something and give time for task1 to start
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} will be hold until task1 completes");
                task1.Wait();
            });

        }
    }
}
