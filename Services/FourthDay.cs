using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class FourthDay : IDay
    {
        private readonly ILogger<FourthDay> _logger;
        private readonly static string s_FILE = "Resources/4.txt";

        public FourthDay(ILogger<FourthDay> logger)
        {
            _logger = logger;
        }

        private int ToInt(string? s)
        {
            int value;
            if (int.TryParse(s, out value))
            {
                return value;
            }
            _logger.LogWarning($"Parsing Int Failed {s}");
            return 0;
        }

        private async Task<IEnumerable<((int low, int high) first, (int low, int high) second)>?> ProcessAsync()
        {
            var input = await s_FILE.ReadLinesAsync();
            _logger.LogDebug($"File {s_FILE} is valid? {input != null}");
            return input?.Select(s =>
            {
                var values = s.Split(",");
                var first = values?.FirstOrDefault()?.Split("-");
                var second = values?.Skip(1)?.FirstOrDefault()?.Split("-");
                var firstLow = ToInt(first?.FirstOrDefault());
                var firstHigh = ToInt(first?.Skip(1)?.FirstOrDefault());
                var secondLow = ToInt(second?.FirstOrDefault());
                var secondHigh = ToInt(second?.Skip(1)?.FirstOrDefault());
                return ((firstLow, firstHigh), (secondLow, secondHigh));
            });
        }
        private bool DecideFirst(((int low, int high) first, (int low, int high) second) pair)
        {
            return (
                    (pair.first.low <= pair.second.low && pair.second.high <= pair.first.high)
                    ||
                    (pair.second.low <= pair.first.low && pair.first.high <= pair.second.high)
            );
        }


        private bool DecideSecond(((int low, int high) first, (int low, int high) second) pair)
        {
            return (
                    (pair.first.low <= pair.second.high && pair.second.high <= pair.first.high)
                    ||
                    (pair.second.low <= pair.first.high && pair.first.high <= pair.second.high)
            );
        }

        public async Task<string?> FirstAsync()
        {
            var groups = await ProcessAsync();
            return $"{groups?.Where(s => DecideFirst(s)).Count()}";
        }
        public async Task<string?> SecondAsync()
        {
            var groups = await ProcessAsync();
            return $"{groups?.Where(s => DecideSecond(s)).Count()}";
        }
    }
}