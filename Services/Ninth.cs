using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Ninth : IDay
    {

        private readonly ILogger<Ninth> _logger;
        private readonly static string s_FILE = "Resources/9.txt";

        public Ninth(ILogger<Ninth> logger)
        {
            _logger = logger;
        }

        public async Task<List<(string dir, int mag)>> ProcessAsync()
        {
            var lines = await s_FILE.ReadLinesAsync();
            return lines.Select(s =>
            {
                var chars = s.Split(" ");
                return (chars.First(), int.Parse(chars.Skip(1).FirstOrDefault("")));
            }).ToList();

        }

        public (int x, int y) Move((int x, int y) position, (string dir, int mag) move)
        {
            return move.dir switch
            {
                "U" => (position.x, position.y + move.mag),
                "D" => (position.x, position.y - move.mag),
                "R" => (position.x + move.mag, position.y),
                "L" => (position.x - move.mag, position.y),
                _ => position
            };
        }

        public async Task<string?> FirstAsync()
        {
            return "";
        }
        public async Task<string?> SecondAsync()
        {
            return "";
        }
    }
}