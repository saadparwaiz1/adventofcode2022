using System.Text;
using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class ThirdDay : IDay
    {
        private readonly ILogger<ThirdDay> _logger;
        private readonly static string s_FILE = "Resources/3.txt";

        public ThirdDay(ILogger<ThirdDay> logger)
        {
            _logger = logger;
        }

        public int Convert(char c)
        {
            return char.IsUpper(c) ? c - 'A' + 27 : c - 'a' + 1;
        }

        private async Task<IEnumerable<int>?> ProcessFirstAsync()
        {
            var input = await s_FILE.ReadLinesAsync();
            _logger.LogDebug($"File {s_FILE} is valid? {input != null}");
            return input?.Select(s =>
            {
                var first = s.Take(s.Length / 2);
                var second = s.Skip(s.Length / 2);
                var common = first.Intersect(second).FirstOrDefault();
                return Convert(common);
            });
        }

        private async Task<IEnumerable<int>?> ProcessSecondAsync()
        {
            var input = await s_FILE.ReadLinesAsync();
            _logger.LogDebug($"File {s_FILE} is valid? {input != null}");
            return input?.Chunk(3)
                    .Select(s => s.Aggregate((acc, next) => string.Join("", acc.Intersect(next))))
                    .Select(s => Convert(s.FirstOrDefault()));
        }


        public async Task<string?> FirstAsync()
        {
            var groups = await ProcessFirstAsync();
            return $"{groups?.Sum()}";
        }
        public async Task<string?> SecondAsync()
        {
            var groups = await ProcessSecondAsync();
            return $"{groups?.Sum()}";
        }
    }
}