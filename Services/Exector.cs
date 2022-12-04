using Microsoft.Extensions.Logging;

namespace Advent.Code.Services
{
    public class Executor : IExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Executor> _logger;

        public Executor(ILogger<Executor> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(Type type)
        {
            var _day = _serviceProvider.GetService(type);
            if (_day != null)
            {
                var day = (IDay)_day;
                var tasks = await Task.WhenAll(day.FirstAsync(), day.SecondAsync());
                _logger.LogInformation($"{type} - First: {tasks[0]}");
                _logger.LogInformation($"{type} - Second: {tasks[1]}");
            }
            else
            {
                _logger.LogCritical($"No Service Exists For Type {type}");
            }
        }
    }
}