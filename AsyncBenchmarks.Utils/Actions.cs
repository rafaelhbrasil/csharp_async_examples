namespace AsyncBenchmarks.Utils
{
    public static class Actions
    {

        public static int ExecutionCounter { get; private set; } = 0;              // ignoring thread safety for now because it's not the purpose here
        public static void ResetCounter() => ExecutionCounter = 0;
        
        public static Task<int> DoSomethingAsync()
        {
            return Task.FromResult(++ExecutionCounter);
        }
        
        public static ValueTask<int> DoSomethingAsyncValueTask()
        {
            return ValueTask.FromResult(++ExecutionCounter);
        }

        public static int DoSomethingSynchronous()
        {
            return ++ExecutionCounter;
        }

        public static int DoSomethingWithNewThread()
        {
            var thread = new Thread(() => ++ExecutionCounter);
            thread.Start();
            thread.Join();
            return ExecutionCounter;
        }

        public static async Task DoSomethingAndThrowError(string message)
        {
            await Task.Delay(100);
            throw new ExampleException(message);
        }

        public static async Task DoLongRunningOperation(CancellationToken ct = default)
        {
            Console.WriteLine("Long running operation started.");
            await Task.Delay(100);
            for (int i = 1; i < 5; i++)
            {
                if (ct.IsCancellationRequested) break;
                await Task.Delay(1000, ct);
            }
            Console.WriteLine("Long running operation completed.");
        }
    }
}
