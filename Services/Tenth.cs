using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Tenth : IDay
    {
        private readonly ILogger<Tenth> _logger;
        private readonly static int[] s_VALUEs = { 20, 60, 100, 140, 180, 220 };
        private readonly static string s_FILE = "Resources/10.txt";

        public Tenth(ILogger<Tenth> logger)
        {
            _logger = logger;
        }

        public async Task<List<(string type, string count)>> ProcessAsync()
        {
            var lines = await s_FILE.ReadLinesAsync();
            return lines.Select(s =>
            {
                var chars = s.Split(" ");
                return (chars.First(), chars.Skip(1).FirstOrDefault(""));
            }).ToList();

        }
        public async Task<Dictionary<int, int>> GetCyclesChanges()
        {
            var cycles = new Dictionary<int, int>();
            var cycle = 1;
            var instructions = await ProcessAsync();
            foreach (var insturction in instructions)
            {
                if (!insturction.type.Equals("noop"))
                {
                    cycles.Add(cycle + 2, int.Parse(insturction.count));
                    cycle += 2;
                }
                else
                {
                    cycle += 1;
                }
            }
            return cycles;
        }

        public async Task<Dictionary<int, int>> GetCycles()
        {
            var changes = await GetCyclesChanges();
            var cycles = new Dictionary<int, int>();
            var currValue = 1;
            for (int i = 1; i <= 240; i++)
            {
                if (changes.ContainsKey(i))
                {
                    currValue += changes[i];
                }
                cycles.Add(i, currValue);
            }
            return cycles;
        }

        public async Task<string?> FirstAsync()
        {
            var cycles = await GetCycles();
            var signals = s_VALUEs.Zip(s_VALUEs).Select(pair => cycles[pair.First] * pair.Second);
            return $"{signals.Sum()}";
        }
        public async Task<string?> SecondAsync()
        {
            var cycles = await GetCycles();
            var lines = new List<string>();
            for (int i = 1; i <= 240; i++)
            {
                var position = (i - 1) % 40;
                var cycle = cycles[i];
                if (position == cycle || position == cycle - 1 || position == cycle + 1)
                {
                    lines.Add("#");
                }
                else
                {
                    lines.Add(".");
                }
            }
            var chunks = string.Join("\n", lines.Chunk(40).Select(s => string.Join("", s)));
            return $"\n{chunks}";
        }

    }
}