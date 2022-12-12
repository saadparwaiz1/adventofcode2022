using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

using Advent.Code.Models;
using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Eleventh : IDay
    {

        private readonly ILogger<Eleventh> _logger;
        private readonly static string s_FILE = "Resources/11.txt";

        public Eleventh(ILogger<Eleventh> logger)
        {
            _logger = logger;
        }

        public async Task<Dictionary<int, Monkey>> ProcessAsync()
        {
            var text = await s_FILE.ReadTextAsync();
            var monkeyDicts = text.Split("\n\n").Select(monkey =>
            {
                return monkey
                            .Split("\n")
                            .Select(s => s.Replace(" ", "").Replace("\t", "").Split(":"))
                            .ToDictionary(s => s.First(), s => s.Skip(1).First());
            });

            var monkeys = monkeyDicts.Select(
                (monkey, idx) => new Monkey(
                    idx,
                    monkey["Startingitems"].Split(",").ToList(),
                    monkey["Operation"],
                    int.Parse(monkey["Iftrue"].Replace("throwtomonkey", "")),
                    int.Parse(monkey["Iffalse"].Replace("throwtomonkey", "")),
                    int.Parse(monkey["Test"].Replace("divisibleby", ""))
                )
            );
            return monkeys.ToDictionary(key => key.Id, value => value);
        }

        public async Task<string?> FirstAsync()
        {
            var monkeys = await ProcessAsync();
            _logger.LogInformation(JsonConvert.SerializeObject(monkeys));
            return "";
        }
        public async Task<string?> SecondAsync()
        {
            return "";
        }
    }
}