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
            var ctSource = new CancellationTokenSource();
            ctSource.Token.Register(() => Console.WriteLine("Cancellation requested."));

            _ = Actions.DoLongRunningOperation(ctSource.Token);
            
            Console.WriteLine($"Press ENTER to cancel.");
            Console.ReadLine();
            ctSource.Cancel();
        }
    }
}
