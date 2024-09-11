// See https://aka.ms/new-console-template for more information
using AsyncBenchmarks.ConsoleApp.Benchmarks;
using AsyncBenchmarks.Utils;
using System.Reflection;

var _items = new Dictionary<string, Benchmark>();

ListBenchmarks();
var option = Console.ReadLine();
while (option != "0")
{
    if (_items.TryGetValue(option!, out var benchmark))
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
                                        && typeof(BenchmarkBase).IsAssignableFrom(t));

        foreach (var type in derivedTypes)
        {
            var instance = Activator.CreateInstance(type) as BenchmarkBase;
            newItems.Add(new Benchmark(instance!.Number, type.Name, instance!.Run, true));
        }
        newItems = [.. newItems.OrderBy(a => a.Number)];
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


