using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class TaskCancellation : SyncBenchmarkBase
    {
        public override int Number => 3;

        protected override void InnerRun()
        {
            Console.WriteLine($"Press ENTER to cancel.");
            var ctSource = new CancellationTokenSource();
            ctSource.Token.Register(() => Console.WriteLine("Cancellation requested."));

            _ = Actions.DoLongRunningOperation(ctSource.Token);
            
            Console.ReadLine();
            ctSource.Cancel();

            Console.WriteLine($"Finished.");
        }
    }
}
