// See https://aka.ms/new-console-template for more information
using AsyncBenchmarks.ConsoleApp.Benchmarks;
using AsyncBenchmarks.Utils;
using System.Reflection;

var _items = new Dictionary<string, Benchmark>();

ListBenchmarks();
var option = Console.ReadLine();
while (option != "0")
{
    if (_items.TryGetValue(option, out var benchmark))
    {
        await benchmark.Run();
        Console.WriteLine($"{Environment.NewLine} Press any key to proceed... {Environment.NewLine}");
    }
    else
    {
        Console.WriteLine("Invalid option");
    }


    ListBenchmarks();
    option = Console.ReadLine();
}


void ListBenchmarks()
{
    if (_items is not { Count: > 0 })
    {
        var newItems = new List<Benchmark>();

        var derivedTypes = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(t => t.IsClass && !t.IsAbstract
                                        && (typeof(AsyncBenchmarkBase).IsAssignableFrom(t)
                                            || typeof(SyncBenchmarkBase).IsAssignableFrom(t)));

        foreach (var type in derivedTypes)
        {
            if (type.IsAssignableTo(typeof(AsyncBenchmarkBase)))
            {
                var instance = (AsyncBenchmarkBase)Activator.CreateInstance(type);
                //items.Add((items.Count+1).ToString(), new(type.Name, instance!.Run, true));
                newItems.Add(new Benchmark(instance!.Number, type.Name, instance!.Run, true));
            }
            else if (type.IsAssignableTo(typeof(SyncBenchmarkBase)))
            {
                var instance = (SyncBenchmarkBase)Activator.CreateInstance(type);
                //items.Add((items.Count+1).ToString(), new(type.Name, instance!.Run, false));
                newItems.Add(new Benchmark(instance!.Number, type.Name, async () => await Task.Run(instance!.Run), false));
            }

        }
        newItems = newItems.OrderBy(a => a.Number).ToList();
        foreach (var item in newItems)
            _items.Add(item.Number.ToString(), item);
    }

    Console.WriteLine($"Type the desired option:");
    foreach (var key in _items.Keys)
    {
        Console.WriteLine($"{key}. {_items[key].Name}");
    }
    Console.WriteLine($"or {0} to quit");
}

public record Benchmark(int Number, string Name, Func<Task> Run, bool IsAsync);

//public class Test
//{
//    public async Task DoSomething()
//    {
//        var task1 = Actions.DoSomethingAsync();
//        var result1 = await task1;

//        var task2 = Actions.DoSomethingAsync();
//        var result2 = task2.Result;

//        var task3 = Actions.DoSomethingAsync();
//        task3.Wait();
//        var result3 = task3.Result;

//        var task4 = Actions.DoSomethingAsync();
//        var result4 = task4.ConfigureAwait(false).GetAwaiter().GetResult();
//    }

//}

//public class Test2
//{
//    public async Task DoSomething()
//    {
//        var task1 = Actions.DoSomethingAsync();
//        // continue immediately
//        await task1;
//        // continue after task1 is done
//    }

//    public void DoSomething2()
//    {
//        var task2 = Actions.DoSomethingAsync();
//        task2.ContinueWith(t => {
//            // continue after task2 is done
//        });
//        // continue immediately
//    }

//}

