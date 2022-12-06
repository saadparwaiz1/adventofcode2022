using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Sixth : IDay
    {

        private readonly ILogger<Sixth> _logger;
        private readonly static string s_FILE = "Resources/6.txt";

        public Sixth(ILogger<Sixth> logger)
        {
            _logger = logger;
        }

        private async Task<string> Process(int count)
        {
            var sequence = await s_FILE.ReadTextAsync();
            var cache = new List<char>();
            var idx = 0;
            foreach (char c in sequence)
            {
                if (cache.Count() == count)
                {
                    if (cache.Distinct().Count() == count)
                    {
                        return $"{idx}";
                    }
                    cache.RemoveAt(0);
                }
                cache.Add(c);
                idx += 1;
            }
            return "-1";
        }

        public async Task<string?> FirstAsync()
        {
            return await Process(4);
        }

        public async Task<string?> SecondAsync()
        {
            return await Process(14);
        }
    }
}