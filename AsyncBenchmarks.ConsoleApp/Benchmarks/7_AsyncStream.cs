using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class AsyncStream : AsyncBenchmarkBase
    {
        public override int Number => 7;

        protected override async Task InnerRun()
        {
            await foreach (var number in GenerateNumbersAsync())
            {
                Console.WriteLine($"Received number: {number}");
            }
        }

        static async IAsyncEnumerable<int> GenerateNumbersAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(1000); // Simulate asynchronous work
                yield return i; // Yield each number one at a time
            }
        }
    }

    
}
