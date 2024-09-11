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

            static void Run(string[] args)
            {
                Console.WriteLine("Starting..."); // #1

                // This will now cause a deadlock because we're blocking on the result
                DeadlockExample()                 // #2
                    .Wait();                      // #4

                Console.WriteLine("Finished."); // This line will never be reached
            }

            static async Task DeadlockExample()
            {
                // Simulate async work
                await Task.Delay(1000);           // #3

                // This line will never be executed because of the deadlock
                Console.WriteLine("This line will never be executed.");
            }

        }
    }
}
