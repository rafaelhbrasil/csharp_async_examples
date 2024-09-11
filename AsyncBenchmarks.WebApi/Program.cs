using AsyncBenchmarks.Utils;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/sync", () =>
{
    Thread.Sleep(1000);
    return Actions.DoSomethingSynchronous();
});

app.MapGet("/async", async () =>
{
    await Task.Delay(1000);
    return await Actions.DoSomethingAsync();
});

app.Run();



