using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class FirstDay : IDay
    {
        private readonly ILogger<FirstDay> _logger;
        private readonly static string s_FILE = "Resources/1.txt";

        public FirstDay(ILogger<FirstDay> logger)
        {
            _logger = logger;
        }

        private async Task<IEnumerable<int?>?> ProcessAsync()
        {
            var input = await s_FILE.ReadTextAsync();
            _logger.LogDebug($"File {s_FILE} is valid? {input != null}");
            var groups = input?.Split("\n\n").Select(group => group?.Split("\n"));
            var parsed = groups?.Select(group => group?.Select(s =>
            {
                int value;
                if (int.TryParse(s, out value))
                {
                    return value;
                }
                _logger.LogWarning($"Parsing Int Failed {s}");
                return 0;
            }).Sum());
            return parsed;
        }

        public async Task<string?> FirstAsync()
        {
            var groups = await ProcessAsync();
            return $"{groups?.Max()}";
        }
        public async Task<string?> SecondAsync()
        {
            var groups = await ProcessAsync();
            return $"{groups?.OrderByDescending(s => s)?.Take(3)?.Sum()}";
        }
    }
}