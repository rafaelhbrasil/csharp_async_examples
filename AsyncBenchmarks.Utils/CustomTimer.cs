using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBenchmarks.Utils
{
    public static class CustomTimer
    {
        public static int ComputeTime(Action action, int repeatTimes = 1, bool displayTime = false)
        {
            var start = DateTime.Now;
            for (int i = 0; i < repeatTimes; i++)
                action();
            var end = DateTime.Now;
            var elapsed = (int)(end - start).TotalMilliseconds;
            if (displayTime)
                Console.WriteLine($"Elapsed time: {elapsed} ms");
            return elapsed;
        }

        public static async Task<int> ComputeTime(Func<Task> action, int repeatTimes = 1, bool displayTime = false)
        {
            var start = DateTime.Now;
            for (int i = 0; i < repeatTimes; i++)
                await action();
            var end = DateTime.Now;
            var elapsed = (int)(end - start).TotalMilliseconds;
            if (displayTime)
                Console.WriteLine($"Elapsed time: {elapsed} ms");
            return elapsed;
        }
    }
}
