using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Advent.Code.Services;


var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                    {
                        services.AddLogging(factory => factory.SetMinimumLevel(LogLevel.Information));
                        services.AddSingleton<IExecutor, Executor>();
                        services.AddSingleton<FirstDay>();
                        services.AddSingleton<SecondDay>();
                        services.AddSingleton<ThirdDay>();
                        services.AddSingleton<FourthDay>();
                        services.AddSingleton<FifthDay>();
                    }).Build();

var executor = host.Services.GetRequiredService<IExecutor>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();
var watch = new System.Diagnostics.Stopwatch();
watch.Start();
logger.LogInformation("Starting Execution");
var types = new Type[] { typeof(FirstDay), typeof(SecondDay), typeof(ThirdDay), typeof(FourthDay), typeof(FifthDay) };
var tasks = types.Select(t => executor.ExecuteAsync(t));
await Task.WhenAll(tasks);
watch.Stop();
logger.LogInformation($"Ending Execution - Total Time: {watch.Elapsed.TotalSeconds}");