using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Advent.Code.Services;


var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                    {
                        services.AddLogging(factory => factory.SetMinimumLevel(LogLevel.Information));
                        services.AddSingleton<IExecutor, Executor>();
                        services.AddSingleton<First>();
                        services.AddSingleton<Second>();
                        services.AddSingleton<Third>();
                        services.AddSingleton<Fourth>();
                        services.AddSingleton<Fifth>();
                        services.AddSingleton<Sixth>();
                        services.AddSingleton<Seventh>();
                        services.AddSingleton<Eighth>();
                        services.AddSingleton<Ninth>();
                        services.AddSingleton<Tenth>();
                    }).Build();

var executor = host.Services.GetRequiredService<IExecutor>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();
var watch = new System.Diagnostics.Stopwatch();
watch.Start();
logger.LogInformation("Starting Execution");
var types = new Type[] { typeof(First), typeof(Second), typeof(Third), typeof(Fourth), typeof(Fifth), typeof(Sixth), typeof(Seventh), typeof(Eighth), typeof(Tenth), typeof(Ninth) };
var tasks = types.Select(t => executor.ExecuteAsync(t));
await Task.WhenAll(tasks);
watch.Stop();
logger.LogInformation($"Ending Execution - Total Time: {watch.Elapsed.TotalSeconds}");