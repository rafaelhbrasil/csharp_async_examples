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
            Console.WriteLine($"1 Running Task and awaiting the result...");
            try
            {
                await Actions.DoSomethingAndThrowError(message: "Exception awaited");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"2 Exception captured: '{ex.Message}'");
            }
            Console.WriteLine("3 Finished");

            Console.WriteLine($"1 Running Task and NOT awaiting the result...");
            try
            {
                _ = Actions.DoSomethingAndThrowError(message: "Exception NOT awaited");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"2 Exception captured: '{ex.Message}'");
            }
            Console.WriteLine("3 Finished");


            Console.WriteLine($"1 Running Task and NOT awaiting the result, but with a Continuation set...");
            _ = Actions.DoSomethingAndThrowError(message: "Exception NOT awaited")
                .ContinueWith((task) =>
                {
                    Console.WriteLine($"2 Exception captured by continuation: '{task.Exception?.InnerException?.Message}'");
                }, TaskContinuationOptions.OnlyOnFaulted);

            Console.WriteLine("3 Finished");

            await Task.Delay(1000);
        }
    }
}
