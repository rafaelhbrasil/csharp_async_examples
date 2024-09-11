using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;

// credits: https://gist.github.com/Xorcerer/6505468#file-singlethreadsynchronizationcontext-cs
namespace TestSynchronizationContext
{
    public class SingleThreadSynchronizationContext : SynchronizationContext
    {

        BlockingCollection<Action> _queue = new BlockingCollection<Action>();

        public override void Post(SendOrPostCallback d, object state)
        {
            Console.WriteLine("Posted from thread: {0}, {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            _queue.Add(() => d(state));
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            throw new NotImplementedException();
        }

        public void InvokeCallbacks()
        {
            Action a;
            while (_queue.TryTake(out a, Timeout.Infinite))
                a();
            Console.WriteLine("InvokeCallbacks exited.");
        }

        public void Complete()
        {
            _queue.CompleteAdding();
            Console.WriteLine("Completed.");
        }

        public static void Run(Func<Task> func)
        {
            var prevCtx = SynchronizationContext.Current;
            try
            {
                var syncCtx = new SingleThreadSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(syncCtx);

                Task main = func();
                main.ContinueWith(t => syncCtx.Complete(), TaskScheduler.Default);

                syncCtx.InvokeCallbacks();

                main.GetAwaiter().GetResult();
            }
            finally { SynchronizationContext.SetSynchronizationContext(prevCtx); }
        }
    }
}
