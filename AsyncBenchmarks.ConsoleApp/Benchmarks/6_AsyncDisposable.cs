using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class AsyncDisposable : AsyncBenchmarkBase
    {
        public override int Number => 6;

        protected override async Task InnerRun()
        {
            await using var disposableClass1 = new DisposableClass();
            await Task.Delay(500); // Simulate some work

            // or

            await using (var disposableClass2 = new DisposableClass())
            {
                await Task.Delay(500); // Simulate some work
            }
        }
    }

    internal class DisposableClass : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("Async disposal started");
            await Task.Delay(1000);
            Console.WriteLine("Async disposal ended");
        }
    }
}
