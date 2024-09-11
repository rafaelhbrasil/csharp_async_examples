using AsyncBenchmarks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.ConsoleApp.Benchmarks
{
    public class EnsuringSuccessRuns : AsyncBenchmarkBase
    {
        public override int Number => 2;

        protected override async Task InnerRun()
        {
            Console.WriteLine($"Running Task and awaiting the result...");
            try
            {
                await Actions.DoSomethingAndThrowError("Exeption awaited");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception captured: '{ex.Message}'");
            }

            Console.WriteLine($"Running Task and NOT awaiting the result...");
            try
            {
                _ = Actions.DoSomethingAndThrowError("Exeption NOT awaited");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception captured: '{ex.Message}'");
            }

            Console.WriteLine($"Running Task and NOT awaiting the result, but with a Continuation set...");
            _ = Actions.DoSomethingAndThrowError("Exeption NOT awaited")
                .ContinueWith((task) =>
                {
                    Console.WriteLine($"Exception captured by continuation: '{task.Exception?.InnerException?.Message}'");
                }, TaskContinuationOptions.OnlyOnFaulted);

            await Task.Delay(1000);
        }
    }
}
